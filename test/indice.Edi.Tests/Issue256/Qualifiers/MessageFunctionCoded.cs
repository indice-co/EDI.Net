using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Message function, coded
/// </summary>
public class MessageFunctionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MessageFunctionCoded(string s) => new MessageFunctionCoded { Code = s };

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
		{ "1", "Cancellation" },
		{ "2", "Addition" },
		{ "3", "Deletion" },
		{ "4", "Change" },
		{ "5", "Replace" },
		{ "6", "Confirmation" },
		{ "7", "Duplicate" },
		{ "8", "Status" },
		{ "9", "Original" },
		{ "10", "Not found" },
		{ "11", "Response" },
		{ "12", "Not processed" },
		{ "13", "Request" },
		{ "14", "Advance notification" },
		{ "15", "Reminder" },
		{ "16", "Proposal" },
		{ "17", "Cancel, to be reissued" },
		{ "18", "Reissue" },
		{ "19", "Seller initiated change" },
		{ "20", "Replace heading section only" },
		{ "21", "Replace item detail and summary only" },
		{ "22", "Final transmission" },
		{ "23", "Transaction on hold" },
		{ "24", "Delivery instruction" },
		{ "25", "Forecast" },
		{ "26", "Delivery instruction and forecast" },
		{ "27", "Not accepted" },
		{ "28", "Accepted, with amendment in heading section" },
		{ "29", "Accepted without amendment" },
		{ "30", "Accepted, with amendment in detail section" },
		{ "31", "Copy" },
		{ "32", "Approval" },
		{ "33", "Change in heading section" },
		{ "34", "Accepted with amendment" },
		{ "35", "Retransmission" },
		{ "36", "Change in detail section" },
		{ "37", "Reversal of a debit" },
		{ "38", "Reversal of a credit" },
		{ "39", "Reversal for cancellation" },
		{ "40", "Request for deletion" },
		{ "41", "Finishing/closing order" },
		{ "42", "Confirmation via specific means" },
		{ "43", "Additional transmission" },
		{ "44", "Accepted without reserves" },
		{ "45", "Accepted with reserves" },
		{ "46", "Provisional" },
		{ "47", "Definitive" },
		{ "48", "Accepted, contents rejected" },
		{ "49", "Settled dispute" },
		{ "50", "Withdraw" },
		{ "51", "Authorisation" },
		{ "52", "Proposed amendment" },
		{ "53", "Test" },
		{ "54", "Extract" },
		{ "55", "Notification only" },
	};
}