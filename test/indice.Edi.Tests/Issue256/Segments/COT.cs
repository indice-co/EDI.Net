using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify details about membership contributions.
/// </summary>
[EdiSegment, EdiPath("COT")]
public class COT
{
	/// <summary>
	/// Specification of the meaning of a financial contribution to a scheme or group.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/0", Mandatory = true)]
	public ContributionQualifier? ContributionQualifier { get; set; }

	/// <summary>
	/// Identification of the type of a contribution to a scheme or group.
	/// </summary>
	[EdiPath("COT/1")]
	public COT_ContributionType? ContributionType { get; set; }

	/// <summary>
	/// To specify an instruction.
	/// </summary>
	[EdiPath("COT/2")]
	public COT_Instruction? Instruction { get; set; }

	/// <summary>
	/// Identification of the applicable rate/tariff class.
	/// </summary>
	[EdiPath("COT/3")]
	public COT_RateTariffClass? RateTariffClass { get; set; }

	/// <summary>
	/// Code and/or description of the reason for a change.
	/// </summary>
	[EdiPath("COT/4")]
	public COT_ReasonForChange? ReasonForChange { get; set; }
}

/// <summary>
/// Identification of the type of a contribution to a scheme or group.
/// </summary>
[EdiElement]
public class COT_ContributionType
{
	/// <summary>
	/// Identification of the type of a contribution to a scheme or group.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/0", Mandatory = true)]
	public ContributionTypeCoded? ContributionTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of the type of a contribution to a scheme or group.
	/// </summary>
	[EdiValue("X(35)", Path = "COT/*/3", Mandatory = false)]
	public string? ContributionType { get; set; }
}

/// <summary>
/// To specify an instruction.
/// </summary>
[EdiElement]
public class COT_Instruction
{
	/// <summary>
	/// Code giving specific meaning to the type of instructions.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/0", Mandatory = true)]
	public InstructionQualifier? InstructionQualifier { get; set; }

	/// <summary>
	/// Specification of an action to be taken by the receiver of the message.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/1", Mandatory = false)]
	public InstructionCoded? InstructionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of an instruction.
	/// </summary>
	[EdiValue("X(35)", Path = "COT/*/4", Mandatory = false)]
	public string? Instruction { get; set; }
}

/// <summary>
/// Identification of the applicable rate/tariff class.
/// </summary>
[EdiElement]
public class COT_RateTariffClass
{
	/// <summary>
	/// Identification of the rate/tariff class.
	/// </summary>
	[EdiValue("X(9)", Path = "COT/*/0", Mandatory = true)]
	public RateTariffClassIdentification? RateTariffClassIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier1 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded1 { get; set; }

	/// <summary>
	/// Description of applicable rate/tariff class.
	/// </summary>
	[EdiValue("X(35)", Path = "COT/*/3", Mandatory = false)]
	public string? RateTariffClass { get; set; }

	/// <summary>
	/// Code identifying supplementary rate/tariff.
	/// </summary>
	[EdiValue("X(6)", Path = "COT/*/4", Mandatory = false)]
	public string? SupplementaryRateTariffBasisIdentification1 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/5", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier2 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/6", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded2 { get; set; }

	/// <summary>
	/// Code identifying supplementary rate/tariff.
	/// </summary>
	[EdiValue("X(6)", Path = "COT/*/7", Mandatory = false)]
	public string? SupplementaryRateTariffBasisIdentification2 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/8", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier3 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/9", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded3 { get; set; }
}

/// <summary>
/// Code and/or description of the reason for a change.
/// </summary>
[EdiElement]
public class COT_ReasonForChange
{
	/// <summary>
	/// Identification of the reason for a change.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/0", Mandatory = false)]
	public ChangeReasonCoded? ChangeReasonCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of the reason for a change.
	/// </summary>
	[EdiValue("X(35)", Path = "COT/*/3", Mandatory = false)]
	public string? ChangeReason { get; set; }
}