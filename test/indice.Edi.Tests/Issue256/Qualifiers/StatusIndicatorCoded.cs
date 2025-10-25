using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Status indicator, coded
/// </summary>
public class StatusIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator StatusIndicatorCoded(string s) => new StatusIndicatorCoded { Code = s };

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
		{ "1", "Amendment" },
		{ "2", "Cancellation" },
		{ "3", "Created new" },
		{ "4", "No change" },
		{ "5", "Replacement" },
		{ "6", "Agreement" },
		{ "7", "Proposal" },
		{ "8", "Suggested correction" },
		{ "9", "Test/do not deliver" },
		{ "10", "Already delivered" },
		{ "11", "Reporting item details included" },
		{ "12", "No advice" },
		{ "13", "Reporting item details sent separately" },
		{ "14", "Reporting item details to follow" },
	};
}