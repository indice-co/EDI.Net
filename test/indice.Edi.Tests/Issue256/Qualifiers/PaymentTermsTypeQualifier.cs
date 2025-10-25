using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Payment terms type qualifier
/// </summary>
public class PaymentTermsTypeQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PaymentTermsTypeQualifier(string s) => new PaymentTermsTypeQualifier { Code = s };

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
		{ "1", "Basic" },
		{ "2", "End of month" },
		{ "3", "Fixed date" },
		{ "4", "Deferred" },
		{ "5", "Discount not applicable" },
		{ "6", "Mixed" },
		{ "7", "Extended" },
		{ "8", "Basic discount offered" },
		{ "9", "Proximo" },
		{ "10", "Instant" },
		{ "11", "Elective" },
		{ "12", "10 days after end of month" },
		{ "13", "Seller to advise buyer" },
		{ "14", "Paid against statement" },
		{ "15", "No charge" },
		{ "16", "Not yet defined" },
		{ "17", "Ultimo" },
		{ "18", "Previously agreed upon" },
		{ "20", "Penalty terms" },
		{ "21", "Payment by instalment" },
		{ "22", "Discount" },
		{ "23", "Available by sight payment" },
		{ "24", "Available by deferred payment" },
		{ "25", "Available by acceptance" },
		{ "26", "Available by negotiation with any bank" },
		{ "27", "Available by negotiation with any bank in ..." },
		{ "28", "Available by negotiation by named bank" },
		{ "29", "Available by negotiation" },
		{ "30", "Adjustment payment" },
		{ "31", "Late payment" },
		{ "32", "Advanced payment" },
		{ "33", "Payment by instalments according to progress (as agreed)" },
		{ "34", "Payment by instalments according to progress (to be agreed)" },
		{ "35", "Nonstandard" },
		{ "ZZZ", "Mutually defined" },
	};
}