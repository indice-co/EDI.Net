using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Charge category, coded
/// </summary>
public class ChargeCategoryCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ChargeCategoryCoded(string s) => new ChargeCategoryCoded { Code = s };

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
		{ "1", "All charges" },
		{ "2", "Additional charges" },
		{ "3", "Transport charges + additional charges" },
		{ "4", "Basic freight" },
		{ "5", "Destination haulage charges" },
		{ "6", "Disbursement" },
		{ "7", "Destination port charges" },
		{ "8", "Miscellaneous charges" },
		{ "9", "Transport charges up to a specified location" },
		{ "10", "Origin port charges" },
		{ "11", "Origin haulage charges" },
		{ "12", "Other charges" },
		{ "13", "Specific amount payable" },
		{ "14", "Transport costs (carriage charges)" },
		{ "15", "All costs up to a specified location" },
		{ "16", "Weight/valuation charge" },
		{ "17", "All costs" },
		{ "18", "Transport costs and supplementary costs" },
	};
}