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



public partial class TSetSpaceQuotaReq : TBase
{

  public List<string> Database { get; set; }

  public TSpaceQuota SpaceLimit { get; set; }

  public TSetSpaceQuotaReq()
  {
  }

  public TSetSpaceQuotaReq(List<string> database, TSpaceQuota spaceLimit) : this()
  {
    this.Database = database;
    this.SpaceLimit = spaceLimit;
  }

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_database = false;
      bool isset_spaceLimit = false;
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
            if (field.Type == TType.List)
            {
              {
                TList _list38 = await iprot.ReadListBeginAsync(cancellationToken);
                Database = new List<string>(_list38.Count);
                for(int _i39 = 0; _i39 < _list38.Count; ++_i39)
                {
                  string _elem40;
                  _elem40 = await iprot.ReadStringAsync(cancellationToken);
                  Database.Add(_elem40);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_database = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.Struct)
            {
              SpaceLimit = new TSpaceQuota();
              await SpaceLimit.ReadAsync(iprot, cancellationToken);
              isset_spaceLimit = true;
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
      if (!isset_database)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_spaceLimit)
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
      var struc = new TStruct("TSetSpaceQuotaReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "database";
      field.Type = TType.List;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      {
        await oprot.WriteListBeginAsync(new TList(TType.String, Database.Count), cancellationToken);
        foreach (string _iter41 in Database)
        {
          await oprot.WriteStringAsync(_iter41, cancellationToken);
        }
        await oprot.WriteListEndAsync(cancellationToken);
      }
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "spaceLimit";
      field.Type = TType.Struct;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await SpaceLimit.WriteAsync(oprot, cancellationToken);
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
    var other = that as TSetSpaceQuotaReq;
    if (other == null) return false;
    if (ReferenceEquals(this, other)) return true;
    return TCollections.Equals(Database, other.Database)
      && System.Object.Equals(SpaceLimit, other.SpaceLimit);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + TCollections.GetHashCode(Database);
      hashcode = (hashcode * 397) + SpaceLimit.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSetSpaceQuotaReq(");
    sb.Append(", Database: ");
    sb.Append(Database);
    sb.Append(", SpaceLimit: ");
    sb.Append(SpaceLimit== null ? "<null>" : SpaceLimit.ToString());
    sb.Append(")");
    return sb.ToString();
  }
}
