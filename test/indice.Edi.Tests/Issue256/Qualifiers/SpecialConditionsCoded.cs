using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Special conditions, coded
/// </summary>
public class SpecialConditionsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SpecialConditionsCoded(string s) => new SpecialConditionsCoded { Code = s };

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
		{ "1", "Item for national preference" },
		{ "2", "Item qualifying for payment discount" },
		{ "3", "Item not qualifying for payment discount" },
		{ "5", "Item not to be included in bonus calculation" },
		{ "6", "Subject to bonus" },
		{ "7", "Subject to commission" },
		{ "8", "Subject to discount" },
		{ "9", "Freely available in EEC" },
		{ "10", "Subject to settlement discount" },
		{ "11", "Price includes excise" },
		{ "12", "Price includes tax" },
		{ "13", "Price include duty" },
		{ "14", "Not subject to commission" },
		{ "15", "Not subject to discount" },
		{ "16", "Subject to embargo restriction" },
		{ "17", "Item not subject to embargo restrictions" },
		{ "18", "Item subject to national export restrictions" },
		{ "19", "Item not subject to national export restrictions" },
		{ "20", "Item subject to import restrictions" },
		{ "21", "Item not subject to import restrictions" },
		{ "22", "Signed" },
		{ "23", "Authenticated" },
		{ "24", "Original(s) to be certified" },
		{ "25", "Original(s) to be legalized" },
		{ "26", "Quote documentary credit number" },
		{ "27", "Full set bill of lading" },
		{ "28", "Full set bill of lading less one original" },
		{ "29", "Full set bill of lading less two originals" },
		{ "30", "Shipped on board" },
		{ "31", "Freight prepaid to be marked" },
		{ "32", "Freight collect to be marked" },
		{ "33", "Issued to order and blank endorsed" },
		{ "34", "Issued and endorsed to the order of" },
		{ "35", "Consigned to" },
		{ "36", "Notify" },
		{ "37", "Issued by" },
		{ "38", "Charter party allowed" },
		{ "39", "Loading on deck allowed" },
		{ "40", "Quote actual flight date and flight number" },
		{ "41", "House AWB allowed" },
		{ "42", "Express post receipt" },
		{ "43", "Air parcel post receipt" },
		{ "44", "Parcel post receipt" },
		{ "45", "Issued to bearer" },
		{ "46", "Full set of insurance certificate" },
		{ "47", "Full set of insurance policy" },
		{ "48", "Addressed to" },
		{ "49", "Transmission by telecommunication" },
		{ "50", "Bill of exchange drawn on" },
		{ "51", "Bill of exchange in duplicate" },
		{ "52", "Insurance certificate alternative" },
		{ "53", "Insurance policy alternative" },
		{ "54", "Original(s) and copies to be certified" },
		{ "55", "Original(s) and copies to be legalized" },
		{ "56", "Consolidators AWB allowed" },
		{ "57", "Full set" },
		{ "58", "Full set less one original" },
		{ "59", "Full set less two originals" },
		{ "60", "Goods despatched to" },
		{ "61", "Insurance certificate allowed" },
		{ "62", "Issued to" },
		{ "63", "Original(s) and copy(ies) signed" },
		{ "64", "Original(s) signed" },
		{ "65", "No disposal clause" },
		{ "66", "Delivery without change of ownership" },
		{ "67", "Delivery with change of ownership" },
		{ "68", "Supply for outright purchase/sale" },
		{ "69", "Supply for consignment" },
		{ "70", "Supply for sale 'on approval' or after trial" },
		{ "71", "Exchange of goods compensated in kind" },
		{ "72", "Sale for export by foreigner travelling in member state concerned" },
		{ "73", "Samples" },
		{ "74", "Temporary export, loan or hire" },
		{ "75", "Temporary export, leasing" },
		{ "76", "Temporary export, operation for job processing" },
		{ "77", "Temporary export, repair and maintenance against payment" },
		{ "78", "Temporary export, repair and maintenance free of charge" },
		{ "79", "Reimport following job processing" },
		{ "80", "Reimport following repair and maintenance against payment" },
		{ "81", "Reimport following repair and maintenance free of charge" },
		{ "82", "Supply of goods under joint production contract for defence purposes" },
		{ "83", "Supply of goods under joint production contract for civil purposes" },
		{ "84", "Supply of goods for warehousing for foreign account" },
		{ "85", "Supply of goods as gifts by country of despatch and food aid under European Economic Community regulation" },
		{ "86", "Supply of goods for disaster relief equipment" },
		{ "87", "Supply of goods as transactions without compensation" },
		{ "88", "Supply of goods as returned consignment on which payment has been made" },
		{ "89", "Supply of goods as returned consignment on which no payment has been made" },
		{ "90", "Supply of goods in standard exchange giving rise to payment" },
		{ "91", "Supply of goods in standard exchange not giving rise to payment" },
		{ "92", "Supply of goods/services in standard exchange under warranty" },
		{ "93", "Goods" },
		{ "94", "Service" },
		{ "95", "Financial regulation" },
		{ "96", "Promotional advertising" },
		{ "97", "Promotional price" },
		{ "98", "Promotional shelf display" },
		{ "99", "Safety data sheet required to accompany goods when moved" },
		{ "100", "Multiple delivery points" },
		{ "101", "Provisional settlement" },
		{ "102", "Hire purchase" },
		{ "103", "Loan" },
		{ "104", "Rental" },
		{ "105", "Processing" },
		{ "106", "Exchange" },
		{ "107", "Sale on commission" },
		{ "108", "Financial compensation" },
		{ "109", "Sale or return" },
		{ "110", "Final settlement" },
		{ "111", "Requires national pricing authority agreement" },
		{ "112", "National pricing authority approved price" },
		{ "113", "Not subject to national pricing authority approval" },
		{ "114", "Agency approved price" },
		{ "115", "Exempt from tax" },
		{ "116", "Subject to postponed discount" },
		{ "117", "Repair" },
		{ "118", "Illustration affected by provisioning change" },
	};
}