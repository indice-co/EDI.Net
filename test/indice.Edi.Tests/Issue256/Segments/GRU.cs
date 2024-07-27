using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the usage of a segment group within a message type structure and its maintenance operation.
/// </summary>
[EdiSegment, EdiPath("GRU")]
public class GRU
{
	/// <summary>
	/// To identify a group within a message type structure.
	/// </summary>
	[EdiValue("X(4)", Path = "GRU/0", Mandatory = true)]
	public string? GroupIdentification { get; set; }

	/// <summary>
	/// To specify the designated requirement.
	/// </summary>
	[EdiValue("X(3)", Path = "GRU/1", Mandatory = false)]
	public RequirementDesignatorCoded? RequirementDesignatorCoded { get; set; }

	/// <summary>
	/// To specify the maximum number of occurrences.
	/// </summary>
	[EdiValue("9(7)", Path = "GRU/2", Mandatory = false)]
	public decimal? MaximumNumberOfOccurrences { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "GRU/3", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "GRU/4", Mandatory = false)]
	public string? SequenceNumber { get; set; }

}