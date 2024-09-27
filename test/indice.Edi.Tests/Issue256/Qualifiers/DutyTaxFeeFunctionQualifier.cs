using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Duty/tax/fee function qualifier
/// </summary>
public class DutyTaxFeeFunctionQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DutyTaxFeeFunctionQualifier(string s) => new DutyTaxFeeFunctionQualifier { Code = s };

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
		{ "1", "Individual duty, tax or fee (Customs item)" },
		{ "2", "Total of all duties, taxes and fees (Customs item)" },
		{ "3", "Total of each duty, tax or fee type (Customs declaration)" },
		{ "4", "Total of all duties, taxes and fee types (Customs declaration)" },
		{ "5", "Customs duty" },
		{ "6", "Fee" },
		{ "7", "Tax" },
		{ "9", "Tax related information" },
	};
}