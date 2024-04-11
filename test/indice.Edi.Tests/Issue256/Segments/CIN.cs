using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To describe an item of clinical information.
/// </summary>
[EdiSegment, EdiPath("CIN")]
public class CIN
{
	/// <summary>
	/// To specify type of clinical information.
	/// </summary>
	[EdiValue("X(3)", Path = "CIN/0", Mandatory = true)]
	public ClinicalInformationQualifier? ClinicalInformationQualifier { get; set; }

	/// <summary>
	/// To specify an item of clinical information.
	/// </summary>
	[EdiPath("CIN/1")]
	public CIN_ClinicalInformationDetails? ClinicalInformationDetails { get; set; }

	/// <summary>
	/// To specify the certainty.
	/// </summary>
	[EdiPath("CIN/2")]
	public CIN_CertaintyDetails? CertaintyDetails { get; set; }
}

/// <summary>
/// To specify an item of clinical information.
/// </summary>
[EdiElement]
public class CIN_ClinicalInformationDetails
{
	/// <summary>
	/// To specify an item of clinical information using code.
	/// </summary>
	[EdiValue("X(17)", Path = "CIN/*/0", Mandatory = false)]
	public string? ClinicalInformationIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CIN/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CIN/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify an item of clinical information as free text.
	/// </summary>
	[EdiValue("X(70)", Path = "CIN/*/3", Mandatory = false)]
	public string? ClinicalInformation { get; set; }
}

/// <summary>
/// To specify the certainty.
/// </summary>
[EdiElement]
public class CIN_CertaintyDetails
{
	/// <summary>
	/// To specify the certainty using code.
	/// </summary>
	[EdiValue("X(3)", Path = "CIN/*/0", Mandatory = false)]
	public string? CertaintyCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CIN/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CIN/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify certainty as free text.
	/// </summary>
	[EdiValue("X(35)", Path = "CIN/*/3", Mandatory = false)]
	public string? Certainty { get; set; }
}