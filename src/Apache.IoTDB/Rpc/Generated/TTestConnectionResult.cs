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


public partial class TTestConnectionResult : TBase
{
  private string _reason;

  public TServiceProvider ServiceProvider { get; set; }

  public TSender Sender { get; set; }

  public bool Success { get; set; }

  public string Reason
  {
    get
    {
      return _reason;
    }
    set
    {
      __isset.reason = true;
      this._reason = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool reason;
  }

  public TTestConnectionResult()
  {
  }

  public TTestConnectionResult(TServiceProvider serviceProvider, TSender sender, bool success) : this()
  {
    this.ServiceProvider = serviceProvider;
    this.Sender = sender;
    this.Success = success;
  }

  public TTestConnectionResult DeepCopy()
  {
    var tmp94 = new TTestConnectionResult();
    if((ServiceProvider != null))
    {
      tmp94.ServiceProvider = (TServiceProvider)this.ServiceProvider.DeepCopy();
    }
    if((Sender != null))
    {
      tmp94.Sender = (TSender)this.Sender.DeepCopy();
    }
    tmp94.Success = this.Success;
    if((Reason != null) && __isset.reason)
    {
      tmp94.Reason = this.Reason;
    }
    tmp94.__isset.reason = this.__isset.reason;
    return tmp94;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_serviceProvider = false;
      bool isset_sender = false;
      bool isset_success = false;
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
              ServiceProvider = new TServiceProvider();
              await ServiceProvider.ReadAsync(iprot, cancellationToken);
              isset_serviceProvider = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.Struct)
            {
              Sender = new TSender();
              await Sender.ReadAsync(iprot, cancellationToken);
              isset_sender = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Bool)
            {
              Success = await iprot.ReadBoolAsync(cancellationToken);
              isset_success = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.String)
            {
              Reason = await iprot.ReadStringAsync(cancellationToken);
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
      if (!isset_serviceProvider)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_sender)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_success)
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
      var struc = new TStruct("TTestConnectionResult");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((ServiceProvider != null))
      {
        field.Name = "serviceProvider";
        field.Type = TType.Struct;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await ServiceProvider.WriteAsync(oprot, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Sender != null))
      {
        field.Name = "sender";
        field.Type = TType.Struct;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await Sender.WriteAsync(oprot, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      field.Name = "success";
      field.Type = TType.Bool;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteBoolAsync(Success, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Reason != null) && __isset.reason)
      {
        field.Name = "reason";
        field.Type = TType.String;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Reason, cancellationToken);
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
    if (!(that is TTestConnectionResult other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(ServiceProvider, other.ServiceProvider)
      && System.Object.Equals(Sender, other.Sender)
      && System.Object.Equals(Success, other.Success)
      && ((__isset.reason == other.__isset.reason) && ((!__isset.reason) || (System.Object.Equals(Reason, other.Reason))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((ServiceProvider != null))
      {
        hashcode = (hashcode * 397) + ServiceProvider.GetHashCode();
      }
      if((Sender != null))
      {
        hashcode = (hashcode * 397) + Sender.GetHashCode();
      }
      hashcode = (hashcode * 397) + Success.GetHashCode();
      if((Reason != null) && __isset.reason)
      {
        hashcode = (hashcode * 397) + Reason.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TTestConnectionResult(");
    if((ServiceProvider != null))
    {
      sb.Append(", ServiceProvider: ");
      ServiceProvider.ToString(sb);
    }
    if((Sender != null))
    {
      sb.Append(", Sender: ");
      Sender.ToString(sb);
    }
    sb.Append(", Success: ");
    Success.ToString(sb);
    if((Reason != null) && __isset.reason)
    {
      sb.Append(", Reason: ");
      Reason.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

