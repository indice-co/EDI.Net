using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide specific details related to the delivery sequence.
/// </summary>
[EdiSegment, EdiPath("SEQ")]
public class SEQ
{
	/// <summary>
	/// Specification of a status or disposition.
	/// </summary>
	[EdiValue("X(3)", Path = "SEQ/0", Mandatory = false)]
	public StatusIndicatorCoded? StatusIndicatorCoded { get; set; }

	/// <summary>
	/// Identification of a sequence and source for sequencing.
	/// </summary>
	[EdiPath("SEQ/1")]
	public SEQ_SequenceInformation? SequenceInformation { get; set; }
}

/// <summary>
/// Identification of a sequence and source for sequencing.
/// </summary>
[EdiElement]
public class SEQ_SequenceInformation
{
	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "SEQ/*/0", Mandatory = true)]
	public string? SequenceNumber { get; set; }

	/// <summary>
	/// Specification of the source for a specified sequence number in a coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "SEQ/*/1", Mandatory = false)]
	public SequenceNumberSourceCoded? SequenceNumberSourceCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SEQ/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SEQ/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}