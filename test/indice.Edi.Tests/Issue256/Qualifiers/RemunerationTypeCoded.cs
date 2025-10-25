using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Remuneration type, coded
/// </summary>
public class RemunerationTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RemunerationTypeCoded(string s) => new RemunerationTypeCoded { Code = s };

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
		{ "1", "Minimum guaranteed wages" },
		{ "2", "Basic remuneration" },
		{ "3", "Net wages" },
	};
}