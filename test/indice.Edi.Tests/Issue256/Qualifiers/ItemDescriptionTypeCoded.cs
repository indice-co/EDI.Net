using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Item description type, coded
/// </summary>
public class ItemDescriptionTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ItemDescriptionTypeCoded(string s) => new ItemDescriptionTypeCoded { Code = s };

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
		{ "A", "Free-form long description" },
		{ "B", "Code and text" },
		{ "C", "Code (from industry code list)" },
		{ "D", "Free-form price look up" },
		{ "E", "Free-form short description" },
		{ "F", "Free-form" },
		{ "S", "Structured (from industry code list)" },
		{ "X", "Semi-structured (code + text)" },
	};
}