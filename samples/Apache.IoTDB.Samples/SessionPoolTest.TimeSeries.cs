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
        public async Task TestCreateMultiTimeSeries()
        {
            // by Luzhan
            var session_pool = new SessionPool(host, port, username, password, poolSize);
            await session_pool.Open(false);
            var status = 0;
            if (debug) session_pool.OpenDebugMode();

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var measurement_lst = new List<int> { 1, 2, 3, 4, 5, 6 };
            var ts_path_lst = new List<string>(measurement_lst.ConvertAll(
                (measurement) => string.Format("{0}.{1}.{2}{3}", testDatabaseName, testDevice, testMeasurement, measurement)));
            var data_type_lst = new List<TSDataType>()
            {
                TSDataType.BOOLEAN, TSDataType.INT32, TSDataType.INT64, TSDataType.FLOAT, TSDataType.DOUBLE,
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
            status = await session_pool.CreateMultiTimeSeriesAsync(ts_path_lst, data_type_lst, encoding_lst,
                compressor_lst);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestCreateMultiTimeSeries Passed!");
        }

        public async Task TestDeleteTimeSeries()
        {
            var session_pool = new SessionPool(host, port, username, password, poolSize);
            await session_pool.Open(false);
            var status = 0;
            if (debug) session_pool.OpenDebugMode();

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            var measurement_lst = new List<int> { 1, 2, 3, 4, 5, 6 };
            var ts_path_lst = new List<string>(measurement_lst.ConvertAll(
                (measurement) => string.Format("{0}.{1}.{2}{3}", testDatabaseName, testDevice, testMeasurement, measurement)));
            var data_type_lst = new List<TSDataType>()
            {
                TSDataType.BOOLEAN, TSDataType.INT32, TSDataType.INT64, TSDataType.FLOAT, TSDataType.DOUBLE,
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
            status = await session_pool.CreateMultiTimeSeriesAsync(ts_path_lst, data_type_lst, encoding_lst,
                compressor_lst);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.DeleteTimeSeriesAsync(ts_path_lst);
            System.Diagnostics.Debug.Assert(status == 0);
            Console.WriteLine("TestDeleteTimeSeries Passed!");
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
        }
        public async Task TestCreateTimeSeries()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.BOOLEAN, TSEncoding.PLAIN, Compressor.SNAPPY) == 0);
            System.Diagnostics.Debug.Assert(await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]),
                TSDataType.INT32, TSEncoding.PLAIN, Compressor.SNAPPY) == 0);
            System.Diagnostics.Debug.Assert(await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[3]),
                TSDataType.INT64, TSEncoding.PLAIN, Compressor.SNAPPY) == 0);
            System.Diagnostics.Debug.Assert(await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[4]),
                TSDataType.FLOAT, TSEncoding.PLAIN, Compressor.SNAPPY) == 0);
            System.Diagnostics.Debug.Assert(await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[5]),
                TSDataType.DOUBLE, TSEncoding.PLAIN, Compressor.SNAPPY) == 0);
            System.Diagnostics.Debug.Assert(await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[6]),
                TSDataType.TEXT, TSEncoding.PLAIN, Compressor.SNAPPY) == 0);
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestCreateTimeSeries Passed!");
        }

        public async Task TestCreateAlignedTimeseries()
        {
            var session_pool = new SessionPool(host, port, username, password, poolSize);
            await session_pool.Open(false);
            var status = 0;
            if (debug) session_pool.OpenDebugMode();

            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);

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
                TSDataType.BOOLEAN, TSDataType.INT32, TSDataType.INT64, TSDataType.FLOAT, TSDataType.DOUBLE,
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
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestCreateAlignedTimeSeries Passed!");
        }
        public async Task TestCheckTimeSeriesExists()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            var status = 0;
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.CreateTimeSeries(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]),
                TSDataType.BOOLEAN, TSEncoding.PLAIN, Compressor.SNAPPY);
            var ifExist_1 = await session_pool.CheckTimeSeriesExistsAsync(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[1]));
            var ifExist_2 = await session_pool.CheckTimeSeriesExistsAsync(
                string.Format("{0}.{1}.{2}", testDatabaseName, testDevice, testMeasurements[2]));
            System.Diagnostics.Debug.Assert(ifExist_1 == true && ifExist_2 == false);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            System.Diagnostics.Debug.Assert(status == 0);
            await session_pool.Close();
            Console.WriteLine("TestCheckTimeSeriesExists Passed!");
        }
    }

}