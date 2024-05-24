using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Contribution qualifier
/// </summary>
public class ContributionQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ContributionQualifier(string s) => new ContributionQualifier { Code = s };

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
		{ "1", "Normal" },
		{ "2", "Special" },
		{ "3", "Reversal" },
		{ "4", "Back payment" },
		{ "5", "Advanced payment" },
		{ "6", "Ceasing contributions" },
		{ "ZZZ", "Mutually defined" },
	};
}