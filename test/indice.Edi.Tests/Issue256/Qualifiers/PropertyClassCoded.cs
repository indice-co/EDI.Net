using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Property class, coded
/// </summary>
public class PropertyClassCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PropertyClassCoded(string s) => new PropertyClassCoded { Code = s };

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
		{ "1", "Chemistry" },
		{ "2", "Mechanical" },
		{ "3", "Component of measurable quantity" },
		{ "4", "System of measurable quantity" },
	};
}