using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify items of person demographic information.
/// </summary>
[EdiSegment, EdiPath("PDI")]
public class PDI
{
	/// <summary>
	/// Code giving the gender of a person, animal or plant.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/0", Mandatory = false)]
	public string? SexCoded { get; set; }

	/// <summary>
	/// To specify the marital status of a person.
	/// </summary>
	[EdiPath("PDI/1")]
	public PDI_MaritalStatusDetails? MaritalStatusDetails { get; set; }

	/// <summary>
	/// To specify the religion of a person.
	/// </summary>
	[EdiPath("PDI/2")]
	public PDI_ReligionDetails? ReligionDetails { get; set; }
}

/// <summary>
/// To specify the marital status of a person.
/// </summary>
[EdiElement]
public class PDI_MaritalStatusDetails
{
	/// <summary>
	/// Code giving the marital status of a person.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/*/0", Mandatory = false)]
	public MaritalStatusCoded? MaritalStatusCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Marital status of a person.
	/// </summary>
	[EdiValue("X(35)", Path = "PDI/*/3", Mandatory = false)]
	public string? MaritalStatus { get; set; }
}

/// <summary>
/// To specify the religion of a person.
/// </summary>
[EdiElement]
public class PDI_ReligionDetails
{
	/// <summary>
	/// To specify the religion of a person in a coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/*/0", Mandatory = false)]
	public string? ReligionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PDI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify the religion of a person.
	/// </summary>
	[EdiValue("X(35)", Path = "PDI/*/3", Mandatory = false)]
	public string? Religion { get; set; }
}