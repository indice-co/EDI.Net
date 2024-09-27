using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Text function, coded
/// </summary>
public class TextFunctionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TextFunctionCoded(string s) => new TextFunctionCoded { Code = s };

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
		{ "1", "Text for subsequent use" },
		{ "2", "Text replacing missing code" },
		{ "3", "Text for immediate use" },
		{ "4", "No action required" },
	};
}