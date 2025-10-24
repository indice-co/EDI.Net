using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

[EdiMessage]
public class Edifact_Issue280
{
    public UNH unh { get; set; }
    public BGM bgm { get; set; }
    public IList<DTM> Dates { get; set; }
    public IList<FTX> ftx {  get; set; }
    public IList<SegmentGroup3> Refrences { get; set; }
    public IList<SegmentGroup4> Parties { get; set; }
    public UNT unt { get; set; }


    [EdiSegmentGroup("RFF")]
    public record SegmentGroup3 : RFF
    {
    }

    [EdiSegmentGroup("NAD", "CTA", "COM", "RFF")]
    public record SegmentGroup4 : NAD 
    {
        public IList<SegmentGroup5>? Contacts { get; set; }
        public IList<SegmentGroup6>? Refrences { get; set; }
    }

    [EdiSegmentGroup("CTA", "COM")]
    public record SegmentGroup5 : CTA 
    {
        public IList<COM> com { get; set; }
    }

    [EdiSegmentGroup("RFF")]
    public record SegmentGroup6 : RFF
    { 
    }

    #region EdiSegments
    [EdiSegment, EdiPath("UNH")]
    public record UNH
    {
        [EdiValue("X(14)", Path = "UNH/0/0", Mandatory = true)]
        public string MessageRefNum { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/0", Mandatory = true)]
        public string MessageType { get; set; }

        [EdiValue("X(3)", Path = "UNH/1/1", Mandatory = true)]
        public string Version { get; set; }

        [EdiValue("X(3)", Path = "UNH/1/2", Mandatory = true)]
        public string Release { get; set; }

        [EdiValue("X(2)", Path = "UNH/1/3", Mandatory = true)]
        public string ControllingAgency { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/4", Mandatory = false)]
        public string? AssociationAssignedCode { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/5", Mandatory = false)]
        public string? CodeListDirectoryVersionNumber { get; set; }
    }

    [EdiSegment, EdiPath("BGM")]
    public record BGM
    {
        [EdiValue("X(3)", Path = "BGM/0/0", Mandatory = false)]
        public string? DocumentNameCode { get; set; }

        [EdiValue("X(17)", Path = "BGM/0/1", Mandatory = false)]
        public string? CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/0/2", Mandatory = false)]
        public string? CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "BGM/0/3", Mandatory = false)]
        public string? DocumentName { get; set; }

        [EdiValue("X(70)", Path = "BGM/1/0", Mandatory = false)]
        public string? DocumentIdentifier { get; set; }

        [EdiValue("X(9)", Path = "BGM/1/1", Mandatory = false)]
        public string? VersionIdentifier { get; set; }

        [EdiValue("X(6)", Path = "BGM/1/2", Mandatory = false)]
        public string? RevisionIdentifier { get; set; }

        [EdiValue("X(3)", Path = "BGM/2", Mandatory = true)]
        public string? MessageFunctionCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/3", Mandatory = false)]
        public string? ResponseTypeCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/4", Mandatory = false)]
        public string? DocumentStatusCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/5", Mandatory = false)]
        public string? LanguageNameCode { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public record DTM
    {

        [EdiValue("X(3)", Path = "DTM/0/0", Mandatory = true)]
        public string DateTimeQualifier { get; set; }

        [EdiValue("X(35)", Path = "DTM/0/1", Mandatory = false)]
        public string? DateTime { get; set; }

        [EdiValue("X(3)", Path = "DTM/0/2", Mandatory = false)]
        public string? FormatCode { get; set; }
    }

    [EdiSegment, EdiPath("FTX")]
    public record FTX
    {

        [EdiValue("X(3)", Path = "FTX/0", Mandatory = true)]
        public string TextSubjectQualifier { get; set; }

        [EdiValue("X(3)", Path = "FTX/1", Mandatory = false)]
        public string? TextFunctionCode { get; set; }

        [EdiValue("X(17)", Path = "FTX/2/0", Mandatory = false)]
        public string? TextDescriptionCode { get; set; }

        [EdiValue("X(17)", Path = "FTX/2/1", Mandatory = false)]
        public string? CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "FTX/2/2", Mandatory = false)]
        public string? CodeListAgencyCode { get; set; }

        [EdiValue("X(512)", Path = "FTX/3/0", Mandatory = false)]
        public string? FreeText1 { get; set; }

        [EdiValue("X(512)", Path = "FTX/3/1", Mandatory = false)]
        public string? FreeText2 { get; set; }

        [EdiValue("X(512)", Path = "FTX/3/2", Mandatory = false)]
        public string? FreeText3 { get; set; }

        [EdiValue("X(512)", Path = "FTX/3/3", Mandatory = false)]
        public string? FreeText4 { get; set; }

        [EdiValue("X(512)", Path = "FTX/3/4", Mandatory = false)]
        public string? FreeText5 { get; set; }

        [EdiValue("X(3)", Path = "FTX/4", Mandatory = false)]
        public string? LanguageNameCode { get; set; }

        [EdiValue("X(3)", Path = "FTX/5", Mandatory = false)]
        public string? FormatCode { get; set; }
    }

    [EdiSegment, EdiPath("NAD")]
    public record NAD
    {
        [EdiValue("X(3)", Path = "NAD/0", Mandatory = true)]
        public string? PartyFunctionCodeQualifier { get; set; }

        [EdiValue("X(35)", Path = "NAD/1/0", Mandatory = false)]
        public string? PartyIdentifier { get; set; }

        [EdiValue("X(17)", Path = "NAD/1/1", Mandatory = false)]
        public string? CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "NAD/1/2", Mandatory = false)]
        public string? CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/0", Mandatory = false)]
        public string? NameAndAddress1 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/1", Mandatory = false)]
        public string? NameAndAddress2 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/2", Mandatory = false)]
        public string? NameAndAddress3 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/3", Mandatory = false)]
        public string? NameAndAddress4 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/4", Mandatory = false)]
        public string? NameAndAddress5 { get; set; }

        [EdiValue("X(70)", Path = "NAD/3/0", Mandatory = false)]
        public string? PartyName1 { get; set; }

        [EdiValue("X(70)", Path = "NAD/3/1", Mandatory = false)]
        public string? PartyName2 { get; set; }

        [EdiValue("X(70)", Path = "NAD/3/2", Mandatory = false)]
        public string? PartyName3 { get; set; }

        [EdiValue("X(70)", Path = "NAD/3/3", Mandatory = false)]
        public string? PartyName4 { get; set; }

        [EdiValue("X(70)", Path = "NAD/3/4", Mandatory = false)]
        public string? PartyName5 { get; set; }

        [EdiValue("X(3)", Path = "NAD/3/5", Mandatory = false)]
        public string? PartyNameFormatCode { get; set; }

        [EdiValue("X(256)", Path = "NAD/4/0", Mandatory = false)]
        public string? Street1 { get; set; }

        [EdiValue("X(256)", Path = "NAD/4/1", Mandatory = false)]
        public string? Street2 { get; set; }

        [EdiValue("X(256)", Path = "NAD/4/2", Mandatory = false)]
        public string? Street3 { get; set; }

        [EdiValue("X(256)", Path = "NAD/4/3", Mandatory = false)]
        public string? Street4 { get; set; }

        [EdiValue("X(35)", Path = "NAD/5", Mandatory = false)]
        public string? CityName { get; set; }

        [EdiValue("X(9)", Path = "NAD/6/0", Mandatory = false)]
        public string? CountrySubdivisionIdentifier { get; set; }

        [EdiValue("X(17)", Path = "NAD/6/1", Mandatory = false)]
        public string? CountryCodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "NAD/6/2", Mandatory = false)]
        public string? CountryCodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(70)", Path = "NAD/6/3", Mandatory = false)]
        public string? CountrySubdivisionName { get; set; }

        [EdiValue("X(17)", Path = "NAD/7", Mandatory = false)]
        public string? PostalIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "NAD/8", Mandatory = false)]
        public string? CountryIdentifier { get; set; }

    }

    [EdiSegment, EdiPath("CTA")]
    public record CTA
    {

        [EdiValue("X(3)", Path = "CTA/0", Mandatory = false)]
        public string? ContactFunctionCode { get; set; }

        [EdiValue("X(17)", Path = "CTA/1/0", Mandatory = false)]
        public string? ContactIdentifier { get; set; }

        [EdiValue("X(256)", Path = "CTA/1/1", Mandatory = false)]
        public string? ContactName { get; set; }
    }

    [EdiSegment, EdiPath("RFF")]
    public record RFF
    {
        [EdiValue("X(3)", Path = "RFF/0/0", Mandatory = true)]
        public string ReferenceCodeQualifier { get; set; }

        [EdiValue("X(70)", Path = "RFF/0/1", Mandatory = false)]
        public string? ReferenceIdentifier { get; set; }

        [EdiValue("X(6)", Path = "RFF/0/2", Mandatory = false)]
        public string? DocumentLineIdentifier { get; set; }

        [EdiValue("X(9)", Path = "RFF/0/3", Mandatory = false)]
        public string? VersionIdentifier { get; set; }

        [EdiValue("X(6)", Path = "RFF/0/4", Mandatory = false)]
        public string? RevisionIdentifier { get; set; }

    }

    [EdiSegment, EdiPath("COM")]
    public record COM
    {

        [EdiValue("X(512)", Path = "COM/0/0", Mandatory = true)]
        public string AddressIdentifier { get; set; }

        [EdiValue("X(3)", Path = "COM/0/1", Mandatory = true)]
        public string CommMeansTypeCode { get; set; }
    }

    [EdiSegment, EdiPath("UNT")]
    public record UNT
    {
        [EdiValue("9(10)", Path = "UNT/0", Description = "Number of Segments in Message", Mandatory = true)]
        [EdiCount(EdiCountScope.Segments)]
        public int SegmentCount { get; set; }

        [EdiValue("X(14)", Path = "UNT/1", Description = "Message Reference Number", Mandatory = true)]
        public string MessageRefNum { get; set; }
    }

    #endregion
}

