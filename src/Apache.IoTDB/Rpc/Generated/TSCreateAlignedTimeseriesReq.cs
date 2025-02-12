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


public partial class TSCreateAlignedTimeseriesReq : TBase
{
  private List<string> _measurementAlias;
  private List<Dictionary<string, string>> _tagsList;
  private List<Dictionary<string, string>> _attributesList;

  public long SessionId { get; set; }

  public string PrefixPath { get; set; }

  public List<string> Measurements { get; set; }

  public List<int> DataTypes { get; set; }

  public List<int> Encodings { get; set; }

  public List<int> Compressors { get; set; }

  public List<string> MeasurementAlias
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

  public List<Dictionary<string, string>> TagsList
  {
    get
    {
      return _tagsList;
    }
    set
    {
      __isset.tagsList = true;
      this._tagsList = value;
    }
  }

  public List<Dictionary<string, string>> AttributesList
  {
    get
    {
      return _attributesList;
    }
    set
    {
      __isset.attributesList = true;
      this._attributesList = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool measurementAlias;
    public bool tagsList;
    public bool attributesList;
  }

  public TSCreateAlignedTimeseriesReq()
  {
  }

  public TSCreateAlignedTimeseriesReq(long sessionId, string prefixPath, List<string> measurements, List<int> dataTypes, List<int> encodings, List<int> compressors) : this()
  {
    this.SessionId = sessionId;
    this.PrefixPath = prefixPath;
    this.Measurements = measurements;
    this.DataTypes = dataTypes;
    this.Encodings = encodings;
    this.Compressors = compressors;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_prefixPath = false;
      bool isset_measurements = false;
      bool isset_dataTypes = false;
      bool isset_encodings = false;
      bool isset_compressors = false;
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
              PrefixPath = await iprot.ReadStringAsync(cancellationToken);
              isset_prefixPath = true;
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
                TList _list262 = await iprot.ReadListBeginAsync(cancellationToken);
                Measurements = new List<string>(_list262.Count);
                for(int _i263 = 0; _i263 < _list262.Count; ++_i263)
                {
                  string _elem264;
                  _elem264 = await iprot.ReadStringAsync(cancellationToken);
                  Measurements.Add(_elem264);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_measurements = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.List)
            {
              {
                TList _list265 = await iprot.ReadListBeginAsync(cancellationToken);
                DataTypes = new List<int>(_list265.Count);
                for(int _i266 = 0; _i266 < _list265.Count; ++_i266)
                {
                  int _elem267;
                  _elem267 = await iprot.ReadI32Async(cancellationToken);
                  DataTypes.Add(_elem267);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_dataTypes = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.List)
            {
              {
                TList _list268 = await iprot.ReadListBeginAsync(cancellationToken);
                Encodings = new List<int>(_list268.Count);
                for(int _i269 = 0; _i269 < _list268.Count; ++_i269)
                {
                  int _elem270;
                  _elem270 = await iprot.ReadI32Async(cancellationToken);
                  Encodings.Add(_elem270);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_encodings = true;
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
                TList _list271 = await iprot.ReadListBeginAsync(cancellationToken);
                Compressors = new List<int>(_list271.Count);
                for(int _i272 = 0; _i272 < _list271.Count; ++_i272)
                {
                  int _elem273;
                  _elem273 = await iprot.ReadI32Async(cancellationToken);
                  Compressors.Add(_elem273);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_compressors = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.List)
            {
              {
                TList _list274 = await iprot.ReadListBeginAsync(cancellationToken);
                MeasurementAlias = new List<string>(_list274.Count);
                for(int _i275 = 0; _i275 < _list274.Count; ++_i275)
                {
                  string _elem276;
                  _elem276 = await iprot.ReadStringAsync(cancellationToken);
                  MeasurementAlias.Add(_elem276);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
            if (field.Type == TType.List)
            {
              {
                TList _list277 = await iprot.ReadListBeginAsync(cancellationToken);
                TagsList = new List<Dictionary<string, string>>(_list277.Count);
                for(int _i278 = 0; _i278 < _list277.Count; ++_i278)
                {
                  Dictionary<string, string> _elem279;
                  {
                    TMap _map280 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem279 = new Dictionary<string, string>(_map280.Count);
                    for(int _i281 = 0; _i281 < _map280.Count; ++_i281)
                    {
                      string _key282;
                      string _val283;
                      _key282 = await iprot.ReadStringAsync(cancellationToken);
                      _val283 = await iprot.ReadStringAsync(cancellationToken);
                      _elem279[_key282] = _val283;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  TagsList.Add(_elem279);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 9:
            if (field.Type == TType.List)
            {
              {
                TList _list284 = await iprot.ReadListBeginAsync(cancellationToken);
                AttributesList = new List<Dictionary<string, string>>(_list284.Count);
                for(int _i285 = 0; _i285 < _list284.Count; ++_i285)
                {
                  Dictionary<string, string> _elem286;
                  {
                    TMap _map287 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem286 = new Dictionary<string, string>(_map287.Count);
                    for(int _i288 = 0; _i288 < _map287.Count; ++_i288)
                    {
                      string _key289;
                      string _val290;
                      _key289 = await iprot.ReadStringAsync(cancellationToken);
                      _val290 = await iprot.ReadStringAsync(cancellationToken);
                      _elem286[_key289] = _val290;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  AttributesList.Add(_elem286);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
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
      if (!isset_prefixPath)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_measurements)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_dataTypes)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_encodings)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_compressors)
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
      var struc = new TStruct("TSCreateAlignedTimeseriesReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((PrefixPath != null))
      {
        field.Name = "prefixPath";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(PrefixPath, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Measurements != null))
      {
        field.Name = "measurements";
        field.Type = TType.List;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, Measurements.Count), cancellationToken);
          foreach (string _iter291 in Measurements)
          {
            await oprot.WriteStringAsync(_iter291, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((DataTypes != null))
      {
        field.Name = "dataTypes";
        field.Type = TType.List;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, DataTypes.Count), cancellationToken);
          foreach (int _iter292 in DataTypes)
          {
            await oprot.WriteI32Async(_iter292, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Encodings != null))
      {
        field.Name = "encodings";
        field.Type = TType.List;
        field.ID = 5;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, Encodings.Count), cancellationToken);
          foreach (int _iter293 in Encodings)
          {
            await oprot.WriteI32Async(_iter293, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Compressors != null))
      {
        field.Name = "compressors";
        field.Type = TType.List;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, Compressors.Count), cancellationToken);
          foreach (int _iter294 in Compressors)
          {
            await oprot.WriteI32Async(_iter294, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((MeasurementAlias != null) && __isset.measurementAlias)
      {
        field.Name = "measurementAlias";
        field.Type = TType.List;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, MeasurementAlias.Count), cancellationToken);
          foreach (string _iter295 in MeasurementAlias)
          {
            await oprot.WriteStringAsync(_iter295, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((TagsList != null) && __isset.tagsList)
      {
        field.Name = "tagsList";
        field.Type = TType.List;
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, TagsList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter296 in TagsList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter296.Count), cancellationToken);
              foreach (string _iter297 in _iter296.Keys)
              {
                await oprot.WriteStringAsync(_iter297, cancellationToken);
                await oprot.WriteStringAsync(_iter296[_iter297], cancellationToken);
              }
              await oprot.WriteMapEndAsync(cancellationToken);
            }
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((AttributesList != null) && __isset.attributesList)
      {
        field.Name = "attributesList";
        field.Type = TType.List;
        field.ID = 9;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, AttributesList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter298 in AttributesList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter298.Count), cancellationToken);
              foreach (string _iter299 in _iter298.Keys)
              {
                await oprot.WriteStringAsync(_iter299, cancellationToken);
                await oprot.WriteStringAsync(_iter298[_iter299], cancellationToken);
              }
              await oprot.WriteMapEndAsync(cancellationToken);
            }
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
    if (!(that is TSCreateAlignedTimeseriesReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && System.Object.Equals(PrefixPath, other.PrefixPath)
      && TCollections.Equals(Measurements, other.Measurements)
      && TCollections.Equals(DataTypes, other.DataTypes)
      && TCollections.Equals(Encodings, other.Encodings)
      && TCollections.Equals(Compressors, other.Compressors)
      && ((__isset.measurementAlias == other.__isset.measurementAlias) && ((!__isset.measurementAlias) || (TCollections.Equals(MeasurementAlias, other.MeasurementAlias))))
      && ((__isset.tagsList == other.__isset.tagsList) && ((!__isset.tagsList) || (TCollections.Equals(TagsList, other.TagsList))))
      && ((__isset.attributesList == other.__isset.attributesList) && ((!__isset.attributesList) || (TCollections.Equals(AttributesList, other.AttributesList))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      if((PrefixPath != null))
      {
        hashcode = (hashcode * 397) + PrefixPath.GetHashCode();
      }
      if((Measurements != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Measurements);
      }
      if((DataTypes != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(DataTypes);
      }
      if((Encodings != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Encodings);
      }
      if((Compressors != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Compressors);
      }
      if((MeasurementAlias != null) && __isset.measurementAlias)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(MeasurementAlias);
      }
      if((TagsList != null) && __isset.tagsList)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(TagsList);
      }
      if((AttributesList != null) && __isset.attributesList)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(AttributesList);
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSCreateAlignedTimeseriesReq(");
    sb.Append(", SessionId: ");
    SessionId.ToString(sb);
    if((PrefixPath != null))
    {
      sb.Append(", PrefixPath: ");
      PrefixPath.ToString(sb);
    }
    if((Measurements != null))
    {
      sb.Append(", Measurements: ");
      Measurements.ToString(sb);
    }
    if((DataTypes != null))
    {
      sb.Append(", DataTypes: ");
      DataTypes.ToString(sb);
    }
    if((Encodings != null))
    {
      sb.Append(", Encodings: ");
      Encodings.ToString(sb);
    }
    if((Compressors != null))
    {
      sb.Append(", Compressors: ");
      Compressors.ToString(sb);
    }
    if((MeasurementAlias != null) && __isset.measurementAlias)
    {
      sb.Append(", MeasurementAlias: ");
      MeasurementAlias.ToString(sb);
    }
    if((TagsList != null) && __isset.tagsList)
    {
      sb.Append(", TagsList: ");
      TagsList.ToString(sb);
    }
    if((AttributesList != null) && __isset.attributesList)
    {
      sb.Append(", AttributesList: ");
      AttributesList.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

