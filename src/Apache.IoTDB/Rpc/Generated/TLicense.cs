/**
 * Autogenerated by Thrift Compiler (0.14.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE1006  // parts of the code use IDL spelling


public partial class TLicense : TBase
{

  public long LicenseIssueTimestamp { get; set; }

  public long ExpireTimestamp { get; set; }

  public short DataNodeNumLimit { get; set; }

  public int CpuCoreNumLimit { get; set; }

  public long DeviceNumLimit { get; set; }

  public long SensorNumLimit { get; set; }

  public long DisconnectionFromActiveNodeTimeLimit { get; set; }

  public short MlNodeNumLimit { get; set; }

  public TLicense()
  {
  }

  public TLicense(long licenseIssueTimestamp, long expireTimestamp, short dataNodeNumLimit, int cpuCoreNumLimit, long deviceNumLimit, long sensorNumLimit, long disconnectionFromActiveNodeTimeLimit, short mlNodeNumLimit) : this()
  {
    this.LicenseIssueTimestamp = licenseIssueTimestamp;
    this.ExpireTimestamp = expireTimestamp;
    this.DataNodeNumLimit = dataNodeNumLimit;
    this.CpuCoreNumLimit = cpuCoreNumLimit;
    this.DeviceNumLimit = deviceNumLimit;
    this.SensorNumLimit = sensorNumLimit;
    this.DisconnectionFromActiveNodeTimeLimit = disconnectionFromActiveNodeTimeLimit;
    this.MlNodeNumLimit = mlNodeNumLimit;
  }

  public TLicense DeepCopy()
  {
    var tmp88 = new TLicense();
    tmp88.LicenseIssueTimestamp = this.LicenseIssueTimestamp;
    tmp88.ExpireTimestamp = this.ExpireTimestamp;
    tmp88.DataNodeNumLimit = this.DataNodeNumLimit;
    tmp88.CpuCoreNumLimit = this.CpuCoreNumLimit;
    tmp88.DeviceNumLimit = this.DeviceNumLimit;
    tmp88.SensorNumLimit = this.SensorNumLimit;
    tmp88.DisconnectionFromActiveNodeTimeLimit = this.DisconnectionFromActiveNodeTimeLimit;
    tmp88.MlNodeNumLimit = this.MlNodeNumLimit;
    return tmp88;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_licenseIssueTimestamp = false;
      bool isset_expireTimestamp = false;
      bool isset_dataNodeNumLimit = false;
      bool isset_cpuCoreNumLimit = false;
      bool isset_deviceNumLimit = false;
      bool isset_sensorNumLimit = false;
      bool isset_disconnectionFromActiveNodeTimeLimit = false;
      bool isset_mlNodeNumLimit = false;
      TField field;
      await iprot.ReadStructBeginAsync(cancellationToken);
      while (true)
      {
        field = await iprot.ReadFieldBeginAsync(cancellationToken);
        if (field.Type == TType.Stop)
        {
          break;
        }

        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I64)
            {
              LicenseIssueTimestamp = await iprot.ReadI64Async(cancellationToken);
              isset_licenseIssueTimestamp = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.I64)
            {
              ExpireTimestamp = await iprot.ReadI64Async(cancellationToken);
              isset_expireTimestamp = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.I16)
            {
              DataNodeNumLimit = await iprot.ReadI16Async(cancellationToken);
              isset_dataNodeNumLimit = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I32)
            {
              CpuCoreNumLimit = await iprot.ReadI32Async(cancellationToken);
              isset_cpuCoreNumLimit = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.I64)
            {
              DeviceNumLimit = await iprot.ReadI64Async(cancellationToken);
              isset_deviceNumLimit = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.I64)
            {
              SensorNumLimit = await iprot.ReadI64Async(cancellationToken);
              isset_sensorNumLimit = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
            if (field.Type == TType.I64)
            {
              DisconnectionFromActiveNodeTimeLimit = await iprot.ReadI64Async(cancellationToken);
              isset_disconnectionFromActiveNodeTimeLimit = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 9:
            if (field.Type == TType.I16)
            {
              MlNodeNumLimit = await iprot.ReadI16Async(cancellationToken);
              isset_mlNodeNumLimit = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          default: 
            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            break;
        }

        await iprot.ReadFieldEndAsync(cancellationToken);
      }

      await iprot.ReadStructEndAsync(cancellationToken);
      if (!isset_licenseIssueTimestamp)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_expireTimestamp)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_dataNodeNumLimit)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_cpuCoreNumLimit)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_deviceNumLimit)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_sensorNumLimit)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_disconnectionFromActiveNodeTimeLimit)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_mlNodeNumLimit)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
  {
    oprot.IncrementRecursionDepth();
    try
    {
      var struc = new TStruct("TLicense");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "licenseIssueTimestamp";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(LicenseIssueTimestamp, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "expireTimestamp";
      field.Type = TType.I64;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(ExpireTimestamp, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "dataNodeNumLimit";
      field.Type = TType.I16;
      field.ID = 4;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI16Async(DataNodeNumLimit, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "cpuCoreNumLimit";
      field.Type = TType.I32;
      field.ID = 5;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(CpuCoreNumLimit, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "deviceNumLimit";
      field.Type = TType.I64;
      field.ID = 6;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(DeviceNumLimit, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "sensorNumLimit";
      field.Type = TType.I64;
      field.ID = 7;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SensorNumLimit, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "disconnectionFromActiveNodeTimeLimit";
      field.Type = TType.I64;
      field.ID = 8;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(DisconnectionFromActiveNodeTimeLimit, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "mlNodeNumLimit";
      field.Type = TType.I16;
      field.ID = 9;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI16Async(MlNodeNumLimit, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      await oprot.WriteFieldStopAsync(cancellationToken);
      await oprot.WriteStructEndAsync(cancellationToken);
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override bool Equals(object that)
  {
    if (!(that is TLicense other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(LicenseIssueTimestamp, other.LicenseIssueTimestamp)
      && System.Object.Equals(ExpireTimestamp, other.ExpireTimestamp)
      && System.Object.Equals(DataNodeNumLimit, other.DataNodeNumLimit)
      && System.Object.Equals(CpuCoreNumLimit, other.CpuCoreNumLimit)
      && System.Object.Equals(DeviceNumLimit, other.DeviceNumLimit)
      && System.Object.Equals(SensorNumLimit, other.SensorNumLimit)
      && System.Object.Equals(DisconnectionFromActiveNodeTimeLimit, other.DisconnectionFromActiveNodeTimeLimit)
      && System.Object.Equals(MlNodeNumLimit, other.MlNodeNumLimit);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + LicenseIssueTimestamp.GetHashCode();
      hashcode = (hashcode * 397) + ExpireTimestamp.GetHashCode();
      hashcode = (hashcode * 397) + DataNodeNumLimit.GetHashCode();
      hashcode = (hashcode * 397) + CpuCoreNumLimit.GetHashCode();
      hashcode = (hashcode * 397) + DeviceNumLimit.GetHashCode();
      hashcode = (hashcode * 397) + SensorNumLimit.GetHashCode();
      hashcode = (hashcode * 397) + DisconnectionFromActiveNodeTimeLimit.GetHashCode();
      hashcode = (hashcode * 397) + MlNodeNumLimit.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TLicense(");
    sb.Append(", LicenseIssueTimestamp: ");
    LicenseIssueTimestamp.ToString(sb);
    sb.Append(", ExpireTimestamp: ");
    ExpireTimestamp.ToString(sb);
    sb.Append(", DataNodeNumLimit: ");
    DataNodeNumLimit.ToString(sb);
    sb.Append(", CpuCoreNumLimit: ");
    CpuCoreNumLimit.ToString(sb);
    sb.Append(", DeviceNumLimit: ");
    DeviceNumLimit.ToString(sb);
    sb.Append(", SensorNumLimit: ");
    SensorNumLimit.ToString(sb);
    sb.Append(", DisconnectionFromActiveNodeTimeLimit: ");
    DisconnectionFromActiveNodeTimeLimit.ToString(sb);
    sb.Append(", MlNodeNumLimit: ");
    MlNodeNumLimit.ToString(sb);
    sb.Append(')');
    return sb.ToString();
  }
}

