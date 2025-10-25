using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Nature of cargo, coded
/// </summary>
public class NatureOfCargoCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator NatureOfCargoCoded(string s) => new NatureOfCargoCoded { Code = s };

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
		{ "1", "Documents" },
		{ "2", "Low value non-dutiable consignments" },
		{ "3", "Low value dutiable consignments" },
		{ "4", "High value consignments" },
		{ "5", "Other non-containerized" },
		{ "6", "Vehicles" },
		{ "7", "Roll-on roll-off" },
		{ "8", "Palletized" },
		{ "9", "Containerized" },
		{ "10", "Breakbulk" },
		{ "11", "Hazardous cargo" },
		{ "12", "General cargo" },
		{ "13", "Liquid cargo" },
		{ "14", "Temperature controlled cargo" },
		{ "15", "Environmental pollutant cargo" },
		{ "16", "Not-hazardous cargo" },
	};
}