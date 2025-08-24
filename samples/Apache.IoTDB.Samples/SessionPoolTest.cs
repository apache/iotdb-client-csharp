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
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Apache.IoTDB.Data;
using Apache.IoTDB.DataStructure;
using ConsoleTableExt;

namespace Apache.IoTDB.Samples
{
    public partial class SessionPoolTest
    {
        public string host = "localhost";
        public int port = 6667;
        public string username = "root";
        public string password = "root";
        public List<string> nodeUrls = new();
        public int fetchSize = 500;
        public int processedSize = 4;
        public bool debug = false;
        private int poolSize = 2;
        public static string testTemplateName = "TEST_CSHARP_CLIENT_TEMPLATE_97209";
        public static string testDatabaseName = "root.TEST_CSHARP_CLIENT_GROUP_97209";
        public static string testDevice = "TEST_CSHARP_CLIENT_DEVICE";
        public static string testMeasurement = "TEST_CSHARP_CLIENT_TS";
        public static List<int> deviceCount = new List<int>() { 0, 1, 2, 3 };
        public static List<int> measurementCount = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        public static List<string> testDevices = new List<string>(
            deviceCount.ConvertAll(x => testDevice + x.ToString()).ToArray()
        );
        public List<string> testMeasurements = new List<string>(
            measurementCount.ConvertAll(x => testMeasurement + x.ToString()).ToArray()
        );
        public SessionPoolTest(string _host = "localhost")
        {
            host = _host;
            nodeUrls.Add(host + ":" + port);
        }

        public SessionPoolTest(List<string> _nodeUrls)
        {
            nodeUrls = _nodeUrls;
        }
        public async Task Test()
        {
            if (nodeUrls.Count == 1)
            {
                await TestOpenWithNodeUrls();

                await TestOpenWith2NodeUrls();

                await TestOpenWithNodeUrlsAndInsertOneRecord();

                await TestInsertOneRecord();

                await TestInsertAlignedRecord();

                await TestInsertAlignedRecords();

                await TestInsertAlignedStringRecords();

                await TestInsertAlignedStringRecordsOfOneDevice();

                await TestInsertStringRecord();

                await TestInsertAlignedStringRecord();

                await TestInsertStringRecords();

                await TestInsertStringRecordsOfOneDevice();

                await TestInsertAlignedRecordsOfOneDevice();

                await TestInsertAlignedTablet();

                await TestInsertAlignedTablets();

                await TestInsertRecord();

                await TestCreateMultiTimeSeries();

                await TestInsertStrRecord();

                await TestInsertRecords();

                await TestInsertRecordsWithAllType();

                await TestInsertRecordsOfOneDevice();

                await TestInsertTablet();

                await TestInsertTabletWithAllType();

                await TestInsertTabletWithNullValue();

                await TestInsertTablets();

                await TestSetAndUnsetSchemaTemplate();

                await TestCreateAlignedTimeseries();

                await TestCreateAndDropSchemaTemplate();

                await TestGetTimeZone();

                await TestCreateAndDeleteDatabase();

                await TestCreateTimeSeries();

                await TestDeleteTimeSeries();

                await TestDeleteDatabase();

                await TestCheckTimeSeriesExists();

                await TestSetTimeZone();

                await TestDeleteData();

                await TestNonSql();

                await TestRawDataQuery();

                await TestLastDataQuery();

                await TestSqlQuery();

                await TestNonSqlBy_ADO();
            }
            else
            {
                await TestMultiNodeDataFetch();
            }
        }
        public async Task TestOpenWithNodeUrls()
        {
            var session_pool = new SessionPool(nodeUrls, 8);
            await session_pool.Open(false);
            Debug.Assert(session_pool.IsOpen());
            if (debug) session_pool.OpenDebugMode();
            await session_pool.Close();
            Console.WriteLine("TestOpenWithNodeUrls Passed!");
        }
        public async Task TestOpenWith2NodeUrls()
        {
            var session_pool = new SessionPool(new List<string>() { host + ":" + port, host + ":" + (port + 1) }, 8);
            await session_pool.Open(false);
            Debug.Assert(session_pool.IsOpen());
            if (debug) session_pool.OpenDebugMode();
            await session_pool.Close();

            session_pool = new SessionPool(new List<string>() { host + ":" + (port + 1), host + ":" + port }, 8);
            await session_pool.Open(false);
            Debug.Assert(session_pool.IsOpen());
            if (debug) session_pool.OpenDebugMode();
            await session_pool.Close();
            Console.WriteLine("TestOpenWith2NodeUrls Passed!");
        }
        public async Task TestOpenWithNodeUrlsAndInsertOneRecord()
        {
            var session_pool = new SessionPool(nodeUrls, 8);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[0]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            var rowRecord = new RowRecord(1668404120807, new() { "1111111", "22222", "333333" }, new() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });
            status = await session_pool.InsertRecordsAsync(new List<string>() { string.Format("{0}.{1}", testDatabaseName, testDevice) }, new List<RowRecord>() { rowRecord });
            Debug.Assert(status == 0);
            Console.WriteLine("TestOpenWithNodeUrlsAndInsertOneRecord Passed!");
        }
        public async Task TestInsertOneRecord()
        {
            var session_pool = new SessionPool(host, port, 1);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[0]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            status = await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY);
            var rowRecord = new RowRecord(1668404120807, new() { "1111111", "22222", "333333" }, new() { testMeasurements[0], testMeasurements[1], testMeasurements[2] });
            status = await session_pool.InsertRecordsAsync(new List<string>() { string.Format("{0}.{1}", testDatabaseName, testDevice) }, new List<RowRecord>() { rowRecord });
        }
        public async Task TestGetTimeZone()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var time_zone = await session_pool.GetTimeZone();
            System.Diagnostics.Debug.Assert(time_zone == "Asia/Shanghai");
            await session_pool.Close();
            Console.WriteLine("TestGetTimeZone Passed!");
        }



        public async Task TestCreateAndDeleteDatabase()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(
                await session_pool.CreateDatabase(testDatabaseName) == 0);
            System.Diagnostics.Debug.Assert(
                await session_pool.DeleteDatabaseAsync(testDatabaseName) == 0);
            await session_pool.Close();
            Console.WriteLine("TestSetAndDeleteStorageGroup Passed!");
        }

        public async Task TestDeleteDatabase()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            await session_pool.CreateDatabase(string.Format("{0}{1}", testDatabaseName, "_01"));
            await session_pool.CreateDatabase(string.Format("{0}{1}", testDatabaseName, "_02"));
            await session_pool.CreateDatabase(string.Format("{0}{1}", testDatabaseName, "_03"));
            await session_pool.CreateDatabase(string.Format("{0}{1}", testDatabaseName, "_04"));
            var database_names = new List<string>() { };
            database_names.Add(string.Format("{0}{1}", testDatabaseName, "_01"));
            database_names.Add(string.Format("{0}{1}", testDatabaseName, "_02"));
            database_names.Add(string.Format("{0}{1}", testDatabaseName, "_03"));
            database_names.Add(string.Format("{0}{1}", testDatabaseName, "_04"));
            System.Diagnostics.Debug.Assert(await session_pool.DeleteDatabasesAsync(database_names) == 0);
            await session_pool.Close();
            Console.WriteLine("TestDeleteStorageGroups Passed!");
        }


        public async Task TestSetTimeZone()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.SetTimeZone("GMT+8:00");
            System.Diagnostics.Debug.Assert(await session_pool.GetTimeZone() == "GMT+8:00");
            await session_pool.Close();
            Console.WriteLine("TestSetTimeZone Passed!");
        }

        public async Task TestDeleteData()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
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
            {
                testMeasurements[1], testMeasurements[2], testMeasurements[3]
            };
            var values = new List<object> { "test_text", true, (int)123 };
            status = await session_pool.InsertRecordAsync(
                string.Format("{0}.{1}", testDatabaseName, testDevice), new RowRecord(1, values, measures));
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.InsertRecordAsync(
                string.Format("{0}.{1}", testDatabaseName, testDevice), new RowRecord(2, values, measures));
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.InsertRecordAsync(
                string.Format("{0}.{1}", testDatabaseName, testDevice), new RowRecord(3, values, measures));
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.InsertRecordAsync(
                string.Format("{0}.{1}", testDatabaseName, testDevice), new RowRecord(4, values, measures));
            System.Diagnostics.Debug.Assert(status == 0);
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            var ts_path_lst = new List<string>()
            {
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
            };
            await session_pool.DeleteDataAsync(ts_path_lst, 2, 3);
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestDeleteData Passed!");
        }

        public async Task TestNonSql()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.ExecuteNonQueryStatementAsync(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".status with datatype=BOOLEAN,encoding=PLAIN");
            await session_pool.ExecuteNonQueryStatementAsync(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".temperature with datatype=FLOAT,encoding=PLAIN");
            await session_pool.ExecuteNonQueryStatementAsync(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".hardware with datatype=TEXT,encoding=PLAIN");
            status = await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (4, false, 20, 'yxl')");
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (5, true, 12, 'myy')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (6, true, 21, 'lz')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, hardware) VALUES (7, true,'lz')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, hardware) VALUES (7, true,'lz')");
            var res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");

            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestNonSql Passed");
        }

        public async Task TestNonSqlBy_ADO()
        {
            var cnts = new IoTDB.Data.IoTDBConnectionStringBuilder();
            cnts.DataSource = host;
            cnts.TimeOut = (int)TimeSpan.FromSeconds(20).TotalMilliseconds;
            var cnt = new IoTDB.Data.IoTDBConnection(cnts.ConnectionString);
            await cnt.OpenAsync();
            var session_pool = cnt.SessionPool;
            System.Diagnostics.Debug.Assert(cnt.State == System.Data.ConnectionState.Open);
            var status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await cnt.CreateCommand(
                 "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".status with datatype=BOOLEAN,encoding=PLAIN").ExecuteNonQueryAsync();
            await cnt.CreateCommand(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".temperature with datatype=FLOAT,encoding=PLAIN").ExecuteNonQueryAsync();
            await cnt.CreateCommand(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".hardware with datatype=TEXT,encoding=PLAIN").ExecuteNonQueryAsync();

            status = await cnt.CreateCommand(
    "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (3, false, 20, '1yxl')").ExecuteNonQueryAsync();
            status = await cnt.CreateCommand(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (4, false, 20, 'yxl')").ExecuteNonQueryAsync();
            System.Diagnostics.Debug.Assert(status == 0);
            await cnt.CreateCommand(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (5, true, 12, 'myy')").ExecuteNonQueryAsync();
            await cnt.CreateCommand(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (6, true, 21, 'lz')").ExecuteNonQueryAsync();
            await cnt.CreateCommand(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, hardware) VALUES (7, true,'lz')").ExecuteNonQueryAsync();
            await cnt.CreateCommand(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, hardware) VALUES (7, true,'lz')").ExecuteNonQueryAsync();
            var reader = await cnt.CreateCommand(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10").ExecuteReaderAsync();
            ConsoleTableBuilder.From(reader.ToDataTable()).WithFormatter(0, fc => $"{fc:yyyy-MM-dd HH:mm:ss.fff}").WithFormat(ConsoleTableBuilderFormat.Default).ExportAndWriteLine();
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await cnt.CloseAsync();

            System.Diagnostics.Debug.Assert(status == 0);

            Console.WriteLine("TestNonSqlBy_ADO Passed");
        }

        public async Task TestSqlQuery()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.ExecuteNonQueryStatementAsync(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".status with datatype=BOOLEAN,encoding=PLAIN");
            await session_pool.ExecuteNonQueryStatementAsync(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".temperature with datatype=FLOAT,encoding=PLAIN");
            await session_pool.ExecuteNonQueryStatementAsync(
                "create timeseries " + string.Format("{0}.{1}", testDatabaseName, testDevice) + ".hardware with datatype=TEXT,encoding=PLAIN");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (4, false, 20, 'yxl')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (5, true, 12, 'myy')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, temperature, hardware) VALUES (6, true, 21, 'lz')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, hardware) VALUES (7, true,'lz')");
            await session_pool.ExecuteNonQueryStatementAsync(
                "insert into " + string.Format("{0}.{1}", testDatabaseName, testDevice) + "(timestamp, status, hardware) VALUES (7, true,'lz')");

            var res = await session_pool.ExecuteQueryStatementAsync("show timeseries root");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            Console.WriteLine("SHOW TIMESERIES ROOT sql passed!");
            res = await session_pool.ExecuteQueryStatementAsync("show devices");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            Console.WriteLine("SHOW DEVICES sql passed!");
            res = await session_pool.ExecuteQueryStatementAsync($"COUNT TIMESERIES {testDatabaseName}");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            Console.WriteLine("COUNT TIMESERIES root sql Passed");
            res = await session_pool.ExecuteQueryStatementAsync("select * from root.ln.wf01 where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            Console.WriteLine("SELECT sql Passed");
            res = await session_pool.ExecuteQueryStatementAsync(
                "select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice) + " where time<10");
            UtilsTest.PrintDataSetByString(res);

            await res.Close();
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("SELECT sql Passed");
        }
        public async Task TestRawDataQuery()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);

            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements = new List<string> { testMeasurements[0], testMeasurements[1] };
            var data_type_lst = new List<TSDataType> { TSDataType.BOOLEAN, TSDataType.FLOAT };
            var encoding_lst = new List<TSEncoding> { TSEncoding.PLAIN, TSEncoding.PLAIN };
            var compressor_lst = new List<Compressor> { Compressor.SNAPPY, Compressor.SNAPPY };
            status = await session_pool.CreateAlignedTimeseriesAsync(device_id, measurements, data_type_lst, encoding_lst, compressor_lst);

            var records = new List<RowRecord>();
            var values = new List<object>() { true, 20.0f };
            var device_id_lst = new List<string>() { };
            for (int i = 1; i <= fetchSize * processedSize; i++)
            {
                var record = new RowRecord(i, values, measurements);
                records.Add(record);
                device_id_lst.Add(device_id);
            }
            status = await session_pool.InsertAlignedRecordsAsync(device_id_lst, records);
            System.Diagnostics.Debug.Assert(status == 0);

            var paths = new List<string>() { string.Format("{0}.{1}", device_id, testMeasurements[0]), string.Format("{0}.{1}", device_id, testMeasurements[1]) };

            var res = await session_pool.ExecuteRawDataQuery(paths, 10, fetchSize * processedSize);
            var count = 0;
            while (res.Next())
            {
                count++;
            }
            Console.WriteLine(count + " " + (fetchSize * processedSize - 10));
            System.Diagnostics.Debug.Assert(count == fetchSize * processedSize - 10);
            await res.Close();

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("RawDataQuery Passed");
        }
        public async Task TestLastDataQuery()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);

            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements = new List<string> { testMeasurements[0], testMeasurements[1] };
            var data_type_lst = new List<TSDataType> { TSDataType.BOOLEAN, TSDataType.FLOAT };
            var encoding_lst = new List<TSEncoding> { TSEncoding.PLAIN, TSEncoding.PLAIN };
            var compressor_lst = new List<Compressor> { Compressor.SNAPPY, Compressor.SNAPPY };
            status = await session_pool.CreateAlignedTimeseriesAsync(device_id, measurements, data_type_lst, encoding_lst, compressor_lst);

            var records = new List<RowRecord>();
            var values = new List<object>() { true, 20.0f };
            var device_id_lst = new List<string>() { };
            for (int i = 1; i <= fetchSize * processedSize; i++)
            {
                var record = new RowRecord(i, values, measurements);
                records.Add(record);
                device_id_lst.Add(device_id);
            }
            status = await session_pool.InsertAlignedRecordsAsync(device_id_lst, records);
            System.Diagnostics.Debug.Assert(status == 0);

            var paths = new List<string>() { string.Format("{0}.{1}", device_id, testMeasurements[0]), string.Format("{0}.{1}", device_id, testMeasurements[1]) };

            var res = await session_pool.ExecuteLastDataQueryAsync(paths, fetchSize * processedSize - 10);
            var count = 0;
            while (res.Next())
            {
                Console.WriteLine(count);
                count++;
            }
            Console.WriteLine(count + " " + (fetchSize * processedSize - 10));
            System.Diagnostics.Debug.Assert(count == 2);
            await res.Close();

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("LastDataQuery Passed");
        }

        public async Task TestMultiNodeDataFetch()
        {
            System.Diagnostics.Debug.Assert(nodeUrls.Count > 1, "nodeUrls.Count should be greater than 1 in MultiNode Test");
            var session_pool = new SessionPool.Builder()
                .SetUsername(username)
                .SetPassword(password)
                .SetNodeUrl(nodeUrls)
                .SetPoolSize(4)
                .Build();
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();
            var status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var device_id = string.Format("{0}.{1}", testDatabaseName, testDevice);
            var measurements = new List<string> { testMeasurements[0], testMeasurements[1] };
            var data_type_lst = new List<TSDataType> { TSDataType.BOOLEAN, TSDataType.FLOAT };
            var encoding_lst = new List<TSEncoding> { TSEncoding.PLAIN, TSEncoding.PLAIN };
            var compressor_lst = new List<Compressor> { Compressor.SNAPPY, Compressor.SNAPPY };
            var ts_path_lst = new List<string>() {
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[0]),
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1])
            };
            status = await session_pool.CreateMultiTimeSeriesAsync(ts_path_lst, data_type_lst, encoding_lst, compressor_lst);

            var records = new List<RowRecord>();
            var values = new List<object>() { true, 20.0f };
            var device_id_lst = new List<string>() { };
            for (int i = 1; i <= fetchSize * processedSize * 4 + 783; i++)
            {
                var record = new RowRecord(i, values, measurements);
                records.Add(record);
                device_id_lst.Add(device_id);
            }

            // insert data
            status = await session_pool.InsertRecordsAsync(device_id_lst, records);
            System.Diagnostics.Debug.Assert(status == 0);
            // fetch data
            var paths = new List<string>() { string.Format("{0}.{1}", device_id, testMeasurements[0]), string.Format("{0}.{1}", device_id, testMeasurements[1]) };
            var res = await session_pool.ExecuteQueryStatementAsync("select * from " + string.Format("{0}.{1}", testDatabaseName, testDevice));

            IReadOnlyList<string> columns = res.GetColumnNames();
            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();

            var count = 0;
            while (res.HasNext()) count++;

            Console.WriteLine(count + " " + (fetchSize * processedSize * 4 + 783));
            System.Diagnostics.Debug.Assert(count == fetchSize * processedSize * 4 + 783);
            await res.Close();
            Console.WriteLine("MultiNodeDataFetch Passed");
        }
    }
}
