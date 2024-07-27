using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Computer environment details qualifier
/// </summary>
public class ComputerEnvironmentDetailsQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ComputerEnvironmentDetailsQualifier(string s) => new ComputerEnvironmentDetailsQualifier { Code = s };

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
		{ "1", "Hardware platform" },
		{ "2", "Operating system" },
		{ "3", "Application software" },
		{ "4", "Network" },
		{ "5", "Sending system" },
	};
}