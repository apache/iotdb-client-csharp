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
using Apache.IoTDB.DataStructure;

namespace Apache.IoTDB.Samples
{
    public partial class SessionPoolTest
    {

        public async Task TestInsertRecord()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            int status;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);

            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]), TSDataType.TEXT,
                TSEncoding.PLAIN, Compressor.UNCOMPRESSED);

            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.BOOLEAN, TSEncoding.PLAIN, Compressor.UNCOMPRESSED);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[3]),
                TSDataType.INT32, TSEncoding.PLAIN, Compressor.UNCOMPRESSED);
            System.Diagnostics.Debug.Assert(status == 0);
            var measures = new List<string>
                {testMeasurements[1], testMeasurements[2], testMeasurements[3]};
            var values = new List<object> { "test_text", true, (int)123 };
            var tasks = new List<Task<int>>();
            var start_ms = DateTime.Now.Ticks / 10000;
            for (var timestamp = 1; timestamp <= fetchSize * processedSize; timestamp++)
            {
                var rowRecord = new RowRecord(timestamp, values, measures);
                var task = session_pool.InsertRecordAsync(
                    string.Format("{0}.{1}", testDatabaseName, testDevice), rowRecord);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            var end_ms = DateTime.Now.Ticks / 10000;
            Console.WriteLine(string.Format("total insert aligned record time is {0}", end_ms - start_ms));
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestInsertRecordAsync Passed");
        }
        public async Task TestInsertStringRecord()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[0]), TSDataType.TEXT,
                TSEncoding.PLAIN, Compressor.UNCOMPRESSED);

            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.UNCOMPRESSED);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.UNCOMPRESSED);
            System.Diagnostics.Debug.Assert(status == 0);
            var measurements = new List<string>
                {testMeasurements[0], testMeasurements[1], testMeasurements[2]};
            var values = new List<string> { "test_text1", "test_text2", "test_text3" };
            var tasks = new List<Task<int>>();
            var start_ms = DateTime.Now.Ticks / 10000;
            for (var timestamp = 1; timestamp <= fetchSize * processedSize; timestamp++)
            {
                var task = session_pool.InsertStringRecordAsync(
                    string.Format("{0}.{1}", testDatabaseName, testDevice), measurements, values, timestamp);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            var end_ms = DateTime.Now.Ticks / 10000;
            Console.WriteLine(string.Format("total insert string record time is {0}", end_ms - start_ms));
            var res = await session_pool.ExecuteQueryStatementAsync("select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            var res_cnt = 0;
            while (res.Next())
            {
                res_cnt++;
            }
            Console.WriteLine(res_cnt + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_cnt == fetchSize * processedSize);
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestInsertStringRecordAsync Passed");
        }
        public async Task TestInsertStrRecord()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.INT32, TSEncoding.PLAIN, Compressor.UNCOMPRESSED);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.INT32, TSEncoding.PLAIN, Compressor.UNCOMPRESSED);
            System.Diagnostics.Debug.Assert(status == 0);

            var measures = new List<string> { testMeasurements[1], testMeasurements[2] };
            var values = new List<object> { (int)1, (int)2 };
            var rowRecord = new RowRecord(1, values, measures);
            status = await session_pool.InsertRecordAsync(
                string.Format("{0}.{1}", testDatabaseName, testDevice), rowRecord);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<2");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();

            var tasks = new List<Task<int>>();
            // large data test
            var rowRecords = new List<RowRecord>() { };
            for (var timestamp = 2; timestamp <= fetchSize * processedSize; timestamp++)
                rowRecords.Add(new RowRecord(timestamp, values, measures));

            for (var timestamp = 2; timestamp <= fetchSize * processedSize; timestamp++)
            {
                var task = session_pool.InsertRecordAsync(
                    string.Format("{0}.{1}", testDatabaseName, testDevice), rowRecords[timestamp - 2]);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            var res_count = 0;
            while (res.Next())
            {
                res_count += 1;
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == fetchSize * processedSize);
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestInsertStrRecord Passed!");
        }
        public async Task TestInsertRecords()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.BOOLEAN, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.INT32, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[3]),
                TSDataType.INT64, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[4]),
                TSDataType.DOUBLE, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[5]),
                TSDataType.FLOAT, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[6]), TSDataType.TEXT,
                TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);

            var device_id = new List<string>() { };
            for (var i = 0; i < 3; i++) device_id.Add(string.Format("{0}.{1}", testDatabaseName, testDevice));

            var measurements_lst = new List<List<string>>() { };
            measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>()
            {
                testMeasurements[1],
                testMeasurements[2],
                testMeasurements[3],
                testMeasurements[4]
            });
            measurements_lst.Add(new List<string>()
            {
                testMeasurements[1],
                testMeasurements[2],
                testMeasurements[3],
                testMeasurements[4],
                testMeasurements[5],
                testMeasurements[6]
            });
            var values_lst = new List<List<object>>() { };
            values_lst.Add(new List<object>() { true, (int)123 });
            values_lst.Add(new List<object>() { true, (int)123, (long)456, (double)1.1 });
            values_lst.Add(new List<object>()
                {true, (int) 123, (long) 456, (double) 1.1, (float) 10001.1, "test_record"});
            var timestamp_lst = new List<long>() { 1, 2, 3 };
            var rowRecords = new List<RowRecord>() { };
            for (var i = 0; i < 3; i++)
            {
                var rowRecord = new RowRecord(timestamp_lst[i], values_lst[i], measurements_lst[i]);
                rowRecords.Add(rowRecord);
            }

            status = await session_pool.InsertRecordsAsync(device_id, rowRecords);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            Console.WriteLine(status);

            // large data test
            device_id = new List<string>() { };
            rowRecords = new List<RowRecord>() { };
            var tasks = new List<Task<int>>();
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                device_id.Add(string.Format("{0}.{1}", testDatabaseName, testDevice));
                rowRecords.Add(new RowRecord(timestamp, new List<object>() { true, (int)123 },
                    new List<string>() { testMeasurements[1], testMeasurements[2] }));
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertRecordsAsync(device_id, rowRecords));
                    device_id = new List<string>() { };
                    rowRecords = new List<RowRecord>() { };
                }
            }

            Task.WaitAll(tasks.ToArray());
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));

            var record_count = fetchSize * processedSize;

            res.ShowTableNames();
            var res_count = 0;
            while (res.Next())
            {
                res_count += 1;
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == record_count);
            System.Diagnostics.Debug.Assert(status == 0);

            string sql = string.Format("select {0}, {1}, {2} from ", testMeasurements[3], testMeasurements[1], testMeasurements[2]) + string.Format("{0}.{1}", testDatabaseName, testDevice);
            res = await session_pool.ExecuteQueryStatementAsync(sql);
            res.ShowTableNames();
            RowRecord row = null;
            while (res.Next())
            {
                row = res.GetRow();
                break;
            }

            Console.WriteLine($"{testDatabaseName}.{testDevice}.{row.Measurements[1]}  {testMeasurements[3]}");
            System.Diagnostics.Debug.Assert($"{testDatabaseName}.{testDevice}.{testMeasurements[3]}" == row.Measurements[1]);
            System.Diagnostics.Debug.Assert($"{testDatabaseName}.{testDevice}.{testMeasurements[1]}" == row.Measurements[2]);
            System.Diagnostics.Debug.Assert($"{testDatabaseName}.{testDevice}.{testMeasurements[2]}" == row.Measurements[3]);

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertRecords Passed!");
        }
        public async Task TestInsertStringRecords()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]), TSDataType.TEXT,
                TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]), TSDataType.TEXT,
                TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);

            var device_id = new List<string>() { };
            for (var i = 0; i < 3; i++) device_id.Add(string.Format("{0}.{1}", testDatabaseName, testDevice));

            var measurements_lst = new List<List<string>>() { };
            measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
            var values_lst = new List<List<string>>() { };
            values_lst.Add(new List<string>() { "test1", "test2" });
            values_lst.Add(new List<string>() { "test3", "test4" });
            values_lst.Add(new List<string>() { "test5", "test6" });
            var timestamp_lst = new List<long>() { 1, 2, 3 };

            status = await session_pool.InsertStringRecordsAsync(device_id, measurements_lst, values_lst, timestamp_lst);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();

            // large data test
            device_id = new List<string>() { };
            measurements_lst = new List<List<string>>() { };
            values_lst = new List<List<string>>() { };
            timestamp_lst = new List<long>() { };
            var tasks = new List<Task<int>>();
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                device_id.Add(string.Format("{0}.{1}", testDatabaseName, testDevice));
                measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
                values_lst.Add(new List<string>() { "test" + timestamp, "test" + timestamp });
                timestamp_lst.Add(timestamp);
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertStringRecordsAsync(device_id, measurements_lst, values_lst, timestamp_lst));
                    device_id = new List<string>() { };
                    measurements_lst = new List<List<string>>() { };
                    values_lst = new List<List<string>>() { };
                    timestamp_lst = new List<long>() { };
                }
            }

            Task.WaitAll(tasks.ToArray());
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            res.ShowTableNames();
            var record_count = fetchSize * processedSize;
            var res_count = 0;
            while (res.Next())
            {
                Console.WriteLine(res.Next());
                res_count += 1;
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == record_count);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertStringRecords Passed!");
        }
        public async Task TestInsertRecordsOfOneDevice()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.BOOLEAN, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.INT32, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[3]),
                TSDataType.INT64, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[4]),
                TSDataType.DOUBLE, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[5]),
                TSDataType.FLOAT, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[6]), TSDataType.TEXT,
                TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements_lst = new List<List<string>>() { };
            measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>()
            {
                testMeasurements[1],
                testMeasurements[2],
                testMeasurements[3],
                testMeasurements[4]
            });
            measurements_lst.Add(new List<string>()
            {
                testMeasurements[1],
                testMeasurements[2],
                testMeasurements[3],
                testMeasurements[4],
                testMeasurements[5],
                testMeasurements[6]
            });
            var values_lst = new List<List<object>>() { };
            values_lst.Add(new List<object>() { true, (int)123 });
            values_lst.Add(new List<object>() { true, (int)123, (long)456, (double)1.1 });
            values_lst.Add(new List<object>()
                {true, (int) 123, (long) 456, (double) 1.1, (float) 10001.1, "test_record"});
            var timestamp_lst = new List<long>() { 1, 2, 3 };
            var rowRecords = new List<RowRecord>() { };
            for (var i = 0; i < 3; i++)
            {
                var rowRecord = new RowRecord(timestamp_lst[i], values_lst[i], measurements_lst[i]);
                rowRecords.Add(rowRecord);
            }

            status = await session_pool.InsertRecordsOfOneDeviceAsync(device_id, rowRecords);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            // large data test
            rowRecords = new List<RowRecord>() { };
            var tasks = new List<Task<int>>();
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                rowRecords.Add(new RowRecord(timestamp, new List<object>() { true, (int)123 },
                    new List<string>() { testMeasurements[1], testMeasurements[2] }));
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertRecordsOfOneDeviceAsync(device_id, rowRecords));
                    rowRecords = new List<RowRecord>() { };
                }
            }

            Task.WaitAll(tasks.ToArray());
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            var res_count = 0;
            while (res.Next())
            {
                res_count += 1;
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == fetchSize * processedSize);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertRecordsOfOneDevice Passed!");
        }
        public async Task TestInsertStringRecordsOfOneDevice()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[0]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            System.Diagnostics.Debug.Assert(status == 0);

            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements_lst = new List<List<string>>() { };
            measurements_lst.Add(new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });

            var values_lst = new List<List<string>>() { };
            values_lst.Add(new List<string>() { "test1", "test2", "test3" });
            values_lst.Add(new List<string>() { "test4", "test5", "test6" });
            values_lst.Add(new List<string>() { "test7", "test8", "test9" });

            var timestamp_lst = new List<long>() { 1, 2, 3 };

            status = await session_pool.InsertStringRecordsOfOneDeviceAsync(device_id, timestamp_lst, measurements_lst, values_lst);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            // large data test
            values_lst = new List<List<string>>() { };
            var tasks = new List<Task<int>>();
            measurements_lst = new List<List<string>>() { };
            timestamp_lst = new List<long>() { };
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                values_lst.Add(new List<string>() { "test1", "test2" });
                measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
                timestamp_lst.Add(timestamp);
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertStringRecordsOfOneDeviceAsync(device_id, timestamp_lst, measurements_lst, values_lst));
                    values_lst = new List<List<string>>() { };
                    measurements_lst = new List<List<string>>() { };
                    timestamp_lst = new List<long>() { };
                }
            }

            Task.WaitAll(tasks.ToArray());
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            var res_count = 0;
            while (res.Next())
            {
                res_count += 1;
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == fetchSize * processedSize);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertStringRecordsOfOneDevice Passed!");
        }

        public async Task TestInsertRecordsWithAllType()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            var measurements = new List<string>
            {
                "boolean_measurement",
                "int32_measurement",
                "int64_measurement",
                "float_measurement",
                "double_measurement",
                "text_measurement",
                "timestamp_measurement",
                "date_measurement",
                "blob_measurement",
                "string_measurement"
            };

            var dataTypes = new List<string>
            {
                "BOOLEAN",
                "INT32",
                "INT64",
                "FLOAT",
                "DOUBLE",
                "TEXT",
                "TIMESTAMP",
                "DATE",
                "BLOB",
                "STRING"
            };


            var values1 = new List<object>
            {
                true, 123, 123456789L, 1.23f, 1.23456789, "iotdb", ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds(), DateTime.Today, new byte[] {0x01, 0x02}, "string1"
            };
            var values2 = new List<object>
            {
                false, 456, 987654321L, 4.56f, 9.87654321, "iotdb2", ((DateTimeOffset)DateTime.Now.AddSeconds(1)).ToUnixTimeMilliseconds(), DateTime.Today.AddDays(1), new byte[] {0x03, 0x04}, "string2"
            };
            var values3 = new List<object>
            {
                true, 789, 123123123L, 7.89f, 7.89101112, "iotdb3", ((DateTimeOffset)DateTime.Now.AddSeconds(2)).ToUnixTimeMilliseconds(), DateTime.Today.AddDays(2), new byte[] {0x05, 0x06}, "string3"
            };

            var rowRecord1 = new RowRecord(1, values1, measurements, dataTypes);
            var rowRecord2 = new RowRecord(2, values2, measurements, dataTypes);
            var rowRecord3 = new RowRecord(3, values3, measurements, dataTypes);

            var device_id = new List<string> { string.Format("{0}.{1}", testDatabaseName, testDevice), string.Format("{0}.{1}", testDatabaseName, testDevice), string.Format("{0}.{1}", testDatabaseName, testDevice) };
            var rowRecords = new List<RowRecord> { rowRecord1, rowRecord2, rowRecord3 };

            status = await session_pool.InsertRecordsAsync(device_id, rowRecords);
            System.Diagnostics.Debug.Assert(status == 0);

            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            res.ShowTableNames();
            var res_count = 0;
            while (res.Next())
            {
                Console.WriteLine(res.GetRow());
                res_count += 1;
            }

            await res.Close();
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertRecordsWithAllType Passed!");
        }
    }
}
