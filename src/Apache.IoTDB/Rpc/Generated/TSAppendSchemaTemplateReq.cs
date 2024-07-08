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


public partial class TSAppendSchemaTemplateReq : TBase
{

  public long SessionId { get; set; }

  public string Name { get; set; }

  public bool IsAligned { get; set; }

  public List<string> Measurements { get; set; }

  public List<int> DataTypes { get; set; }

  public List<int> Encodings { get; set; }

  public List<int> Compressors { get; set; }

  public TSAppendSchemaTemplateReq()
  {
  }

  public TSAppendSchemaTemplateReq(long sessionId, string name, bool isAligned, List<string> measurements, List<int> dataTypes, List<int> encodings, List<int> compressors) : this()
  {
    this.SessionId = sessionId;
    this.Name = name;
    this.IsAligned = isAligned;
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
      bool isset_name = false;
      bool isset_isAligned = false;
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
              Name = await iprot.ReadStringAsync(cancellationToken);
              isset_name = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Bool)
            {
              IsAligned = await iprot.ReadBoolAsync(cancellationToken);
              isset_isAligned = true;
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
                TList _list369 = await iprot.ReadListBeginAsync(cancellationToken);
                Measurements = new List<string>(_list369.Count);
                for(int _i370 = 0; _i370 < _list369.Count; ++_i370)
                {
                  string _elem371;
                  _elem371 = await iprot.ReadStringAsync(cancellationToken);
                  Measurements.Add(_elem371);
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
          case 5:
            if (field.Type == TType.List)
            {
              {
                TList _list372 = await iprot.ReadListBeginAsync(cancellationToken);
                DataTypes = new List<int>(_list372.Count);
                for(int _i373 = 0; _i373 < _list372.Count; ++_i373)
                {
                  int _elem374;
                  _elem374 = await iprot.ReadI32Async(cancellationToken);
                  DataTypes.Add(_elem374);
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
          case 6:
            if (field.Type == TType.List)
            {
              {
                TList _list375 = await iprot.ReadListBeginAsync(cancellationToken);
                Encodings = new List<int>(_list375.Count);
                for(int _i376 = 0; _i376 < _list375.Count; ++_i376)
                {
                  int _elem377;
                  _elem377 = await iprot.ReadI32Async(cancellationToken);
                  Encodings.Add(_elem377);
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
          case 7:
            if (field.Type == TType.List)
            {
              {
                TList _list378 = await iprot.ReadListBeginAsync(cancellationToken);
                Compressors = new List<int>(_list378.Count);
                for(int _i379 = 0; _i379 < _list378.Count; ++_i379)
                {
                  int _elem380;
                  _elem380 = await iprot.ReadI32Async(cancellationToken);
                  Compressors.Add(_elem380);
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
      if (!isset_name)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_isAligned)
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
      var struc = new TStruct("TSAppendSchemaTemplateReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteI64Async(SessionId, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Name != null))
      {
        field.Name = "name";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Name, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      field.Name = "isAligned";
      field.Type = TType.Bool;
      field.ID = 3;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteBoolAsync(IsAligned, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if((Measurements != null))
      {
        field.Name = "measurements";
        field.Type = TType.List;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, Measurements.Count), cancellationToken);
          foreach (string _iter381 in Measurements)
          {
            await oprot.WriteStringAsync(_iter381, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((DataTypes != null))
      {
        field.Name = "dataTypes";
        field.Type = TType.List;
        field.ID = 5;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, DataTypes.Count), cancellationToken);
          foreach (int _iter382 in DataTypes)
          {
            await oprot.WriteI32Async(_iter382, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Encodings != null))
      {
        field.Name = "encodings";
        field.Type = TType.List;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, Encodings.Count), cancellationToken);
          foreach (int _iter383 in Encodings)
          {
            await oprot.WriteI32Async(_iter383, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Compressors != null))
      {
        field.Name = "compressors";
        field.Type = TType.List;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I32, Compressors.Count), cancellationToken);
          foreach (int _iter384 in Compressors)
          {
            await oprot.WriteI32Async(_iter384, cancellationToken);
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
    if (!(that is TSAppendSchemaTemplateReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(SessionId, other.SessionId)
      && System.Object.Equals(Name, other.Name)
      && System.Object.Equals(IsAligned, other.IsAligned)
      && TCollections.Equals(Measurements, other.Measurements)
      && TCollections.Equals(DataTypes, other.DataTypes)
      && TCollections.Equals(Encodings, other.Encodings)
      && TCollections.Equals(Compressors, other.Compressors);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + SessionId.GetHashCode();
      if((Name != null))
      {
        hashcode = (hashcode * 397) + Name.GetHashCode();
      }
      hashcode = (hashcode * 397) + IsAligned.GetHashCode();
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
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSAppendSchemaTemplateReq(");
    sb.Append(", SessionId: ");
    SessionId.ToString(sb);
    if((Name != null))
    {
      sb.Append(", Name: ");
      Name.ToString(sb);
    }
    sb.Append(", IsAligned: ");
    IsAligned.ToString(sb);
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
    sb.Append(')');
    return sb.ToString();
  }
}

