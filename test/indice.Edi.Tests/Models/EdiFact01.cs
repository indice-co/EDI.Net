using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models.EdiFact01
{
    [EdiElement, EdiPath("DTM/0")]
    public class DTM
    {
        [EdiValue("9(3)", Path = "DTM/0/0")]
        public int Code { get; set; }
        [EdiValue("X(12)", Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
        public DateTime Date { get; set; }
    }
    
    [EdiElement, EdiPath("DTM/0"), EdiCondition("ZZZ", Path = "DTM/0/0")]
    public class UTCOffset
    {
        [EdiValue("X(3)", Path = "DTM/0/0")]
        public string Code { get; set; }
        [EdiValue("9(2)", Path = "DTM/0/1")]
        public int Hours { get; set; }
    }

    [EdiSegment, EdiPath("NAD")]
    public class NAD
    {
        [EdiValue("X(3)", Path = "NAD/0/0")]
        public string PartyQualifier { get; set; }

        [EdiValue("X(35)", Path = "NAD/1/0")]
        public string PartyId { get; set; }

        [EdiValue("X(3)", Path = "NAD/1/2")]
        public string ResponsibleAgency { get; set; }
    }


    public class Interchange
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

        public Quote QuoteMessage { get; set; }
    }

    [EdiMessage]
    public class Quote
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

        [EdiValue("X(3)", Path = "BGM/0/0")]
        public string MessageName { get; set; }

        [EdiValue("X(35)", Path = "BGM/1/0")]
        public string DocumentNumber { get; set; }

        [EdiValue("X(3)", Path = "BGM/2/0", Mandatory = false)]
        public string MessageFunction { get; set; }

        [EdiValue("X(3)", Path = "BGM/3/0")]
        public string ResponseType { get; set; }

        [EdiCondition("137", Path = "DTM/0/0")]
        public DTM MessageDate { get; set; }

        [EdiCondition("163", Path = "DTM/0/0")]
        public DTM ProcessingStartDate { get; set; }

        [EdiCondition("164", Path = "DTM/0/0")]
        public DTM ProcessingEndDate { get; set; }

        public UTCOffset UTCOffset { get; set; }

        [EdiValue("X(3)", Path = "CUX/0/0")]
        public string CurrencyQualifier { get; set; }

        [EdiValue("X(3)", Path = "CUX/0/1")]
        public string ISOCurrency { get; set; }

        public List<NAD> NAD { get; set; }

        [EdiValue("X(3)", Path = "LOC/0/0")]
        public string LocationQualifier { get; set; }

        [EdiValue("X(3)", Path = "LOC/1/0")]
        public string LocationId { get; set; }

        [EdiValue("X(3)", Path = "LOC/1/2")]
        public string LocationResponsibleAgency { get; set; }

        public List<LinItem> ItemsOfLin { get; set; }

        [EdiValue("X(1)", Path = "UNS/0/0")]
        public char? UNS { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("LIN", SequenceEnd = "UNS")]
    public class LinItem
    {
        [EdiCondition("324", Path = "DTM/0/0")]
        public DTM SomeOtherDate { get; set; }

        public List<PRI> PREList { get; set; }
    }

    [EdiSegment, EdiPath("PRI")]
    public class PRI
    {

        [EdiValue("X(3)", Path = "PRI/0/0")]
        public string PRI_Text { get; set; }

        [EdiValue("9(3)", Path = "PRI/0/1")]
        public int PRI_Value { get; set; }
    }
}
