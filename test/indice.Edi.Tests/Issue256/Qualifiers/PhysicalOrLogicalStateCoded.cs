using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Physical or logical state, coded
/// </summary>
public class PhysicalOrLogicalStateCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PhysicalOrLogicalStateCoded(string s) => new PhysicalOrLogicalStateCoded { Code = s };

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
		{ "1", "Split" },
		{ "2", "Missing" },
		{ "3", "Wrong identification" },
		{ "4", "Damaged" },
	};
}