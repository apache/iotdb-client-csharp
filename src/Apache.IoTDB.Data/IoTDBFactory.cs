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

﻿using System.Data.Common;

namespace Apache.IoTDB.Data
{
    /// <summary>
    ///     Creates instances of various Maikebing.Data.IoTDB classes.
    /// </summary>
    public class IoTDBFactory : DbProviderFactory
    {
        private IoTDBFactory()
        {
        }

        /// <summary>
        ///     The singleton instance.
        /// </summary>
        public static readonly IoTDBFactory Instance = new IoTDBFactory();

        /// <summary>
        ///     Creates a new command.
        /// </summary>
        /// <returns>The new command.</returns>
        public override DbCommand CreateCommand()
            => new IoTDBCommand();

        /// <summary>
        ///     Creates a new connection.
        /// </summary>
        /// <returns>The new connection.</returns>
        public override DbConnection CreateConnection()
            => new IoTDBConnection();

        /// <summary>
        ///     Creates a new connection string builder.
        /// </summary>
        /// <returns>The new connection string builder.</returns>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
            => new IoTDBConnectionStringBuilder();

        /// <summary>
        ///     Creates a new parameter.
        /// </summary>
        /// <returns>The new parameter.</returns>
        public override DbParameter CreateParameter()
            => new IoTDBParameter();
    }
}
