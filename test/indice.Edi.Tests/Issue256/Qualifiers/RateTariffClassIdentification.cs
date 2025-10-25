using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Rate/tariff class identification
/// </summary>
public class RateTariffClassIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RateTariffClassIdentification(string s) => new RateTariffClassIdentification { Code = s };

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
		{ "A", "Senior person rate" },
		{ "B", "Basic" },
		{ "C", "Specific commodity rate" },
		{ "D", "Teenager rate" },
		{ "E", "Child rate" },
		{ "F", "Adult rate" },
		{ "K", "Rate per kilogram" },
		{ "M", "Minimum charge rate" },
		{ "N", "Normal rate" },
		{ "Q", "Quantity rate" },
		{ "R", "Class rate (Reduction on normal rate)" },
		{ "S", "Class rate (Surcharge on normal rate)" },
	};
}