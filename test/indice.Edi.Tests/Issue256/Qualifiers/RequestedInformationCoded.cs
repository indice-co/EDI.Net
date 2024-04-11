using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Requested information, coded
/// </summary>
public class RequestedInformationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RequestedInformationCoded(string s) => new RequestedInformationCoded { Code = s };

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
		{ "1", "Article price composition" },
		{ "2", "Article price" },
		{ "3", "Constituent material" },
		{ "4", "Carrier" },
		{ "5", "Conditions of sale" },
		{ "6", "Delivery party" },
		{ "7", "Economics dates" },
		{ "8", "Lead time" },
		{ "9", "Packaging price composition" },
		{ "10", "Packaging details" },
		{ "11", "Production location" },
		{ "12", "Packaging price" },
		{ "13", "Payment terms" },
		{ "14", "Shipment from location" },
		{ "15", "Tooling price composition" },
		{ "16", "Tooling items details" },
		{ "17", "Tooling total details" },
		{ "18", "Validity dates" },
		{ "19", "Working pattern" },
	};
}