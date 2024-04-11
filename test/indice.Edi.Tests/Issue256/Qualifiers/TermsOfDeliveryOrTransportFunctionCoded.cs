using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Terms of delivery or transport function, coded
/// </summary>
public class TermsOfDeliveryOrTransportFunctionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TermsOfDeliveryOrTransportFunctionCoded(string s) => new TermsOfDeliveryOrTransportFunctionCoded { Code = s };

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
		{ "1", "Price condition" },
		{ "2", "Despatch condition" },
		{ "3", "Price and despatch condition" },
		{ "4", "Collected by customer" },
		{ "5", "Transport condition" },
		{ "6", "Delivery condition" },
	};
}