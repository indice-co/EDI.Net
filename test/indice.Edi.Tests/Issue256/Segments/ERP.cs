using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// A segment to identify the location of an application error within a message.
/// </summary>
[EdiSegment, EdiPath("ERP")]
public class ERP
{
	/// <summary>
	/// Indication of the point of error in a message.
	/// </summary>
	[EdiPath("ERP/0")]
	public ERP_ErrorPointDetails? ErrorPointDetails { get; set; }

	/// <summary>
	/// To indicate the exact segment location of an application error within a message.
	/// </summary>
	[EdiPath("ERP/1")]
	public ERP_ErrorSegmentPointDetails? ErrorSegmentPointDetails { get; set; }
}

/// <summary>
/// Indication of the point of error in a message.
/// </summary>
[EdiElement]
public class ERP_ErrorPointDetails
{
	/// <summary>
	/// Recognition of a particular part of a message.
	/// </summary>
	[EdiValue("X(3)", Path = "ERP/*/0", Mandatory = false)]
	public MessageSectionCoded? MessageSectionCoded { get; set; }

	/// <summary>
	/// The referenced number allocated to an identifiable item in a message. eg Customs item or invoice item.
	/// </summary>
	[EdiValue("X(35)", Path = "ERP/*/1", Mandatory = false)]
	public string? MessageItemNumber { get; set; }

	/// <summary>
	/// The reference number allocated to an identifiable sub-item in a message. eg: invoice line number.
	/// </summary>
	[EdiValue("9(6)", Path = "ERP/*/2", Mandatory = false)]
	public decimal? MessageSubitemNumber { get; set; }
}

/// <summary>
/// To indicate the exact segment location of an application error within a message.
/// </summary>
[EdiElement]
public class ERP_ErrorSegmentPointDetails
{
	/// <summary>
	/// Tag of a segment.
	/// </summary>
	[EdiValue("X(3)", Path = "ERP/*/0", Mandatory = false)]
	public string? SegmentTag { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "ERP/*/1", Mandatory = false)]
	public string? SequenceNumber { get; set; }

	/// <summary>
	/// Specification of the source for a specified sequence number in a coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "ERP/*/2", Mandatory = false)]
	public SequenceNumberSourceCoded? SequenceNumberSourceCoded { get; set; }
}