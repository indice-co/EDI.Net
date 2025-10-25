using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To indicate which information is requested in a responding message.
/// </summary>
[EdiSegment, EdiPath("IRQ")]
public class IRQ
{
	/// <summary>
	/// To specify the information requested in a responding message.
	/// </summary>
	[EdiPath("IRQ/0")]
	public IRQ_InformationRequest? InformationRequest { get; set; }
}

/// <summary>
/// To specify the information requested in a responding message.
/// </summary>
[EdiElement]
public class IRQ_InformationRequest
{
	/// <summary>
	/// To specify the information requested in a responding message in a coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "IRQ/*/0", Mandatory = false)]
	public RequestedInformationCoded? RequestedInformationCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IRQ/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IRQ/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify the information requested in a responding message in a clear text form.
	/// </summary>
	[EdiValue("X(35)", Path = "IRQ/*/3", Mandatory = false)]
	public string? RequestedInformation { get; set; }
}