using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To communicate how dose(s) are administered.
/// </summary>
[EdiSegment, EdiPath("DSG")]
public class DSG
{
	/// <summary>
	/// To provide a part of dosage specification.
	/// </summary>
	[EdiValue("X(3)", Path = "DSG/0", Mandatory = true)]
	public DosageAdministrationQualifier? DosageAdministrationQualifier { get; set; }

	/// <summary>
	/// To specify a dosage.
	/// </summary>
	[EdiPath("DSG/1")]
	public DSG_DosageDetails? DosageDetails { get; set; }
}

/// <summary>
/// To specify a dosage.
/// </summary>
[EdiElement]
public class DSG_DosageDetails
{
	/// <summary>
	/// To specify a dosage using code.
	/// </summary>
	[EdiValue("X(8)", Path = "DSG/*/0", Mandatory = false)]
	public string? DosageIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DSG/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DSG/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify a dosage as free text.
	/// </summary>
	[EdiValue("X(70)", Path = "DSG/*/3", Mandatory = false)]
	public string? Dosage { get; set; }
}