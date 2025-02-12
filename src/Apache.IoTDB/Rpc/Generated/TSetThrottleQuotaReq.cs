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


public partial class TSetThrottleQuotaReq : TBase
{

  public string UserName { get; set; }

  public TThrottleQuota ThrottleQuota { get; set; }

  public TSetThrottleQuotaReq()
  {
  }

  public TSetThrottleQuotaReq(string userName, TThrottleQuota throttleQuota) : this()
  {
    this.UserName = userName;
    this.ThrottleQuota = throttleQuota;
  }

  public TSetThrottleQuotaReq DeepCopy()
  {
    var tmp90 = new TSetThrottleQuotaReq();
    if((UserName != null))
    {
      tmp90.UserName = this.UserName;
    }
    if((ThrottleQuota != null))
    {
      tmp90.ThrottleQuota = (TThrottleQuota)this.ThrottleQuota.DeepCopy();
    }
    return tmp90;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_userName = false;
      bool isset_throttleQuota = false;
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
              UserName = await iprot.ReadStringAsync(cancellationToken);
              isset_userName = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.Struct)
            {
              ThrottleQuota = new TThrottleQuota();
              await ThrottleQuota.ReadAsync(iprot, cancellationToken);
              isset_throttleQuota = true;
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
      if (!isset_userName)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_throttleQuota)
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
      var struc = new TStruct("TSetThrottleQuotaReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((UserName != null))
      {
        field.Name = "userName";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(UserName, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((ThrottleQuota != null))
      {
        field.Name = "throttleQuota";
        field.Type = TType.Struct;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await ThrottleQuota.WriteAsync(oprot, cancellationToken);
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
    if (!(that is TSetThrottleQuotaReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(UserName, other.UserName)
      && System.Object.Equals(ThrottleQuota, other.ThrottleQuota);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((UserName != null))
      {
        hashcode = (hashcode * 397) + UserName.GetHashCode();
      }
      if((ThrottleQuota != null))
      {
        hashcode = (hashcode * 397) + ThrottleQuota.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSetThrottleQuotaReq(");
    if((UserName != null))
    {
      sb.Append(", UserName: ");
      UserName.ToString(sb);
    }
    if((ThrottleQuota != null))
    {
      sb.Append(", ThrottleQuota: ");
      ThrottleQuota.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

