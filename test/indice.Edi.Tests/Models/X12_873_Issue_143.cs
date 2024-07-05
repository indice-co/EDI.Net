using System;
using System.Collections.Generic;
using indice.Edi.Serialization;
using indice.Edi.Utilities;

namespace indice.Edi.Tests.Models;

class X12_873_Issue_143
{

    [EdiValue("9(2)", Path = "ISA/0", Description = "I01 - Authorization Information Qualifier")]
    public int AuthorizationInformationQualifier { get; set; }
    [EdiValue("X(10)", Path = "ISA/1", Description = "")]
    public string AuthorizationInformation { get; set; }
    [EdiValue("9(2)", Path = "ISA/2", Description = "I03 - Security Information Qualifier")]
    public string Security_Information_Qualifier { get; set; }

    [EdiValue("X(10)", Path = "ISA/3", Description = "I04 - Security Information")]
    public string Security_Information { get; set; }

    [EdiValue("9(2)", Path = "ISA/4", Description = "I05 - Interchange ID Qualifier")]
    public string ID_Qualifier { get; set; }

    [EdiValue("X(15)", Path = "ISA/5", Description = "I06 - Interchange Sender ID")]
    public string Sender_ID { get; set; }

    [EdiValue("9(2)", Path = "ISA/6", Description = "I05 - Interchange ID Qualifier")]
    public string ID_Qualifier2 { get; set; }

    [EdiValue("X(15)", Path = "ISA/7", Description = "I07 - Interchange Receiver ID")]
    public string Receiver_ID { get; set; }

    [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I08 - Interchange Date")]
    [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "TI09 - Interchange Time")]
    public DateTime Date { get; set; }

    [EdiValue("X(1)", Path = "ISA/10", Description = "I10 - Interchange Control Standards ID")]
    public string Control_Standards_ID { get; set; }

    [EdiValue("9(5)", Path = "ISA/11", Description = "I11 - Interchange Control Version Num")]
    public int ControlVersion { get; set; }

    [EdiValue("9(9)", Path = "ISA/12", Description = "I12 - Interchange Control Number")]
    public int ControlNumber { get; set; }

    [EdiValue("9(1)", Path = "ISA/13", Description = "I13 - Acknowledgement Requested")]
    public bool? AcknowledgementRequested { get; set; }

    [EdiValue("X(1)", Path = "ISA/14", Description = "I14 - Usage Indicator")]
    public string Usage_Indicator { get; set; }

    [EdiValue("X(1)", Path = "ISA/15", Description = "I15 - Component Element Separator")]
    public char? Component_Element_Separator { get; set; }
    [EdiValue("9(1)", Path = "IEA/0", Description = "I16 - Num of Included Functional Grps")]
    public int GroupsCount { get; set; }

    [EdiValue("9(9)", Path = "IEA/1", Description = "I12 - Interchange Control Number")]
    public int TrailerControlNumber { get; set; }
    public List<FunctionalGroup> Groups { get; set; }

    [EdiGroup]
    public class FunctionalGroup
    {
        [EdiValue("X(2)", Path = "GS/0", Description = "479 - Functional Identifier Code")]
        public string FunctionalIdentifierCode { get; set; }

        [EdiValue("X(15)", Path = "GS/1", Description = "142 - Application Sender's Code")]
        public string ApplicationSenderCode { get; set; }

        [EdiValue("X(15)", Path = "GS/2", Description = "124 - Application Receiver's Code")]
        public string ApplicationReceiverCode { get; set; }

        [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "373 - Date")]
        [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "337 - Time")]
        public DateTime Date { get; set; }

        [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "28 - Group Control Number")]
        public int GroupControlNumber { get; set; }

        [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "455 Responsible Agency Code")]
        public string AgencyCode { get; set; }

        [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "480 Version / Release / Industry Identifier Code")]
        public string Version { get; set; }

        public List<Message> Messages { get; set; }

        [EdiValue("9(1)", Path = "GE/0", Description = "97 Number of Transaction Sets Included")]
        public int TransactionsCount { get; set; }

        [EdiValue("9(9)", Path = "GE/1", Description = "28 Group Control Number")]
        public int GroupTrailerControlNumber { get; set; }
    }

    [EdiMessage]
    public class Message
    {
        [EdiValue("X(3)", Path = "ST/0", Description = "ST01 - Transaction set ID code")]
        public string TransactionSetCode { get; set; }

        [EdiValue("X(9)", Path = "ST/1", Description = "ST02 - Transaction set control number")]
        public string TransactionSetControlNumber { get; set; }
        //Optional
        [EdiValue("X(35)", Path = "ST/2", Description = "ST03 - Transaction set control number")]
        public string ImplementationConventionReference { get; set; }

        [EdiValue(Path = "SE/0", Description = "SE01 - Number of included segments")]
        public int SegmentCounts { get; set; }

        [EdiValue("X(9)", Path = "SE/1", Description = "SE02 - Transaction set control number (same as ST02)")]
        public string TrailerTransactionSetControlNumber { get; set; }

        public BGN BeginningSegment { get; set; }
        public DTM MyProperty { get; set; }
        public List<N1> PartyIdentification { get; set; }
        public List<DTM_Loop> Details { get; set; }
    }

    [EdiSegment, EdiPath("BGN")]
    public class BGN 
    {
        [EdiValue("X(2)", Path = "BGN/0", Description = "Transaction Set Purpose Code")]
        public string TransactionSetPurposeCode { get; set; }

        [EdiValue("X(50)", Path = "BGN/1", Description = "Reference Identification")]
        public string ReferenceIdentification { get; set; }

        [EdiValue("9(8)", Path = "BGN/2", Format = "yyyyMMdd", Description = "Date")]
        public DateTime Date { get; set; }

        [EdiValue("9(8)", Path = "BGN/3", Format = "HHmm", Description = "Time")]
        public TimeSpan Time { get; set; }

        [EdiValue("X(2)", Path = "BGN/4", Description = "Time Code")]
        public string TimeCode { get; set; }

        [EdiValue("X(50)", Path = "BGN/5", Description = "Reference Identification")]
        public string ReferenceIdentification1 { get; set; }

        [EdiValue("X(2)", Path = "BGN/6", Description = "Transaction Type Code")]
        public string TransactionTypeCode { get; set; }

        [EdiValue("X(2)", Path = "BGN/7", Description = "Action Code")]
        public string ActionCode { get; set; }

        [EdiValue("X(2)", Path = "BGN/8", Description = "Security Level Code")]
        public string SecurityLevelCode { get; set; }
    }

    [EdiSegmentGroup("DTM", "N9", "LQ", "LCD", "CS", "SLN")]
    public class DTM_Loop : DTM
    {
        public N9 N9 { get; set; }
        public LQ LQ { get; set; }
        public LCD LCD { get; set; }
        public CS_Loop ContractSummary { get; set; }
        public SLN_Loop SublineItemDetail { get; set; }
    }

    [EdiSegmentGroup("CS", "N1")]
    public class CS_Loop : CS
    {
        public N1 N1 { get; set; }
    }

    [EdiSegmentGroup("SLN", "LQ", "N1")]
    public class SLN_Loop : SLN
    {
        public List<N1_Loop> N1List { get; set; }
    }

    [EdiSegmentGroup("N1", "N9")]
    public class N1_Loop : N1
    {
        public N9 N9 { get; set; }
    }


    [EdiSegment, EdiPath("N1")]
    public class N1
    {
        [EdiValue("X(3)", Path = "N1/0")]
        public string EntityIdentifierCode { get; set; }
        [EdiValue("X(2)", Path = "N1/2")]
        public string IdentificationCodeQualifier { get; set; }
        [EdiValue("X(17)", Path = "N1/3")]
        public string IdentificationCode { get; set; }
    }

    [EdiSegment, EdiPath("N9")]
    public class N9
    {
        [EdiValue("X(3)", Path = "N9/0")]
        public string ReferenceIdentificationQualifier { get; set; }
        [EdiValue("X(30)", Path = "N9/1")]
        public string ReferenceIdentification { get; set; }
    }

    [EdiSegment, EdiPath("CS")]
    public class CS
    {
        [EdiValue("X(30)", Path = "CS/0")]
        public string ContractNumber { get; set; }
    }

    [EdiSegment, EdiPath("SLN")]
    public class SLN
    {

        [EdiValue("X(30)", Path = "SLN/0")]
        public string AssignedIdentification { get; set; }
        [EdiValue("X(30)", Path = "SLN/2")]
        public string RelationshipCode { get; set; }
        [EdiValue("9(30)", Path = "SLN/3")]
        public decimal Quantity { get; set; }
        [EdiValue("X(30)", Path = "SLN/4")]
        public string CompositeUnitofMeasure { get; set; }
    }

    [EdiSegment, EdiPath("LQ")]
    public class LQ
    {

        [EdiValue("X(3)", Path = "LQ/0")]
        public string CodeListQualifierCode { get; set; }
        [EdiValue("X(30)", Path = "LQ/1")]
        public string IndustryCode { get; set; }
    }

    [EdiSegment, EdiPath("LCD")]
    public class LCD
    {

        [EdiValue("X(3)", Path = "LCD/1")]
        public string EntityIdentifierCode { get; set; }
        [EdiValue("X(2)", Path = "LCD/4")]
        public string IdentificationCodeQualifier { get; set; }
        [EdiValue("X(17)", Path = "LCD/5")]
        public string IdentificationCode { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DTM
    {
        [EdiValue("X(3)", Path = "DTM/0", Description = "Date/Time Qualifier")]
        public string DateTimeQualifier { get; set; }

        [EdiValue("X(3)", Path = "DTM/4", Description = "Date/Time/Period Qualifier")]
        public string DateTimePeriodQualifier { get; set; }

        [EdiValue("X(17)", Path = "DTM/5", Description = "DateTime")]
        public string DateTime { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DTM007
    {
        [EdiValue("X(3)", Path = "DTM/0", Description = "Date/Time Qualifier")]
        public string DateTimeQualifier { get; set; }

        [EdiValue("X(3)", Path = "DTM/4", Description = "Date/Time/Period Qualifier")]
        public string DateTimePeriodQualifier { get; set; }

        [EdiValue("X(17)", Path = "DTM/5", Description = "Period")]
        public DTMPeriod Period { get; set; }
    }

    public struct DTMPeriod
    {
        public readonly DateTime From;
        public readonly DateTime To;

        public DTMPeriod(DateTime from, DateTime to) {
            From = from;
            To = to;
        }

        public static DTMPeriod Parse(string text) {
            var textFrom = text?.Substring(0, 8);
            var textTo = text?.Substring(9, 8);
            return new DTMPeriod(
                    textFrom.ParseEdiDate("yyyyMMdd"),
                    textTo.ParseEdiDate("yyyyMMdd")
                );
        }

        public override string ToString() {
            return $"{From:yyyyMMdd}-{To:yyyyMMdd}";
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
}
