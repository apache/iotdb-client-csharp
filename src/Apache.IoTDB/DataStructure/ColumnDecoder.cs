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
    public interface ColumnDecoder
    {
        Column ReadColumn(ByteBuffer reader, TSDataType dataType, int positionCount);
    }

    public class BaseColumnDecoder
    {
        
        private static Dictionary<ColumnEncoding, ColumnDecoder> decoders = new Dictionary<ColumnEncoding, ColumnDecoder>
        {
            { ColumnEncoding.Int32Array, new Int32ArrayColumnDecoder() },
            { ColumnEncoding.Int64Array, new Int64ArrayColumnDecoder() },
            { ColumnEncoding.ByteArray, new ByteArrayColumnDecoder() },
            { ColumnEncoding.BinaryArray, new BinaryArrayColumnDecoder() },
            { ColumnEncoding.Rle, new RunLengthColumnDecoder() }
        };

        public static ColumnDecoder GetDecoder(ColumnEncoding encoding)
        {
            if (decoders.TryGetValue(encoding, out var decoder))
                return decoder;
            throw new ArgumentException($"Unsupported encoding: {encoding}");
        }

        public static ColumnEncoding DeserializeColumnEncoding(ByteBuffer reader)
        {
            return (ColumnEncoding)reader.GetByte();
        }
    }

    public static class ColumnDeserializer
    {
        public static bool[] DeserializeNullIndicators(ByteBuffer reader, int positionCount)
        {
            byte b = reader.GetByte();
            bool mayHaveNull = b != 0;
            if (!mayHaveNull)
                return null;
            return DeserializeBooleanArray(reader, positionCount);
        }

        public static bool[] DeserializeBooleanArray(ByteBuffer reader, int size)
        {
            int packedSize = (size + 7) / 8;
            byte[] packedBytes = reader.GetBytesbyLength(packedSize);
            if (packedBytes.Length < packedSize)
                throw new InvalidDataException(
                    $"Boolean array decoding failed: expected {packedSize} bytes for {size} bits, but only received {packedBytes.Length} bytes from buffer."
                );
            bool[] output = new bool[size];
            int currentByte = 0;
            int fullGroups = size & ~0b111;

            for (int pos = 0; pos < fullGroups; pos += 8)
            {
                byte b = packedBytes[currentByte++];
                output[pos + 0] = (b & 0b10000000) != 0;
                output[pos + 1] = (b & 0b01000000) != 0;
                output[pos + 2] = (b & 0b00100000) != 0;
                output[pos + 3] = (b & 0b00010000) != 0;
                output[pos + 4] = (b & 0b00001000) != 0;
                output[pos + 5] = (b & 0b00000100) != 0;
                output[pos + 6] = (b & 0b00000010) != 0;
                output[pos + 7] = (b & 0b00000001) != 0;
            }

            if (size % 8 != 0)
            {
                byte b = packedBytes[packedSize - 1];
                byte mask = 0b10000000;
                for (int pos = fullGroups; pos < size; pos++)
                {
                    output[pos] = (b & mask) != 0;
                    mask >>= 1;
                }
            }

            return output;
        }
    }

    public class Int32ArrayColumnDecoder : ColumnDecoder
    {
        public Column ReadColumn(ByteBuffer reader, TSDataType dataType, int positionCount)
        {
            bool[] nullIndicators = ColumnDeserializer.DeserializeNullIndicators(reader, positionCount);

            switch (dataType)
            {
                case TSDataType.INT32:
                case TSDataType.DATE:
                    int[] intValues = new int[positionCount];
                    for (int i = 0; i < positionCount; i++)
                    {
                        if (nullIndicators != null && nullIndicators[i])
                            continue;
                        intValues[i] = reader.GetInt();
                    }
                    return new IntColumn(0, positionCount, nullIndicators, intValues);
                case TSDataType.FLOAT:
                    float[] floatValues = new float[positionCount];
                    for (int i = 0; i < positionCount; i++)
                    {
                        if (nullIndicators != null && nullIndicators[i])
                            continue;
                        floatValues[i] = reader.GetFloat();
                    }
                    return new FloatColumn(0, positionCount, nullIndicators, floatValues);
                default:
                    throw new ArgumentException($"Invalid data type: {dataType}");
            }
        }
    }

    public class Int64ArrayColumnDecoder : ColumnDecoder
    {
        public Column ReadColumn(ByteBuffer reader, TSDataType dataType, int positionCount)
        {
            bool[] nullIndicators = ColumnDeserializer.DeserializeNullIndicators(reader, positionCount);

            switch (dataType)
            {
                case TSDataType.INT64:
                case TSDataType.TIMESTAMP:
                    long[] longValues = new long[positionCount];
                    for (int i = 0; i < positionCount; i++)
                    {
                        if (nullIndicators != null && nullIndicators[i])
                            continue;
                        longValues[i] = reader.GetLong();
                    }
                    return new LongColumn(0, positionCount, nullIndicators, longValues);
                case TSDataType.DOUBLE:
                    double[] doubleValues = new double[positionCount];
                    for (int i = 0; i < positionCount; i++)
                    {
                        if (nullIndicators != null && nullIndicators[i])
                            continue;
                        doubleValues[i] = reader.GetDouble();
                    }
                    return new DoubleColumn(0, positionCount, nullIndicators, doubleValues);
                default:
                    throw new ArgumentException($"Invalid data type: {dataType}");
            }
        }
    }

    public class ByteArrayColumnDecoder : ColumnDecoder
    {
        public Column ReadColumn(ByteBuffer reader, TSDataType dataType, int positionCount)
        {
            if (dataType != TSDataType.BOOLEAN)
                throw new ArgumentException($"Invalid data type: {dataType}");

            bool[] nullIndicators = ColumnDeserializer.DeserializeNullIndicators(reader, positionCount);
            bool[] values = ColumnDeserializer.DeserializeBooleanArray(reader, positionCount);
            return new BooleanColumn(0, positionCount, nullIndicators, values);
        }
    }

    public class BinaryArrayColumnDecoder : ColumnDecoder
    {
        public Column ReadColumn(ByteBuffer reader, TSDataType dataType, int positionCount)
        {
            if (dataType != TSDataType.TEXT)
                throw new ArgumentException($"Invalid data type: {dataType}");

            bool[] nullIndicators = ColumnDeserializer.DeserializeNullIndicators(reader, positionCount);
            Binary[] values = new Binary[positionCount];

            for (int i = 0; i < positionCount; i++)
            {
                if (nullIndicators != null && nullIndicators[i])
                    continue;
                int length = reader.GetInt();
                byte[] value = reader.GetBytesbyLength(length);
                values[i] = new Binary(value);
            }

            return new BinaryColumn(0, positionCount, nullIndicators, values);
        }
    }

    public class RunLengthColumnDecoder : ColumnDecoder
    {
        public Column ReadColumn(ByteBuffer reader, TSDataType dataType, int positionCount)
        {
            ColumnEncoding encoding = BaseColumnDecoder.DeserializeColumnEncoding(reader);
            ColumnDecoder decoder = BaseColumnDecoder.GetDecoder(encoding);
            Column column = decoder.ReadColumn(reader, dataType, 1);
            return new RunLengthEncodedColumn(column, positionCount);
        }
    }
}