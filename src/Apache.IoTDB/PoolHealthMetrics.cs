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

using System.Threading;

namespace Apache.IoTDB
{
    /// <summary>
    /// Encapsulates real-time health statistics for connection pool monitoring.
    /// Thread-safe implementation for concurrent access patterns.
    /// </summary>
    internal class PoolHealthMetrics
    {
        private int _reconnectionFailureTally;
        private readonly int _configuredMaxSize;

        public PoolHealthMetrics(int configuredMaxSize)
        {
            _configuredMaxSize = configuredMaxSize;
        }

        public void IncrementReconnectionFailures()
        {
            Interlocked.Increment(ref _reconnectionFailureTally);
        }

        public void ResetAllCounters()
        {
            Interlocked.Exchange(ref _reconnectionFailureTally, 0);
        }

        public int GetReconnectionFailureTally() => Volatile.Read(ref _reconnectionFailureTally);
        
        public int GetConfiguredMaxSize() => _configuredMaxSize;
    }
}
