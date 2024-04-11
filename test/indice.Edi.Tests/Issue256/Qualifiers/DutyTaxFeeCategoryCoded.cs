using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Duty/tax/fee category, coded
/// </summary>
public class DutyTaxFeeCategoryCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DutyTaxFeeCategoryCoded(string s) => new DutyTaxFeeCategoryCoded { Code = s };

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
		{ "A", "Mixed tax rate" },
		{ "B", "Transferred (VAT)" },
		{ "C", "Duty paid by supplier" },
		{ "E", "Exempt from tax" },
		{ "G", "Free export item, tax not charged" },
		{ "H", "Higher rate" },
		{ "O", "Services outside scope of tax" },
		{ "S", "Standard rate" },
		{ "Z", "Zero rated goods" },
		{ "AA", "Lower rate" },
		{ "AB", "Exempt for resale" },
		{ "AC", "Value Added Tax (VAT) not now due for payment" },
		{ "AD", "Value Added Tax (VAT) due from a previous invoice" },
	};
}