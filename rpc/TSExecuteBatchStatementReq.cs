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
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;


#if !SILVERLIGHT
[Serializable]
#endif
public partial class TSExecuteBatchStatementReq : TBase
{

  public long SessionId { get; set; }

  public List<string> Statements { get; set; }

  public TSExecuteBatchStatementReq() {
  }

  public TSExecuteBatchStatementReq(long sessionId, List<string> statements) : this() {
    this.SessionId = sessionId;
    this.Statements = statements;
  }

  public void Read (TProtocol iprot)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_sessionId = false;
      bool isset_statements = false;
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I64) {
              SessionId = iprot.ReadI64();
              isset_sessionId = true;
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.List) {
              {
                Statements = new List<string>();
                TList _list43 = iprot.ReadListBegin();
                for( int _i44 = 0; _i44 < _list43.Count; ++_i44)
                {
                  string _elem45;
                  _elem45 = iprot.ReadString();
                  Statements.Add(_elem45);
                }
                iprot.ReadListEnd();
              }
              isset_statements = true;
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
      if (!isset_sessionId)
        throw new TProtocolException(TProtocolException.INVALID_DATA, "required field SessionId not set");
      if (!isset_statements)
        throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Statements not set");
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public void Write(TProtocol oprot) {
    oprot.IncrementRecursionDepth();
    try
    {
      TStruct struc = new TStruct("TSExecuteBatchStatementReq");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      field.Name = "sessionId";
      field.Type = TType.I64;
      field.ID = 1;
      oprot.WriteFieldBegin(field);
      oprot.WriteI64(SessionId);
      oprot.WriteFieldEnd();
      if (Statements == null)
        throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Statements not set");
      field.Name = "statements";
      field.Type = TType.List;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      {
        oprot.WriteListBegin(new TList(TType.String, Statements.Count));
        foreach (string _iter46 in Statements)
        {
          oprot.WriteString(_iter46);
        }
        oprot.WriteListEnd();
      }
      oprot.WriteFieldEnd();
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override string ToString() {
    StringBuilder __sb = new StringBuilder("TSExecuteBatchStatementReq(");
    __sb.Append(", SessionId: ");
    __sb.Append(SessionId);
    __sb.Append(", Statements: ");
    __sb.Append(Statements);
    __sb.Append(")");
    return __sb.ToString();
  }

}

