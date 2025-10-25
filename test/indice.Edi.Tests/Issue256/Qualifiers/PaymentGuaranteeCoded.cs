using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Payment guarantee, coded
/// </summary>
public class PaymentGuaranteeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PaymentGuaranteeCoded(string s) => new PaymentGuaranteeCoded { Code = s };

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
		{ "1", "Factor guarantee" },
		{ "10", "Bank guarantee" },
		{ "11", "Public authority guarantee" },
		{ "12", "Third party guarantee" },
		{ "13", "Standby letter of credit" },
		{ "14", "No guarantee" },
		{ "20", "Goods as security" },
		{ "21", "Business as security" },
		{ "23", "Warrant or similar (warehouse receipts)" },
		{ "24", "Mortgage" },
		{ "41", "Book guarantee/book bond" },
		{ "44", "Group guarantee" },
		{ "45", "Insurance certificate" },
		{ "ZZZ", "Mutually defined" },
	};
}