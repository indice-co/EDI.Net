using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the usage of a data element.
/// </summary>
[EdiSegment, EdiPath("ELU")]
public class ELU
{
	/// <summary>
	/// Tag of a data element.
	/// </summary>
	[EdiValue("X(4)", Path = "ELU/0", Mandatory = true)]
	public string? DataElementTag { get; set; }

	/// <summary>
	/// To specify the designated requirement.
	/// </summary>
	[EdiValue("X(3)", Path = "ELU/1", Mandatory = false)]
	public RequirementDesignatorCoded? RequirementDesignatorCoded { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "ELU/2", Mandatory = false)]
	public string? SequenceNumber { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "ELU/3", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}