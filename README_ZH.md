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

# Apache IoTDB C#语言客户端
[![E2E Tests](https://github.com/apache/iotdb-client-csharp/actions/workflows/e2e.yml/badge.svg)](https://github.com/apache/iotdb-client-csharp/actions/workflows/e2e.yml)
[![License](https://img.shields.io/badge/license-Apache%202-4EB1BA.svg)](https://www.apache.org/licenses/LICENSE-2.0.html)
![NuGet Version](https://img.shields.io/nuget/v/Apache.IoTDB)
![NuGet Downloads](https://img.shields.io/nuget/dt/Apache.IoTDB)

## 概览

本仓库是Apache IoTDB的C#语言客户端,与其他语言支持相同语义的用户接口。

Apache IoTDB website: https://iotdb.apache.org

Apache IoTDB Github: https://github.com/apache/iotdb

## 如何安装
### 从NuGet Package安装

我们为CSharp用户准备了NuGet包，用户可直接通过.NET CLI进行客户端安装，[NuGet包链接如下](https://www.nuget.org/packages/Apache.IoTDB/),命令行中运行如下命令即可完成安装

我们为 C# 用户准备了一个 Nuget 包。用户可以直接通过 .NET CLI 进行客户端安装。命令行中运行如下命令即可完成安装
    
```sh
dotnet add package Apache.IoTDB
```

详情请访问 [NuGet 上的包](https://www.nuget.org/packages/Apache.IoTDB/)。


> [!NOTE]
> 请注意，`Apache.IoTDB`这个包仅支持大于`.net framework 4.6.1`的版本。

## 环境准备

    .NET SDK Version >= 5.0
    .NET Framework >= 4.6.1 

## 如何使用 (快速上手)
用户可以通过参考Apache-IoTDB-Client-CSharp-UserCase目录下的用例快速入门。这些用例提供了客户端的基本功能和用法的参考。

对于希望深入了解客户端用法并探索更高级特性的用户，samples目录包含了额外的代码示例。

## TLS 和 mTLS

通过 `SetUseSsl(true)` 开启 TLS。C# 客户端使用 .NET 的证书模型，不直接读取 Java truststore；如果证书按 JDK 17 的 Java/keytool 文档生成，`client.keystore` 默认就是 PKCS#12，可以直接作为客户端证书文件使用，并直接使用 `ca.crt` 作为信任根。

| keytool 产物 | C# 客户端用法 |
| --- | --- |
| `ca.crt` | 传给 `SetRootCertificatePath` / `RootCertificatePath`，用于信任服务端证书 |
| `client.keystore` | 包含客户端私钥和客户端证书；JDK 17 默认是 PKCS#12，直接传给 `SetClientCertificatePath` |
| `client.truststore` | Java 客户端的 truststore；C# 侧用 `ca.crt`，不需要这个文件 |
| `server.truststore` | 服务端用于信任客户端证书，不是 C# 客户端参数 |

配置 `RootCertificatePath` 后，`Host` / `DataSource` 必须匹配服务端证书 SAN。如果使用 IP 地址连接，服务端证书需要包含对应的 IP SAN。

只有在复用旧版 JDK 生成的 JKS 文件，或显式使用 `-storetype JKS` 生成 keystore 时，才需要先转换为 PKCS#12：

```bash
$KT -importkeystore \
  -srckeystore client.keystore \
  -srcstorepass $PWD \
  -srcalias client \
  -destkeystore client.p12 \
  -deststoretype PKCS12 \
  -deststorepass $PWD \
  -destkeypass $PWD \
  -destalias client
```

C# builder 示例：

```csharp
var sessionPool = new SessionPool.Builder()
    .SetHost("127.0.0.1")
    .SetPort(6667)
    .SetUseSsl(true)
    .SetRootCertificatePath("tls-certs/ca.crt")
    .SetClientCertificatePath("tls-certs/client.keystore")
    .SetClientCertificatePassword("IoTDB")
    .Build();
```

ADO.NET 连接字符串也支持相同配置：

```text
DataSource=127.0.0.1;Port=6667;UseSsl=True;RootCertificatePath=tls-certs/ca.crt;ClientCertificatePath=tls-certs/client.keystore;ClientCertificatePassword=IoTDB
```

## iotdb-client-csharp的开发者环境要求

```
.NET SDK Version >= 5.0
.NET Framework >= 4.6.1
ApacheThrift >= 0.14.1
NLog >= 4.7.9
```

### 操作系统

* Linux、MacOS 或其他类 Unix 系统
* Windows + Bash (WSL、cygwin、Git Bash)

### 命令行工具

* dotnet CLI
* Thrift

## 代码格式化

本项目使用 `dotnet format` 基于 [.editorconfig](.editorconfig) 规则来强制执行一致的代码风格。

### 本地检查格式

```bash
dotnet format --verify-no-changes
```

### 自动修复格式问题

```bash
dotnet format
```

CI 流水线会在所有 Pull Request 上自动检查代码格式。请确保在提交 PR 之前代码格式正确。

## 在 nuget.org 上发布你自己的客户端
你可以在这个[文档](./PUBLISH.md)中找到如何发布
