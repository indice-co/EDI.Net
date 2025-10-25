using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Reason for inventory movement, coded
/// </summary>
public class ReasonForInventoryMovementCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ReasonForInventoryMovementCoded(string s) => new ReasonForInventoryMovementCoded { Code = s };

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
		{ "1", "Reception" },
		{ "2", "Delivery" },
		{ "3", "Scrapped parts" },
		{ "4", "Difference" },
		{ "5", "Property transfer within warehouse" },
		{ "6", "Inventory recycling" },
		{ "7", "Reversal of previous movement" },
		{ "8", "Defects (technical)" },
		{ "9", "Commercial" },
		{ "10", "Conversion" },
		{ "11", "Consumption" },
	};
}