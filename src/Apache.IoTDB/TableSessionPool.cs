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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Apache.IoTDB.DataStructure;
using Microsoft.Extensions.Logging;

namespace Apache.IoTDB;

public partial class TableSessionPool
{
  private SessionPool sessionPool;

  TableSessionPool(SessionPool sessionPool)
  {
    this.sessionPool = sessionPool;
  }

  public async Task Open(bool enableRpcCompression, CancellationToken cancellationToken = default)
  {
    await sessionPool.Open(enableRpcCompression, cancellationToken);
  }

  public async Task Open(CancellationToken cancellationToken = default)
  {
    await sessionPool.Open(cancellationToken);
  }

  public async Task<int> InsertAsync(Tablet tablet)
  {
    return await sessionPool.InsertRelationalTabletAsync(tablet);
  }

  public async Task<int> ExecuteNonQueryStatementAsync(string sql)
  {
    return await sessionPool.ExecuteNonQueryStatementAsync(sql);
  }

  public async Task<SessionDataSet> ExecuteQueryStatementAsync(string sql)
  {
    return await sessionPool.ExecuteQueryStatementAsync(sql);
  }

  public async Task<SessionDataSet> ExecuteQueryStatementAsync(string sql, long timeoutInMs)
  {
    return await sessionPool.ExecuteQueryStatementAsync(sql, timeoutInMs);
  }

  public void OpenDebugMode(Action<ILoggingBuilder> configure)
  {
    sessionPool.OpenDebugMode(configure);
  }

  public async Task Close()
  {
    await sessionPool.Close();
  }
}
