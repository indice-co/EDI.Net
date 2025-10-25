using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Packaging related information, coded
/// </summary>
public class PackagingRelatedInformationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PackagingRelatedInformationCoded(string s) => new PackagingRelatedInformationCoded { Code = s };

	/// <summary>
	/// Code of the value
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// Value converted from code
	/// </summary>
	public string? Value => Code is null ? null : (Qualifiers.ContainsKey(Code) ? Qualifiers[Code] : null);

	/// <summary>
	/// All possible values
	/// </summary>
	[JsonIgnore]
	public Dictionary<string, string> Qualifiers => new Dictionary<string, string>
	{
		{ "34", "Product marking" },
		{ "35", "Type of package" },
		{ "36", "Package specifications" },
		{ "37", "Package protection" },
		{ "38", "Tarping" },
		{ "39", "Platform/skid location" },
		{ "40", "Bearing piece location" },
		{ "41", "Skid/pallet type" },
		{ "42", "Placement on carrier" },
		{ "43", "Spacing directions" },
		{ "44", "Unloading device" },
		{ "45", "Unloading equipment" },
		{ "50", "Package barcoded EAN-13 or EAN-8" },
		{ "51", "Package barcoded ITF-14 or ITF-6" },
		{ "52", "Package barcoded UCC or EAN-128" },
		{ "53", "Package price marked" },
		{ "54", "Product ingredients marked on package" },
		{ "55", "Core characteristics" },
		{ "56", "Shipping requirement" },
		{ "57", "Customs requirement" },
		{ "58", "Transport contract requirement" },
		{ "59", "Preservation method" },
		{ "60", "Product marking pattern" },
		{ "61", "Product marking location" },
		{ "62", "Package/container mark location" },
		{ "63", "Marking method" },
		{ "66", "Receiving facility limitations" },
		{ "67", "Tagging/bar code instructions" },
		{ "68", "Shipping package labelling" },
		{ "69", "Shipping package sealing" },
		{ "70", "Optional packaging procedure" },
		{ "71", "Cleaning or drying specification" },
		{ "72", "Cushioning thickness specification" },
		{ "73", "Cushioning and dunnage specification" },
		{ "74", "Level of preservation specification" },
		{ "75", "Preservation material specification" },
		{ "76", "Unit container specification" },
		{ "77", "Material wrapping specification" },
	};
}