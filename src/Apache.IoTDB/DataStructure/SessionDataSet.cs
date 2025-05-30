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
using System.Threading.Tasks;
using Thrift;

namespace Apache.IoTDB.DataStructure
{
    public class SessionDataSet : System.IDisposable
    {
        private readonly long _queryId;
        private readonly long _statementId;
        private readonly string _sql;
        private readonly List<string> _columnNames;
        private readonly Dictionary<string, int> _columnNameIndexMap;
        private readonly Dictionary<int, int> _duplicateLocation;
        private readonly List<string> _columnTypeLst;
        private TSQueryDataSet _queryDataset;
        private readonly byte[] _currentBitmap;
        private readonly int _columnSize;
        private List<ByteBuffer> _valueBufferLst, _bitmapBufferLst;
        private ByteBuffer _timeBuffer;
        private readonly ConcurrentClientQueue _clientQueue;
        private Client _client;
        private int _rowIndex;
        private RowRecord _cachedRowRecord;
        private bool _isClosed = false;
        private bool disposedValue;
        private RpcDataSet _rpcDataSet;
        private string _zoneId;

        private string TimestampStr => "Time";
        private int StartIndex => 2;
        private int Flag => 0x80;
        private int DefaultTimeout => 10000;
        public int FetchSize { get; set; }
        public int RowCount { get; set; }
        public SessionDataSet(string sql, TSExecuteStatementResp resp, Client client, ConcurrentClientQueue clientQueue, long statementId, string zoneId)
        {
            _clientQueue = clientQueue;
            _client = client;
            _sql = sql;
            _queryDataset = resp.QueryDataSet;
            _queryId = resp.QueryId;
            _statementId = statementId;
            _columnSize = resp.Columns.Count;
            _currentBitmap = new byte[_columnSize];
            _columnNames = new List<string>();
            _timeBuffer = new ByteBuffer(_queryDataset.Time);
            // column name -> column location
            _columnNameIndexMap = new Dictionary<string, int>();
            _columnTypeLst = new List<string>();
            _duplicateLocation = new Dictionary<int, int>();
            _valueBufferLst = new List<ByteBuffer>();
            _bitmapBufferLst = new List<ByteBuffer>();
            // some internal variable
            _rowIndex = 0;
            RowCount = _queryDataset.Time.Length / sizeof(long);

            _columnNames = resp.Columns;
            _columnTypeLst = resp.DataTypeList;
            _zoneId = zoneId;

            _rpcDataSet = new RpcDataSet(
                _sql, _columnNames, _columnTypeLst, _columnNameIndexMap, resp.IgnoreTimeStamp,
                resp.MoreData, _queryId, _statementId, _client, _client.SessionId, resp.QueryResult, FetchSize,
                DefaultTimeout, _zoneId, resp.ColumnIndex2TsBlockColumnIndexList
            );
        }
        public bool HasNext()
        {
            if (_rpcDataSet.HasCachedRecord) return true;
            return Next();
        }

        public bool Next() => _rpcDataSet.Next();
        public bool IsNull(string columnName) => _rpcDataSet.IsNullByColumnName(columnName);
        public bool IsNullByIndex(int columnIndex) => _rpcDataSet.IsNullByIndex(columnIndex);

        public bool GetBooleanByIndex(int columnIndex) => _rpcDataSet.GetBooleanByIndex(columnIndex);
        public bool GetBoolean(string columnName) => _rpcDataSet.GetBoolean(columnName);

        public double GetDoubleByIndex(int columnIndex) => _rpcDataSet.GetDoubleByIndex(columnIndex);
        public double GetDouble(string columnName) => _rpcDataSet.GetDouble(columnName);

        public float GetFloatByIndex(int columnIndex) => _rpcDataSet.GetFloatByIndex(columnIndex);
        public float GetFloat(string columnName) => _rpcDataSet.GetFloat(columnName);

        public int GetIntByIndex(int columnIndex) => _rpcDataSet.GetIntByIndex(columnIndex);
        public int GetInt(string columnName) => _rpcDataSet.GetInt(columnName);

        public long GetLongByIndex(int columnIndex) => _rpcDataSet.GetLongByIndex(columnIndex);
        public long GetLong(string columnName) => _rpcDataSet.GetLong(columnName);

        public object GetObjectByIndex(int columnIndex) => _rpcDataSet.GetObjectByIndex(columnIndex);
        public object GetObject(string columnName) => _rpcDataSet.GetObject(columnName);

        public string GetStringByIndex(int columnIndex) => _rpcDataSet.GetStringByIndex(columnIndex);
        public string GetString(string columnName) => _rpcDataSet.GetString(columnName);

        public DateTime GetTimestampByIndex(int columnIndex) => _rpcDataSet.GetTimestampByIndex(columnIndex);
        public DateTime GetTimestamp(string columnName) => _rpcDataSet.GetTimestamp(columnName);

        public DateTime GetDateByIndex(int columnIndex) => _rpcDataSet.GetDateByIndex(columnIndex);
        public DateTime GetDate(string columnName) => _rpcDataSet.GetDate(columnName);

        public Binary GetBlobByIndex(int columnIndex) => _rpcDataSet.GetBinaryByIndex(columnIndex);
        public Binary GetBlob(string columnName) => _rpcDataSet.GetBinary(columnName);

        public int FindColumn(string columnName) => _rpcDataSet.FindColumn(columnName);

        public IReadOnlyList<string> GetColumnNames() => _rpcDataSet._columnNameList;
        public IReadOnlyList<string> GetColumnTypes() => _rpcDataSet._columnTypeList;

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
                    var status = await _client.ServiceClient.closeOperationAsync(req);
                }
                catch (TException e)
                {
                    throw new TException("Operation Handle Close Failed", e);
                }
                finally
                {

                    await _rpcDataSet.Close();
                    _clientQueue.Add(_client);
                    _client = null;
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
                        this.Close().Wait();
                    }
                    catch
                    {
                    }
                }
                _queryDataset = null;
                _timeBuffer = null;
                _valueBufferLst = null;
                _bitmapBufferLst = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
