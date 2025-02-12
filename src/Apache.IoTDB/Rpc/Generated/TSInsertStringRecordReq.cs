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


public partial class TSInsertStringRecordReq : TBase
{
  private bool _isAligned;
  private long _timeout;

  public long SessionId { get; set; }

  public string PrefixPath { get; set; }

  public List<string> Measurements { get; set; }

  public List<string> Values { get; set; }

  public long Timestamp { get; set; }

  public bool IsAligned
  {
    get
    {
      return _isAligned;
    }
    set
    {
      __isset.isAligned = true;
      this._isAligned = value;
    }
  }

  public long Timeout
  {
    get
    {
      return _timeout;
    }
    set
    {
      __isset.timeout = true;
      this._timeout = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool isAligned;
    public bool timeout;
  }

  public TSInsertStringRecordReq()
  {
  }

  public TSInsertStringRecordReq(long sessionId, string prefixPath, List<string> measurements, List<string> values, long timestamp) : this()
  {
    this.SessionId = sessionId;
    this.PrefixPath = prefixPath;
    this.Measurements = measurements;
    this.Values = values;
    this.Timestamp = timestamp;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_prefixPath = false;
      bool isset_measurements = false;
      bool isset_values = false;
      bool isset_timestamp = false;
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
              PrefixPath = await iprot.ReadStringAsync(cancellationToken);
              isset_prefixPath = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.List)
            {
              {
                TList _list102 = await iprot.ReadListBeginAsync(cancellationToken);
                Measurements = new List<string>(_list102.Count);
                for(int _i103 = 0; _i103 < _list102.Count; ++_i103)
                {
                  string _elem104;
                  _elem104 = await iprot.ReadStringAsync(cancellationToken);
                  Measurements.Add(_elem104);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_measurements = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.List)
            {
              {
                TList _list105 = await iprot.ReadListBeginAsync(cancellationToken);
                Values = new List<string>(_list105.Count);
                for(int _i106 = 0; _i106 < _list105.Count; ++_i106)
                {
                  string _elem107;
                  _elem107 = await iprot.ReadStringAsync(cancellationToken);
                  Values.Add(_elem107);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_values = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I64)
            {
              Timestamp = await iprot.ReadI64Async(cancellationToken);
              isset_timestamp = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.Bool)
            {
              IsAligned = await iprot.ReadBoolAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.I64)
            {
              Timeout = await iprot.ReadI64Async(cancellationToken);
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
      if (!isset_prefixPath)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_measurements)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_values)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_timestamp)
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
      var struc = new TStruct("TSInsertStringRecordReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((PrefixPath != null))
      {
        field.Name = "prefixPath";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(PrefixPath, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Measurements != null))
      {
        field.Name = "measurements";
        field.Type = TType.List;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, Measurements.Count), cancellationToken);
          foreach (string _iter108 in Measurements)
          {
            await oprot.WriteStringAsync(_iter108, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Values != null))
      {
        field.Name = "values";
        field.Type = TType.List;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, Values.Count), cancellationToken);
          foreach (string _iter109 in Values)
          {
            await oprot.WriteStringAsync(_iter109, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      field.Name = "timestamp";
      field.Type = TType.I64;
      field.ID = 5;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(Timestamp, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if(__isset.isAligned)
      {
        field.Name = "isAligned";
        field.Type = TType.Bool;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(IsAligned, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.timeout)
      {
        field.Name = "timeout";
        field.Type = TType.I64;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(Timeout, cancellationToken);
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
    if (!(that is TSInsertStringRecordReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && System.Object.Equals(PrefixPath, other.PrefixPath)
      && TCollections.Equals(Measurements, other.Measurements)
      && TCollections.Equals(Values, other.Values)
      && System.Object.Equals(Timestamp, other.Timestamp)
      && ((__isset.isAligned == other.__isset.isAligned) && ((!__isset.isAligned) || (System.Object.Equals(IsAligned, other.IsAligned))))
      && ((__isset.timeout == other.__isset.timeout) && ((!__isset.timeout) || (System.Object.Equals(Timeout, other.Timeout))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      if((PrefixPath != null))
      {
        hashcode = (hashcode * 397) + PrefixPath.GetHashCode();
      }
      if((Measurements != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Measurements);
      }
      if((Values != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Values);
      }
      hashcode = (hashcode * 397) + Timestamp.GetHashCode();
      if(__isset.isAligned)
      {
        hashcode = (hashcode * 397) + IsAligned.GetHashCode();
      }
      if(__isset.timeout)
      {
        hashcode = (hashcode * 397) + Timeout.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSInsertStringRecordReq(");
    sb.Append(", SessionId: ");
    SessionId.ToString(sb);
    if((PrefixPath != null))
    {
      sb.Append(", PrefixPath: ");
      PrefixPath.ToString(sb);
    }
    if((Measurements != null))
    {
      sb.Append(", Measurements: ");
      Measurements.ToString(sb);
    }
    if((Values != null))
    {
      sb.Append(", Values: ");
      Values.ToString(sb);
    }
    sb.Append(", Timestamp: ");
    Timestamp.ToString(sb);
    if(__isset.isAligned)
    {
      sb.Append(", IsAligned: ");
      IsAligned.ToString(sb);
    }
    if(__isset.timeout)
    {
      sb.Append(", Timeout: ");
      Timeout.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

