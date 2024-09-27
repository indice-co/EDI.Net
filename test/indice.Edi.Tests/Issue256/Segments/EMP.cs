using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify employment details.
/// </summary>
[EdiSegment, EdiPath("EMP")]
public class EMP
{
	/// <summary>
	/// Specification of the meaning of employment details.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/0", Mandatory = true)]
	public EmploymentQualifier? EmploymentQualifier { get; set; }

	/// <summary>
	/// Code and/or description of an employment category.
	/// </summary>
	[EdiPath("EMP/1")]
	public EMP_EmploymentCategory? EmploymentCategory { get; set; }

	/// <summary>
	/// Description of an occupation.
	/// </summary>
	[EdiPath("EMP/2")]
	public EMP_Occupation? Occupation { get; set; }

	/// <summary>
	/// Qualification classification description and/or code. This specifies the trade, skill, professional or similar qualification category.
	/// </summary>
	[EdiPath("EMP/3")]
	public EMP_QualificationClassification? QualificationClassification { get; set; }

	/// <summary>
	/// Description of job classification.
	/// </summary>
	[EdiValue("X(35)", Path = "EMP/4", Mandatory = false)]
	public string? JobTitle { get; set; }

	/// <summary>
	/// Code identifying the application area of a qualification.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/5", Mandatory = false)]
	public QualificationAreaCoded? QualificationAreaCoded { get; set; }

}

/// <summary>
/// Code and/or description of an employment category.
/// </summary>
[EdiElement]
public class EMP_EmploymentCategory
{
	/// <summary>
	/// Identification of the employment category.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/0", Mandatory = false)]
	public string? EmploymentCategoryCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of employment category.
	/// </summary>
	[EdiValue("X(35)", Path = "EMP/*/3", Mandatory = false)]
	public string? EmploymentCategory { get; set; }
}

/// <summary>
/// Description of an occupation.
/// </summary>
[EdiElement]
public class EMP_Occupation
{
	/// <summary>
	/// Code identifying an occupation.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/0", Mandatory = false)]
	public string? OccupationCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of an occupation.
	/// </summary>
	[EdiValue("X(35)", Path = "EMP/*/3", Mandatory = false)]
	public string? Occupation1 { get; set; }

	/// <summary>
	/// Description of an occupation.
	/// </summary>
	[EdiValue("X(35)", Path = "EMP/*/4", Mandatory = false)]
	public string? Occupation2 { get; set; }
}

/// <summary>
/// Qualification classification description and/or code. This specifies the trade, skill, professional or similar qualification category.
/// </summary>
[EdiElement]
public class EMP_QualificationClassification
{
	/// <summary>
	/// Identification of a qualification classification.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/0", Mandatory = false)]
	public string? QualificationClassificationCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EMP/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a qualification classification.
	/// </summary>
	[EdiValue("X(35)", Path = "EMP/*/3", Mandatory = false)]
	public string? QualificationClassification1 { get; set; }

	/// <summary>
	/// Description of a qualification classification.
	/// </summary>
	[EdiValue("X(35)", Path = "EMP/*/4", Mandatory = false)]
	public string? QualificationClassification2 { get; set; }
}