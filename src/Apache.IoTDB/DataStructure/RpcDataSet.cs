/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public readonly List<string> _columnNameList = new List<string>();
        public readonly List<string> _columnTypeList = new List<string>();
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
        public int _tsBlockSize;
        private int _tsBlockIndex;
        private TimeZoneInfo _zoneId;
        private int _timeFactor;
        private string _timePrecision;
        private bool disposedValue;

        public RpcDataSet(string sql, List<string> columnNameList, List<string> columnTypeList,
            Dictionary<string, int> columnNameIndex, bool ignoreTimestamp, bool moreData, long queryId,
            long statementId, Client client, long sessionId, List<byte[]> queryResult, int fetchSize,
            long timeout, string zoneId, List<int> columnIndex2TsBlockColumnIndexList)
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

            if (!_ignoreTimestamp)
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
                if (!_ignoreTimestamp)
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
                    TSDataType columnType = Client.GetDataTypeByStr(columnTypeList[i]);
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

        public bool Next()
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

            Close().Wait();
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
                var task = _client.ServiceClient.fetchResultsV2Async(req);
                var resp = task.ConfigureAwait(false).GetAwaiter().GetResult();

                if (!resp.HasResultSet)
                {
                    Close().Wait();
                    return false;
                }

                // return _queryResult != null && _queryResultIndex < _queryResultSize;
                _queryResult = resp.QueryResult;
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

        public bool HasCachedByteBuffer()
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

        public bool IsIgnoredTimestamp => _ignoreTimestamp;

        public bool IsNullByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return IsNull(index, _tsBlockIndex);
        }

        public bool IsNullByColumnName(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return IsNull(index, _tsBlockIndex);
        }

        private bool IsNull(int index, int rowNum)
        {
            return index >= 0 && _curTsBlock.GetColumn(index).IsNull(rowNum);
        }

        public bool GetBooleanByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetBooleanByTsBlockColumnIndex(index);
        }

        public bool GetBoolean(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetBooleanByTsBlockColumnIndex(index);
        }

        private bool GetBooleanByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (!IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = false;
                return _curTsBlock.GetColumn(tsBlockColumnIndex).GetBoolean(_tsBlockIndex);
            }
            else
            {
                _lastReadWasNull = true;
                return false;
            }
        }

        public double GetDoubleByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetDoubleByTsBlockColumnIndex(index);
        }

        public double GetDouble(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetDoubleByTsBlockColumnIndex(index);
        }

        private double GetDoubleByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (!IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = false;
                return _curTsBlock.GetColumn(tsBlockColumnIndex).GetDouble(_tsBlockIndex);
            }
            else
            {
                _lastReadWasNull = true;
                return 0.0;
            }
        }

        public float GetFloatByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetFloatByTsBlockColumnIndex(index);
        }

        public float GetFloat(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetFloatByTsBlockColumnIndex(index);
        }

        private float GetFloatByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (!IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = false;
                return _curTsBlock.GetColumn(tsBlockColumnIndex).GetFloat(_tsBlockIndex);
            }
            else
            {
                _lastReadWasNull = true;
                return 0.0f;
            }
        }

        public int GetIntByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetIntByTsBlockColumnIndex(index);
        }

        public int GetInt(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetIntByTsBlockColumnIndex(index);
        }

        private int GetIntByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (!IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = false;
                TSDataType dataType = _curTsBlock.GetColumn(tsBlockColumnIndex).GetDataType();
                if (dataType == TSDataType.INT64)
                {
                    long v = _curTsBlock.GetColumn(tsBlockColumnIndex).GetLong(_tsBlockIndex);
                    return (int)v;
                }
                return _curTsBlock.GetColumn(tsBlockColumnIndex).GetInt(_tsBlockIndex);
            }
            else
            {
                _lastReadWasNull = true;
                return 0;
            }
        }

        public long GetLongByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetLongByTsBlockColumnIndex(index);
        }

        public long GetLong(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetLongByTsBlockColumnIndex(index);
        }

        private long GetLongByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (!IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                if (tsBlockColumnIndex == -1) return _curTsBlock.GetTimeByIndex(_tsBlockIndex);
                _lastReadWasNull = false;
                return _curTsBlock.GetColumn(tsBlockColumnIndex).GetLong(_tsBlockIndex);
            }
            else
            {
                _lastReadWasNull = true;
                return 0L;
            }
        }

        public Binary GetBinaryByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetBinaryByTsBlockColumnIndex(index);
        }

        public Binary GetBinary(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetBinaryByTsBlockColumnIndex(index);
        }

        private Binary GetBinaryByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (!IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = false;
                return _curTsBlock.GetColumn(tsBlockColumnIndex).GetBinary(_tsBlockIndex);
            }
            else
            {
                _lastReadWasNull = true;
                return null;
            }
        }

        public object GetObjectByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetObjectByTsBlockIndex(index);
        }

        public object GetObject(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetObjectByTsBlockIndex(index);
        }

        private object GetObjectByTsBlockIndex(int tsBlockColumnIndex)
        {
            CheckRecord();
            if (IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = true;
                return null;
            }

            _lastReadWasNull = false;
            TSDataType dataType = GetDataTypeByTsBlockColumnIndex(tsBlockColumnIndex);

            switch (dataType)
            {
                case TSDataType.BOOLEAN:
                case TSDataType.INT32:
                case TSDataType.INT64:
                case TSDataType.FLOAT:
                case TSDataType.DOUBLE:
                    return _curTsBlock.GetColumn(tsBlockColumnIndex).GetObject(_tsBlockIndex);

                case TSDataType.TIMESTAMP:
                    long timestamp = tsBlockColumnIndex == -1
                        ? _curTsBlock.GetTimeByIndex(_tsBlockIndex)
                        : _curTsBlock.GetColumn(tsBlockColumnIndex).GetLong(_tsBlockIndex);
                    return ConvertToTimestamp(timestamp, _timeFactor);

                case TSDataType.TEXT:
                case TSDataType.STRING:
                    Binary binaryStr = _curTsBlock.GetColumn(tsBlockColumnIndex).GetBinary(_tsBlockIndex);
                    return binaryStr != null ? Encoding.UTF8.GetString(binaryStr.Data) : null;

                case TSDataType.BLOB:
                    return _curTsBlock.GetColumn(tsBlockColumnIndex).GetBinary(_tsBlockIndex);

                case TSDataType.DATE:
                    int value = _curTsBlock.GetColumn(tsBlockColumnIndex).GetInt(_tsBlockIndex);
                    return Int32ToDate(value);

                default:
                    return null;
            }
        }

        public string GetStringByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetStringByTsBlockColumnIndex(index);
        }

        public string GetString(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetStringByTsBlockColumnIndex(index);
        }

        private string GetStringByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            CheckRecord();

            if (tsBlockColumnIndex == -1)
            {
                long timestamp = _curTsBlock.GetTimeByIndex(_tsBlockIndex);
                return timestamp.ToString();
            }

            if (IsNull(tsBlockColumnIndex, _tsBlockIndex))
            {
                _lastReadWasNull = true;
                Console.WriteLine("null");
                return string.Empty;
            }

            _lastReadWasNull = false;
            return GetStringByTsBlockColumnIndexAndDataType(
                tsBlockColumnIndex,
                GetDataTypeByTsBlockColumnIndex(tsBlockColumnIndex));
        }

        private string GetStringByTsBlockColumnIndexAndDataType(int index, TSDataType tsDataType)
        {
            switch (tsDataType)
            {
                case TSDataType.BOOLEAN:
                    bool boolVal = _curTsBlock.GetColumn(index).GetBoolean(_tsBlockIndex);
                    return boolVal.ToString();

                case TSDataType.INT32:
                    int intVal = _curTsBlock.GetColumn(index).GetInt(_tsBlockIndex);
                    return intVal.ToString();

                case TSDataType.INT64:
                    long longVal = _curTsBlock.GetColumn(index).GetLong(_tsBlockIndex);
                    return longVal.ToString();

                case TSDataType.TIMESTAMP:
                    long tsValue = _curTsBlock.GetColumn(index).GetLong(_tsBlockIndex);
                    return FormatDatetime(DefaultTimeFormat, _timePrecision, tsValue, _zoneId);

                case TSDataType.FLOAT:
                    float floatVal = _curTsBlock.GetColumn(index).GetFloat(_tsBlockIndex);
                    return floatVal.ToString("G9");

                case TSDataType.DOUBLE:
                    double doubleVal = _curTsBlock.GetColumn(index).GetDouble(_tsBlockIndex);
                    return doubleVal.ToString("G17");

                case TSDataType.TEXT:
                case TSDataType.STRING:
                    Binary strBytes = _curTsBlock.GetColumn(index).GetBinary(_tsBlockIndex);
                    return strBytes != null ? Encoding.UTF8.GetString(strBytes.Data) : "0";

                case TSDataType.BLOB:
                    Binary blobBytes = _curTsBlock.GetColumn(index).GetBinary(_tsBlockIndex);
                    return blobBytes.ToString().Replace("-", "");

                case TSDataType.DATE:
                    int dateValue = _curTsBlock.GetColumn(index).GetInt(_tsBlockIndex);
                    DateTime date = Int32ToDate(dateValue);
                    return date.ToString("yyyy-MM-dd");

                default:
                    return string.Empty;
            }
        }

        public RowRecord GetRow()
        {
            IReadOnlyList<string> columns = _columnNameList;
            int i = 0;
            List<object> fieldList = new List<Object>();
            long timestamp = 0;
            foreach (string columnName in columns)
            {
                object localfield;
                string typeStr = _columnTypeList[i];
                TSDataType dataType = Client.GetDataTypeByStr(typeStr);

                switch (dataType)
                {
                    case TSDataType.BOOLEAN:
                        localfield = GetBoolean(columnName);
                        break;
                    case TSDataType.INT32:
                        localfield = GetInt(columnName);
                        break;
                    case TSDataType.INT64:
                        localfield = GetLong(columnName);
                        break;
                    case TSDataType.TIMESTAMP:
                        localfield = null;
                        timestamp = GetLong(columnName);
                        break;
                    case TSDataType.FLOAT:
                        localfield = GetFloat(columnName);
                        break;
                    case TSDataType.DOUBLE:
                        localfield = GetDouble(columnName);
                        break;
                    case TSDataType.TEXT:
                    case TSDataType.STRING:
                    case TSDataType.BLOB:
                    case TSDataType.DATE:
                        localfield = GetString(columnName);
                        break;
                    default:
                        string err_msg = "value format not supported";
                        throw new TException(err_msg, null);
                }
                if (localfield != null)
                    fieldList.Add(localfield);
                i += 1;
            }
            return new RowRecord(timestamp, fieldList, _columnNameList);
        }

        public DateTime GetTimestampByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetTimestampByTsBlockColumnIndex(index);
        }

        public DateTime GetTimestamp(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetTimestampByTsBlockColumnIndex(index);
        }

        private DateTime GetTimestampByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            long value = GetLongByTsBlockColumnIndex(tsBlockColumnIndex);
            return ConvertToTimestamp(value, _timeFactor);
        }

        public DateTime GetDateByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetDateByTsBlockColumnIndex(index);
        }

        public DateTime GetDate(string columnName)
        {
            int index = GetTsBlockColumnIndexForColumnName(columnName);
            return GetDateByTsBlockColumnIndex(index);
        }

        private DateTime GetDateByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            int value = GetIntByTsBlockColumnIndex(tsBlockColumnIndex);
            return Int32ToDate(value);
        }

        public TSDataType GetDataTypeByIndex(int columnIndex)
        {
            int index = GetTsBlockColumnIndexForColumnIndex(columnIndex);
            return GetDataTypeByTsBlockColumnIndex(index);
        }

        public TSDataType GetDataType(string columnName)
        {
            if (!_columnName2TsBlockColumnIndexMap.TryGetValue(columnName, out int index))
                throw new ArgumentException($"Column {columnName} not found");

            return GetDataTypeByTsBlockColumnIndex(index);
        }

        private TSDataType GetDataTypeByTsBlockColumnIndex(int tsBlockColumnIndex)
        {
            return tsBlockColumnIndex < 0
                ? TSDataType.TIMESTAMP
                : _dataTypeForTsBlockColumn[tsBlockColumnIndex];
        }

        private DateTime ConvertToTimestamp(long value, double timeFactor)
        {
            long timestamp = (long)(value * timeFactor);
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
        }

        public static DateTime Int32ToDate(int val)
        {
            int year = val / 10000;
            int remaining = val % 10000;
            int month = remaining / 100;
            int day = remaining % 100;

            if (year < 1 || year > 9999)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(val),
                    message: $"Invalid year value: {year}. Year must be between 1-9999"
                );

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(val),
                    message: $"Invalid month value: {month}. Month must be between 1-12"
                );

            int daysInMonth = DateTime.DaysInMonth(year, month);
            if (day < 1 || day > daysInMonth)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(val),
                    message: $"Invalid day value: {day}. Day must be between 1-{daysInMonth} for {year}-{month}"
                );

            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        }

        private string FormatDatetime(string format, string precision, long value, TimeZoneInfo zone)
        {
            DateTime dt = ConvertToTimestamp(value, 1); // 假设timeFactor=1
            DateTime convertedTime = TimeZoneInfo.ConvertTime(dt, zone);
            return convertedTime.ToString(format);
        }

        private int GetTsBlockColumnIndexForColumnName(string columnName)
        {
            if (!_columnName2TsBlockColumnIndexMap.TryGetValue(columnName, out int index))
                throw new ArgumentException($"Column {columnName} not found");
            return index;
        }

        public int FindColumn(string columnName)
        {
            if (!_columnOrdinalMap.TryGetValue(columnName, out int ordinal))
                throw new ArgumentException($"Column {columnName} not found");
            return ordinal;
        }

        public string FindColumnNameByIndex(int columnIndex)
        {
            if (columnIndex <= 0)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index should start from 1");

            if (columnIndex > _columnNameList.Count)
                throw new ArgumentOutOfRangeException(nameof(columnIndex),
                    $"Column index {columnIndex} out of range {_columnNameList.Count}");

            return _columnNameList[columnIndex - 1];
        }

        private int GetTsBlockColumnIndexForColumnIndex(int columnIndex)
        {
            int adjustedIndex = columnIndex - 1;
            if (adjustedIndex < 0 || adjustedIndex >= _columnIndex2TsBlockColumnIndexList.Count)
                throw new ArgumentOutOfRangeException(nameof(columnIndex),
                    $"Index {adjustedIndex} out of range {_columnIndex2TsBlockColumnIndexList.Count}");

            return _columnIndex2TsBlockColumnIndexList[adjustedIndex];
        }

        private void CheckRecord()
        {
            if (_queryResultIndex > _queryResultSize ||
                _tsBlockIndex >= _tsBlockSize ||
                _queryResult == null ||
                _curTsBlock == null)
            {
                throw new InvalidOperationException("No record remains");
            }
        }

        public int GetValueColumnStartIndex() => _ignoreTimestamp ? 0 : 1;

        public int GetColumnSize() => _columnNameList.Count;

        public List<string> GetColumnTypeList() => new List<string>(_columnTypeList);

        public List<string> GetColumnNameTypeList() => new List<string>(_columnTypeList);

        public bool IsClosed() => _isClosed;

        public int FetchSize
        {
            get => _fetchSize;
            set => _fetchSize = value;
        }

        public bool HasCachedRecord
        {
            get => _hasCachedRecord;
            set => _hasCachedRecord = value;
        }

        public bool IsLastReadWasNull() => _lastReadWasNull;

        public long GetCurrentRowTime() => _time;

        public bool IsIgnoreTimestamp() => _ignoreTimestamp;
    }
}
