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
        public async Task TestInsertAlignedTablet()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurement_lst = new List<string>
                {   testMeasurements[1],
                    testMeasurements[2],
                    testMeasurements[3]
                };
            var value_lst = new List<List<object>>
            {
                new() {"iotdb", true, (int) 12}, new() {"c#", false, (int) 13},
                new() {"client", true, (int) 14}
            };
            var timestamp_lst = new List<long> { 1, 2, 3 };
            var datatype_lst = new List<TSDataType> { TSDataType.TEXT, TSDataType.BOOLEAN, TSDataType.INT32 };
            var tablet = new Tablet(device_id, measurement_lst, datatype_lst, value_lst, timestamp_lst);
            status = await session_pool.InsertAlignedTabletAsync(tablet);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<15");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            // large data test
            value_lst = new List<List<object>>() { };
            timestamp_lst = new List<long>() { };
            var tasks = new List<Task<int>>();
            var start_ms = DateTime.Now.Ticks / 10000;
            for (var timestamp = 4; timestamp <= fetchSize * processedSize; timestamp++)
            {
                timestamp_lst.Add(timestamp);
                value_lst.Add(new List<object>() { "iotdb", true, (int)timestamp });
                if (timestamp % fetchSize == 0)
                {
                    tablet = new Tablet(device_id, measurement_lst, datatype_lst, value_lst, timestamp_lst);
                    tasks.Add(session_pool.InsertAlignedTabletAsync(tablet));
                    value_lst = new List<List<object>>() { };
                    timestamp_lst = new List<long>() { };
                }
            }
            Console.WriteLine(tasks.Count);

            Task.WaitAll(tasks.ToArray());
            var end_ms = DateTime.Now.Ticks / 10000;
            Console.WriteLine(string.Format("total tablet insert time is {0}", end_ms - start_ms));
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));
            res.ShowTableNames();
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
            Console.WriteLine("TestInsertAlignedTablet Passed!");
        }

        public async Task TestInsertAlignedTablets()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var device_id = new List<string>()
            {
                string.Format("{0}.{1}", testDatabaseName, testDevices[1]),
                string.Format("{0}.{1}", testDatabaseName, testDevices[2])
            };
            var measurements_lst = new List<List<string>>()
            {
                new() {testMeasurements[1], testMeasurements[2], testMeasurements[3] },
                new() {testMeasurements[1], testMeasurements[2], testMeasurements[3] }
            };
            var values_lst = new List<List<List<object>>>()
            {
                new()
                {
                    new List<object>() {"iotdb", true, (int) 12}, new List<object>() {"c#", false, (int) 13},
                    new List<object>() {"client", true, (int) 14}
                },
                new()
                {
                    new List<object>() {"iotdb_2", true, (int) 1}, new List<object>() {"c#_2", false, (int) 2},
                    new List<object>() {"client_2", true, (int) 3}
                }
            };
            var datatype_lst = new List<List<TSDataType>>()
            {
                new() {TSDataType.TEXT, TSDataType.BOOLEAN, TSDataType.INT32},
                new() {TSDataType.TEXT, TSDataType.BOOLEAN, TSDataType.INT32}
            };
            var timestamp_lst = new List<List<long>>()
                {new() {2, 1, 3}, new() {3, 1, 2}};
            var tablets = new List<Tablet>() { };
            for (var i = 0; i < device_id.Count; i++)
            {
                var tablet = new Tablet(device_id[i], measurements_lst[i], datatype_lst[i], values_lst[i], timestamp_lst[i]);
                tablets.Add(tablet);
            }

            status = await session_pool.InsertAlignedTabletsAsync(tablets);
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevices[1]) + " where time<15");
            UtilsTest.PrintDataSetByString(res);

            // large data test
            var tasks = new List<Task<int>>();
            // tablets = new List<Tablet>() { };
            for (var timestamp = 4; timestamp <= processedSize * fetchSize; timestamp++)
            {
                var local_device_id = string.Format("{0}.{1}", testDatabaseName, testDevices[1]);
                var local_measurements = new List<string>()
                    {testMeasurements[1], testMeasurements[2], testMeasurements[3]};
                var local_value = new List<List<object>>() { new() { "iotdb", true, (int)timestamp } };
                var local_data_type = new List<TSDataType>() { TSDataType.TEXT, TSDataType.BOOLEAN, TSDataType.INT32 };
                var local_timestamp = new List<long> { timestamp };
                var tablet = new Tablet(local_device_id, local_measurements, local_data_type, local_value, local_timestamp);
                tablets.Add(tablet);
                if (timestamp % fetchSize == 0)
                {
                    tasks.Add(session_pool.InsertAlignedTabletsAsync(tablets));
                    tablets = new List<Tablet>() { };
                }
            }

            Task.WaitAll(tasks.ToArray());
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevices[1]));
            res.ShowTableNames();
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
            Console.WriteLine("TestInsertAlignedTablets Passed!");
        }
    }
}
