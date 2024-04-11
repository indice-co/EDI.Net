using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Price qualifier
/// </summary>
public class PriceQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PriceQualifier(string s) => new PriceQualifier { Code = s };

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
		{ "AAA", "Calculation net" },
		{ "AAB", "Calculation gross" },
		{ "AAC", "Allowances and charges not included, tax included" },
		{ "AAD", "Average selling price" },
		{ "AAE", "Information price, excluding allowances or charges, including taxes" },
		{ "AAF", "Information price, excluding allowances or charges, and taxes" },
		{ "AAG", "Additive unit price component" },
		{ "CAL", "Calculation price" },
		{ "INF", "Information" },
		{ "INV", "Invoice price" },
	};
}