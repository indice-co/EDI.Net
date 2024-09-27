using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Name type, coded
/// </summary>
public class NameTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator NameTypeCoded(string s) => new NameTypeCoded { Code = s };

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
		{ "1", "Maiden name" },
		{ "2", "Marital name" },
		{ "3", "Used name" },
		{ "4", "Call name" },
		{ "5", "Official name" },
		{ "7", "Pseudonym" },
		{ "8", "Alias" },
	};
}