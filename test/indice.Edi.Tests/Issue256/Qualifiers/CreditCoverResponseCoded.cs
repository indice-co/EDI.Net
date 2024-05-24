using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Credit cover response, coded
/// </summary>
public class CreditCoverResponseCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CreditCoverResponseCoded(string s) => new CreditCoverResponseCoded { Code = s };

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
		{ "1", "Preliminary assessment" },
		{ "2", "Approved" },
		{ "3", "Not approved" },
		{ "4", "Conditional approval" },
		{ "5", "Partly approved" },
		{ "6", "Still investigating" },
		{ "7", "Open new account only" },
		{ "8", "Cancellation" },
		{ "9", "Reduction" },
	};
}