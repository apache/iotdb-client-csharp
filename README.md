<!--

    Licensed to the Apache Software Foundation (ASF) under one
    or more contributor license agreements.  See the NOTICE file
    distributed with this work for additional information
    regarding copyright ownership.  The ASF licenses this file
    to you under the Apache License, Version 2.0 (the
    "License"); you may not use this file except in compliance
    with the License.  You may obtain a copy of the License at
    
        http://www.apache.org/licenses/LICENSE-2.0
    
    Unless required by applicable law or agreed to in writing,
    software distributed under the License is distributed on an
    "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
    KIND, either express or implied.  See the License for the
    specific language governing permissions and limitations
    under the License.

-->
[English](./README.md) | [中文](./README_ZH.md)

# Apache IoTDB Client for C#

[![E2E Tests](https://github.com/apache/iotdb-client-csharp/actions/workflows/e2e.yml/badge.svg)](https://github.com/apache/iotdb-client-csharp/actions/workflows/e2e.yml)
[![License](https://img.shields.io/badge/license-Apache%202-4EB1BA.svg)](https://www.apache.org/licenses/LICENSE-2.0.html)
![NuGet Version](https://img.shields.io/nuget/v/Apache.IoTDB)
![NuGet Downloads](https://img.shields.io/nuget/dt/Apache.IoTDB)


## Overview

This is the C# client of Apache IoTDB.

[Apache IoTDB](https://iotdb.apache.org) (Internet of Things Database) is a data management system for time series data, which can provide users specific services, such as, data collection, storage and analysis. Due to its light weight structure, high performance and usable features together with its seamless integration with the Hadoop and Spark ecology, IoTDB meets the requirements of massive dataset storage, high throughput data input, and complex data analysis in the industrial IoT field.

Apache IoTDB website: https://iotdb.apache.org
Apache IoTDB Github: https://github.com/apache/iotdb

## Installation

### Install from NuGet Package

We have prepared a Nuget Package for C# users. Users can directly install the Apache IoTDB client using the .NET CLI. To install, simply run the following command in your command line:

```bash
dotnet add package Apache.IoTDB
```

For more details, visit the package on [NuGet](https://www.nuget.org/packages/Apache.IoTDB/).

> [!NOTE]
> The `Apache.IoTDB` package only supports versions greater than `.net framework 4.6.1`.

## Prerequisites

    .NET SDK Version >= 5.0 
    .NET Framework >= 4.6.1

## How to Use the Client (Quick Start)

Users can quickly get started by referring to the use cases under the Apache-IoTDB-Client-CSharp-UserCase directory. These use cases serve as a useful resource for getting familiar with the client's functionality and capabilities.

For those who wish to delve deeper into the client's usage and explore more advanced features, the samples directory contains additional code samples. 

## Developer environment requirements for iotdb-client-csharp

```
.NET SDK Version >= 5.0
.NET Framework >= 4.6.1
ApacheThrift >= 0.14.1
NLog >= 4.7.9
```

### OS

* Linux, MacOS or other Unix-like OS
* Windows + Bash (WSL, cygwin, Git Bash)

### Command Line Tools

* dotnet CLI
* Thrift

## Publish your own client on nuget.org
You can find out how to publish from this [doc](./PUBLISH.md).