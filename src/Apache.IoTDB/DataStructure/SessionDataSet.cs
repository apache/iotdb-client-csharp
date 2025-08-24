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
        private readonly List<string> _columnTypeLst;
        private Client _client;
        private bool _isClosed = false;
        private bool disposedValue;
        private RpcDataSet _rpcDataSet;
        private string _zoneId;
        private readonly ConcurrentClientQueue _clientQueue;

        private string TimestampStr => "Time";
        private int StartIndex => 2;
        private int Flag => 0x80;
        private int DefaultTimeout => 10000;
        public int FetchSize { get; set; }
        public SessionDataSet(
            string sql, List<string> ColumnNameList, List<string> ColumnTypeList,
            Dictionary<string, int> ColumnNameIndexMap, long QueryId, long statementId, Client client, List<byte[]> QueryResult,
            bool IgnoreTimeStamp, bool MoreData, string zoneId, List<int> ColumnIndex2TsBlockColumnIndexList, ConcurrentClientQueue clientQueue
        )
        {
            _client = client;
            _sql = sql;
            _queryId = QueryId;
            _statementId = statementId;
            _columnNameIndexMap = ColumnNameIndexMap;

            _columnNames = ColumnNameList;
            _columnTypeLst = ColumnTypeList;
            _zoneId = zoneId;
            _clientQueue = clientQueue;
            
            _rpcDataSet = new RpcDataSet(
                _sql, _columnNames, _columnTypeLst, _columnNameIndexMap, IgnoreTimeStamp,
                MoreData, _queryId, _statementId, _client, _client.SessionId, QueryResult, FetchSize,
                DefaultTimeout, _zoneId, ColumnIndex2TsBlockColumnIndexList
            );
        }
        public bool HasNext() => _rpcDataSet.Next();
        public RowRecord Next() => _rpcDataSet.GetRow();
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

        public int RowCount() => _rpcDataSet._tsBlockSize;
        public void ShowTableNames()
        {
            IReadOnlyList<string> columns = GetColumnNames();
            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();
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
                    var status = await _client.ServiceClient.closeOperation(req);
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
