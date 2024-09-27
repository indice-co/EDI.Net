using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify sector/subject requirements and conditions.
/// </summary>
[EdiSegment, EdiPath("RCS")]
public class RCS
{
	/// <summary>
	/// Identification of the subject areas to which the specified conditions apply.
	/// </summary>
	[EdiValue("X(3)", Path = "RCS/0", Mandatory = true)]
	public SectorSubjectIdentificationQualifier? SectorSubjectIdentificationQualifier { get; set; }

	/// <summary>
	/// To identify the specific rule/condition requirement.
	/// </summary>
	[EdiPath("RCS/1")]
	public RCS_RequirementConditionIdentification? RequirementConditionIdentification { get; set; }

	/// <summary>
	/// Code specifying the action to be taken or already taken.
	/// </summary>
	[EdiValue("X(3)", Path = "RCS/2", Mandatory = false)]
	public ActionRequestNotificationCoded? ActionRequestNotificationCoded { get; set; }

}

/// <summary>
/// To identify the specific rule/condition requirement.
/// </summary>
[EdiElement]
public class RCS_RequirementConditionIdentification
{
	/// <summary>
	/// Code identifying a specific rule, regulation or condition.
	/// </summary>
	[EdiValue("X(17)", Path = "RCS/*/0", Mandatory = true)]
	public string? RequirementConditionIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "RCS/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "RCS/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain text identifying a specific requirement or condition.
	/// </summary>
	[EdiValue("X(35)", Path = "RCS/*/3", Mandatory = false)]
	public string? RequirementOrCondition { get; set; }
}