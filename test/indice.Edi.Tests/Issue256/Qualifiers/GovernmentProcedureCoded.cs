using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Government procedure, coded
/// </summary>
public class GovernmentProcedureCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator GovernmentProcedureCoded(string s) => new GovernmentProcedureCoded { Code = s };

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
		{ "1", "Already customs cleared in the importing country" },
		{ "2", "Documents requirements completed" },
		{ "3", "Documents required" },
		{ "4", "Inspection arrangements completed" },
		{ "5", "Inspection arrangements required" },
		{ "6", "No customs procedure" },
		{ "7", "Safety arrangements completed" },
		{ "8", "Safety arrangements required" },
		{ "9", "Security arrangements required" },
		{ "10", "Storage arrangements completed" },
		{ "11", "Storage arrangements required" },
		{ "12", "Transport arrangements completed" },
		{ "13", "Transport arrangements required" },
	};
}