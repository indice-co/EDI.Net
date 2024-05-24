using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Language qualifier
/// </summary>
public class LanguageQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator LanguageQualifier(string s) => new LanguageQualifier { Code = s };

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
		{ "1", "Language normally used" },
		{ "2", "Language understood" },
		{ "3", "Spoken language" },
		{ "4", "Written language" },
		{ "5", "Read language" },
		{ "6", "For all types of communication" },
	};
}