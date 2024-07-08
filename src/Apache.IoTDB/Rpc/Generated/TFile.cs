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


public partial class TFile : TBase
{

  public string FileName { get; set; }

  public byte[] File { get; set; }

  public TFile()
  {
  }

  public TFile(string fileName, byte[] file) : this()
  {
    this.FileName = fileName;
    this.File = file;
  }

  public TFile DeepCopy()
  {
    var tmp61 = new TFile();
    if((FileName != null))
    {
      tmp61.FileName = this.FileName;
    }
    if((File != null))
    {
      tmp61.File = this.File.ToArray();
    }
    return tmp61;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_fileName = false;
      bool isset_file = false;
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
              FileName = await iprot.ReadStringAsync(cancellationToken);
              isset_fileName = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              File = await iprot.ReadBinaryAsync(cancellationToken);
              isset_file = true;
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
      if (!isset_fileName)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      if (!isset_file)
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
      var struc = new TStruct("TFile");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((FileName != null))
      {
        field.Name = "fileName";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(FileName, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((File != null))
      {
        field.Name = "file";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBinaryAsync(File, cancellationToken);
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
    if (!(that is TFile other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(FileName, other.FileName)
      && TCollections.Equals(File, other.File);
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((FileName != null))
      {
        hashcode = (hashcode * 397) + FileName.GetHashCode();
      }
      if((File != null))
      {
        hashcode = (hashcode * 397) + File.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("TFile(");
    if((FileName != null))
    {
      sb.Append(", FileName: ");
      FileName.ToString(sb);
    }
    if((File != null))
    {
      sb.Append(", File: ");
      File.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

