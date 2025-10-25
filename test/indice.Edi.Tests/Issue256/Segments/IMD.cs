using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To describe an item in either an industry or free format.
/// </summary>
[EdiSegment, EdiPath("IMD")]
public class IMD
{
	/// <summary>
	/// Code indicating the format of a description.
	/// </summary>
	[EdiValue("X(3)", Path = "IMD/0", Mandatory = false)]
	public ItemDescriptionTypeCoded? ItemDescriptionTypeCoded { get; set; }

	/// <summary>
	/// Code specifying the item characteristic being described.
	/// </summary>
	[EdiValue("X(3)", Path = "IMD/1", Mandatory = false)]
	public ItemCharacteristicCoded? ItemCharacteristicCoded { get; set; }

	/// <summary>
	/// Description of an item.
	/// </summary>
	[EdiPath("IMD/2")]
	public IMD_ItemDescription? ItemDescription { get; set; }

	/// <summary>
	/// Code indicating the surface or layer of a product that is being described.
	/// </summary>
	[EdiValue("X(3)", Path = "IMD/3", Mandatory = false)]
	public SurfaceLayerIndicatorCoded? SurfaceLayerIndicatorCoded { get; set; }

}

/// <summary>
/// Description of an item.
/// </summary>
[EdiElement]
public class IMD_ItemDescription
{
	/// <summary>
	/// Code from an industry code list which provides specific data about a product characteristic.
	/// </summary>
	[EdiValue("X(17)", Path = "IMD/*/0", Mandatory = false)]
	public string? ItemDescriptionIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IMD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IMD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain language description of articles or products.
	/// </summary>
	[EdiValue("X(35)", Path = "IMD/*/3", Mandatory = false)]
	public string? ItemDescription1 { get; set; }

	/// <summary>
	/// Plain language description of articles or products.
	/// </summary>
	[EdiValue("X(35)", Path = "IMD/*/4", Mandatory = false)]
	public string? ItemDescription2 { get; set; }

	/// <summary>
	/// Code of language (ISO 639-1988).
	/// </summary>
	[EdiValue("X(3)", Path = "IMD/*/5", Mandatory = false)]
	public string? LanguageCoded { get; set; }
}