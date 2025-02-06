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


public partial class TSRawDataQueryReq : TBase
{
  private int _fetchSize;
  private bool _enableRedirectQuery;
  private bool _jdbcQuery;
  private long _timeout;
  private bool _legalPathNodes;

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

  public long StartTime { get; set; }

  public long EndTime { get; set; }

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

  public bool LegalPathNodes
  {
    get
    {
      return _legalPathNodes;
    }
    set
    {
      __isset.legalPathNodes = true;
      this._legalPathNodes = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool fetchSize;
    public bool enableRedirectQuery;
    public bool jdbcQuery;
    public bool timeout;
    public bool legalPathNodes;
  }

  public TSRawDataQueryReq()
  {
  }

  public TSRawDataQueryReq(long sessionId, List<string> paths, long startTime, long endTime, long statementId) : this()
  {
    this.SessionId = sessionId;
    this.Paths = paths;
    this.StartTime = startTime;
    this.EndTime = endTime;
    this.StatementId = statementId;
  }

  public TSRawDataQueryReq DeepCopy()
  {
    var tmp318 = new TSRawDataQueryReq();
    tmp318.SessionId = this.SessionId;
    if((Paths != null))
    {
      tmp318.Paths = this.Paths.DeepCopy();
    }
    if(__isset.fetchSize)
    {
      tmp318.FetchSize = this.FetchSize;
    }
    tmp318.__isset.fetchSize = this.__isset.fetchSize;
    tmp318.StartTime = this.StartTime;
    tmp318.EndTime = this.EndTime;
    tmp318.StatementId = this.StatementId;
    if(__isset.enableRedirectQuery)
    {
      tmp318.EnableRedirectQuery = this.EnableRedirectQuery;
    }
    tmp318.__isset.enableRedirectQuery = this.__isset.enableRedirectQuery;
    if(__isset.jdbcQuery)
    {
      tmp318.JdbcQuery = this.JdbcQuery;
    }
    tmp318.__isset.jdbcQuery = this.__isset.jdbcQuery;
    if(__isset.timeout)
    {
      tmp318.Timeout = this.Timeout;
    }
    tmp318.__isset.timeout = this.__isset.timeout;
    if(__isset.legalPathNodes)
    {
      tmp318.LegalPathNodes = this.LegalPathNodes;
    }
    tmp318.__isset.legalPathNodes = this.__isset.legalPathNodes;
    return tmp318;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_paths = false;
      bool isset_startTime = false;
      bool isset_endTime = false;
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
                TList _list319 = await iprot.ReadListBeginAsync(cancellationToken);
                Paths = new List<string>(_list319.Count);
                for(int _i320 = 0; _i320 < _list319.Count; ++_i320)
                {
                  string _elem321;
                  _elem321 = await iprot.ReadStringAsync(cancellationToken);
                  Paths.Add(_elem321);
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
              StartTime = await iprot.ReadI64Async(cancellationToken);
              isset_startTime = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I64)
            {
              EndTime = await iprot.ReadI64Async(cancellationToken);
              isset_endTime = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
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
          case 7:
            if (field.Type == TType.Bool)
            {
              EnableRedirectQuery = await iprot.ReadBoolAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
            if (field.Type == TType.Bool)
            {
              JdbcQuery = await iprot.ReadBoolAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 9:
            if (field.Type == TType.I64)
            {
              Timeout = await iprot.ReadI64Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 10:
            if (field.Type == TType.Bool)
            {
              LegalPathNodes = await iprot.ReadBoolAsync(cancellationToken);
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
      if (!isset_startTime)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_endTime)
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
      var struc = new TStruct("TSRawDataQueryReq");
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
          foreach (string _iter322 in Paths)
          {
            await oprot.WriteStringAsync(_iter322, cancellationToken);
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
      field.Name = "startTime";
      field.Type = TType.I64;
      field.ID = 4;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(StartTime, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "endTime";
      field.Type = TType.I64;
      field.ID = 5;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(EndTime, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "statementId";
      field.Type = TType.I64;
      field.ID = 6;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(StatementId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if(__isset.enableRedirectQuery)
      {
        field.Name = "enableRedirectQuery";
        field.Type = TType.Bool;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(EnableRedirectQuery, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.jdbcQuery)
      {
        field.Name = "jdbcQuery";
        field.Type = TType.Bool;
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(JdbcQuery, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.timeout)
      {
        field.Name = "timeout";
        field.Type = TType.I64;
        field.ID = 9;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(Timeout, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.legalPathNodes)
      {
        field.Name = "legalPathNodes";
        field.Type = TType.Bool;
        field.ID = 10;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(LegalPathNodes, cancellationToken);
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
    if (!(that is TSRawDataQueryReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && TCollections.Equals(Paths, other.Paths)
      && ((__isset.fetchSize == other.__isset.fetchSize) && ((!__isset.fetchSize) || (System.Object.Equals(FetchSize, other.FetchSize))))
      && System.Object.Equals(StartTime, other.StartTime)
      && System.Object.Equals(EndTime, other.EndTime)
      && System.Object.Equals(StatementId, other.StatementId)
      && ((__isset.enableRedirectQuery == other.__isset.enableRedirectQuery) && ((!__isset.enableRedirectQuery) || (System.Object.Equals(EnableRedirectQuery, other.EnableRedirectQuery))))
      && ((__isset.jdbcQuery == other.__isset.jdbcQuery) && ((!__isset.jdbcQuery) || (System.Object.Equals(JdbcQuery, other.JdbcQuery))))
      && ((__isset.timeout == other.__isset.timeout) && ((!__isset.timeout) || (System.Object.Equals(Timeout, other.Timeout))))
      && ((__isset.legalPathNodes == other.__isset.legalPathNodes) && ((!__isset.legalPathNodes) || (System.Object.Equals(LegalPathNodes, other.LegalPathNodes))));
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
      hashcode = (hashcode * 397) + StartTime.GetHashCode();
      hashcode = (hashcode * 397) + EndTime.GetHashCode();
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
      if(__isset.legalPathNodes)
      {
        hashcode = (hashcode * 397) + LegalPathNodes.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSRawDataQueryReq(");
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
    sb.Append(", StartTime: ");
    StartTime.ToString(sb);
    sb.Append(", EndTime: ");
    EndTime.ToString(sb);
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
    if(__isset.legalPathNodes)
    {
      sb.Append(", LegalPathNodes: ");
      LegalPathNodes.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

