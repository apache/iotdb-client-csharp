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
using System.Threading;
using System.Threading.Tasks;
using Apache.IoTDB.DataStructure;
namespace Apache.IoTDB.Samples
{
    public partial class SessionPoolTest
    {
        public async Task TestCreateAndDropSchemaTemplate()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DropSchemaTemplateAsync(testTemplateName);

            MeasurementNode node1 = new MeasurementNode(testMeasurements[1], TSDataType.INT32, TSEncoding.PLAIN, Compressor.SNAPPY);
            MeasurementNode node2 = new MeasurementNode(testMeasurements[2], TSDataType.INT64, TSEncoding.PLAIN, Compressor.SNAPPY);
            MeasurementNode node3 = new MeasurementNode(testMeasurements[3], TSDataType.DOUBLE, TSEncoding.PLAIN, Compressor.SNAPPY);
            MeasurementNode node4 = new MeasurementNode(testMeasurements[4], TSDataType.FLOAT, TSEncoding.PLAIN, Compressor.SNAPPY);

            Template template = new Template(testTemplateName);
            template.addToTemplate(node1);
            template.addToTemplate(node2);
            template.addToTemplate(node3);
            template.addToTemplate(node4);

            status = await session_pool.CreateSchemaTemplateAsync(template);
            System.Diagnostics.Debug.Assert(status == 0);
            var templates = await session_pool.ShowAllTemplatesAsync();
            foreach (var t in templates)
            {
                Console.WriteLine("template name :\t{0}", t);
            }
            status = await session_pool.DropSchemaTemplateAsync(testTemplateName);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestCreateAndDropSchemaTemplate Passed!");
        }

        public async Task TestSetAndUnsetSchemaTemplate()
        {
            var session_pool = new SessionPool(host, port, poolSize);
            await session_pool.Open(false);
            if (debug) session_pool.OpenDebugMode();

            System.Diagnostics.Debug.Assert(session_pool.IsOpen());
            var status = 0;
            await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.UnsetSchemaTemplateAsync(string.Format("{0}.{1}", testDatabaseName, testDevice), "template");
            await session_pool.DropSchemaTemplateAsync(testTemplateName);

            MeasurementNode node1 = new MeasurementNode(testMeasurements[1], TSDataType.INT32, TSEncoding.PLAIN, Compressor.SNAPPY);
            MeasurementNode node2 = new MeasurementNode(testMeasurements[2], TSDataType.INT64, TSEncoding.PLAIN, Compressor.SNAPPY);
            MeasurementNode node3 = new MeasurementNode(testMeasurements[3], TSDataType.DOUBLE, TSEncoding.PLAIN, Compressor.SNAPPY);
            MeasurementNode node4 = new MeasurementNode(testMeasurements[4], TSDataType.FLOAT, TSEncoding.PLAIN, Compressor.SNAPPY);

            Template template = new Template(testTemplateName);
            template.addToTemplate(node1);
            template.addToTemplate(node2);
            template.addToTemplate(node3);
            template.addToTemplate(node4);

            status = await session_pool.CreateSchemaTemplateAsync(template);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.SetSchemaTemplateAsync(testTemplateName, string.Format("{0}.{1}", testDatabaseName, testDevice));
            var paths = await session_pool.ShowPathsTemplateSetOnAsync(testTemplateName);
            foreach (var p in paths)
            {
                Console.WriteLine("path :\t{0}", p);
            }
            status = await session_pool.UnsetSchemaTemplateAsync(string.Format("{0}.{1}", testDatabaseName, testDevice), testTemplateName);
            status = await session_pool.DropSchemaTemplateAsync(testTemplateName);
            System.Diagnostics.Debug.Assert(status == 0);
            status = await session_pool.DeleteDatabaseAsync(testDatabaseName);
            await session_pool.Close();
            Console.WriteLine("TestSetAndUnsetSchemaTemplate Passed!");
        }
    }

}