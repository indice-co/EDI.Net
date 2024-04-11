using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Packaging level, coded
/// </summary>
public class PackagingLevelCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PackagingLevelCoded(string s) => new PackagingLevelCoded { Code = s };

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
		{ "1", "Inner" },
		{ "2", "Intermediate" },
		{ "3", "Outer" },
		{ "4", "No packaging hierarchy" },
	};
}