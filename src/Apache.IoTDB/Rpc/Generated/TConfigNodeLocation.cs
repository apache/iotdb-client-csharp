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



public partial class TConfigNodeLocation : TBase
{

  public int ConfigNodeId { get; set; }

  public TEndPoint InternalEndPoint { get; set; }

  public TEndPoint ConsensusEndPoint { get; set; }

  public TConfigNodeLocation()
  {
  }

  public TConfigNodeLocation(int configNodeId, TEndPoint internalEndPoint, TEndPoint consensusEndPoint) : this()
  {
    this.ConfigNodeId = configNodeId;
    this.InternalEndPoint = internalEndPoint;
    this.ConsensusEndPoint = consensusEndPoint;
  }

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_configNodeId = false;
      bool isset_internalEndPoint = false;
      bool isset_consensusEndPoint = false;
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
              ConfigNodeId = await iprot.ReadI32Async(cancellationToken);
              isset_configNodeId = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.Struct)
            {
              InternalEndPoint = new TEndPoint();
              await InternalEndPoint.ReadAsync(iprot, cancellationToken);
              isset_internalEndPoint = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Struct)
            {
              ConsensusEndPoint = new TEndPoint();
              await ConsensusEndPoint.ReadAsync(iprot, cancellationToken);
              isset_consensusEndPoint = true;
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
      if (!isset_configNodeId)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_internalEndPoint)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_consensusEndPoint)
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
      var struc = new TStruct("TConfigNodeLocation");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "configNodeId";
      field.Type = TType.I32;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(ConfigNodeId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "internalEndPoint";
      field.Type = TType.Struct;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await InternalEndPoint.WriteAsync(oprot, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "consensusEndPoint";
      field.Type = TType.Struct;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await ConsensusEndPoint.WriteAsync(oprot, cancellationToken);
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
    var other = that as TConfigNodeLocation;
    if (other == null) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(ConfigNodeId, other.ConfigNodeId)
      && System.Object.Equals(InternalEndPoint, other.InternalEndPoint)
      && System.Object.Equals(ConsensusEndPoint, other.ConsensusEndPoint);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + ConfigNodeId.GetHashCode();
      hashcode = (hashcode * 397) + InternalEndPoint.GetHashCode();
      hashcode = (hashcode * 397) + ConsensusEndPoint.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TConfigNodeLocation(");
    sb.Append(", ConfigNodeId: ");
    sb.Append(ConfigNodeId);
    sb.Append(", InternalEndPoint: ");
    sb.Append(InternalEndPoint== null ? "<null>" : InternalEndPoint.ToString());
    sb.Append(", ConsensusEndPoint: ");
    sb.Append(ConsensusEndPoint== null ? "<null>" : ConsensusEndPoint.ToString());
    sb.Append(")");
    return sb.ToString();
  }
}

