using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class X12_832_PriceScalesCatalog
{
    [EdiValue("X(2)", Path = "BCT/0", Description = "BCT01 - Catalog Purpose Code")]
    public string CatalogPurposeCode { get; set; }

    [EdiValue("X(15)", Path = "BCT/1", Description = "BCT02 - Catalog Number")]
    public string CatalogNumber { get; set; }

    [EdiValue("X(2)", Path = "BCT/9", Description = "BCT10 - Transaction Set Purpose Code")]
    public string TransactionSetPurposeCode { get; set; }

    public List<PartyIdentification> Parties { get; set; }

    public List<ItemDetail> ItemDetails { get; set; }

    [EdiValue("X(3)", Path = "CUR/1", Description = "CUR02 - Currency Code")]
    public string CurrencyCode { get; set; }

    [EdiSegment, EdiPath("N1")]
    public class PartyIdentification
    {
        [EdiValue("9(3)", Path = "N1/0", Description = "N101 - Party Identification")]
        public string PublicIdentification { get; set; }
        [EdiValue("X(60)", Path = "N1/1/0", Description = "N102 - Name")]
        public string Name { get; set; }
        [EdiValue("9(2)", Path = "N1/2", Description = "N103 - Identification Code Qualifier")]
        public string IdentificationCodeQualifier { get; set; }
        [EdiValue("X(80)", Path = "N1/3", Description = "N104 - Idenfication Code")]
        public string IdentificationCode { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("LIN", "PID", Description = "LIN - Item Identification")]
    public class ItemDetail
    {
        [EdiValue("X(20)", Path = "LIN/0")]
        public int Index { get; set; }
        [EdiValue("9(2)", Path = "LIN/1")]
        public string ProductServiceIDQualifier1 { get; set; }
        [EdiValue("X(80)", Path = "LIN/2")]
        public string ProductServiceID1 { get; set; }
        [EdiValue("9(2)", Path = "LIN/3")]
        public string ProductServiceIDQualifier2 { get; set; }
        [EdiValue("X(80)", Path = "LIN/4")]
        public string ProductServiceID2 { get; set; }
        public List<ItemDescription> Descriptions { get; set; }
    }

    [EdiSegment, EdiPath("PID")]
    public class ItemDescription
    {
        [EdiValue("9(1)", Path = "PID/0", Description = "PID01 - Item Description Type")]
        public char ItemDescriptionType { get; set; }
        [EdiValue("X(3)", Path = "PID/1", Description = "PID02 - Product/Process Characteristic Code")]
        public string ProductProcessCharacteristicCode { get; set; }
        [EdiValue("9(2)", Path = "PID/2", Description = "PID03 - Agency Qualifier Code")]
        public string AgencyQualifierCode { get; set; }
        [EdiValue("X(12)", Path = "PID/3", Description = "PID04 - Product Description Code")]
        public string ProductDescriptionCode { get; set; }
        [EdiValue("X(80)", Path = "PID/4", Description = "PID05 - Description")]
        public string Description { get; set; }
        [EdiValue("9(1)", Path = "PID/7", Description = "PID08 - YesNo Condition or Response Code")]
        public int YesNoCondition { get; set; }
        [EdiValue("X(80)", Path = "PID/8", Description = "PID09 - Language Code")]
        public string LanguageCode { get; set; }
    }
}
