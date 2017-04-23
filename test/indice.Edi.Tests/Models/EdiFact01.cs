using indice.Edi.Serialization;
using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using indice.Edi.FormatSpec;

namespace indice.Edi.Tests.Models.EdiFact01
{
    public struct DTMPeriod
    {
        public readonly DateTime From;
        public readonly DateTime To;

        public DTMPeriod(DateTime from, DateTime to) {
            From = from;
            To = to;
        }

        public static DTMPeriod Parse(string text) {
            var textFrom = text?.Substring(0, 12);
            var textTo = text?.Substring(12, 12);
            return new DTMPeriod(
                    textFrom.ParseEdiDate("yyyyMMddHHmm"),
                    textTo.ParseEdiDate("yyyyMMddHHmm")
                );
        }

        public override string ToString() {
            return $"{From:yyyyMMddHHmm}{To:yyyyMMddHHmm}";
        }

        public static implicit operator string(DTMPeriod value) {
            return value.ToString();
        }

        // With a cast operator from string --> MyClass or MyStruct 
        // we can convert any edi component value to our custom implementation.
        public static explicit operator DTMPeriod(string value) {
            return DTMPeriod.Parse(value);
        }
    }

    [EdiElement, EdiPath("DTM/0")]
    public class DTM
    {
        [EdiValue("n..3", FormatterType.EdifactSpec, Path = "DTM/0/0")]
        public int ID { get; set; }
        [EdiValue("an..12", FormatterType.EdifactSpec, Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
        public DateTime DateTime { get; set; }
        [EdiValue("n3", FormatterType.EdifactSpec, Path = "DTM/0/2")]
        public int Code { get; set; }

        public override string ToString() {
            return DateTime.ToString();
        }
    }

    [EdiElement, EdiPath("DTM/0"), EdiCondition("ZZZ", Path = "DTM/0/0")]
    public class UTCOffset
    {
        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "DTM/0/0")]
        public int? ID { get; set; }
        [EdiValue("n..1", FormatterType.EdifactSpec, Path = "DTM/0/1")]
        public int Hours { get; set; }
        [EdiValue("n3", FormatterType.EdifactSpec, Path = "DTM/0/2")]
        public int Code { get; set; }

        public override string ToString() {
            return Hours.ToString();
        }
    }

    [EdiElement, EdiPath("DTM/0"), EdiCondition("324", Path = "DTM/0/0")]
    public class Period
    {
        [EdiValue("n..3", FormatterType.EdifactSpec, Path = "DTM/0/0")]
        public int ID { get; set; }

        [EdiValue("n..24", FormatterType.EdifactSpec, Path = "DTM/0/1")]
        public DTMPeriod Date { get; set; }

        [EdiValue("n3", FormatterType.EdifactSpec, Path = "DTM/0/2")]
        public int Code { get; set; }

        public override string ToString() {
            return $"{Date.From} | {Date.To}";
        }
    }

    [EdiElement, EdiPath("LIN/2")]
    public class ItemNumber
    {
        [EdiValue("an..1", FormatterType.EdifactSpec, Path = "LIN/2/0")]
        public string Number { get; set; }

        [EdiValue("n..3", FormatterType.EdifactSpec, Path = "LIN/2/1")]
        public string Type { get; set; }

        [EdiValue("n..3", FormatterType.EdifactSpec, Path = "LIN/2/2")]
        public string CodeListQualifier { get; set; }

        [EdiValue("n..3", FormatterType.EdifactSpec, Path = "LIN/2/3")]
        public string CodeListResponsibleAgency { get; set; }

        public override string ToString() {
            return $"{Number} {Type} {CodeListQualifier}";
        }
    }

    [EdiSegment, EdiPath("NAD")]
    public class NAD
    {
        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "NAD/0/0")]
        public string PartyQualifier { get; set; }

        [EdiValue("an..35", FormatterType.EdifactSpec, Path = "NAD/1/0")]
        public string PartyId { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "NAD/1/2")]
        public string ResponsibleAgency { get; set; }
    }


    public class Interchange
    {
        [EdiValue("an..4", FormatterType.EdifactSpec, Mandatory = true, Path = "UNB/0")]
        public string SyntaxIdentifier { get; set; }

        [EdiValue("n..1", FormatterType.EdifactSpec, Path = "UNB/0/1", Mandatory = true)]
        public int SyntaxVersion { get; set; }

        [EdiValue("an..35", FormatterType.EdifactSpec, Path = "UNB/1/0", Mandatory = true)]
        public string SenderId { get; set; }
        [EdiValue("an..4", FormatterType.EdifactSpec, Path = "UNB/1/1", Mandatory = true)]
        public string PartnerIDCodeQualifier { get; set; }
        [EdiValue("an..14", FormatterType.EdifactSpec, Path = "UNB/1/2", Mandatory = false)]
        public string ReverseRoutingAddress { get; set; }

        [EdiValue("an..35", FormatterType.EdifactSpec, Path = "UNB/2/0", Mandatory = true)]
        public string RecipientId { get; set; }

        [EdiValue("an..4", FormatterType.EdifactSpec, Path = "UNB/2/1", Mandatory = true)]
        public string ParterIDCode { get; set; }
        [EdiValue("an..14", FormatterType.EdifactSpec, Path = "UNB/2/2", Mandatory = false)]
        public string RoutingAddress { get; set; }

        [EdiValue("n..6", FormatterType.EdifactSpec, Path = "UNB/3/0", Format = "ddMMyy", Description = "Date of Preparation")]
        [EdiValue("n..4", FormatterType.EdifactSpec, Path = "UNB/3/1", Format = "HHmm", Description = "Time or Prep")]
        public DateTime DateOfPreparation { get; set; }

        [EdiValue("an..14", FormatterType.EdifactSpec, Path = "UNB/4", Mandatory = true)]
        public string ControlRef { get; set; }

        [EdiValue("n..1", FormatterType.EdifactSpec, Path = "UNB/8", Mandatory = false)]
        public int AckRequest { get; set; }

        public Quote QuoteMessage { get; set; }

        [EdiValue("an..1", FormatterType.EdifactSpec, Path = "UNZ/0")]
        public int TrailerControlCount { get; set; }

        [EdiValue("an..14", FormatterType.EdifactSpec, Path = "UNZ/1")]
        public string TrailerControlReference { get; set; }
    }

    [EdiMessage]
    public class Quote
    {

        [EdiValue("an..14", FormatterType.EdifactSpec, Path = "UNH/0/0")]
        public string MessageRef { get; set; }


        [EdiValue("an..6", FormatterType.EdifactSpec, Path = "UNH/1/0")]
        public string MessageType { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "UNH/1/1")]
        public string Version { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "UNH/1/2")]
        public string ReleaseNumber { get; set; }

        [EdiValue("an..2", FormatterType.EdifactSpec, Path = "UNH/1/3")]
        public string ControllingAgency { get; set; }

        [EdiValue("an..6", FormatterType.EdifactSpec, Path = "UNH/1/4")]
        public string AssociationAssignedCode { get; set; }

        [EdiValue("an..35", FormatterType.EdifactSpec, Path = "UNH/2/0")]
        public string CommonAccessRef { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "BGM/0/0")]
        public string MessageName { get; set; }

        [EdiValue("an..35", FormatterType.EdifactSpec, Path = "BGM/1/0")]
        public string DocumentNumber { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "BGM/2/0", Mandatory = false)]
        public string MessageFunction { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "BGM/3/0")]
        public string ResponseType { get; set; }

        [EdiCondition("137", Path = "DTM/0/0")]
        public DTM MessageDate { get; set; }

        [EdiCondition("163", Path = "DTM/0/0")]
        public DTM ProcessingStartDate { get; set; }

        [EdiCondition("164", Path = "DTM/0/0")]
        public DTM ProcessingEndDate { get; set; }

        public UTCOffset UTCOffset { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "CUX/0/0")]
        public string CurrencyQualifier { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "CUX/0/1")]
        public string ISOCurrency { get; set; }

        public List<NAD> NAD { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "LOC/0/0")]
        public string LocationQualifier { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "LOC/1/0")]
        public string LocationId { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "LOC/1/2")]
        public string LocationResponsibleAgency { get; set; }

        public List<LineItem> Lines { get; set; }

        [EdiValue("an..1", FormatterType.EdifactSpec, Path = "UNS/0/0")]
        public char? UNS { get; set; }

        [EdiValue("an..1", FormatterType.EdifactSpec, Path = "UNT/0")]
        public int TrailerMessageSegmentsCount { get; set; }

        [EdiValue("an..14", FormatterType.EdifactSpec, Path = "UNT/1")]
        public string TrailerMessageReference { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("LIN", SequenceEnd = "UNS")]
    public class LineItem
    {
        [EdiValue("an..1", FormatterType.EdifactSpec, Path = "LIN/0/0")]
        public int LineNumber { get; set; }

        [EdiValue("n..3", FormatterType.EdifactSpec, Path = "LIN/1/0")]
        public string Code { get; set; }

        public ItemNumber NumberIdentification { get; set; }

        public Period Period { get; set; }

        public List<PriceDetails> Prices { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("PRI")]
    public class PriceDetails
    {
        public Price Price { get; set; }

        public Range Range { get; set; }

    }

    [EdiElement, EdiPath("PRI/0")]
    public class Price
    {
        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "PRI/0/0")]
        public string Code { get; set; }

        [EdiValue("an..15", FormatterType.EdifactSpec, Path = "PRI/0/1")]
        public decimal? Amount { get; set; }

        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "PRI/0/2")]
        public string Type { get; set; }
    }

    [EdiSegment, EdiPath("RNG")]
    public class Range
    {
        [EdiValue("an..3", FormatterType.EdifactSpec, Path = "RNG/0/0")]
        public string MeasurementUnitCode { get; set; }

        [EdiValue("an..18", FormatterType.EdifactSpec, Path = "RNG/1/0")]
        public decimal? Minimum { get; set; }

        [EdiValue("an..18", FormatterType.EdifactSpec, Path = "RNG/1/1")]
        public decimal? Maximum { get; set; }
    }
}
