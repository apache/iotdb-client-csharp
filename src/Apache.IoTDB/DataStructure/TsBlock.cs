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
            if (valueColumns == null)
                throw new ArgumentNullException(nameof(valueColumns));

            _timeColumn = timeColumn;
            _valueColumns = new List<Column>(valueColumns);
            _positionCount = positionCount;
        }

        public static TsBlock Deserialize(ByteBuffer reader)
        {

            // Read value column count
            var valueColumnCount = reader.GetInt();

            // Read value column data types
            var valueColumnDataTypes = new TSDataType[valueColumnCount];
            for (int i = 0; i < valueColumnCount; i++)
            {
                valueColumnDataTypes[i] = DeserializeDataType(reader);
            }

            // Read position count
            var positionCount = ReadBigEndianInt32(reader);

            // Read column encodings (valueColumnCount + 1 for time column)
            var columnEncodings = new ColumnEncoding[valueColumnCount + 1];
            for (int i = 0; i < valueColumnCount + 1; i++)
            {
                columnEncodings[i] = DeserializeColumnEncoding(reader);
            }

            // Read time column
            var timeColumnDecoder = baseColumnDecoder.GetDecoder(columnEncodings[0]);
            var timeColumn = timeColumnDecoder.ReadColumn(reader, TSDataType.INT64, positionCount);

            // Read value columns
            var valueColumns = new Column[valueColumnCount];
            for (int i = 0; i < valueColumnCount; i++)
            {
                var decoder = baseColumnDecoder.GetDecoder(columnEncodings[i + 1]);
                valueColumns[i] = decoder.ReadColumn(reader, valueColumnDataTypes[i], positionCount);
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

        private static int ReadBigEndianInt32(ByteBuffer reader)
        {
            byte[] bytes = reader.GetBinary();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
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