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
using System.Linq;

namespace Apache.IoTDB.DataStructure
{
    public enum ColumnEncoding : byte
    {
        ByteArray,
        Int32Array,
        Int64Array,
        BinaryArray,
        Rle
    }

    public class Binary
    {
        public byte[] Data { get; }

        public Binary(byte[] data)
        {
            Data = data;
        }
    }

    public interface Column
    {
        TSDataType GetDataType();
        ColumnEncoding GetEncoding();
        bool GetBoolean(int position);
        int GetInt(int position);
        long GetLong(int position);
        float GetFloat(int position);
        double GetDouble(int position);
        Binary GetBinary(int position);
        object GetObject(int position);

        bool[] GetBooleans();
        int[] GetInts();
        long[] GetLongs();
        float[] GetFloats();
        double[] GetDoubles();
        Binary[] GetBinaries();
        object[] GetObjects();

        bool MayHaveNull();
        bool IsNull(int position);
        bool[] GetNulls();

        int GetPositionCount();
    }

    public abstract class BaseColumn : Column
    {
        public virtual TSDataType GetDataType() => throw new NotSupportedException();
        public virtual ColumnEncoding GetEncoding() => throw new NotSupportedException();
        public virtual bool GetBoolean(int position) => throw new NotSupportedException("GetBoolean not supported");
        public virtual int GetInt(int position) => throw new NotSupportedException("GetInt not supported");
        public virtual long GetLong(int position) => throw new NotSupportedException("GetLong not supported");
        public virtual float GetFloat(int position) => throw new NotSupportedException("GetFloat not supported");
        public virtual double GetDouble(int position) => throw new NotSupportedException("GetDouble not supported");
        public virtual Binary GetBinary(int position) => throw new NotSupportedException("GetBinary not supported");
        public virtual object GetObject(int position) => throw new NotSupportedException("GetObject not supported");

        public virtual bool[] GetBooleans() => throw new NotSupportedException("GetBooleans not supported");
        public virtual int[] GetInts() => throw new NotSupportedException("GetInts not supported");
        public virtual long[] GetLongs() => throw new NotSupportedException("GetLongs not supported");
        public virtual float[] GetFloats() => throw new NotSupportedException("GetFloats not supported");
        public virtual double[] GetDoubles() => throw new NotSupportedException("GetDoubles not supported");
        public virtual Binary[] GetBinaries() => throw new NotSupportedException("GetBinaries not supported");
        public virtual object[] GetObjects() => throw new NotSupportedException("GetObjects not supported");

        public virtual bool MayHaveNull() => false;
        public virtual bool IsNull(int position) => false;
        public virtual bool[] GetNulls() => new bool[GetPositionCount()];
        public abstract int GetPositionCount();
    }

    public abstract class PrimitiveColumn<T> : BaseColumn
    {
        protected readonly T[] _values;
        protected readonly int _arrayOffset;
        protected readonly int _positionCount;
        protected readonly bool[] _valueIsNull;

        private readonly TSDataType _dataType;
        private readonly ColumnEncoding _encoding;

        protected PrimitiveColumn(
            TSDataType dataType,
            ColumnEncoding encoding,
            int arrayOffset,
            int positionCount,
            bool[] valueIsNull,
            T[] values)
        {
            if (arrayOffset < 0)
                throw new ArgumentException("arrayOffset is negative");
            if (positionCount < 0)
                throw new ArgumentException("positionCount is negative");
            if (values == null || values.Length - arrayOffset < positionCount)
                throw new ArgumentException("values array is too short");
            if (valueIsNull != null && valueIsNull.Length - arrayOffset < positionCount)
                throw new ArgumentException("isNull array is too short");

            _dataType = dataType;
            _encoding = encoding;
            _arrayOffset = arrayOffset;
            _positionCount = positionCount;
            _valueIsNull = valueIsNull;
            _values = values;
        }

        public override TSDataType GetDataType() => _dataType;
        public override ColumnEncoding GetEncoding() => _encoding;

        public override bool MayHaveNull() => _valueIsNull != null;
        public override bool IsNull(int position) => _valueIsNull?[position + _arrayOffset] ?? false;
        public override bool[] GetNulls()
        {
            if (_valueIsNull == null)
                return new bool[_positionCount];

            return _valueIsNull.Skip(_arrayOffset).Take(_positionCount).ToArray();
        }

        public override int GetPositionCount() => _positionCount;
    }

    public class TimeColumn : PrimitiveColumn<long>
    {
        public TimeColumn(int arrayOffset, int positionCount, long[] values)
        : base(
            dataType: TSDataType.INT64,
            encoding: ColumnEncoding.Int64Array,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: null,
            values: values)
        { }

        public override long GetLong(int position) => _values[position + _arrayOffset];
        public override long[] GetLongs() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetLong(position);
        public override object[] GetObjects() => GetLongs().Cast<object>().ToArray();

        public long GetStartTime() => _values[_arrayOffset];
        public long GetEndTime() => _values[_arrayOffset + _positionCount - 1];
        public long[] GetTimes() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
    }

    public class BinaryColumn : PrimitiveColumn<Binary>
    {
        public BinaryColumn(int arrayOffset, int positionCount, bool[] valueIsNull, Binary[] values)
        : base(
            TSDataType.TEXT,
            ColumnEncoding.BinaryArray,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: valueIsNull,
            values: values)
        { }

        public override Binary GetBinary(int position) => _values[position + _arrayOffset];
        public override Binary[] GetBinaries() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetBinary(position);
        public override object[] GetObjects() => GetBinaries().Cast<object>().ToArray();
    }

    public class IntColumn : PrimitiveColumn<int>
    {
        public IntColumn(int arrayOffset, int positionCount, bool[] valueIsNull, int[] values)
        : base(
            TSDataType.INT32,
            ColumnEncoding.Int32Array,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: valueIsNull,
            values: values)
        { }

        public override int GetInt(int position) => _values[position + _arrayOffset];
        public override int[] GetInts() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetInt(position);
        public override object[] GetObjects() => GetInts().Cast<object>().ToArray();
    }

    public class FloatColumn : PrimitiveColumn<float>
    {
        public FloatColumn(int arrayOffset, int positionCount, bool[] valueIsNull, float[] values)
        : base(
            TSDataType.FLOAT,
            ColumnEncoding.Int32Array,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: valueIsNull,
            values: values)
        { }

        public override float GetFloat(int position) => _values[position + _arrayOffset];
        public override float[] GetFloats() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetFloat(position);
        public override object[] GetObjects() => GetFloats().Cast<object>().ToArray();
    }

    public class LongColumn : PrimitiveColumn<long>
    {
        public LongColumn(int arrayOffset, int positionCount, bool[] valueIsNull, long[] values)
        : base(
            TSDataType.INT64,
            ColumnEncoding.Int64Array,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: valueIsNull,
            values: values)
        { }

        public override long GetLong(int position) => _values[position + _arrayOffset];
        public override long[] GetLongs() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetLong(position);
        public override object[] GetObjects() => GetLongs().Cast<object>().ToArray();
    }

    public class DoubleColumn : PrimitiveColumn<double>
    {
        public DoubleColumn(int arrayOffset, int positionCount, bool[] valueIsNull, double[] values)
        : base(
            TSDataType.DOUBLE,
            ColumnEncoding.Int64Array,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: valueIsNull,
            values: values)
        { }

        public override double GetDouble(int position) => _values[position + _arrayOffset];
        public override double[] GetDoubles() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetDouble(position);
        public override object[] GetObjects() => GetDoubles().Cast<object>().ToArray();
    }

    public class BooleanColumn : PrimitiveColumn<bool>
    {
        public BooleanColumn(int arrayOffset, int positionCount, bool[] valueIsNull, bool[] values)
        : base(
            TSDataType.BOOLEAN,
            ColumnEncoding.ByteArray,
            arrayOffset: arrayOffset,
            positionCount: positionCount,
            valueIsNull: valueIsNull,
            values: values)
        { }

        public override bool GetBoolean(int position) => _values[position + _arrayOffset];
        public override bool[] GetBooleans() => _values.Skip(_arrayOffset).Take(_positionCount).ToArray();
        public override object GetObject(int position) => GetBoolean(position);
        public override object[] GetObjects() => GetBooleans().Cast<object>().ToArray();
    }

    public class RunLengthEncodedColumn : BaseColumn
    {
        private readonly Column _value;
        private readonly int _positionCount;

        public RunLengthEncodedColumn(Column value, int positionCount)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value.GetPositionCount() != 1)
                throw new ArgumentException("Expected value to contain a single position");
            if (positionCount < 0)
                throw new ArgumentException("positionCount is negative");

            // Unwrap nested RLE columns
            _value = value is RunLengthEncodedColumn rle ? rle.Value : value;
            _positionCount = positionCount;
        }

        public Column Value => _value;

        public override TSDataType GetDataType() => _value.GetDataType();
        public override ColumnEncoding GetEncoding() => ColumnEncoding.Rle;

        public override bool GetBoolean(int position) => _value.GetBoolean(0);
        public override int GetInt(int position) => _value.GetInt(0);
        public override long GetLong(int position) => _value.GetLong(0);
        public override float GetFloat(int position) => _value.GetFloat(0);
        public override double GetDouble(int position) => _value.GetDouble(0);
        public override Binary GetBinary(int position) => _value.GetBinary(0);
        public override object GetObject(int position) => _value.GetObject(0);

        public override bool[] GetBooleans() => Enumerable.Repeat(_value.GetBoolean(0), _positionCount).ToArray();
        public override int[] GetInts() => Enumerable.Repeat(_value.GetInt(0), _positionCount).ToArray();
        public override long[] GetLongs() => Enumerable.Repeat(_value.GetLong(0), _positionCount).ToArray();
        public override float[] GetFloats() => Enumerable.Repeat(_value.GetFloat(0), _positionCount).ToArray();
        public override double[] GetDoubles() => Enumerable.Repeat(_value.GetDouble(0), _positionCount).ToArray();
        public override Binary[] GetBinaries() => Enumerable.Repeat(_value.GetBinary(0), _positionCount).ToArray();
        public override object[] GetObjects() => Enumerable.Repeat(_value.GetObject(0), _positionCount).ToArray();

        public override bool MayHaveNull() => _value.MayHaveNull();
        public override bool IsNull(int position) => _value.IsNull(0);
        public override bool[] GetNulls() => Enumerable.Repeat(_value.IsNull(0), _positionCount).ToArray();

        public override int GetPositionCount() => _positionCount;
    }
}
