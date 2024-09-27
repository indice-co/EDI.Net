using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Statistic type, coded
/// </summary>
public class StatisticTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator StatisticTypeCoded(string s) => new StatisticTypeCoded { Code = s };

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
		{ "1", "Mean average" },
		{ "2", "Median" },
		{ "3", "Estimate" },
		{ "4", "Efficiency performance" },
		{ "5", "Process capability upper" },
		{ "6", "Process capability lower" },
		{ "7", "Process capability CPK" },
		{ "8", "Range average" },
		{ "9", "Standard deviation" },
		{ "10", "In limits" },
		{ "11", "On gauge" },
	};
}