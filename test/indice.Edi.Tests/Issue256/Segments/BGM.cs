using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To indicate the type and function of a message and to transmit the identifying number.
/// </summary>
[EdiSegment, EdiPath("BGM")]
public class BGM
{
	/// <summary>
	/// Identification of a type of document/message by code or name. Code preferred.
	/// </summary>
	[EdiPath("BGM/0")]
	public BGM_DocumentMessageName? DocumentMessageName { get; set; }

	/// <summary>
	/// Identification of a document/message by its number and eventually its version or revision.
	/// </summary>
	[EdiPath("BGM/1")]
	public BGM_DocumentMessageIdentification? DocumentMessageIdentification { get; set; }

	/// <summary>
	/// Code indicating the function of the message.
	/// </summary>
	[EdiValue("X(3)", Path = "BGM/2", Mandatory = false)]
	public MessageFunctionCoded? MessageFunctionCoded { get; set; }

	/// <summary>
	/// Code specifying the type of acknowledgment required or transmitted.
	/// </summary>
	[EdiValue("X(3)", Path = "BGM/3", Mandatory = false)]
	public ResponseTypeCoded? ResponseTypeCoded { get; set; }

}

/// <summary>
/// Identification of a type of document/message by code or name. Code preferred.
/// </summary>
[EdiElement]
public class BGM_DocumentMessageName
{
	/// <summary>
	/// Document/message identifier expressed in code.
	/// </summary>
	[EdiValue("X(3)", Path = "BGM/*/0", Mandatory = false)]
	public DocumentMessageNameCoded? DocumentMessageNameCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "BGM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "BGM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain language identifier specifying the function of a document/message.
	/// </summary>
	[EdiValue("X(35)", Path = "BGM/*/3", Mandatory = false)]
	public string? DocumentMessageName { get; set; }
}

/// <summary>
/// Identification of a document/message by its number and eventually its version or revision.
/// </summary>
[EdiElement]
public class BGM_DocumentMessageIdentification
{
	/// <summary>
	/// Reference number assigned to the document/message by the issuer.
	/// </summary>
	[EdiValue("X(35)", Path = "BGM/*/0", Mandatory = false)]
	public string? DocumentMessageNumber { get; set; }

	/// <summary>
	/// To specify the version number or name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "BGM/*/1", Mandatory = false)]
	public string? Version { get; set; }

	/// <summary>
	/// To specify a revision number.
	/// </summary>
	[EdiValue("X(6)", Path = "BGM/*/2", Mandatory = false)]
	public string? RevisionNumber { get; set; }
}