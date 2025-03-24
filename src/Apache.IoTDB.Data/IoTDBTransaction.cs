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
using System.Data;
using System.Data.Common;

namespace Apache.IoTDB.Data
{
    /// <summary>
    ///     Represents a transaction made against a IoTDB database.
    /// </summary>
    public class IoTDBTransaction : DbTransaction
    {
        private IoTDBConnection _connection;
        private readonly IsolationLevel _isolationLevel;
        private bool _completed;
        private bool _externalRollback;

        internal IoTDBTransaction(IoTDBConnection connection, IsolationLevel isolationLevel)
        {
            _connection = connection;
            _isolationLevel = isolationLevel;
        }

        /// <summary>
        ///     Gets the connection associated with the transaction.
        /// </summary>
        /// <value>The connection associated with the transaction.</value>
        public new virtual IoTDBConnection Connection
            => _connection;

        /// <summary>
        ///     Gets the connection associated with the transaction.
        /// </summary>
        /// <value>The connection associated with the transaction.</value>
        protected override DbConnection DbConnection
            => Connection;

        internal bool ExternalRollback
            => _externalRollback;

        /// <summary>
        ///     Gets the isolation level for the transaction. This cannot be changed if the transaction is completed or
        ///     closed.
        /// </summary>
        /// <value>The isolation level for the transaction.</value>
        public override IsolationLevel IsolationLevel => IsolationLevel.Unspecified;


        /// <summary>
        ///     Applies the changes made in the transaction.
        /// </summary>
        public override void Commit()
        {
            //if (_externalRollback || _completed || _connection.State != ConnectionState.Open)
            //{
            //    throw new InvalidOperationException(Resources.TransactionCompleted);
            //}

            //_connection.ExecuteNonQuery("COMMIT;");
            Complete();
        }

        /// <summary>
        ///     Reverts the changes made in the transaction.
        /// </summary>
        public override void Rollback()
        {
            //if (_completed || _connection.State != ConnectionState.Open)
            //{
            //    throw new InvalidOperationException(Resources.TransactionCompleted);
            //}

            RollbackInternal();
        }

        /// <summary>
        ///     Releases any resources used by the transaction and rolls it back.
        /// </summary>
        /// <param name="disposing">
        ///     true to release managed and unmanaged resources; false to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing
                && !_completed
                && _connection.State == ConnectionState.Open)
            {
                RollbackInternal();
            }
        }

        private void Complete()
        {
            if (_connection != null) _connection.Transaction = null;
            _connection = null;
            _completed = true;
        }

        private void RollbackInternal()
        {
            Complete();
        }

        private void RollbackExternal(object userData)
        {
            _externalRollback = true;
        }
    }
}
