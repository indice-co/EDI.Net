using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Sample description, coded
/// </summary>
public class SampleDescriptionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SampleDescriptionCoded(string s) => new SampleDescriptionCoded { Code = s };

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
		{ "1", "Round" },
		{ "2", "Rectangular" },
		{ "3", "Turned" },
		{ "4", "Forged" },
		{ "5", "Tinned" },
		{ "6", "Prismatic" },
		{ "7", "Cylindric" },
	};
}