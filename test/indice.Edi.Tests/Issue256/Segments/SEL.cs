using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify a seal number related to equipment.
/// </summary>
[EdiSegment, EdiPath("SEL")]
public class SEL
{
	/// <summary>
	/// The number of a custom seal or another seal affixed to the containers or other transport unit.
	/// </summary>
	[EdiValue("X(10)", Path = "SEL/0", Mandatory = true)]
	public string? SealNumber { get; set; }

	/// <summary>
	/// Identification of the issuer of a seal on equipment either by code or by name.
	/// </summary>
	[EdiPath("SEL/1")]
	public SEL_SealIssuer? SealIssuer { get; set; }

	/// <summary>
	/// To indicate the condition of a seal.
	/// </summary>
	[EdiValue("X(3)", Path = "SEL/2", Mandatory = false)]
	public SealConditionCoded? SealConditionCoded { get; set; }

}

/// <summary>
/// Identification of the issuer of a seal on equipment either by code or by name.
/// </summary>
[EdiElement]
public class SEL_SealIssuer
{
	/// <summary>
	/// Identification of the issuer of the seal number.
	/// </summary>
	[EdiValue("X(3)", Path = "SEL/*/0", Mandatory = false)]
	public SealingPartyCoded? SealingPartyCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SEL/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SEL/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Clear text, representing the name of the issuer of the seal number.
	/// </summary>
	[EdiValue("X(35)", Path = "SEL/*/3", Mandatory = false)]
	public string? SealingParty { get; set; }
}