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


public partial class TEndPoint : TBase
{

  public string Ip { get; set; }

  public int Port { get; set; }

  public TEndPoint()
  {
  }

  public TEndPoint(string ip, int port) : this()
  {
    this.Ip = ip;
    this.Port = port;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_ip = false;
      bool isset_port = false;
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
              Ip = await iprot.ReadStringAsync(cancellationToken);
              isset_ip = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.I32)
            {
              Port = await iprot.ReadI32Async(cancellationToken);
              isset_port = true;
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
      if (!isset_ip)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_port)
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
      var struc = new TStruct("TEndPoint");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((Ip != null))
      {
        field.Name = "ip";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Ip, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      field.Name = "port";
      field.Type = TType.I32;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(Port, cancellationToken);
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
    if (!(that is TEndPoint other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Ip, other.Ip)
      && System.Object.Equals(Port, other.Port);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((Ip != null))
      {
        hashcode = (hashcode * 397) + Ip.GetHashCode();
      }
      hashcode = (hashcode * 397) + Port.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TEndPoint(");
    if((Ip != null))
    {
      sb.Append(", Ip: ");
      Ip.ToString(sb);
    }
    sb.Append(", Port: ");
    Port.ToString(sb);
    sb.Append(')');
    return sb.ToString();
  }
}

