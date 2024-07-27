using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a footnote.
/// </summary>
[EdiSegment, EdiPath("FNT")]
public class FNT
{
	/// <summary>
	/// The identification of a footnote.
	/// </summary>
	[EdiPath("FNT/0")]
	public FNT_FootnoteIdentification? FootnoteIdentification { get; set; }

	/// <summary>
	/// Identification of a transaction party by code.
	/// </summary>
	[EdiPath("FNT/1")]
	public FNT_PartyIdentificationDetails? PartyIdentificationDetails { get; set; }

	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "FNT/2", Mandatory = false)]
	public StatusCoded? StatusCoded { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "FNT/3", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}

/// <summary>
/// The identification of a footnote.
/// </summary>
[EdiElement]
public class FNT_FootnoteIdentification
{
	/// <summary>
	/// The identifier of a footnote.
	/// </summary>
	[EdiValue("X(35)", Path = "FNT/*/0", Mandatory = true)]
	public string? FootnoteIdentifier { get; set; }

	/// <summary>
	/// Code specifying the type/source of identity number.
	/// </summary>
	[EdiValue("X(3)", Path = "FNT/*/1", Mandatory = false)]
	public IdentityNumberQualifier? IdentityNumberQualifier { get; set; }
}

/// <summary>
/// Identification of a transaction party by code.
/// </summary>
[EdiElement]
public class FNT_PartyIdentificationDetails
{
	/// <summary>
	/// Code identifying a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "FNT/*/0", Mandatory = true)]
	public string? PartyIdIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "FNT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "FNT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}