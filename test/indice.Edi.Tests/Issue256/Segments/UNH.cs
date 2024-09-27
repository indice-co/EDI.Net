using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To head, identify and specify a message.
/// </summary>
[EdiSegment, EdiPath("UNH")]
public class UNH
{
	/// <summary>
	/// Unique message reference assigned by the sender.
	/// </summary>
	[EdiValue("X(14)", Path = "UNH/0", Mandatory = true)]
	public string? MessageReferenceNumber { get; set; }

	/// <summary>
	/// Identification of the type, version etc. of the message being interchanged.
	/// </summary>
	[EdiPath("UNH/1")]
	public UNH_MessageIdentifier? MessageIdentifier { get; set; }

	/// <summary>
	/// Reference serving as a key to relate all subsequent transfers of data to the same business case or file.
	/// </summary>
	[EdiValue("X(35)", Path = "UNH/2", Mandatory = false)]
	public string? CommonAccessReference { get; set; }

	/// <summary>
	/// Statement that the message is one in a sequence of transfers relating to the same topic.
	/// </summary>
	[EdiPath("UNH/3")]
	public UNH_StatusOfTheTransfer? StatusOfTheTransfer { get; set; }
}

/// <summary>
/// Identification of the type, version etc. of the message being interchanged.
/// </summary>
[EdiElement]
public class UNH_MessageIdentifier
{
	/// <summary>
	/// Code identifying a type of message and assigned by its controlling agency.
	/// </summary>
	[EdiValue("X(6)", Path = "UNH/*/0", Mandatory = true)]
	public MessageTypeIdentifier? MessageTypeIdentifier { get; set; }

	/// <summary>
	/// Version number of a message type.
	/// </summary>
	[EdiValue("X(3)", Path = "UNH/*/1", Mandatory = true)]
	public MessageVersionNumber? MessageTypeVersionNumber { get; set; }

	/// <summary>
	/// Release number within the current message type version number (0052).
	/// </summary>
	[EdiValue("X(3)", Path = "UNH/*/2", Mandatory = true)]
	public MessageReleaseNumber? MessageTypeReleaseNumber { get; set; }

	/// <summary>
	/// Code to identify the agency controlling the specification, maintenance and publication of the message type.
	/// </summary>
	[EdiValue("X(2)", Path = "UNH/*/3", Mandatory = true)]
	public ControllingAgency? ControllingAgency { get; set; }

	/// <summary>
	/// A code assigned by the association responsible for the design and maintenance of the message type concerned, which further identifies the message.
	/// </summary>
	[EdiValue("X(6)", Path = "UNH/*/4", Mandatory = false)]
	public string? AssociationAssignedCode { get; set; }
}

/// <summary>
/// Statement that the message is one in a sequence of transfers relating to the same topic.
/// </summary>
[EdiElement]
public class UNH_StatusOfTheTransfer
{
	/// <summary>
	/// Number assigned by the sender indicating the numerical sequence of one or more transfers.
	/// </summary>
	[EdiValue("9(2)", Path = "UNH/*/0", Mandatory = true)]
	public decimal? SequenceMessageTransferNumber { get; set; }

	/// <summary>
	/// Indication used for the first and last message in a sequence of the same type of message relating to the same topic.
	/// </summary>
	[EdiValue("X(1)", Path = "UNH/*/1", Mandatory = false)]
	public FirstLastSequenceMessageTransferIndication? FirstLastSequenceMessageTransferIndication { get; set; }
}