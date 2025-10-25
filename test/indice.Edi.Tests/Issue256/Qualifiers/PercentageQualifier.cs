using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Percentage qualifier
/// </summary>
public class PercentageQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PercentageQualifier(string s) => new PercentageQualifier { Code = s };

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
		{ "1", "Allowance" },
		{ "2", "Charge" },
		{ "3", "Allowance or charge" },
		{ "4", "Reinsurer's share" },
		{ "5", "Entry percentage" },
		{ "6", "Quality/yield" },
		{ "7", "Percentage of invoice" },
		{ "8", "Reduction/surcharge percentage" },
		{ "9", "Adjustment" },
		{ "10", "Bureau share" },
		{ "11", "Buffer stock requirement" },
		{ "12", "Discount" },
		{ "13", "Amount tolerance" },
		{ "14", "Percentage of note" },
		{ "15", "Penalty percentage" },
		{ "16", "Interest percentage" },
		{ "17", "Part of documentary credit amount" },
		{ "18", "Percentage credit note" },
		{ "19", "Percentage debit note" },
		{ "20", "Percentage of insurance" },
		{ "21", "Own risk percentage" },
		{ "22", "Transferred VAT percentage" },
		{ "23", "Part time employment" },
		{ "24", "Voluntary contribution" },
		{ "25", "Attribute factor" },
		{ "26", "Additional contribution" },
		{ "27", "Benefits allocation" },
		{ "28", "Attribute classification" },
		{ "29", "Renegotiation trigger upper limit" },
		{ "30", "Renegotiation trigger lower limit" },
		{ "31", "Material reduction factor" },
		{ "32", "Acceptable price difference" },
		{ "33", "Share of buyer's total requirement" },
		{ "34", "Price increase" },
		{ "35", "Share of tool cost paid by buyer" },
		{ "36", "Volume capacity usage" },
		{ "37", "Weight capacity usage" },
		{ "38", "Loading length capacity usage" },
		{ "39", "Share of packaging cost paid by vendor" },
		{ "40", "Reduction percentage" },
		{ "41", "Surcharge percentage" },
		{ "42", "Local content" },
		{ "43", "Chargeback" },
		{ "44", "Gross turnover commission" },
		{ "45", "Progress payment percentage" },
		{ "46", "Offset" },
		{ "47", "Prepaid payment percentage" },
		{ "48", "Percentage of work completed" },
	};
}