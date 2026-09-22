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

namespace Apache.IoTDB
{
    /// <summary>
    /// Outcome of a <see cref="SessionPool.CheckHealthAsync"/> probe.
    /// </summary>
    public enum SessionPoolHealthStatus
    {
        /// <summary>
        /// The pool has not been opened yet, or it has already been closed.
        /// </summary>
        NotOpen,

        /// <summary>
        /// The server answered the probe on a pooled connection.
        /// </summary>
        Healthy,

        /// <summary>
        /// The pool is open but could not be probed, because no connection was idle at that moment.
        /// This says nothing about the server: the pool may simply be saturated by concurrent work.
        /// </summary>
        Degraded,

        /// <summary>
        /// A connection was available but the server did not answer the probe.
        /// </summary>
        Unhealthy
    }

    /// <summary>
    /// A point-in-time snapshot of pool connectivity, returned by <see cref="SessionPool.CheckHealthAsync"/>.
    /// Unlike <see cref="SessionPool.IsOpen"/> - which only reports whether the caller has opened the pool -
    /// this reflects whether the server actually answered just now.
    /// </summary>
    public class SessionPoolHealth
    {
        /// <summary>
        /// The probe outcome.
        /// </summary>
        public SessionPoolHealthStatus Status { get; }

        /// <summary>
        /// True only when <see cref="Status"/> is <see cref="SessionPoolHealthStatus.Healthy"/>.
        /// </summary>
        public bool IsHealthy => Status == SessionPoolHealthStatus.Healthy;

        /// <summary>
        /// Idle clients in the pool at the time the snapshot was taken.
        /// </summary>
        public int AvailableClients { get; }

        /// <summary>
        /// Configured maximum capacity of the pool.
        /// </summary>
        public int TotalPoolSize { get; }

        /// <summary>
        /// Cumulative tally of reconnection failures since the pool was opened.
        /// </summary>
        public int FailedReconnections { get; }

        /// <summary>
        /// Human-readable explanation of <see cref="Status"/>.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The exception that caused an <see cref="SessionPoolHealthStatus.Unhealthy"/> result, if any.
        /// </summary>
        public Exception Error { get; }

        public SessionPoolHealth(SessionPoolHealthStatus status, int availableClients, int totalPoolSize,
            int failedReconnections, string message, Exception error = null)
        {
            Status = status;
            AvailableClients = availableClients;
            TotalPoolSize = totalPoolSize;
            FailedReconnections = failedReconnections;
            Message = message;
            Error = error;
        }

        public override string ToString()
            => $"{Status}: {Message} (available {AvailableClients}/{TotalPoolSize}, failed reconnections {FailedReconnections})";
    }
}
