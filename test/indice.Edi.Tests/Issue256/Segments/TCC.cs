using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify charges.
/// </summary>
[EdiSegment, EdiPath("TCC")]
public class TCC
{
	/// <summary>
	/// Identification of a charge by code and/or by name.
	/// </summary>
	[EdiPath("TCC/0")]
	public TCC_Charge? Charge { get; set; }

	/// <summary>
	/// Identification of the applicable rate/tariff class.
	/// </summary>
	[EdiPath("TCC/1")]
	public TCC_RateTariffClass? RateTariffClass { get; set; }

	/// <summary>
	/// Identification of commodity/rates.
	/// </summary>
	[EdiPath("TCC/2")]
	public TCC_CommodityRateDetail? CommodityRateDetail { get; set; }

	/// <summary>
	/// Identification of the applicable rate/tariff class.
	/// </summary>
	[EdiPath("TCC/3")]
	public TCC_RateTariffClassDetail? RateTariffClassDetail { get; set; }
}

/// <summary>
/// Identification of a charge by code and/or by name.
/// </summary>
[EdiElement]
public class TCC_Charge
{
	/// <summary>
	/// Coded description of freight charges and other charges (used in combination with 1131/3055).
	/// </summary>
	[EdiValue("X(17)", Path = "TCC/*/0", Mandatory = false)]
	public string? FreightAndChargesIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain language statement describing freight and other charges.
	/// </summary>
	[EdiValue("X(26)", Path = "TCC/*/3", Mandatory = false)]
	public string? FreightAndCharges { get; set; }

	/// <summary>
	/// Code indicating whether freight item amount is prepaid or to be collected.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/4", Mandatory = false)]
	public PrepaidCollectIndicatorCoded? PrepaidCollectIndicatorCoded { get; set; }

	/// <summary>
	/// A number allocated to a group or item.
	/// </summary>
	[EdiValue("X(35)", Path = "TCC/*/5", Mandatory = false)]
	public string? ItemNumber { get; set; }
}

/// <summary>
/// Identification of the applicable rate/tariff class.
/// </summary>
[EdiElement]
public class TCC_RateTariffClass
{
	/// <summary>
	/// Identification of the rate/tariff class.
	/// </summary>
	[EdiValue("X(9)", Path = "TCC/*/0", Mandatory = true)]
	public RateTariffClassIdentification? RateTariffClassIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier1 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded1 { get; set; }

	/// <summary>
	/// Description of applicable rate/tariff class.
	/// </summary>
	[EdiValue("X(35)", Path = "TCC/*/3", Mandatory = false)]
	public string? RateTariffClass { get; set; }

	/// <summary>
	/// Code identifying supplementary rate/tariff.
	/// </summary>
	[EdiValue("X(6)", Path = "TCC/*/4", Mandatory = false)]
	public string? SupplementaryRateTariffBasisIdentification1 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/5", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier2 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/6", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded2 { get; set; }

	/// <summary>
	/// Code identifying supplementary rate/tariff.
	/// </summary>
	[EdiValue("X(6)", Path = "TCC/*/7", Mandatory = false)]
	public string? SupplementaryRateTariffBasisIdentification2 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/8", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier3 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/9", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded3 { get; set; }
}

/// <summary>
/// Identification of commodity/rates.
/// </summary>
[EdiElement]
public class TCC_CommodityRateDetail
{
	/// <summary>
	/// Code identifying goods for Customs, transport or statistical purposes (generic term).
	/// </summary>
	[EdiValue("X(18)", Path = "TCC/*/0", Mandatory = false)]
	public string? CommodityRateIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Identification of the applicable rate/tariff class.
/// </summary>
[EdiElement]
public class TCC_RateTariffClassDetail
{
	/// <summary>
	/// Identification of the rate/tariff class.
	/// </summary>
	[EdiValue("X(9)", Path = "TCC/*/0", Mandatory = false)]
	public RateTariffClassIdentification? RateTariffClassIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TCC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}