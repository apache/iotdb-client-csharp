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

using Thrift.Transport;

namespace Apache.IoTDB
{
    public class Client
    {
        public IClientRPCService.Client ServiceClient { get; }
        public long SessionId { get; }
        public long StatementId { get; }
        public TFramedTransport Transport { get; }
        public TEndPoint EndPoint { get; }

        public Client(IClientRPCService.Client client, long sessionId, long statementId, TFramedTransport transport, TEndPoint endpoint)
        {
            ServiceClient = client;
            SessionId = sessionId;
            StatementId = statementId;
            Transport = transport;
            EndPoint = endpoint;
        }

        static public TSDataType GetDataTypeByStr(string typeStr)
        {
            return typeStr switch
            {
                "BOOLEAN" => TSDataType.BOOLEAN,
                "INT32" => TSDataType.INT32,
                "INT64" => TSDataType.INT64,
                "FLOAT" => TSDataType.FLOAT,
                "DOUBLE" => TSDataType.DOUBLE,
                "TEXT" => TSDataType.TEXT,
                "STRING" => TSDataType.STRING,
                "BLOB" => TSDataType.BLOB,
                "TIMESTAMP" => TSDataType.TIMESTAMP,
                "DATE" => TSDataType.DATE,
                _ => TSDataType.NONE
            };
        }
    }
}
