using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify documents, either printed, electronically transferred, or referenced as specified in message description, including, where relevant, the identification of the type of transaction that will result from this message.
/// </summary>
[EdiSegment, EdiPath("DOC")]
public class DOC
{
	/// <summary>
	/// Identification of a type of document/message by code or name. Code preferred.
	/// </summary>
	[EdiPath("DOC/0")]
	public DOC_DocumentMessageName? DocumentMessageName { get; set; }

	/// <summary>
	/// Identification of document/message by number, status, source and/or language.
	/// </summary>
	[EdiPath("DOC/1")]
	public DOC_DocumentMessageDetails? DocumentMessageDetails { get; set; }

	/// <summary>
	/// Code identifying the type of communication channel being used.
	/// </summary>
	[EdiValue("X(3)", Path = "DOC/2", Mandatory = false)]
	public CommunicationChannelIdentifierCoded? CommunicationChannelIdentifierCoded { get; set; }

	/// <summary>
	/// Number of originals or copies of a document stipulated or referred to as being required.
	/// </summary>
	[EdiValue("9(2)", Path = "DOC/3", Mandatory = false)]
	public decimal? NumberOfCopiesOfDocumentRequired { get; set; }

	/// <summary>
	/// Specification of the number of originals of a stipulated document that are required.
	/// </summary>
	[EdiValue("9(2)", Path = "DOC/4", Mandatory = false)]
	public decimal? NumberOfOriginalsOfDocumentRequired { get; set; }

}

/// <summary>
/// Identification of a type of document/message by code or name. Code preferred.
/// </summary>
[EdiElement]
public class DOC_DocumentMessageName
{
	/// <summary>
	/// Document/message identifier expressed in code.
	/// </summary>
	[EdiValue("X(3)", Path = "DOC/*/0", Mandatory = false)]
	public DocumentMessageNameCoded? DocumentMessageNameCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DOC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DOC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain language identifier specifying the function of a document/message.
	/// </summary>
	[EdiValue("X(35)", Path = "DOC/*/3", Mandatory = false)]
	public string? DocumentMessageName { get; set; }
}

/// <summary>
/// Identification of document/message by number, status, source and/or language.
/// </summary>
[EdiElement]
public class DOC_DocumentMessageDetails
{
	/// <summary>
	/// Reference number assigned to the document/message by the issuer.
	/// </summary>
	[EdiValue("X(35)", Path = "DOC/*/0", Mandatory = false)]
	public string? DocumentMessageNumber { get; set; }

	/// <summary>
	/// To identify the status of a document/message.
	/// </summary>
	[EdiValue("X(3)", Path = "DOC/*/1", Mandatory = false)]
	public DocumentMessageStatusCoded? DocumentMessageStatusCoded { get; set; }

	/// <summary>
	/// Indication of the source from which the printed information is to be or has been obtained.
	/// </summary>
	[EdiValue("X(35)", Path = "DOC/*/2", Mandatory = false)]
	public string? DocumentMessageSource { get; set; }

	/// <summary>
	/// Code of language (ISO 639-1988).
	/// </summary>
	[EdiValue("X(3)", Path = "DOC/*/3", Mandatory = false)]
	public string? LanguageCoded { get; set; }
}