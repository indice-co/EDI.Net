using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Duty/tax/fee rate basis identification
/// </summary>
public class DutyTaxFeeRateBasisIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DutyTaxFeeRateBasisIdentification(string s) => new DutyTaxFeeRateBasisIdentification { Code = s };

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
		{ "1", "Value" },
		{ "2", "Weight" },
		{ "3", "Quantity" },
	};
}