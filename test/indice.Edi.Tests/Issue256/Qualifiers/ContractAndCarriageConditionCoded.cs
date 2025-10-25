using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Contract and carriage condition, coded
/// </summary>
public class ContractAndCarriageConditionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ContractAndCarriageConditionCoded(string s) => new ContractAndCarriageConditionCoded { Code = s };

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
		{ "1", "AVC conditions" },
		{ "2", "Special agreement for parcels transport" },
		{ "3", "Special agreement for full loading transport" },
		{ "4", "Combined transport" },
		{ "5", "FIATA combined transport bill of lading" },
		{ "6", "Freight forwarders national conditions" },
		{ "7", "Normal tariff, parcels transport" },
		{ "8", "Normal tariff, full loading transport" },
		{ "9", "Ordinary" },
		{ "10", "Port to port" },
		{ "11", "CMR carnet" },
		{ "12", "Special tariff, parcels transport" },
		{ "13", "Special tariff, full transport" },
		{ "14", "Through transport" },
		{ "15", "Cancel space allocation" },
		{ "16", "Report sale of space" },
		{ "17", "Alternative space allocation" },
		{ "18", "No alternative space allocation" },
		{ "19", "Allotment sale" },
		{ "20", "Confirmation of space" },
		{ "21", "Unable to confirm" },
		{ "22", "Non-operative flight" },
		{ "23", "Wait list" },
		{ "24", "Prior space allocation request" },
		{ "25", "Holding confirmed space allocation" },
		{ "26", "Holding wait list" },
		{ "27", "Door-to-door" },
		{ "28", "Door-to-pier" },
		{ "29", "Pier-to-door" },
		{ "30", "Pier-to-pier" },
		{ "31", "Space cancellation noted" },
		{ "32", "Mini landbridge service" },
		{ "33", "Space cancellation noted" },
		{ "34", "Speed level - required" },
		{ "35", "Speed level - adopted" },
		{ "36", "Normal tariff, less than full load transport" },
	};
}