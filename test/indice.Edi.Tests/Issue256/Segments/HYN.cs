using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// A segment to identify hierarchical connections from a given item to higher or lower levelled item.
/// </summary>
[EdiSegment, EdiPath("HYN")]
public class HYN
{
	/// <summary>
	/// To qualify the object of the given hierarchy (e.g. product hierarchy, company hierarchy..).
	/// </summary>
	[EdiValue("X(3)", Path = "HYN/0", Mandatory = true)]
	public HierarchyObjectQualifier? HierarchyObjectQualifier { get; set; }

	/// <summary>
	/// To identify the relationship between the hierarchical object and the identified product within the PRODAT message.
	/// </summary>
	[EdiValue("X(3)", Path = "HYN/1", Mandatory = true)]
	public HierarchicalLevelCoded? HierarchicalLevelCoded { get; set; }

	/// <summary>
	/// Code specifying the action to be taken or already taken.
	/// </summary>
	[EdiValue("X(3)", Path = "HYN/2", Mandatory = false)]
	public ActionRequestNotificationCoded? ActionRequestNotificationCoded { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("HYN/3")]
	public HYN_ItemNumberIdentification? ItemNumberIdentification { get; set; }
}

/// <summary>
/// Goods identification for a specified source.
/// </summary>
[EdiElement]
public class HYN_ItemNumberIdentification
{
	/// <summary>
	/// A number allocated to a group or item.
	/// </summary>
	[EdiValue("X(35)", Path = "HYN/*/0", Mandatory = false)]
	public string? ItemNumber { get; set; }

	/// <summary>
	/// Identification of the type of item number.
	/// </summary>
	[EdiValue("X(3)", Path = "HYN/*/1", Mandatory = false)]
	public ItemNumberTypeCoded? ItemNumberTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "HYN/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "HYN/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}