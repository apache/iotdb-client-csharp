/**
 * Autogenerated by Thrift Compiler (0.14.2)
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


public partial class TSLastDataQueryReq : TBase
{
  private int _fetchSize;
  private bool _enableRedirectQuery;
  private bool _jdbcQuery;
  private long _timeout;

  public long SessionId { get; set; }

  public List<string> Paths { get; set; }

  public int FetchSize
  {
    get
    {
      return _fetchSize;
    }
    set
    {
      __isset.fetchSize = true;
      this._fetchSize = value;
    }
  }

  public long Time { get; set; }

  public long StatementId { get; set; }

  public bool EnableRedirectQuery
  {
    get
    {
      return _enableRedirectQuery;
    }
    set
    {
      __isset.enableRedirectQuery = true;
      this._enableRedirectQuery = value;
    }
  }

  public bool JdbcQuery
  {
    get
    {
      return _jdbcQuery;
    }
    set
    {
      __isset.jdbcQuery = true;
      this._jdbcQuery = value;
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
    public bool fetchSize;
    public bool enableRedirectQuery;
    public bool jdbcQuery;
    public bool timeout;
  }

  public TSLastDataQueryReq()
  {
  }

  public TSLastDataQueryReq(long sessionId, List<string> paths, long time, long statementId) : this()
  {
    this.SessionId = sessionId;
    this.Paths = paths;
    this.Time = time;
    this.StatementId = statementId;
  }

  public TSLastDataQueryReq DeepCopy()
  {
    var tmp324 = new TSLastDataQueryReq();
    tmp324.SessionId = this.SessionId;
    if((Paths != null))
    {
      tmp324.Paths = this.Paths.DeepCopy();
    }
    if(__isset.fetchSize)
    {
      tmp324.FetchSize = this.FetchSize;
    }
    tmp324.__isset.fetchSize = this.__isset.fetchSize;
    tmp324.Time = this.Time;
    tmp324.StatementId = this.StatementId;
    if(__isset.enableRedirectQuery)
    {
      tmp324.EnableRedirectQuery = this.EnableRedirectQuery;
    }
    tmp324.__isset.enableRedirectQuery = this.__isset.enableRedirectQuery;
    if(__isset.jdbcQuery)
    {
      tmp324.JdbcQuery = this.JdbcQuery;
    }
    tmp324.__isset.jdbcQuery = this.__isset.jdbcQuery;
    if(__isset.timeout)
    {
      tmp324.Timeout = this.Timeout;
    }
    tmp324.__isset.timeout = this.__isset.timeout;
    return tmp324;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_paths = false;
      bool isset_time = false;
      bool isset_statementId = false;
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
            if (field.Type == TType.List)
            {
              {
                TList _list325 = await iprot.ReadListBeginAsync(cancellationToken);
                Paths = new List<string>(_list325.Count);
                for(int _i326 = 0; _i326 < _list325.Count; ++_i326)
                {
                  string _elem327;
                  _elem327 = await iprot.ReadStringAsync(cancellationToken);
                  Paths.Add(_elem327);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_paths = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.I32)
            {
              FetchSize = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.I64)
            {
              Time = await iprot.ReadI64Async(cancellationToken);
              isset_time = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I64)
            {
              StatementId = await iprot.ReadI64Async(cancellationToken);
              isset_statementId = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.Bool)
            {
              EnableRedirectQuery = await iprot.ReadBoolAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.Bool)
            {
              JdbcQuery = await iprot.ReadBoolAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
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
      if (!isset_paths)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_time)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_statementId)
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
      var struc = new TStruct("TSLastDataQueryReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Paths != null))
      {
        field.Name = "paths";
        field.Type = TType.List;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, Paths.Count), cancellationToken);
          foreach (string _iter328 in Paths)
          {
            await oprot.WriteStringAsync(_iter328, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.fetchSize)
      {
        field.Name = "fetchSize";
        field.Type = TType.I32;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(FetchSize, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      field.Name = "time";
      field.Type = TType.I64;
      field.ID = 4;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(Time, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "statementId";
      field.Type = TType.I64;
      field.ID = 5;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(StatementId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if(__isset.enableRedirectQuery)
      {
        field.Name = "enableRedirectQuery";
        field.Type = TType.Bool;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(EnableRedirectQuery, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.jdbcQuery)
      {
        field.Name = "jdbcQuery";
        field.Type = TType.Bool;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(JdbcQuery, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.timeout)
      {
        field.Name = "timeout";
        field.Type = TType.I64;
        field.ID = 8;
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
    if (!(that is TSLastDataQueryReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && TCollections.Equals(Paths, other.Paths)
      && ((__isset.fetchSize == other.__isset.fetchSize) && ((!__isset.fetchSize) || (System.Object.Equals(FetchSize, other.FetchSize))))
      && System.Object.Equals(Time, other.Time)
      && System.Object.Equals(StatementId, other.StatementId)
      && ((__isset.enableRedirectQuery == other.__isset.enableRedirectQuery) && ((!__isset.enableRedirectQuery) || (System.Object.Equals(EnableRedirectQuery, other.EnableRedirectQuery))))
      && ((__isset.jdbcQuery == other.__isset.jdbcQuery) && ((!__isset.jdbcQuery) || (System.Object.Equals(JdbcQuery, other.JdbcQuery))))
      && ((__isset.timeout == other.__isset.timeout) && ((!__isset.timeout) || (System.Object.Equals(Timeout, other.Timeout))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      if((Paths != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Paths);
      }
      if(__isset.fetchSize)
      {
        hashcode = (hashcode * 397) + FetchSize.GetHashCode();
      }
      hashcode = (hashcode * 397) + Time.GetHashCode();
      hashcode = (hashcode * 397) + StatementId.GetHashCode();
      if(__isset.enableRedirectQuery)
      {
        hashcode = (hashcode * 397) + EnableRedirectQuery.GetHashCode();
      }
      if(__isset.jdbcQuery)
      {
        hashcode = (hashcode * 397) + JdbcQuery.GetHashCode();
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
    var sb = new StringBuilder("TSLastDataQueryReq(");
    sb.Append(", SessionId: ");
    SessionId.ToString(sb);
    if((Paths != null))
    {
      sb.Append(", Paths: ");
      Paths.ToString(sb);
    }
    if(__isset.fetchSize)
    {
      sb.Append(", FetchSize: ");
      FetchSize.ToString(sb);
    }
    sb.Append(", Time: ");
    Time.ToString(sb);
    sb.Append(", StatementId: ");
    StatementId.ToString(sb);
    if(__isset.enableRedirectQuery)
    {
      sb.Append(", EnableRedirectQuery: ");
      EnableRedirectQuery.ToString(sb);
    }
    if(__isset.jdbcQuery)
    {
      sb.Append(", JdbcQuery: ");
      JdbcQuery.ToString(sb);
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
