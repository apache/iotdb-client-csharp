using System;
using System.Collections.Generic;
using Thrift;

namespace Apache.IoTDB.DataStructure
{
    public class TSBlock
    {
        private readonly List<string> _columnNames;
        private readonly Dictionary<int, int> _duplicateLocation;
        private readonly List<string> _columnTypeLst;
        private readonly int _flag = 0x80;
        
        private ByteBuffer _timeBuffer;
        private List<ByteBuffer> _valueBufferLst;
        private List<ByteBuffer> _bitmapBufferLst;
        private byte[] _currentBitmap;
        private int _rowIndex;
        public int RowCount { get; private set; }
        public RowRecord _cachedRowRecord;

        public TSBlock(
            TSQueryDataSet queryDataSet,
            List<string> columnNames,
            List<string> columnTypeLst,
            Dictionary<int, int> duplicateLocation)
        {
            _columnNames = columnNames;
            _columnTypeLst = columnTypeLst;
            _duplicateLocation = duplicateLocation;

            InitializeBuffers(queryDataSet);
            ResetState();
        }

        private void InitializeBuffers(TSQueryDataSet queryDataSet)
        {
            _timeBuffer = new ByteBuffer(queryDataSet.Time);
            _valueBufferLst = new List<ByteBuffer>();
            _bitmapBufferLst = new List<ByteBuffer>();
            
            RowCount = queryDataSet.Time.Length / sizeof(long);
            
            foreach (var valueBuffer in queryDataSet.ValueList)
            {
                _valueBufferLst.Add(new ByteBuffer(valueBuffer));
            }
            foreach (var bitmapBuffer in queryDataSet.BitmapList)
            {
                _bitmapBufferLst.Add(new ByteBuffer(bitmapBuffer));
            }
            
            _currentBitmap = new byte[_valueBufferLst.Count];
        }

        private void ResetState()
        {
            _rowIndex = 0;
            Array.Clear(_currentBitmap, 0, _currentBitmap.Length);
        }

        public bool HasNext()
        {
            return _rowIndex < RowCount;
        }

        public RowRecord Next()
        {
            if (!HasNext()) return null;

            var fieldList = new List<object>();
            long timestamp = _timeBuffer.GetLong();

            for (int i = 0; i < _valueBufferLst.Count; i++)
            {
                if (_duplicateLocation.TryGetValue(i, out int dupIndex))
                {
                    fieldList.Add(fieldList[dupIndex]);
                    continue;
                }

                if (_rowIndex % 8 == 0)
                {
                    _currentBitmap[i] = _bitmapBufferLst[i].GetByte();
                }

                object value = IsNull(i) ? null : ReadValue(i);
                fieldList.Add(value);
            }

            _rowIndex++;
            _cachedRowRecord = new RowRecord(timestamp, fieldList, _columnNames);
            return _cachedRowRecord;
        }

        private bool IsNull(int columnIndex)
        {
            byte bitmap = _currentBitmap[columnIndex];
            int shift = _rowIndex % 8;
            return ((_flag >> shift) & bitmap) == 0;
        }

        private object ReadValue(int columnIndex)
        {
            var dataType = GetDataTypeFromStr(_columnTypeLst[columnIndex]);
            var buffer = _valueBufferLst[columnIndex];

            return dataType switch
            {
                TSDataType.BOOLEAN => buffer.GetBool(),
                TSDataType.INT32 => buffer.GetInt(),
                TSDataType.DATE => Utils.ParseIntToDate(buffer.GetInt()),
                TSDataType.INT64 => buffer.GetLong(),
                TSDataType.TIMESTAMP => buffer.GetLong(),
                TSDataType.FLOAT => buffer.GetFloat(),
                TSDataType.DOUBLE => buffer.GetDouble(),
                TSDataType.TEXT => buffer.GetStr(),
                TSDataType.STRING => buffer.GetStr(),
                TSDataType.BLOB => buffer.GetBinary(),
                _ => throw new TException("Unsupported data type", null)
            };
        }
        private TSDataType GetDataTypeFromStr(string str)
        {
            return str switch
            {
                "BOOLEAN" => TSDataType.BOOLEAN,
                "INT32" => TSDataType.INT32,
                "INT64" => TSDataType.INT64,
                "FLOAT" => TSDataType.FLOAT,
                "DOUBLE" => TSDataType.DOUBLE,
                "TEXT" => TSDataType.TEXT,
                "NULLTYPE" => TSDataType.NONE,
                "TIMESTAMP" => TSDataType.TIMESTAMP,
                "DATE" => TSDataType.DATE,
                "BLOB" => TSDataType.BLOB,
                "STRING" => TSDataType.STRING,
                _ => TSDataType.STRING
            };
        }
    }

}
