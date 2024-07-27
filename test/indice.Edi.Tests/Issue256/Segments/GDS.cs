using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To indicate the type of cargo as a general classification.
/// </summary>
[EdiSegment, EdiPath("GDS")]
public class GDS
{
	/// <summary>
	/// Rough classification of a type of cargo.
	/// </summary>
	[EdiPath("GDS/0")]
	public GDS_NatureOfCargo? NatureOfCargo { get; set; }
}

/// <summary>
/// Rough classification of a type of cargo.
/// </summary>
[EdiElement]
public class GDS_NatureOfCargo
{
	/// <summary>
	/// Code indicating the type of cargo as a rough classification.
	/// </summary>
	[EdiValue("X(3)", Path = "GDS/*/0", Mandatory = true)]
	public NatureOfCargoCoded? NatureOfCargoCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "GDS/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "GDS/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}