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
using System.Threading.Tasks;
using Apache.IoTDB.DataStructure;

namespace Apache.IoTDB.Samples;

public class TableSessionPoolTest
{
    private readonly SessionPoolTest sessionPoolTest;

    public TableSessionPoolTest(SessionPoolTest sessionPoolTest)
    {
        this.sessionPoolTest = sessionPoolTest;
    }

    public async Task Test()
    {
        await TestCleanup();

        await TestSelectAndInsert();
        await TestUseDatabase();
        // await TestCleanup();
    }


    public async Task TestSelectAndInsert()
    {
        var tableSessionPool = new TableSessionPool.Builder()
                .SetNodeUrls(sessionPoolTest.nodeUrls)
                .SetUsername(sessionPoolTest.username)
                .SetPassword(sessionPoolTest.password)
                .SetFetchSize(1024)
                .Build();

        await tableSessionPool.Open(false);

        if (sessionPoolTest.debug) tableSessionPool.OpenDebugMode();


        await tableSessionPool.ExecuteNonQueryStatementAsync("CREATE DATABASE test1");
        await tableSessionPool.ExecuteNonQueryStatementAsync("CREATE DATABASE test2");

        await tableSessionPool.ExecuteNonQueryStatementAsync("use test2");

        // or use full qualified table name
        await tableSessionPool.ExecuteNonQueryStatementAsync(
            "create table test1.table1(region_id STRING TAG, plant_id STRING TAG, device_id STRING TAG, model STRING ATTRIBUTE, temperature FLOAT FIELD, humidity DOUBLE FIELD) with (TTL=3600000)");

        await tableSessionPool.ExecuteNonQueryStatementAsync(
            "create table table2(region_id STRING TAG, plant_id STRING TAG, color STRING ATTRIBUTE, temperature FLOAT FIELD, speed DOUBLE FIELD) with (TTL=6600000)");

        // show tables from current database
        var res = await tableSessionPool.ExecuteQueryStatementAsync("SHOW TABLES");
        res.ShowTableNames();
        while (res.HasNext()) Console.WriteLine(res.Next());
        await res.Close();

        // show tables by specifying another database
        // using SHOW tables FROM
        res = await tableSessionPool.ExecuteQueryStatementAsync("SHOW TABLES FROM test1");
        res.ShowTableNames();
        while (res.HasNext()) Console.WriteLine(res.Next());
        await res.Close();

        var tableName = "testTable1";
        List<string> columnNames =
            new List<string> {
            "region_id",
            "plant_id",
            "device_id",
            "model",
            "temperature",
            "humidity" };
        List<TSDataType> dataTypes =
            new List<TSDataType>{
            TSDataType.STRING,
            TSDataType.STRING,
            TSDataType.STRING,
            TSDataType.STRING,
            TSDataType.FLOAT,
            TSDataType.DOUBLE};
        List<ColumnCategory> columnCategories =
            new List<ColumnCategory>{
            ColumnCategory.TAG,
            ColumnCategory.TAG,
            ColumnCategory.TAG,
            ColumnCategory.ATTRIBUTE,
            ColumnCategory.FIELD,
            ColumnCategory.FIELD};
        var values = new List<List<object>> { };
        var timestamps = new List<long> { };
        for (long timestamp = 0; timestamp < 100; timestamp++)
        {
            timestamps.Add(timestamp);
            values.Add(new List<object> { "1", "5", "3", "A", 1.23F + timestamp, 111.1 + timestamp });
        }
        var tablet = new Tablet(tableName, columnNames, columnCategories, dataTypes, values, timestamps);

        await tableSessionPool.InsertAsync(tablet);


        res = await tableSessionPool.ExecuteQueryStatementAsync("select * from testTable1 "
              + "where region_id = '1' and plant_id in ('3', '5') and device_id = '3'");
        res.ShowTableNames();
        while (res.HasNext()) Console.WriteLine(res.Next());
        await res.Close();

        await tableSessionPool.Close();
    }


    public async Task TestUseDatabase()
    {
        var tableSessionPool = new TableSessionPool.Builder()
                .SetNodeUrls(sessionPoolTest.nodeUrls)
                .SetUsername(sessionPoolTest.username)
                .SetPassword(sessionPoolTest.password)
                .SetDatabase("test1")
                .SetFetchSize(1024)
                .Build();

        await tableSessionPool.Open(false);

        if (sessionPoolTest.debug) tableSessionPool.OpenDebugMode();


        // show tables from current database
        var res = await tableSessionPool.ExecuteQueryStatementAsync("SHOW TABLES");
        res.ShowTableNames();
        while (res.HasNext()) Console.WriteLine(res.Next());
        await res.Close();

        await tableSessionPool.ExecuteNonQueryStatementAsync("use test2");

        // show tables from current database
        res = await tableSessionPool.ExecuteQueryStatementAsync("SHOW TABLES");
        res.ShowTableNames();
        while (res.HasNext()) Console.WriteLine(res.Next());
        await res.Close();

        await tableSessionPool.Close();
    }

    public async Task TestCleanup()
    {
        var tableSessionPool = new TableSessionPool.Builder()
                .SetNodeUrls(sessionPoolTest.nodeUrls)
                .SetUsername(sessionPoolTest.username)
                .SetPassword(sessionPoolTest.password)
                .SetFetchSize(1024)
                .Build();

        await tableSessionPool.Open(false);

        if (sessionPoolTest.debug) tableSessionPool.OpenDebugMode();

        await tableSessionPool.ExecuteNonQueryStatementAsync("drop database test1");
        await tableSessionPool.ExecuteNonQueryStatementAsync("drop database test2");

        await tableSessionPool.Close();
    }
}
