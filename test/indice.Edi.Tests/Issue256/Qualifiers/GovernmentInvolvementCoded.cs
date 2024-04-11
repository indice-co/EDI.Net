using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Government involvement, coded
/// </summary>
public class GovernmentInvolvementCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator GovernmentInvolvementCoded(string s) => new GovernmentInvolvementCoded { Code = s };

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
		{ "1", "Carried out as instructed" },
		{ "2", "Carried out as amended" },
		{ "3", "Completed" },
		{ "4", "Not applicable" },
		{ "5", "Optimal" },
		{ "6", "Required" },
		{ "7", "Applicable" },
	};
}