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
            if (timeColumn.GetPositionCount() != positionCount)
                throw new ArgumentException(
                    $"input positionCount {positionCount} does not match timeColumn.positionCount {timeColumn.GetPositionCount()}"
                );
            for (int i = 0; i <= ValueColumnCount; i++)
            {
                if (valueColumns[i].GetPositionCount() != positionCount)
                throw new ArgumentException(
                    $"input positionCount {positionCount} does not match valueColumn{i}.positionCount {valueColumns[i].GetPositionCount()}"
                );
            }
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
            var positionCount = reader.GetInt();

            // Read column encodings 
            // Read time column encoding
            ColumnEncoding timeColumnEncodings = DeserializeColumnEncoding(reader);
            
            // Read value column encodings
            var valuecolumnEncodings = new ColumnEncoding[valueColumnCount];
            for (int i = 0; i < valueColumnCount + 1; i++)
            {
                valuecolumnEncodings[i] = DeserializeColumnEncoding(reader);
            }

            // Read time column
            var timeColumnDecoder = BaseColumnDecoder.GetDecoder(timeColumnEncodings);
            var timeColumn = timeColumnDecoder.ReadColumn(reader, TSDataType.INT64, positionCount);

            // Read value columns
            var valueColumns = new Column[valueColumnCount];
            for (int i = 0; i < valueColumnCount; i++)
            {
                var decoder = BaseColumnDecoder.GetDecoder(valuecolumnEncodings[i]);
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