using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Rate type qualifier
/// </summary>
public class RateTypeQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RateTypeQualifier(string s) => new RateTypeQualifier { Code = s };

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
		{ "1", "Allowance rate" },
		{ "2", "Charge rate" },
		{ "3", "Actual versus calculated price difference rate" },
	};
}