using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Requirement designator, coded
/// </summary>
public class RequirementDesignatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RequirementDesignatorCoded(string s) => new RequirementDesignatorCoded { Code = s };

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
		{ "1", "Conditional" },
		{ "2", "Mandatory" },
		{ "3", "Optional" },
		{ "4", "Floating" },
	};
}