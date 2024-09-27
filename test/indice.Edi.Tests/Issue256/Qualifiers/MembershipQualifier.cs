using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Membership qualifier
/// </summary>
public class MembershipQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MembershipQualifier(string s) => new MembershipQualifier { Code = s };

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
		{ "1", "Life insurance" },
		{ "2", "Superannuation" },
		{ "ZZZ", "Mutually defined" },
	};
}