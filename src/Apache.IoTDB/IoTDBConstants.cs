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

namespace Apache.IoTDB
{
    public enum TSDataType
    {
        BOOLEAN = 0,
        INT32 = 1,
        INT64 = 2,
        FLOAT = 3,
        DOUBLE = 4,
        TEXT = 5,
        // unused
        NONE = 7,
        TIMESTAMP = 8,
        DATE = 9,
        BLOB = 10,
        STRING = 11,
    }

    public enum TSEncoding
    {
        PLAIN,
        DICTIONARY,
        RLE,
        DIFF,
        TS_2DIFF,
        BITMAP,
        GORILLA_V1,
        REGULAR,
        GORILLA,
        ZIGZAG,
        FREQ,
        CHIMP,
        SPRINTZ,
        RLBE
    }

    public enum Compressor
    {
        UNCOMPRESSED = 0,
        SNAPPY = 1,
        GZIP = 2,
        LZ4 = 7,
        ZSTD = 8,
        LZMA2 = 9
    }
    public enum TemplateQueryType
    {
        COUNT_MEASUREMENTS,
        IS_MEASUREMENT,
        PATH_EXIST,
        SHOW_MEASUREMENTS,
        SHOW_TEMPLATES,
        SHOW_SET_TEMPLATES,
        SHOW_USING_TEMPLATES
    }
    public enum ColumnCategory
    {
        TAG,
        FIELD,
        ATTRIBUTE
    }
    public class TsFileConstant
    {

        public static string TSFILE_SUFFIX = ".tsfile";
        public static string TSFILE_HOME = "TSFILE_HOME";
        public static string TSFILE_CONF = "TSFILE_CONF";
        public static string PATH_ROOT = "root";
        public static string TMP_SUFFIX = "tmp";
        public static string PATH_SEPARATOR = ".";
        public static char PATH_SEPARATOR_CHAR = '.';
        public static string PATH_SEPARATER_NO_REGEX = "\\.";
        public static char DOUBLE_QUOTE = '"';

        public static byte TIME_COLUMN_MASK = (byte)0x80;
        public static byte VALUE_COLUMN_MASK = (byte)0x40;

        private TsFileConstant() { }
    }
    public class IoTDBConstant
    {
        public static string TREE_SQL_DIALECT = "tree";
        public static string TABLE_SQL_DIALECT = "table";
    }
}
