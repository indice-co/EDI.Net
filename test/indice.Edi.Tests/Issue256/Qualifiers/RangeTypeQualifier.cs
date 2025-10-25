using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Range type qualifier
/// </summary>
public class RangeTypeQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RangeTypeQualifier(string s) => new RangeTypeQualifier { Code = s };

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
		{ "1", "Allowance range" },
		{ "2", "Charge range" },
		{ "3", "Monetary range" },
		{ "4", "Quantity range" },
		{ "5", "Temperature range" },
		{ "6", "Order quantity range" },
		{ "7", "Delivery quantity range" },
		{ "8", "Production batch range" },
		{ "9", "Monthly quantity range" },
		{ "10", "Annual quantity range" },
		{ "11", "Package stacking range" },
		{ "12", "Transport temperature range" },
		{ "13", "Equipment pre-tripping temperature range" },
	};
}