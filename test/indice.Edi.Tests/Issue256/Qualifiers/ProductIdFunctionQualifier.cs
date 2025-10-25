using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Product id. function qualifier
/// </summary>
public class ProductIdFunctionQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ProductIdFunctionQualifier(string s) => new ProductIdFunctionQualifier { Code = s };

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
		{ "1", "Additional identification" },
		{ "2", "Identification for potential substitution" },
		{ "3", "Substituted by" },
		{ "4", "Substituted for" },
		{ "5", "Product identification" },
		{ "6", "Successor product id" },
		{ "7", "Predecessor product id" },
		{ "8", "Expired/out of production" },
		{ "9", "Deletion of secondary identification" },
	};
}