using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Monetary function, coded
/// </summary>
public class MonetaryFunctionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MonetaryFunctionCoded(string s) => new MonetaryFunctionCoded { Code = s };

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
		{ "1", "Destination charge totals" },
		{ "2", "Alternative currency amount" },
		{ "3", "Total message amount" },
		{ "4", "Invoices total amount summary" },
		{ "5", "Amount for Customs purposes" },
		{ "7", "Financial transaction amount" },
		{ "8", "Total(s) of deferred items" },
		{ "9", "Total(s) of open cash claims" },
		{ "10", "Reinsurance account balance" },
		{ "11", "Prepaid totals" },
		{ "12", "Collect totals" },
		{ "14", "Valuation amounts" },
		{ "15", "Prepayment amount" },
		{ "16", "Alternative currency total amount" },
		{ "17", "Documentary credit amount" },
		{ "18", "Additional amounts covered: freight costs" },
		{ "19", "Additional amounts covered: insurance costs" },
		{ "20", "Additional amounts covered: interest" },
		{ "21", "Additional amounts covered: inspection costs" },
		{ "22", "Part of documentary credit amount" },
		{ "23", "Amount of note" },
		{ "24", "Hash total" },
		{ "25", "Cumulative total, this period" },
		{ "26", "Period total" },
		{ "27", "Cumulative total, preceding period" },
		{ "28", "Total balance credit risk covered" },
	};
}