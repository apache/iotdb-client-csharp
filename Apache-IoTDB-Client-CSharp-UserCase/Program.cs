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
using System.Threading;
using System.Threading.Tasks;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;

namespace Apache.IoTDB.UserCase
{
    class Program
    {
        static string host = "localhost";
        static int port = 6667;
        static int pool_size = 2;


        static async Task OpenAndCloseSessionPool()
        {
            var session_pool = new SessionPool(host, port, pool_size);
            await session_pool.Open(false);
            if (session_pool.IsOpen())
            {
                Console.WriteLine("SessionPool open success");
            }
            else
            {
                Console.WriteLine("SessionPool open failed");
            }
            await session_pool.Close();
        }

        static async Task CreateTimeseries()
        {
            var session_pool = new SessionPool(host, port, pool_size);
            await session_pool.Open(false);

            await session_pool.DeleteDatabaseAsync("root.ln.wf01.wt01");
            var status = await session_pool.CreateTimeSeries("root.ln.wf01.wt01.status", TSDataType.BOOLEAN, TSEncoding.PLAIN, Compressor.SNAPPY);
            status = await session_pool.CreateTimeSeries("root.ln.wf01.wt01.temperature", TSDataType.DOUBLE, TSEncoding.PLAIN, Compressor.SNAPPY);
            status = await session_pool.CreateTimeSeries("root.ln.wf01.wt01.hardware", TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);

            await session_pool.Close();
        }

        static async Task InsertRecord()
        {
            var session_pool = new SessionPool(host, port, pool_size);
            await session_pool.Open(false);
            long timestamp = 1;
            var values = new List<object> { true, (double)1.1, "test" };
            var measures = new List<string> { "status", "temperature", "hardware" };
            var rowRecord = new RowRecord(timestamp, values, measures);
            var status = await session_pool.InsertRecordAsync("root.ln.wf01.wt01", rowRecord);

            await session_pool.Close();
        }

        static async Task InsertTablet()
        {
            var session_pool = new SessionPool(host, port, pool_size);
            await session_pool.Open(false);
            var device_id = "root.ln.wf01.wt01";
            var measurement_lst = new List<string> { "status", "temperature", "hardware" };
            var value_lst = new List<List<object>>
            {
                new() {true, (double)1.1, "test"},
                new() {false, (double)2.2, "test2"},
                new() {true, (double)3.3, "test3"}
            };
            var timestamp_lst = new List<long> { 1, 2, 3 };
            var datatype_lst = new List<TSDataType> { TSDataType.BOOLEAN, TSDataType.DOUBLE, TSDataType.TEXT };
            var tablet = new Tablet(device_id, measurement_lst, datatype_lst, value_lst, timestamp_lst);
            var status = await session_pool.InsertTabletAsync(tablet);
            await session_pool.Close();
        }

        static async Task ExecuteQueryStatement()
        {
            var session_pool = new SessionPool(host, port, pool_size);
            await session_pool.Open(false);
            var res = await session_pool.ExecuteQueryStatementAsync("select * from root.ln.wf01.wt01");
            res.ShowTableNames();
            while (res.Next())
            {
                Console.WriteLine(res.GetRow());
            }
            await res.Close();
            await session_pool.Close();
        }

        static async Task Main(string[] args)
        {
            await OpenAndCloseSessionPool();
            await CreateTimeseries();
            await InsertRecord();
            await InsertTablet();
            await ExecuteQueryStatement();
        }
    }

}
