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
using NUnit.Framework;

namespace Apache.IoTDB.Tests
{
    [TestFixture]
    public class UtilsTests
    {
        private Utils _utils;

        [SetUp]
        public void Setup()
        {
            _utils = new Utils();
        }

        [TestFixture]
        public class ParseTEndPointIpv4AndIpv6UrlTests : UtilsTests
        {
            [Test]
            public void ParseTEndPointIpv4AndIpv6Url_ValidIPv4Address_ReturnsCorrectEndpoint()
            {
                // Arrange
                string correctEndpointIPv4 = "192.168.1.1:8080";

                // Act
                var endpoint = _utils.ParseTEndPointIpv4AndIpv6Url(correctEndpointIPv4);

                // Assert
                Assert.That(endpoint.Ip, Is.EqualTo("192.168.1.1"), "IPv4 address mismatch.");
                Assert.That(endpoint.Port, Is.EqualTo(8080), "IPv4 port mismatch.");
            }

            [Test]
            public void ParseTEndPointIpv4AndIpv6Url_ValidIPv6Address_ReturnsCorrectEndpoint()
            {
                // Arrange
                string correctEndpointIPv6 = "[2001:db8:85a3::8a2e:370:7334]:443";

                // Act
                var endpoint = _utils.ParseTEndPointIpv4AndIpv6Url(correctEndpointIPv6);

                // Assert
                Assert.That(endpoint.Ip, Is.EqualTo("2001:db8:85a3::8a2e:370:7334"), "IPv6 address mismatch.");
                Assert.That(endpoint.Port, Is.EqualTo(443), "IPv6 port mismatch.");
            }

            [Test]
            public void ParseTEndPointIpv4AndIpv6Url_NoPort_ReturnsEmptyEndpoint()
            {
                // Arrange
                string noPort = "192.168.1.1";

                // Act
                var endpoint = _utils.ParseTEndPointIpv4AndIpv6Url(noPort);

                // Assert
                Assert.That(string.IsNullOrEmpty(endpoint.Ip) && endpoint.Port == 0,
                           Is.True, "Failed to handle missing port.");
            }

            [Test]
            public void ParseTEndPointIpv4AndIpv6Url_EmptyInput_ReturnsEmptyEndpoint()
            {
                // Arrange
                string emptyInput = "";

                // Act
                var endpoint = _utils.ParseTEndPointIpv4AndIpv6Url(emptyInput);

                // Assert
                Assert.That(string.IsNullOrEmpty(endpoint.Ip) && endpoint.Port == 0,
                           Is.True, "Failed to handle empty input.");
            }

            [Test]
            public void ParseTEndPointIpv4AndIpv6Url_InvalidPortFormat_ThrowsFormatException()
            {
                // Arrange
                string invalidFormat = "192.168.1.1:port";

                // Act & Assert
                Assert.Throws<FormatException>(() => _utils.ParseTEndPointIpv4AndIpv6Url(invalidFormat),
                                              "Should have thrown an exception due to invalid port.");
            }
        }

        [TestFixture]
        public class ParseSeedNodeUrlsTests : UtilsTests
        {
            [Test]
            public void ParseSeedNodeUrls_ValidUrls_ReturnsCorrectEndpoints()
            {
                // Arrange
                var urls = new List<string> { "192.168.1.1:8080", "[2001:db8::1]:443" };

                // Act
                var endpoints = _utils.ParseSeedNodeUrls(urls);

                // Assert
                Assert.That(endpoints, Has.Count.EqualTo(2));
                Assert.That(endpoints[0].Ip, Is.EqualTo("192.168.1.1"));
                Assert.That(endpoints[0].Port, Is.EqualTo(8080));
                Assert.That(endpoints[1].Ip, Is.EqualTo("2001:db8::1"));
                Assert.That(endpoints[1].Port, Is.EqualTo(443));
            }

            [Test]
            public void ParseSeedNodeUrls_NullInput_ThrowsArgumentException()
            {
                // Act & Assert
                Assert.Throws<ArgumentException>(() => _utils.ParseSeedNodeUrls(null),
                                                "Should throw ArgumentException for null input.");
            }

            [Test]
            public void ParseSeedNodeUrls_EmptyList_ThrowsArgumentException()
            {
                // Arrange
                var emptyList = new List<string>();

                // Act & Assert
                Assert.Throws<ArgumentException>(() => _utils.ParseSeedNodeUrls(emptyList),
                                                "Should throw ArgumentException for empty list.");
            }
        }

        [TestFixture]
        public class IsSortedTests : UtilsTests
        {
            [Test]
            public void IsSorted_SortedList_ReturnsTrue()
            {
                // Arrange
                var sortedList = new List<long> { 1, 2, 3, 4, 5 };

                // Act
                var result = _utils.IsSorted(sortedList);

                // Assert
                Assert.That(result, Is.True, "Should return true for sorted list.");
            }

            [Test]
            public void IsSorted_UnsortedList_ReturnsFalse()
            {
                // Arrange
                var unsortedList = new List<long> { 1, 3, 2, 4, 5 };

                // Act
                var result = _utils.IsSorted(unsortedList);

                // Assert
                Assert.That(result, Is.False, "Should return false for unsorted list.");
            }

            [Test]
            public void IsSorted_EmptyList_ReturnsTrue()
            {
                // Arrange
                var emptyList = new List<long>();

                // Act
                var result = _utils.IsSorted(emptyList);

                // Assert
                Assert.That(result, Is.True, "Should return true for empty list.");
            }

            [Test]
            public void IsSorted_SingleElementList_ReturnsTrue()
            {
                // Arrange
                var singleElementList = new List<long> { 42 };

                // Act
                var result = _utils.IsSorted(singleElementList);

                // Assert
                Assert.That(result, Is.True, "Should return true for single element list.");
            }
        }

        [TestFixture]
        public class DateUtilsTests : UtilsTests
        {
            [Test]
            public void ParseIntToDate_ValidDate_ReturnsCorrectDateTime()
            {
                // Arrange
                int dateInt = 20231225;

                // Act
                var result = Utils.ParseIntToDate(dateInt);

                // Assert
                Assert.That(result.Year, Is.EqualTo(2023));
                Assert.That(result.Month, Is.EqualTo(12));
                Assert.That(result.Day, Is.EqualTo(25));
            }

            [Test]
            public void ParseIntToDate_InvalidDate_ThrowsArgumentException()
            {
                // Arrange
                int invalidDate = 123;

                // Act & Assert
                Assert.Throws<ArgumentException>(() => Utils.ParseIntToDate(invalidDate),
                                                "Should throw ArgumentException for invalid date.");
            }

            [Test]
            public void ParseDateToInt_ValidDateTime_ReturnsCorrectInt()
            {
                // Arrange
                var dateTime = new DateTime(2023, 12, 25);

                // Act
                var result = Utils.ParseDateToInt(dateTime);

                // Assert
                Assert.That(result, Is.EqualTo(20231225));
            }

            [Test]
            public void ParseDateToInt_NullDateTime_ThrowsArgumentException()
            {
                // Act & Assert
                Assert.Throws<ArgumentException>(() => Utils.ParseDateToInt(null),
                                                "Should throw ArgumentException for null DateTime.");
            }

            [Test]
            public void ParseDateToInt_YearLessThan1000_ThrowsArgumentException()
            {
                // Arrange
                var dateTime = new DateTime(999, 12, 25);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => Utils.ParseDateToInt(dateTime),
                                                "Should throw ArgumentException for year less than 1000.");
            }
        }
    }
}
