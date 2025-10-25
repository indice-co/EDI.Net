using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Identification qualifier
/// </summary>
public class IdentificationQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator IdentificationQualifier(string s) => new IdentificationQualifier { Code = s };

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
		{ "1", "Value list" },
		{ "2", "Name value in list" },
		{ "3", "Footnote" },
		{ "4", "Code value" },
		{ "5", "Data set structure" },
		{ "6", "Statistical concept" },
		{ "7", "Array segment presentation" },
		{ "8", "Data set scope" },
		{ "9", "Message number assigned by party" },
	};
}