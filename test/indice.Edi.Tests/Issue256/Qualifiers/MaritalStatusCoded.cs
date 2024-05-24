using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Marital status, coded
/// </summary>
public class MaritalStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MaritalStatusCoded(string s) => new MaritalStatusCoded { Code = s };

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
		{ "1", "Unmarried and never been married" },
		{ "2", "Married" },
		{ "3", "Unmarried and been married before" },
		{ "4", "Separated" },
		{ "5", "Widow or widower" },
		{ "6", "Unknown" },
	};
}