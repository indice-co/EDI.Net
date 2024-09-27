using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Returnable package freight payment responsibility, coded
/// </summary>
public class ReturnablePackageFreightPaymentResponsibilityCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ReturnablePackageFreightPaymentResponsibilityCoded(string s) => new ReturnablePackageFreightPaymentResponsibilityCoded { Code = s };

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
		{ "1", "Paid by customer" },
		{ "2", "Free" },
		{ "3", "Paid by supplier" },
		{ "ZZZ", "Mutually defined" },
	};
}