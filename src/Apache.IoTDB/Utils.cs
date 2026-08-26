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
using System.Globalization;
using System.Linq;
using System.Text;

namespace Apache.IoTDB
{
    public class Utils
    {
        const string PointColon = ":";
        const string AbbColon = "[";
        public bool IsSorted(IList<long> collection)
        {
            for (var i = 1; i < collection.Count; i++)
            {
                if (collection[i] < collection[i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        public int VerifySuccess(TSStatus status)
        {
            if (status.Code == (int)TSStatusCode.MULTIPLE_ERROR)
            {
                if (status.SubStatus.Any(subStatus => VerifySuccess(subStatus) != 0))
                {
                    return -1;
                }
                return 0;
            }
            if (status.Code == (int)TSStatusCode.REDIRECTION_RECOMMEND)
            {
                return 0;
            }
            if (status.Code == (int)TSStatusCode.SUCCESS_STATUS)
            {
                return 0;
            }
            return -1;
        }
        /// <summary>
        /// Parse TEndPoint from a given TEndPointUrl
        /// example:[D80:0000:0000:0000:ABAA:0000:00C2:0002]:22227
        /// </summary>
        /// <param name="endPointUrl">ip:port</param>
        /// <returns>TEndPoint null if parse error</returns>
        public TEndPoint ParseTEndPointIpv4AndIpv6Url(string endPointUrl)
        {
            TEndPoint endPoint = new();

            if (endPointUrl.Contains(PointColon))
            {
                int pointPosition = endPointUrl.LastIndexOf(PointColon);
                string port = endPointUrl[(pointPosition + 1)..];
                string ip = endPointUrl[..pointPosition];
                if (ip.Contains(AbbColon))
                {
                    ip = ip[1..^1]; // Remove the square brackets from IPv6
                }
                endPoint.Ip = ip;
                endPoint.Port = int.Parse(port);
            }

            return endPoint;
        }
        public List<TEndPoint> ParseSeedNodeUrls(List<string> nodeUrls)
        {
            if (nodeUrls == null || nodeUrls.Count == 0)
            {
                throw new ArgumentException("No seed node URLs provided.");
            }
            return nodeUrls.Select(ParseTEndPointIpv4AndIpv6Url).ToList();
        }

        public static DateTime ParseIntToDate(int dateInt)
        {
            if (dateInt < 10000101 || dateInt > 99991231)
            {
                throw new ArgumentException("Date must be between 10000101 and 99991231.");
            }
            return DateTime.TryParseExact(dateInt.ToString(), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date) ? date : throw new ArgumentException("Date must be between 10000101 and 99991231.");
        }

        public static int ParseDateToInt(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                throw new ArgumentException("Date expression is none or empty.");
            }
            if (dateTime.Value.Year < 1000)
            {
                throw new ArgumentException("Year must be between 1000 and 9999.");
            }
            return dateTime.Value.Year * 10000 + dateTime.Value.Month * 100 + dateTime.Value.Day;
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            return "0x" + BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// Formats the wire representation of a stored OBJECT value for display.
        /// The server stores OBJECT cells as an 8-byte big-endian file size
        /// followed by the internal object path; this renders the size in
        /// human-readable units (mirrors the Go client's objectBytesToString).
        /// </summary>
        public static string ObjectBytesToString(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length < 8)
                throw new ArgumentException(
                    $"Invalid OBJECT value: expected at least 8 bytes, got {input.Length}.",
                    nameof(input));

            ulong size = 0;
            for (int i = 0; i < 8; i++)
            {
                size = (size << 8) | input[i];
            }

            const ulong kilobyte = 1024;
            const ulong megabyte = kilobyte * 1024;
            const ulong gigabyte = megabyte * 1024;

            if (size < kilobyte)
                return $"(Object) {size} B";
            if (size < megabyte)
                return string.Format(CultureInfo.InvariantCulture, "(Object) {0:F2} KB", (double)size / kilobyte);
            if (size < gigabyte)
                return string.Format(CultureInfo.InvariantCulture, "(Object) {0:F2} MB", (double)size / megabyte);
            return string.Format(CultureInfo.InvariantCulture, "(Object) {0:F2} GB", (double)size / gigabyte);
        }
    }
}
