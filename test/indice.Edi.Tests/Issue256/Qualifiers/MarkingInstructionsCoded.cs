using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Marking instructions, coded
/// </summary>
public class MarkingInstructionsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MarkingInstructionsCoded(string s) => new MarkingInstructionsCoded { Code = s };

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
		{ "1", "Do not mark suppliers company name" },
		{ "2", "Mark customers company name" },
		{ "3", "Mark customers references" },
		{ "4", "Mark additionally customers article description" },
		{ "5", "Mark exclusively customers article description" },
		{ "6", "Mark packages dimensions" },
		{ "7", "Mark net weight" },
		{ "8", "Mark gross weight" },
		{ "9", "Mark tare weight" },
		{ "10", "Mark batch number" },
		{ "11", "Mark article number customer" },
		{ "12", "Mark running number of packages" },
		{ "13", "Mark date of production" },
		{ "14", "Mark expiry date" },
		{ "15", "Mark supplier number" },
		{ "16", "Buyer's instructions" },
		{ "17", "Seller's instructions" },
		{ "18", "Carrier's instructions" },
		{ "19", "Legal requirements" },
		{ "20", "Industry instructions" },
		{ "21", "Line item only" },
		{ "22", "Premarked by buyer" },
		{ "23", "Entire shipment" },
		{ "24", "Shipper assigned" },
		{ "25", "Shipper assigned roll number" },
		{ "26", "Shipper assigned skid number" },
		{ "27", "Uniform Code Council (UCC) format" },
		{ "28", "Mark free text" },
		{ "29", "Mark case number" },
		{ "30", "Mark serial shipping container code" },
		{ "ZZZ", "Mutually defined" },
	};
}