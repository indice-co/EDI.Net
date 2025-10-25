using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a segment and give its class and maintenance operation.
/// </summary>
[EdiSegment, EdiPath("SEG")]
public class SEG
{
	/// <summary>
	/// Tag of a segment.
	/// </summary>
	[EdiValue("X(3)", Path = "SEG/0", Mandatory = true)]
	public string? SegmentTag { get; set; }

	/// <summary>
	/// To identify a designated class.
	/// </summary>
	[EdiValue("X(3)", Path = "SEG/1", Mandatory = false)]
	public ClassDesignatorCoded? ClassDesignatorCoded { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "SEG/2", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}