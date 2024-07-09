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


public partial class TShowTTLReq : TBase
{

  public List<string> PathPattern { get; set; }

  public TShowTTLReq()
  {
  }

  public TShowTTLReq(List<string> pathPattern) : this()
  {
    this.PathPattern = pathPattern;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_pathPattern = false;
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
                TList _list40 = await iprot.ReadListBeginAsync(cancellationToken);
                PathPattern = new List<string>(_list40.Count);
                for(int _i41 = 0; _i41 < _list40.Count; ++_i41)
                {
                  string _elem42;
                  _elem42 = await iprot.ReadStringAsync(cancellationToken);
                  PathPattern.Add(_elem42);
                }
                await iprot.ReadListEndAsync(cancellationToken);
              }
              isset_pathPattern = true;
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
      if (!isset_pathPattern)
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
      var struc = new TStruct("TShowTTLReq");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((PathPattern != null))
      {
        field.Name = "pathPattern";
        field.Type = TType.List;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteListBeginAsync(new TList(TType.String, PathPattern.Count), cancellationToken);
          foreach (string _iter43 in PathPattern)
          {
            await oprot.WriteStringAsync(_iter43, cancellationToken);
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
    if (!(that is TShowTTLReq other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return TCollections.Equals(PathPattern, other.PathPattern);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((PathPattern != null))
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(PathPattern);
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TShowTTLReq(");
    if((PathPattern != null))
    {
      sb.Append(", PathPattern: ");
      PathPattern.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

