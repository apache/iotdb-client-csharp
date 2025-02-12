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


public partial class TPipeSubscribeReq : TBase
{
  private byte[] _body;

  public sbyte Version { get; set; }

  public short Type { get; set; }

  public byte[] Body
  {
    get
    {
      return _body;
    }
    set
    {
      __isset.body = true;
      this._body = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool body;
  }

  public TPipeSubscribeReq()
  {
  }

  public TPipeSubscribeReq(sbyte version, short type) : this()
  {
    this.Version = version;
    this.Type = type;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_version = false;
      bool isset_type = false;
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
      var struc = new TStruct("TPipeSubscribeReq");
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
      if((Body != null) && __isset.body)
      {
        field.Name = "body";
        field.Type = TType.String;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBinaryAsync(Body, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
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
    if (!(that is TPipeSubscribeReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Version, other.Version)
      && System.Object.Equals(Type, other.Type)
      && ((__isset.body == other.__isset.body) && ((!__isset.body) || (TCollections.Equals(Body, other.Body))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + Version.GetHashCode();
      hashcode = (hashcode * 397) + Type.GetHashCode();
      if((Body != null) && __isset.body)
      {
        hashcode = (hashcode * 397) + Body.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TPipeSubscribeReq(");
    sb.Append(", Version: ");
    Version.ToString(sb);
    sb.Append(", Type: ");
    Type.ToString(sb);
    if((Body != null) && __isset.body)
    {
      sb.Append(", Body: ");
      Body.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

