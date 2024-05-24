using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify damage including action taken.
/// </summary>
[EdiSegment, EdiPath("DAM")]
public class DAM
{
	/// <summary>
	/// Code giving specific meaning to the damage details.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/0", Mandatory = true)]
	public DamageDetailsQualifier? DamageDetailsQualifier { get; set; }

	/// <summary>
	/// To specify the type of damage to an object.
	/// </summary>
	[EdiPath("DAM/1")]
	public DAM_TypeOfDamage? TypeOfDamage { get; set; }

	/// <summary>
	/// To specify where the damage is on an object.
	/// </summary>
	[EdiPath("DAM/2")]
	public DAM_DamageArea? DamageArea { get; set; }

	/// <summary>
	/// To specify the severity of damage to an object.
	/// </summary>
	[EdiPath("DAM/3")]
	public DAM_DamageSeverity? DamageSeverity { get; set; }

	/// <summary>
	/// To indicate an action which has been taken or is to be taken (e.g. in relation to a certain object).
	/// </summary>
	[EdiPath("DAM/4")]
	public DAM_Action? Action { get; set; }
}

/// <summary>
/// To specify the type of damage to an object.
/// </summary>
[EdiElement]
public class DAM_TypeOfDamage
{
	/// <summary>
	/// Code specifying the type of damage to an object.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/0", Mandatory = false)]
	public string? TypeOfDamageCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description specifying the type of damage to an object.
	/// </summary>
	[EdiValue("X(35)", Path = "DAM/*/3", Mandatory = false)]
	public string? TypeOfDamage { get; set; }
}

/// <summary>
/// To specify where the damage is on an object.
/// </summary>
[EdiElement]
public class DAM_DamageArea
{
	/// <summary>
	/// Code specifying where the damage is on an object.
	/// </summary>
	[EdiValue("X(4)", Path = "DAM/*/0", Mandatory = false)]
	public string? DamageAreaIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description specifying where the damage is on an object.
	/// </summary>
	[EdiValue("X(35)", Path = "DAM/*/3", Mandatory = false)]
	public string? DamageArea { get; set; }
}

/// <summary>
/// To specify the severity of damage to an object.
/// </summary>
[EdiElement]
public class DAM_DamageSeverity
{
	/// <summary>
	/// Code specifying the severity of damage to an object.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/0", Mandatory = false)]
	public string? DamageSeverityCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description specifying the severity of damage to an object.
	/// </summary>
	[EdiValue("X(35)", Path = "DAM/*/3", Mandatory = false)]
	public string? DamageSeverity { get; set; }
}

/// <summary>
/// To indicate an action which has been taken or is to be taken (e.g. in relation to a certain object).
/// </summary>
[EdiElement]
public class DAM_Action
{
	/// <summary>
	/// Code specifying the action to be taken or already taken.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/0", Mandatory = false)]
	public ActionRequestNotificationCoded? ActionRequestNotificationCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DAM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain text specifying action taken or to be taken.
	/// </summary>
	[EdiValue("X(35)", Path = "DAM/*/3", Mandatory = false)]
	public string? ActionRequestNotification { get; set; }
}