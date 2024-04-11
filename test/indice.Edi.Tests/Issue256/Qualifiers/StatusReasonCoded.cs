using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Status reason, coded
/// </summary>
public class StatusReasonCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator StatusReasonCoded(string s) => new StatusReasonCoded { Code = s };

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
		{ "1", "Address ex delivery area" },
		{ "2", "After transport departed" },
		{ "3", "Agent refusal" },
		{ "4", "Altered seals" },
		{ "5", "Appointment scheduled" },
		{ "6", "Attempt unsuccessful" },
		{ "7", "Business closed" },
		{ "8", "Changed schedule" },
		{ "9", "Complementary address needed" },
		{ "10", "Computer system down" },
		{ "11", "Credit approval requested" },
		{ "12", "Customer arrangements" },
		{ "13", "Customs refusal" },
		{ "14", "Damaged" },
		{ "15", "Delivery at specific requested dates/times/periods" },
		{ "16", "Destination incorrect" },
		{ "17", "Departure delay" },
		{ "18", "Derailment" },
		{ "19", "Discrepancy" },
		{ "20", "Dock strike" },
		{ "21", "Due to customer" },
		{ "22", "Empty" },
		{ "23", "Equipment failure" },
		{ "24", "Examination required by relevant authority" },
		{ "25", "Export restrictions" },
		{ "26", "Frustrated export" },
		{ "27", "Goods units missing" },
		{ "28", "Import restrictions" },
		{ "29", "Incorrect pick information" },
		{ "30", "Incorrect address" },
		{ "31", "Industrial dispute" },
		{ "32", "Instructions awaited" },
		{ "33", "Lost goods/consignments/equipment" },
		{ "34", "Means of transport damaged" },
		{ "35", "Mechanical breakdown" },
		{ "36", "Mechanical inspection" },
		{ "37", "Missing and/or incorrect documents" },
		{ "38", "New delivery arrangements" },
		{ "39", "No recipient contact information" },
		{ "40", "Not identified" },
		{ "41", "Not loaded" },
		{ "42", "On deck" },
		{ "43", "Package not ready" },
		{ "44", "Package tracking number unknown" },
		{ "45", "Partly missing" },
		{ "46", "Payment not received" },
		{ "47", "Payment refused" },
		{ "48", "Plundered" },
		{ "49", "Refused without reason given" },
		{ "50", "Scheduled past cut-off" },
		{ "51", "Shunted to siding" },
		{ "52", "Signature not required" },
		{ "53", "Sorted wrong route" },
		{ "54", "Special service required" },
		{ "55", "Split" },
		{ "56", "Totally missing" },
		{ "57", "Tracking information unavailable" },
		{ "58", "Transit delay" },
		{ "59", "Unable to locate" },
		{ "60", "Unacceptable condition" },
		{ "61", "Under deck" },
		{ "62", "Unknown" },
		{ "63", "Weather conditions" },
		{ "64", "Expired free time" },
		{ "65", "Outstanding claims settled" },
	};
}