using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify the sequence in which physical packing is presented in the consignment, and optionally to identify the hierarchical relationship between packing layers.
/// </summary>
[EdiSegment, EdiPath("CPS")]
public class CPS
{
	/// <summary>
	/// A unique number assigned by the sender to identify a level within a hierarchical structure.
	/// </summary>
	[EdiValue("X(12)", Path = "CPS/0", Mandatory = true)]
	public string? HierarchicalIdNumber { get; set; }

	/// <summary>
	/// Identification number of the next higher hierarchical data segment in a hierarchical structure.
	/// </summary>
	[EdiValue("X(12)", Path = "CPS/1", Mandatory = false)]
	public string? HierarchicalParentId { get; set; }

	/// <summary>
	/// Indication of level of packaging specified.
	/// </summary>
	[EdiValue("X(3)", Path = "CPS/2", Mandatory = false)]
	public PackagingLevelCoded? PackagingLevelCoded { get; set; }

}