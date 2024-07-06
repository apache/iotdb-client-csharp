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



public partial class TSCreateTimeseriesReq : TBase
{
  private Dictionary<string, string> _props;
  private Dictionary<string, string> _tags;
  private Dictionary<string, string> _attributes;
  private string _measurementAlias;

  public long SessionId { get; set; }

  public string Path { get; set; }

  public int DataType { get; set; }

  public int Encoding { get; set; }

  public int Compressor { get; set; }

  public Dictionary<string, string> Props
  {
    get
    {
      return _props;
    }
    set
    {
      __isset.props = true;
      this._props = value;
    }
  }

  public Dictionary<string, string> Tags
  {
    get
    {
      return _tags;
    }
    set
    {
      __isset.tags = true;
      this._tags = value;
    }
  }

  public Dictionary<string, string> Attributes
  {
    get
    {
      return _attributes;
    }
    set
    {
      __isset.attributes = true;
      this._attributes = value;
    }
  }

  public string MeasurementAlias
  {
    get
    {
      return _measurementAlias;
    }
    set
    {
      __isset.measurementAlias = true;
      this._measurementAlias = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool props;
    public bool tags;
    public bool attributes;
    public bool measurementAlias;
  }

  public TSCreateTimeseriesReq()
  {
  }

  public TSCreateTimeseriesReq(long sessionId, string path, int dataType, int encoding, int compressor) : this()
  {
    this.SessionId = sessionId;
    this.Path = path;
    this.DataType = dataType;
    this.Encoding = encoding;
    this.Compressor = compressor;
  }

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_path = false;
      bool isset_dataType = false;
      bool isset_encoding = false;
      bool isset_compressor = false;
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
            if (field.Type == TType.String)
            {
              Path = await iprot.ReadStringAsync(cancellationToken);
              isset_path = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.I32)
            {
              DataType = await iprot.ReadI32Async(cancellationToken);
              isset_dataType = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.I32)
            {
              Encoding = await iprot.ReadI32Async(cancellationToken);
              isset_encoding = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I32)
            {
              Compressor = await iprot.ReadI32Async(cancellationToken);
              isset_compressor = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.Map)
            {
              {
                TMap _map207 = await iprot.ReadMapBeginAsync(cancellationToken);
                Props = new Dictionary<string, string>(_map207.Count);
                for(int _i208 = 0; _i208 < _map207.Count; ++_i208)
                {
                  string _key209;
                  string _val210;
                  _key209 = await iprot.ReadStringAsync(cancellationToken);
                  _val210 = await iprot.ReadStringAsync(cancellationToken);
                  Props[_key209] = _val210;
                }
                await iprot.ReadMapEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.Map)
            {
              {
                TMap _map211 = await iprot.ReadMapBeginAsync(cancellationToken);
                Tags = new Dictionary<string, string>(_map211.Count);
                for(int _i212 = 0; _i212 < _map211.Count; ++_i212)
                {
                  string _key213;
                  string _val214;
                  _key213 = await iprot.ReadStringAsync(cancellationToken);
                  _val214 = await iprot.ReadStringAsync(cancellationToken);
                  Tags[_key213] = _val214;
                }
                await iprot.ReadMapEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
            if (field.Type == TType.Map)
            {
              {
                TMap _map215 = await iprot.ReadMapBeginAsync(cancellationToken);
                Attributes = new Dictionary<string, string>(_map215.Count);
                for(int _i216 = 0; _i216 < _map215.Count; ++_i216)
                {
                  string _key217;
                  string _val218;
                  _key217 = await iprot.ReadStringAsync(cancellationToken);
                  _val218 = await iprot.ReadStringAsync(cancellationToken);
                  Attributes[_key217] = _val218;
                }
                await iprot.ReadMapEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 9:
            if (field.Type == TType.String)
            {
              MeasurementAlias = await iprot.ReadStringAsync(cancellationToken);
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
      if (!isset_path)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_dataType)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_encoding)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_compressor)
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
      var struc = new TStruct("TSCreateTimeseriesReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "path";
      field.Type = TType.String;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteStringAsync(Path, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "dataType";
      field.Type = TType.I32;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(DataType, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "encoding";
      field.Type = TType.I32;
      field.ID = 4;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(Encoding, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "compressor";
      field.Type = TType.I32;
      field.ID = 5;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI32Async(Compressor, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if (Props != null && __isset.props)
      {
        field.Name = "props";
        field.Type = TType.Map;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Props.Count), cancellationToken);
          foreach (string _iter219 in Props.Keys)
          {
            await oprot.WriteStringAsync(_iter219, cancellationToken);
            await oprot.WriteStringAsync(Props[_iter219], cancellationToken);
          }
          await oprot.WriteMapEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if (Tags != null && __isset.tags)
      {
        field.Name = "tags";
        field.Type = TType.Map;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Tags.Count), cancellationToken);
          foreach (string _iter220 in Tags.Keys)
          {
            await oprot.WriteStringAsync(_iter220, cancellationToken);
            await oprot.WriteStringAsync(Tags[_iter220], cancellationToken);
          }
          await oprot.WriteMapEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if (Attributes != null && __isset.attributes)
      {
        field.Name = "attributes";
        field.Type = TType.Map;
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Attributes.Count), cancellationToken);
          foreach (string _iter221 in Attributes.Keys)
          {
            await oprot.WriteStringAsync(_iter221, cancellationToken);
            await oprot.WriteStringAsync(Attributes[_iter221], cancellationToken);
          }
          await oprot.WriteMapEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if (MeasurementAlias != null && __isset.measurementAlias)
      {
        field.Name = "measurementAlias";
        field.Type = TType.String;
        field.ID = 9;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(MeasurementAlias, cancellationToken);
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
    var other = that as TSCreateTimeseriesReq;
    if (other == null) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && System.Object.Equals(Path, other.Path)
      && System.Object.Equals(DataType, other.DataType)
      && System.Object.Equals(Encoding, other.Encoding)
      && System.Object.Equals(Compressor, other.Compressor)
      && ((__isset.props == other.__isset.props) && ((!__isset.props) || (TCollections.Equals(Props, other.Props))))
      && ((__isset.tags == other.__isset.tags) && ((!__isset.tags) || (TCollections.Equals(Tags, other.Tags))))
      && ((__isset.attributes == other.__isset.attributes) && ((!__isset.attributes) || (TCollections.Equals(Attributes, other.Attributes))))
      && ((__isset.measurementAlias == other.__isset.measurementAlias) && ((!__isset.measurementAlias) || (System.Object.Equals(MeasurementAlias, other.MeasurementAlias))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      hashcode = (hashcode * 397) + Path.GetHashCode();
      hashcode = (hashcode * 397) + DataType.GetHashCode();
      hashcode = (hashcode * 397) + Encoding.GetHashCode();
      hashcode = (hashcode * 397) + Compressor.GetHashCode();
      if(__isset.props)
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Props);
      if(__isset.tags)
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Tags);
      if(__isset.attributes)
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Attributes);
      if(__isset.measurementAlias)
        hashcode = (hashcode * 397) + MeasurementAlias.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSCreateTimeseriesReq(");
    sb.Append(", SessionId: ");
    sb.Append(SessionId);
    sb.Append(", Path: ");
    sb.Append(Path);
    sb.Append(", DataType: ");
    sb.Append(DataType);
    sb.Append(", Encoding: ");
    sb.Append(Encoding);
    sb.Append(", Compressor: ");
    sb.Append(Compressor);
    if (Props != null && __isset.props)
    {
      sb.Append(", Props: ");
      sb.Append(Props);
    }
    if (Tags != null && __isset.tags)
    {
      sb.Append(", Tags: ");
      sb.Append(Tags);
    }
    if (Attributes != null && __isset.attributes)
    {
      sb.Append(", Attributes: ");
      sb.Append(Attributes);
    }
    if (MeasurementAlias != null && __isset.measurementAlias)
    {
      sb.Append(", MeasurementAlias: ");
      sb.Append(MeasurementAlias);
    }
    sb.Append(")");
    return sb.ToString();
  }
}

