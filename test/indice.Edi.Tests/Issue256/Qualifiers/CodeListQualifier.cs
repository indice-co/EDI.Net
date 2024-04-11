using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Code list qualifier
/// </summary>
public class CodeListQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CodeListQualifier(string s) => new CodeListQualifier { Code = s };

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
		{ "12", "Telephone directory" },
		{ "16", "Postcode directory" },
		{ "23", "Clearing house automated payment" },
		{ "25", "Bank identification" },
		{ "35", "Rail additional charges" },
		{ "36", "Railways networks" },
		{ "37", "Railway locations" },
		{ "38", "Rail customers" },
		{ "39", "Rail unified nomenclature of goods" },
		{ "42", "Business function" },
		{ "43", "Clearing House Interbank Payment System Participants ID" },
		{ "44", "Clearing House Interbank Payment System Universal ID" },
		{ "45", "United Nations Common Coding System (UNCCS)" },
		{ "46", "DUNS (Dun and Bradstreet) +4" },
		{ "52", "Value added tax identification" },
		{ "53", "Passport number" },
		{ "54", "Statistical object" },
		{ "55", "Quality conformance" },
		{ "56", "Safety regulation" },
		{ "57", "Product code" },
		{ "58", "Business account number" },
		{ "59", "Railway services harmonized code" },
		{ "60", "Type of financial account" },
		{ "61", "Type of assets and liabilities" },
		{ "62", "Requirements indicator" },
		{ "63", "Handling action" },
		{ "64", "Freight forwarder" },
		{ "65", "Shipping agent" },
		{ "67", "Type of package" },
		{ "68", "Type of industrial activity" },
		{ "69", "Type of survey question" },
		{ "70", "Customs inspection type" },
		{ "71", "Nature of transaction" },
		{ "72", "Container terminal" },
		{ "100", "Enhanced party identification" },
		{ "101", "Air carrier" },
		{ "102", "Size and type" },
		{ "103", "Call sign directory" },
		{ "104", "Customs area of transaction" },
		{ "105", "Customs declaration type" },
		{ "106", "Incoterms 1980" },
		{ "107", "Excise duty" },
		{ "108", "Tariff schedule" },
		{ "109", "Customs indicator" },
		{ "110", "Customs special codes" },
		{ "112", "Statistical nature of transaction" },
		{ "113", "Customs office" },
		{ "114", "Railcar letter marking" },
		{ "115", "Examination facility" },
		{ "116", "Customs preference" },
		{ "117", "Customs procedure" },
		{ "118", "Government agency procedure" },
		{ "119", "Customs simplified procedure" },
		{ "120", "Customs status of goods" },
		{ "121", "Shipment description" },
		{ "122", "Commodity" },
		{ "123", "Entitlement" },
		{ "125", "Customs transit guarantee" },
		{ "126", "Accounting information identifier" },
		{ "127", "Customs valuation method" },
		{ "128", "Service" },
		{ "129", "Customs warehouse" },
		{ "130", "Special handling" },
		{ "131", "Free zone" },
		{ "132", "Charge" },
		{ "133", "Financial regime" },
		{ "134", "Duty, tax or fee payment method" },
		{ "135", "Rate class" },
		{ "136", "Restrictions/prohibitions on re-use of certain wagons" },
		{ "137", "Rail harmonized codification of tariffs" },
		{ "139", "Port" },
		{ "140", "Area" },
		{ "141", "Forwarding restrictions" },
		{ "142", "Train identification" },
		{ "143", "Removable accessories and special equipment on railcars" },
		{ "144", "Rail routes" },
		{ "145", "Airport/city" },
		{ "146", "Means of transport identification" },
		{ "147", "Document requested by Customs" },
		{ "148", "Customs release notification" },
		{ "149", "Customs transit type" },
		{ "150", "Financial routing" },
		{ "151", "Locations for tariff calculation" },
		{ "152", "Materials" },
		{ "153", "Methods of payment" },
		{ "154", "Bank branch sorting identification" },
		{ "155", "Automated clearing house" },
		{ "156", "Location of goods" },
		{ "157", "Clearing code" },
		{ "158", "Terms of delivery" },
		{ "160", "Party identification" },
		{ "161", "Goods description" },
		{ "162", "Country" },
		{ "163", "Country sub-entity" },
		{ "164", "Member organizations" },
		{ "165", "Amendment code (Customs)" },
		{ "166", "Social security identification" },
		{ "167", "Tax party identification" },
		{ "168", "Rail document names" },
		{ "169", "Harmonized system" },
		{ "170", "Bank securities code" },
		{ "172", "Carrier code" },
		{ "173", "Export requirements" },
		{ "174", "Citizen identification" },
		{ "175", "Account analysis codes" },
		{ "176", "Flow of the goods" },
		{ "177", "Statistical procedures" },
		{ "178", "Standard text according US embargo regulations" },
		{ "179", "Standard text for export according national prescriptions" },
		{ "180", "Airport terminal" },
		{ "181", "Activity" },
		{ "182", "Combiterms 1990" },
		{ "183", "Dangerous goods packing type" },
		{ "184", "Tax assessment method" },
		{ "185", "Item type" },
		{ "186", "Product supply condition" },
		{ "187", "Supplier's stock turnover" },
		{ "188", "Article status" },
		{ "189", "Quality control code" },
		{ "190", "Item sourcing category" },
		{ "191", "Dumping or countervailing assessment method" },
		{ "192", "Dumping specification" },
		{ "ZZZ", "Mutually defined" },
	};
}