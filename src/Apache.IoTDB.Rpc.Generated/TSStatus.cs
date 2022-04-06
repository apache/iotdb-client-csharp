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


public partial class TSStatus : TBase
{
  private string _message;
  private List<TSStatus> _subStatus;
  private EndPoint _redirectNode;

  public int Code { get; set; }

  public string Message
  {
    get
    {
      return _message;
    }
    set
    {
      __isset.message = true;
      this._message = value;
    }
  }

  public List<TSStatus> SubStatus
  {
    get
    {
      return _subStatus;
    }
    set
    {
      __isset.subStatus = true;
      this._subStatus = value;
    }
  }

  public EndPoint RedirectNode
  {
    get
    {
      return _redirectNode;
    }
    set
    {
      __isset.redirectNode = true;
      this._redirectNode = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool message;
    public bool subStatus;
    public bool redirectNode;
  }

  public TSStatus()
  {
  }

  public TSStatus(int code) : this()
  {
    this.Code = code;
  }

  public TSStatus DeepCopy()
  {
    var tmp2 = new TSStatus();
    tmp2.Code = this.Code;
    if((Message != null) && __isset.message)
    {
      tmp2.Message = this.Message;
    }
    tmp2.__isset.message = this.__isset.message;
    if((SubStatus != null) && __isset.subStatus)
    {
      tmp2.SubStatus = this.SubStatus.DeepCopy();
    }
    tmp2.__isset.subStatus = this.__isset.subStatus;
    if((RedirectNode != null) && __isset.redirectNode)
    {
      tmp2.RedirectNode = (EndPoint)this.RedirectNode.DeepCopy();
    }
    tmp2.__isset.redirectNode = this.__isset.redirectNode;
    return tmp2;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_code = false;
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
            if (field.Type == TType.I32)
            {
              Code = await iprot.ReadI32Async(cancellationToken);
              isset_code = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              Message = await iprot.ReadStringAsync(cancellationToken);
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
                TList _list3 = await iprot.ReadListBeginAsync(cancellationToken);
                SubStatus = new List<TSStatus>(_list3.Count);
                for(int _i4 = 0; _i4 < _list3.Count; ++_i4)
                {
                  TSStatus _elem5;
                  _elem5 = new TSStatus();
                  await _elem5.ReadAsync(iprot, cancellationToken);
                  SubStatus.Add(_elem5);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.Struct)
            {
              RedirectNode = new EndPoint();
              await RedirectNode.ReadAsync(iprot, cancellationToken);
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
      if (!isset_code)
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
      var struc = new TStruct("TSStatus");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "code";
      field.Type = TType.I32;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(Code, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Message != null) && __isset.message)
      {
        field.Name = "message";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Message, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((SubStatus != null) && __isset.subStatus)
      {
        field.Name = "subStatus";
        field.Type = TType.List;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Struct, SubStatus.Count), cancellationToken);
          foreach (TSStatus _iter6 in SubStatus)
          {
            await _iter6.WriteAsync(oprot, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((RedirectNode != null) && __isset.redirectNode)
      {
        field.Name = "redirectNode";
        field.Type = TType.Struct;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await RedirectNode.WriteAsync(oprot, cancellationToken);
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
    if (!(that is TSStatus other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Code, other.Code)
      && ((__isset.message == other.__isset.message) && ((!__isset.message) || (System.Object.Equals(Message, other.Message))))
      && ((__isset.subStatus == other.__isset.subStatus) && ((!__isset.subStatus) || (TCollections.Equals(SubStatus, other.SubStatus))))
      && ((__isset.redirectNode == other.__isset.redirectNode) && ((!__isset.redirectNode) || (System.Object.Equals(RedirectNode, other.RedirectNode))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + Code.GetHashCode();
      if((Message != null) && __isset.message)
      {
        hashcode = (hashcode * 397) + Message.GetHashCode();
      }
      if((SubStatus != null) && __isset.subStatus)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(SubStatus);
      }
      if((RedirectNode != null) && __isset.redirectNode)
      {
        hashcode = (hashcode * 397) + RedirectNode.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSStatus(");
    sb.Append(", Code: ");
    Code.ToString(sb);
    if((Message != null) && __isset.message)
    {
      sb.Append(", Message: ");
      Message.ToString(sb);
    }
    if((SubStatus != null) && __isset.subStatus)
    {
      sb.Append(", SubStatus: ");
      SubStatus.ToString(sb);
    }
    if((RedirectNode != null) && __isset.redirectNode)
    {
      sb.Append(", RedirectNode: ");
      RedirectNode.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

