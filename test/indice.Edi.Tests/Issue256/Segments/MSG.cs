using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a message type and to give its class and maintenance operation.
/// </summary>
[EdiSegment, EdiPath("MSG")]
public class MSG
{
	/// <summary>
	/// Identification of the message.
	/// </summary>
	[EdiPath("MSG/0")]
	public MSG_MessageIdentifier? MessageIdentifier { get; set; }

	/// <summary>
	/// To identify a designated class.
	/// </summary>
	[EdiValue("X(3)", Path = "MSG/1", Mandatory = false)]
	public ClassDesignatorCoded? ClassDesignatorCoded { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "MSG/2", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}

/// <summary>
/// Identification of the message.
/// </summary>
[EdiElement]
public class MSG_MessageIdentifier
{
	/// <summary>
	/// Code identifying a type of message as assigned by its controlling agency.
	/// </summary>
	[EdiValue("X(6)", Path = "MSG/*/0", Mandatory = true)]
	public string? MessageTypeIdentifier { get; set; }

	/// <summary>
	/// To specify the version number or name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "MSG/*/1", Mandatory = true)]
	public string? Version { get; set; }

	/// <summary>
	/// To specify the release number or release name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "MSG/*/2", Mandatory = true)]
	public string? Release { get; set; }

	/// <summary>
	/// Identification of the agency controlling the specification, maintenance and publication of the message.
	/// </summary>
	[EdiValue("X(2)", Path = "MSG/*/3", Mandatory = true)]
	public string? ControlAgency { get; set; }

	/// <summary>
	/// An association assigned code to further identify implementation of a message.
	/// </summary>
	[EdiValue("X(6)", Path = "MSG/*/4", Mandatory = false)]
	public string? AssociationAssignedIdentification { get; set; }

	/// <summary>
	/// To specify a revision number.
	/// </summary>
	[EdiValue("X(6)", Path = "MSG/*/5", Mandatory = false)]
	public string? RevisionNumber { get; set; }

	/// <summary>
	/// To identify the status of a document/message.
	/// </summary>
	[EdiValue("X(3)", Path = "MSG/*/6", Mandatory = false)]
	public DocumentMessageStatusCoded? DocumentMessageStatusCoded { get; set; }
}