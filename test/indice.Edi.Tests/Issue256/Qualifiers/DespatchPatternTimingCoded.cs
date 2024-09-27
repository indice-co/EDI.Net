using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Despatch pattern timing, coded
/// </summary>
public class DespatchPatternTimingCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DespatchPatternTimingCoded(string s) => new DespatchPatternTimingCoded { Code = s };

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
		{ "A", "1st shift (normal working hours)" },
		{ "B", "2nd shift" },
		{ "C", "3rd shift" },
		{ "D", "A.M." },
		{ "E", "P.M." },
		{ "F", "As directed" },
		{ "G", "Any shift" },
		{ "H", "24 hour clock" },
		{ "Y", "None" },
		{ "ZZZ", "Mutually defined" },
	};
}