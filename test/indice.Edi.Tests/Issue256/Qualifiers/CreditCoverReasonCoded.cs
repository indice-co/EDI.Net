using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Credit cover reason, coded
/// </summary>
public class CreditCoverReasonCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CreditCoverReasonCoded(string s) => new CreditCoverReasonCoded { Code = s };

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
		{ "1", "Requested financial information not provided" },
		{ "2", "No financial information available" },
		{ "3", "Debtor is out of business" },
		{ "4", "Debtor is new company" },
		{ "5", "Debtor is not registered" },
		{ "6", "Debtor is non-trading company" },
		{ "7", "No comment" },
		{ "8", "Only insolvency risk covered" },
		{ "9", "Subject to acceptance of bill of exchange" },
		{ "10", "Document against acceptance" },
		{ "11", "Document against payment" },
		{ "12", "Adverse information is publicly available" },
		{ "13", "Credit limit full" },
		{ "14", "Lack of turnover" },
		{ "15", "Other" },
	};
}