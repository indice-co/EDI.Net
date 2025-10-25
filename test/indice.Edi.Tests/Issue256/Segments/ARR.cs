using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To contain the data in a array.
/// </summary>
[EdiSegment, EdiPath("ARR")]
public class ARR
{
	/// <summary>
	/// To identify the position of an object in a structure containing the object.
	/// </summary>
	[EdiPath("ARR/0")]
	public ARR_PositionIdentification? PositionIdentification { get; set; }

	/// <summary>
	/// To contain the data for a contiguous set of cells in an array.
	/// </summary>
	[EdiPath("ARR/1")]
	public ARR_ArrayCellDetails? ArrayCellDetails { get; set; }
}

/// <summary>
/// To identify the position of an object in a structure containing the object.
/// </summary>
[EdiElement]
public class ARR_PositionIdentification
{
	/// <summary>
	/// A unique number assigned by the sender to identify a level within a hierarchical structure.
	/// </summary>
	[EdiValue("X(12)", Path = "ARR/*/0", Mandatory = false)]
	public string? HierarchicalIdNumber { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "ARR/*/1", Mandatory = false)]
	public string? SequenceNumber { get; set; }
}

/// <summary>
/// To contain the data for a contiguous set of cells in an array.
/// </summary>
[EdiElement]
public class ARR_ArrayCellDetails
{
	/// <summary>
	/// Contains the data for a cell in an array.
	/// </summary>
	[EdiValue("X(35)", Path = "ARR/*/0", Mandatory = false)]
	public string? ArrayCellInformation { get; set; }
}