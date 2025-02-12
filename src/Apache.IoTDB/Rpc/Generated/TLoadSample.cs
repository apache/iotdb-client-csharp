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


public partial class TLoadSample : TBase
{

  public double CpuUsageRate { get; set; }

  public double MemoryUsageRate { get; set; }

  public double DiskUsageRate { get; set; }

  public double FreeDiskSpace { get; set; }

  public TLoadSample()
  {
  }

  public TLoadSample(double cpuUsageRate, double memoryUsageRate, double diskUsageRate, double freeDiskSpace) : this()
  {
    this.CpuUsageRate = cpuUsageRate;
    this.MemoryUsageRate = memoryUsageRate;
    this.DiskUsageRate = diskUsageRate;
    this.FreeDiskSpace = freeDiskSpace;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_cpuUsageRate = false;
      bool isset_memoryUsageRate = false;
      bool isset_diskUsageRate = false;
      bool isset_freeDiskSpace = false;
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
            if (field.Type == TType.Double)
            {
              CpuUsageRate = await iprot.ReadDoubleAsync(cancellationToken);
              isset_cpuUsageRate = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.Double)
            {
              MemoryUsageRate = await iprot.ReadDoubleAsync(cancellationToken);
              isset_memoryUsageRate = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Double)
            {
              DiskUsageRate = await iprot.ReadDoubleAsync(cancellationToken);
              isset_diskUsageRate = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.Double)
            {
              FreeDiskSpace = await iprot.ReadDoubleAsync(cancellationToken);
              isset_freeDiskSpace = true;
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
      if (!isset_cpuUsageRate)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_memoryUsageRate)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_diskUsageRate)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_freeDiskSpace)
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
      var struc = new TStruct("TLoadSample");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "cpuUsageRate";
      field.Type = TType.Double;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteDoubleAsync(CpuUsageRate, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "memoryUsageRate";
      field.Type = TType.Double;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteDoubleAsync(MemoryUsageRate, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "diskUsageRate";
      field.Type = TType.Double;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteDoubleAsync(DiskUsageRate, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "freeDiskSpace";
      field.Type = TType.Double;
      field.ID = 4;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteDoubleAsync(FreeDiskSpace, cancellationToken);
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
    if (!(that is TLoadSample other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(CpuUsageRate, other.CpuUsageRate)
      && System.Object.Equals(MemoryUsageRate, other.MemoryUsageRate)
      && System.Object.Equals(DiskUsageRate, other.DiskUsageRate)
      && System.Object.Equals(FreeDiskSpace, other.FreeDiskSpace);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + CpuUsageRate.GetHashCode();
      hashcode = (hashcode * 397) + MemoryUsageRate.GetHashCode();
      hashcode = (hashcode * 397) + DiskUsageRate.GetHashCode();
      hashcode = (hashcode * 397) + FreeDiskSpace.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TLoadSample(");
    sb.Append(", CpuUsageRate: ");
    CpuUsageRate.ToString(sb);
    sb.Append(", MemoryUsageRate: ");
    MemoryUsageRate.ToString(sb);
    sb.Append(", DiskUsageRate: ");
    DiskUsageRate.ToString(sb);
    sb.Append(", FreeDiskSpace: ");
    FreeDiskSpace.ToString(sb);
    sb.Append(')');
    return sb.ToString();
  }
}

