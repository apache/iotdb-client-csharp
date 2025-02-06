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
using Thrift;

namespace Apache.IoTDB.DataStructure
{
    public class RowRecord
    {
        public long Timestamps { get; }
        public List<object> Values { get; }
        public List<string> Measurements { get; }
        public List<TSDataType> DataTypes { get; }

        public RowRecord(DateTime timestamp, List<object> values, List<string> measurements, List<string> dataTypes)
            :this(new DateTimeOffset(timestamp.ToUniversalTime()).ToUnixTimeMilliseconds(), values,measurements, dataTypes)
        {
        }

        public RowRecord(DateTime timestamp, List<object> values, List<string> measurements, List<TSDataType> dataTypes)
            :this(new DateTimeOffset(timestamp.ToUniversalTime()).ToUnixTimeMilliseconds(), values,measurements, dataTypes)
        {
        }

        [Obsolete("Use the constructor with List<TSDataType> instead")]
        public RowRecord(DateTime timestamp, List<object> values, List<string> measurements)
            :this(new DateTimeOffset(timestamp.ToUniversalTime()).ToUnixTimeMilliseconds(), values,measurements)
        {
        }
        [Obsolete("Use the constructor with List<TSDataType> instead")]
        public RowRecord(long timestamps, List<object> values, List<string> measurements)
        {
            Timestamps = timestamps;
            Values = values;
            Measurements = measurements;
        }
        public RowRecord(long timestamps, List<object> values, List<string> measurements, List<string> dataTypes){
            Timestamps = timestamps;
            Values = values;
            Measurements = measurements;
            DataTypes = new List<TSDataType>();
            foreach (var dataType in dataTypes)
            {
                switch (dataType)
                {
                    case "BOOLEAN":
                        DataTypes.Add(TSDataType.BOOLEAN);
                        break;
                    case "INT32":
                        DataTypes.Add(TSDataType.INT32);
                        break;
                    case "INT64":
                        DataTypes.Add(TSDataType.INT64);
                        break;
                    case "FLOAT":
                        DataTypes.Add(TSDataType.FLOAT);
                        break;
                    case "DOUBLE":
                        DataTypes.Add(TSDataType.DOUBLE);
                        break;
                    case "TEXT":
                        DataTypes.Add(TSDataType.TEXT);
                        break;
                    case "TIMESTAMP":
                        DataTypes.Add(TSDataType.TIMESTAMP);
                        break;
                    case "BLOB":
                        DataTypes.Add(TSDataType.BLOB);
                        break;
                    case "DATE":
                        DataTypes.Add(TSDataType.DATE);
                        break;
                    case "STRING":
                        DataTypes.Add(TSDataType.STRING);
                        break;
                    default:
                        throw new TException($"Unsupported data type:{dataType}", null);
                }
            }
        }
        public RowRecord(long timestamps, List<object> values, List<string> measurements, List<TSDataType> dataTypes)
        {
            Timestamps = timestamps;
            Values = values;
            Measurements = measurements;
            DataTypes = dataTypes;
        }

        public void Append(string measurement, object value, TSDataType dataType)
        {
            Values.Add(value);
            Measurements.Add(measurement);
            DataTypes.Add(dataType);
        }

        public DateTime GetDateTime()
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(Timestamps).DateTime.ToLocalTime();
        }

        public override string ToString()
        {
            var str = "TimeStamp";
            foreach (var measurement in Measurements)
            {
                str += "\t\t";
                str += measurement;
            }

            str += "\n";

            str += Timestamps.ToString();
            foreach (var rowValue in Values)
            {
                str += "\t\t";
                if(rowValue is byte[] bytes)
                {
                  str += Utils.ByteArrayToHexString(bytes);
                }
                else
                {
                  str += rowValue.ToString();
                }
            }

            return str;
        }
        
        public Type GetCrlType(int index)
        {
            Type tSDataType =  typeof(object);
            var valueType = Values[index];
            switch (valueType)
            {
                case bool _:
                    tSDataType = typeof( bool);
                    break;
                case int _:
                    tSDataType = typeof(int);
                    break;
                case long _:
                    tSDataType = typeof(long);
                    break;
                case float _:
                    tSDataType = typeof(float);
                    break;
                case double _:
                    tSDataType = typeof(double);
                    break;
                case string _:
                    tSDataType = typeof(string);
                    break;
                case byte[] _:
                    tSDataType = typeof(byte[]);
                    break;
                case DateTime _:
                    tSDataType = typeof(DateTime);
                    break;
            }
            return tSDataType;
        }
        public byte[] ToBytes()
        {
            var buffer = new ByteBuffer(Values.Count * 8);

            for (int i = 0; i < Values.Count; i++)
            {
                var value = Values[i];
                var dataType = DataTypes != null && DataTypes.Count == Values.Count ? DataTypes[i] : GetTSDataType(value);

                switch (dataType)
                {
                    case TSDataType.BOOLEAN:
                        buffer.AddByte((byte) TSDataType.BOOLEAN);
                        buffer.AddBool((bool)value);
                        break;
                    case TSDataType.INT32:
                        buffer.AddByte((byte) TSDataType.INT32);
                        buffer.AddInt((int)value);
                        break;
                    case TSDataType.INT64:
                        buffer.AddByte((byte) TSDataType.INT64);
                        buffer.AddLong((long)value);
                        break;
                    case TSDataType.FLOAT:
                        buffer.AddByte((byte) TSDataType.FLOAT);
                        buffer.AddFloat((float)value);
                        break;
                    case TSDataType.DOUBLE:
                        buffer.AddByte((byte) TSDataType.DOUBLE);
                        buffer.AddDouble((double)value);
                        break;
                    case TSDataType.TEXT:
                        buffer.AddByte((byte) TSDataType.TEXT);
                        buffer.AddStr((string)value);
                        break;
                    case TSDataType.TIMESTAMP:
                        buffer.AddByte((byte) TSDataType.TIMESTAMP);
                        buffer.AddLong((long)value);
                        break;
                    case TSDataType.BLOB:
                        buffer.AddByte((byte) TSDataType.BLOB);
                        buffer.AddBinary((byte[])value);
                        break;
                    case TSDataType.DATE:
                        buffer.AddByte((byte) TSDataType.DATE);
                        buffer.AddInt(Utils.ParseDateToInt((DateTime)value));
                        break;
                    case TSDataType.STRING:
                        buffer.AddByte((byte) TSDataType.STRING);
                        buffer.AddStr((string)value);
                        break;
                    default:
                        throw new TException($"Unsupported data type:{dataType}", null);
                }
            }

            return buffer.GetBuffer();
        }

        private TSDataType GetTSDataType(object value)
        {
            switch (value)
            {
                case bool _:
                    return TSDataType.BOOLEAN;
                case int _:
                    return TSDataType.INT32;
                case long _:
                    return TSDataType.INT64;
                case float _:
                    return TSDataType.FLOAT;
                case double _:
                    return TSDataType.DOUBLE;
                case string _:
                    return TSDataType.TEXT;
                case byte[] _:
                    return TSDataType.BLOB;
                case DateTime _:
                    return TSDataType.DATE;
                default:
                    throw new TException($"Unsupported data type:{value.GetType()}", null);
            }
        }
    }
}