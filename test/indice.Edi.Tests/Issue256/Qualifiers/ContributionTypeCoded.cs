using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Contribution type, coded
/// </summary>
public class ContributionTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ContributionTypeCoded(string s) => new ContributionTypeCoded { Code = s };

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
		{ "1", "Company" },
		{ "2", "Company award" },
		{ "3", "Company over-award" },
		{ "4", "Lump sum" },
		{ "5", "Company additional" },
		{ "6", "Company voluntary" },
		{ "7", "Member voluntary" },
		{ "8", "Member additional" },
		{ "9", "Member individual" },
		{ "10", "Group" },
		{ "11", "Other" },
		{ "ZZZ", "Mutually defined" },
	};
}