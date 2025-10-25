using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify the type of application error within a message.
/// </summary>
[EdiSegment, EdiPath("ERC")]
public class ERC
{
	/// <summary>
	/// Code assigned by the recipient of a message to indicate a data validation error condition.
	/// </summary>
	[EdiPath("ERC/0")]
	public ERC_ApplicationErrorDetail? ApplicationErrorDetail { get; set; }
}

/// <summary>
/// Code assigned by the recipient of a message to indicate a data validation error condition.
/// </summary>
[EdiElement]
public class ERC_ApplicationErrorDetail
{
	/// <summary>
	/// The code assigned by the receiver of a message to the identification of a data validation error condition.
	/// </summary>
	[EdiValue("X(8)", Path = "ERC/*/0", Mandatory = true)]
	public string? ApplicationErrorIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ERC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ERC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}