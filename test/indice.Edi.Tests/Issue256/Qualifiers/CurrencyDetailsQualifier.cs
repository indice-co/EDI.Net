using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Currency details qualifier
/// </summary>
public class CurrencyDetailsQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CurrencyDetailsQualifier(string s) => new CurrencyDetailsQualifier { Code = s };

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
		{ "1", "Charge payment currency" },
		{ "2", "Reference currency" },
		{ "3", "Target currency" },
		{ "4", "Transport document currency" },
		{ "5", "Calculation base currency" },
		{ "6", "Information Currency" },
		{ "7", "Currency of the account" },
	};
}