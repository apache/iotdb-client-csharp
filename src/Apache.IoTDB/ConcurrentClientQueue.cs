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
            ClientQueue.Enqueue(client);
            Monitor.PulseAll(ClientQueue); // wake up all threads waiting on the queue, refresh the waiting time
            Monitor.Exit(ClientQueue);
            Thread.Sleep(0);
        }
        int _ref = 0;
        public void AddRef()
        {
            lock (this)
            {
                _ref++;
            }
        }
        public int GetRef()
        {
            return _ref;
        }
        public void RemoveRef()
        {
            lock (this)
            {
                _ref--;
            }
        }
        public int Timeout { get; set; } = 10;
        public Client Take()
        {
            Client client = null;
            Monitor.Enter(ClientQueue);
            while(true){
                bool timeout = false;
                if (ClientQueue.IsEmpty)
                {
                    timeout = !Monitor.Wait(ClientQueue, TimeSpan.FromSeconds(Timeout));
                }
                ClientQueue.TryDequeue(out client);

                if(client != null || timeout){
                    break;
                }
            }
            Monitor.Exit(ClientQueue);
            if (client == null)
            {
                throw new TimeoutException($"Connection pool is empty and wait time out({Timeout}s)!");
            }
            return client;
        }
    }
}