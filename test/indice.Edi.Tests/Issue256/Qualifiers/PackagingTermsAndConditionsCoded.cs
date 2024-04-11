using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Packaging terms and conditions, coded
/// </summary>
public class PackagingTermsAndConditionsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PackagingTermsAndConditionsCoded(string s) => new PackagingTermsAndConditionsCoded { Code = s };

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
		{ "1", "Packaging cost paid by supplier" },
		{ "2", "Packaging cost paid by recipient" },
		{ "3", "Packaging cost not charged (returnable)" },
		{ "4", "Buyer's" },
		{ "5", "Carrier's durable" },
		{ "6", "Carrier's expendable" },
		{ "7", "Seller's durable" },
		{ "8", "Seller's expendable" },
		{ "9", "Special purpose buyer's durable" },
		{ "10", "Special purpose buyer's expendable" },
		{ "11", "Multiple usage buyer's durable" },
		{ "12", "Multiple usage seller's durable" },
		{ "13", "Not packed" },
		{ "14", "Special purpose seller's durable" },
		{ "15", "Export quality" },
		{ "16", "Domestic quality" },
		{ "17", "Packaging included in price" },
		{ "18", "Packaging costs split" },
		{ "19", "Packaging costs invoiced separately" },
		{ "20", "Nil packaging costs" },
		{ "21", "Nil packaging costs if packaging returned" },
		{ "22", "Return chargeable" },
		{ "23", "Chargeable, two thirds of paid amount with credit note on return of loaned package" },
	};
}