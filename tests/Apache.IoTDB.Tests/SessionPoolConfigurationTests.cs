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
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class SessionPoolConfigurationTests
    {
        private static int ReadPoolWaitTimeout(SessionPool pool)
        {
            var field = typeof(SessionPool).GetField("_poolWaitTimeoutInMs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(field, Is.Not.Null, "_poolWaitTimeoutInMs field is expected to exist on SessionPool.");
            return (int)field.GetValue(pool);
        }

        [Test]
        public void Builder_WithoutExplicitPoolWaitTimeout_UsesDefault()
        {
            var pool = new SessionPool.Builder().SetHost("127.0.0.1").SetPort(6667).Build();

            Assert.That(ReadPoolWaitTimeout(pool), Is.EqualTo(SessionPool.DefaultPoolWaitTimeoutInMs));
        }

        [Test]
        public void Builder_PoolWaitTimeoutIsIndependentOfConnectionTimeout()
        {
            // The pool wait timeout used to be derived from the connection timeout (_timeout * 5) and then
            // read back as seconds, which turned a 500ms connection timeout into a ~41 minute pool wait.
            var pool = new SessionPool.Builder()
                .SetHost("127.0.0.1")
                .SetPort(6667)
                .SetConnectionTimeoutInMs(500)
                .SetPoolWaitTimeoutInMs(3_000)
                .Build();

            Assert.That(ReadPoolWaitTimeout(pool), Is.EqualTo(3_000));
        }

        [Test]
        public void Builder_WithNodeUrls_PropagatesPoolWaitTimeout()
        {
            var pool = new SessionPool.Builder()
                .SetNodeUrl(new List<string> { "127.0.0.1:6667", "127.0.0.1:6668" })
                .SetPoolWaitTimeoutInMs(7_500)
                .Build();

            Assert.That(ReadPoolWaitTimeout(pool), Is.EqualTo(7_500));
        }

        [Test]
        public void TableSessionPoolBuilder_PropagatesPoolWaitTimeout()
        {
            var tablePool = new TableSessionPool.Builder()
                .SetHost("127.0.0.1")
                .SetPort(6667)
                .SetPoolWaitTimeoutInMs(4_200)
                .Build();

            var innerField = typeof(TableSessionPool).GetField("sessionPool", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(innerField, Is.Not.Null, "TableSessionPool is expected to wrap a SessionPool.");

            var inner = (SessionPool)innerField.GetValue(tablePool);
            Assert.That(ReadPoolWaitTimeout(inner), Is.EqualTo(4_200));
        }

        [Test]
        public void NewPool_StartsWithNoUnrealizedCapacityAndIsNotOpen()
        {
            var pool = new SessionPool.Builder().SetHost("127.0.0.1").SetPort(6667).Build();

            Assert.That(pool.UnrealizedCapacity, Is.Zero);
            Assert.That(pool.IsOpen(), Is.False);
        }

        private static void SetPrivateField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(field, Is.Not.Null, $"{name} field is expected to exist.");
            field.SetValue(target, value);
        }

        [Test]
        public async Task Close_EmptyClientQueue_StillMarksThePoolClosed()
        {
            // Regression guard: _isClose used to be assigned only inside the foreach over queued clients.
            // Once every connection had been discarded the queue was empty, the loop ran zero times,
            // and Close() returned while IsOpen() stayed true - leaving the rebuild path armed.
            var pool = new SessionPool.Builder().SetHost("127.0.0.1").SetPort(6667).Build();
            SetPrivateField(pool, "_clients", new ConcurrentClientQueue());
            SetPrivateField(pool, "_isClose", false);
            SetPrivateField(pool, "_unrealizedCapacity", 8);

            Assert.That(pool.IsOpen(), Is.True, "Precondition: the pool looks open with an empty queue.");

            await pool.Close();

            Assert.That(pool.IsOpen(), Is.False, "Close() must flip the lifecycle flag regardless of queue contents.");
            Assert.That(pool.UnrealizedCapacity, Is.Zero, "Close() must disarm capacity refill.");
        }

        [Test]
        public async Task Close_IsIdempotentWhenQueueIsEmpty()
        {
            var pool = new SessionPool.Builder().SetHost("127.0.0.1").SetPort(6667).Build();
            SetPrivateField(pool, "_clients", new ConcurrentClientQueue());
            SetPrivateField(pool, "_isClose", false);

            await pool.Close();
            await pool.Close();

            Assert.That(pool.IsOpen(), Is.False);
        }
    }
}
