using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify allowance or charge details.
/// </summary>
[EdiSegment, EdiPath("ALC")]
public class ALC
{
	/// <summary>
	/// Specification of an allowance or charge for the service specified.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/0", Mandatory = true)]
	public AllowanceOrChargeQualifier? AllowanceOrChargeQualifier { get; set; }

	/// <summary>
	/// Identification of allowance/charge information by number and/or code.
	/// </summary>
	[EdiPath("ALC/1")]
	public ALC_AllowanceChargeInformation? AllowanceChargeInformation { get; set; }

	/// <summary>
	/// Indication of how allowances or charges are to be settled.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/2", Mandatory = false)]
	public SettlementCoded? SettlementCoded { get; set; }

	/// <summary>
	/// Code indicating the sequence of cumulated calculations to be agreed between interchange partners.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/3", Mandatory = false)]
	public CalculationSequenceIndicatorCoded? CalculationSequenceIndicatorCoded { get; set; }

	/// <summary>
	/// Identification of a special service by a code from a specified source or by description.
	/// </summary>
	[EdiPath("ALC/4")]
	public ALC_SpecialServicesIdentification? SpecialServicesIdentification { get; set; }
}

/// <summary>
/// Identification of allowance/charge information by number and/or code.
/// </summary>
[EdiElement]
public class ALC_AllowanceChargeInformation
{
	/// <summary>
	/// Number assigned by a party referencing an allowance, promotion, deal or charge.
	/// </summary>
	[EdiValue("X(35)", Path = "ALC/*/0", Mandatory = false)]
	public string? AllowanceOrChargeNumber { get; set; }

	/// <summary>
	/// Identification of a charge or allowance.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/*/1", Mandatory = false)]
	public ChargeAllowanceDescriptionCoded? ChargeAllowanceDescriptionCoded { get; set; }
}

/// <summary>
/// Identification of a special service by a code from a specified source or by description.
/// </summary>
[EdiElement]
public class ALC_SpecialServicesIdentification
{
	/// <summary>
	/// Code identifying a special service.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/*/0", Mandatory = false)]
	public SpecialServicesCoded? SpecialServicesCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ALC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a special service.
	/// </summary>
	[EdiValue("X(35)", Path = "ALC/*/3", Mandatory = false)]
	public string? SpecialService1 { get; set; }

	/// <summary>
	/// Description of a special service.
	/// </summary>
	[EdiValue("X(35)", Path = "ALC/*/4", Mandatory = false)]
	public string? SpecialService2 { get; set; }
}