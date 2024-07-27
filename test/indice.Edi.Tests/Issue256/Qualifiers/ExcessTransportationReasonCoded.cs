using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Excess transportation reason, coded
/// </summary>
public class ExcessTransportationReasonCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ExcessTransportationReasonCoded(string s) => new ExcessTransportationReasonCoded { Code = s };

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
		{ "A", "Special rail car order, schedule increase forecast change" },
		{ "B", "Engineering change or late release" },
		{ "C", "Specification (schedule) error/overbuilding" },
		{ "D", "Shipment tracing delay" },
		{ "E", "Plant inventory loss" },
		{ "F", "Building ahead of schedule" },
		{ "G", "Vendor behind schedule" },
		{ "H", "Failed to include in last shipment" },
		{ "I", "Carrier loss claim" },
		{ "J", "Transportation failure" },
		{ "K", "Insufficient weight for carload" },
		{ "L", "Reject or discrepancy (material rejected in prior shipment)" },
		{ "M", "Transportation delay" },
		{ "N", "Lack of railcar or railroad equipment" },
		{ "P", "Releasing error" },
		{ "R", "Record error or cate reported discrepancy report" },
		{ "T", "Common or peculiar part schedule increase" },
		{ "U", "Alternative supplier shipping for responsible supplier" },
		{ "V", "Direct schedule or locally controlled" },
		{ "W", "Purchasing waiver approval" },
		{ "X", "Authorization code to be determined" },
		{ "Y", "Pilot material" },
		{ "ZZZ", "Mutually defined" },
	};
}