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
using System.Diagnostics;
using Apache.IoTDB.DataStructure;

namespace Apache.IoTDB.Samples
{
    public class UtilsTest
    {
        private Utils _utilFunctions = new Utils();
        public void Test()
        {
            TestParseEndPoint();
        }

        public void TestParseEndPoint()
        {
            TestIPv4Address();
            TestIPv6Address();
            TestInvalidInputs();
        }

        private void TestIPv4Address()
        {
            string correctEndpointIPv4 = "192.168.1.1:8080";
            var endpoint = _utilFunctions.ParseTEndPointIpv4AndIpv6Url(correctEndpointIPv4);
            Debug.Assert(endpoint.Ip == "192.168.1.1", "IPv4 address mismatch.");
            Debug.Assert(endpoint.Port == 8080, "IPv4 port mismatch.");
            Console.WriteLine("TestIPv4Address passed.");
        }

        private void TestIPv6Address()
        {
            string correctEndpointIPv6 = "[2001:db8:85a3::8a2e:370:7334]:443";
            var endpoint = _utilFunctions.ParseTEndPointIpv4AndIpv6Url(correctEndpointIPv6);
            Debug.Assert(endpoint.Ip == "2001:db8:85a3::8a2e:370:7334", "IPv6 address mismatch.");
            Debug.Assert(endpoint.Port == 443, "IPv6 port mismatch.");
            Console.WriteLine("TestIPv6Address passed.");
        }

        private void TestInvalidInputs()
        {
            string noPort = "192.168.1.1";
            var endpointNoPort = _utilFunctions.ParseTEndPointIpv4AndIpv6Url(noPort);
            Debug.Assert(string.IsNullOrEmpty(endpointNoPort.Ip) && endpointNoPort.Port == 0, "Failed to handle missing port.");

            string emptyInput = "";
            var endpointEmpty = _utilFunctions.ParseTEndPointIpv4AndIpv6Url(emptyInput);
            Debug.Assert(string.IsNullOrEmpty(endpointEmpty.Ip) && endpointEmpty.Port == 0, "Failed to handle empty input.");

            string invalidFormat = "192.168.1.1:port";
            try
            {
                var endpointInvalid = _utilFunctions.ParseTEndPointIpv4AndIpv6Url(invalidFormat);
                Debug.Fail("Should have thrown an exception due to invalid port.");
            }
            catch (FormatException)
            {
                // Expected exception
            }
            Console.WriteLine("TestInvalidInputs passed.");
        }

        static public void PrintDataSetByType(SessionDataSet sessionDataSet)
        {
            IReadOnlyList<string> columns = sessionDataSet.GetColumnNames();

            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();

            while (sessionDataSet.Next())
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    string columnName = columns[i];
                    string typeStr = sessionDataSet.GetColumnTypes()[i];
                    TSDataType dataType = Client.GetDataTypeByStr(typeStr);

                    switch (dataType)
                    {
                        case TSDataType.BOOLEAN:
                            bool boolValue = sessionDataSet.GetBoolean(columnName);
                            Console.Write(boolValue);
                            break;
                        case TSDataType.INT32:
                            int intValue = sessionDataSet.GetInt(columnName);
                            Console.Write(intValue);
                            break;
                        case TSDataType.INT64:
                        case TSDataType.TIMESTAMP:
                            long longValue = sessionDataSet.GetLong(columnName);
                            Console.Write(longValue);
                            break;
                        case TSDataType.FLOAT:
                            float floatValue = sessionDataSet.GetFloat(columnName);
                            Console.Write(floatValue);
                            break;
                        case TSDataType.DOUBLE:
                            double doubleValue = sessionDataSet.GetDouble(columnName);
                            Console.Write(doubleValue);
                            break;
                        case TSDataType.TEXT:
                        case TSDataType.STRING:
                        case TSDataType.BLOB:
                        case TSDataType.DATE:
                            string stringValue = sessionDataSet.GetString(columnName);
                            Console.Write(stringValue);
                            break;
                        default:
                            break;
                    }
                    Console.Write("\t\t");
                }
                Console.WriteLine();
            }
        }

        static public void PrintDataSetByObject(SessionDataSet sessionDataSet)
        {
            IReadOnlyList<string> columns = sessionDataSet.GetColumnNames();

            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();

            while (sessionDataSet.Next())
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    string columnName = columns[i];
                    Console.Write(sessionDataSet.GetObject(columnName));
                    Console.Write("\t\t");
                }
                Console.WriteLine();
            }
        }

        static public void PrintDataSetByString(SessionDataSet sessionDataSet)
        {
            IReadOnlyList<string> columns = sessionDataSet.GetColumnNames();

            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();

            while (sessionDataSet.Next())
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    string columnName = columns[i];
                    Console.Write(sessionDataSet.GetString(columnName));
                    Console.Write("\t\t");
                }
                Console.WriteLine();
            }
        }
    }
}
