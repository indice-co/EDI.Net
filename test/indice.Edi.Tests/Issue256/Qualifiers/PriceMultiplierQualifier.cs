using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Price multiplier qualifier
/// </summary>
public class PriceMultiplierQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PriceMultiplierQualifier(string s) => new PriceMultiplierQualifier { Code = s };

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
		{ "A", "Price adjustment coefficient" },
		{ "B", "Escalation coefficient" },
		{ "C", "Timesing factor" },
		{ "CSD", "Cost markup multiplier - original cost" },
		{ "CSR", "Cost markup multiplier - retail cost" },
		{ "DIS", "Discount multiplier" },
		{ "SEL", "Selling multiplier" },
	};
}