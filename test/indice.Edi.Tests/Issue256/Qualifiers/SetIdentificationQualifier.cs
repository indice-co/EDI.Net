using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Set identification qualifier
/// </summary>
public class SetIdentificationQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SetIdentificationQualifier(string s) => new SetIdentificationQualifier { Code = s };

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
		{ "1", "Product" },
		{ "2", "Licence" },
		{ "3", "Package" },
		{ "4", "Vehicle reference set" },
		{ "5", "Source database" },
		{ "6", "Target database" },
		{ "7", "Value list" },
		{ "8", "Contract" },
		{ "9", "Financial security" },
		{ "10", "Accounting" },
	};
}