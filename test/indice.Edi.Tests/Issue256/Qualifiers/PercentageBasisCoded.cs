using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Percentage basis, coded
/// </summary>
public class PercentageBasisCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PercentageBasisCoded(string s) => new PercentageBasisCoded { Code = s };

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
		{ "1", "Per unit" },
		{ "2", "Per ton" },
		{ "3", "Per equipment unit" },
		{ "4", "Per unit price" },
		{ "5", "Per quantity" },
		{ "6", "Basic charge" },
		{ "7", "Rate per kilogram" },
		{ "8", "Minimum charge" },
		{ "9", "Normal rate" },
		{ "10", "Quantity rate" },
		{ "11", "Amount of drawing" },
		{ "12", "Documentary credit amount" },
		{ "13", "Invoice value" },
		{ "14", "CIF value" },
	};
}