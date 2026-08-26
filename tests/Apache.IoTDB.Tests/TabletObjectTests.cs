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
using Apache.IoTDB.DataStructure;
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class TabletObjectTests
    {
        private static Tablet NewObjectTablet(int rows = 2)
        {
            var values = new List<List<object>>();
            var timestamps = new List<long>();
            for (int i = 0; i < rows; i++)
            {
                values.Add(new List<object> { $"r{i}", null });
                timestamps.Add(i + 1);
            }

            return new Tablet(
                "object_table",
                new List<string> { "region_id", "file" },
                new List<ColumnCategory> { ColumnCategory.TAG, ColumnCategory.FIELD },
                new List<TSDataType> { TSDataType.STRING, TSDataType.OBJECT },
                values,
                timestamps);
        }

        [Test]
        public void BuildObjectValue_PrependsIsEOFAndBigEndianOffset()
        {
            var whole = Tablet.BuildObjectValue(true, 0, new byte[] { 0x11, 0x22 });
            Assert.That(whole, Is.EqualTo(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0x11, 0x22 }));

            var segment = Tablet.BuildObjectValue(false, 512, new byte[] { 0x33 });
            Assert.That(segment, Is.EqualTo(new byte[] { 0, 0, 0, 0, 0, 0, 0, 2, 0, 0x33 }));
        }

        [Test]
        public void BuildObjectValue_RejectsInvalidInputs()
        {
            Assert.Throws<ArgumentNullException>(() => Tablet.BuildObjectValue(true, 0, null));
            Assert.Throws<ArgumentOutOfRangeException>(() => Tablet.BuildObjectValue(true, -1, new byte[] { 0x01 }));
        }

        [Test]
        public void SetObjectValueAt_WritesWholeAndSegmentedValues()
        {
            var tablet = NewObjectTablet();

            tablet.SetObjectValueAt(true, 0, new byte[] { 0x11, 0x22 }, 1, 0);
            tablet.SetObjectValueAt(false, 512, new byte[] { 0x33 }, 1, 1);

            var bytes = tablet.GetBinaryValues();
            var expected = new List<byte>();
            expected.AddRange(new byte[] { 0, 0, 0, 2, (byte)'r', (byte)'0' }); // "r0"
            expected.AddRange(new byte[] { 0, 0, 0, 2, (byte)'r', (byte)'1' }); // "r1"
            expected.AddRange(new byte[] { 0, 0, 0, 11 }); // object row 0 length
            expected.AddRange(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0x11, 0x22 });
            expected.AddRange(new byte[] { 0, 0, 0, 10 }); // object row 1 length
            expected.AddRange(new byte[] { 0, 0, 0, 0, 0, 0, 0, 2, 0, 0x33 });
            // No trailing bitmap section on the first serialization: BitMaps is
            // only allocated once a null cell is encountered.

            Assert.That(bytes, Is.EqualTo(expected.ToArray()));
        }

        [Test]
        public void SetObjectValueAt_ExposesObjectTypeCodeInRequest()
        {
            var tablet = NewObjectTablet(1);
            tablet.SetObjectValueAt(true, 0, new byte[] { 0x01, 0x02, 0x03 }, 1, 0);

            var pool = new SessionPool("localhost", 6667);
            var req = pool.GenInsertTabletReq(tablet, 1);

            Assert.That(req.Types, Is.EqualTo(new List<int> { (int)TSDataType.STRING, (int)TSDataType.OBJECT }));
            Assert.That(req.Size, Is.EqualTo(1));
            Assert.That(req.PrefixPath, Is.EqualTo("object_table"));
            Assert.That(req.Values, Is.EqualTo(tablet.GetBinaryValues()));
        }

        [Test]
        public void SetObjectValueAt_ClearsPreviouslyMarkedNullBit()
        {
            var tablet = NewObjectTablet(1);
            // First serialization marks the null OBJECT cell.
            var withNull = tablet.GetBinaryValues();
            // Bitmap section: [STRING hasNull=0][OBJECT hasNull=1][bitmap 0x01].
            Assert.That(withNull[withNull.Length - 2], Is.EqualTo(1), "OBJECT column has one null");
            Assert.That(withNull[withNull.Length - 1], Is.EqualTo(0x01), "null bitmap marks row 0");

            tablet.SetObjectValueAt(true, 0, new byte[] { 0x44 }, 1, 0);
            var withoutNull = tablet.GetBinaryValues();
            Assert.That(withoutNull[withoutNull.Length - 1], Is.EqualTo(0), "OBJECT null bit cleared");
        }

        [Test]
        public void SetObjectValueAt_RejectsNonObjectColumnAndOutOfRangeIndexes()
        {
            var tablet = NewObjectTablet(1);

            Assert.Throws<ArgumentException>(
                () => tablet.SetObjectValueAt(true, 0, new byte[] { 0x01 }, 0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => tablet.SetObjectValueAt(true, 0, new byte[] { 0x01 }, 1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => tablet.SetObjectValueAt(true, 0, new byte[] { 0x01 }, -1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => tablet.SetObjectValueAt(true, 0, new byte[] { 0x01 }, 1, 1));
        }

        [Test]
        public void GetDataTypeByStr_ResolvesObject()
        {
            Assert.That(Client.GetDataTypeByStr("OBJECT"), Is.EqualTo(TSDataType.OBJECT));
        }
    }
}
