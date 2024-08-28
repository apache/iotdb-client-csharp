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

using System.Collections.Generic;
using System.IO;
using Apache.IoTDB.DataStructure;
namespace Apache.IoTDB
{
    public abstract class TemplateNode
    {
        private string name;
        public TemplateNode(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        public virtual Dictionary<string, TemplateNode> getChildren()
        {
            return null;
        }

        public virtual void addChild(TemplateNode node) { }
        public virtual void deleteChild(TemplateNode node) { }
        public virtual bool isMeasurement()
        {
            return false;
        }
        public virtual bool isShareTime()
        {
            return false;
        }
        public virtual byte[] ToBytes()
        {
            return null;
        }
    }
}