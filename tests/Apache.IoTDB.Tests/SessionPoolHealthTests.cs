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
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class SessionPoolHealthTests
    {
        private static Client NewStubClient() => new Client(null, 1L, 1L, null, null);

        private static SessionPool NewUnopenedPool()
            => new SessionPool.Builder().SetHost("127.0.0.1").SetPort(6667).Build();

        [Test]
        public async Task CheckHealthAsync_PoolNeverOpened_ReportsNotOpen()
        {
            var health = await NewUnopenedPool().CheckHealthAsync();

            Assert.That(health.Status, Is.EqualTo(SessionPoolHealthStatus.NotOpen));
            Assert.That(health.IsHealthy, Is.False);
            Assert.That(health.Error, Is.Null);
        }

        [Test]
        public async Task CheckHealthAsync_PoolNeverOpened_ReturnsPromptlyWithoutTouchingTheNetwork()
        {
            // The probe must not attempt to connect, and must never block on the empty queue.
            var stopwatch = Stopwatch.StartNew();
            var health = await NewUnopenedPool().CheckHealthAsync();
            stopwatch.Stop();

            Assert.That(health.Status, Is.EqualTo(SessionPoolHealthStatus.NotOpen));
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2_000));
        }

        [Test]
        public void IsOpen_StaysFalseUntilOpened()
        {
            // IsOpen is a lifecycle flag; CheckHealthAsync is the connectivity probe.
            Assert.That(NewUnopenedPool().IsOpen(), Is.False);
        }

        [Test]
        public void TryTake_EmptyQueue_ReturnsFalseImmediately()
        {
            var queue = new ConcurrentClientQueue { TimeoutInMs = 30_000 };
            var stopwatch = Stopwatch.StartNew();

            var taken = queue.TryTake(out var client);

            stopwatch.Stop();
            Assert.That(taken, Is.False);
            Assert.That(client, Is.Null);
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(1_000), "TryTake must never wait on the queue.");
        }

        [Test]
        public void TryTake_ReturnsIdleClientWithoutRemovingOthers()
        {
            var queue = new ConcurrentClientQueue();
            var first = NewStubClient();
            var second = NewStubClient();
            queue.Add(first);
            queue.Add(second);

            Assert.That(queue.TryTake(out var taken), Is.True);
            Assert.That(taken, Is.SameAs(first));
            Assert.That(queue.ClientQueue.Count, Is.EqualTo(1));
        }

        [Test]
        public void SessionPoolHealth_ToStringIncludesStatusAndCounters()
        {
            var health = new SessionPoolHealth(SessionPoolHealthStatus.Unhealthy, 3, 8, 2, "probe failed", new Exception("boom"));

            Assert.That(health.IsHealthy, Is.False);
            Assert.That(health.ToString(), Does.Contain("Unhealthy"));
            Assert.That(health.ToString(), Does.Contain("3/8"));
            Assert.That(health.ToString(), Does.Contain("2"));
        }

        [Test]
        public void SessionPoolHealth_HealthyStatusSetsIsHealthy()
        {
            var health = new SessionPoolHealth(SessionPoolHealthStatus.Healthy, 8, 8, 0, "ok");

            Assert.That(health.IsHealthy, Is.True);
            Assert.That(health.Error, Is.Null);
        }
    }
}
