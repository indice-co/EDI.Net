using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Message version number
/// </summary>
public class MessageVersionNumber
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MessageVersionNumber(string s) => new MessageVersionNumber { Code = s };

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
		{ "1", "Status 1 version" },
		{ "2", "Status 2 version" },
		{ "D", "Draft version/UN/EDIFACT Directory" },
		{ "S", "Standard version" },
		{ "88", "1988 version" },
		{ "89", "1989 version" },
		{ "90", "1990 version" },
	};
}