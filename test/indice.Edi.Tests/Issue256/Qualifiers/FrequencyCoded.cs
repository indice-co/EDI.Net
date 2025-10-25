using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Frequency, coded
/// </summary>
public class FrequencyCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator FrequencyCoded(string s) => new FrequencyCoded { Code = s };

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
		{ "A", "Annually (calendar year)" },
		{ "B", "Continuous" },
		{ "C", "Synchronous" },
		{ "D", "Discrete" },
		{ "E", "Replenishment" },
		{ "F", "Flexible interval (from date X through date Y)" },
		{ "J", "Just-in-time" },
		{ "M", "Monthly (calendar months)" },
		{ "Q", "Quarterly (calendar quarters)" },
		{ "S", "Semi-annually (calendar year)" },
		{ "T", "Four week period (13 periods per year)" },
		{ "W", "Weekly" },
		{ "Y", "Daily" },
		{ "ZZZ", "Mutually defined" },
	};
}