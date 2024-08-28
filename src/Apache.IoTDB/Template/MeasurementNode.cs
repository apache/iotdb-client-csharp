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

using System.IO;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;

namespace Apache.IoTDB
{
    public class MeasurementNode : TemplateNode
    {
        private TSDataType dataType;
        private TSEncoding encoding;
        private Compressor compressor;
        public MeasurementNode(string name, TSDataType dataType, TSEncoding encoding, Compressor compressor) : base(name)
        {
            this.dataType = dataType;
            this.encoding = encoding;
            this.compressor = compressor;
        }
        public override bool isMeasurement()
        {
            return true;
        }
        public TSDataType DataType
        {
            get
            {
                return dataType;
            }
        }
        public TSEncoding Encoding
        {
            get
            {
                return encoding;
            }
        }
        public Compressor Compressor
        {
            get
            {
                return compressor;
            }
        }

        public override byte[] ToBytes()
        {
            var buffer = new ByteBuffer();
            buffer.AddStr(this.Name);
            buffer.AddByte((byte)this.DataType);
            buffer.AddByte((byte)this.Encoding);
            buffer.AddByte((byte)this.Compressor);
            return buffer.GetBuffer();
        }



    }
}