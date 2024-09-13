using System.Globalization;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class EdiFact_Issue154_Interchange
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

    public List<Group> Groups { get; set; }

    [EdiValue("X(1)", Path = "UNZ/0")]
    public int TrailerControlCount { get; set; }

    [EdiValue("X(14)", Path = "UNZ/1")]
    public string TrailerControlReference { get; set; }

    [EdiGroup]
    public class Group
    {
        public List<Message> Invoices { get; set; }
    }

    [EdiMessage]
    public class Message
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

        public Invoice Invoice { get; set; }
    }


    [EdiSegmentGroup("BGM")]
    public class Invoice : InvoiceInformation
    {
        [EdiCondition("137", Path = "DTM/0/0")]
        public DTM MessageCreation { get; set; }

        [EdiCondition("PE", Path = "NAD/0/0")]
        public NAD Payee { get; set; }

        [EdiCondition("PR", Path = "NAD/0/0")]
        public NAD Payer { get; set; }

        [EdiCondition("AP", Path = "NAD/0/0")]
        public NAD AssignedCarrier { get; set; }

        public List<LineItem> LineItems { get; set; }

        public SectionControl SectionControl { get; set; }
    }

    [EdiSegment, EdiPath("UNS"), EdiSegmentGroup("UNS")]
    public class SectionControl
    {
        public CNT LineItemCount { get; set; }

        [EdiCondition("9", Path = "MOA/0/0")]
        public MOA Payment1 { get; set; }

        [EdiCondition("91", Path = "MOA/0/0")]
        public MOA Payment2 { get; set; }

        [EdiCondition("8", Path = "MOA/0/0")]
        public MOA Charge1 { get; set; }

        [EdiCondition("23", Path = "MOA/0/0")]
        public MOA Charge2 { get; set; }

        [EdiCondition("58", Path = "MOA/0/0")]
        public MOA Charge3 { get; set; }

        [EdiCondition("104", Path = "MOA/0/0")]
        public MOA Charge4 { get; set; }

        [EdiCondition("143", Path = "MOA/0/0")]
        public MOA Change5 { get; set; }

        [EdiCondition("5", Path = "MOA/0/0")]
        public MOA Charge6 { get; set; }

        [EdiCondition("81", Path = "MOA/0/0")]
        public MOA Charge7 { get; set; }
    }

    [EdiSegment, EdiPath("BGM")]
    public class InvoiceInformation
    {
        [EdiValue("X(35)", Path = "BGM/1/0")]
        public string InvoiceNo { get; set; }

        [EdiValue("X(3)", Path = "BGM/2/0")]
        public string InvoiceAdviceType { get; set; }
    }

    [EdiSegmentGroup("LIN")]
    public class LineItem : LineItemInformation
    {

    }

    [EdiSegment, EdiPath("LIN")]
    public class LineItemInformation
    {
        [EdiValue("9(6)", Path = "LIN/0/0")]
        public int LineNo { get; set; }

        [EdiValue("X(35)", Path = "PIA/1/0")]
        public string Number { get; set; }

        [EdiValue("X(35)", Path = "PIA/2/0")]
        public string Text1 { get; set; }

        [EdiValue("X(35)", Path = "PIA/3/0")]
        public string Text2 { get; set; }

        [EdiValue("X(35)", Path = "PIA/4/0")]
        public string Text3 { get; set; }

        [EdiValue("X(35)", Path = "PIA/5/0")]
        public string Text4 { get; set; }

        [EdiValue("X(3)", Path = "MEA/1/1")]
        public int? IntValue1 { get; set; }

        [EdiValue("9(18)", Path = "MEA/1/2")]
        public int? IntValue2 { get; set; }

        [EdiValue("9(2)", Path = "MEA/1/3")]
        public int? IntValue3 { get; set; }


        [EdiCondition("182", Path = "DTM/0/0")]
        public DTM DateTime1 { get; set; }

        [EdiCondition("191", Path = "DTM/0/0")]
        public DTM DateTime2 { get; set; }

        [EdiCondition("133", Path = "DTM/0/0")]
        public DTM DateTime3 { get; set; }

        [EdiCondition("3", Path = "DTM/0/0")]
        public DTM DateTime4 { get; set; }

        [EdiCondition("9", Path = "DTM/0/0")]
        public DTM DateTime5 { get; set; }

        [EdiCondition("AAD", Path = "FTX/0/0")]
        public FTX Code1 { get; set; }

        [EdiCondition("ABN", Path = "FTX/0/0")]
        public FTX Code2 { get; set; }

        [EdiCondition("PKG", Path = "FTX/0/0")]
        public FTX Code3 { get; set; }

        [EdiCondition("PMT", Path = "FTX/0/0")]
        public FTX Code4 { get; set; }

        [EdiCondition("ORI", Path = "FTX/0/0")]
        public FTX Code5 { get; set; }

        [EdiCondition("39", Path = "MOA/0/0")]
        public MOA Payment1 { get; set; }

        [EdiCondition("309", Path = "MOA/0/0")]
        public MOA Payment2 { get; set; }

        [EdiCondition("64", Path = "MOA/0/0")]
        public MOA Payment3 { get; set; }

        [EdiCondition("165", Path = "MOA/0/0")]
        public MOA Payment4 { get; set; }

        [EdiCondition("81", Path = "MOA/0/0")]
        public MOA Payment5 { get; set; }

        [EdiCondition("5", Path = "MOA/0/0")]
        public MOA Payment6 { get; set; }

        [EdiCondition("122", Path = "MOA/0/0")]
        public MOA Payment7 { get; set; }

        [EdiCondition("130", Path = "MOA/0/0")]
        public MOA Payment8 { get; set; }

        [EdiCondition("128", Path = "MOA/0/0")]
        public MOA Payment9 { get; set; }

        [EdiCondition("INF", Path = "PRI/0/0")]
        public PRI Payment10 { get; set; }

        [EdiCondition("CAL", Path = "PRI/0/0")]
        public PRI Payment11 { get; set; }

        [EdiCondition("AAA", Path = "PRI/0/0")]
        public PRI Payment12 { get; set; }

        [EdiCondition("AAB", Path = "PRI/0/0")]
        public PRI Payment13 { get; set; }

        [EdiCondition("AAC", Path = "PRI/0/0")]
        public PRI Payment14 { get; set; }

        [EdiCondition("AAD", Path = "PRI/0/0")]
        public PRI Payment15 { get; set; }

        [EdiCondition("AAE", Path = "PRI/0/0")]
        public PRI Payment16 { get; set; }

        [EdiCondition("AAF", Path = "PRI/0/0")]
        public PRI Payment17 { get; set; }

        [EdiCondition("INV", Path = "PRI/0/0")]
        public PRI Payment18 { get; set; }

        [EdiCondition("CT", Path = "RFF/0/0")]
        public ContractReference ContractReference { get; set; }

        public PackageInformation Package { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/0")]
        public string Tag1 { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/1")]
        public string Tag2 { get; set; }

        public Transport1 Transport1 { get; set; }
        public Transport2 Transport2 { get; set; }

        public Transport3 Transport3 { get; set; }

        [EdiCondition("22", Path = "TDT/0/0")]
        public TransportGeneric GenericTransport1 { get; set; }

        [EdiCondition("23", Path = "TDT/0/0")]
        public TransportGeneric GenericTransport2 { get; set; }

        [EdiCondition("24", Path = "TDT/0/0")]
        public TransportGeneric GenericTransport3 { get; set; }

        public Transport4 Transport4 { get; set; }

        public Transport5 Transport5 { get; set; }

        [EdiCondition("20", Path = "LOC/0/0")]
        public LOC Location1 { get; set; }

        public Requirements Requirements { get; set; }

    }

    [EdiSegment, EdiPath("RCS"), EdiCondition("13", Path = "RCS/0/0"), EdiSegmentGroup("RCS")]
    public class Requirements
    {
        [EdiCondition("AAU", Path = "FTX/0/0")]
        public FTX ControlNumber { get; set; }

        [EdiCondition("ZZZ", Path = "FTX/0/0")]
        public FTX AdjustmentReasonCode { get; set; }


        [EdiCondition("ACD", Path = "FTX/0/0")]
        public FTX Code1 { get; set; }


        [EdiCondition("AAP", Path = "FTX/0/0")]
        public FTX Code2 { get; set; }

        public ClaimText Text1 { get; set; }
    }

    [EdiSegment, EdiPath("PAC")]
    public class PackageInformation
    {
        [EdiValue("9(8)", Path = "PAC/0/0")]
        public int Count { get; set; }

        [EdiValue("X(17)", Path = "PAC/2/0")]
        public string Type { get; set; }

        [EdiCondition("WT", Path = "MEA/0/0")]
        public Measurement Weight { get; set; }
    }

    [EdiSegment, EdiPath("TDT"), EdiCondition("10", Path = "TDT/0/0"), EdiSegmentGroup("TDT")]
    public class Transport1
    {
        [EdiValue("X(3)", Path = "TDT/0/0")]
        public string Qualifier { get; set; }
        [EdiValue("X(17)", Path = "TDT/2/1")]
        public string ModeOfTransport { get; set; }

        [EdiValue("X(17)", Path = "TDT/4/0")]
        public string Text1 { get; set; }

        [EdiValue("X(9)", Path = "TDT/7/0")]
        public string Text2 { get; set; }

        [EdiValue("X(35)", Path = "TDT/7/3")]
        public string Text3 { get; set; }
    }

    [EdiSegment, EdiPath("TDT"), EdiCondition("20", Path = "TDT/0/0"), EdiSegmentGroup("TDT")]
    public class Transport2
    {
        [EdiValue("X(17)", Path = "TDT/1/0")]
        public string ControlNumber { get; set; }

        [EdiValue("X(17)", Path = "TDT/2/1")]
        public string Mode { get; set; }

        [EdiValue("X(17)", Path = "TDT/4/0")]
        public string Text1 { get; set; }

        [EdiValue("X(9)", Path = "TDT/7/0")]
        public string Text2 { get; set; }

        [EdiCondition("5", Path = "LOC/0/0")]
        public LOC Location { get; set; }

        [EdiCondition("200", Path = "DTM/0/0")]
        public DTM DateTime1 { get; set; }

        [EdiCondition("310", Path = "DTM/0/0")]
        public DTM DateTime2 { get; set; }
    }

    [EdiSegment, EdiPath("TDT"), EdiCondition("21", Path = "TDT/0/0"), EdiSegmentGroup("TDT")]
    public class Transport3
    {
        [EdiValue("X(17)", Path = "TDT/1/0")]
        public string ControlNumber { get; set; }

        [EdiValue("X(17)", Path = "TDT/2/1")]
        public string Mode { get; set; }

        [EdiValue("X(17)", Path = "TDT/4/0")]
        public string Text1 { get; set; }

        [EdiValue("X(9)", Path = "TDT/7/0")]
        public string Text2 { get; set; }

        [EdiValue("X(35)", Path = "TDT/7/3")]
        public string Text3 { get; set; }

        [EdiCondition("149", Path = "LOC/0/0")]
        public LOC Location { get; set; }

        [EdiCondition("342", Path = "DTM/0/0")]
        public DTM DateTime1 { get; set; }

        [EdiCondition("311", Path = "DTM/0/0")]
        public DTM DateTime2 { get; set; }
    }

    [EdiSegment, EdiPath("TDT"), EdiSegmentGroup("TDT")]
    public class TransportGeneric
    {
        [EdiValue("X(17)", Path = "TDT/1/0")]
        public string ControlNumber { get; set; }

        [EdiValue("X(17)", Path = "TDT/2/1")]
        public string Mode { get; set; }

        [EdiValue("X(17)", Path = "TDT/4/0")]
        public string Text1 { get; set; }

        [EdiValue("X(9)", Path = "TDT/7/0")]
        public string Text2 { get; set; }

        [EdiValue("X(35)", Path = "TDT/7/3")]
        public string Text3 { get; set; }

        [EdiCondition("103", Path = "LOC/0/0")]
        public LOC Location { get; set; }

        [EdiCondition("295", Path = "DTM/0/0")]
        public DTM DateTime1 { get; set; }

        [EdiCondition("294", Path = "DTM/0/0")]
        public DTM DateTime2 { get; set; }
    }

    [EdiSegment, EdiPath("TDT"), EdiCondition("25", Path = "TDT/0/0"), EdiSegmentGroup("TDT")]
    public class Transport4
    {
        [EdiValue("X(17)", Path = "TDT/1/0")]
        public string ControlNumber { get; set; }

        [EdiValue("X(17)", Path = "TDT/2/1")]
        public string Mode { get; set; }

        [EdiValue("X(17)", Path = "TDT/4/0")]
        public string Text1 { get; set; }

        [EdiCondition("7", Path = "LOC/0/0")]
        public LOC Location1 { get; set; }

        [EdiCondition("35", Path = "DTM/0/0")]
        public DTM DateTime1 { get; set; }

        [EdiCondition("36", Path = "DTM/0/0")]
        public DTM DateTime2 { get; set; }

        [EdiCondition("20", Path = "LOC/0/0")]
        public LOC Location2 { get; set; }
    }

    [EdiSegment, EdiPath("TDT"), EdiCondition("2", Path = "TDT/0/0"), EdiSegmentGroup("TDT")]
    public class Transport5
    {
        [EdiValue("X(17)", Path = "TDT/1/0")]
        public string ControlNumber { get; set; }

        [EdiValue("X(17)", Path = "TDT/4/0")]
        public string Text1 { get; set; }

        [EdiValue("X(17)", Path = "TDT/7/0")]
        public string Text2 { get; set; }

        [EdiCondition("ZZZ", Path = "LOC/0/0")]
        public LOC Location1 { get; set; }

        [EdiCondition("15", Path = "DTM/0/0")]
        public DTM DateTime1 { get; set; }

        [EdiCondition("36", Path = "DTM/0/0")]
        public DTM DateTime2 { get; set; }

        [EdiCondition("20", Path = "LOC/0/0")]
        public LOC Location2 { get; set; }
    }

    [EdiSegment, EdiPath("LOC")]
    public class LOC
    {
        [EdiValue("X(3)", Path = "LOC/0/0")]
        public string Qualifier { get; set; }

        [EdiValue("X(25)", Path = "LOC/1/0")]
        public string Loc1 { get; set; }
        [EdiValue("X(25)", Path = "LOC/2/0")]
        public string Loc2 { get; set; }
    }

    [EdiSegment, EdiPath("MEA")]
    public class Measurement
    {
        [EdiValue("X(3)", Path = "MEA/0/0")]
        public string Qualifier { get; set; }

        [EdiValue("X(3)", Path = "MEA/2/0")]
        public string UnitQualifier { get; set; }

        [EdiValue("X(18)", Path = "MEA/2/1")]
        public string Value { get; set; }
    }

    [EdiSegment, EdiPath("RFF")]
    public class ContractReference
    {
        [EdiValue("X(3)", Path = "RFF/0/0")]
        public string Qualifer { get; set; }

        [EdiValue("X(35)", Path = "RFF/0/1")]
        public string ContractNumber { get; set; }

        [EdiValue("X(6)", Path = "RFF/0/2")]
        public string LineNumber { get; set; }

        [EdiValue("X(35)", Path = "RFF/0/3")]
        public string ContractTypeCode { get; set; }
    }

    [EdiSegment, EdiPath("PRI")]
    public class PRI
    {
        [EdiValue("X(3)", Path = "PRI/0/0")]
        public string Qualifier { get; set; }

        [EdiValue("9(15)", Path = "PRI/0/1")]
        public decimal Value { get; set; }
    }

    [EdiSegment, EdiPath("FTX")]
    public class FTX
    {
        [EdiValue(Path = "FTX/0/0")]
        public string Qualifier { get; set; }
        [EdiValue(Path = "FTX/1/0")]
        public string Function { get; set; }

        [EdiValue(Path = "FTX/2/0")]
        public string TextReference { get; set; }

        [EdiValue("X(17)", Path = "FTX/3/0")]
        public string TextIdentification { get; set; }
    }

    [EdiSegment, EdiPath("FTX"), EdiCondition("AAO", Path = "FTX/0/0")]
    public class ClaimText
    {
        [EdiValue("X(70)", Path = "FTX/3/0")]
        public string Text1 { get; set; }
        [EdiValue("X(70)", Path = "FTX/3/1")]
        public string Text2 { get; set; }
        [EdiValue("X(70)", Path = "FTX/3/2")]
        public string Text3 { get; set; }
        [EdiValue("X(70)", Path = "FTX/3/3")]
        public string Text4 { get; set; }

        public string Text {
            get {
                return (Text1 ?? string.Empty) + (Text2 ?? string.Empty) + (Text3 ?? string.Empty) + (Text4 ?? string.Empty);
            }
        }
    }
    [EdiSegment, EdiPath("CNT")]
    public class CNT
    {
        [EdiValue(Path = "CNT/0/0")]
        public string Qualifier { get; set; }
        [EdiValue(Path = "CNT/0/1")]
        public int Quantity { get; set; }
    }

    [EdiSegment, EdiPath("NAD")]
    public class NAD
    {
        [EdiValue("X(3)", Path = "NAD/0/0")]
        public string PartyQualifier { get; set; }

        [EdiValue("X(35)", Path = "NAD/1/0")]
        public string PartyId { get; set; }
    }


    [EdiElement, EdiPath("MOA/0")]
    public class MOA
    {
        [EdiValue("X(3)", Path = "MOA/0/0")]
        public string Qualifier { get; set; }

        [EdiValue("9(35)", Path = "MOA/0/1")]
        public decimal Amount { get; set; }
    }

    [EdiElement, EdiPath("DTM/0")]
    public class DTM
    {
        [EdiValue("9(3)", Path = "DTM/0/0")]
        public int ID { get; set; }

        [EdiValue("X(35)", Path = "DTM/0/1")]
        public string Value { get; set; }


        [EdiValue("9(3)", Path = "DTM/0/2")]
        public int FormatQualifier { get; set; }

        public DateTime? AsDateTime() {
            if (string.IsNullOrWhiteSpace(Value)) return null;
            string[] formats = null;
            switch (FormatQualifier) {
                case 203:
                    formats = new[] { "yyyyMMddHHmm" };
                    break;
                case 204:
                    formats = new[] { "yyyyMMddHHmmss" };
                    break;
                case 201:
                    formats = new[] { "yyMMddHHmm" };
                    break;
                case 102:
                    formats = new[] { "yyyyMMdd" };
                    break;
            }
            if (DateTime.TryParseExact(Value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datetime))
                return datetime;
            return null;
        }
    }
}
