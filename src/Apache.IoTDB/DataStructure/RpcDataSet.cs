using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thrift;

namespace Apache.IoTDB.DataStructure
{
    public class RpcDataSet : IDisposable
    {
        private readonly long _queryId;
        private readonly long _statementId;
        private readonly string _sql;
        private readonly List<string> _columnNames;
        private readonly Dictionary<string, int> _columnNameIndexMap;
        private readonly Dictionary<int, int> _duplicateLocation;
        private readonly List<string> _columnTypeLst;
        private readonly ConcurrentClientQueue _clientQueue;
        private Client _client;
        private bool _isClosed = false;
        private bool disposedValue;
        private TSBlock _currentBlock;
        private bool _hasMoreData = true;
        private const int DefaultTimeout = 10000;
        public int FetchSize { get; set; } = 1024;
        public int RowCount { get; set; }

        public RpcDataSet(string sql, TSExecuteStatementResp resp, Client client,
            ConcurrentClientQueue clientQueue, long statementId)
        {
            _clientQueue = clientQueue;
            _client = client;
            _sql = sql;
            _queryId = resp.QueryId;
            _statementId = statementId;
            _columnNames = resp.Columns;
            _columnTypeLst = resp.DataTypeList;
            _columnNameIndexMap = new Dictionary<string, int>();
            _duplicateLocation = new Dictionary<int, int>();

            InitializeColumnMappings(resp);
            InitializeFirstBlock(resp.QueryDataSet);
        }

        private void InitializeColumnMappings(TSExecuteStatementResp resp)
        {
            int deduplicateIdx = 0;
            var columnToFirstIndexMap = new Dictionary<string, int>();

            for (var i = 0; i < _columnNames.Count; i++)
            {
                var columnName = _columnNames[i];
                if (_columnNameIndexMap.ContainsKey(columnName))
                {
                    _duplicateLocation[i] = columnToFirstIndexMap[columnName];
                }
                else
                {
                    columnToFirstIndexMap[columnName] = i;
                    _columnNameIndexMap[columnName] = deduplicateIdx++;
                }
            }
        }

        private void InitializeFirstBlock(TSQueryDataSet queryDataSet)
        {
            _currentBlock = new TSBlock(
                queryDataSet,
                _columnNames,
                _columnTypeLst,
                _duplicateLocation
            );
            RowCount = _currentBlock.RowCount;
        }

        public List<string> ColumnNames => new List<string>(_columnNames);

        public bool HasNext()
        {
            if (_currentBlock == null) return false;

            if (_currentBlock.HasNext()) return true;

            if (!_hasMoreData) return false;

            // 尝试获取新数据块
            return FetchNextBlock();
        }

        public RowRecord Next()
        {
            if (!HasNext()) return null;
            
            return _currentBlock.Next();
        }

        public RowRecord GetRow()
        {
            return _currentBlock._cachedRowRecord;
        }

        private bool FetchNextBlock()
        {
            if (!_hasMoreData) return false;

            var req = new TSFetchResultsReq(
                _client.SessionId,
                _sql,
                FetchSize,
                _queryId,
                true
            )
            { Timeout = DefaultTimeout };

            try
            {
                var resp = _client.ServiceClient.fetchResultsAsync(req)
                    .ConfigureAwait(false).GetAwaiter().GetResult();

                _hasMoreData = resp.MoreData;
                if (resp.HasResultSet)
                {
                    _currentBlock = new TSBlock(
                        resp.QueryDataSet,
                        _columnNames,
                        _columnTypeLst,
                        _duplicateLocation
                    );
                    RowCount = _currentBlock.RowCount;
                    return _currentBlock.HasNext();
                }
                return false;
            }
            catch (TException e)
            {
                throw new TException("Fetch results failed", e);
            }
        }

        public async Task Close()
        {
            if (!_isClosed)
            {
                var req = new TSCloseOperationReq(_client.SessionId)
                {
                    QueryId = _queryId,
                    StatementId = _statementId
                };

                try
                {
                    await _client.ServiceClient.closeOperationAsync(req);
                }
                catch (TException e)
                {
                    throw new TException("Operation close failed", e);
                }
                finally
                {
                    _clientQueue.Add(_client);
                    _client = null;
                    _isClosed = true;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        Close().Wait();
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

        public void ShowTableNames()
        {
            var str = GetColumnNames().Aggregate("", (current, name) => current + $"{name}\t\t");
            Console.WriteLine(str);
        }

        private List<string> GetColumnNames()
        {
            var lst = new List<string> { "Time" };
            lst.AddRange(_columnNames);
            return lst;
        }
    }
}