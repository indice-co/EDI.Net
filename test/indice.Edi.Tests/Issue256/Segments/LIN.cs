using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a line item and configuration.
/// </summary>
[EdiSegment, EdiPath("LIN")]
public class LIN
{
	/// <summary>
	/// Serial number designating each separate item within a series of articles.
	/// </summary>
	[EdiValue("X(6)", Path = "LIN/0", Mandatory = false)]
	public string? LineItemNumber { get; set; }

	/// <summary>
	/// Code specifying the action to be taken or already taken.
	/// </summary>
	[EdiValue("X(3)", Path = "LIN/1", Mandatory = false)]
	public ActionRequestNotificationCoded? ActionRequestNotificationCoded { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("LIN/2")]
	public LIN_ItemNumberIdentification? ItemNumberIdentification { get; set; }

	/// <summary>
	/// To provide an indication that a segment or segment group is used to contain sub-line or sub-line item information and to optionally enable the sub-line to be identified.
	/// </summary>
	[EdiPath("LIN/3")]
	public LIN_SublineInformation? SublineInformation { get; set; }

	/// <summary>
	/// Number indicating the level of an object which is in a hierarchy.
	/// </summary>
	[EdiValue("9(2)", Path = "LIN/4", Mandatory = false)]
	public decimal? ConfigurationLevel { get; set; }

	/// <summary>
	/// Code indicating the status of the sub-line item in the configuration.
	/// </summary>
	[EdiValue("X(3)", Path = "LIN/5", Mandatory = false)]
	public ConfigurationCoded? ConfigurationCoded { get; set; }

}

/// <summary>
/// Goods identification for a specified source.
/// </summary>
[EdiElement]
public class LIN_ItemNumberIdentification
{
	/// <summary>
	/// A number allocated to a group or item.
	/// </summary>
	[EdiValue("X(35)", Path = "LIN/*/0", Mandatory = false)]
	public string? ItemNumber { get; set; }

	/// <summary>
	/// Identification of the type of item number.
	/// </summary>
	[EdiValue("X(3)", Path = "LIN/*/1", Mandatory = false)]
	public ItemNumberTypeCoded? ItemNumberTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LIN/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LIN/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// To provide an indication that a segment or segment group is used to contain sub-line or sub-line item information and to optionally enable the sub-line to be identified.
/// </summary>
[EdiElement]
public class LIN_SublineInformation
{
	/// <summary>
	/// Indication that the segment and/or segment group is used for sub-line item information.
	/// </summary>
	[EdiValue("X(3)", Path = "LIN/*/0", Mandatory = false)]
	public SublineIndicatorCoded? SublineIndicatorCoded { get; set; }

	/// <summary>
	/// Serial number designating each separate item within a series of articles.
	/// </summary>
	[EdiValue("X(6)", Path = "LIN/*/1", Mandatory = false)]
	public string? LineItemNumber { get; set; }
}