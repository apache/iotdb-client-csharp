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
using System.Data.Common;


namespace Apache.IoTDB.Data
{
    /// <summary>
    ///     Represents a IoTDB error.
    /// </summary>
    public class IoTDBException : DbException
    {
        IoTDBErrorResult _IoTDBError;

        public IoTDBException(IoTDBErrorResult IoTDBError) : base(IoTDBError.Error, null)
        {
            _IoTDBError = IoTDBError;
            base.HResult = _IoTDBError.Code;
        }

        public IoTDBException(IoTDBErrorResult IoTDBError, Exception ex) : base(IoTDBError.Error, ex)
        {
            _IoTDBError = IoTDBError;
            base.HResult = _IoTDBError.Code;
        }





        public override string Message => _IoTDBError?.Error;
        public override int ErrorCode =>   (int) _IoTDBError?.Code;
        /// <summary>
        ///     Throws an exception with a specific IoTDB error code value.
        /// </summary>
        /// <param name="rc">The IoTDB error code corresponding to the desired exception.</param>
        /// <param name="db">A handle to database connection.</param>
        /// <remarks>
        ///     No exception is thrown for non-error result codes.
        /// </remarks>
        public static void ThrowExceptionForRC(string _commandText, IoTDBErrorResult IoTDBError)
        {
            var te = new IoTDBException(IoTDBError);
            te.Data.Add("commandText", _commandText);
            throw te;
        }
        public static void ThrowExceptionForRC( IoTDBErrorResult IoTDBError)
        {
            var te = new IoTDBException(IoTDBError);
            throw te;
        }
        public static void ThrowExceptionForRC(IntPtr _IoTDB)
        {
            var te = new IoTDBException(new IoTDBErrorResult() {   });
            throw te;
        }
        public static void ThrowExceptionForRC(int code, string message, Exception ex)
        {
            var te = new IoTDBException(new IoTDBErrorResult() { Code = code, Error = message }, ex);
            throw te;
        }
        public static void ThrowExceptionForRC(int code, string message)
        {
            var te = new IoTDBException(new IoTDBErrorResult() { Code = code, Error = message });
            throw te;
        }
    }
}
