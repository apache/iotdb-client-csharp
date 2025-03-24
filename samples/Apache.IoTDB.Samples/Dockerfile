# Licensed to the Apache Software Foundation (ASF) under one
# or more contributor license agreements.  See the NOTICE file
# distributed with this work for additional information
# regarding copyright ownership.  The ASF licenses this file
# to you under the Apache License, Version 2.0 (the
# "License"); you may not use this file except in compliance
# with the License.  You may obtain a copy of the License at
#
#   http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing,
# software distributed under the License is distributed on an
# "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
# KIND, either express or implied.  See the License for the
# specific language governing permissions and limitations
# under the License.

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["samples/Apache.IoTDB.Samples/Apache.IoTDB.Samples.csproj", "samples/Apache.IoTDB.Samples/"]
COPY ["src/Apache.IoTDB/Apache.IoTDB.csproj", "src/Apache.IoTDB/"]
COPY ["src/Apache.IoTDB.Data/Apache.IoTDB.Data.csproj", "src/Apache.IoTDB.Data/"]
RUN dotnet restore "samples/Apache.IoTDB.Samples/Apache.IoTDB.Samples.csproj"
COPY . .
WORKDIR "/src/samples/Apache.IoTDB.Samples"
RUN dotnet build "Apache.IoTDB.Samples.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apache.IoTDB.Samples.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apache.IoTDB.Samples.dll"]
