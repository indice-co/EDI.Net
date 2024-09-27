using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Physical or logical state qualifier
/// </summary>
public class PhysicalOrLogicalStateQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PhysicalOrLogicalStateQualifier(string s) => new PhysicalOrLogicalStateQualifier { Code = s };

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
		{ "1", "Upon receipt" },
		{ "2", "Upon despatch" },
	};
}