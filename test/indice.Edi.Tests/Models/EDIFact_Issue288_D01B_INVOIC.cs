using System.Globalization;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models.Issue288;

/// <summary>
/// https://www.stylusstudio.com/edifact/D04B/INVOIC.htm
/// </summary>
public class EDIFact_Issue288_D01B_INVOIC
{
    public UNB Header { get; set; }
    public List<Msg> Msgs { get; set; }
    public UNZ Footer { get; set; }

    [EdiMessage]
    public class Msg
    {

        //HEADER SECTION
        public UNH MsgHeader { get; set; }

        //Other message fields
        public BeginingOfMessage BeginOfMsg { get; set; }
        [EdiCondition("137", Path = "DTM/0/0")]
        public DTM HeadDate { get; set; }
        [EdiCondition("35", Path = "DTM/0/0")]
        public DTM DeliveryDate { get; set; }
        public PAI Payment_instructions { get; set; }
        public List<ALI> Additional_information { get; set; } = [];
        public IMD Item_description { get; set; }
        public List<FTX> FreeText { get; set; } = [];
        public List<LOC> Place_location { get; set; } = [];

        public List<GEI> Processing_information { get; set; } = [];
        public DGS Dangerous_goods { get; set; }
        public List<GIR> Related_identification_numbers { get; set; } = [];

        public List<SG1_RFF_DTM_GIR_LOC_MEA_QTY_FTX_MOA_RTE> SegmentGroup1 { get; set; } = [];
        public List<SG2_NAD_LOC_FII_MOA_SG3_SG4_SG5> SegmentGroup2 { get; set; } = [];
        public List<SG6_TAX_MOA_LOC> SegmentGroup6 { get; set; } = [];
        public List<SG7_CUX_DTM> SegmentGroup7 { get; set; } = [];
        public List<SG8_PAT_DTM_PCD_MOA_PAI_FII> SegmentGroup8 { get; set; } = [];
        public List<SG9_TDT_TSR_SG10_SG11> SegmentGroup9 { get; set; } = [];
        public List<SG12_TOD_LOC> SegmentGroup12 { get; set; } = [];
        public List<SG13_EQD_SEL> SegmentGroup13 { get; set; } = [];
        public List<SG14_PAC_MEA_EQD_SG15> SegmentGroup14 { get; set; } = [];
        public List<SG16_ALC_ALI_FTX_SG17_SG18_SG19_SG20_SG21_SG22> SegmentGroup16 { get; set; } = [];
        public List<SG23_RCS_RFF_DTM_FTX> SegmentGroup23 { get; set; } = [];
        public List<SG24_AJT_FTX> SegmentGroup24 { get; set; } = [];
        public List<SG25_INP_FTX> SegmentGroup25 { get; set; } = [];

        //DETAIL SECTION
        public List<SG26_LIN_PIA_IMD_MEA_QTY_PCD_ALI_DTM_GIN_GIR_QVR_DOC_PAI_FTX> SegmentGroup26 { get; set; } = [];

        //SUMMARY SECTION

        public UNS UNS { get; set; }
        public CNT CNT { get; set; }
        public MOA MOA { get; set; }
        public UNT UNT { get; set; }
    }

    [EdiSegmentGroup("RFF", "DTM","GIR","LOC","MEA","QTY","FTX","MOA","RTE")]
    public class SG1_RFF_DTM_GIR_LOC_MEA_QTY_FTX_MOA_RTE : RFF
    {
        public DTM DTM { get; set; }
        public GIR GIR { get; set; }
        public LOC LOC { get; set; }
        public MEA MEA { get; set; }
        public QTY QTY { get; set; }
        public FTX FTX { get; set; }
        public MOA MOA { get; set; }
        public RTE RTE { get; set; }
    }

    /// <summary>
    /// Segment group 2:  NAD-LOC-FII-MOA-SG3-SG4-SG5
    /// </summary>
    [EdiSegmentGroup("NAD", "LOC","FII","MOA", "RFF", "DOC", "CTA")]
    public class SG2_NAD_LOC_FII_MOA_SG3_SG4_SG5 : NAD
    {
        public LOC LOC { get; set; }
        public FII FII { get; set; }
        public MOA MOA { get; set; }
        public SG3_RFF_DTM SG3 { get; set; }
        public SG4_DOC_DTM SG4 { get; set; }
        public SG5_CTA_COM SG5 { get; set; }
    }

    /// <summary>
    /// Segment group 3:  RFF-DTM
    /// </summary>
    [EdiSegmentGroup("RFF", "DTM")]
    public class SG3_RFF_DTM : RFF
    {
        public DTM DTM { get; set; }
    }

    /// <summary>
    /// Segment group 4:  DOC-DTM
    /// </summary>
    [EdiSegmentGroup("DOC", "DTM")]
    public class SG4_DOC_DTM : DOC
    {
        public DTM DTM { get; set; }
    }

    /// <summary>
    /// Segment group 5:  CTA-COM
    /// </summary>
    [EdiSegmentGroup("CTA", "COM")]
    public class SG5_CTA_COM : CTA
    {
        public COM COM { get; set; }
    }

    /// <summary>
    /// Segment group 6:  TAX-MOA-LOC
    /// </summary>
    [EdiSegmentGroup("TAX", "MOA", "LOC")]
    public class SG6_TAX_MOA_LOC : TAX
    {
        [EdiCondition("79", Path = "MOA/0/0")]
        public MOA Amount { get; set; }

        public LOC LOC { get; set; }

        public override string ToString() => $"{Percent}%: {Amount.Amount}";
    }


    /// <summary>
    /// Segment group 7:  CUX-DTM
    /// </summary>
    [EdiSegmentGroup("CUX", "DTM")]
    public class SG7_CUX_DTM : CUX
    {
        public DTM DTM { get; set; }
    }

    /// <summary>
    /// Segment group 8:  PAT-DTM-PCD-MOA-PAI-FII
    /// </summary>
    [EdiSegmentGroup("PAT", "DTM", "PCD", "MOA", "PAI", "FII")]
    public class SG8_PAT_DTM_PCD_MOA_PAI_FII : PAT
    {
        public List<DTM> DTM { get; set; } = [];
        public PCD PCD { get; set; }
        public MOA MOA { get; set; }
        public PAI PAI { get; set; }
        public FII FII { get; set; }
    }

    /// <summary>
    /// Segment group 9:  TDT-LOC-SG10
    /// </summary>
    [EdiSegmentGroup("TDT", "TSR", "LOC", "RFF")]
    public class SG9_TDT_TSR_SG10_SG11 : TDT
    {
        public TSR TSR { get; set; }
        public SG10_LOC_DTM SG10 { get; set; }
        public SG11_RFF_DTM SG11 { get; set; }
    }

    /// <summary>
    /// Segment group 10:  LOC-DTM
    /// </summary>
    [EdiSegmentGroup("LOC", "DTM")]
    public class SG10_LOC_DTM : LOC
    {
        public DTM DTM { get; set; }
    }

    /// <summary>
    /// Segment group 11:  RFF-DTM
    /// </summary>
    [EdiSegmentGroup("RFF", "DTM")]
    public class SG11_RFF_DTM : RFF
    {
        public List<DTM> DTM { get; set; } = [];
    }

    /// <summary>
    /// Segment group 10:  TSR-RFF-LOC-TPL-FTX
    /// </summary>
    [EdiSegmentGroup("TSR", "RFF", "LOC", "TPL", "FTX")]
    public class SG10_TSR_RFF_LOC_TPL_FTX : TSR
    {
        public RFF RFF { get; set; }
        public LOC LOC { get; set; }
        public TPL TPL { get; set; }
        public List<FTX> FTX { get; set; } = [];
    }

    /// <summary>
    /// Segment group 12:  TOD-LOC
    /// </summary>
    [EdiSegmentGroup("TOD", "LOC")]
    public class SG12_TOD_LOC : TOD
    {
        public List<LOC> LOC { get; set; } = [];
    }
    /// <summary>
    /// Segment group 13:  EQD-SEL
    /// </summary>
    [EdiSegmentGroup("EQD", "SEL")]
    public class SG13_EQD_SEL : EQD
    {
        public SEL SEL { get; set; }
    }

    /// <summary>
    /// Segment group 14:  PAC-MEA-EQD-SG15
    /// </summary>
    [EdiSegmentGroup("PAC", "MEA", "EQD", "PCI")]
    public class SG14_PAC_MEA_EQD_SG15 : PAC
    {
        public List<MEA> MEA { get; set; } = [];
        public List<EQD> EQD { get; set; } = [];

        public SG15_PCI_RFF_DTM_GIN SegmentGroup15 { get; set; }
    }

    /// <summary>
    /// Segment group 15:  PCI-RFF-DTM-GIN
    /// </summary>
    [EdiSegmentGroup("PCI", "RFF", "DTM", "GIN")]
    public class SG15_PCI_RFF_DTM_GIN : PCI
    {
        public RFF RFF { get; set; }
        public List<DTM> DTM { get; set; } = [];
        public List<GIN> GIN { get; set; } = [];
    }

    /// <summary>
    /// Segment group 16:  ALC-ALI-DTM-SG16-SG19-SG20-SG21
    /// </summary>
    [EdiSegmentGroup("ALC", "ALI", "FTX", "RFF", "QTY", "PCD", "MOA", "RTE", "TAX")]
    public class SG16_ALC_ALI_FTX_SG17_SG18_SG19_SG20_SG21_SG22 : ALC
    {
        public ALI ALI { get; set; }
        public FTX FTX { get; set; }
        public SG17_RFF_DTM SG17 { get; set; }
        public SG18_QTY_RNG SG18 { get; set; }
        public SG19_PCD_RNG SG19 { get; set; }
        public SG20_MOA_RNG_CUX_DTM SG20 { get; set; }
        public SG21_RTE_RNG SG21 { get; set; }
        public SG22_TAX_MOA SG22 { get; set; }
    }

    /// <summary>
    /// Segment group 17:  RFF-DTM
    /// </summary>
    [EdiSegmentGroup("RFF", "DTM")]
    public class SG17_RFF_DTM : RFF
    {
        public List<DTM> DTM { get; set; } = [];
    }

    /// <summary>
    /// Segment group 18:  QTY-RNG
    /// </summary>
    [EdiSegmentGroup("QTY", "RNG")]
    public class SG18_QTY_RNG : QTY
    {
        public RNG RNG { get; set; }
    }

    /// <summary>
    /// Segment group 19:  PCD-RNG
    /// </summary>
    [EdiSegmentGroup("PCD", "RNG")]
    public class SG19_PCD_RNG : PCD
    {
        public RNG RNG { get; set; }
    }

    /// <summary>
    /// Segment group 20:  MOA-RNG-CUX-DTM
    /// </summary>
    [EdiSegmentGroup("MOA", "RNG", "CUX", "DTM")]
    public class SG20_MOA_RNG_CUX_DTM : MOA
    {
        public RNG RNG { get; set; }
        public CUX CUX { get; set; }
        public List<DTM> DTM { get; set; } = [];
    }

    /// <summary>
    /// Segment group 21:  RTE-RNG
    /// </summary>
    [EdiSegmentGroup("RTE", "RNG")]
    public class SG21_RTE_RNG : RTE
    {
        public RNG RNG { get; set; }
    }

    /// <summary>
    /// Segment group 22:  TAX-MOA
    /// </summary>
    [EdiSegmentGroup("TAX", "MOA")]
    public class SG22_TAX_MOA : TAX
    {
        public MOA MOA { get; set; }
    }

    /// <summary>
    /// Segment group 23:  RCS-RFF-DTM-FTX
    /// </summary>
    [EdiSegmentGroup("RCS", "RFF", "DTM", "FTX")]
    public class SG23_RCS_RFF_DTM_FTX : RCS
    {
        public List<RFF> RFF { get; set; } = [];
        public List<DTM> DTM { get; set; } = [];
        public List<FTX> FTX { get; set; } = [];
    }
    /// <summary>
    /// Segment group 24:  AJT-FTX
    /// </summary>
    [EdiSegmentGroup("AJT", "FTX")]
    public class SG24_AJT_FTX : AJT
    {
        public FTX FTX { get; set; }
    }

    /// <summary>
    /// Segment group 25:  INP-FTX
    /// </summary>
    [EdiSegmentGroup("INP", "FTX")]
    public class SG25_INP_FTX : INP
    {
        public FTX FTX { get; set; }
    }
    /// <summary>
    /// Segment group 25:  LIN-PIA-IMD-MEA-QTY-PCD-ALI-DTM-GIN-GIR-QVR-DOC-PAI-FTX-SG26-SG27-SG28-SG29-SG31-SG34-SG38-SG44-SG48-SG49-SG50
    /// </summary>
    [EdiSegmentGroup("LIN", "PIA", "PGI", "IMD", "MEA", "QTY", "PCD", "ALI", "DTM", "GIN", "GIR", "QVR", "DOC", "PAI", "FTX", "CCI", "PRI", "RFF", "PAC", "LOC", "TAX", "NAD", "ALC", "TDT", "TOD", "RCS")]
    public class SG26_LIN_PIA_IMD_MEA_QTY_PCD_ALI_DTM_GIN_GIR_QVR_DOC_PAI_FTX : LIN
    {
        public List<PIA> PIA { get; set; } = [];
        //public List<PGI>  PGI { get; set; } = [];
        public List<IMD> IMD { get; set; } = [];
        public List<MEA> MEA { get; set; } = [];
        public QTY QTY { get; set; }
        public List<PCD> PCD { get; set; } = [];
        public List<ALI> ALI { get; set; } = [];
        public List<DTM> DTM { get; set; } = [];
        public List<GIN> GIN { get; set; } = [];
        public List<GIR> GIR { get; set; } = [];
        public QVR QVR { get; set; }
        public EQD EQD { get; set; }
        public List<FTX> FTX { get; set; } = [];
        public DGS DGS { get; set; }
    }

    [EdiSegment, EdiPath("PCD")]
    public class PCD
    {
        [EdiValue("X(3)", Path = "PCD/0/0")]
        public string PercentageQualifier { get; set; }

        [EdiValue("9(10)", Path = "PCD/0/1")]
        public decimal? Percentage { get; set; }

        [EdiValue("X(3)", Path = "PCD/0/2")]
        public string PercentageBasisCode { get; set; }

        [EdiValue("X(3)", Path = "PCD/0/3")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "PCD/0/4")]
        public string CodeListResponsibleAgencyCode { get; set; }
    }

    [EdiSegment, EdiPath("TDT")]
    public class TDT
    {
        [EdiValue("X(3)", Path = "TDT/0/0")]
        public string TransportStageQualifier { get; set; }

        [EdiValue("X(17)", Path = "TDT/1/0")]
        public string ConveyanceReferenceNumber { get; set; }

        [EdiValue("X(3)", Path = "TDT/2/0")]
        public string ModeOfTransportCode { get; set; }

        [EdiValue("X(17)", Path = "TDT/2/1")]
        public string ModeOfTransportName { get; set; }

        [EdiValue("X(3)", Path = "TDT/3/0")]
        public string TransportMeansDescriptionCode { get; set; }

        [EdiValue("X(17)", Path = "TDT/3/1")]
        public string TransportMeansDescription { get; set; }

        [EdiValue("X(35)", Path = "TDT/4/0")]
        public string CarrierIdentification { get; set; }

        [EdiValue("X(3)", Path = "TDT/4/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "TDT/4/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "TDT/4/3")]
        public string CarrierName { get; set; }
    }

    [EdiSegment, EdiPath("TSR")]
    public class TSR
    {
        [EdiValue("X(17)", Path = "TSR/0/0")]
        public string ContractAndCarriageConditionCode { get; set; }

        [EdiValue("X(3)", Path = "TSR/0/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "TSR/0/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(70)", Path = "TSR/1/0")]
        public string ServiceDescription1 { get; set; }

        [EdiValue("X(70)", Path = "TSR/1/1")]
        public string ServiceDescription2 { get; set; }
    }

    [EdiSegment, EdiPath("TPL")]
    public class TPL
    {
        [EdiValue("X(3)", Path = "TPL/0/0")]
        public string TransportChargesMethodOfPaymentCode { get; set; }
    }

    [EdiSegment, EdiPath("TOD")]
    public class TOD
    {
        [EdiValue("X(3)", Path = "TOD/0/0")]
        public string TermsOfDeliveryOrTransportFunctionCode { get; set; }

        [EdiValue("X(3)", Path = "TOD/1/0")]
        public string TransportChargesMethodOfPaymentCode { get; set; }

        [EdiValue("X(17)", Path = "TOD/2/0")]
        public string TermsOfDeliveryOrTransportCode { get; set; }

        [EdiValue("X(3)", Path = "TOD/2/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "TOD/2/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(70)", Path = "TOD/2/3")]
        public string TermsOfDeliveryOrTransport1 { get; set; }

        [EdiValue("X(70)", Path = "TOD/2/4")]
        public string TermsOfDeliveryOrTransport2 { get; set; }
    }

    [EdiSegment, EdiPath("PAC")]
    public class PAC
    {
        [EdiValue("9(8)", Path = "PAC/0/0")]
        public int? NumberOfPackages { get; set; }

        [EdiValue("X(17)", Path = "PAC/1/0")]
        public string PackagingLevelCode { get; set; }

        [EdiValue("X(3)", Path = "PAC/1/1")]
        public string PackagingRelatedInformationCode { get; set; }

        [EdiValue("X(3)", Path = "PAC/1/2")]
        public string PackagingTermsAndConditionsCode { get; set; }

        [EdiValue("X(17)", Path = "PAC/2/0")]
        public string PackageTypeCode { get; set; }

        [EdiValue("X(3)", Path = "PAC/2/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "PAC/2/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "PAC/2/3")]
        public string TypeOfPackages { get; set; }
    }

    [EdiSegment, EdiPath("EQD")]
    public class EQD
    {
        [EdiValue("X(3)", Path = "EQD/0/0")]
        public string EquipmentQualifier { get; set; }

        [EdiValue("X(17)", Path = "EQD/1/0")]
        public string EquipmentIdentificationNumber { get; set; }

        [EdiValue("X(3)", Path = "EQD/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "EQD/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(17)", Path = "EQD/2/0")]
        public string EquipmentSizeAndTypeCode { get; set; }

        [EdiValue("X(3)", Path = "EQD/2/1")]
        public string CodeListIdentificationCode2 { get; set; }

        [EdiValue("X(3)", Path = "EQD/2/2")]
        public string CodeListResponsibleAgencyCode2 { get; set; }

        [EdiValue("X(35)", Path = "EQD/2/3")]
        public string EquipmentSizeAndType { get; set; }
    }

    [EdiSegment, EdiPath("PCI")]
    public class PCI
    {
        [EdiValue("X(3)", Path = "PCI/0/0")]
        public string MarkingInstructionsCode { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/0")]
        public string ShippingMarks1 { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/1")]
        public string ShippingMarks2 { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/2")]
        public string ShippingMarks3 { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/3")]
        public string ShippingMarks4 { get; set; }

        [EdiValue("X(35)", Path = "PCI/1/4")]
        public string ShippingMarks5 { get; set; }
    }

    [EdiSegment, EdiPath("GIN")]
    public class INP
    {
        [EdiValue("X(35)", Path = "*/0/0")]
        public string Enacting_party_identifier { get; set; }
        [EdiValue("X(35)", Path = "*/0/1")]
        public string Instruction_receiving_party_identifier { get; set; }
        [EdiValue("X(3)", Path = "*/1/0")]
        public string Instruction_type_code_qualifier { get; set; }
        [EdiValue("X(3)", Path = "*/2/0")]
        public string Status_description_code { get; set; }
    }


    [EdiSegment, EdiPath("GIN")]
    public class GIN
    {
        [EdiValue("X(3)", Path = "GIN/0/0")]
        public string IdentityNumberQualifier { get; set; }

        [EdiValue("X(35)", Path = "GIN/1/0")]
        public string IdentityNumber1 { get; set; }

        [EdiValue("X(35)", Path = "GIN/1/1")]
        public string IdentityNumber2 { get; set; }

        [EdiValue("X(35)", Path = "GIN/1/2")]
        public string IdentityNumber3 { get; set; }

        [EdiValue("X(35)", Path = "GIN/1/3")]
        public string IdentityNumber4 { get; set; }

        [EdiValue("X(35)", Path = "GIN/1/4")]
        public string IdentityNumber5 { get; set; }
    }

    [EdiSegment, EdiPath("ALC")]
    public class ALC
    {
        [EdiValue("X(3)", Path = "ALC/0/0")]
        public string AllowanceOrChargeQualifier { get; set; }

        [EdiValue("X(3)", Path = "ALC/1/0")]
        public string AllowanceChargeIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "ALC/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "ALC/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "ALC/2/0")]
        public string SettlementMeansCode { get; set; }

        [EdiValue("X(3)", Path = "ALC/3/0")]
        public string CalculationSequenceCode { get; set; }

        [EdiValue("X(17)", Path = "ALC/4/0")]
        public string SpecialServicesCode { get; set; }

        [EdiValue("X(3)", Path = "ALC/4/1")]
        public string CodeListIdentificationCode2 { get; set; }

        [EdiValue("X(3)", Path = "ALC/4/2")]
        public string CodeListResponsibleAgencyCode2 { get; set; }
    }

    [EdiSegment, EdiPath("RNG")]
    public class RNG
    {
        [EdiValue("X(3)", Path = "RNG/0/0")]
        public string RangeTypeQualifier { get; set; }

        [EdiValue("X(18)", Path = "RNG/1/0")]
        public string MinimumValue { get; set; }

        [EdiValue("X(18)", Path = "RNG/1/1")]
        public string MaximumValue { get; set; }

        [EdiValue("X(3)", Path = "RNG/1/2")]
        public string MeasureUnitQualifier { get; set; }
    }

    [EdiSegment, EdiPath("RTE")]
    public class RTE
    {
        [EdiValue("X(3)", Path = "RTE/0/0")]
        public string RateTypeQualifier { get; set; }

        [EdiValue("9(15)", Path = "RTE/0/1")]
        public decimal? UnitPriceBasis { get; set; }

        [EdiValue("X(9)", Path = "RTE/0/2")]
        public decimal? UnitPriceBasisValue { get; set; }

        [EdiValue("X(3)", Path = "RTE/0/3")]
        public string MeasureUnitQualifier { get; set; }
    }

    [EdiSegment, EdiPath("RCS")]
    public class RCS
    {
        [EdiValue("X(3)", Path = "RCS/0/0")]
        public string SectorSubjectIdentificationQualifier { get; set; }

        [EdiValue("X(17)", Path = "RCS/1/0")]
        public string RequirementConditionCode { get; set; }

        [EdiValue("X(3)", Path = "RCS/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "RCS/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "RCS/1/3")]
        public string RequirementOrCondition { get; set; }

        [EdiValue("X(3)", Path = "RCS/2/0")]
        public string ActionRequestNotificationCode { get; set; }

        [EdiValue("X(3)", Path = "RCS/3/0")]
        public string CountryNameCode { get; set; }
    }

    [EdiSegment, EdiPath("LIN")]
    public class LIN
    {
        [EdiValue("9(6)", Path = "LIN/0/0")]
        public int? LineItemNumber { get; set; }

        [EdiValue("X(3)", Path = "LIN/1/0")]
        public string ActionRequestNotificationCode { get; set; }

        [EdiValue("X(35)", Path = "LIN/2/0")]
        public string ItemNumber { get; set; }

        [EdiValue("X(3)", Path = "LIN/2/1")]
        public string ItemNumberTypeCode { get; set; }

        [EdiValue("X(3)", Path = "LIN/2/2")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "LIN/2/3")]
        public string CodeListResponsibleAgencyCode { get; set; }
    }

    [EdiSegment, EdiPath("PIA")]
    public class PIA
    {
        [EdiValue("X(3)", Path = "PIA/0/0")]
        public string ProductIdentificationFunctionQualifier { get; set; }

        [EdiValue("X(35)", Path = "PIA/1/0")]
        public string ItemNumber1 { get; set; }

        [EdiValue("X(3)", Path = "PIA/1/1")]
        public string ItemNumberTypeCode1 { get; set; }

        [EdiValue("X(3)", Path = "PIA/1/2")]
        public string CodeListIdentificationCode1 { get; set; }

        [EdiValue("X(3)", Path = "PIA/1/3")]
        public string CodeListResponsibleAgencyCode1 { get; set; }
    }

    [EdiSegment, EdiPath("QVR")]
    public class QVR
    {
        [EdiValue("9(35)", Path = "QVR/0/0")]
        public decimal? QuantityVariance { get; set; }

        [EdiValue("X(3)", Path = "QVR/0/1")]
        public string QuantityVarianceReasonCode { get; set; }
    }

    [EdiSegment, EdiPath("NAD")]
    public class NAD
    {
        [EdiValue("X(3)", Path = "NAD/0/0")]
        public string PartyQualifier { get; set; }

        [EdiValue("X(35)", Path = "NAD/1/0")]
        public string PartyIdentification { get; set; }

        [EdiValue("X(3)", Path = "NAD/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "NAD/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/0")]
        public string NameAndAddressLine1 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/1")]
        public string NameAndAddressLine2 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/2")]
        public string NameAndAddressLine3 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/3")]
        public string NameAndAddressLine4 { get; set; }

        [EdiValue("X(35)", Path = "NAD/2/4")]
        public string NameAndAddressLine5 { get; set; }

        [EdiValue("X(35)", Path = "NAD/3/0")]
        public string PartyName1 { get; set; }

        [EdiValue("X(35)", Path = "NAD/3/1")]
        public string PartyName2 { get; set; }

        [EdiValue("X(35)", Path = "NAD/3/2")]
        public string PartyName3 { get; set; }

        [EdiValue("X(35)", Path = "NAD/3/3")]
        public string PartyName4 { get; set; }

        [EdiValue("X(35)", Path = "NAD/3/4")]
        public string PartyName5 { get; set; }

        [EdiValue("X(3)", Path = "NAD/3/5")]
        public string PartyNameFormatCode { get; set; }

        [EdiValue("X(35)", Path = "NAD/4/0")]
        public string StreetAndNumberPOBox1 { get; set; }

        [EdiValue("X(35)", Path = "NAD/4/1")]
        public string StreetAndNumberPOBox2 { get; set; }

        [EdiValue("X(35)", Path = "NAD/4/2")]
        public string StreetAndNumberPOBox3 { get; set; }

        [EdiValue("X(35)", Path = "NAD/4/3")]
        public string StreetAndNumberPOBox4 { get; set; }

        [EdiValue("X(35)", Path = "NAD/5/0")]
        public string CityName { get; set; }

        [EdiValue("X(9)", Path = "NAD/6/0")]
        public string CountrySubEntityIdentification { get; set; }

        [EdiValue("X(17)", Path = "NAD/7/0")]
        public string PostalIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "NAD/8/0")]
        public string CountryCode { get; set; }
    }

    [EdiSegment, EdiPath("LOC")]
    public class LOC
    {
        [EdiValue("X(3)", Path = "LOC/0/0")]
        public string LocationQualifier { get; set; }

        [EdiValue("X(35)", Path = "LOC/1/0")]
        public string LocationIdentification { get; set; }

        [EdiValue("X(3)", Path = "LOC/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "LOC/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(70)", Path = "LOC/1/3")]
        public string LocationName { get; set; }

        [EdiValue("X(25)", Path = "LOC/2/0")]
        public string RelatedLocationOneIdentification { get; set; }

        [EdiValue("X(3)", Path = "LOC/2/1")]
        public string CodeListIdentificationCode2 { get; set; }

        [EdiValue("X(3)", Path = "LOC/2/2")]
        public string CodeListResponsibleAgencyCode2 { get; set; }

        [EdiValue("X(70)", Path = "LOC/2/3")]
        public string RelatedLocationOneName { get; set; }

        [EdiValue("X(25)", Path = "LOC/3/0")]
        public string RelatedLocationTwoIdentification { get; set; }

        [EdiValue("X(3)", Path = "LOC/3/1")]
        public string CodeListIdentificationCode3 { get; set; }

        [EdiValue("X(3)", Path = "LOC/3/2")]
        public string CodeListResponsibleAgencyCode3 { get; set; }

        [EdiValue("X(70)", Path = "LOC/3/3")]
        public string RelatedLocationTwoName { get; set; }

        [EdiValue("X(3)", Path = "LOC/4/0")]
        public string RelationCode { get; set; }
    }

    [EdiSegment, EdiPath("MEA")]
    public class MEA
    {
        [EdiValue("X(3)", Path = "MEA/0/0")]
        public string MeasurementPurposeQualifier { get; set; }

        [EdiValue("X(17)", Path = "MEA/1/0")]
        public string MeasurementDimensionCode { get; set; }

        [EdiValue("X(3)", Path = "MEA/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "MEA/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(70)", Path = "MEA/1/3")]
        public string MeasurementDimension { get; set; }

        [EdiValue("X(3)", Path = "MEA/2/0")]
        public string MeasurementSignificanceCode { get; set; }

        [EdiValue("X(3)", Path = "MEA/3/0")]
        public string MeasureUnitQualifier { get; set; }

        [EdiValue("9(18)", Path = "MEA/3/1")]
        public decimal? MeasurementValue { get; set; }

        [EdiValue("9(10)", Path = "MEA/3/2")]
        public decimal? RangeMinimum { get; set; }

        [EdiValue("9(10)", Path = "MEA/3/3")]
        public decimal? RangeMaximum { get; set; }

        [EdiValue("9(2)", Path = "MEA/3/4")]
        public int? SignificantDigits { get; set; }
    }

    [EdiSegment, EdiPath("QTY")]
    public class QTY
    {
        [EdiValue("X(3)", Path = "QTY/0/0")]
        public string QuantityQualifier { get; set; }

        [EdiValue("9(35)", Path = "QTY/0/1")]
        public decimal? Quantity { get; set; }

        [EdiValue("X(3)", Path = "QTY/0/2")]
        public string MeasureUnitQualifier { get; set; }
    }

    [EdiSegment, EdiPath("PAI")]
    public class PAI
    {
        [EdiValue("X(3)", Path = "PAI/0/0")]
        public string PaymentInstructionCode { get; set; }

        [EdiValue("X(3)", Path = "PAI/1/0")]
        public string PaymentMeansCode { get; set; }

        [EdiValue("X(17)", Path = "PAI/1/1")]
        public string PaymentChannelCode { get; set; }
    }

    [EdiSegment, EdiPath("ALI")]
    public class ALI
    {
        [EdiValue("X(3)", Path = "ALI/0/0")]
        public string CountryOfOriginCode { get; set; }

        [EdiValue("X(3)", Path = "ALI/1/0")]
        public string TypeOfDutyRegimeCode { get; set; }

        [EdiValue("X(3)", Path = "ALI/2/0")]
        public string SpecialConditionCode1 { get; set; }

        [EdiValue("X(3)", Path = "ALI/2/1")]
        public string SpecialConditionCode2 { get; set; }

        [EdiValue("X(3)", Path = "ALI/2/2")]
        public string SpecialConditionCode3 { get; set; }

        [EdiValue("X(3)", Path = "ALI/2/3")]
        public string SpecialConditionCode4 { get; set; }

        [EdiValue("X(3)", Path = "ALI/2/4")]
        public string SpecialConditionCode5 { get; set; }
    }

    [EdiSegment, EdiPath("IMD")]
    public class IMD
    {
        [EdiValue("X(3)", Path = "IMD/0/0")]
        public string ItemDescriptionTypeCode { get; set; }

        [EdiValue("X(3)", Path = "IMD/1/0")]
        public string ItemCharacteristicCode { get; set; }

        [EdiValue("X(17)", Path = "IMD/2/0")]
        public string ItemDescriptionCode { get; set; }

        [EdiValue("X(3)", Path = "IMD/2/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "IMD/2/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(256)", Path = "IMD/2/3")]
        public string ItemDescription1 { get; set; }

        [EdiValue("X(256)", Path = "IMD/2/4")]
        public string ItemDescription2 { get; set; }

        [EdiValue("X(3)", Path = "IMD/3/0")]
        public string SurfaceLayerIndicatorCode { get; set; }
    }

    [EdiSegment, EdiPath("GEI")]
    public class GEI
    {
        [EdiValue("X(3)", Path = "GEI/0/0")]
        public string ProcessingIndicatorCode { get; set; }

        [EdiValue("X(17)", Path = "GEI/1/0")]
        public string ProcessTypeCode { get; set; }

        [EdiValue("X(3)", Path = "GEI/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "GEI/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(70)", Path = "GEI/1/3")]
        public string ProcessType { get; set; }
    }

    [EdiSegment, EdiPath("DGS")]
    public class DGS
    {
        [EdiValue("X(3)", Path = "DGS/0/0")]
        public string DangerousGoodsRegulationsCode { get; set; }

        [EdiValue("X(17)", Path = "DGS/1/0")]
        public string HazardCode { get; set; }

        [EdiValue("X(3)", Path = "DGS/1/1")]
        public string AdditionalHazardClassificationCode { get; set; }

        [EdiValue("X(3)", Path = "DGS/1/2")]
        public string HazardCodeVersionNumber { get; set; }

        [EdiValue("X(35)", Path = "DGS/2/0")]
        public string UNDGNumber { get; set; }
    }

    [EdiSegment, EdiPath("GIR")]
    public class GIR
    {
        [EdiValue("X(3)", Path = "GIR/0/0")]
        public string SetIdentificationQualifier { get; set; }

        [EdiValue("X(35)", Path = "GIR/1/0")]
        public string IdentityNumber1 { get; set; }

        [EdiValue("X(3)", Path = "GIR/1/1")]
        public string IdentityNumberQualifier1 { get; set; }

        [EdiValue("X(3)", Path = "GIR/1/2")]
        public string StatusCode1 { get; set; }
    }

    [EdiSegment, EdiPath("UNZ")]
    public class UNZ
    {
        [EdiValue("X(2)", Path = "UNZ/0/0")]
        public string Value1 { get; set; }
        [EdiValue("X(2)", Path = "UNZ/1/0")]
        public string Value2 { get; set; }
    }

    [EdiSegment, EdiPath("UNT")]
    public class UNT
    {
        [EdiValue("X(2)", Path = "UNT/0/0")]
        public string Value1 { get; set; }
        [EdiValue("X(2)", Path = "UNT/1/0")]
        public string Value2 { get; set; }
    }

    [EdiSegment, EdiPath("UNS")]
    public class UNS
    {
        [EdiValue("X(1)", Path = "UNS/0/0")]
        public string Qualifier { get; set; }
    }

    [EdiSegment, EdiPath("COM")]
    public class COM
    {

        [EdiValue("X(512)", Path = "COM/0/0", Mandatory = true)]
        public string AddressIdentifier { get; set; }

        [EdiValue("X(3)", Path = "COM/0/1", Mandatory = true)]
        public string CommMeansTypeCode { get; set; }
    }

    [EdiSegment, EdiPath("FII")]
    public class FII
    {
        [EdiValue("X(3)", Path = "*/0/0", Mandatory = true)]
        public string PartyFunctionCodeQualifier { get; set; }

        [EdiValue("X(35)", Path = "*/1/0")]
        public string AccountHolderIdentifier { get; set; }

        [EdiValue("X(35)", Path = "*/1/1")]
        public string AccountHolderName { get; set; }

        [EdiValue("X(35)", Path = "*/1/2")]
        public string AccountHolderName2 { get; set; }

        [EdiValue("X(3)", Path = "*/1/3")]
        public string CurrencyIdentificationCode { get; set; }

        [EdiValue("X(11)", Path = "*/2/0")]
        public string InstitutionNameCode { get; set; }

        [EdiValue("X(3)", Path = "*/2/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "*/2/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(17)", Path = "*/2/3")]
        public string InstitutionBranchIdentifier { get; set; }

        [EdiValue("X(3)", Path = "*/2/4")]
        public string CodeListIdentificationCode2 { get; set; }

        [EdiValue("X(3)", Path = "*/2/5")]
        public string CodeListResponsibleAgencyCode2 { get; set; }

        [EdiValue("X(70)", Path = "*/2/6")]
        public string InstitutionName { get; set; }

        [EdiValue("X(70)", Path = "*/2/7")]
        public string InstitutionBranchLocationName { get; set; }

        [EdiValue("X(3)", Path = "*/3/0")]
        public string CountryNameCode { get; set; }
    }

    [EdiSegment, EdiPath("PAT")]
    public class PAT
    {
        [EdiValue("X(3)", Path = "*/0/0", Mandatory = true)]
        public string PaymentTermsTypeCodeQualifier { get; set; }

        [EdiValue("X(17)", Path = "*/1/0")]
        public string PaymentTermsDescriptionIdentifier { get; set; }

        [EdiValue("X(3)", Path = "*/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "*/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "*/1/3")]
        public string PaymentTermsDescription { get; set; }

        [EdiValue("X(35)", Path = "*/1/4")]
        public string PaymentTermsDescription2 { get; set; }

        [EdiValue("X(3)", Path = "*/2/0")]
        public string TimeReferenceCode { get; set; }

        [EdiValue("X(3)", Path = "*/2/1")]
        public string TermsTimeRelationCode { get; set; }

        [EdiValue("X(3)", Path = "*/2/2")]
        public string PeriodTypeCode { get; set; }

        [EdiValue("9(3)", Path = "*/2/3")]
        public int? PeriodCountQuantity { get; set; }
    }

    [EdiSegment, EdiPath("DOC")]
    public class DOC
    {
        [EdiValue("X(3)", Path = "*/0/0")]
        public string DocumentNameCode { get; set; }
        [EdiValue("X(3)", Path = "*/0/1")]
        public string CodeListIdentificationCode { get; set; }
        [EdiValue("X(3)", Path = "*/0/2")]
        public string CodeListResponsibleAgencyCode { get; set; }
        [EdiValue("X(35)", Path = "*/0/3")]
        public string DocumentName { get; set; }
        [EdiValue("X(35)", Path = "*/1/0")]
        public string DocumentIdentifier { get; set; }
        [EdiValue("X(3)", Path = "*/1/1")]
        public string DocumentStatusCode { get; set; }
        [EdiValue("X(70)", Path = "*/1/2")]
        public string DocumentSourceDescription { get; set; }
        [EdiValue("X(3)", Path = "*/1/3")]
        public string LanguageNameCode { get; set; }
        [EdiValue("X(9)", Path = "*/1/4")]
        public string VersionIdentifier { get; set; }
        [EdiValue("X(6)", Path = "*/1/5")]
        public string RevisionIdentifier { get; set; }
        [EdiValue("X(3)", Path = "*/2/0")]
        public string COMMUNICATION_MEDIUM_TYPE_CODE { get; set; }
        [EdiValue("9(2)", Path = "*/3/0")]
        public int? DOCUMENT_COPIES_REQUIRED_QUANTITY { get; set; }
        [EdiValue("9(2)", Path = "*/4/0")]
        public int? DOCUMENT_ORIGINALS_REQUIRED_QUANTITY { get; set; }
    }


    [EdiSegment, EdiPath("CNT")]
    public class CNT
    {
        [EdiValue("9(3)", Path = "CNT/0/0")]
        public string CountQuali { get; set; }
        [EdiValue("9(3)", Path = "CNT/0/1")]
        public string LineCount { get; set; }
    }


    [EdiSegment, EdiPath("TAX")]
    public class TAX
    {
        [EdiValue("X(2)", Path = "TAX/0/0")]
        public string Qualifier { get; set; }
        [EdiValue("X(5)", Path = "TAX/1/0")]
        public string TaxType { get; set; }
        [EdiValue("X(7)", Path = "TAX/4/3")]
        public decimal Percent { get; set; }
        [EdiValue("X(3)", Path = "TAX/5/0")]
        public string Category { get; set; }
    }

    [EdiSegment, EdiPath("MOA")]
    public class MOA
    {
        public MOAQualifierType Type { 
            get => (MOAQualifierType)(int.Parse(Qualifier));
            set => Qualifier = ((int)value).ToString(); 
        }

        [EdiValue("X(3)", Path = "MOA/0/0")]
        public string Qualifier { get; set; }
        [EdiValue("9(16)", Path = "MOA/0/1")]
        public decimal Amount { get; set; }
        [EdiValue("X(3)", Path = "MOA/0/2")]
        public string Currency { get; set; }

        public override string ToString() {
            return $"{Currency}{Amount} ({Qualifier})";
        }

        public enum MOAQualifierType
        {
            DueAmount = 9,
            InvoiceAmount = 77,
            TotalLineItemsAmount = 79,
            TotalMonetaryAmount = 86,
            TaxValue = 124,
            TaxableValue = 125
        }
    }

    [EdiSegment, EdiPath("SEL")]
    public class SEL
    {
        [EdiValue("X(35)", Path = "SEL/0/0")]
        public string TransportUnitSealIdentifier { get; set; }

        [EdiValue("X(3)", Path = "SEL/1/0")]
        public string SealingPartyNameCode { get; set; }

        [EdiValue("X(17)", Path = "SEL/1/1")]
        public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "SEL/1/2")]
        public string CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "SEL/1/3")]
        public string SealingPartyName { get; set; }

        [EdiValue("X(3)", Path = "SEL/2/0")]
        public string SealConditionCode { get; set; }

        [EdiValue("X(35)", Path = "SEL/3/0")]
        public string ObjectIdentifier1 { get; set; }

        [EdiValue("X(35)", Path = "SEL/3/1")]
        public string ObjectIdentifier2 { get; set; }
    }



    [EdiSegment, EdiPath("FTX")]
    public class FTX
    {
        [EdiValue("X(3)", Path = "FTX/0/0")]
        public string Qualifier { get; set; }
        [EdiValue("X(3)", Path = "FTX/1/0")]
        public string FunctionCode { get; set; }
        [EdiValue("X(17)", Path = "FTX/2/0")]
        public string TextValueCode { get; set; }
        [EdiValue("X(17)", Path = "FTX/2/1")]
        public string CodeListId { get; set; }
        [EdiValue("X(3)", Path = "FTX/2/2")]
        public string CodeListAgency { get; set; }
        [EdiValue("X(512)", Path = "FTX/3/0")]
        public string Text { get; set; }
        [EdiValue("X(512)", Path = "FTX/3/1")]
        public string Text2 { get; set; }
        [EdiValue("X(3)", Path = "FTX/4/0")]
        public string Language { get; set; }
    }

    [EdiSegment, EdiPath("RFF")]
    public class RFF
    {
        [EdiPath("*/0")]
        public RffElement AbkommNr { get; set; }

        [EdiElement]
        public class RffElement
        {
            [EdiValue("X(3)", Path = "*/*/0")]
            public string Qualifier { get; set; }

            [EdiValue("X(50)", Path = "*/*/1")]
            public string Value { get; set; }
        }
    }


    [EdiSegment, EdiPath("CTA")]
    public class CTA
    {

        [EdiValue("X(3)", Path = "*/0", Mandatory = false)]
        public string ContactFunctionCode { get; set; }

        [EdiValue("X(17)", Path = "*/1/0", Mandatory = false)]
        public string ContactIdentifier { get; set; }

        [EdiValue("X(256)", Path = "*/1/1", Mandatory = false)]
        public string ContactName { get; set; }
    }

    [EdiSegment, EdiPath("AJT")]
    public class AJT
    {
        [EdiValue("X(3)", Path = "*/0/0")]
        public string Adjustment_reason_description_code { get; set; }
        [EdiValue("X(6)", Path = "*/0/1")]
        public string Line_item_identifier { get; set; }
    }

    [EdiSegment, EdiPath("CUX")]
    public class CUX
    {
        [EdiValue("X(3)", Path = "CUX/0/0")]
        public string CurrencyUsageCodeQualifier { get; set; }

        [EdiValue("X(3)", Path = "CUX/0/1")]
        public string CurrencyIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "CUX/0/2")]
        public string CurrencyTypeCodeQualifier { get; set; }

        [EdiValue("9(4)", Path = "CUX/0/3")]
        public decimal? CurrencyRate { get; set; }

        [EdiValue("X(3)", Path = "CUX/1/0")]
        public string CurrencyUsageCodeQualifier2 { get; set; }

        [EdiValue("X(3)", Path = "CUX/1/1")]
        public string CurrencyIdentificationCode2 { get; set; }

        [EdiValue("X(3)", Path = "CUX/1/2")]
        public string CurrencyTypeCodeQualifier2 { get; set; }

        [EdiValue("9(4)", Path = "CUX/1/3")]
        public decimal? CurrencyRate2 { get; set; }

        [EdiValue("9(12)", Path = "CUX/2/0")]
        public decimal? CurrencyExchangeRate { get; set; }

        [EdiValue("X(3)", Path = "CUX/3/0")]
        public string ExchangeRateCurrencyMarketIdentifier { get; set; }
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
        public string SenderIdCode { get; set; }
        [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
        public string ReverseRoutingAddress { get; set; }

        [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
        public string RecipientId { get; set; }

        [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = true)]
        public string RecipientIDCode { get; set; }
        [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
        public string RoutingAddress { get; set; }

        [EdiValue("9(6)", Path = "UNB/3/0", Format = "yyMMdd", Description = "Date of Preparation")]
        [EdiValue("9(4)", Path = "UNB/3/1", Format = "HHmm", Description = "Time of Preparation")]
        public DateTime DateOfPreparation { get; set; }

        [EdiValue("X(14)", Path = "UNB/4", Mandatory = true)]
        public string ControlRef { get; set; }

        //[EdiValue("9(1)", Path = "UNB/8", Mandatory = false)]
        //public int AckRequest { get; set; }
    }

    [EdiSegment, EdiPath("UNH")]
    public class UNH
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

    [EdiSegment, EdiPath("BGM")]
    public class BeginingOfMessage
    {
        public MessageName Name { get; set; }

        public MessageIdentification Identification { get; set; }
        [EdiValue("X(3)", Path = "BGM/2/0")]
        public string WhatEver { get; set; }

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
            public string MessageFunctionCode { get; set; }
            [EdiValue("X(3)", Path = "BGM/1/4")]
            public string ResponseTypeCode { get; set; }
            public override string ToString() {
                return $"{DocumentId}.{VersionId}.{RevisionId}";
            }
        }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DTM
    {
        [EdiPath("*/0")]
        public DateTimeElement DateTimePeriod { get; set; }

        public override string ToString() {
            return DateTimePeriod?.ToString() ?? base.ToString();
        }
    }

    [EdiElement]
    public class DateTimeElement
    {
        [EdiValue("9(3)", Path = "*/*/0")]
        public DateTimePeriodFunctionCode ID { get; set; }

        [EdiValue("X(35)", Path = "*/*/1")]
        public string Value { get; set; }

        [EdiValue("9(3)", Path = "*/*/2")]
        public int FormatQualifier { get; set; }

        public DateTime? AsDateTime() {
            if (string.IsNullOrWhiteSpace(Value)) return null;
            string[] formats = GetQualifierFormatString(FormatQualifier);
            if (DateTime.TryParseExact(Value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datetime))
                return datetime;
            return null;
        }

        public void FromDatetime(DateTime datetime) {
            string[] formats = GetQualifierFormatString(FormatQualifier);

            Value = datetime.ToString(formats[0]);
        }

        public override string ToString() {
            return AsDateTime().ToString() ?? base.ToString();
        }

        public static string[] GetQualifierFormatString(int formatQualifier) {
            string[] formats = null;
            switch (formatQualifier) {
                case 203:
                    formats = ["yyyyMMddHHmm"];
                    break;
                case 204:
                    formats = ["yyyyMMddHHmmss"];
                    break;
                case 201:
                    formats = ["yyMMddHHmm"];
                    break;
                case 102:
                    formats = ["yyyyMMdd"];
                    break;
            }

            return formats;
        }

        public enum DateTimePeriodFunctionCode
        {
            /// <summary>
            /// Actual date/time on which the service was completed.
            /// </summary>
            Service_completion_datetime_Actual = 1,

            /// <summary>
            /// Date on which buyer requests goods to be delivered.
            /// </summary>
            Delivery_datetime_requested = 2,

            /// <summary>
            /// Date when a Commercial Invoice is issued.
            /// </summary>
            Invoice_datetime = 3,

            /// <summary>
            /// Date when an order is issued.
            /// </summary>
            Order_datetime = 4,

            /// <summary>
            /// A period of time when saleable stocks are expected to cover demand for a product.
            /// </summary>
            Saleable_stock_demand_cover_period_expected = 5,

            /// <summary>
            /// The date an entity moved from a location.
            /// </summary>
            Moved_from_location_date = 6,

            /// <summary>
            /// Date and/or time at which specified event or document becomes effective.
            /// </summary>
            Effective_datetime = 7,

            /// <summary>
            /// Date/time when the purchase order is received by the seller.
            /// </summary>
            Order_received_datetime = 8,

            /// <summary>
            /// Date/time of processing.
            /// </summary>
            Processing_datetime = 9,

            /// <summary>
            /// Date on which goods should be shipped or despatched by the supplier.
            /// </summary>
            Shipment_datetime_requested = 10,

            /// <summary>
            /// Date/time on which the goods are or are expected to be despatched or shipped.
            /// </summary>
            Despatch_date_and_or_time = 11,

            /// <summary>
            /// Date by which payment should be made if discount terms are to apply.
            /// </summary>
            Terms_discount_due_datetime = 12,

            /// <summary>
            /// Date by which payment must be made.
            /// </summary>
            Terms_net_due_date = 13,

            /// <summary>
            /// Date/time when instalments are due.
            /// </summary>
            Payment_datetime_deferred = 14,

            /// <summary>
            /// Date/time when promotion activities begin.
            /// </summary>
            Promotion_start_datetime = 15,

            /// <summary>
            /// Date/time when promotion activities end.
            /// </summary>
            Promotion_end_datetime = 16,

            /// <summary>
            /// Date and/or time when the shipper of the goods expects delivery will take place.
            /// </summary>
            Delivery_datetime_estimated = 17,

            /// <summary>
            /// Installation date/time/period.
            /// </summary>
            Installation_datetimeperiod = 18,

            /// <summary>
            /// Period of time between slaughter and delivery during which meat is ageing.
            /// </summary>
            Meat_ageing_period = 19,

            /// <summary>
            /// Date/time when cheque is issued.
            /// </summary>
            Cheque_datetime = 20,

            /// <summary>
            /// Date/time when charge back is made.
            /// </summary>
            Charge_back_datetime = 21,

            /// <summary>
            /// Date/time when freight bill is issued.
            /// </summary>
            Freight_bill_datetime = 22,

            /// <summary>
            /// Actual date/time of the reconditioning of a piece of equipment.
            /// </summary>
            Equipment_reconditioning_datetime_actual = 23,

            /// <summary>
            /// Date and time when a transfer note is recognised as being valid by the carrier.
            /// </summary>
            Transfer_note_acceptance_date_and_time = 24,

            /// <summary>
            /// Date/time on which goods or consignment are delivered at their destination.
            /// </summary>
            Delivery_datetime_actual = 35,

            /// <summary>
            /// Date of expiry of the validity of a referenced document, price information or any other referenced data element with a limited validity period.
            /// </summary>
            Expiry_date = 36,

            /// <summary>
            /// Goods should not be shipped before given date/time.
            /// </summary>
            Ship_not_before_datetime = 37,

            /// <summary>
            /// Date/time by which the goods should have been shipped.
            /// </summary>
            Ship_not_later_than_datetime = 38,

            /// <summary>
            /// Date identifying the week during which goods should be shipped.
            /// </summary>
            Ship_week_of_date = 39,

            /// <summary>
            /// Date/time being overlaid by a date given elsewhere.
            /// </summary>
            Superseded_datetime = 42,

            /// <summary>
            /// Date/time when received item is available.
            /// </summary>
            Availability = 44,

            /// <summary>
            /// Date and time of the compilation.
            /// </summary>
            Compilation_date_and_time = 45,

            /// <summary>
            /// Date on which a document or message has been cancelled.
            /// </summary>
            Cancellation_date = 46,

            /// <summary>
            /// Date for statistical time series purposes.
            /// </summary>
            Statistical_time_series_date = 47,

            /// <summary>
            /// Duration.
            /// </summary>
            Duration = 48,

            /// <summary>
            /// Deliver not before and not after a specific date range.
            /// </summary>
            Deliver_not_before_and_not_after_dates = 49,

            /// <summary>
            /// Date/time upon which the goods were received by a given party.
            /// </summary>
            Goods_receipt_datetime = 50,

            /// <summary>
            /// First Date for accumulation of delivery quantities.
            /// </summary>
            Cumulative_quantity_start_date = 51,

            /// <summary>
            /// Last Date for accumulation of delivery quantities.
            /// </summary>
            Cumulative_quantity_end_date = 52,

            /// <summary>
            /// Time at the buyer's location.
            /// </summary>
            Buyers_local_time = 53,

            /// <summary>
            /// Time at the seller's location.
            /// </summary>
            Sellers_local_time = 54,

            /// <summary>
            /// Date/time which has been confirmed.
            /// </summary>
            Confirmed_datetime = 55,

            /// <summary>
            /// Date on which Customs formalities necessary to allow goods to be exported, to enter home use, or to be placed under another Customs procedure has been accomplished.
            /// </summary>
            Clearance_date_Customs = 58,

            /// <summary>
            /// Inland movement authorization date.
            /// </summary>
            Inbound_movement_authorization_date = 59,

            /// <summary>
            /// Date the engineering level of goods is changed.
            /// </summary>
            Engineering_change_level_date = 60,

            /// <summary>
            /// Cancel if not delivered by this date.
            /// </summary>
            Cancel_if_not_delivered_by_this_date = 61,

            /// <summary>
            /// Date identifying a point of time after which goods shall not or will not be delivered.
            /// </summary>
            Delivery_datetime_latest = 63,

            /// <summary>
            /// Date identifying a point in time before which the goods shall not be delivered.
            /// </summary>
            Delivery_datetime_earliest = 64,

            /// <summary>
            /// First scheduled delivery date/time.
            /// </summary>
            Delivery_datetime_1st_schedule = 65,

            /// <summary>
            /// Delivery Date deriving from actual schedule.
            /// </summary>
            Delivery_datetime_current_schedule = 67,

            /// <summary>
            /// Date by which, or period within which, the merchandise should be delivered to the buyer, as agreed between the seller and the buyer.
            /// </summary>
            Delivery_datetime_promised_for = 69,

            /// <summary>
            /// Delivery is requested to happen after or on given date.
            /// </summary>
            Delivery_datetime_requested_for_after_and_including = 71,

            /// <summary>
            /// Delivery might take place earliest at given date.
            /// </summary>
            Delivery_datetime_promised_for_after_and_including = 72,

            /// <summary>
            /// Delivery is requested to happen prior to or including the given date.
            /// </summary>
            Delivery_datetime_requested_for_prior_to_and_including = 74,

            /// <summary>
            /// Delivery might take place latest at given date.
            /// </summary>
            Delivery_datetime_promised_for_prior_to_and_including = 75,

            /// <summary>
            /// Scheduled delivery date/time.
            /// </summary>
            Delivery_datetime_scheduled_for = 76,

            /// <summary>
            /// Shipment might happen at given date/time.
            /// </summary>
            Shipment_datetime_promised_for = 79,

            /// <summary>
            /// Shipment should happen earliest at given date.
            /// </summary>
            Shipment_datetime_requested_for_after_and_including = 81,

            /// <summary>
            /// Shipment should take place latest at given date.
            /// </summary>
            Shipment_datetime_requested_for_prior_to_and_including = 84,

            /// <summary>
            /// Shipment might take place latest at given date.
            /// </summary>
            Shipment_datetime_promised_for_prior_to_and_including = 85,

            /// <summary>
            /// Inquiry date.
            /// </summary>
            Inquiry_date = 89,

            /// <summary>
            /// Report start date.
            /// </summary>
            Report_start_date = 90,

            /// <summary>
            /// Report end date.
            /// </summary>
            Report_end_date = 91,

            /// <summary>
            /// Date when a contract becomes valid.
            /// </summary>
            Contract_effective_date = 92,

            /// <summary>
            /// Date when a contract expires.
            /// </summary>
            Contract_expiry_date = 93,

            /// <summary>
            /// Date on which goods are produced.
            /// </summary>
            Production_manufacture_date = 94,

            /// <summary>
            /// Date as specified on the bill of lading.
            /// </summary>
            Bill_of_lading_date = 95,

            /// <summary>
            /// Date/time when goods should, might or have been discharged from the means of transport.
            /// </summary>
            Discharge_datetime = 96,

            /// <summary>
            /// Transaction creation date.
            /// </summary>
            Transaction_creation_date = 97,

            /// <summary>
            /// Date as of there is no valid production schedule.
            /// </summary>
            Production_date_no_schedule_established_as_of = 101,

            /// <summary>
            /// Deposit date/time.
            /// </summary>
            Deposit_datetime = 107,

            /// <summary>
            /// Postmark date/time.
            /// </summary>
            Postmark_datetime = 108,

            /// <summary>
            /// The date on which a financial institution collects an amount of money on behalf of a company.
            /// </summary>
            Receive_at_lockbox_date = 109,

            /// <summary>
            /// The date on which the shipment of goods was originally scheduled.
            /// </summary>
            Ship_date_originally_scheduled = 110,

            /// <summary>
            /// The date of issuance of a manifest or ship notice.
            /// </summary>
            Manifest_ship_notice_date = 111,

            /// <summary>
            /// The first date from which interest is borne.
            /// </summary>
            First_interest_bearing_date = 112,

            /// <summary>
            /// Date as of a sample has to be available customer defined.
            /// </summary>
            Sample_required_date = 113,

            /// <summary>
            /// Date as of a tool has to be available customer defined.
            /// </summary>
            Tooling_required_date = 114,

            /// <summary>
            /// Date as of a sample will be available seller defined.
            /// </summary>
            Sample_available_date = 115,

            /// <summary>
            /// Period until which equipment is expected to be hired.
            /// </summary>
            Equipment_return_period_expected = 116,

            /// <summary>
            /// First possible date/time for delivery.
            /// </summary>
            Delivery_datetime_first = 117,

            /// <summary>
            /// Date/time at which the cargo booking has been accepted by the carrier.
            /// </summary>
            Cargo_booking_confirmed_datetime = 118,

            /// <summary>
            /// Date when a test has been completed.
            /// </summary>
            Test_completion_date = 119,

            /// <summary>
            /// The last date from which interest is borne.
            /// </summary>
            Last_interest_bearing_date = 120,

            /// <summary>
            /// Date of entry.
            /// </summary>
            Entry_date = 121,

            /// <summary>
            /// The date a contract is completed.
            /// </summary>
            Contract_completion_date = 122,

            /// <summary>
            /// The latest date/time for presentation of the documents to the bank where the credit expires.
            /// </summary>
            Documentary_credit_expiry_datetime = 123,

            /// <summary>
            /// Date when a Despatch Note is issued.
            /// </summary>
            Despatch_note_date = 124,

            /// <summary>
            /// Date when Import Licence is issued.
            /// </summary>
            Import_licence_date = 125,

            /// <summary>
            /// Date when a Contract is agreed.
            /// </summary>
            Contract_date = 126,

            /// <summary>
            /// Date of the previous report.
            /// </summary>
            Previous_report_date = 127,

            /// <summary>
            /// Date when the last delivery should be or has been accomplished.
            /// </summary>
            Delivery_datetime_last = 128,

            /// <summary>
            /// Date when imported vessel/merchandise last left the country of export for the country of import.
            /// </summary>
            Exportation_date = 129,

            /// <summary>
            /// Date of the current report.
            /// </summary>
            Current_report_date = 130,

            /// <summary>
            /// Date on which tax is due or calculated.
            /// </summary>
            Tax_point_date = 131,

            /// <summary>
            /// Date/time when carrier estimates that a means of transport should arrive at the port of discharge or place of destination.
            /// </summary>
            Arrival_datetime_estimated = 132,

            /// <summary>
            /// Date/time when carrier estimates that a means of transport should depart at the place of departure.
            /// </summary>
            Departure_datetime_estimated = 133,

            /// <summary>
            /// Date/time on which the exchange rate was fixed.
            /// </summary>
            Rate_of_exchange_datetime = 134,

            /// <summary>
            /// Date identifying when a telex message was sent.
            /// </summary>
            Telex_date = 135,

            /// <summary>
            /// Date (and time) of departure of means of transport.
            /// </summary>
            Departure_datetime = 136,

            /// <summary>
            /// Date/time when a document/message is issued.
            /// </summary>
            Document_message_datetime = 137,

            /// <summary>
            /// Date on which an amount due is made available to the creditor, in accordance with the terms of payment.
            /// </summary>
            Payment_date = 138,

            /// <summary>
            /// Date/time at which funds should be made available.
            /// </summary>
            Payment_due_date = 140,

            /// <summary>
            /// Date on which a Goods declaration is presented or lodged with Customs.
            /// </summary>
            Presentation_date_of_Goods_declaration_Customs = 141,

            /// <summary>
            /// The date a labour wage is determined.
            /// </summary>
            Labour_wage_determination_date = 142,

            /// <summary>
            /// Date on which the goods are taken over by the carrier at the place of acceptance.
            /// </summary>
            Acceptance_datetime_of_goods = 143,

            /// <summary>
            /// Date that the quota applies to.
            /// </summary>
            Quota_date = 144,

            /// <summary>
            /// A date specifying an event.
            /// </summary>
            Event_date = 145,

            /// <summary>
            /// Date on which the official date of Customs entry is anticipated.
            /// </summary>
            Entry_date_estimated_Customs = 146,

            /// <summary>
            /// Date of expiry of the validity of an Export Licence.
            /// </summary>
            Expiry_date_of_export_licence = 147,

            /// <summary>
            /// Date on which a Goods declaration is accepted by Customs in accordance with Customs legislation.
            /// </summary>
            Acceptance_date_of_Goods_declaration_Customs = 148,

            /// <summary>
            /// Date required for invoice issue.
            /// </summary>
            Invoice_date_required = 149,

            /// <summary>
            /// Date when item has been or has to be declared/presented.
            /// </summary>
            Declaration_presentation_date = 150,

            /// <summary>
            /// Date on which goods are imported, as determined by the governing Customs administration.
            /// </summary>
            Importation_date = 151,

            /// <summary>
            /// Date when imported textiles last left the country of origin for the country of importation.
            /// </summary>
            Exportation_date_for_textiles = 152,

            /// <summary>
            /// The latest date/time on which cancellation of the payment order may be requested.
            /// </summary>
            Cancellation_datetime_latest = 153,

            /// <summary>
            /// The date on which a document was accepted.
            /// </summary>
            Acceptance_date_of_document = 154,

            /// <summary>
            /// Accounting period start date.
            /// </summary>
            Accounting_period_start_date = 155,

            /// <summary>
            /// Accounting period end date.
            /// </summary>
            Accounting_period_end_date = 156,

            /// <summary>
            /// Validity start date.
            /// </summary>
            Validity_start_date = 157,

            /// <summary>
            /// The first date of a period forming a horizon.
            /// </summary>
            Horizon_start_date = 158,

            /// <summary>
            /// The last date of a period forming a horizon.
            /// </summary>
            Horizon_end_date = 159,

            /// <summary>
            /// Date when an authorization was given.
            /// </summary>
            Authorization_date = 160,

            /// <summary>
            /// Date the customer authorised the goods' release.
            /// </summary>
            Release_date_of_customer = 161,

            /// <summary>
            /// Date when the supplier released goods.
            /// </summary>
            Release_date_of_supplier = 162,

            /// <summary>
            /// Date/Time when a specific process starts.
            /// </summary>
            Processing_start_datetime = 163,

            /// <summary>
            /// Date/Time when a specific process ends.
            /// </summary>
            Processing_end_datetime = 164,

            /// <summary>
            /// Date when a tax period begins.
            /// </summary>
            Tax_period_start_date = 165,

            /// <summary>
            /// Date when a tax period ends.
            /// </summary>
            Tax_period_end_date = 166,

            /// <summary>
            /// The charge period's first date.
            /// </summary>
            Charge_period_start_date = 167,

            /// <summary>
            /// The charge period's last date.
            /// </summary>
            Charge_period_end_date = 168,

            /// <summary>
            /// Instalment payment due date.
            /// </summary>
            Instalment_payment_due_date = 327,

            /// <summary>
            /// Mutually defined.
            /// </summary>
            Mutually_defined = 999
        }
    }
}
