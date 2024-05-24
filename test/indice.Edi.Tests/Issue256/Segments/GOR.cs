using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To indicate the requirement for a specific govermental action and/or procedure or which specific procedure is valid for a specific part of the transport.
/// </summary>
[EdiSegment, EdiPath("GOR")]
public class GOR
{
	/// <summary>
	/// Code indicating the movement of goods (e.g. import, export, transit).
	/// </summary>
	[EdiValue("X(3)", Path = "GOR/0", Mandatory = false)]
	public TransportMovementCoded? TransportMovementCoded { get; set; }

	/// <summary>
	/// Code indicating a type of government action.
	/// </summary>
	[EdiPath("GOR/1")]
	public GOR_GovernmentAction? GovernmentAction1 { get; set; }

	/// <summary>
	/// Code indicating a type of government action.
	/// </summary>
	[EdiPath("GOR/2")]
	public GOR_GovernmentAction? GovernmentAction2 { get; set; }

	/// <summary>
	/// Code indicating a type of government action.
	/// </summary>
	[EdiPath("GOR/3")]
	public GOR_GovernmentAction? GovernmentAction3 { get; set; }

	/// <summary>
	/// Code indicating a type of government action.
	/// </summary>
	[EdiPath("GOR/4")]
	public GOR_GovernmentAction? GovernmentAction4 { get; set; }
}

/// <summary>
/// Code indicating a type of government action.
/// </summary>
[EdiElement]
public class GOR_GovernmentAction
{
	/// <summary>
	/// To indicate government agencies that are involved.
	/// </summary>
	[EdiValue("X(3)", Path = "GOR/*/0", Mandatory = false)]
	public GovernmentAgencyCoded? GovernmentAgencyCoded { get; set; }

	/// <summary>
	/// Indication of requirement and status of government involvement.
	/// </summary>
	[EdiValue("X(3)", Path = "GOR/*/1", Mandatory = false)]
	public GovernmentInvolvementCoded? GovernmentInvolvementCoded { get; set; }

	/// <summary>
	/// To indicate type of government action such as inspection, detention, fumigation, security.
	/// </summary>
	[EdiValue("X(3)", Path = "GOR/*/2", Mandatory = false)]
	public GovernmentActionCoded? GovernmentActionCoded { get; set; }

	/// <summary>
	/// Code identifying the treatment applied by the government to goods which are subject to a control.
	/// </summary>
	[EdiValue("X(3)", Path = "GOR/*/3", Mandatory = false)]
	public GovernmentProcedureCoded? GovernmentProcedureCoded { get; set; }
}