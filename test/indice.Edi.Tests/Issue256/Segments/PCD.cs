using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify percentage information.
/// </summary>
[EdiSegment, EdiPath("PCD")]
public class PCD
{
	/// <summary>
	/// Percentage relating to a specified basis.
	/// </summary>
	[EdiPath("PCD/0")]
	public PCD_PercentageDetails? PercentageDetails { get; set; }
}

/// <summary>
/// Percentage relating to a specified basis.
/// </summary>
[EdiElement]
public class PCD_PercentageDetails
{
	/// <summary>
	/// Identification of the usage of a percentage.
	/// </summary>
	[EdiValue("X(3)", Path = "PCD/*/0", Mandatory = true)]
	public PercentageQualifier? PercentageQualifier { get; set; }

	/// <summary>
	/// Value expressed as a percentage of a specified amount.
	/// </summary>
	[EdiValue("9(10)", Path = "PCD/*/1", Mandatory = false)]
	public decimal? Percentage { get; set; }

	/// <summary>
	/// Indication of the application of a percentage.
	/// </summary>
	[EdiValue("X(3)", Path = "PCD/*/2", Mandatory = false)]
	public PercentageBasisCoded? PercentageBasisCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PCD/*/3", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PCD/*/4", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}