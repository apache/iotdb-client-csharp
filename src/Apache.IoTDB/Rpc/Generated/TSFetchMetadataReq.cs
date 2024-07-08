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


public partial class TSFetchMetadataReq : TBase
{
  private string _columnPath;

  public long SessionId { get; set; }

  public string Type { get; set; }

  public string ColumnPath
  {
    get
    {
      return _columnPath;
    }
    set
    {
      __isset.columnPath = true;
      this._columnPath = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool columnPath;
  }

  public TSFetchMetadataReq()
  {
  }

  public TSFetchMetadataReq(long sessionId, string type) : this()
  {
    this.SessionId = sessionId;
    this.Type = type;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
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
            if (field.Type == TType.I64)
            {
              SessionId = await iprot.ReadI64Async(cancellationToken);
              isset_sessionId = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              Type = await iprot.ReadStringAsync(cancellationToken);
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
              ColumnPath = await iprot.ReadStringAsync(cancellationToken);
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
      if (!isset_sessionId)
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
      var struc = new TStruct("TSFetchMetadataReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Type != null))
      {
        field.Name = "type";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Type, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((ColumnPath != null) && __isset.columnPath)
      {
        field.Name = "columnPath";
        field.Type = TType.String;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(ColumnPath, cancellationToken);
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
    if (!(that is TSFetchMetadataReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && System.Object.Equals(Type, other.Type)
      && ((__isset.columnPath == other.__isset.columnPath) && ((!__isset.columnPath) || (System.Object.Equals(ColumnPath, other.ColumnPath))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      if((Type != null))
      {
        hashcode = (hashcode * 397) + Type.GetHashCode();
      }
      if((ColumnPath != null) && __isset.columnPath)
      {
        hashcode = (hashcode * 397) + ColumnPath.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSFetchMetadataReq(");
    sb.Append(", SessionId: ");
    SessionId.ToString(sb);
    if((Type != null))
    {
      sb.Append(", Type: ");
      Type.ToString(sb);
    }
    if((ColumnPath != null) && __isset.columnPath)
    {
      sb.Append(", ColumnPath: ");
      ColumnPath.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

