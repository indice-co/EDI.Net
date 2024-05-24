using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Segments;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256;

/// <summary>
/// INVOIC
/// </summary>
[EdiMessage]
public class INVOIC
{
	/// <summary>
	/// Message header
	/// </summary>
	public UNH? MessageHeaderM { get; set; }

	/// <summary>
	/// Beginning of message
	/// </summary>
	public BGM? BeginningOfMessageM { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriod1M { get; set; }

	/// <summary>
	/// Payment instructions
	/// </summary>
	public PAI? PaymentInstructions1C { get; set; }

	/// <summary>
	/// Additional information
	/// </summary>
	public List<ALI>? AdditionalInformation1C { get; set; }

	/// <summary>
	/// Item description
	/// </summary>
	public IMD? ItemDescription1C { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeText1C { get; set; }

	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentification1C { get; set; }

	/// <summary>
	/// General indicator
	/// </summary>
	public List<GIS>? GeneralIndicator1C { get; set; }

	/// <summary>
	/// Dangerous goods
	/// </summary>
	public DGS? DangerousGoods1C { get; set; }

	/// <summary>
	/// SG1
	/// </summary>
	public List<INVOIC_SG1>? SG1C { get; set; }

	/// <summary>
	/// SG2
	/// </summary>
	public List<INVOIC_SG2>? SG2C { get; set; }

	/// <summary>
	/// SG6
	/// </summary>
	public List<INVOIC_SG6>? SG6C { get; set; }

	/// <summary>
	/// SG7
	/// </summary>
	public List<INVOIC_SG7>? SG7C { get; set; }

	/// <summary>
	/// SG8
	/// </summary>
	public List<INVOIC_SG8>? SG8C { get; set; }

	/// <summary>
	/// SG9
	/// </summary>
	public List<INVOIC_SG9>? SG9C { get; set; }

	/// <summary>
	/// SG12
	/// </summary>
	public List<INVOIC_SG12>? SG12C { get; set; }

	/// <summary>
	/// SG13
	/// </summary>
	public List<INVOIC_SG13>? SG13C { get; set; }

	/// <summary>
	/// SG15
	/// </summary>
	public List<INVOIC_SG15>? SG15C { get; set; }

	/// <summary>
	/// SG22
	/// </summary>
	public List<INVOIC_SG22>? SG22C { get; set; }

	/// <summary>
	/// SG23
	/// </summary>
	public INVOIC_SG23? SG23C { get; set; }

	/// <summary>
	/// SG24
	/// </summary>
	public INVOIC_SG24? SG24C { get; set; }

	/// <summary>
	/// SG25
	/// </summary>
	public List<INVOIC_SG25>? SG25C { get; set; }

	/// <summary>
	/// Section control
	/// </summary>
	public UNS? SectionControlM { get; set; }

	/// <summary>
	/// Control total
	/// </summary>
	public List<CNT>? ControlTotalC { get; set; }

	/// <summary>
	/// SG49
	/// </summary>
	public List<INVOIC_SG49>? SG49M { get; set; }

	/// <summary>
	/// SG51
	/// </summary>
	public List<INVOIC_SG51>? SG51C { get; set; }

	/// <summary>
	/// SG52
	/// </summary>
	public List<INVOIC_SG52>? SG52C { get; set; }

	/// <summary>
	/// Message trailer
	/// </summary>
	public UNT? MessageTrailerM { get; set; }
}

[EdiSegmentGroup("RFF", "DTM", "GIR", "LOC", "MEA", "QTY", "FTX", "MOA")]
public class INVOIC_SG1 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Related identification numbers
	/// </summary>
	public List<GIR>? RelatedIdentificationNumbersC { get; set; }

	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }

	/// <summary>
	/// Measurements
	/// </summary>
	public List<MEA>? MeasurementsC { get; set; }

	/// <summary>
	/// Quantity
	/// </summary>
	public List<QTY>? QuantityC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }

	/// <summary>
	/// Monetary amount
	/// </summary>
	public List<MOA>? MonetaryAmountC { get; set; }
}

[EdiSegmentGroup("NAD", "LOC", "FII", "RFF", "DOC", "CTA")]
public class INVOIC_SG2 : NAD
{
	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }

	/// <summary>
	/// Financial institution information
	/// </summary>
	public List<FII>? FinancialInstitutionInformationC { get; set; }

	/// <summary>
	/// SG3
	/// </summary>
	public List<INVOIC_SG3>? SG3C { get; set; }

	/// <summary>
	/// SG4
	/// </summary>
	public List<INVOIC_SG4>? SG4C { get; set; }

	/// <summary>
	/// SG5
	/// </summary>
	public List<INVOIC_SG5>? SG5C { get; set; }

}

[EdiSegmentGroup("RFF", "DTM")]
public class INVOIC_SG3 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("DOC", "DTM")]
public class INVOIC_SG4 : DOC
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("CTA", "COM")]
public class INVOIC_SG5 : CTA
{
	/// <summary>
	/// Communication contact
	/// </summary>
	public List<COM>? CommunicationContactC { get; set; }
}

[EdiSegmentGroup("TAX", "MOA", "LOC")]
public class INVOIC_SG6 : TAX
{
	/// <summary>
	/// Monetary amount
	/// </summary>
	public MOA? MonetaryAmountC { get; set; }

	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }
}

[EdiSegmentGroup("CUX", "DTM")]
public class INVOIC_SG7 : CUX
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("PAT", "DTM", "PCD", "MOA", "PAI", "FII")]
public class INVOIC_SG8 : PAT
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Percentage details
	/// </summary>
	public PCD? PercentageDetailsC { get; set; }

	/// <summary>
	/// Monetary amount
	/// </summary>
	public MOA? MonetaryAmountC { get; set; }

	/// <summary>
	/// Payment instructions
	/// </summary>
	public PAI? PaymentInstructionsC { get; set; }

	/// <summary>
	/// Financial institution information
	/// </summary>
	public FII? FinancialInstitutionInformationC { get; set; }
}

[EdiSegmentGroup("TDT", "TSR", "LOC", "RFF")]
public class INVOIC_SG9 : TDT
{
	/// <summary>
	/// Transport service requirements
	/// </summary>
	public TSR? TransportServiceRequirementsC { get; set; }

	/// <summary>
	/// SG10
	/// </summary>
	public List<INVOIC_SG10>? SG10C { get; set; }

	/// <summary>
	/// SG11
	/// </summary>
	public List<INVOIC_SG11>? SG11C { get; set; }

}

[EdiSegmentGroup("LOC", "DTM")]
public class INVOIC_SG10 : LOC
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("RFF", "DTM")]
public class INVOIC_SG11 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("TOD", "LOC")]
public class INVOIC_SG12 : TOD
{
	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }
}

[EdiSegmentGroup("PAC", "MEA", "EQD", "PCI")]
public class INVOIC_SG13 : PAC
{
	/// <summary>
	/// Measurements
	/// </summary>
	public List<MEA>? MeasurementsC { get; set; }

	/// <summary>
	/// Equipment details
	/// </summary>
	public EQD? EquipmentDetailsC { get; set; }

	/// <summary>
	/// SG14
	/// </summary>
	public List<INVOIC_SG14>? SG14C { get; set; }

}

[EdiSegmentGroup("PCI", "RFF", "DTM", "GIN")]
public class INVOIC_SG14 : PCI
{
	/// <summary>
	/// Reference
	/// </summary>
	public RFF? ReferenceC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Goods identity number
	/// </summary>
	public List<GIN>? GoodsIdentityNumberC { get; set; }
}

[EdiSegmentGroup("ALC", "ALI", "FTX", "RFF", "QTY", "PCD", "MOA", "RTE", "TAX")]
public class INVOIC_SG15 : ALC
{
	/// <summary>
	/// Additional information
	/// </summary>
	public List<ALI>? AdditionalInformationC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public FTX? FreeTextC { get; set; }

	/// <summary>
	/// SG16
	/// </summary>
	public List<INVOIC_SG16>? SG16C { get; set; }

	/// <summary>
	/// SG17
	/// </summary>
	public INVOIC_SG17? SG17C { get; set; }

	/// <summary>
	/// SG18
	/// </summary>
	public INVOIC_SG18? SG18C { get; set; }

	/// <summary>
	/// SG19
	/// </summary>
	public List<INVOIC_SG19>? SG19C { get; set; }

	/// <summary>
	/// SG20
	/// </summary>
	public INVOIC_SG20? SG20C { get; set; }

	/// <summary>
	/// SG21
	/// </summary>
	public List<INVOIC_SG21>? SG21C { get; set; }

}

[EdiSegmentGroup("RFF", "DTM")]
public class INVOIC_SG16 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("QTY", "RNG")]
public class INVOIC_SG17 : QTY
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }
}

[EdiSegmentGroup("PCD", "RNG")]
public class INVOIC_SG18 : PCD
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }
}

[EdiSegmentGroup("MOA", "RNG", "CUX", "DTM")]
public class INVOIC_SG19 : MOA
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }

	/// <summary>
	/// Currencies
	/// </summary>
	public CUX? CurrenciesC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public DTM? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("RTE", "RNG")]
public class INVOIC_SG20 : RTE
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }
}

[EdiSegmentGroup("TAX", "MOA")]
public class INVOIC_SG21 : TAX
{
	/// <summary>
	/// Monetary amount
	/// </summary>
	public MOA? MonetaryAmountC { get; set; }
}

[EdiSegmentGroup("RCS", "RFF", "DTM", "FTX")]
public class INVOIC_SG22 : RCS
{
	/// <summary>
	/// Reference
	/// </summary>
	public List<RFF>? ReferenceC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }
}

[EdiSegmentGroup("AJT", "FTX")]
public class INVOIC_SG23 : AJT
{
	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }
}

[EdiSegmentGroup("INP", "FTX")]
public class INVOIC_SG24 : INP
{
	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }
}

[EdiSegmentGroup("LIN", "PIA", "IMD", "MEA", "QTY", "PCD", "ALI", "DTM", "GIN", "GIR", "QVR", "EQD", "FTX", "DGS", "MOA", "PAT", "PRI", "RFF", "PAC", "LOC", "TAX", "NAD", "ALC", "TDT", "TOD", "RCS", "GIS")]
public class INVOIC_SG25 : LIN
{
	/// <summary>
	/// Additional product id
	/// </summary>
	public List<PIA>? AdditionalProductIdC { get; set; }

	/// <summary>
	/// Item description
	/// </summary>
	public List<IMD>? ItemDescriptionC { get; set; }

	/// <summary>
	/// Measurements
	/// </summary>
	public List<MEA>? MeasurementsC { get; set; }

	/// <summary>
	/// Quantity
	/// </summary>
	public List<QTY>? QuantityC { get; set; }

	/// <summary>
	/// Percentage details
	/// </summary>
	public PCD? PercentageDetailsC { get; set; }

	/// <summary>
	/// Additional information
	/// </summary>
	public List<ALI>? AdditionalInformationC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Goods identity number
	/// </summary>
	public List<GIN>? GoodsIdentityNumberC { get; set; }

	/// <summary>
	/// Related identification numbers
	/// </summary>
	public List<GIR>? RelatedIdentificationNumbersC { get; set; }

	/// <summary>
	/// Quantity variances
	/// </summary>
	public QVR? QuantityVariancesC { get; set; }

	/// <summary>
	/// Equipment details
	/// </summary>
	public EQD? EquipmentDetailsC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }

	/// <summary>
	/// Dangerous goods
	/// </summary>
	public DGS? DangerousGoodsC { get; set; }

	/// <summary>
	/// SG26
	/// </summary>
	public List<INVOIC_SG26>? SG26C { get; set; }

	/// <summary>
	/// SG27
	/// </summary>
	public List<INVOIC_SG27>? SG27C { get; set; }

	/// <summary>
	/// SG28
	/// </summary>
	public List<INVOIC_SG28>? SG28C { get; set; }

	/// <summary>
	/// SG29
	/// </summary>
	public List<INVOIC_SG29>? SG29C { get; set; }

	/// <summary>
	/// SG30
	/// </summary>
	public List<INVOIC_SG30>? SG30C { get; set; }

	/// <summary>
	/// SG32
	/// </summary>
	public List<INVOIC_SG32>? SG32C { get; set; }

	/// <summary>
	/// SG33
	/// </summary>
	public List<INVOIC_SG33>? SG33C { get; set; }

	/// <summary>
	/// SG34
	/// </summary>
	public List<INVOIC_SG34>? SG34C { get; set; }

	/// <summary>
	/// SG38
	/// </summary>
	public List<INVOIC_SG38>? SG38C { get; set; }

	/// <summary>
	/// SG44
	/// </summary>
	public List<INVOIC_SG44>? SG44C { get; set; }

	/// <summary>
	/// SG46
	/// </summary>
	public List<INVOIC_SG46>? SG46C { get; set; }

	/// <summary>
	/// SG47
	/// </summary>
	public List<INVOIC_SG47>? SG47C { get; set; }

	/// <summary>
	/// SG48
	/// </summary>
	public List<INVOIC_SG48>? SG48C { get; set; }

}

[EdiSegmentGroup("MOA", "CUX")]
public class INVOIC_SG26 : MOA
{
	/// <summary>
	/// Currencies
	/// </summary>
	public CUX? CurrenciesC { get; set; }
}

[EdiSegmentGroup("PAT", "DTM", "PCD", "MOA")]
public class INVOIC_SG27 : PAT
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Percentage details
	/// </summary>
	public PCD? PercentageDetailsC { get; set; }

	/// <summary>
	/// Monetary amount
	/// </summary>
	public MOA? MonetaryAmountC { get; set; }
}

[EdiSegmentGroup("PRI", "CUX", "APR", "RNG", "DTM")]
public class INVOIC_SG28 : PRI
{
	/// <summary>
	/// Currencies
	/// </summary>
	public CUX? CurrenciesC { get; set; }

	/// <summary>
	/// Additional price information
	/// </summary>
	public APR? AdditionalPriceInformationC { get; set; }

	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("RFF", "DTM")]
public class INVOIC_SG29 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("PAC", "MEA", "EQD", "PCI")]
public class INVOIC_SG30 : PAC
{
	/// <summary>
	/// Measurements
	/// </summary>
	public List<MEA>? MeasurementsC { get; set; }

	/// <summary>
	/// Equipment details
	/// </summary>
	public EQD? EquipmentDetailsC { get; set; }

	/// <summary>
	/// SG31
	/// </summary>
	public List<INVOIC_SG31>? SG31C { get; set; }

}

[EdiSegmentGroup("PCI", "RFF", "DTM", "GIN")]
public class INVOIC_SG31 : PCI
{
	/// <summary>
	/// Reference
	/// </summary>
	public RFF? ReferenceC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Goods identity number
	/// </summary>
	public List<GIN>? GoodsIdentityNumberC { get; set; }
}

[EdiSegmentGroup("LOC", "QTY", "DTM")]
public class INVOIC_SG32 : LOC
{
	/// <summary>
	/// Quantity
	/// </summary>
	public List<QTY>? QuantityC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("TAX", "MOA", "LOC")]
public class INVOIC_SG33 : TAX
{
	/// <summary>
	/// Monetary amount
	/// </summary>
	public MOA? MonetaryAmountC { get; set; }

	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }
}

[EdiSegmentGroup("NAD", "LOC", "RFF", "DOC", "CTA")]
public class INVOIC_SG34 : NAD
{
	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }

	/// <summary>
	/// SG35
	/// </summary>
	public List<INVOIC_SG35>? SG35C { get; set; }

	/// <summary>
	/// SG36
	/// </summary>
	public List<INVOIC_SG36>? SG36C { get; set; }

	/// <summary>
	/// SG37
	/// </summary>
	public List<INVOIC_SG37>? SG37C { get; set; }

}

[EdiSegmentGroup("RFF", "DTM")]
public class INVOIC_SG35 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("DOC", "DTM")]
public class INVOIC_SG36 : DOC
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("CTA", "COM")]
public class INVOIC_SG37 : CTA
{
	/// <summary>
	/// Communication contact
	/// </summary>
	public List<COM>? CommunicationContactC { get; set; }
}

[EdiSegmentGroup("ALC", "ALI", "DTM", "FTX", "QTY", "PCD", "MOA", "RTE", "TAX")]
public class INVOIC_SG38 : ALC
{
	/// <summary>
	/// Additional information
	/// </summary>
	public List<ALI>? AdditionalInformationC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public FTX? FreeTextC { get; set; }

	/// <summary>
	/// SG39
	/// </summary>
	public INVOIC_SG39? SG39C { get; set; }

	/// <summary>
	/// SG40
	/// </summary>
	public INVOIC_SG40? SG40C { get; set; }

	/// <summary>
	/// SG41
	/// </summary>
	public List<INVOIC_SG41>? SG41C { get; set; }

	/// <summary>
	/// SG42
	/// </summary>
	public INVOIC_SG42? SG42C { get; set; }

	/// <summary>
	/// SG43
	/// </summary>
	public List<INVOIC_SG43>? SG43C { get; set; }

}

[EdiSegmentGroup("QTY", "RNG")]
public class INVOIC_SG39 : QTY
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }
}

[EdiSegmentGroup("PCD", "RNG")]
public class INVOIC_SG40 : PCD
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }
}

[EdiSegmentGroup("MOA", "RNG", "CUX", "DTM")]
public class INVOIC_SG41 : MOA
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }

	/// <summary>
	/// Currencies
	/// </summary>
	public CUX? CurrenciesC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public DTM? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("RTE", "RNG")]
public class INVOIC_SG42 : RTE
{
	/// <summary>
	/// Range details
	/// </summary>
	public RNG? RangeDetailsC { get; set; }
}

[EdiSegmentGroup("TAX", "MOA")]
public class INVOIC_SG43 : TAX
{
	/// <summary>
	/// Monetary amount
	/// </summary>
	public MOA? MonetaryAmountC { get; set; }
}

[EdiSegmentGroup("TDT", "LOC")]
public class INVOIC_SG44 : TDT
{
	/// <summary>
	/// SG45
	/// </summary>
	public List<INVOIC_SG45>? SG45C { get; set; }

}

[EdiSegmentGroup("LOC", "DTM")]
public class INVOIC_SG45 : LOC
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("TOD", "LOC")]
public class INVOIC_SG46 : TOD
{
	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }
}

[EdiSegmentGroup("RCS", "RFF", "DTM", "FTX")]
public class INVOIC_SG47 : RCS
{
	/// <summary>
	/// Reference
	/// </summary>
	public List<RFF>? ReferenceC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }
}

[EdiSegmentGroup("GIS", "RFF", "DTM", "GIR", "LOC", "MEA", "QTY", "FTX", "MOA")]
public class INVOIC_SG48 : GIS
{
	/// <summary>
	/// Reference
	/// </summary>
	public RFF? ReferenceC { get; set; }

	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }

	/// <summary>
	/// Related identification numbers
	/// </summary>
	public List<GIR>? RelatedIdentificationNumbersC { get; set; }

	/// <summary>
	/// Place/location identification
	/// </summary>
	public List<LOC>? PlaceLocationIdentificationC { get; set; }

	/// <summary>
	/// Measurements
	/// </summary>
	public List<MEA>? MeasurementsC { get; set; }

	/// <summary>
	/// Quantity
	/// </summary>
	public List<QTY>? QuantityC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public List<FTX>? FreeTextC { get; set; }

	/// <summary>
	/// Monetary amount
	/// </summary>
	public List<MOA>? MonetaryAmountC { get; set; }
}

[EdiSegmentGroup("MOA", "RFF")]
public class INVOIC_SG49 : MOA
{
	/// <summary>
	/// SG50
	/// </summary>
	public INVOIC_SG50? SG50C { get; set; }

}

[EdiSegmentGroup("RFF", "DTM")]
public class INVOIC_SG50 : RFF
{
	/// <summary>
	/// Date/time/period
	/// </summary>
	public List<DTM>? DateTimePeriodC { get; set; }
}

[EdiSegmentGroup("TAX", "MOA")]
public class INVOIC_SG51 : TAX
{
	/// <summary>
	/// Monetary amount
	/// </summary>
	public List<MOA>? MonetaryAmountC { get; set; }
}

[EdiSegmentGroup("ALC", "ALI", "MOA", "FTX")]
public class INVOIC_SG52 : ALC
{
	/// <summary>
	/// Additional information
	/// </summary>
	public ALI? AdditionalInformationC { get; set; }

	/// <summary>
	/// Monetary amount
	/// </summary>
	public List<MOA>? MonetaryAmountC { get; set; }

	/// <summary>
	/// Free text
	/// </summary>
	public FTX? FreeTextC { get; set; }
}