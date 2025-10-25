using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Allowance or charge qualifier
/// </summary>
public class AllowanceOrChargeQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator AllowanceOrChargeQualifier(string s) => new AllowanceOrChargeQualifier { Code = s };

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
		{ "A", "Allowance" },
		{ "B", "Total other" },
		{ "C", "Charge" },
		{ "D", "Allowance per call of" },
		{ "E", "Charge per call of" },
		{ "F", "Allowance message" },
		{ "G", "Allowance line items" },
		{ "H", "Line item allowance" },
		{ "J", "Adjustment" },
		{ "K", "Charge message" },
		{ "L", "Charge line items" },
		{ "M", "Line item charge" },
		{ "N", "No allowance or charge" },
		{ "O", "About" },
		{ "P", "Minus (percentage)" },
		{ "Q", "Minus (amount)" },
		{ "R", "Plus (percentage)" },
		{ "S", "Plus (amount)" },
		{ "T", "Plus/minus (percentage)" },
		{ "U", "Plus/minus (amount)" },
		{ "V", "No allowance" },
		{ "W", "No charge" },
		{ "X", "Maximum" },
		{ "Y", "Exact" },
	};
}