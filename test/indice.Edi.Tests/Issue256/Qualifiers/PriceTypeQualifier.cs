using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Price type qualifier
/// </summary>
public class PriceTypeQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PriceTypeQualifier(string s) => new PriceTypeQualifier { Code = s };

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
		{ "AI", "Active ingredient" },
		{ "AP", "Advice price" },
		{ "BR", "Broker price" },
		{ "CP", "Current price" },
		{ "CU", "Consumer unit" },
		{ "DR", "Dealer price" },
		{ "EC", "ECSC price" },
		{ "ES", "Estimated price" },
		{ "NE", "Not-to-exceed price" },
		{ "NW", "Net weight" },
		{ "PW", "Gross weight" },
		{ "SW", "Gross weight without wooden pallets" },
		{ "TB", "To be negotiated" },
		{ "TU", "Traded unit" },
		{ "TW", "Theoretical weight" },
		{ "WH", "Wholesale price" },
		{ "AAA", "Reference price" },
		{ "AAB", "Price includes tax" },
		{ "AAC", "Buyer suggested retail price" },
		{ "AAD", "Ocean charges rate" },
		{ "AAE", "Not subject to fluctuation" },
		{ "AAF", "Subject to escalation" },
		{ "AAG", "Subject to price adjustment" },
		{ "AAH", "Subject to escalation and price adjustment" },
		{ "AAI", "Fluctuation conditions not specified" },
		{ "AAJ", "All in price" },
		{ "AAK", "New price" },
		{ "AAL", "Old price" },
		{ "AAM", "Per week" },
		{ "AAN", "Price on application" },
		{ "AAO", "Unpacked price" },
		{ "AAP", "Trade price" },
		{ "AAQ", "Firm price" },
		{ "AAR", "Material share of item price" },
		{ "AAS", "Labour share of item price" },
		{ "AAT", "Transport share of item price" },
		{ "AAU", "Packing share of item price" },
		{ "AAV", "Tooling share of item price" },
		{ "AAW", "Temporary vehicle charge" },
		{ "AAX", "Price component due to interest" },
		{ "AAY", "Price component due to management services" },
		{ "AAZ", "Price component due to maintenance" },
		{ "ABA", "Individual buyer price" },
		{ "ABB", "Group buying price" },
		{ "ABC", "Group member buying price" },
		{ "ABD", "Pre-payment price" },
		{ "ABE", "Retail price - excluding taxes" },
		{ "ABF", "Suggested retail price - excluding taxes" },
		{ "ABG", "Agreed minimum price" },
		{ "ABH", "Statutory minimum retail price" },
		{ "ABI", "Cost reimbursement price" },
		{ "ABJ", "Market price" },
		{ "ABK", "Open tender price" },
		{ "ALT", "Alternate price" },
		{ "CAT", "Catalogue price" },
		{ "CDV", "Current domestic value" },
		{ "CON", "Contract price" },
		{ "CUP", "Confirmed unit price" },
		{ "CUS", "Declared customs unit value" },
		{ "DAP", "Dealer adjusted price" },
		{ "DIS", "Distributor price" },
		{ "DPR", "Discount price" },
		{ "DSC", "Discount amount allowed" },
		{ "EUP", "Expected unit price" },
		{ "FCR", "Freight/charge rate" },
		{ "GRP", "Gross unit price" },
		{ "INV", "Invoice price" },
		{ "LBL", "Labelling price" },
		{ "MAX", "Maximum order quantity price" },
		{ "MIN", "Minimum order quantity price" },
		{ "MNR", "Minimum release quantity price" },
		{ "MSR", "Manufacturer's suggested retail" },
		{ "MXR", "Maximum release quantity price" },
		{ "NQT", "No quote" },
		{ "NTP", "Net unit price" },
		{ "OCR", "Ocean charges rate" },
		{ "OFR", "Ocean freight rate" },
		{ "PAQ", "Price break quantity(s)" },
		{ "PBQ", "Unit price beginning quantity" },
		{ "PPD", "Prepaid freight charges" },
		{ "PPR", "Provisional price" },
		{ "PRO", "Producer's price" },
		{ "PRP", "Promotional price" },
		{ "QTE", "Quote price" },
		{ "RES", "Resale price" },
		{ "RTP", "Retail price" },
		{ "SHD", "Ship and debit" },
		{ "SRP", "Suggested retail price" },
		{ "TRF", "Transfer" },
	};
}