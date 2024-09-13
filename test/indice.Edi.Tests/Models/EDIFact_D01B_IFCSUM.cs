using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class EDIFact_D01B_IFCSUM
{
    public List<Message> Messages { get; set; }

    [EdiMessage]
    public class Message
    {
        public BeginingOfMessage BeginingOfMessage { get; set; }

        [EdiCondition("10", Path = "CNT/0/0")]
        public ControlTotal TotalNumberOfConsignments { get; set; }

        [EdiCondition("7", Path = "CNT/0/0")]
        public ControlTotal TotalGrossWeight { get; set; }

        [EdiCondition("137", Path = "DTM/0/0")]
        public Date Date { get; set; }

        [EdiCondition("11", Path = "DTM/0/0")]
        public Date DespatchDate { get; set; }

        /// <summary>
        ///  Party which, by contract with a carrier, consigns or sends goods with the carrier, or has them conveyed by him. Synonym: shipper, sender.
        /// </summary>
        [EdiCondition("CZ", Path = "NAD/0/0")]
        public NameAndAddress Sender { get; set; }

        /// <summary>
        /// Party undertaking or arranging transport of goods between named points.
        /// </summary>
        [EdiCondition("CA", Path = "NAD/0/0")]
        public NameAndAddress Carrier { get; set; }
        public List<Consignment> Consignments { get; set; }
    }


    [EdiSegment, EdiPath("BGM")]
    public class BeginingOfMessage
    {
        public MessageName Name { get; set; }

        public MessageIdentification Identification { get; set; }

        public override string ToString() => $"{Name} {Identification}";

        [EdiElement, EdiPath("BGM/0")]
        public class MessageName
        {
            [EdiValue("X(3)", Path = "BGM/0/0")]
            public string NameCode { get; set; }

            [EdiValue("X(17)", Path = "BGM/0/1")]
            public string IdentificationCode { get; set; }

            [EdiValue("X(3)", Path = "BGM/0/2")]
            public string ResponsibleAgencyCode { get; set; }

            [EdiValue("X(35)", Path = "BGM/0/3")]
            public string Name { get; set; }

            public override string ToString() {
                return $"{NameCode} {Name}".Trim();
            }
        }

        [EdiElement, EdiPath("BGM/1")]
        public class MessageIdentification
        {

            [EdiValue("X(35)", Path = "BGM/1/0")]
            public string DocumentId { get; set; }
            [EdiValue("X(9)", Path = "BGM/1/1")]
            public string VersionId { get; set; }
            [EdiValue("X(6)", Path = "BGM/1/2")]
            public string RevisionId { get; set; }
            [EdiValue("X(3)", Path = "BGM/1/3")]
            public string MessageFucntionCode { get; set; }
            [EdiValue("X(3)", Path = "BGM/1/4")]
            public string ResponseTypeCode { get; set; }
            public override string ToString() {
                return $"{DocumentId}.{VersionId}.{RevisionId}";
            }
        }

    }

    [EdiSegment, EdiPath("CNT")]
    public class ControlTotal
    {
        [EdiValue("X(3)", Path = "CNT/0/0")]
        public string Code { get; set; }
        [EdiValue("9(18)", Path = "CNT/0/1")]
        public decimal Value { get; set; }
        [EdiValue("X(3)", Path = "CNT/0/2")]
        public string MeasurementUnit { get; set; }

        public override string ToString() => $"{Value} {MeasurementUnit}".Trim();

        public static implicit operator decimal(ControlTotal total) => total.Value;
    }

    [EdiSegmentGroup("CNI")]
    public class Consignment : ConsignmentInformation
    {
        [EdiCondition("2", Path = "DTM/0/0")]
        public DateTimePeriod DeliveryDate { get; set; }

        [EdiCondition("2", Path = "CNT/0/0")]
        public ControlTotal NumberOfLines { get; set; }

        [EdiCondition("7", Path = "CNT/0/0")]
        public ControlTotal TotalGrossWeight { get; set; }

        [EdiCondition("11", Path = "CNT/0/0")]
        public ControlTotal TotalNumberOfPackages { get; set; }

        public Party Party { get; set; }
        public List<GoodsItem> Goods { get; set; }
    }

    [EdiSegment, EdiPath("CNI")]
    public class ConsignmentInformation
    {
        [EdiValue("9(4)", Path = "CNI/0/0")]
        public int ConsolidationItemNumber { get; set; }
        public DocumentDetails Document { get; set; }

        [EdiValue("9(4)", Path = "CNI/2/0")]
        public string LoadSequenceIdentifier { get; set; }

        [EdiElement, EdiPath("CNI/1")]
        public class DocumentDetails
        {
            [EdiValue("X(35)", Path = "CNI/1/0")]
            public string Identifier { get; set; }
            [EdiValue("X(3)", Path = "CNI/1/1")]
            public string StatusCode { get; set; }
            [EdiValue("X(70)", Path = "CNI/1/2")]
            public string SourceDescription { get; set; }
            [EdiValue("X(3)", Path = "CNI/1/3")]
            public string LanguageNameCode { get; set; }
            [EdiValue("X(9)", Path = "CNI/1/4")]
            public string VersionIdentifier { get; set; }
            [EdiValue("X(6)", Path = "CNI/1/5")]
            public string RevisionIdentifier { get; set; }

            public override string ToString() => $"{Identifier} {SourceDescription}".Trim();
        }

        public override string ToString() => $"{ConsolidationItemNumber}. {Document}".Trim();
    }

    [EdiSegmentGroup("GID", "HAN", "TMP", "RNG", "TMD", "LOC", "MOA", "PIA", "GIN", "FTX")]
    public class GoodsItem : GoodsItemDetails
    {

    }

    [EdiSegment, EdiPath("GID")]
    public class GoodsItemDetails
    {
        [EdiValue("9(5)", Path = "GID/0/0")]
        public int ItemNumber { get; set; }
    }


    [EdiSegment, EdiPath("NAD")]
    public class NameAndAddress
    {
        [EdiValue("X(3)", Path = "NAD/0/0")]
        public string PartyFunctionCode { get; set; }

        public PartyIdentificationDetails Identification { get; set; }
        public NameAndAddressDescription Description { get; set; }
        public PartyName Name { get; set; }
        public AddressLines Street { get; set; }

        [EdiValue("X(35)", Path = "NAD/5/0")]
        public string City { get; set; }

        public CountryDetails Country { get; set; }

        [EdiValue("X(17)", Path = "NAD/7/0")]
        public string PostalIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "NAD/8/0")]
        public string CountryNameCode { get; set; }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine(Name.ToString());
            sb.AppendLine(" @ ");
            sb.AppendLine($"{Street}");
            sb.AppendLine($"{City}, {Country}".Trim(' ', ','));
            return sb.ToString();
        }

        [EdiElement, EdiPath("NAD/1")]
        public class PartyIdentificationDetails
        {
            [EdiValue("X(35)", Path = "NAD/1/0")]
            public string Identifier { get; set; }
            [EdiValue("X(17)", Path = "NAD/1/1")]
            public string CodeListIdentificationCode { get; set; }
            [EdiValue("X(3)", Path = "NAD/1/2")]
            public string CodeListResponsibleAgencyCode { get; set; }

            public override string ToString() => Identifier;
        }

        [EdiElement, EdiPath("NAD/2")]
        public class NameAndAddressDescription
        {
            [EdiValue("X(35)", Path = "NAD/2/0")]
            public string Part1 { get; set; }
            [EdiValue("X(35)", Path = "NAD/2/1")]
            public string Part2 { get; set; }
            [EdiValue("X(35)", Path = "NAD/2/2")]
            public string Part3 { get; set; }
            [EdiValue("X(35)", Path = "NAD/2/3")]
            public string Part4 { get; set; }
            [EdiValue("X(35)", Path = "NAD/2/4")]
            public string Part5 { get; set; }

            public override string ToString() => $"{Part1} {Part2} {Part3} {Part4} {Part5}".Trim();
        }

        [EdiElement, EdiPath("NAD/3")]
        public class PartyName
        {
            [EdiValue("X(35)", Path = "NAD/3/0")]
            public string Part1 { get; set; }
            [EdiValue("X(35)", Path = "NAD/3/1")]
            public string Part2 { get; set; }
            [EdiValue("X(35)", Path = "NAD/3/2")]
            public string Part3 { get; set; }
            [EdiValue("X(35)", Path = "NAD/3/3")]
            public string Part4 { get; set; }
            [EdiValue("X(35)", Path = "NAD/3/4")]
            public string Part5 { get; set; }
            [EdiValue("X(3)", Path = "NAD/3/5")]
            public string FormatCode { get; set; }

            public override string ToString() => $"{Part1} {Part2} {Part3} {Part4} {Part5}".Trim();
        }

        [EdiElement, EdiPath("NAD/4")]
        public class AddressLines
        {
            [EdiValue("X(35)", Path = "NAD/4/0")]
            public string Line1 { get; set; }
            [EdiValue("X(35)", Path = "NAD/4/1")]
            public string Line2 { get; set; }
            [EdiValue("X(35)", Path = "NAD/4/2")]
            public string Line3 { get; set; }
            [EdiValue("X(35)", Path = "NAD/4/3")]
            public string Line4 { get; set; }

            public override string ToString() => Line1;
        }

        [EdiElement, EdiPath("NAD/6")]
        public class CountryDetails
        {
            [EdiValue("X(9)", Path = "NAD/6/0")]
            public string CountrySubentryNameCode { get; set; }
            [EdiValue("X(17)", Path = "NAD/6/1")]
            public string CodeListIdentificationCode { get; set; }
            [EdiValue("X(3)", Path = "NAD/6/2")]
            public string CodeListResponsibleAgencyCode { get; set; }
            [EdiValue("X(70)", Path = "NAD/6/3")]
            public string CountrySubentryName { get; set; }

            public override string ToString() => $"{CountrySubentryNameCode} {CountrySubentryName}".Trim();
        }
    }

    [EdiSegmentGroup("NAD", "CTA", "RFF")]
    public class Party : NameAndAddress
    {
        public Contact Contact { get; set; }

        [EdiCondition("DQ", Path = "RFF/0/0")]
        public ReferenceAndDate DeliveryNoteNumber { get; set; }

        [EdiCondition("CR", Path = "RFF/0/0")]
        public ReferenceAndDate CustomerReferenceNumber { get; set; }

        [EdiCondition("DQ", "CR", CheckFor = EdiConditionCheckType.NotEqual, Path = "RFF/0/0")]
        public List<ReferenceAndDate> OtherReferences { get; set; }
    }

    [EdiSegment, EdiPath("RFF")]
    public class Reference
    {
        [EdiValue("X(3)", Path = "RFF/0/0")]
        public string CodeQualifier { get; set; }
        [EdiValue("X(70)", Path = "RFF/0/1")]
        public string Identifier { get; set; }
        [EdiValue("X(6)", Path = "RFF/0/2")]
        public string DocumentLineIdentifier { get; set; }
        [EdiValue("X(35)", Path = "RFF/0/3")]
        public string ReferenceVersionIdentifier { get; set; }
        [EdiValue("X(6)", Path = "RFF/0/4")]
        public string RevisionIdentifier { get; set; }

        public override string ToString() => $"{Identifier} {DocumentLineIdentifier} {ReferenceVersionIdentifier} {RevisionIdentifier}".Trim();
    }

    [EdiSegmentGroup("RFF", "DTM")]
    public class ReferenceAndDate : Reference
    {
        public DateTimePeriod Date { get; set; }
    }

    [EdiSegmentGroup("CTA", "COM")]
    public class Contact : ContactInformation
    {
        public ContactCommunication Communication { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DateTimePeriod
    {
        [EdiValue(Path = "DTM/0/0")]
        public int Function { get; set; }

        [EdiValue("9(12)", Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
        public virtual DateTime Value { get; set; }

        [EdiValue("9(8)", Path = "DTM/0/2")]
        public string Code { get; set; }

        public static implicit operator DateTime(DateTimePeriod input) => input.Value;
        public override string ToString() => Value.ToString();
    }


    public class Date : DateTimePeriod
    {
        [EdiValue("9(8)", Path = "DTM/0/1", Format = "yyyyMMdd")]
        public override DateTime Value { get => base.Value; set => base.Value = value; }
    }

    /// <summary>
    /// COM - To identify a communication number of a department or a person to whom communication should be directed.
    /// </summary>
    [EdiSegment, EdiPath("COM")]
    public class ContactCommunication
    {
        /// <summary>
        /// This should be a list
        /// </summary>
        public ContactCommunicationEntry[] Entries { get; set; }

        public override string ToString() => Entries?.FirstOrDefault()?.ToString();

        [EdiElement, EdiPath("COM/0")]
        public class ContactCommunicationEntry
        {
            [EdiValue("X(512)", Path = "COM/0/0")]
            public string AddressIdentifier { get; set; }

            [EdiValue("X(3)", Path = "COM/0/1")]
            public string AddressCodeQualifier { get; set; }

            public override string ToString() => $"{AddressCodeQualifier} {AddressIdentifier}".Trim();
        }
    }

    [EdiSegment, EdiPath("CNI")]
    public class ContactInformation
    {
        [EdiValue("X(3)", Path = "CNI/0/0")]
        public string ContactFunctionCode { get; set; }

        public DepartmentOrEmployeeDetails Details { get; set; }

        public override string ToString() => $"{ContactFunctionCode} {Details}".Trim();

        [EdiElement, EdiPath("CNI/1")]
        public class DepartmentOrEmployeeDetails
        {
            [EdiValue("X(17)", Path = "CNI/1/0")]
            public string DepartmentOrEmployeeNameCode { get; set; }

            [EdiValue("X(35)", Path = "CNI/1/1")]
            public string DepartmentOrEmployeeName { get; set; }

            public override string ToString() => $"{DepartmentOrEmployeeNameCode} {DepartmentOrEmployeeName}".Trim();
        }
    }
}
