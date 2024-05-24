using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify details about membership.
/// </summary>
[EdiSegment, EdiPath("MEM")]
public class MEM
{
	/// <summary>
	/// Specification of the meaning of a code to qualify membership data.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/0", Mandatory = true)]
	public MembershipQualifier? MembershipQualifier { get; set; }

	/// <summary>
	/// Identification and/or description of a membership category for a member of a scheme or group.
	/// </summary>
	[EdiPath("MEM/1")]
	public MEM_MembershipCategory? MembershipCategory { get; set; }

	/// <summary>
	/// Code and/or description of membership status.
	/// </summary>
	[EdiPath("MEM/2")]
	public MEM_MembershipStatus? MembershipStatus { get; set; }

	/// <summary>
	/// Identification of a membership level.
	/// </summary>
	[EdiPath("MEM/3")]
	public MEM_MembershipLevel? MembershipLevel { get; set; }

	/// <summary>
	/// Identification of the applicable rate/tariff class.
	/// </summary>
	[EdiPath("MEM/4")]
	public MEM_RateTariffClass? RateTariffClass { get; set; }

	/// <summary>
	/// Code and/or description of the reason for a change.
	/// </summary>
	[EdiPath("MEM/5")]
	public MEM_ReasonForChange? ReasonForChange { get; set; }
}

/// <summary>
/// Identification and/or description of a membership category for a member of a scheme or group.
/// </summary>
[EdiElement]
public class MEM_MembershipCategory
{
	/// <summary>
	/// Identification of a membership category.
	/// </summary>
	[EdiValue("X(4)", Path = "MEM/*/0", Mandatory = true)]
	public MembershipCategoryIdentification? MembershipCategoryIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a membership category.
	/// </summary>
	[EdiValue("X(35)", Path = "MEM/*/3", Mandatory = false)]
	public string? MembershipCategory { get; set; }
}

/// <summary>
/// Code and/or description of membership status.
/// </summary>
[EdiElement]
public class MEM_MembershipStatus
{
	/// <summary>
	/// Identification of the membership status.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/0", Mandatory = false)]
	public string? MembershipStatusCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of membership status.
	/// </summary>
	[EdiValue("X(35)", Path = "MEM/*/3", Mandatory = false)]
	public string? MembershipStatus { get; set; }
}

/// <summary>
/// Identification of a membership level.
/// </summary>
[EdiElement]
public class MEM_MembershipLevel
{
	/// <summary>
	/// Specification of the meaning of a membership level.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/0", Mandatory = true)]
	public MembershipLevelQualifier? MembershipLevelQualifier { get; set; }

	/// <summary>
	/// Identification of a membership level.
	/// </summary>
	[EdiValue("X(9)", Path = "MEM/*/1", Mandatory = false)]
	public string? MembershipLevelIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a membership level.
	/// </summary>
	[EdiValue("X(35)", Path = "MEM/*/4", Mandatory = false)]
	public string? MembershipLevel { get; set; }
}

/// <summary>
/// Identification of the applicable rate/tariff class.
/// </summary>
[EdiElement]
public class MEM_RateTariffClass
{
	/// <summary>
	/// Identification of the rate/tariff class.
	/// </summary>
	[EdiValue("X(9)", Path = "MEM/*/0", Mandatory = true)]
	public RateTariffClassIdentification? RateTariffClassIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier1 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded1 { get; set; }

	/// <summary>
	/// Description of applicable rate/tariff class.
	/// </summary>
	[EdiValue("X(35)", Path = "MEM/*/3", Mandatory = false)]
	public string? RateTariffClass { get; set; }

	/// <summary>
	/// Code identifying supplementary rate/tariff.
	/// </summary>
	[EdiValue("X(6)", Path = "MEM/*/4", Mandatory = false)]
	public string? SupplementaryRateTariffBasisIdentification1 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/5", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier2 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/6", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded2 { get; set; }

	/// <summary>
	/// Code identifying supplementary rate/tariff.
	/// </summary>
	[EdiValue("X(6)", Path = "MEM/*/7", Mandatory = false)]
	public string? SupplementaryRateTariffBasisIdentification2 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/8", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier3 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/9", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded3 { get; set; }
}

/// <summary>
/// Code and/or description of the reason for a change.
/// </summary>
[EdiElement]
public class MEM_ReasonForChange
{
	/// <summary>
	/// Identification of the reason for a change.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/0", Mandatory = false)]
	public ChangeReasonCoded? ChangeReasonCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "MEM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of the reason for a change.
	/// </summary>
	[EdiValue("X(35)", Path = "MEM/*/3", Mandatory = false)]
	public string? ChangeReason { get; set; }
}