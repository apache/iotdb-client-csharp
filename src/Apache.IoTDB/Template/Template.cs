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
using Apache.IoTDB.DataStructure;
using Thrift;
namespace Apache.IoTDB
{
    public class Template
    {
        private string name;
        private Dictionary<string, TemplateNode> children;
        private bool shareTime;
        public Template(string name, bool shareTime)
        {
            this.name = name;
            this.children = new Dictionary<string, TemplateNode>();
            this.shareTime = shareTime;
        }
        public Template(string name)
        {
            this.name = name;
            this.children = new Dictionary<string, TemplateNode>();
            this.shareTime = false;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public bool ShareTime
        {
            get
            {
                return shareTime;
            }
            set
            {
                shareTime = value;
            }
        }
        public void addToTemplate(TemplateNode child)
        {
            if (this.children.ContainsKey(child.Name))
            {
                throw new Exception("Duplicated child of node in template.");
            }
            this.children.Add(child.Name, child);
        }

        public void deleteFromTemplate(string name)
        {
            if (this.children.ContainsKey(name))
            {
                this.children.Remove(name);
            }
            else
            {
                throw new Exception("It is not a direct child of the template: " + name);
            }
        }

        public byte[] ToBytes()
        {
            var buffer = new ByteBuffer();
            var stack = new Stack<KeyValuePair<string, TemplateNode>>();
            var alignedPrefix = new HashSet<string>();
            buffer.AddStr(this.name);
            buffer.AddBool(this.shareTime);
            if (this.shareTime)
            {
                alignedPrefix.Add("");
            }

            foreach (var child in this.children.Values)
            {
                stack.Push(new KeyValuePair<string, TemplateNode>("", child));
            }

            while (stack.Count != 0)
            {
                var pair = stack.Pop();
                var prefix = pair.Key;
                var curNode = pair.Value;
                var fullPath = prefix;

                if (!curNode.isMeasurement())
                {
                    if (!"".Equals(prefix))
                    {
                        fullPath += TsFileConstant.PATH_SEPARATOR;
                    }
                    fullPath += curNode.Name;
                    if (curNode.isShareTime())
                    {
                        alignedPrefix.Add(fullPath);
                    }

                    foreach (var child in curNode.getChildren().Values)
                    {
                        stack.Push(new KeyValuePair<string, TemplateNode>(fullPath, child));
                    }
                }
                else
                {
                    buffer.AddStr(prefix);
                    if (alignedPrefix.Contains(prefix))
                    {
                        buffer.AddBool(true);
                    }
                    else
                    {
                        buffer.AddBool(false);
                    }
                    foreach (var singleByte in curNode.ToBytes())
                    {
                        buffer.AddByte(singleByte);
                    }
                }
            }
            return buffer.GetBuffer();

        }
    }
}