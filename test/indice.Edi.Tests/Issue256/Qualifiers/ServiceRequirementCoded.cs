using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Service requirement, coded
/// </summary>
public class ServiceRequirementCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ServiceRequirementCoded(string s) => new ServiceRequirementCoded { Code = s };

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
		{ "1", "Carrier loads" },
		{ "2", "Full loads" },
		{ "3", "Less than full loads" },
		{ "4", "Shipper loads" },
		{ "5", "To be delivered" },
		{ "6", "To be kept" },
		{ "7", "Transhipment allowed" },
		{ "8", "Transhipment not allowed" },
		{ "9", "Partial shipment allowed" },
		{ "10", "Partial shipment not allowed" },
		{ "11", "Partial shipment and/or drawing allowed" },
		{ "12", "Partial shipment and/or drawing not allowed" },
		{ "13", "Carrier unloads" },
		{ "14", "Shipper unloads" },
		{ "15", "Consignee unloads" },
		{ "16", "Consignee loads" },
		{ "17", "Exclusive usage of equipment" },
		{ "18", "Non exclusive usage of equipment" },
		{ "19", "Direct delivery" },
		{ "20", "Direct pick-up" },
		{ "21", "Request for delivery advice services" },
		{ "22", "Do not arrange customs clearance" },
		{ "23", "Arrange customs clearance" },
		{ "24", "Check container condition" },
		{ "25", "Damaged containers allowed" },
		{ "26", "Dirty containers allowed" },
		{ "27", "Fork lift holes not required" },
		{ "28", "Fork lift holes required" },
		{ "29", "Insure goods during transport" },
		{ "30", "Arrange main-carriage" },
		{ "31", "Arrange on-carriage" },
		{ "32", "Arrange pre-carriage" },
		{ "33", "Report container safety convention information" },
		{ "34", "Check seals" },
		{ "35", "Container must be clean" },
		{ "36", "Request for proof of delivery" },
		{ "37", "Request for Customs procedure" },
		{ "38", "Request for administration services" },
		{ "39", "Transport insulated under Intercontainer INTERFRIGO conditions" },
		{ "40", "Transport mechanically refrigerated under Intercontainer INTERFRIGO conditions" },
		{ "41", "Cool or freeze service, not under Intercontainer INTERFRIGO conditions" },
		{ "42", "Transhipment overseas" },
	};
}