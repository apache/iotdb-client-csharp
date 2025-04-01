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
using System.CommandLine;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Apache.IoTDB.Samples
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var singleOption = new Option<string>(
            "--single",
            () => "localhost",
            description: "Use single endpoint (e.g. --single localhost)");

            var multiOption = new Option<List<string>>(
                "--multi",
                description: "Use multiple endpoints (e.g. --multi localhost:6667 localhost:6668)")
            {
                AllowMultipleArgumentsPerToken = true
            };

            var rootCommand = new RootCommand
        {
            singleOption,
            multiOption
        };

            rootCommand.SetHandler(async (string single, List<string> multi) =>
            {
                var utilsTest = new UtilsTest();
                utilsTest.TestParseEndPoint();

                SessionPoolTest sessionPoolTest;

                if (!string.IsNullOrEmpty(single) && (multi == null || multi.Count == 0))
                {
                    sessionPoolTest = new SessionPoolTest(single);
                }
                else if (multi != null && multi.Count != 0)
                {
                    sessionPoolTest = new SessionPoolTest(multi);
                }
                else
                {
                    Console.WriteLine("Please specify either --single or --multi endpoints.");
                    return;
                }

                await sessionPoolTest.Test();

                var tableSessionPoolTest = new TableSessionPoolTest(sessionPoolTest);
                await tableSessionPoolTest.Test();

            }, singleOption, multiOption);

            await rootCommand.InvokeAsync(args);
        }

        public static void OpenDebugMode(this SessionPool session)
        {
            session.OpenDebugMode(builder =>
            {
                builder.AddConsole();
                builder.AddNLog();
            });
        }
        public static void OpenDebugMode(this TableSessionPool session)
        {
            session.OpenDebugMode(builder =>
            {
                builder.AddConsole();
                builder.AddNLog();
            });
        }
    }
}
