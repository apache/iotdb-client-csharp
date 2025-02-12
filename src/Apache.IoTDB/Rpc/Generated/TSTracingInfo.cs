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


public partial class TSTracingInfo : TBase
{
  private int _seriesPathNum;
  private int _seqFileNum;
  private int _unSeqFileNum;
  private int _sequenceChunkNum;
  private long _sequenceChunkPointNum;
  private int _unsequenceChunkNum;
  private long _unsequenceChunkPointNum;
  private int _totalPageNum;
  private int _overlappedPageNum;

  public List<string> ActivityList { get; set; }

  public List<long> ElapsedTimeList { get; set; }

  public int SeriesPathNum
  {
    get
    {
      return _seriesPathNum;
    }
    set
    {
      __isset.seriesPathNum = true;
      this._seriesPathNum = value;
    }
  }

  public int SeqFileNum
  {
    get
    {
      return _seqFileNum;
    }
    set
    {
      __isset.seqFileNum = true;
      this._seqFileNum = value;
    }
  }

  public int UnSeqFileNum
  {
    get
    {
      return _unSeqFileNum;
    }
    set
    {
      __isset.unSeqFileNum = true;
      this._unSeqFileNum = value;
    }
  }

  public int SequenceChunkNum
  {
    get
    {
      return _sequenceChunkNum;
    }
    set
    {
      __isset.sequenceChunkNum = true;
      this._sequenceChunkNum = value;
    }
  }

  public long SequenceChunkPointNum
  {
    get
    {
      return _sequenceChunkPointNum;
    }
    set
    {
      __isset.sequenceChunkPointNum = true;
      this._sequenceChunkPointNum = value;
    }
  }

  public int UnsequenceChunkNum
  {
    get
    {
      return _unsequenceChunkNum;
    }
    set
    {
      __isset.unsequenceChunkNum = true;
      this._unsequenceChunkNum = value;
    }
  }

  public long UnsequenceChunkPointNum
  {
    get
    {
      return _unsequenceChunkPointNum;
    }
    set
    {
      __isset.unsequenceChunkPointNum = true;
      this._unsequenceChunkPointNum = value;
    }
  }

  public int TotalPageNum
  {
    get
    {
      return _totalPageNum;
    }
    set
    {
      __isset.totalPageNum = true;
      this._totalPageNum = value;
    }
  }

  public int OverlappedPageNum
  {
    get
    {
      return _overlappedPageNum;
    }
    set
    {
      __isset.overlappedPageNum = true;
      this._overlappedPageNum = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool seriesPathNum;
    public bool seqFileNum;
    public bool unSeqFileNum;
    public bool sequenceChunkNum;
    public bool sequenceChunkPointNum;
    public bool unsequenceChunkNum;
    public bool unsequenceChunkPointNum;
    public bool totalPageNum;
    public bool overlappedPageNum;
  }

  public TSTracingInfo()
  {
  }

  public TSTracingInfo(List<string> activityList, List<long> elapsedTimeList) : this()
  {
    this.ActivityList = activityList;
    this.ElapsedTimeList = elapsedTimeList;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_activityList = false;
      bool isset_elapsedTimeList = false;
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
            if (field.Type == TType.List)
            {
              {
                TList _list18 = await iprot.ReadListBeginAsync(cancellationToken);
                ActivityList = new List<string>(_list18.Count);
                for(int _i19 = 0; _i19 < _list18.Count; ++_i19)
                {
                  string _elem20;
                  _elem20 = await iprot.ReadStringAsync(cancellationToken);
                  ActivityList.Add(_elem20);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_activityList = true;
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
                TList _list21 = await iprot.ReadListBeginAsync(cancellationToken);
                ElapsedTimeList = new List<long>(_list21.Count);
                for(int _i22 = 0; _i22 < _list21.Count; ++_i22)
                {
                  long _elem23;
                  _elem23 = await iprot.ReadI64Async(cancellationToken);
                  ElapsedTimeList.Add(_elem23);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_elapsedTimeList = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.I32)
            {
              SeriesPathNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.I32)
            {
              SeqFileNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.I32)
            {
              UnSeqFileNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.I32)
            {
              SequenceChunkNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 7:
            if (field.Type == TType.I64)
            {
              SequenceChunkPointNum = await iprot.ReadI64Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 8:
            if (field.Type == TType.I32)
            {
              UnsequenceChunkNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 9:
            if (field.Type == TType.I64)
            {
              UnsequenceChunkPointNum = await iprot.ReadI64Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 10:
            if (field.Type == TType.I32)
            {
              TotalPageNum = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 11:
            if (field.Type == TType.I32)
            {
              OverlappedPageNum = await iprot.ReadI32Async(cancellationToken);
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
      if (!isset_activityList)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_elapsedTimeList)
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
      var struc = new TStruct("TSTracingInfo");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((ActivityList != null))
      {
        field.Name = "activityList";
        field.Type = TType.List;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, ActivityList.Count), cancellationToken);
          foreach (string _iter24 in ActivityList)
          {
            await oprot.WriteStringAsync(_iter24, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((ElapsedTimeList != null))
      {
        field.Name = "elapsedTimeList";
        field.Type = TType.List;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.I64, ElapsedTimeList.Count), cancellationToken);
          foreach (long _iter25 in ElapsedTimeList)
          {
            await oprot.WriteI64Async(_iter25, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
        }
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.seriesPathNum)
      {
        field.Name = "seriesPathNum";
        field.Type = TType.I32;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(SeriesPathNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.seqFileNum)
      {
        field.Name = "seqFileNum";
        field.Type = TType.I32;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(SeqFileNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.unSeqFileNum)
      {
        field.Name = "unSeqFileNum";
        field.Type = TType.I32;
        field.ID = 5;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(UnSeqFileNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.sequenceChunkNum)
      {
        field.Name = "sequenceChunkNum";
        field.Type = TType.I32;
        field.ID = 6;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(SequenceChunkNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.sequenceChunkPointNum)
      {
        field.Name = "sequenceChunkPointNum";
        field.Type = TType.I64;
        field.ID = 7;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(SequenceChunkPointNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.unsequenceChunkNum)
      {
        field.Name = "unsequenceChunkNum";
        field.Type = TType.I32;
        field.ID = 8;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(UnsequenceChunkNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.unsequenceChunkPointNum)
      {
        field.Name = "unsequenceChunkPointNum";
        field.Type = TType.I64;
        field.ID = 9;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(UnsequenceChunkPointNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.totalPageNum)
      {
        field.Name = "totalPageNum";
        field.Type = TType.I32;
        field.ID = 10;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(TotalPageNum, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.overlappedPageNum)
      {
        field.Name = "overlappedPageNum";
        field.Type = TType.I32;
        field.ID = 11;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(OverlappedPageNum, cancellationToken);
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
    if (!(that is TSTracingInfo other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return TCollections.Equals(ActivityList, other.ActivityList)
      && TCollections.Equals(ElapsedTimeList, other.ElapsedTimeList)
      && ((__isset.seriesPathNum == other.__isset.seriesPathNum) && ((!__isset.seriesPathNum) || (System.Object.Equals(SeriesPathNum, other.SeriesPathNum))))
      && ((__isset.seqFileNum == other.__isset.seqFileNum) && ((!__isset.seqFileNum) || (System.Object.Equals(SeqFileNum, other.SeqFileNum))))
      && ((__isset.unSeqFileNum == other.__isset.unSeqFileNum) && ((!__isset.unSeqFileNum) || (System.Object.Equals(UnSeqFileNum, other.UnSeqFileNum))))
      && ((__isset.sequenceChunkNum == other.__isset.sequenceChunkNum) && ((!__isset.sequenceChunkNum) || (System.Object.Equals(SequenceChunkNum, other.SequenceChunkNum))))
      && ((__isset.sequenceChunkPointNum == other.__isset.sequenceChunkPointNum) && ((!__isset.sequenceChunkPointNum) || (System.Object.Equals(SequenceChunkPointNum, other.SequenceChunkPointNum))))
      && ((__isset.unsequenceChunkNum == other.__isset.unsequenceChunkNum) && ((!__isset.unsequenceChunkNum) || (System.Object.Equals(UnsequenceChunkNum, other.UnsequenceChunkNum))))
      && ((__isset.unsequenceChunkPointNum == other.__isset.unsequenceChunkPointNum) && ((!__isset.unsequenceChunkPointNum) || (System.Object.Equals(UnsequenceChunkPointNum, other.UnsequenceChunkPointNum))))
      && ((__isset.totalPageNum == other.__isset.totalPageNum) && ((!__isset.totalPageNum) || (System.Object.Equals(TotalPageNum, other.TotalPageNum))))
      && ((__isset.overlappedPageNum == other.__isset.overlappedPageNum) && ((!__isset.overlappedPageNum) || (System.Object.Equals(OverlappedPageNum, other.OverlappedPageNum))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((ActivityList != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(ActivityList);
      }
      if((ElapsedTimeList != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(ElapsedTimeList);
      }
      if(__isset.seriesPathNum)
      {
        hashcode = (hashcode * 397) + SeriesPathNum.GetHashCode();
      }
      if(__isset.seqFileNum)
      {
        hashcode = (hashcode * 397) + SeqFileNum.GetHashCode();
      }
      if(__isset.unSeqFileNum)
      {
        hashcode = (hashcode * 397) + UnSeqFileNum.GetHashCode();
      }
      if(__isset.sequenceChunkNum)
      {
        hashcode = (hashcode * 397) + SequenceChunkNum.GetHashCode();
      }
      if(__isset.sequenceChunkPointNum)
      {
        hashcode = (hashcode * 397) + SequenceChunkPointNum.GetHashCode();
      }
      if(__isset.unsequenceChunkNum)
      {
        hashcode = (hashcode * 397) + UnsequenceChunkNum.GetHashCode();
      }
      if(__isset.unsequenceChunkPointNum)
      {
        hashcode = (hashcode * 397) + UnsequenceChunkPointNum.GetHashCode();
      }
      if(__isset.totalPageNum)
      {
        hashcode = (hashcode * 397) + TotalPageNum.GetHashCode();
      }
      if(__isset.overlappedPageNum)
      {
        hashcode = (hashcode * 397) + OverlappedPageNum.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TSTracingInfo(");
    if((ActivityList != null))
    {
      sb.Append(", ActivityList: ");
      ActivityList.ToString(sb);
    }
    if((ElapsedTimeList != null))
    {
      sb.Append(", ElapsedTimeList: ");
      ElapsedTimeList.ToString(sb);
    }
    if(__isset.seriesPathNum)
    {
      sb.Append(", SeriesPathNum: ");
      SeriesPathNum.ToString(sb);
    }
    if(__isset.seqFileNum)
    {
      sb.Append(", SeqFileNum: ");
      SeqFileNum.ToString(sb);
    }
    if(__isset.unSeqFileNum)
    {
      sb.Append(", UnSeqFileNum: ");
      UnSeqFileNum.ToString(sb);
    }
    if(__isset.sequenceChunkNum)
    {
      sb.Append(", SequenceChunkNum: ");
      SequenceChunkNum.ToString(sb);
    }
    if(__isset.sequenceChunkPointNum)
    {
      sb.Append(", SequenceChunkPointNum: ");
      SequenceChunkPointNum.ToString(sb);
    }
    if(__isset.unsequenceChunkNum)
    {
      sb.Append(", UnsequenceChunkNum: ");
      UnsequenceChunkNum.ToString(sb);
    }
    if(__isset.unsequenceChunkPointNum)
    {
      sb.Append(", UnsequenceChunkPointNum: ");
      UnsequenceChunkPointNum.ToString(sb);
    }
    if(__isset.totalPageNum)
    {
      sb.Append(", TotalPageNum: ");
      TotalPageNum.ToString(sb);
    }
    if(__isset.overlappedPageNum)
    {
      sb.Append(", OverlappedPageNum: ");
      OverlappedPageNum.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

