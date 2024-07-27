using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Delivery plan status indicator, coded
/// </summary>
public class DeliveryPlanStatusIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DeliveryPlanStatusIndicatorCoded(string s) => new DeliveryPlanStatusIndicatorCoded { Code = s };

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
		{ "1", "Firm" },
		{ "2", "Commitment for manufacturing and material" },
		{ "3", "Commitment for material" },
		{ "4", "Planning/forecast" },
		{ "5", "Short delivered on previous delivery" },
		{ "9", "User defined" },
		{ "10", "Immediate" },
		{ "11", "Pilot/Pre-volume" },
		{ "12", "Planning" },
		{ "13", "Potential order increase" },
		{ "14", "Average plant usage" },
		{ "15", "First time reported firm" },
		{ "16", "Maximum" },
		{ "17", "Tooling capacity" },
		{ "18", "Normal tooling capacity" },
		{ "19", "Prototype" },
		{ "20", "Strike protection" },
		{ "21", "Required tooling capacity" },
		{ "22", "Deliver to schedule" },
		{ "23", "Await manual pull" },
		{ "24", "Reference to commercial agreement between partners" },
		{ "25", "Reference to commercial agreement between partners" },
		{ "26", "Proposed" },
	};
}