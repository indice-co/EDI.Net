using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Transport charges method of payment, coded
/// </summary>
public class TransportChargesMethodOfPaymentCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TransportChargesMethodOfPaymentCoded(string s) => new TransportChargesMethodOfPaymentCoded { Code = s };

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
		{ "A", "Account" },
		{ "CA", "Advance collect" },
		{ "CC", "Collect" },
		{ "CF", "Collect, freight credited to payment customer" },
		{ "DF", "Defined by buyer and seller" },
		{ "FO", "FOB port of call" },
		{ "IC", "Information copy, no payment due" },
		{ "MX", "Mixed" },
		{ "NC", "Service freight, no charge" },
		{ "NS", "Not specified" },
		{ "PA", "Advance prepaid" },
		{ "PB", "Customer pick-up/backhaul" },
		{ "PC", "Prepaid but charged to customer" },
		{ "PE", "Payable elsewhere" },
		{ "PO", "Prepaid only" },
		{ "PP", "Prepaid (by seller)" },
		{ "PU", "Pickup" },
		{ "RC", "Return container freight paid by customer" },
		{ "RF", "Return container freight free" },
		{ "RS", "Return container freight paid by supplier" },
		{ "TP", "Third party pay" },
		{ "WC", "Weight condition" },
		{ "ZZZ", "Mutually defined" },
	};
}