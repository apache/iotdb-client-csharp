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

﻿#if NET461_OR_GREATER || NETSTANDARD2_0
//https://github.com/Grax32/ArrayExtensions/blob/master/docs/index.md

using System;
namespace Grax32.Extensions
{
    public static class ArrayExtensions
    {
        public static void Fill<T>(this T[] destinationArray, T value)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException(nameof(destinationArray));
            }

            destinationArray[0] = value;
            FillInternal(destinationArray, 1);
        }

        public static void Fill<T>(this T[] destinationArray, T[] values)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException(nameof(destinationArray));
            }

            var copyLength = values.Length;
            var destinationArrayLength = destinationArray.Length;

            if (copyLength == 0)
            {
                throw new ArgumentException("Parameter must contain at least one value.", nameof(values));
            }

            if (copyLength > destinationArrayLength)
            {
                // value to copy is longer than destination,
                // so fill destination with first part of value
                Array.Copy(values, destinationArray, destinationArrayLength);
                return;
            }

            Array.Copy(values, destinationArray, copyLength);

            FillInternal(destinationArray, copyLength);
        }

        private static void FillInternal<T>(this T[] destinationArray, int copyLength)
        {
            var destinationArrayLength = destinationArray.Length;
            var destinationArrayHalfLength = destinationArrayLength / 2;

            // looping copy from beginning of array to current position
            // doubling copy length with each pass
            for (; copyLength < destinationArrayHalfLength; copyLength *= 2)
            {
                Array.Copy(
                    sourceArray: destinationArray,
                    sourceIndex: 0,
                    destinationArray: destinationArray,
                    destinationIndex: copyLength,
                    length: copyLength);
            }

            // we're past halfway, meaning only a single copy remains
            // exactly fill remainder of array
            Array.Copy(
                sourceArray: destinationArray,
                sourceIndex: 0,
                destinationArray: destinationArray,
                destinationIndex: copyLength,
                length: destinationArrayLength - copyLength);
        }
    }
}
#endif