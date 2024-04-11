using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Name component qualifier
/// </summary>
public class NameComponentQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator NameComponentQualifier(string s) => new NameComponentQualifier { Code = s };

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
		{ "1", "Surname" },
		{ "2", "Christian name" },
		{ "3", "First part of a composite surname" },
		{ "5", "Second part of a composite surname" },
		{ "6", "Official first Christian name" },
		{ "7", "Official second Christian name" },
		{ "8", "Initial of the second Christian name" },
		{ "9", "Official third Christian name" },
		{ "10", "Whole name" },
	};
}