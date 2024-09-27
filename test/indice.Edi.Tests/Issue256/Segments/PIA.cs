using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify additional or substitutional item identification codes.
/// </summary>
[EdiSegment, EdiPath("PIA")]
public class PIA
{
	/// <summary>
	/// Indication of the function of the product code.
	/// </summary>
	[EdiValue("X(3)", Path = "PIA/0", Mandatory = true)]
	public ProductIdFunctionQualifier? ProductIdFunctionQualifier { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("PIA/1")]
	public PIA_ItemNumberIdentification? ItemNumberIdentification1 { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("PIA/2")]
	public PIA_ItemNumberIdentification? ItemNumberIdentification2 { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("PIA/3")]
	public PIA_ItemNumberIdentification? ItemNumberIdentification3 { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("PIA/4")]
	public PIA_ItemNumberIdentification? ItemNumberIdentification4 { get; set; }

	/// <summary>
	/// Goods identification for a specified source.
	/// </summary>
	[EdiPath("PIA/5")]
	public PIA_ItemNumberIdentification? ItemNumberIdentification5 { get; set; }
}

/// <summary>
/// Goods identification for a specified source.
/// </summary>
[EdiElement]
public class PIA_ItemNumberIdentification
{
	/// <summary>
	/// A number allocated to a group or item.
	/// </summary>
	[EdiValue("X(35)", Path = "PIA/*/0", Mandatory = false)]
	public string? ItemNumber { get; set; }

	/// <summary>
	/// Identification of the type of item number.
	/// </summary>
	[EdiValue("X(3)", Path = "PIA/*/1", Mandatory = false)]
	public ItemNumberTypeCoded? ItemNumberTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PIA/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PIA/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}