using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To describe a physical or logical state.
/// </summary>
[EdiSegment, EdiPath("CDI")]
public class CDI
{
	/// <summary>
	/// Code giving a specific meaning to a physical or logical state.
	/// </summary>
	[EdiValue("X(3)", Path = "CDI/0", Mandatory = true)]
	public PhysicalOrLogicalStateQualifier? PhysicalOrLogicalStateQualifier { get; set; }

	/// <summary>
	/// To give information in coded or clear text form on the physical or logical state.
	/// </summary>
	[EdiPath("CDI/1")]
	public CDI_PhysicalOrLogicalStateInformation? PhysicalOrLogicalStateInformation { get; set; }
}

/// <summary>
/// To give information in coded or clear text form on the physical or logical state.
/// </summary>
[EdiElement]
public class CDI_PhysicalOrLogicalStateInformation
{
	/// <summary>
	/// Code of the physical or logical state.
	/// </summary>
	[EdiValue("X(3)", Path = "CDI/*/0", Mandatory = false)]
	public PhysicalOrLogicalStateCoded? PhysicalOrLogicalStateCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CDI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CDI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of the physical or logical state.
	/// </summary>
	[EdiValue("X(35)", Path = "CDI/*/3", Mandatory = false)]
	public string? PhysicalOrLogicalState { get; set; }
}