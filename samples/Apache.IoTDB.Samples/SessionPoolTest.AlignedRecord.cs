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
        public async Task TestInsertAlignedRecord()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            int status;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);

            string prefixPath = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements = new List<string> { testMeasurements[1], testMeasurements[2], testMeasurements[3] };
            var types = new List<TSDataType> { TSDataType.TEXT, TSDataType.BOOLEAN, TSDataType.INT32 };
            var encodings = new List<TSEncoding> { TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN };
            var compressors = new List<Compressor> { Compressor.UNCOMPRESSED, Compressor.UNCOMPRESSED, Compressor.UNCOMPRESSED };
            //status = await session_pool.CreateAlignedTimeseriesAsync(prefixPath, measurements, types, encodings, compressors);
            //System.Diagnostics.Debug.Assert(status == 0);

            var measures = new List<string>
                { testMeasurements[1], testMeasurements[2], testMeasurements[3] };
            var values = new List<object> { "test_text", true, (int)123 };
            var tasks = new List<Task<int>>();
            var start_ms = DateTime.Now.Ticks / 10000;
            for (var timestamp = 1; timestamp <= fetchSize * processedSize; timestamp++)
            {
                var rowRecord = new RowRecord(timestamp, values, measures);
                var task = session_pool.InsertAlignedRecordAsync(
                    string.Format("{0}.{1}", testDatabaseName, testDevice), rowRecord);
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            var end_ms = DateTime.Now.Ticks / 10000;
            Console.WriteLine(string.Format("total insert record time is {0}", end_ms - start_ms));
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestInsertAlignedRecordAsync Passed");
        }
        public async Task TestInsertAlignedStringRecord()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            status = await session_pool.CreateAlignedTimeseriesAsync(
                string.Format("{0}.{1}", testDatabaseName, testDevice),
                new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] },
                new List<TSDataType>() { TSDataType.TEXT, TSDataType.TEXT, TSDataType.TEXT },
                new List<TSEncoding>() { TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN },
                new List<Compressor>() { Compressor.UNCOMPRESSED, Compressor.UNCOMPRESSED, Compressor.UNCOMPRESSED });

            System.Diagnostics.Debug.Assert(status == 0);
            var measurements = new List<string>
                {testMeasurements[0], testMeasurements[1], testMeasurements[2]};
            var values = new List<string> { "test_text1", "test_text2", "test_text3" };
            var tasks = new List<Task<int>>();
            var start_ms = DateTime.Now.Ticks / 10000;
            for (var timestamp = 1; timestamp <= fetchSize * processedSize; timestamp++)
            {
                var task = session_pool.InsertAlignedStringRecordAsync(
                    string.Format("{0}.{1}", testDatabaseName, testDevice), measurements, values, timestamp);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            var end_ms = DateTime.Now.Ticks / 10000;
            Console.WriteLine(string.Format("total insert aligned string record time is {0}", end_ms - start_ms));
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
            Console.WriteLine("TestInsertAlignedStringRecordAsync Passed");
        }
        public async Task TestInsertAlignedRecords()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            string prefixPath = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurement_lst = new List<string>()
            {
                testMeasurements[1],
                testMeasurements[2],
                testMeasurements[3],
                testMeasurements[4],
                testMeasurements[5],
                testMeasurements[6]
            };
            var data_type_lst = new List<TSDataType>()
            {
                TSDataType.BOOLEAN, TSDataType.INT32, TSDataType.INT64, TSDataType.DOUBLE, TSDataType.FLOAT,
                TSDataType.TEXT
            };
            var encoding_lst = new List<TSEncoding>()
            {
                TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN,
                TSEncoding.PLAIN
            };
            var compressor_lst = new List<Compressor>()
            {
                Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY,
                Compressor.SNAPPY
            };
            status = await session_pool.CreateAlignedTimeseriesAsync(prefixPath, measurement_lst, data_type_lst, encoding_lst,
                compressor_lst);
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

            status = await session_pool.InsertAlignedRecordsAsync(device_id, rowRecords);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);
            Console.WriteLine(rowRecords);

            System.Diagnostics.Debug.Assert(true);

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
                    tasks.Add(session_pool.InsertAlignedRecordsAsync(device_id, rowRecords));
                    device_id = new List<string>() { };
                    rowRecords = new List<RowRecord>() { };
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
                res_count += 1;
                Console.WriteLine(res.Next());
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == record_count);
            System.Diagnostics.Debug.Assert(status == 0);

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertAlignedRecords Passed!");
        }
        public async Task TestInsertAlignedStringRecords()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            string prefixPath = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurement_lst = new List<string>() { testMeasurements[1], testMeasurements[2] };
            var data_type_lst = new List<TSDataType>() { TSDataType.TEXT, TSDataType.TEXT };
            var encoding_lst = new List<TSEncoding>() { TSEncoding.PLAIN, TSEncoding.PLAIN };
            var compressor_lst = new List<Compressor>() { Compressor.SNAPPY, Compressor.SNAPPY };
            status = await session_pool.CreateAlignedTimeseriesAsync(prefixPath, measurement_lst, data_type_lst, encoding_lst,
                compressor_lst);
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
            List<long> timestamp_lst = new List<long>() { 1, 2, 3 };

            status = await session_pool.InsertAlignedStringRecordsAsync(device_id, measurements_lst, values_lst, timestamp_lst);
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
            List<Task<int>> tasks = new List<Task<int>>();
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                device_id.Add(string.Format("{0}.{1}", testDatabaseName, testDevice));
                measurements_lst.Add(new List<string>() { testMeasurements[1], testMeasurements[2] });
                values_lst.Add(new List<string>() { "test1", "test2" });
                timestamp_lst.Add(timestamp);
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertAlignedStringRecordsAsync(device_id, measurements_lst, values_lst, timestamp_lst));
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
            var res_count = 0;
            while (res.Next())
            {
                Console.WriteLine(res.Next());
                res_count += 1;
            }

            await res.Close();
            Console.WriteLine(res_count + " " + fetchSize * processedSize);
            System.Diagnostics.Debug.Assert(res_count == fetchSize * processedSize);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestInsertAlignedStringRecords Passed!");
        }
        public async Task TestInsertAlignedRecordsOfOneDevice()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);

            string prefixPath = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurement_lst = new List<string>()
            {
                testMeasurements[1],
                testMeasurements[2],
                testMeasurements[3],
                testMeasurements[4],
                testMeasurements[5],
                testMeasurements[6]
            };
            var data_type_lst = new List<TSDataType>()
            {
                TSDataType.BOOLEAN, TSDataType.INT32, TSDataType.INT64, TSDataType.DOUBLE, TSDataType.FLOAT,
                TSDataType.TEXT
            };
            var encoding_lst = new List<TSEncoding>()
            {
                TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN,
                TSEncoding.PLAIN
            };
            var compressor_lst = new List<Compressor>()
            {
                Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY,
                Compressor.SNAPPY
            };

            status = await session_pool.CreateAlignedTimeseriesAsync(prefixPath, measurement_lst, data_type_lst, encoding_lst,
                compressor_lst);
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
            status = await session_pool.InsertAlignedRecordsOfOneDeviceAsync(device_id, rowRecords);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            rowRecords = new List<RowRecord>() { };
            var tasks = new List<Task<int>>();
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                rowRecords.Add(new RowRecord(timestamp, new List<object>() { true, (int)123 },
                    new List<string>() { testMeasurements[1], testMeasurements[2] }));
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertAlignedRecordsOfOneDeviceAsync(device_id, rowRecords));
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
            Console.WriteLine("TestInsertAlignedRecordsOfOneDevice Passed!");
        }
        public async Task TestInsertAlignedStringRecordsOfOneDevice()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements = new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] };
            var data_type_lst = new List<TSDataType>() { TSDataType.TEXT, TSDataType.TEXT, TSDataType.TEXT };
            var encoding_lst = new List<TSEncoding>() { TSEncoding.PLAIN, TSEncoding.PLAIN, TSEncoding.PLAIN };
            var compressor_lst = new List<Compressor>() { Compressor.SNAPPY, Compressor.SNAPPY, Compressor.SNAPPY };
            status = await session_pool.CreateAlignedTimeseriesAsync(device_id, measurements, data_type_lst, encoding_lst, compressor_lst);
            System.Diagnostics.Debug.Assert(status == 0);

            var measurements_lst = new List<List<string>>() { };
            measurements_lst.Add(new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });
            measurements_lst.Add(new List<string>() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });

            var values_lst = new List<List<string>>() { };
            values_lst.Add(new List<string>() { "test1", "test2", "test3" });
            values_lst.Add(new List<string>() { "test4", "test5", "test6" });
            values_lst.Add(new List<string>() { "test7", "test8", "test9" });

            var timestamp_lst = new List<long>() { 1, 2, 3 };

            status = await session_pool.InsertAlignedStringRecordsOfOneDeviceAsync(device_id, timestamp_lst, measurements_lst, values_lst);
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
                    tasks.Add(session_pool.InsertAlignedStringRecordsOfOneDeviceAsync(device_id, timestamp_lst, measurements_lst, values_lst));
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
            Console.WriteLine("TestInsertAlignedStringRecordsOfOneDevice Passed!");
        }
    }
}
