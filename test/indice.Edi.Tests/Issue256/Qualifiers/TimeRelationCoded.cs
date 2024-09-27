using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Time relation, coded
/// </summary>
public class TimeRelationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TimeRelationCoded(string s) => new TimeRelationCoded { Code = s };

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
		{ "1", "Reference date" },
		{ "2", "Before reference" },
		{ "3", "After reference" },
		{ "4", "End of 10-day period containing the reference date" },
		{ "5", "End of 2-week period containing the reference date" },
		{ "6", "End of month containing the reference date" },
		{ "7", "End of the month following the month of reference date" },
		{ "8", "End of quarter containing the reference date" },
		{ "9", "End of year containing the reference date" },
		{ "10", "End of week containing the reference date" },
		{ "11", "End of ten day period following month after reference date's month" },
		{ "12", "End of half year containing the reference date" },
	};
}