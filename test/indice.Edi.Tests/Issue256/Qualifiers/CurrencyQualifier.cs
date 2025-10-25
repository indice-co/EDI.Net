using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Currency qualifier
/// </summary>
public class CurrencyQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CurrencyQualifier(string s) => new CurrencyQualifier { Code = s };

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
		{ "1", "Customs valuation currency" },
		{ "2", "Insurance currency" },
		{ "3", "Home currency" },
		{ "4", "Invoicing currency" },
		{ "5", "Account currency" },
		{ "6", "Reference currency" },
		{ "7", "Target currency" },
		{ "8", "Price list currency" },
		{ "9", "Order currency" },
		{ "10", "Pricing currency" },
		{ "11", "Payment currency" },
		{ "12", "Quotation currency" },
		{ "13", "Recipient local currency" },
		{ "14", "Supplier currency" },
		{ "15", "Sender local currency" },
		{ "16", "Tariff currency" },
		{ "17", "Charge calculation currency" },
	};
}