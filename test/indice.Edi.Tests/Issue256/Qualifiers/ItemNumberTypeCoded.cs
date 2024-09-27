using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Item number type, coded
/// </summary>
public class ItemNumberTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ItemNumberTypeCoded(string s) => new ItemNumberTypeCoded { Code = s };

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
		{ "AA", "Product version number" },
		{ "AB", "Assembly" },
		{ "AC", "HIBC (Health Industry Bar Code)" },
		{ "AD", "Cold roll number" },
		{ "AE", "Hot roll number" },
		{ "AF", "Slab number" },
		{ "AG", "Software revision number" },
		{ "AH", "UPC (Universal Product Code) Consumer package code (1-5-5)" },
		{ "AI", "UPC (Universal Product Code) Consumer package code (1-5-5-1)" },
		{ "AJ", "Sample number" },
		{ "AK", "Pack number" },
		{ "AL", "UPC (Universal Product Code) Shipping container code (1-2-5- 5)" },
		{ "AM", "UPC (Universal Product Code)/EAN (European article number) Shipping container code (1-2-5-5-1)" },
		{ "AN", "UPC (Universal Product Code) suffix" },
		{ "AO", "State label code" },
		{ "AP", "Heat number" },
		{ "AT", "Price look up number" },
		{ "AU", "NSN (North Atlantic Treaty Organization Stock Number)" },
		{ "AV", "Refined product code" },
		{ "AW", "Exhibit" },
		{ "AX", "End item" },
		{ "AY", "Federal supply classification" },
		{ "AZ", "Engineering data list" },
		{ "BB", "Lot number" },
		{ "BC", "National drug code 4-4-2 format" },
		{ "BD", "National drug code 5-3-2 format" },
		{ "BE", "National drug code 5-4-1 format" },
		{ "BF", "National drug code 5-4-2 format" },
		{ "BG", "National drug code" },
		{ "BH", "Part number" },
		{ "BI", "Local Stock Number (LSN)" },
		{ "BJ", "Next higher assembly number" },
		{ "BK", "Data category" },
		{ "BL", "Control number" },
		{ "BM", "Special material identification code" },
		{ "BO", "Buyers color" },
		{ "BP", "Buyer's part number" },
		{ "CC", "Industry commodity code" },
		{ "CG", "Commodity grouping" },
		{ "CL", "Color number" },
		{ "CR", "Contract number" },
		{ "CV", "Customs article number" },
		{ "DR", "Drawing revision number" },
		{ "DW", "Drawing" },
		{ "EC", "Engineering change level" },
		{ "EF", "Material code" },
		{ "EN", "International Article Numbering Association (EAN)" },
		{ "GB", "Buyer's internal product group code" },
		{ "GN", "National product group code" },
		{ "GS", "General specification number" },
		{ "HS", "Harmonised system" },
		{ "IB", "ISBN (International Standard Book Number)" },
		{ "IN", "Buyer's item number" },
		{ "IS", "ISSN (International Standard Serial Number)" },
		{ "IT", "Buyer's style number" },
		{ "IZ", "Buyer's size code" },
		{ "MA", "Machine number" },
		{ "MF", "Manufacturer's (producer's) article number" },
		{ "MN", "Model number" },
		{ "MP", "Product/service identification number" },
		{ "NB", "Batch number" },
		{ "ON", "Customer order number" },
		{ "PD", "Part number description" },
		{ "PL", "Purchaser's order line number" },
		{ "PO", "Purchase order number" },
		{ "PV", "Promotional variant number" },
		{ "QS", "Buyer's qualifier for size" },
		{ "RC", "Returnable container number" },
		{ "RN", "Release number" },
		{ "RU", "Run number" },
		{ "RY", "Record keeping of model year" },
		{ "SA", "Supplier's article number" },
		{ "SG", "Standard group of products (mixed assortment)" },
		{ "SK", "SKU (Stock keeping unit)" },
		{ "SN", "Serial number" },
		{ "SS", "Supplier's supplier article number" },
		{ "ST", "Style number" },
		{ "TG", "Transport group number" },
		{ "UA", "Ultimate customer's article number" },
		{ "UP", "UPC (Universal product code)" },
		{ "VN", "Vendor item number" },
		{ "VP", "Vendor's (seller's) part number" },
		{ "VS", "Vendor's supplemental item number" },
		{ "VX", "Vendor specification number" },
		{ "SRS", "RSK number" },
		{ "ZZZ", "Mutually defined" },
	};
}