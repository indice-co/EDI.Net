using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Processing indicator, coded
/// </summary>
public class ProcessingIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ProcessingIndicatorCoded(string s) => new ProcessingIndicatorCoded { Code = s };

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
		{ "1", "Message content accepted" },
		{ "2", "Message content rejected with comment" },
		{ "3", "Message content rejected without comment" },
		{ "4", "Goods released" },
		{ "5", "Goods required for examination" },
		{ "6", "All documents or as specified to be produced" },
		{ "7", "Goods detained" },
		{ "8", "Goods may move under Customs transfer" },
		{ "9", "Declaration accepted awaiting goods arrival" },
		{ "10", "Declaration requested" },
		{ "11", "Pre-entry information" },
		{ "12", "Sender not allowed the message type" },
		{ "13", "Message type not supported" },
		{ "14", "Error message" },
		{ "15", "Response after correction, correction approved" },
		{ "16", "Response after correction, correction non approved" },
		{ "17", "Message received" },
		{ "18", "Request for clearance" },
		{ "19", "Bulk goods" },
		{ "20", "Cash payment deferred" },
		{ "21", "Unsolicited text" },
		{ "22", "Export" },
		{ "23", "Import" },
		{ "24", "Transit" },
		{ "25", "Prohibited/restricted goods" },
		{ "26", "Container quarantine" },
		{ "27", "Onward carriage: immediate export" },
		{ "28", "Transhipment" },
		{ "29", "Onward carriage: inland clearance" },
		{ "30", "Transaction will not be processed until action is taken by sender" },
		{ "31", "Transaction cannot be processed by the recipient" },
		{ "32", "Transaction accepted" },
		{ "33", "Transaction rejected" },
		{ "34", "Transaction awaiting processing" },
		{ "35", "Transaction unknown" },
		{ "36", "Changed information" },
		{ "37", "Complete information" },
		{ "38", "Manual procedures" },
		{ "39", "Unrestow" },
		{ "40", "Seal intact" },
		{ "41", "Seal not intact" },
		{ "42", "Container equipment not damaged" },
		{ "43", "Container equipment damaged" },
		{ "44", "Status of message" },
		{ "45", "Beneficiary's account number unknown" },
		{ "46", "Payee's account number unknown" },
		{ "47", "Payor' account number unknown" },
		{ "48", "Correspondent bank not possible" },
		{ "49", "Execution date not possible" },
		{ "50", "Value date not possible" },
		{ "51", "Currency code not possible" },
		{ "52", "Invalid decimal number" },
		{ "53", "Orders executed (on)" },
		{ "54", "Transaction(s) effected and advised (on)" },
		{ "55", "Not yet debited" },
		{ "56", "Beneficiary is unable to identify the transaction" },
		{ "57", "Restowage of cargo" },
		{ "58", "Onward carriage: sea clearance" },
		{ "59", "Perishable goods" },
		{ "60", "Fumigation" },
		{ "61", "Transaction reason non reportable" },
		{ "62", "Reporting via a bank with forwarding abroad authorized" },
		{ "63", "Reporting via a bank with forwarding abroad denied" },
		{ "64", "Balance Of Payments complementary information is requested" },
		{ "65", "Direct reporting" },
		{ "66", "Message content accepted, with comments" },
		{ "67", "Message content partly accepted, with comments" },
		{ "ZZZ", "Mutually defined" },
	};
}