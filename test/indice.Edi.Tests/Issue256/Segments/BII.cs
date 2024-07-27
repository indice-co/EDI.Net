using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// A segment used to convey an indexing structure mechanism which identifies the positioning of a group or item.
/// </summary>
[EdiSegment, EdiPath("BII")]
public class BII
{
	/// <summary>
	/// To qualify the indexing structure established for a project.
	/// </summary>
	[EdiValue("X(3)", Path = "BII/0", Mandatory = true)]
	public IndexingStructureQualifier? IndexingStructureQualifier { get; set; }

	/// <summary>
	/// A sequenced collection of facetted codes used for multiple indexing purposes.
	/// </summary>
	[EdiPath("BII/1")]
	public BII_BillLevelIdentification? BillLevelIdentification { get; set; }

	/// <summary>
	/// A number allocated to a group or item.
	/// </summary>
	[EdiValue("X(35)", Path = "BII/2", Mandatory = false)]
	public string? ItemNumber { get; set; }

}

/// <summary>
/// A sequenced collection of facetted codes used for multiple indexing purposes.
/// </summary>
[EdiElement]
public class BII_BillLevelIdentification
{
	/// <summary>
	/// The first facet of a sequencing mechanism used to position an item within an indexing structure.
	/// </summary>
	[EdiValue("X(17)", Path = "BII/*/0", Mandatory = false)]
	public string? LevelOneId { get; set; }

	/// <summary>
	/// The second facet of a sequencing mechanism used to position an item within an indexing structure.
	/// </summary>
	[EdiValue("X(17)", Path = "BII/*/1", Mandatory = false)]
	public string? LevelTwoId { get; set; }

	/// <summary>
	/// The third facet of a sequencing mechanism used to position an item within an indexing structure.
	/// </summary>
	[EdiValue("X(17)", Path = "BII/*/2", Mandatory = false)]
	public string? LevelThreeId { get; set; }

	/// <summary>
	/// The fourth facet of a sequencing mechanism used to position an item within an indexing structure.
	/// </summary>
	[EdiValue("X(17)", Path = "BII/*/3", Mandatory = false)]
	public string? LevelFourId { get; set; }

	/// <summary>
	/// The fifth facet of a sequencing mechanism used to position an item within an indexing structure.
	/// </summary>
	[EdiValue("X(17)", Path = "BII/*/4", Mandatory = false)]
	public string? LevelFiveId { get; set; }

	/// <summary>
	/// The sixth facet of a sequencing mechanism used to position an item within an indexing structure.
	/// </summary>
	[EdiValue("X(17)", Path = "BII/*/5", Mandatory = false)]
	public string? LevelSixId { get; set; }
}