using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To indicate that special conditions due to the origin, customs preference, fiscal or commercial factors are applicable.
/// </summary>
[EdiSegment, EdiPath("ALI")]
public class ALI
{
	/// <summary>
	/// Country in which the goods have been produced or manufactured, according to criteria laid down for the purposes of application of the Customs tariff, of quantitative restrictions, or of any other measure related to trade.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/0", Mandatory = false)]
	public string? CountryOfOriginCoded { get; set; }

	/// <summary>
	/// Identification of the statement of the full/preferential regime according to which duty is assessed.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/1", Mandatory = false)]
	public TypeOfDutyRegimeCoded? TypeOfDutyRegimeCoded { get; set; }

	/// <summary>
	/// Indication of a specific condition.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/2", Mandatory = false)]
	public SpecialConditionsCoded? SpecialConditionsCoded1 { get; set; }

	/// <summary>
	/// Indication of a specific condition.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/3", Mandatory = false)]
	public SpecialConditionsCoded? SpecialConditionsCoded2 { get; set; }

	/// <summary>
	/// Indication of a specific condition.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/4", Mandatory = false)]
	public SpecialConditionsCoded? SpecialConditionsCoded3 { get; set; }

	/// <summary>
	/// Indication of a specific condition.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/5", Mandatory = false)]
	public SpecialConditionsCoded? SpecialConditionsCoded4 { get; set; }

	/// <summary>
	/// Indication of a specific condition.
	/// </summary>
	[EdiValue("X(3)", Path = "ALI/6", Mandatory = false)]
	public SpecialConditionsCoded? SpecialConditionsCoded5 { get; set; }

}