using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Government action, coded
/// </summary>
public class GovernmentActionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator GovernmentActionCoded(string s) => new GovernmentActionCoded { Code = s };

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
		{ "1", "Clearance" },
		{ "2", "Detention" },
		{ "3", "Fumigation" },
		{ "4", "Inspection" },
		{ "5", "Security" },
	};
}