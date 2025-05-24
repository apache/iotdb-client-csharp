using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thrift;

namespace Apache.IoTDB.DataStructure
{
    public class RpcDataSet : System.IDisposable
    {
        private const string TimestampColumnName = "Time";
        private const string DefaultTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        private readonly string _sql;
        private bool _isClosed;
        private readonly Client _client;
        private readonly List<string> _columnNameList = new List<string>();
        private readonly List<string> _columnTypeList = new List<string>();
        private readonly Dictionary<string, int> _columnOrdinalMap = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _columnName2TsBlockColumnIndexMap = new Dictionary<string, int>();
        private readonly List<int> _columnIndex2TsBlockColumnIndexList = new List<int>();
        private readonly TSDataType[] _dataTypeForTsBlockColumn;
        private int _fetchSize;
        private long _timeout;
        private bool _hasCachedRecord;
        private bool _lastReadWasNull;

        private int _columnSize;
        private long _sessionId;
        private long _queryId;
        private long _statementId;
        private long _time;
        private bool _ignoreTimestamp;
        private bool _moreData;

        private List<byte[]> _queryResult;
        private TsBlock _curTsBlock;
        private int _queryResultSize;
        private int _queryResultIndex;
        private int _tsBlockSize;
        private int _tsBlockIndex;

        private TimeZoneInfo _zoneId;
        private string _timeFormat;
        private int _timeFactor;
        private string _timePrecision;
        private bool disposedValue;

        public RpcDataSet(string sql, List<string> columnNameList, List<string> columnTypeList,
            Dictionary<string, int> columnNameIndex, bool ignoreTimestamp, bool moreData, long queryId,
            long statementId, Client client, long sessionId, List<byte[]> queryResult, int fetchSize,
            long timeout, string zoneId, string timeFormat, List<int> columnIndex2TsBlockColumnIndexList)
        {
            _sql = sql;
            _client = client;
            _fetchSize = fetchSize;
            _timeout = timeout;
            _moreData = moreData;
            _columnSize = columnNameList.Count;
            _sessionId = sessionId;
            _queryId = queryId;
            _statementId = statementId;
            _ignoreTimestamp = ignoreTimestamp;

            int columnStartIndex = 1;
            int resultSetColumnSize = columnNameList.Count;
            int startIndexForColumnIndex2TsBlockColumnIndexList = 0;

            if (!ignoreTimestamp)
            {
                _columnNameList.Add(TimestampColumnName);
                _columnTypeList.Add("INT64");
                _columnName2TsBlockColumnIndexMap[TimestampColumnName] = -1;
                _columnOrdinalMap[TimestampColumnName] = 1;

                if (columnIndex2TsBlockColumnIndexList != null)
                {
                    columnIndex2TsBlockColumnIndexList.Insert(0, -1);
                    startIndexForColumnIndex2TsBlockColumnIndexList = 1;
                }
                columnStartIndex++;
                resultSetColumnSize++;
            }

            _columnNameList.AddRange(columnNameList);
            _columnTypeList.AddRange(columnTypeList);

            if (columnIndex2TsBlockColumnIndexList == null)
            {
                columnIndex2TsBlockColumnIndexList = new List<int>();
                if (!ignoreTimestamp)
                {
                    startIndexForColumnIndex2TsBlockColumnIndexList = 1;
                    columnIndex2TsBlockColumnIndexList.Add(-1);
                }
                for (int i = 0; i < columnNameList.Count; i++)
                    columnIndex2TsBlockColumnIndexList.Add(i);
            }

            int tsBlockColumnSize = columnIndex2TsBlockColumnIndexList.Max() + 1;
            _dataTypeForTsBlockColumn = new TSDataType[tsBlockColumnSize];

            for (int i = 0; i < columnNameList.Count; i++)
            {
                int tsBlockColumnIndex = columnIndex2TsBlockColumnIndexList[startIndexForColumnIndex2TsBlockColumnIndexList + i];
                if (tsBlockColumnIndex != -1)
                {
                    TSDataType columnType = GetDataTypeByStr(columnTypeList[i]);
                    _dataTypeForTsBlockColumn[tsBlockColumnIndex] = columnType;
                }

                if (!_columnName2TsBlockColumnIndexMap.ContainsKey(columnNameList[i]))
                {
                    _columnOrdinalMap[columnNameList[i]] = i + columnStartIndex;
                    _columnName2TsBlockColumnIndexMap[columnNameList[i]] = tsBlockColumnIndex;
                }
            }

            _queryResult = queryResult;
            _queryResultSize = queryResult?.Count ?? 0;
            _queryResultIndex = 0;
            _tsBlockSize = 0;
            _tsBlockIndex = -1;

            _zoneId = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            _timeFormat = timeFormat;

            if (columnIndex2TsBlockColumnIndexList.Count != _columnNameList.Count)
                throw new ArgumentException("Column index list size mismatch");

            _columnIndex2TsBlockColumnIndexList = columnIndex2TsBlockColumnIndexList;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        this.Close().Wait();
                    }
                    catch
                    {
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task Close()
        {
            if (_isClosed) return;

            var closeRequest = new TSCloseOperationReq
            {
                SessionId = _sessionId,
                StatementId = _statementId,
                QueryId = _queryId
            };

            try
            {
                var status = await _client.ServiceClient.closeOperationAsync(closeRequest);
            }
            catch (TException e)
            {
                throw new TException("Operation Handle Close Failed", e);
            }
            _isClosed = true;
        }

        public async Task<bool> Next()
        {
            if (HasCachedBlock())
            {
                _lastReadWasNull = false;
                ConstructOneRow();
                return true;
            }

            if (HasCachedByteBuffer())
            {
                ConstructOneTsBlock();
                ConstructOneRow();
                return true;
            }

            if (_moreData)
            {
                bool hasResultSet = FetchResults();
                if (hasResultSet && HasCachedByteBuffer())
                {
                    ConstructOneTsBlock();
                    ConstructOneRow();
                    return true;
                }
            }

            await Close();
            return false;
        }

        private bool FetchResults()
        {
            if (_isClosed)
                throw new InvalidOperationException("Dataset closed");

            var req = new TSFetchResultsReq
            {
                SessionId = _sessionId,
                Statement = _sql,
                FetchSize = _fetchSize,
                QueryId = _queryId,
                IsAlign = true,
                Timeout = _timeout
            };
            
            try
            {
                var task = _client.ServiceClient.fetchResultsAsync(req);

                var resp = task.ConfigureAwait(false).GetAwaiter().GetResult();

                if (!resp.HasResultSet)
                {
                    Close().Wait();
                    return false;
                }

                _queryResultIndex = 0;
                _queryResultSize = _queryResult?.Count ?? 0;
                _tsBlockSize = 0;
                _tsBlockIndex = -1;
                return true;
            }
            catch (TException e)
            {
                throw new TException("Cannot fetch result from server, because of network connection", e);
            }
        }

        private bool HasCachedBlock()
        {
            return _curTsBlock != null && _tsBlockIndex < _tsBlockSize - 1;
        }

        private bool HasCachedByteBuffer()
        {
            return _queryResult != null && _queryResultIndex < _queryResultSize;
        }

        private void ConstructOneRow()
        {
            _tsBlockIndex++;
            _hasCachedRecord = true;
            _time = _curTsBlock.GetTimeByIndex(_tsBlockIndex);
        }

        private void ConstructOneTsBlock()
        {
            _lastReadWasNull = false;
            byte[] curTsBlockBytes = _queryResult[_queryResultIndex];
            _queryResultIndex++;
            _curTsBlock = TsBlock.Deserialize(new ByteBuffer(curTsBlockBytes));
            _tsBlockIndex = -1;
            _tsBlockSize = _curTsBlock.PositionCount;
        }

        // 其他Get方法类似实现，篇幅限制省略部分代码

        private TSDataType GetDataTypeByStr(string typeStr)
        {
            return typeStr switch
            {
                "BOOLEAN" => TSDataType.BOOLEAN,
                "INT32" => TSDataType.INT32,
                "INT64" => TSDataType.INT64,
                "FLOAT" => TSDataType.FLOAT,
                "DOUBLE" => TSDataType.DOUBLE,
                "TEXT" => TSDataType.TEXT,
                "STRING" => TSDataType.STRING,
                "BLOB" => TSDataType.BLOB,
                "TIMESTAMP" => TSDataType.TIMESTAMP,
                "DATE" => TSDataType.DATE,
                _ => TSDataType.NONE
            };
        }
    }
}