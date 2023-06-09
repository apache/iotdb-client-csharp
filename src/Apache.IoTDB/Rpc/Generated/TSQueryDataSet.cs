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


public partial class TSQueryDataSet : TBase
{

  public byte[] Time { get; set; }

  public List<byte[]> ValueList { get; set; }

  public List<byte[]> BitmapList { get; set; }

  public TSQueryDataSet()
  {
  }

  public TSQueryDataSet(byte[] time, List<byte[]> valueList, List<byte[]> bitmapList) : this()
  {
    this.Time = time;
    this.ValueList = valueList;
    this.BitmapList = bitmapList;
  }

  public TSQueryDataSet DeepCopy()
  {
    var tmp0 = new TSQueryDataSet();
    if((Time != null))
    {
      tmp0.Time = this.Time.ToArray();
    }
    if((ValueList != null))
    {
      tmp0.ValueList = this.ValueList.DeepCopy();
    }
    if((BitmapList != null))
    {
      tmp0.BitmapList = this.BitmapList.DeepCopy();
    }
    return tmp0;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_time = false;
      bool isset_valueList = false;
      bool isset_bitmapList = false;
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
              Time = await iprot.ReadBinaryAsync(cancellationToken);
              isset_time = true;
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
                TList _list1 = await iprot.ReadListBeginAsync(cancellationToken);
                ValueList = new List<byte[]>(_list1.Count);
                for(int _i2 = 0; _i2 < _list1.Count; ++_i2)
                {
                  byte[] _elem3;
                  _elem3 = await iprot.ReadBinaryAsync(cancellationToken);
                  ValueList.Add(_elem3);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_valueList = true;
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
                TList _list4 = await iprot.ReadListBeginAsync(cancellationToken);
                BitmapList = new List<byte[]>(_list4.Count);
                for(int _i5 = 0; _i5 < _list4.Count; ++_i5)
                {
                  byte[] _elem6;
                  _elem6 = await iprot.ReadBinaryAsync(cancellationToken);
                  BitmapList.Add(_elem6);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_bitmapList = true;
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
      if (!isset_time)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_valueList)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_bitmapList)
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
      var struc = new TStruct("TSQueryDataSet");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((Time != null))
      {
        field.Name = "time";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBinaryAsync(Time, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((ValueList != null))
      {
        field.Name = "valueList";
        field.Type = TType.List;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, ValueList.Count), cancellationToken);
          foreach (byte[] _iter7 in ValueList)
          {
            await oprot.WriteBinaryAsync(_iter7, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((BitmapList != null))
      {
        field.Name = "bitmapList";
        field.Type = TType.List;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, BitmapList.Count), cancellationToken);
          foreach (byte[] _iter8 in BitmapList)
          {
            await oprot.WriteBinaryAsync(_iter8, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
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
    if (!(that is TSQueryDataSet other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return TCollections.Equals(Time, other.Time)
      && TCollections.Equals(ValueList, other.ValueList)
      && TCollections.Equals(BitmapList, other.BitmapList);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((Time != null))
      {
        hashcode = (hashcode * 397) + Time.GetHashCode();
      }
      if((ValueList != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(ValueList);
      }
      if((BitmapList != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(BitmapList);
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSQueryDataSet(");
    if((Time != null))
    {
      sb.Append(", Time: ");
      Time.ToString(sb);
    }
    if((ValueList != null))
    {
      sb.Append(", ValueList: ");
      ValueList.ToString(sb);
    }
    if((BitmapList != null))
    {
      sb.Append(", BitmapList: ");
      BitmapList.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

