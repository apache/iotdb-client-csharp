/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;



public partial class TSyncTransportMetaInfo : TBase
{

  public string FileName { get; set; }

  public long StartIndex { get; set; }

  public TSyncTransportMetaInfo()
  {
  }

  public TSyncTransportMetaInfo(string fileName, long startIndex) : this()
  {
    this.FileName = fileName;
    this.StartIndex = startIndex;
  }

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_fileName = false;
      bool isset_startIndex = false;
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
            if (field.Type == TType.String)
            {
              FileName = await iprot.ReadStringAsync(cancellationToken);
              isset_fileName = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.I64)
            {
              StartIndex = await iprot.ReadI64Async(cancellationToken);
              isset_startIndex = true;
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
      if (!isset_fileName)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_startIndex)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
  {
    oprot.IncrementRecursionDepth();
    try
    {
      var struc = new TStruct("TSyncTransportMetaInfo");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "fileName";
      field.Type = TType.String;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteStringAsync(FileName, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "startIndex";
      field.Type = TType.I64;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(StartIndex, cancellationToken);
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
    var other = that as TSyncTransportMetaInfo;
    if (other == null) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(FileName, other.FileName)
      && System.Object.Equals(StartIndex, other.StartIndex);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + FileName.GetHashCode();
      hashcode = (hashcode * 397) + StartIndex.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSyncTransportMetaInfo(");
    sb.Append(", FileName: ");
    sb.Append(FileName);
    sb.Append(", StartIndex: ");
    sb.Append(StartIndex);
    sb.Append(")");
    return sb.ToString();
  }
}

