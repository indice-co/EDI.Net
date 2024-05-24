using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Government agency, coded
/// </summary>
public class GovernmentAgencyCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator GovernmentAgencyCoded(string s) => new GovernmentAgencyCoded { Code = s };

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
		{ "1", "Agriculture" },
		{ "2", "Ammunition" },
		{ "3", "Commerce" },
		{ "4", "Coastguard" },
		{ "5", "Customs" },
		{ "6", "Food and drug" },
		{ "7", "Health certificate" },
		{ "8", "Harbour police" },
		{ "9", "Immigration" },
		{ "10", "Live animals" },
		{ "11", "Port authority" },
		{ "12", "Public health" },
		{ "13", "Transportation" },
	};
}