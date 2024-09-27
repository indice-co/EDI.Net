using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Settlement, coded
/// </summary>
public class SettlementCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SettlementCoded(string s) => new SettlementCoded { Code = s };

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
		{ "1", "Bill back" },
		{ "2", "Off invoice" },
		{ "3", "Vendor check to customer" },
		{ "4", "Credit customer account" },
		{ "5", "Charge to be paid by vendor" },
		{ "6", "Charge to be paid by customer" },
		{ "7", "Optional" },
		{ "8", "Off gross quantity invoiced" },
		{ "9", "Electric cost recovery factor" },
		{ "10", "Gas cost recovery factor" },
		{ "11", "Prior credit balance" },
		{ "12", "Non-dutiable" },
		{ "13", "All charges borne by payee" },
		{ "14", "Each pay own cost" },
		{ "15", "All charges borne by payor" },
		{ "16", "All bank charges to be borne by applicant" },
		{ "17", "All bank charges except confirmation commission to be borne by applicant" },
		{ "18", "All bank charges to be borne by beneficiary" },
		{ "19", "All bank charges outside country of applicant borne by beneficiary" },
		{ "20", "Amendment charges to be borne by applicant" },
		{ "21", "Amendment charges to be borne by beneficiary" },
		{ "22", "Discount charges to be borne by applicant" },
		{ "23", "Discount charges to be borne by beneficiary" },
		{ "24", "All bank charges other than those of the issuing bank to be borne by beneficiary" },
		{ "ZZZ", "Mutually defined" },
	};
}