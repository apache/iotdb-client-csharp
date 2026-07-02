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

using Apache.IoTDB.Data;
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class MtlsConfigurationTests
    {
        [Test]
        public void SessionPoolBuilder_AcceptsClientCertificateConfiguration()
        {
            var sessionPool = new SessionPool.Builder()
                .SetHost("localhost")
                .SetPort(6667)
                .SetUseSsl(true)
                .SetClientCertificatePath("/tmp/client.pfx")
                .SetClientCertificatePassword("secret")
                .SetRootCertificatePath("/tmp/root-ca.pem")
                .Build();

            Assert.That(sessionPool, Is.Not.Null);
        }

        [Test]
        public void SessionPoolBuilder_AcceptsObsoleteCertificatePath()
        {
#pragma warning disable CS0618
            var sessionPool = new SessionPool.Builder()
                .SetHost("localhost")
                .SetPort(6667)
                .SetUseSsl(true)
                .SetCertificatePath("/tmp/client.pfx")
                .SetClientCertificatePassword("secret")
                .SetRootCertificatePath("/tmp/root-ca.pem")
                .Build();
#pragma warning restore CS0618

            Assert.That(sessionPool, Is.Not.Null);
        }

        [Test]
        public void TableSessionPoolBuilder_AcceptsClientCertificateConfiguration()
        {
            var tableSessionPool = new TableSessionPool.Builder()
                .SetHost("localhost")
                .SetPort(6667)
                .SetUseSsl(true)
                .SetClientCertificatePath("/tmp/client.pfx")
                .SetClientCertificatePassword("secret")
                .SetRootCertificatePath("/tmp/root-ca.pem")
                .Build();

            Assert.That(tableSessionPool, Is.Not.Null);
        }

        [Test]
        public void TableSessionPoolBuilder_AcceptsObsoleteCertificatePath()
        {
#pragma warning disable CS0618
            var tableSessionPool = new TableSessionPool.Builder()
                .SetHost("localhost")
                .SetPort(6667)
                .SetUseSsl(true)
                .SetCertificatePath("/tmp/client.pfx")
                .SetClientCertificatePassword("secret")
                .SetRootCertificatePath("/tmp/root-ca.pem")
                .Build();
#pragma warning restore CS0618

            Assert.That(tableSessionPool, Is.Not.Null);
        }

        [Test]
        public void ConnectionStringBuilder_ParsesMtlsConfiguration()
        {
            var builder = new IoTDBConnectionStringBuilder(
                "DataSource=localhost;Port=6667;UseSsl=True;ClientCertificatePath=/tmp/client.pfx;ClientCertificatePassword=secret;RootCertificatePath=/tmp/root-ca.pem");

            Assert.That(builder.UseSsl, Is.True);
            Assert.That(builder.ClientCertificatePath, Is.EqualTo("/tmp/client.pfx"));
            Assert.That(builder.ClientCertificatePassword, Is.EqualTo("secret"));
            Assert.That(builder.RootCertificatePath, Is.EqualTo("/tmp/root-ca.pem"));
        }

        [Test]
        public void ConnectionStringBuilder_TimeOutSerializesWithTimeOutKeyword()
        {
            var builder = new IoTDBConnectionStringBuilder
            {
                TimeOut = 1234
            };

            Assert.That(builder.ConnectionString, Does.Contain("TimeOut=1234"));
            Assert.That(builder.ConnectionString, Does.Not.Contain("PoolSize=1234"));
        }
    }
}
