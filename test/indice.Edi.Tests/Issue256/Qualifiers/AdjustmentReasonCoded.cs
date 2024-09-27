using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Adjustment reason, coded
/// </summary>
public class AdjustmentReasonCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator AdjustmentReasonCoded(string s) => new AdjustmentReasonCoded { Code = s };

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
		{ "1", "Agreed settlement" },
		{ "2", "Below specification goods" },
		{ "3", "Damaged goods" },
		{ "4", "Short delivery" },
		{ "5", "Price query" },
		{ "6", "Proof of delivery required" },
		{ "7", "Payment on account" },
		{ "8", "Returnable container charge included" },
		{ "9", "Invoice error" },
		{ "10", "Costs for draft" },
		{ "11", "Bank charges" },
		{ "12", "Agent commission" },
		{ "13", "Counter claim" },
		{ "14", "Wrong delivery" },
		{ "15", "Goods returned to agent" },
		{ "16", "Goods partly returned" },
		{ "17", "Transport damage" },
		{ "18", "Goods on consignment" },
		{ "19", "Trade discount" },
		{ "20", "Discount for late delivery" },
		{ "21", "Advertising costs" },
		{ "22", "Customs duties" },
		{ "23", "Telephone and postal costs" },
		{ "24", "Repair costs" },
		{ "25", "Attorney fees" },
		{ "26", "Taxes" },
		{ "27", "Reclaimed deduction" },
		{ "28", "See separate advice" },
		{ "29", "Buyer refused to take delivery" },
		{ "30", "Direct payment to seller" },
		{ "31", "Buyer disagrees with due date" },
		{ "32", "Goods not delivered" },
		{ "33", "Late delivery" },
		{ "34", "Quoted as paid to you" },
		{ "35", "Goods returned" },
		{ "36", "Invoice not received" },
		{ "37", "Credit note to debtor/not to us" },
		{ "38", "Deducted bonus" },
		{ "39", "Deducted discount" },
		{ "40", "Deducted freight costs" },
		{ "41", "Deduction against other invoices" },
		{ "42", "Credit balance(s)" },
		{ "43", "Reason unknown" },
		{ "44", "Awaiting message from seller" },
		{ "45", "Debit note to seller" },
		{ "46", "Discount beyond terms" },
		{ "47", "See buyer's letter" },
		{ "48", "Allowance/charge error" },
		{ "49", "Substitute product" },
		{ "50", "Terms of sale error" },
		{ "51", "Required data missing" },
		{ "52", "Wrong invoice" },
		{ "53", "Duplicate invoice" },
		{ "54", "Weight error" },
		{ "55", "Additional charge not authorized" },
		{ "56", "Incorrect discount" },
		{ "57", "Price change" },
		{ "58", "Variation" },
		{ "59", "Chargeback" },
		{ "60", "Offset" },
		{ "61", "Indirect payment" },
		{ "62", "Financial reassignment" },
		{ "63", "Reinstatement of chargeback/offset" },
		{ "64", "Expecting new terms" },
		{ "65", "Settlement to agent" },
		{ "ZZZ", "Mutually defined" },
	};
}