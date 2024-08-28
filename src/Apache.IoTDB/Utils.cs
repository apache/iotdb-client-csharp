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
using System.Linq;

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
    }
}