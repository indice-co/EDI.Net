using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Name status, coded
/// </summary>
public class NameStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator NameStatusCoded(string s) => new NameStatusCoded { Code = s };

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
		{ "1", "Name given at birth" },
		{ "2", "Current name" },
		{ "3", "Previous name" },
		{ "5", "Checked" },
		{ "6", "Not checked" },
	};
}