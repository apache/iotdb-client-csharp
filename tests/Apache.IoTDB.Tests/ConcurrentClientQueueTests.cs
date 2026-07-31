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
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class ConcurrentClientQueueTests
    {
        private static Client NewStubClient() => new Client(null, 1L, 1L, null, null);

        [Test]
        public void TimeoutInMs_DefaultsToTenSeconds()
        {
            var queue = new ConcurrentClientQueue();

            Assert.That(queue.TimeoutInMs, Is.EqualTo(10_000), "Default pool wait timeout should be 10 seconds.");
        }

        [Test]
        public void Take_EmptyQueue_ThrowsWithinConfiguredMilliseconds()
        {
            // Regression guard: the wait timeout used to be interpreted as seconds while callers assigned
            // a millisecond value, so a 200ms budget blocked the caller for 200 seconds instead.
            var queue = new ConcurrentClientQueue { TimeoutInMs = 200 };
            var stopwatch = Stopwatch.StartNew();

            Assert.Throws<TimeoutException>(() => queue.Take());

            stopwatch.Stop();
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(5_000),
                "Take() must honour TimeoutInMs as milliseconds, not seconds.");
        }

        [Test]
        public void Take_EmptyQueue_ReportsTimeoutUnitInMessage()
        {
            var queue = new ConcurrentClientQueue { TimeoutInMs = 150 };

            var ex = Assert.Throws<TimeoutException>(() => queue.Take());

            Assert.That(ex.Message, Does.Contain("150ms"), "The depletion message should state the timeout in milliseconds.");
        }

        [Test]
        public void Take_ReturnsClientOnceOneIsHandedBack()
        {
            var queue = new ConcurrentClientQueue { TimeoutInMs = 5_000 };
            var expected = NewStubClient();

            var taker = Task.Run(() => queue.Take());
            Thread.Sleep(100); // let the taker block on the empty queue
            queue.Return(expected);

            Assert.That(taker.Wait(TimeSpan.FromSeconds(5)), Is.True, "Take() should be woken by Return().");
            Assert.That(taker.Result, Is.SameAs(expected));
        }

        [Test]
        public void Take_DequeuesWithoutWaitingWhenClientAvailable()
        {
            var queue = new ConcurrentClientQueue { TimeoutInMs = 60_000 };
            var expected = NewStubClient();
            queue.Add(expected);

            var stopwatch = Stopwatch.StartNew();
            var actual = queue.Take();
            stopwatch.Stop();

            Assert.That(actual, Is.SameAs(expected));
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(1_000), "A ready client must be returned immediately.");
        }

#pragma warning disable CS0618 // exercising the obsolete compatibility shim on purpose
        [Test]
        public void ObsoleteTimeoutProperty_ConvertsBetweenSecondsAndMilliseconds()
        {
            var queue = new ConcurrentClientQueue { Timeout = 3 };

            Assert.That(queue.TimeoutInMs, Is.EqualTo(3_000), "Setting Timeout (seconds) should scale to milliseconds.");
            Assert.That(queue.Timeout, Is.EqualTo(3), "Reading Timeout should scale back to seconds.");
        }
#pragma warning restore CS0618

        [Test]
        public void Take_RepeatedWakeUps_StillHonoursTheOverallDeadline()
        {
            // Return() pulses ALL waiters while only one can dequeue, so a losing waiter re-enters the loop
            // and waits again. If the full timeout were re-armed on each wake-up, that waiter could exceed
            // its budget indefinitely under steady churn. Here the queue is deliberately never fed: the
            // waiter is only ever pulsed, so it must still give up once the overall deadline passes.
            var queue = new ConcurrentClientQueue { TimeoutInMs = 200 };
            var stop = new ManualResetEventSlim(false);

            var pulser = Task.Run(() =>
            {
                while (!stop.IsSet)
                {
                    Monitor.Enter(queue.ClientQueue);
                    try
                    {
                        Monitor.PulseAll(queue.ClientQueue);
                    }
                    finally
                    {
                        Monitor.Exit(queue.ClientQueue);
                    }
                    Thread.Sleep(20);
                }
            });

            var stopwatch = Stopwatch.StartNew();
            Assert.Throws<TimeoutException>(() => queue.Take());
            stopwatch.Stop();
            stop.Set();
            pulser.Wait(TimeSpan.FromSeconds(5));

            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(1_000),
                $"Take() must not re-arm its budget on every wake-up (waited {stopwatch.ElapsedMilliseconds}ms for a 200ms budget).");
        }

        [Test]
        public void Take_MultipleWaiters_EachHonoursTheOverallDeadline()
        {
            var queue = new ConcurrentClientQueue { TimeoutInMs = 300 };
            const int waiterCount = 4;

            var stopwatch = Stopwatch.StartNew();
            var waiters = new Task[waiterCount];
            for (int i = 0; i < waiterCount; i++)
            {
                waiters[i] = Task.Run(() =>
                {
                    try
                    {
                        queue.Take();
                        return true;    // won the single client
                    }
                    catch (TimeoutException)
                    {
                        return false;   // timed out within budget
                    }
                });
            }

            Thread.Sleep(100);
            queue.Return(NewStubClient());  // wakes all waiters; only one can win

            Assert.That(Task.WaitAll(waiters, TimeSpan.FromSeconds(5)), Is.True,
                "Every waiter should settle within its own deadline rather than waiting indefinitely.");
            stopwatch.Stop();

            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2_000),
                $"Losing waiters must not restart their budget (took {stopwatch.ElapsedMilliseconds}ms for a 300ms budget).");
        }
    }
}
