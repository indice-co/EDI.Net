using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Charge/allowance description, coded
/// </summary>
public class ChargeAllowanceDescriptionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ChargeAllowanceDescriptionCoded(string s) => new ChargeAllowanceDescriptionCoded { Code = s };

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
		{ "1", "Handling commission" },
		{ "2", "Amendment commission" },
		{ "3", "Acceptance commission" },
		{ "4", "Commission for obtaining acceptance" },
		{ "5", "Commission on delivery" },
		{ "6", "Advising commission" },
		{ "7", "Confirmation commission" },
		{ "8", "Deferred payment commission" },
		{ "9", "Commission for taking up documents" },
		{ "10", "Opening commission" },
		{ "11", "Fee for payment under reserve" },
		{ "12", "Discrepancy fee" },
		{ "13", "Domicilation commission" },
		{ "14", "Commission for release of goods" },
		{ "15", "Collection commission" },
		{ "16", "Negotiation commission" },
		{ "17", "Return commission" },
		{ "18", "B/L splitting charges" },
		{ "19", "Trust commission" },
		{ "20", "Transfer commission" },
		{ "21", "Commission for opening irrevocable doc. credits" },
		{ "22", "Pre-advice commission" },
		{ "23", "Supervisory commission" },
		{ "24", "Model charges" },
		{ "25", "Risk commission" },
		{ "26", "Guarantee commission" },
		{ "27", "Reimbursement commission" },
		{ "28", "Stamp duty" },
		{ "29", "Brokerage" },
		{ "30", "Bank charges" },
		{ "31", "Bank charges information" },
		{ "32", "Courier fee" },
		{ "33", "Phone fee" },
		{ "34", "Postage fee" },
		{ "35", "S.W.I.F.T. fee" },
		{ "36", "Telex fee" },
		{ "37", "Penalty for late delivery of documents" },
		{ "38", "Penalty for late delivery of valuation of works" },
		{ "39", "Penalty for execution of works behind schedule" },
		{ "40", "Other penalties" },
		{ "41", "Bonus for works ahead of schedule" },
		{ "42", "Other bonus" },
		{ "43", "Penalty refund" },
		{ "44", "Project management cost" },
		{ "45", "Pro rata retention" },
		{ "46", "Contractual retention" },
		{ "47", "Other retentions" },
		{ "48", "Interest on arrears" },
		{ "49", "Interest" },
		{ "50", "Charge per credit cover" },
		{ "51", "Charge per unused credit cover" },
		{ "52", "Minimum commission" },
		{ "53", "Factoring commission" },
		{ "54", "Chamber of commerce charge" },
	};
}