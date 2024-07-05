using System;
using System.Collections.Generic;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;


public class PPRHDR
{
    [EdiValue("X(14)", Path = "STX/1/0")]
    public string SenderCode { get; set; }
    [EdiValue("9(6)", Path = "STX/3/0", Format = "yyMMdd", Description = "TRDT - Date")]
    [EdiValue("9(6)", Path = "STX/3/1", Format = "HHmmss", Description = "TRDT - Time")]
    public DateTime TransmissionStamp { get; set; }
    public PPRHDR_Header Header { get; set; }
    public PPRHDR_Trailer Trailer { get; set; }
    public PPRHDR_Detail Detail { get; set; }
}


[EdiMessage, EdiCondition("PPRHDR", Path = "MHD/1")]

public class PPRHDR_Header
{
    [EdiValue("9(4)"), EdiPath("TYP")]
    public string TransactionCode { get; set; }
    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }
}

[EdiMessage, EdiCondition("PPRTLR", Path = "MHD/1")]
public class PPRHDR_Trailer
{
    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }
}

[EdiMessage, EdiCondition("PPRDET", Path = "MHD/1")]
public class PPRHDR_Detail
{

    [EdiValue("9(6)", Path = "SFR/0", Format = "yyMMdd", Description = "ReferencePeriod - Date")]
    public DateTime ReferencePeriodDate { get; set; }
    public List<PPRHDR_ProductRows> ProductRows { get; set; }
}

[EdiSegmentGroup("PDN", SequenceEnd = "SFL" /*"PLO", "SFX", "SFC"*/)]
public class PPRHDR_ProductRows

{
    [EdiValue("9(10)", Path = "PDN/0")]
    public int SequenceNumber { get; set; }
    [EdiValue("X(12)"), EdiPath("PDN/1/1")]
    public string SkuCode { get; set; }
    [EdiValue("X(6)"), EdiPath("PDN/2/1")]
    public string RefCode { get; set; }
    public List<PPRHDR_PosRows> PosRows { get; set; }
}

[EdiSegmentGroup("PLO",  SequenceEnd = "SFC"/*"SFP", "SFS" */)]
public class PPRHDR_PosRows
{
    [EdiValue("9(10)", Path = "PLO/0")]
    public int SequenceNumber { get; set; }
    [EdiValue("X(12)"), EdiPath("PLO/2/1")]
    public string PosCode { get; set; }
    [EdiValue("9(10)"), EdiPath("SFS/5")]
    public int STK { get; set; }
    [EdiValue("9(10)"), EdiPath("SFS/5")]
    public int SAL { get; set; }
}
