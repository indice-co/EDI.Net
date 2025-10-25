using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Action request/notification, coded
/// </summary>
public class ActionRequestNotificationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ActionRequestNotificationCoded(string s) => new ActionRequestNotificationCoded { Code = s };

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
		{ "1", "Added" },
		{ "2", "Deleted" },
		{ "3", "Changed" },
		{ "4", "No action" },
		{ "5", "Accepted without amendment" },
		{ "6", "Accepted with amendment" },
		{ "7", "Not accepted" },
		{ "8", "Schedule only" },
		{ "9", "Amendments" },
		{ "10", "Not found" },
		{ "11", "Not amended" },
		{ "12", "Line item numbers changed" },
		{ "13", "Buyer has deducted amount" },
		{ "14", "Buyer claims against invoice" },
		{ "15", "Charge back by seller" },
		{ "16", "Seller will issue credit note" },
		{ "17", "Terms changed for new terms" },
		{ "18", "Abide outcome of negotiations" },
		{ "19", "Seller rejects dispute" },
		{ "20", "Settlement" },
		{ "21", "No delivery" },
		{ "22", "Call-off delivery" },
		{ "23", "Proposed amendment" },
		{ "24", "Accepted with amendment, no confirmation required" },
		{ "25", "Equipment provisionally repaired" },
	};
}