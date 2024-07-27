using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Product group type, coded
/// </summary>
public class ProductGroupTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ProductGroupTypeCoded(string s) => new ProductGroupTypeCoded { Code = s };

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
		{ "1", "Base x coefficient" },
		{ "2", "No price group used" },
		{ "3", "Catalogue" },
		{ "4", "Group of products with same price" },
		{ "5", "Itemized" },
		{ "6", "Base price plus" },
		{ "7", "Current discount group" },
		{ "8", "Previous discount group" },
		{ "9", "No group used" },
		{ "10", "Price group" },
		{ "11", "Product group" },
	};
}