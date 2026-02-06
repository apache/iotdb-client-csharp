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
using Thrift;

namespace Apache.IoTDB
{
    /// <summary>
    /// Specialized exception raised when SessionPool cannot allocate a client connection.
    /// Includes diagnostic data for troubleshooting capacity and connectivity problems.
    /// </summary>
    public class SessionPoolDepletedException : TException
    {
        /// <summary>
        /// Descriptive explanation of what caused the pool depletion event.
        /// </summary>
        public string DepletionReason { get; }

        /// <summary>
        /// Number of clients available for use at the moment of exception.
        /// </summary>
        public int AvailableClients { get; }

        /// <summary>
        /// Maximum configured pool size limit.
        /// </summary>
        public int TotalPoolSize { get; }

        /// <summary>
        /// Accumulated number of unsuccessful reconnection attempts.
        /// </summary>
        public int FailedReconnections { get; }

        internal SessionPoolDepletedException(
            string depletionReason,
            int availableClients,
            int totalPoolSize,
            int failedReconnections)
            : this(depletionReason, availableClients, totalPoolSize, failedReconnections, null)
        {
        }

        internal SessionPoolDepletedException(
            string depletionReason,
            int availableClients,
            int totalPoolSize,
            int failedReconnections,
            Exception causedBy)
            : base($"SessionPool depletion detected: {depletionReason}", causedBy)
        {
            DepletionReason = depletionReason;
            AvailableClients = availableClients;
            TotalPoolSize = totalPoolSize;
            FailedReconnections = failedReconnections;
        }
    }
}
