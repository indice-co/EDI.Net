using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify a clinical intervention such as treatments and investigations.
/// </summary>
[EdiSegment, EdiPath("CLI")]
public class CLI
{
	/// <summary>
	/// To specify the type of clinical intervention.
	/// </summary>
	[EdiValue("X(3)", Path = "CLI/0", Mandatory = true)]
	public ClinicalInterventionQualifier? ClinicalInterventionQualifier { get; set; }

	/// <summary>
	/// To specify a clinical intervention.
	/// </summary>
	[EdiPath("CLI/1")]
	public CLI_ClinicalInterventionDetails? ClinicalInterventionDetails { get; set; }
}

/// <summary>
/// To specify a clinical intervention.
/// </summary>
[EdiElement]
public class CLI_ClinicalInterventionDetails
{
	/// <summary>
	/// To specify a clinical intervention.
	/// </summary>
	[EdiValue("X(17)", Path = "CLI/*/0", Mandatory = false)]
	public string? ClinicalInterventionIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CLI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CLI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify clinical intervention as free text.
	/// </summary>
	[EdiValue("X(70)", Path = "CLI/*/3", Mandatory = false)]
	public string? ClinicalIntervention { get; set; }
}