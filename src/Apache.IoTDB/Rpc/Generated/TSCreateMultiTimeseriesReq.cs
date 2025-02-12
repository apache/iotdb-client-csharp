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


public partial class TSCreateMultiTimeseriesReq : TBase
{
  private List<Dictionary<string, string>> _propsList;
  private List<Dictionary<string, string>> _tagsList;
  private List<Dictionary<string, string>> _attributesList;
  private List<string> _measurementAliasList;

  public long SessionId { get; set; }

  public List<string> Paths { get; set; }

  public List<int> DataTypes { get; set; }

  public List<int> Encodings { get; set; }

  public List<int> Compressors { get; set; }

  public List<Dictionary<string, string>> PropsList
  {
    get
    {
      return _propsList;
    }
    set
    {
      __isset.propsList = true;
      this._propsList = value;
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

  public List<string> MeasurementAliasList
  {
    get
    {
      return _measurementAliasList;
    }
    set
    {
      __isset.measurementAliasList = true;
      this._measurementAliasList = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool propsList;
    public bool tagsList;
    public bool attributesList;
    public bool measurementAliasList;
  }

  public TSCreateMultiTimeseriesReq()
  {
  }

  public TSCreateMultiTimeseriesReq(long sessionId, List<string> paths, List<int> dataTypes, List<int> encodings, List<int> compressors) : this()
  {
    this.SessionId = sessionId;
    this.Paths = paths;
    this.DataTypes = dataTypes;
    this.Encodings = encodings;
    this.Compressors = compressors;
  }

  public TSCreateMultiTimeseriesReq DeepCopy()
  {
    var tmp348 = new TSCreateMultiTimeseriesReq();
    tmp348.SessionId = this.SessionId;
    if((Paths != null))
    {
      tmp348.Paths = this.Paths.DeepCopy();
    }
    if((DataTypes != null))
    {
      tmp348.DataTypes = this.DataTypes.DeepCopy();
    }
    if((Encodings != null))
    {
      tmp348.Encodings = this.Encodings.DeepCopy();
    }
    if((Compressors != null))
    {
      tmp348.Compressors = this.Compressors.DeepCopy();
    }
    if((PropsList != null) && __isset.propsList)
    {
      tmp348.PropsList = this.PropsList.DeepCopy();
    }
    tmp348.__isset.propsList = this.__isset.propsList;
    if((TagsList != null) && __isset.tagsList)
    {
      tmp348.TagsList = this.TagsList.DeepCopy();
    }
    tmp348.__isset.tagsList = this.__isset.tagsList;
    if((AttributesList != null) && __isset.attributesList)
    {
      tmp348.AttributesList = this.AttributesList.DeepCopy();
    }
    tmp348.__isset.attributesList = this.__isset.attributesList;
    if((MeasurementAliasList != null) && __isset.measurementAliasList)
    {
      tmp348.MeasurementAliasList = this.MeasurementAliasList.DeepCopy();
    }
    tmp348.__isset.measurementAliasList = this.__isset.measurementAliasList;
    return tmp348;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_paths = false;
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
            if (field.Type == TType.List)
            {
              {
                TList _list349 = await iprot.ReadListBeginAsync(cancellationToken);
                Paths = new List<string>(_list349.Count);
                for(int _i350 = 0; _i350 < _list349.Count; ++_i350)
                {
                  string _elem351;
                  _elem351 = await iprot.ReadStringAsync(cancellationToken);
                  Paths.Add(_elem351);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_paths = true;
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
                TList _list352 = await iprot.ReadListBeginAsync(cancellationToken);
                DataTypes = new List<int>(_list352.Count);
                for(int _i353 = 0; _i353 < _list352.Count; ++_i353)
                {
                  int _elem354;
                  _elem354 = await iprot.ReadI32Async(cancellationToken);
                  DataTypes.Add(_elem354);
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
          case 4:
            if (field.Type == TType.List)
            {
              {
                TList _list355 = await iprot.ReadListBeginAsync(cancellationToken);
                Encodings = new List<int>(_list355.Count);
                for(int _i356 = 0; _i356 < _list355.Count; ++_i356)
                {
                  int _elem357;
                  _elem357 = await iprot.ReadI32Async(cancellationToken);
                  Encodings.Add(_elem357);
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
          case 5:
            if (field.Type == TType.List)
            {
              {
                TList _list358 = await iprot.ReadListBeginAsync(cancellationToken);
                Compressors = new List<int>(_list358.Count);
                for(int _i359 = 0; _i359 < _list358.Count; ++_i359)
                {
                  int _elem360;
                  _elem360 = await iprot.ReadI32Async(cancellationToken);
                  Compressors.Add(_elem360);
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
          case 6:
            if (field.Type == TType.List)
            {
              {
                TList _list361 = await iprot.ReadListBeginAsync(cancellationToken);
                PropsList = new List<Dictionary<string, string>>(_list361.Count);
                for(int _i362 = 0; _i362 < _list361.Count; ++_i362)
                {
                  Dictionary<string, string> _elem363;
                  {
                    TMap _map364 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem363 = new Dictionary<string, string>(_map364.Count);
                    for(int _i365 = 0; _i365 < _map364.Count; ++_i365)
                    {
                      string _key366;
                      string _val367;
                      _key366 = await iprot.ReadStringAsync(cancellationToken);
                      _val367 = await iprot.ReadStringAsync(cancellationToken);
                      _elem363[_key366] = _val367;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  PropsList.Add(_elem363);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
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
                TList _list368 = await iprot.ReadListBeginAsync(cancellationToken);
                TagsList = new List<Dictionary<string, string>>(_list368.Count);
                for(int _i369 = 0; _i369 < _list368.Count; ++_i369)
                {
                  Dictionary<string, string> _elem370;
                  {
                    TMap _map371 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem370 = new Dictionary<string, string>(_map371.Count);
                    for(int _i372 = 0; _i372 < _map371.Count; ++_i372)
                    {
                      string _key373;
                      string _val374;
                      _key373 = await iprot.ReadStringAsync(cancellationToken);
                      _val374 = await iprot.ReadStringAsync(cancellationToken);
                      _elem370[_key373] = _val374;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  TagsList.Add(_elem370);
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
                TList _list375 = await iprot.ReadListBeginAsync(cancellationToken);
                AttributesList = new List<Dictionary<string, string>>(_list375.Count);
                for(int _i376 = 0; _i376 < _list375.Count; ++_i376)
                {
                  Dictionary<string, string> _elem377;
                  {
                    TMap _map378 = await iprot.ReadMapBeginAsync(cancellationToken);
                    _elem377 = new Dictionary<string, string>(_map378.Count);
                    for(int _i379 = 0; _i379 < _map378.Count; ++_i379)
                    {
                      string _key380;
                      string _val381;
                      _key380 = await iprot.ReadStringAsync(cancellationToken);
                      _val381 = await iprot.ReadStringAsync(cancellationToken);
                      _elem377[_key380] = _val381;
                    }
                    await iprot.ReadMapEndAsync(cancellationToken);
                  }
                  AttributesList.Add(_elem377);
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
                TList _list382 = await iprot.ReadListBeginAsync(cancellationToken);
                MeasurementAliasList = new List<string>(_list382.Count);
                for(int _i383 = 0; _i383 < _list382.Count; ++_i383)
                {
                  string _elem384;
                  _elem384 = await iprot.ReadStringAsync(cancellationToken);
                  MeasurementAliasList.Add(_elem384);
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
      if (!isset_paths)
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
      var struc = new TStruct("TSCreateMultiTimeseriesReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Paths != null))
      {
        field.Name = "paths";
        field.Type = TType.List;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, Paths.Count), cancellationToken);
          foreach (string _iter385 in Paths)
          {
            await oprot.WriteStringAsync(_iter385, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((DataTypes != null))
      {
        field.Name = "dataTypes";
        field.Type = TType.List;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, DataTypes.Count), cancellationToken);
          foreach (int _iter386 in DataTypes)
          {
            await oprot.WriteI32Async(_iter386, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Encodings != null))
      {
        field.Name = "encodings";
        field.Type = TType.List;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, Encodings.Count), cancellationToken);
          foreach (int _iter387 in Encodings)
          {
            await oprot.WriteI32Async(_iter387, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Compressors != null))
      {
        field.Name = "compressors";
        field.Type = TType.List;
        field.ID = 5;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, Compressors.Count), cancellationToken);
          foreach (int _iter388 in Compressors)
          {
            await oprot.WriteI32Async(_iter388, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((PropsList != null) && __isset.propsList)
      {
        field.Name = "propsList";
        field.Type = TType.List;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, PropsList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter389 in PropsList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter389.Count), cancellationToken);
              foreach (string _iter390 in _iter389.Keys)
              {
                await oprot.WriteStringAsync(_iter390, cancellationToken);
                await oprot.WriteStringAsync(_iter389[_iter390], cancellationToken);
              }
              await oprot.WriteMapEndAsync(cancellationToken);
            }
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((TagsList != null) && __isset.tagsList)
      {
        field.Name = "tagsList";
        field.Type = TType.List;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, TagsList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter391 in TagsList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter391.Count), cancellationToken);
              foreach (string _iter392 in _iter391.Keys)
              {
                await oprot.WriteStringAsync(_iter392, cancellationToken);
                await oprot.WriteStringAsync(_iter391[_iter392], cancellationToken);
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
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.Map, AttributesList.Count), cancellationToken);
          foreach (Dictionary<string, string> _iter393 in AttributesList)
          {
            {
              await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter393.Count), cancellationToken);
              foreach (string _iter394 in _iter393.Keys)
              {
                await oprot.WriteStringAsync(_iter394, cancellationToken);
                await oprot.WriteStringAsync(_iter393[_iter394], cancellationToken);
              }
              await oprot.WriteMapEndAsync(cancellationToken);
            }
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((MeasurementAliasList != null) && __isset.measurementAliasList)
      {
        field.Name = "measurementAliasList";
        field.Type = TType.List;
        field.ID = 9;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, MeasurementAliasList.Count), cancellationToken);
          foreach (string _iter395 in MeasurementAliasList)
          {
            await oprot.WriteStringAsync(_iter395, cancellationToken);
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
    if (!(that is TSCreateMultiTimeseriesReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && TCollections.Equals(Paths, other.Paths)
      && TCollections.Equals(DataTypes, other.DataTypes)
      && TCollections.Equals(Encodings, other.Encodings)
      && TCollections.Equals(Compressors, other.Compressors)
      && ((__isset.propsList == other.__isset.propsList) && ((!__isset.propsList) || (TCollections.Equals(PropsList, other.PropsList))))
      && ((__isset.tagsList == other.__isset.tagsList) && ((!__isset.tagsList) || (TCollections.Equals(TagsList, other.TagsList))))
      && ((__isset.attributesList == other.__isset.attributesList) && ((!__isset.attributesList) || (TCollections.Equals(AttributesList, other.AttributesList))))
      && ((__isset.measurementAliasList == other.__isset.measurementAliasList) && ((!__isset.measurementAliasList) || (TCollections.Equals(MeasurementAliasList, other.MeasurementAliasList))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      if((Paths != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Paths);
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
      if((PropsList != null) && __isset.propsList)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(PropsList);
      }
      if((TagsList != null) && __isset.tagsList)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(TagsList);
      }
      if((AttributesList != null) && __isset.attributesList)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(AttributesList);
      }
      if((MeasurementAliasList != null) && __isset.measurementAliasList)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(MeasurementAliasList);
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSCreateMultiTimeseriesReq(");
    sb.Append(", SessionId: ");
    SessionId.ToString(sb);
    if((Paths != null))
    {
      sb.Append(", Paths: ");
      Paths.ToString(sb);
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
    if((PropsList != null) && __isset.propsList)
    {
      sb.Append(", PropsList: ");
      PropsList.ToString(sb);
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
    if((MeasurementAliasList != null) && __isset.measurementAliasList)
    {
      sb.Append(", MeasurementAliasList: ");
      MeasurementAliasList.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

