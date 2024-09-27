using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Calculation sequence indicator, coded
/// </summary>
public class CalculationSequenceIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CalculationSequenceIndicatorCoded(string s) => new CalculationSequenceIndicatorCoded { Code = s };

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
		{ "1", "First step of calculation" },
		{ "2", "Second step of calculation" },
		{ "3", "Third step of calculation" },
		{ "4", "Fourth step of calculation" },
		{ "5", "Fifth step of calculation" },
		{ "6", "Sixth step of calculation" },
		{ "7", "Seventh step of calculation" },
		{ "8", "Eighth step of calculation" },
		{ "9", "Ninth step of calculation" },
	};
}