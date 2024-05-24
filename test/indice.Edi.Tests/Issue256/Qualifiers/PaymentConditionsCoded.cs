using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Payment conditions, coded
/// </summary>
public class PaymentConditionsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PaymentConditionsCoded(string s) => new PaymentConditionsCoded { Code = s };

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
		{ "1", "Direct payment" },
		{ "2", "Automatic clearing house credit" },
		{ "3", "Automatic clearing house debit" },
		{ "4", "Automatic clearing house credit-savings account" },
		{ "5", "Automatic clearing house debit-demand account" },
		{ "6", "Bank book transfer (credit)" },
		{ "7", "Bank book transfer (debit)" },
		{ "8", "Doc collection via 3rd party with bill of EX" },
		{ "9", "Doc collection via 3rd party no bill of EX" },
		{ "10", "Irrevocable documentary credit" },
		{ "11", "Transferable irrevocable documentary credit" },
		{ "12", "Confirmed irrevocable documentary credit" },
		{ "13", "Transferable confirmed irrevocable documentary credit" },
		{ "14", "Revocable documentary credit" },
		{ "15", "Irrevocable letter of credit-confirmed" },
		{ "16", "Letter of guarantee" },
		{ "17", "Revocable letter of credit" },
		{ "18", "Standby letter of credit" },
		{ "19", "Irrevocable letter of credit unconfirmed" },
		{ "20", "Clean collection (ICC)" },
		{ "21", "Documentary collection (ICC)" },
		{ "22", "Documentary sight collection (ICC)" },
		{ "23", "Documentary collection with date of expiry (ICC)" },
		{ "24", "Documentary collection: bill of exchange against acceptance" },
		{ "25", "Documentary collection: bill of exchange against payment" },
		{ "26", "Collection subject to buyer's approval (ICC)" },
		{ "27", "Collection by a bank consignee for the goods (ICC)" },
		{ "28", "Collection under CMEA rules with immediate payment and subsequent AC" },
		{ "29", "Collection under CMEA rules with prior acceptance" },
		{ "30", "Other collection" },
		{ "31", "Open account against payment in advance" },
		{ "32", "Open account for contra" },
		{ "33", "Open account for payment" },
		{ "34", "Seller to advise buyer" },
		{ "35", "Documents through banks" },
		{ "36", "Charging (to account)" },
		{ "37", "Available with issuing bank" },
		{ "38", "Available with advising bank" },
		{ "39", "Available with named bank" },
		{ "40", "Available with any bank" },
		{ "41", "Available with any bank in ..." },
		{ "42", "Indirect payment" },
		{ "43", "Reassignment" },
		{ "44", "Offset" },
		{ "45", "Special entries" },
		{ "46", "Instalment payment" },
		{ "47", "Instalment payment with draft" },
		{ "61", "Set-off by exchange of documents" },
		{ "62", "Set-off by reciprocal credits" },
		{ "63", "Set-off by linkage (against reciprocal benefits)" },
		{ "64", "Set-off by exchange of goods" },
		{ "69", "Other set-off" },
		{ "70", "Supplier to invoice" },
		{ "71", "Recipient to self bill" },
		{ "ZZZ", "Mutually defined" },
	};
}