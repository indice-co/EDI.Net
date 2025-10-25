using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify one consignment.
/// </summary>
[EdiSegment, EdiPath("CNI")]
public class CNI
{
	/// <summary>
	/// Serial number differentiating each separate consignment included in the consolidation.
	/// </summary>
	[EdiValue("9(4)", Path = "CNI/0", Mandatory = false)]
	public decimal? ConsolidationItemNumber { get; set; }

	/// <summary>
	/// Identification of document/message by number, status, source and/or language.
	/// </summary>
	[EdiPath("CNI/1")]
	public CNI_DocumentMessageDetails? DocumentMessageDetails { get; set; }

	/// <summary>
	/// To indicate the sequence of loading in which the consignments are loaded in a piece of equipment or in a means of transport (e.g. truck).
	/// </summary>
	[EdiValue("9(4)", Path = "CNI/2", Mandatory = false)]
	public decimal? ConsignmentLoadSequenceNumber { get; set; }

}

/// <summary>
/// Identification of document/message by number, status, source and/or language.
/// </summary>
[EdiElement]
public class CNI_DocumentMessageDetails
{
	/// <summary>
	/// Reference number assigned to the document/message by the issuer.
	/// </summary>
	[EdiValue("X(35)", Path = "CNI/*/0", Mandatory = false)]
	public string? DocumentMessageNumber { get; set; }

	/// <summary>
	/// To identify the status of a document/message.
	/// </summary>
	[EdiValue("X(3)", Path = "CNI/*/1", Mandatory = false)]
	public DocumentMessageStatusCoded? DocumentMessageStatusCoded { get; set; }

	/// <summary>
	/// Indication of the source from which the printed information is to be or has been obtained.
	/// </summary>
	[EdiValue("X(35)", Path = "CNI/*/2", Mandatory = false)]
	public string? DocumentMessageSource { get; set; }

	/// <summary>
	/// Code of language (ISO 639-1988).
	/// </summary>
	[EdiValue("X(3)", Path = "CNI/*/3", Mandatory = false)]
	public string? LanguageCoded { get; set; }
}