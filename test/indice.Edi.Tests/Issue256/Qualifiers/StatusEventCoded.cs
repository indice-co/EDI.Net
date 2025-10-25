using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Status event, coded
/// </summary>
public class StatusEventCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator StatusEventCoded(string s) => new StatusEventCoded { Code = s };

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
		{ "1", "Arrived" },
		{ "2", "Authorized to load" },
		{ "3", "Arrived in defective condition" },
		{ "4", "Defective equipment release" },
		{ "5", "Begun" },
		{ "6", "Booked" },
		{ "7", "Booking cancelled" },
		{ "8", "Cleared import restrictions" },
		{ "9", "Cleared export restrictions" },
		{ "10", "Cleared by agriculture, food or fisheries authorities" },
		{ "11", "Cleared by port authority" },
		{ "12", "Cleared by customs" },
		{ "13", "Collected" },
		{ "14", "Completed" },
		{ "15", "Consolidated" },
		{ "16", "Crossed border" },
		{ "17", "Customs refusal" },
		{ "18", "Damaged" },
		{ "19", "Damaged equipment quoted" },
		{ "20", "Delayed" },
		{ "21", "Delivered" },
		{ "22", "Delivery completed" },
		{ "23", "Delivery not completed" },
		{ "24", "Departed" },
		{ "25", "Departure delay" },
		{ "26", "Deramped" },
		{ "27", "Despatched" },
		{ "28", "Stripped" },
		{ "29", "Discharged" },
		{ "30", "Empty on inspection" },
		{ "31", "En route" },
		{ "32", "Equipment in from repair" },
		{ "33", "Equipment out for repair" },
		{ "34", "Equipment repaired" },
		{ "35", "Expedited to destination" },
		{ "36", "Not found" },
		{ "37", "Found" },
		{ "38", "Freight paid" },
		{ "39", "From bond" },
		{ "40", "Goods/consignments/equipment at port" },
		{ "41", "Handover" },
		{ "42", "Handover delivered" },
		{ "43", "Handover received" },
		{ "44", "Ill-routed consignment reforwarded" },
		{ "45", "Informed Consignee of arrival" },
		{ "46", "Into bond" },
		{ "47", "Into packing depot" },
		{ "48", "Loaded" },
		{ "49", "Lost" },
		{ "50", "Manifested" },
		{ "51", "Means of transport damaged" },
		{ "52", "Mechanical breakdown" },
		{ "53", "No pick-up" },
		{ "54", "Not identified" },
		{ "55", "Not collected" },
		{ "56", "Not delivered" },
		{ "57", "Not loaded" },
		{ "58", "Off hire" },
		{ "59", "Off loaded" },
		{ "60", "On hire" },
		{ "61", "Outstanding claims settled" },
		{ "62", "Over landed" },
		{ "63", "Package not ready" },
		{ "64", "Pick-up awaited" },
		{ "65", "Plugged equipment" },
		{ "66", "Plundered" },
		{ "67", "Positioned goods/consignments/equipment" },
		{ "68", "Pre-informed" },
		{ "69", "Put to refuse" },
		{ "70", "Ramped equipment" },
		{ "71", "Ready for transportation" },
		{ "72", "Receipt fully acknowledged" },
		{ "73", "Receipt partially acknowledged" },
		{ "74", "Received" },
		{ "75", "Reconsigned" },
		{ "76", "Reforwarding on request" },
		{ "77", "Refused without reason given" },
		{ "78", "Released" },
		{ "79", "Reloaded" },
		{ "80", "Returned as instructed" },
		{ "81", "Returned as wreck" },
		{ "82", "Returned" },
		{ "83", "Sealed equipment" },
		{ "84", "Service ordered" },
		{ "85", "Short landed" },
		{ "86", "Short shipped" },
		{ "87", "Sorted wrong route" },
		{ "88", "Split" },
		{ "89", "Steam cleaned" },
		{ "90", "Stopped" },
		{ "91", "Stored" },
		{ "92", "Stowed" },
		{ "93", "Stuffed" },
		{ "94", "Stuffed and sealed" },
		{ "95", "Sub-lease in" },
		{ "96", "Sub-lease out" },
		{ "97", "Surveyed damage" },
		{ "98", "Transferred in" },
		{ "99", "Transferred out" },
		{ "100", "Transhipment" },
		{ "101", "Transit delay" },
		{ "102", "Unknown goods/consignments/equipment" },
		{ "103", "Unplugged equipment" },
	};
}