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

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
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
                TList _list222 = await iprot.ReadListBeginAsync(cancellationToken);
                Measurements = new List<string>(_list222.Count);
                for(int _i223 = 0; _i223 < _list222.Count; ++_i223)
                {
                  string _elem224;
                  _elem224 = await iprot.ReadStringAsync(cancellationToken);
                  Measurements.Add(_elem224);
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
                TList _list225 = await iprot.ReadListBeginAsync(cancellationToken);
                DataTypes = new List<int>(_list225.Count);
                for(int _i226 = 0; _i226 < _list225.Count; ++_i226)
                {
                  int _elem227;
                  _elem227 = await iprot.ReadI32Async(cancellationToken);
                  DataTypes.Add(_elem227);
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
                TList _list228 = await iprot.ReadListBeginAsync(cancellationToken);
                Encodings = new List<int>(_list228.Count);
                for(int _i229 = 0; _i229 < _list228.Count; ++_i229)
                {
                  int _elem230;
                  _elem230 = await iprot.ReadI32Async(cancellationToken);
                  Encodings.Add(_elem230);
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
                TList _list231 = await iprot.ReadListBeginAsync(cancellationToken);
                Compressors = new List<int>(_list231.Count);
                for(int _i232 = 0; _i232 < _list231.Count; ++_i232)
                {
                  int _elem233;
                  _elem233 = await iprot.ReadI32Async(cancellationToken);
                  Compressors.Add(_elem233);
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
                TList _list234 = await iprot.ReadListBeginAsync(cancellationToken);
                MeasurementAlias = new List<string>(_list234.Count);
                for(int _i235 = 0; _i235 < _list234.Count; ++_i235)
                {
                  string _elem236;
                  _elem236 = await iprot.ReadStringAsync(cancellationToken);
                  MeasurementAlias.Add(_elem236);
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
                TList _list237 = await iprot.ReadListBeginAsync(cancellationToken);
                TagsList = new List<Dictionary<string, string>>(_list237.Count);
                for(int _i238 = 0; _i238 < _list237.Count; ++_i238)
                {
                  Dictionary<string, string> _elem239;
                  {
                    TMap _map240 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem239 = new Dictionary<string, string>(_map240.Count);
                    for(int _i241 = 0; _i241 < _map240.Count; ++_i241)
                    {
                      string _key242;
                      string _val243;
                      _key242 = await iprot.ReadStringAsync(cancellationToken);
                      _val243 = await iprot.ReadStringAsync(cancellationToken);
                      _elem239[_key242] = _val243;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  TagsList.Add(_elem239);
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
                TList _list244 = await iprot.ReadListBeginAsync(cancellationToken);
                AttributesList = new List<Dictionary<string, string>>(_list244.Count);
                for(int _i245 = 0; _i245 < _list244.Count; ++_i245)
                {
                  Dictionary<string, string> _elem246;
                  {
                    TMap _map247 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem246 = new Dictionary<string, string>(_map247.Count);
                    for(int _i248 = 0; _i248 < _map247.Count; ++_i248)
                    {
                      string _key249;
                      string _val250;
                      _key249 = await iprot.ReadStringAsync(cancellationToken);
                      _val250 = await iprot.ReadStringAsync(cancellationToken);
                      _elem246[_key249] = _val250;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  AttributesList.Add(_elem246);
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

  public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
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
      field.Name = "prefixPath";
      field.Type = TType.String;
      field.ID = 2;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteStringAsync(PrefixPath, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "measurements";
      field.Type = TType.List;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      {
        await oprot.WriteListBeginAsync(new TList(TType.String, Measurements.Count), cancellationToken);
        foreach (string _iter251 in Measurements)
        {
          await oprot.WriteStringAsync(_iter251, cancellationToken);
        }
        await oprot.WriteListEndAsync(cancellationToken);
      }
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "dataTypes";
      field.Type = TType.List;
      field.ID = 4;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      {
        await oprot.WriteListBeginAsync(new TList(TType.I32, DataTypes.Count), cancellationToken);
        foreach (int _iter252 in DataTypes)
        {
          await oprot.WriteI32Async(_iter252, cancellationToken);
        }
        await oprot.WriteListEndAsync(cancellationToken);
      }
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "encodings";
      field.Type = TType.List;
      field.ID = 5;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      {
        await oprot.WriteListBeginAsync(new TList(TType.I32, Encodings.Count), cancellationToken);
        foreach (int _iter253 in Encodings)
        {
          await oprot.WriteI32Async(_iter253, cancellationToken);
        }
        await oprot.WriteListEndAsync(cancellationToken);
      }
      await oprot.WriteFieldEndAsync(cancellationToken);
      field.Name = "compressors";
      field.Type = TType.List;
      field.ID = 6;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      {
        await oprot.WriteListBeginAsync(new TList(TType.I32, Compressors.Count), cancellationToken);
        foreach (int _iter254 in Compressors)
        {
          await oprot.WriteI32Async(_iter254, cancellationToken);
        }
        await oprot.WriteListEndAsync(cancellationToken);
      }
      await oprot.WriteFieldEndAsync(cancellationToken);
      if (MeasurementAlias != null && __isset.measurementAlias)
      {
        field.Name = "measurementAlias";
        field.Type = TType.List;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, MeasurementAlias.Count), cancellationToken);
          foreach (string _iter255 in MeasurementAlias)
          {
            await oprot.WriteStringAsync(_iter255, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if (TagsList != null && __isset.tagsList)
      {
        field.Name = "tagsList";
        field.Type = TType.List;
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, TagsList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter256 in TagsList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter256.Count), cancellationToken);
              foreach (string _iter257 in _iter256.Keys)
              {
                await oprot.WriteStringAsync(_iter257, cancellationToken);
                await oprot.WriteStringAsync(_iter256[_iter257], cancellationToken);
              }
              await oprot.WriteMapEndAsync(cancellationToken);
            }
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if (AttributesList != null && __isset.attributesList)
      {
        field.Name = "attributesList";
        field.Type = TType.List;
        field.ID = 9;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, AttributesList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter258 in AttributesList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter258.Count), cancellationToken);
              foreach (string _iter259 in _iter258.Keys)
              {
                await oprot.WriteStringAsync(_iter259, cancellationToken);
                await oprot.WriteStringAsync(_iter258[_iter259], cancellationToken);
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
    var other = that as TSCreateAlignedTimeseriesReq;
    if (other == null) return false;
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
      hashcode = (hashcode * 397) + PrefixPath.GetHashCode();
      hashcode = (hashcode * 397) + TCollections.GetHashCode(Measurements);
      hashcode = (hashcode * 397) + TCollections.GetHashCode(DataTypes);
      hashcode = (hashcode * 397) + TCollections.GetHashCode(Encodings);
      hashcode = (hashcode * 397) + TCollections.GetHashCode(Compressors);
      if(__isset.measurementAlias)
        hashcode = (hashcode * 397) + TCollections.GetHashCode(MeasurementAlias);
      if(__isset.tagsList)
        hashcode = (hashcode * 397) + TCollections.GetHashCode(TagsList);
      if(__isset.attributesList)
        hashcode = (hashcode * 397) + TCollections.GetHashCode(AttributesList);
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSCreateAlignedTimeseriesReq(");
    sb.Append(", SessionId: ");
    sb.Append(SessionId);
    sb.Append(", PrefixPath: ");
    sb.Append(PrefixPath);
    sb.Append(", Measurements: ");
    sb.Append(Measurements);
    sb.Append(", DataTypes: ");
    sb.Append(DataTypes);
    sb.Append(", Encodings: ");
    sb.Append(Encodings);
    sb.Append(", Compressors: ");
    sb.Append(Compressors);
    if (MeasurementAlias != null && __isset.measurementAlias)
    {
      sb.Append(", MeasurementAlias: ");
      sb.Append(MeasurementAlias);
    }
    if (TagsList != null && __isset.tagsList)
    {
      sb.Append(", TagsList: ");
      sb.Append(TagsList);
    }
    if (AttributesList != null && __isset.attributesList)
    {
      sb.Append(", AttributesList: ");
      sb.Append(AttributesList);
    }
    sb.Append(")");
    return sb.ToString();
  }
}

