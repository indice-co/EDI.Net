using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Code set indicator, coded
/// </summary>
public class CodeSetIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CodeSetIndicatorCoded(string s) => new CodeSetIndicatorCoded { Code = s };

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
		{ "1", "Associated code set" },
		{ "2", "No associated code set" },
	};
}