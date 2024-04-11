using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Membership category identification
/// </summary>
public class MembershipCategoryIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MembershipCategoryIdentification(string s) => new MembershipCategoryIdentification { Code = s };

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
		{ "1", "Family" },
		{ "2", "Unaccompanied person" },
		{ "3", "Senior person" },
		{ "4", "Child" },
	};
}