using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide classification details relating to the activities of an organisation.
/// </summary>
[EdiSegment, EdiPath("SPR")]
public class SPR
{
	/// <summary>
	/// Identification of the subject areas to which the specified conditions apply.
	/// </summary>
	[EdiValue("X(3)", Path = "SPR/0", Mandatory = true)]
	public SectorSubjectIdentificationQualifier? SectorSubjectIdentificationQualifier { get; set; }

	/// <summary>
	/// A code to specify the classification of an organisation.
	/// </summary>
	[EdiValue("X(3)", Path = "SPR/1", Mandatory = false)]
	public OrganisationClassificationCoded? OrganisationClassificationCoded { get; set; }

	/// <summary>
	/// To specify details regarding the class of an organisation.
	/// </summary>
	[EdiPath("SPR/2")]
	public SPR_OrganisationClassificationDetail? OrganisationClassificationDetail { get; set; }
}

/// <summary>
/// To specify details regarding the class of an organisation.
/// </summary>
[EdiElement]
public class SPR_OrganisationClassificationDetail
{
	/// <summary>
	/// A code to specify the class of an organisation.
	/// </summary>
	[EdiValue("X(17)", Path = "SPR/*/0", Mandatory = false)]
	public string? OrganisationalClassIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SPR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SPR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify the class of an organisation.
	/// </summary>
	[EdiValue("X(70)", Path = "SPR/*/3", Mandatory = false)]
	public string? OrganisationalClass { get; set; }
}