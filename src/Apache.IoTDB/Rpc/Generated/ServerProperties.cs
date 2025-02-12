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


public partial class ServerProperties : TBase
{
  private int _maxConcurrentClientNum;
  private int _thriftMaxFrameSize;
  private bool _isReadOnly;
  private string _buildInfo;
  private string _logo;

  public string Version { get; set; }

  public List<string> SupportedTimeAggregationOperations { get; set; }

  public string TimestampPrecision { get; set; }

  public int MaxConcurrentClientNum
  {
    get
    {
      return _maxConcurrentClientNum;
    }
    set
    {
      __isset.maxConcurrentClientNum = true;
      this._maxConcurrentClientNum = value;
    }
  }

  public int ThriftMaxFrameSize
  {
    get
    {
      return _thriftMaxFrameSize;
    }
    set
    {
      __isset.thriftMaxFrameSize = true;
      this._thriftMaxFrameSize = value;
    }
  }

  public bool IsReadOnly
  {
    get
    {
      return _isReadOnly;
    }
    set
    {
      __isset.isReadOnly = true;
      this._isReadOnly = value;
    }
  }

  public string BuildInfo
  {
    get
    {
      return _buildInfo;
    }
    set
    {
      __isset.buildInfo = true;
      this._buildInfo = value;
    }
  }

  public string Logo
  {
    get
    {
      return _logo;
    }
    set
    {
      __isset.logo = true;
      this._logo = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool maxConcurrentClientNum;
    public bool thriftMaxFrameSize;
    public bool isReadOnly;
    public bool buildInfo;
    public bool logo;
  }

  public ServerProperties()
  {
  }

  public ServerProperties(string version, List<string> supportedTimeAggregationOperations, string timestampPrecision) : this()
  {
    this.Version = version;
    this.SupportedTimeAggregationOperations = supportedTimeAggregationOperations;
    this.TimestampPrecision = timestampPrecision;
  }

  public ServerProperties DeepCopy()
  {
    var tmp397 = new ServerProperties();
    if((Version != null))
    {
      tmp397.Version = this.Version;
    }
    if((SupportedTimeAggregationOperations != null))
    {
      tmp397.SupportedTimeAggregationOperations = this.SupportedTimeAggregationOperations.DeepCopy();
    }
    if((TimestampPrecision != null))
    {
      tmp397.TimestampPrecision = this.TimestampPrecision;
    }
    if(__isset.maxConcurrentClientNum)
    {
      tmp397.MaxConcurrentClientNum = this.MaxConcurrentClientNum;
    }
    tmp397.__isset.maxConcurrentClientNum = this.__isset.maxConcurrentClientNum;
    if(__isset.thriftMaxFrameSize)
    {
      tmp397.ThriftMaxFrameSize = this.ThriftMaxFrameSize;
    }
    tmp397.__isset.thriftMaxFrameSize = this.__isset.thriftMaxFrameSize;
    if(__isset.isReadOnly)
    {
      tmp397.IsReadOnly = this.IsReadOnly;
    }
    tmp397.__isset.isReadOnly = this.__isset.isReadOnly;
    if((BuildInfo != null) && __isset.buildInfo)
    {
      tmp397.BuildInfo = this.BuildInfo;
    }
    tmp397.__isset.buildInfo = this.__isset.buildInfo;
    if((Logo != null) && __isset.logo)
    {
      tmp397.Logo = this.Logo;
    }
    tmp397.__isset.logo = this.__isset.logo;
    return tmp397;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_version = false;
      bool isset_supportedTimeAggregationOperations = false;
      bool isset_timestampPrecision = false;
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
              Version = await iprot.ReadStringAsync(cancellationToken);
              isset_version = true;
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
                TList _list398 = await iprot.ReadListBeginAsync(cancellationToken);
                SupportedTimeAggregationOperations = new List<string>(_list398.Count);
                for(int _i399 = 0; _i399 < _list398.Count; ++_i399)
                {
                  string _elem400;
                  _elem400 = await iprot.ReadStringAsync(cancellationToken);
                  SupportedTimeAggregationOperations.Add(_elem400);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_supportedTimeAggregationOperations = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.String)
            {
              TimestampPrecision = await iprot.ReadStringAsync(cancellationToken);
              isset_timestampPrecision = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.I32)
            {
              MaxConcurrentClientNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I32)
            {
              ThriftMaxFrameSize = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.Bool)
            {
              IsReadOnly = await iprot.ReadBoolAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.String)
            {
              BuildInfo = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
            if (field.Type == TType.String)
            {
              Logo = await iprot.ReadStringAsync(cancellationToken);
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
      if (!isset_version)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_supportedTimeAggregationOperations)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_timestampPrecision)
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
      var struc = new TStruct("ServerProperties");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((Version != null))
      {
        field.Name = "version";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Version, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((SupportedTimeAggregationOperations != null))
      {
        field.Name = "supportedTimeAggregationOperations";
        field.Type = TType.List;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, SupportedTimeAggregationOperations.Count), cancellationToken);
          foreach (string _iter401 in SupportedTimeAggregationOperations)
          {
            await oprot.WriteStringAsync(_iter401, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((TimestampPrecision != null))
      {
        field.Name = "timestampPrecision";
        field.Type = TType.String;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(TimestampPrecision, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.maxConcurrentClientNum)
      {
        field.Name = "maxConcurrentClientNum";
        field.Type = TType.I32;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(MaxConcurrentClientNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.thriftMaxFrameSize)
      {
        field.Name = "thriftMaxFrameSize";
        field.Type = TType.I32;
        field.ID = 5;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(ThriftMaxFrameSize, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.isReadOnly)
      {
        field.Name = "isReadOnly";
        field.Type = TType.Bool;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(IsReadOnly, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((BuildInfo != null) && __isset.buildInfo)
      {
        field.Name = "buildInfo";
        field.Type = TType.String;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(BuildInfo, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Logo != null) && __isset.logo)
      {
        field.Name = "logo";
        field.Type = TType.String;
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Logo, cancellationToken);
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
    if (!(that is ServerProperties other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Version, other.Version)
      && TCollections.Equals(SupportedTimeAggregationOperations, other.SupportedTimeAggregationOperations)
      && System.Object.Equals(TimestampPrecision, other.TimestampPrecision)
      && ((__isset.maxConcurrentClientNum == other.__isset.maxConcurrentClientNum) && ((!__isset.maxConcurrentClientNum) || (System.Object.Equals(MaxConcurrentClientNum, other.MaxConcurrentClientNum))))
      && ((__isset.thriftMaxFrameSize == other.__isset.thriftMaxFrameSize) && ((!__isset.thriftMaxFrameSize) || (System.Object.Equals(ThriftMaxFrameSize, other.ThriftMaxFrameSize))))
      && ((__isset.isReadOnly == other.__isset.isReadOnly) && ((!__isset.isReadOnly) || (System.Object.Equals(IsReadOnly, other.IsReadOnly))))
      && ((__isset.buildInfo == other.__isset.buildInfo) && ((!__isset.buildInfo) || (System.Object.Equals(BuildInfo, other.BuildInfo))))
      && ((__isset.logo == other.__isset.logo) && ((!__isset.logo) || (System.Object.Equals(Logo, other.Logo))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((Version != null))
      {
        hashcode = (hashcode * 397) + Version.GetHashCode();
      }
      if((SupportedTimeAggregationOperations != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(SupportedTimeAggregationOperations);
      }
      if((TimestampPrecision != null))
      {
        hashcode = (hashcode * 397) + TimestampPrecision.GetHashCode();
      }
      if(__isset.maxConcurrentClientNum)
      {
        hashcode = (hashcode * 397) + MaxConcurrentClientNum.GetHashCode();
      }
      if(__isset.thriftMaxFrameSize)
      {
        hashcode = (hashcode * 397) + ThriftMaxFrameSize.GetHashCode();
      }
      if(__isset.isReadOnly)
      {
        hashcode = (hashcode * 397) + IsReadOnly.GetHashCode();
      }
      if((BuildInfo != null) && __isset.buildInfo)
      {
        hashcode = (hashcode * 397) + BuildInfo.GetHashCode();
      }
      if((Logo != null) && __isset.logo)
      {
        hashcode = (hashcode * 397) + Logo.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("ServerProperties(");
    if((Version != null))
    {
      sb.Append(", Version: ");
      Version.ToString(sb);
    }
    if((SupportedTimeAggregationOperations != null))
    {
      sb.Append(", SupportedTimeAggregationOperations: ");
      SupportedTimeAggregationOperations.ToString(sb);
    }
    if((TimestampPrecision != null))
    {
      sb.Append(", TimestampPrecision: ");
      TimestampPrecision.ToString(sb);
    }
    if(__isset.maxConcurrentClientNum)
    {
      sb.Append(", MaxConcurrentClientNum: ");
      MaxConcurrentClientNum.ToString(sb);
    }
    if(__isset.thriftMaxFrameSize)
    {
      sb.Append(", ThriftMaxFrameSize: ");
      ThriftMaxFrameSize.ToString(sb);
    }
    if(__isset.isReadOnly)
    {
      sb.Append(", IsReadOnly: ");
      IsReadOnly.ToString(sb);
    }
    if((BuildInfo != null) && __isset.buildInfo)
    {
      sb.Append(", BuildInfo: ");
      BuildInfo.ToString(sb);
    }
    if((Logo != null) && __isset.logo)
    {
      sb.Append(", Logo: ");
      Logo.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

