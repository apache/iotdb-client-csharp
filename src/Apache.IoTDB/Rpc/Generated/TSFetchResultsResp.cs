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


public partial class TSFetchResultsResp : TBase
{
  private TSQueryDataSet _queryDataSet;
  private TSQueryNonAlignDataSet _nonAlignQueryDataSet;
  private List<byte[]> _queryResult;
  private bool _moreData;

  public TSStatus Status { get; set; }

  public bool HasResultSet { get; set; }

  public bool IsAlign { get; set; }

  public TSQueryDataSet QueryDataSet
  {
    get
    {
      return _queryDataSet;
    }
    set
    {
      __isset.queryDataSet = true;
      this._queryDataSet = value;
    }
  }

  public TSQueryNonAlignDataSet NonAlignQueryDataSet
  {
    get
    {
      return _nonAlignQueryDataSet;
    }
    set
    {
      __isset.nonAlignQueryDataSet = true;
      this._nonAlignQueryDataSet = value;
    }
  }

  public List<byte[]> QueryResult
  {
    get
    {
      return _queryResult;
    }
    set
    {
      __isset.queryResult = true;
      this._queryResult = value;
    }
  }

  public bool MoreData
  {
    get
    {
      return _moreData;
    }
    set
    {
      __isset.moreData = true;
      this._moreData = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool queryDataSet;
    public bool nonAlignQueryDataSet;
    public bool queryResult;
    public bool moreData;
  }

  public TSFetchResultsResp()
  {
  }

  public TSFetchResultsResp(TSStatus status, bool hasResultSet, bool isAlign) : this()
  {
    this.Status = status;
    this.HasResultSet = hasResultSet;
    this.IsAlign = isAlign;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_status = false;
      bool isset_hasResultSet = false;
      bool isset_isAlign = false;
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
            if (field.Type == TType.Struct)
            {
              Status = new TSStatus();
              await Status.ReadAsync(iprot, cancellationToken);
              isset_status = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.Bool)
            {
              HasResultSet = await iprot.ReadBoolAsync(cancellationToken);
              isset_hasResultSet = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Bool)
            {
              IsAlign = await iprot.ReadBoolAsync(cancellationToken);
              isset_isAlign = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.Struct)
            {
              QueryDataSet = new TSQueryDataSet();
              await QueryDataSet.ReadAsync(iprot, cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.Struct)
            {
              NonAlignQueryDataSet = new TSQueryNonAlignDataSet();
              await NonAlignQueryDataSet.ReadAsync(iprot, cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.List)
            {
              {
                TList _list76 = await iprot.ReadListBeginAsync(cancellationToken);
                QueryResult = new List<byte[]>(_list76.Count);
                for(int _i77 = 0; _i77 < _list76.Count; ++_i77)
                {
                  byte[] _elem78;
                  _elem78 = await iprot.ReadBinaryAsync(cancellationToken);
                  QueryResult.Add(_elem78);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.Bool)
            {
              MoreData = await iprot.ReadBoolAsync(cancellationToken);
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
      if (!isset_status)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_hasResultSet)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_isAlign)
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
      var struc = new TStruct("TSFetchResultsResp");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((Status != null))
      {
        field.Name = "status";
        field.Type = TType.Struct;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await Status.WriteAsync(oprot, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      field.Name = "hasResultSet";
      field.Type = TType.Bool;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteBoolAsync(HasResultSet, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "isAlign";
      field.Type = TType.Bool;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteBoolAsync(IsAlign, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((QueryDataSet != null) && __isset.queryDataSet)
      {
        field.Name = "queryDataSet";
        field.Type = TType.Struct;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await QueryDataSet.WriteAsync(oprot, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
      {
        field.Name = "nonAlignQueryDataSet";
        field.Type = TType.Struct;
        field.ID = 5;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await NonAlignQueryDataSet.WriteAsync(oprot, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((QueryResult != null) && __isset.queryResult)
      {
        field.Name = "queryResult";
        field.Type = TType.List;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, QueryResult.Count), cancellationToken);
          foreach (byte[] _iter79 in QueryResult)
          {
            await oprot.WriteBinaryAsync(_iter79, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.moreData)
      {
        field.Name = "moreData";
        field.Type = TType.Bool;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(MoreData, cancellationToken);
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
    if (!(that is TSFetchResultsResp other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Status, other.Status)
      && System.Object.Equals(HasResultSet, other.HasResultSet)
      && System.Object.Equals(IsAlign, other.IsAlign)
      && ((__isset.queryDataSet == other.__isset.queryDataSet) && ((!__isset.queryDataSet) || (System.Object.Equals(QueryDataSet, other.QueryDataSet))))
      && ((__isset.nonAlignQueryDataSet == other.__isset.nonAlignQueryDataSet) && ((!__isset.nonAlignQueryDataSet) || (System.Object.Equals(NonAlignQueryDataSet, other.NonAlignQueryDataSet))))
      && ((__isset.queryResult == other.__isset.queryResult) && ((!__isset.queryResult) || (TCollections.Equals(QueryResult, other.QueryResult))))
      && ((__isset.moreData == other.__isset.moreData) && ((!__isset.moreData) || (System.Object.Equals(MoreData, other.MoreData))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((Status != null))
      {
        hashcode = (hashcode * 397) + Status.GetHashCode();
      }
      hashcode = (hashcode * 397) + HasResultSet.GetHashCode();
      hashcode = (hashcode * 397) + IsAlign.GetHashCode();
      if((QueryDataSet != null) && __isset.queryDataSet)
      {
        hashcode = (hashcode * 397) + QueryDataSet.GetHashCode();
      }
      if((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
      {
        hashcode = (hashcode * 397) + NonAlignQueryDataSet.GetHashCode();
      }
      if((QueryResult != null) && __isset.queryResult)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(QueryResult);
      }
      if(__isset.moreData)
      {
        hashcode = (hashcode * 397) + MoreData.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSFetchResultsResp(");
    if((Status != null))
    {
      sb.Append(", Status: ");
      Status.ToString(sb);
    }
    sb.Append(", HasResultSet: ");
    HasResultSet.ToString(sb);
    sb.Append(", IsAlign: ");
    IsAlign.ToString(sb);
    if((QueryDataSet != null) && __isset.queryDataSet)
    {
      sb.Append(", QueryDataSet: ");
      QueryDataSet.ToString(sb);
    }
    if((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
    {
      sb.Append(", NonAlignQueryDataSet: ");
      NonAlignQueryDataSet.ToString(sb);
    }
    if((QueryResult != null) && __isset.queryResult)
    {
      sb.Append(", QueryResult: ");
      QueryResult.ToString(sb);
    }
    if(__isset.moreData)
    {
      sb.Append(", MoreData: ");
      MoreData.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

