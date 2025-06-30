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

namespace Apache.IoTDB;

public partial class SessionPool
{
    public class Builder
    {
        private string _host = "localhost";
        private int _port = 6667;
        private string _username = "root";
        private string _password = "root";
        private int _fetchSize = 1024;
        private string _zoneId = "Asia/Shanghai";
        private int _poolSize = 8;
        private bool _enableRpcCompression = false;
        private int _connectionTimeoutInMs = 500;
        private string _sqlDialect = IoTDBConstant.TREE_SQL_DIALECT;
        private string _database = "";
        private List<string> _nodeUrls = new List<string>();

        public Builder SetHost(string host)
        {
            _host = host;
            return this;
        }

        public Builder SetPort(int port)
        {
            _port = port;
            return this;
        }

        public Builder SetUsername(string username)
        {
            _username = username;
            return this;
        }

        public Builder SetPassword(string password)
        {
            _password = password;
            return this;
        }

        public Builder SetFetchSize(int fetchSize)
        {
            _fetchSize = fetchSize;
            return this;
        }

        public Builder SetZoneId(string zoneId)
        {
            _zoneId = zoneId;
            return this;
        }

        public Builder SetPoolSize(int poolSize)
        {
            _poolSize = poolSize;
            return this;
        }

        public Builder SetEnableRpcCompression(bool enableRpcCompression)
        {
            _enableRpcCompression = enableRpcCompression;
            return this;
        }

        public Builder SetConnectionTimeoutInMs(int timeout)
        {
            _connectionTimeoutInMs = timeout;
            return this;
        }

        public Builder SetNodeUrl(List<string> nodeUrls)
        {
            _nodeUrls = nodeUrls;
            return this;
        }

        protected internal Builder SetSqlDialect(string sqlDialect)
        {
            _sqlDialect = sqlDialect;
            return this;
        }

        protected internal Builder SetDatabase(string database)
        {
            _database = database;
            return this;
        }

        public Builder()
        {
            _host = "localhost";
            _port = 6667;
            _username = "root";
            _password = "root";
            _fetchSize = 1024;
            _zoneId = "Asia/Shanghai";
            _poolSize = 8;
            _enableRpcCompression = false;
            _connectionTimeoutInMs = 500;
            _sqlDialect = IoTDBConstant.TREE_SQL_DIALECT;
            _database = "";
        }

        public SessionPool Build()
        {
            // if nodeUrls is not empty, use nodeUrls to create session pool
            if (_nodeUrls.Count > 0)
            {
                return new SessionPool(_nodeUrls, _username, _password, _fetchSize, _zoneId, _poolSize, _enableRpcCompression, _connectionTimeoutInMs, _sqlDialect, _database);
            }
            return new SessionPool(_host, _port, _username, _password, _fetchSize, _zoneId, _poolSize, _enableRpcCompression, _connectionTimeoutInMs, _sqlDialect, _database);
        }
    }
}
