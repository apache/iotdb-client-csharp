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
using System.IO;
using System.Text;

namespace Apache.IoTDB.DataStructure
{
    public class TsBlock
    {
        private readonly Column _timeColumn;
        private readonly List<Column> _valueColumns;
        private readonly int _positionCount;

        public TsBlock(int positionCount, Column timeColumn, params Column[] valueColumns)
        {
            _positionCount = positionCount;
            _timeColumn = timeColumn;
            _valueColumns = new List<Column>(valueColumns);
            if (valueColumns == null)
                throw new ArgumentNullException(nameof(valueColumns));
            if (timeColumn.GetPositionCount() != positionCount)
                throw new ArgumentException(
                    $"input positionCount {positionCount} does not match timeColumn.positionCount {timeColumn.GetPositionCount()}"
                );
            for (int i = 0; i < ValueColumnCount; i++)
            {
                if (valueColumns[i].GetPositionCount() != positionCount)
                throw new ArgumentException(
                    $"input positionCount {positionCount} does not match valueColumn{i}.positionCount {valueColumns[i].GetPositionCount()}"
                );
            }
        }

        public static TsBlock Deserialize(ByteBuffer reader)
        {
            // Serialized tsblock:
            //    +-------------+---------------+---------+------------+-----------+----------+
            //    | val col cnt | val col types | pos cnt | encodings  | time col  | val col  |
            //    +-------------+---------------+---------+------------+-----------+----------+
            //    | int32       | list[byte]    | int32   | list[byte] |  bytes    | bytes    |
            //    +-------------+---------------+---------+------------+-----------+----------+

            // Read value column count
            var valueColumnCount = reader.GetInt();

            // Read value column data types
            var valueColumnDataTypes = new TSDataType[valueColumnCount];
            for (int i = 0; i < valueColumnCount; i++)
            {
                valueColumnDataTypes[i] = DeserializeDataType(reader);
            }

            // Read position count
            var positionCount = reader.GetInt();

            // Read column encodings 
            // Read time column encoding
            ColumnEncoding timeColumnEncodings = DeserializeColumnEncoding(reader);
            
            // Read value column encodings
            var valuecolumnEncodings = new ColumnEncoding[valueColumnCount];
            for (int i = 1; i < valueColumnCount + 1; i++)
            {
                valuecolumnEncodings[i - 1] = DeserializeColumnEncoding(reader);
            }

            // Read time column
            var timeColumnDecoder = BaseColumnDecoder.GetDecoder(timeColumnEncodings);
            var timeColumn = timeColumnDecoder.ReadColumn(reader, TSDataType.INT64, positionCount);

            // Read value columns
            var valueColumns = new Column[valueColumnCount];
            for (int i = 1; i < valueColumnCount + 1; i++)
            {
                var decoder = BaseColumnDecoder.GetDecoder(valuecolumnEncodings[i - 1]);
                valueColumns[i - 1] = decoder.ReadColumn(reader, valueColumnDataTypes[i - 1], positionCount);
            }

            return new TsBlock(positionCount, timeColumn, valueColumns);
        }

        private static TSDataType DeserializeDataType(ByteBuffer reader)
        {
            byte b = reader.GetByte();
            return (TSDataType)b;
        }

        private static ColumnEncoding DeserializeColumnEncoding(ByteBuffer reader)
        {
            byte b = reader.GetByte();
            return (ColumnEncoding)b;
        }

        public int PositionCount => _positionCount;

        public long GetStartTime() => _timeColumn.GetLong(0);
        public long GetEndTime() => _timeColumn.GetLong(_positionCount - 1);
        public bool IsEmpty => _positionCount == 0;
        public long GetTimeByIndex(int index) => _timeColumn.GetLong(index);
        public int ValueColumnCount => _valueColumns.Count;
        public Column TimeColumn => _timeColumn;
        public IReadOnlyList<Column> ValueColumns => _valueColumns;
        public Column GetColumn(int columnIndex) => _valueColumns[columnIndex];
    }
}