using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Number of units qualifier
/// </summary>
public class NumberOfUnitsQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator NumberOfUnitsQualifier(string s) => new NumberOfUnitsQualifier { Code = s };

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
		{ "1", "Number of pricing units" },
		{ "2", "Transportable unit" },
		{ "3", "Number of debit units" },
		{ "4", "Number of received units" },
		{ "5", "Number of free days for container availability" },
	};
}