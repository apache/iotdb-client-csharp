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
        BOOLEAN,
        INT32,
        INT64,
        FLOAT,
        DOUBLE,
        TEXT,

        // default value must be 0
        NONE
    }

    public enum TSEncoding
    {
        PLAIN,
        PLAIN_DICTIONARY,
        RLE,
        DIFF,
        TS_2DIFF,
        BITMAP,
        GORILLA_V1,
        REGULAR,
        GORILLA,

        // default value must be 0
        NONE
    }

    public enum Compressor
    {
        UNCOMPRESSED,
        SNAPPY,
        GZIP,
        LZO,
        SDT,
        PAA,
        PLA,
        LZ4
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
}