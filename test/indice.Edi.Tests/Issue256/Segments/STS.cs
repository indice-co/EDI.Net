using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the type of industry sector/application to which this segment refers, giving the status and status reason relevant to conducting business and/or services.
/// </summary>
[EdiSegment, EdiPath("STS")]
public class STS
{
	/// <summary>
	/// To specify the type of status in relation to an industry sector or business function.
	/// </summary>
	[EdiPath("STS/0")]
	public STS_StatusType? StatusType { get; set; }

	/// <summary>
	/// To specify a status event.
	/// </summary>
	[EdiPath("STS/1")]
	public STS_StatusEvent? StatusEvent { get; set; }

	/// <summary>
	/// To specify the reason behind a status event.
	/// </summary>
	[EdiPath("STS/2")]
	public STS_StatusReason? StatusReason1 { get; set; }

	/// <summary>
	/// To specify the reason behind a status event.
	/// </summary>
	[EdiPath("STS/3")]
	public STS_StatusReason? StatusReason2 { get; set; }

	/// <summary>
	/// To specify the reason behind a status event.
	/// </summary>
	[EdiPath("STS/4")]
	public STS_StatusReason? StatusReason3 { get; set; }

	/// <summary>
	/// To specify the reason behind a status event.
	/// </summary>
	[EdiPath("STS/5")]
	public STS_StatusReason? StatusReason4 { get; set; }

	/// <summary>
	/// To specify the reason behind a status event.
	/// </summary>
	[EdiPath("STS/6")]
	public STS_StatusReason? StatusReason5 { get; set; }
}

/// <summary>
/// To specify the type of status in relation to an industry sector or business function.
/// </summary>
[EdiElement]
public class STS_StatusType
{
	/// <summary>
	/// Code identifying the type of status event.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/0", Mandatory = true)]
	public StatusTypeCoded? StatusTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// To specify a status event.
/// </summary>
[EdiElement]
public class STS_StatusEvent
{
	/// <summary>
	/// Code identifying a status event.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/0", Mandatory = true)]
	public StatusEventCoded? StatusEventCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a status event.
	/// </summary>
	[EdiValue("X(35)", Path = "STS/*/3", Mandatory = false)]
	public string? StatusEvent { get; set; }
}

/// <summary>
/// To specify the reason behind a status event.
/// </summary>
[EdiElement]
public class STS_StatusReason
{
	/// <summary>
	/// Code identifying the reason behind a status event.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/0", Mandatory = true)]
	public StatusReasonCoded? StatusReasonCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "STS/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Provides the reason behind a status event.
	/// </summary>
	[EdiValue("X(35)", Path = "STS/*/3", Mandatory = false)]
	public string? StatusReason { get; set; }
}