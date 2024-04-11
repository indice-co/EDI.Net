using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a specific attribute.
/// </summary>
[EdiSegment, EdiPath("ATT")]
public class ATT
{
	/// <summary>
	/// Specification of the meaning of an attribute function.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/0", Mandatory = true)]
	public AttributeFunctionQualifier? AttributeFunctionQualifier { get; set; }

	/// <summary>
	/// Identification of the type of attribute.
	/// </summary>
	[EdiPath("ATT/1")]
	public ATT_AttributeType? AttributeType { get; set; }

	/// <summary>
	/// Identification of the attribute related to an entity.
	/// </summary>
	[EdiPath("ATT/2")]
	public ATT_AttributeDetails? AttributeDetails { get; set; }
}

/// <summary>
/// Identification of the type of attribute.
/// </summary>
[EdiElement]
public class ATT_AttributeType
{
	/// <summary>
	/// Coded specification of the type attribute.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/*/0", Mandatory = true)]
	public string? AttributeTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Identification of the attribute related to an entity.
/// </summary>
[EdiElement]
public class ATT_AttributeDetails
{
	/// <summary>
	/// Identification of an attribute.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/*/0", Mandatory = false)]
	public string? AttributeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ATT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of an attribute of an entity.
	/// </summary>
	[EdiValue("X(35)", Path = "ATT/*/3", Mandatory = false)]
	public string? Attribute { get; set; }
}