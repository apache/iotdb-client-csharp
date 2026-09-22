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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Apache.IoTDB
{
    public class ConcurrentClientQueue
    {
        public ConcurrentQueue<Client> ClientQueue { get; }
        internal IPoolDiagnosticReporter DiagnosticReporter { get; set; }

        public ConcurrentClientQueue(List<Client> clients)
        {
            ClientQueue = new ConcurrentQueue<Client>(clients);
        }
        public ConcurrentClientQueue()
        {
            ClientQueue = new ConcurrentQueue<Client>();
        }
        public void Add(Client client) => Return(client);

        public void Return(Client client)
        {
            Monitor.Enter(ClientQueue);
            try
            {
                ClientQueue.Enqueue(client);
                Monitor.PulseAll(ClientQueue); // wake up all threads waiting on the queue, refresh the waiting time
            }
            finally
            {
                Monitor.Exit(ClientQueue);
            }
            Thread.Sleep(0);
        }
        private int _ref = 0;
        public void AddRef() => Interlocked.Increment(ref _ref);
        public int GetRef() => Volatile.Read(ref _ref);
        public void RemoveRef() => Interlocked.Decrement(ref _ref);
        /// <summary>
        /// The maximum time, in milliseconds, that <see cref="Take"/> waits for a client to be
        /// returned to the pool before throwing. Defaults to 10000 (10 seconds).
        /// </summary>
        public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

        internal const int DefaultTimeoutInMs = 10_000;

        /// <summary>
        /// The wait timeout expressed in seconds. Kept for backward compatibility only; it is a thin
        /// wrapper over <see cref="TimeoutInMs"/>. Prefer <see cref="TimeoutInMs"/>, which avoids the
        /// unit ambiguity that previously caused millisecond values to be interpreted as seconds.
        /// </summary>
        [Obsolete("Use TimeoutInMs instead. This property interprets its value as seconds.")]
        public int Timeout
        {
            get => TimeoutInMs / 1000;
            set => TimeoutInMs = value * 1000;
        }

        /// <summary>
        /// Attempts to take a client without ever blocking. Returns false when no client is idle.
        /// Use this for probes and diagnostics, which must not queue behind ordinary work.
        /// </summary>
        public bool TryTake(out Client client) => ClientQueue.TryDequeue(out client);

        public Client Take()
        {
            Client client = null;
            // One overall deadline for the whole call. Return() uses PulseAll, so every waiter wakes up
            // while only one of them can dequeue the returned client; re-arming the full timeout on each
            // wake-up would let an unlucky waiter exceed the configured bound indefinitely under churn.
            var budgetMs = TimeoutInMs;
            var elapsed = Stopwatch.StartNew();
            Monitor.Enter(ClientQueue);
            try
            {
                while (true)
                {
                    if (ClientQueue.TryDequeue(out client))
                    {
                        break;
                    }

                    var remainingMs = budgetMs - (int)elapsed.ElapsedMilliseconds;
                    if (remainingMs <= 0)
                    {
                        break;
                    }

                    Monitor.Wait(ClientQueue, TimeSpan.FromMilliseconds(remainingMs));
                }
            }
            finally
            {
                Monitor.Exit(ClientQueue);
            }
            if (client == null)
            {
                var reasonPhrase = $"Connection pool is empty and wait time out({budgetMs}ms)";
                if (DiagnosticReporter != null)
                {
                    throw DiagnosticReporter.BuildDepletionException(reasonPhrase);
                }
                throw new TimeoutException(reasonPhrase);
            }
            return client;
        }
    }

    internal interface IPoolDiagnosticReporter
    {
        SessionPoolDepletedException BuildDepletionException(string reasonPhrase);
    }
}
