using System;
using System.Collections.Generic;
using indice.Edi.Serialization;
using indice.Edi.Tests.Models.EdiFact01;

namespace indice.Edi.Tests.Models;

class EdiFact01_Segments
{
    public class Interchange
    {
        public UNB Header { get; set; }
        public UNZ Footer { get; set; }
        public Quote2 Message { get; set; }
    }

    public class Interchange_Multi_Message
    {
        public UNB Header { get; set; }
        public UNZ Footer { get; set; }
        public List<Quote2> Message { get; set; }
    }

    [EdiMessage]
    public class Quote2
    {
        public UNH_Segment Header { get; set; }

        public BGM_Segment BGM { get; set; }

        public DTM_Segment DTM { get; set; }

        public CUX_Segment CUX { get; set; }

        public List<EdiFact01.NAD> NAD { get; set; }

        public LOC_Segment LOC { get; set; }

        public List<LineItem> Lines { get; set; }

        public UNS_Segment UNS { get; set; }

        public UNT_Segment Trailer { get; set; }
    }

    public class Interchange_Segments_Only
    {
        public UNB UNB { get; set; }
        public UNZ UNZ { get; set; }
    }

    [EdiMessage]
    public class Quote : Interchange_Segments_Only
    {
        public UNH_Segment UNH { get; set; }

        public BGM_Segment BGM { get; set; }

        public DTM_Segment DTM { get; set; }

        public CUX_Segment CUX { get; set; }

        public List<EdiFact01.NAD> NAD { get; set; }

        public LOC_Segment LOC { get; set; }

        public List<LineItem> Lines { get; set; }

        public UNS_Segment UNS { get; set; }

        public UNT_Segment UNT { get; set; }
    }

    [EdiSegment, EdiPath("UNB")]
    public class UNB
    {
        [EdiValue("X(4)", Mandatory = true, Path = "UNB/0")]
        public string SyntaxIdentifier { get; set; }

        [EdiValue("9(1)", Path = "UNB/0/1", Mandatory = true)]
        public int SyntaxVersion { get; set; }

        [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
        public string SenderId { get; set; }
        [EdiValue("X(4)", Path = "UNB/1/1", Mandatory = true)]
        public string PartnerIDCodeQualifier { get; set; }
        [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
        public string ReverseRoutingAddress { get; set; }

        [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
        public string RecipientId { get; set; }

        [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = true)]
        public string ParterIDCode { get; set; }
        [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
        public string RoutingAddress { get; set; }

        [EdiValue("9(6)", Path = "UNB/3/0", Format = "ddMMyy", Description = "Date of Preparation")]
        [EdiValue("9(4)", Path = "UNB/3/1", Format = "HHmm", Description = "Time or Prep")]
        public DateTime DateOfPreparation { get; set; }

        [EdiValue("X(14)", Path = "UNB/4", Mandatory = true)]
        public string ControlRef { get; set; }

        [EdiValue("9(1)", Path = "UNB/8", Mandatory = false)]
        public int AckRequest { get; set; }
    }

    [EdiSegment, EdiPath("UNH")]
    public class UNH_Segment
    {
        [EdiValue("X(14)", Path = "UNH/0/0")]
        public string MessageRef { get; set; }


        [EdiValue("X(6)", Path = "UNH/1/0")]
        public string MessageType { get; set; }

        [EdiValue("X(3)", Path = "UNH/1/1")]
        public string Version { get; set; }

        [EdiValue("X(3)", Path = "UNH/1/2")]
        public string ReleaseNumber { get; set; }

        [EdiValue("X(2)", Path = "UNH/1/3")]
        public string ControllingAgency { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/4")]
        public string AssociationAssignedCode { get; set; }

        [EdiValue("X(35)", Path = "UNH/2/0")]
        public string CommonAccessRef { get; set; }
    }

    [EdiSegment, EdiPath("UNZ")]
    public class UNZ
    {
        [EdiValue("X(1)", Path = "UNZ/0")]
        public int TrailerControlCount { get; set; }

        [EdiValue("X(14)", Path = "UNZ/1")]
        public string TrailerControlReference { get; set; }
    }

    [EdiSegment, EdiPath("BGM")]
    public class BGM_Segment
    {
        [EdiValue("X(3)", Path = "BGM/0/0")]
        public string MessageName { get; set; }

        [EdiValue("X(35)", Path = "BGM/1/0")]
        public string DocumentNumber { get; set; }

        [EdiValue("X(3)", Path = "BGM/2/0", Mandatory = false)]
        public string MessageFunction { get; set; }

        [EdiValue("X(3)", Path = "BGM/3/0")]
        public string ResponseType { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DTM_Segment
    {
        [EdiCondition("137", Path = "DTM/0/0")]
        public EdiFact01.DTM MessageDate { get; set; }

        [EdiCondition("163", Path = "DTM/0/0")]
        public EdiFact01.DTM ProcessingStartDate { get; set; }

        [EdiCondition("164", Path = "DTM/0/0")]
        public EdiFact01.DTM ProcessingEndDate { get; set; }

        public UTCOffset UTCOffset { get; set; }
    }

    [EdiSegment, EdiPath("CUX")]
    public class CUX_Segment
    {
        [EdiValue("X(3)", Path = "CUX/0/0")]
        public string CurrencyQualifier { get; set; }

        [EdiValue("X(3)", Path = "CUX/0/1")]
        public string ISOCurrency { get; set; }
    }

    [EdiSegment, EdiPath("LOC")]
    public class LOC_Segment
    {
        [EdiValue("X(3)", Path = "LOC/0/0")]
        public string LocationQualifier { get; set; }

        [EdiValue("X(3)", Path = "LOC/1/0")]
        public string LocationId { get; set; }

        [EdiValue("X(3)", Path = "LOC/1/2")]
        public string LocationResponsibleAgency { get; set; }
    }

    [EdiSegment, EdiPath("UNS")]
    public class UNS_Segment
    {
        [EdiValue("X(1)", Path = "UNS/0/0")]
        public char? UNS { get; set; }
    }

    [EdiSegment, EdiPath("UNT")]
    public class UNT_Segment
    {
        [EdiValue("X(1)", Path = "UNT/0")]
        public int TrailerMessageSegmentsCount { get; set; }

        [EdiValue("X(14)", Path = "UNT/1")]
        public string TrailerMessageReference { get; set; }
    }
    
}
