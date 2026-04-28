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
using Apache.IoTDB.DataStructure;
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class RpcDataSetTests
    {
        /// <summary>
        /// Builds a serialized TsBlock with 2 rows and 3 value columns (INT32, BOOLEAN, DOUBLE).
        /// Row 0: time=1000, int32=42,     boolean=null, double=3.14
        /// Row 1: time=2000, int32=null,    boolean=true, double=null
        /// </summary>
        private static byte[] BuildTestTsBlockBytes()
        {
            var buf = new ByteBuffer(256);

            // 1. value column count
            buf.AddInt(3);

            // 2. value column data types: INT32(1), BOOLEAN(0), DOUBLE(4)
            buf.AddByte((byte)TSDataType.INT32);
            buf.AddByte((byte)TSDataType.BOOLEAN);
            buf.AddByte((byte)TSDataType.DOUBLE);

            // 3. position count
            buf.AddInt(2);

            // 4. column encodings: time=Int64Array(2), int32=Int32Array(1), boolean=ByteArray(0), double=Int64Array(2)
            buf.AddByte((byte)ColumnEncoding.Int64Array);
            buf.AddByte((byte)ColumnEncoding.Int32Array);
            buf.AddByte((byte)ColumnEncoding.ByteArray);
            buf.AddByte((byte)ColumnEncoding.Int64Array);

            // 5. Time column (Int64Array): no nulls, 2 values
            buf.AddByte(0); // mayHaveNull = false
            buf.AddLong(1000L);
            buf.AddLong(2000L);

            // 6. INT32 column (Int32Array): row1 is null
            buf.AddByte(1); // mayHaveNull = true
            // null indicators packed: [false, true] → bit7=0, bit6=1 → 0x40
            buf.AddByte(0x40);
            // only non-null value (row 0)
            buf.AddInt(42);

            // 7. BOOLEAN column (ByteArray): row0 is null
            buf.AddByte(1); // mayHaveNull = true
            // null indicators packed: [true, false] → bit7=1, bit6=0 → 0x80
            buf.AddByte(0x80);
            // boolean values packed (all positions): [false, true] → bit7=0, bit6=1 → 0x40
            buf.AddByte(0x40);

            // 8. DOUBLE column (Int64Array): row1 is null
            buf.AddByte(1); // mayHaveNull = true
            // null indicators packed: [false, true] → 0x40
            buf.AddByte(0x40);
            // only non-null value (row 0)
            buf.AddDouble(3.14);

            return buf.GetBuffer();
        }

        private RpcDataSet CreateTestDataSet(List<byte[]> queryResult = null)
        {
            var columnNames = new List<string> { "s_int32", "s_boolean", "s_double" };
            var columnTypes = new List<string> { "INT32", "BOOLEAN", "DOUBLE" };
            var columnNameIndex = new Dictionary<string, int>
            {
                { "s_int32", 0 },
                { "s_boolean", 1 },
                { "s_double", 2 }
            };
            var columnIndex2TsBlockColumnIndexList = new List<int> { 0, 1, 2 };

            queryResult ??= new List<byte[]> { BuildTestTsBlockBytes() };

            return new RpcDataSet(
                sql: "SELECT * FROM root.test",
                columnNameList: columnNames,
                columnTypeList: columnTypes,
                columnNameIndex: columnNameIndex,
                ignoreTimestamp: false,
                moreData: false,
                queryId: 1,
                statementId: 1,
                client: null,
                sessionId: 1,
                queryResult: queryResult,
                fetchSize: 1024,
                timeout: 10000,
                zoneId: "UTC",
                columnIndex2TsBlockColumnIndexList: columnIndex2TsBlockColumnIndexList
            );
        }

        [Test]
        public void CurrentBatchRowCount_ReturnsCorrectSize_BeforeFirstNext()
        {
            var dataSet = CreateTestDataSet();

            Assert.That(dataSet._tsBlockSize, Is.EqualTo(2),
                "CurrentBatchRowCount should return the TsBlock row count immediately after construction.");
        }

        [Test]
        public void CurrentBatchRowCount_ReturnsZero_WhenNoData()
        {
            var dataSet = CreateTestDataSet(queryResult: new List<byte[]>());

            Assert.That(dataSet._tsBlockSize, Is.EqualTo(0),
                "CurrentBatchRowCount should return 0 when no query results are provided.");
        }

        [Test]
        public void GetRow_ExcludesNullValuedMeasurements_ForValueTypes()
        {
            var dataSet = CreateTestDataSet();

            // Advance to row 0: int32=42, boolean=null, double=3.14
            dataSet.Next();
            var row0 = dataSet.GetRow();

            Assert.That(row0.Timestamps, Is.EqualTo(1000L));
            Assert.That(row0.Measurements, Does.Contain("s_int32"));
            Assert.That(row0.Measurements, Does.Not.Contain("s_boolean"),
                "Null BOOLEAN measurement should be excluded from row.");
            Assert.That(row0.Measurements, Does.Contain("s_double"));
            Assert.That(row0.Values.Count, Is.EqualTo(2), "Row 0 should have 2 non-null values.");
        }

        [Test]
        public void GetRow_ExcludesNullValuedMeasurements_ForInt32AndDouble()
        {
            var dataSet = CreateTestDataSet();

            // Advance to row 0 then row 1
            dataSet.Next();
            dataSet.Next();
            var row1 = dataSet.GetRow();

            Assert.That(row1.Timestamps, Is.EqualTo(2000L));
            Assert.That(row1.Measurements, Does.Not.Contain("s_int32"),
                "Null INT32 measurement should be excluded from row.");
            Assert.That(row1.Measurements, Does.Contain("s_boolean"));
            Assert.That(row1.Measurements, Does.Not.Contain("s_double"),
                "Null DOUBLE measurement should be excluded from row.");
            Assert.That(row1.Values.Count, Is.EqualTo(1), "Row 1 should have 1 non-null value.");
            Assert.That(row1.Values[0], Is.EqualTo(true));
        }

        [Test]
        public void GetRow_DataTypesMatchMeasurements()
        {
            var dataSet = CreateTestDataSet();

            dataSet.Next();
            var row0 = dataSet.GetRow();

            Assert.That(row0.DataTypes.Count, Is.EqualTo(row0.Measurements.Count),
                "DataTypes count should match Measurements count.");
            Assert.That(row0.DataTypes.Count, Is.EqualTo(row0.Values.Count),
                "DataTypes count should match Values count.");
        }
    }
}
