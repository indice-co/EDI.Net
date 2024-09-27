using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Temperature qualifier
/// </summary>
public class TemperatureQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TemperatureQualifier(string s) => new TemperatureQualifier { Code = s };

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
		{ "1", "Storage temperature" },
		{ "2", "Transport temperature" },
		{ "3", "Cargo operating temperature" },
		{ "4", "Transport emergency temperature" },
		{ "5", "Transport control temperature" },
	};
}