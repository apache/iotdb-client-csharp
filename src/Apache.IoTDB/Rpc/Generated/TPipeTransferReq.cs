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



public partial class TPipeTransferReq : TBase
{

  public sbyte Version { get; set; }

  public short Type { get; set; }

  public byte[] Body { get; set; }

  public TPipeTransferReq()
  {
  }

  public TPipeTransferReq(sbyte version, short type, byte[] body) : this()
  {
    this.Version = version;
    this.Type = type;
    this.Body = body;
  }

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_version = false;
      bool isset_type = false;
      bool isset_body = false;
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
            if (field.Type == TType.Byte)
            {
              Version = await iprot.ReadByteAsync(cancellationToken);
              isset_version = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.I16)
            {
              Type = await iprot.ReadI16Async(cancellationToken);
              isset_type = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.String)
            {
              Body = await iprot.ReadBinaryAsync(cancellationToken);
              isset_body = true;
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
      if (!isset_version)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_type)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_body)
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
      var struc = new TStruct("TPipeTransferReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "version";
      field.Type = TType.Byte;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteByteAsync(Version, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "type";
      field.Type = TType.I16;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI16Async(Type, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "body";
      field.Type = TType.String;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteBinaryAsync(Body, cancellationToken);
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
    var other = that as TPipeTransferReq;
    if (other == null) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Version, other.Version)
      && System.Object.Equals(Type, other.Type)
      && TCollections.Equals(Body, other.Body);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + Version.GetHashCode();
      hashcode = (hashcode * 397) + Type.GetHashCode();
      hashcode = (hashcode * 397) + Body.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TPipeTransferReq(");
    sb.Append(", Version: ");
    sb.Append(Version);
    sb.Append(", Type: ");
    sb.Append(Type);
    sb.Append(", Body: ");
    sb.Append(Body);
    sb.Append(")");
    return sb.ToString();
  }
}
