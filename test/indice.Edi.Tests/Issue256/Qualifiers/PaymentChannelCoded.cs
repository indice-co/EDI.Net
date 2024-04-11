using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Payment channel, coded
/// </summary>
public class PaymentChannelCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PaymentChannelCoded(string s) => new PaymentChannelCoded { Code = s };

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
		{ "1", "Ordinary post" },
		{ "2", "Air mail" },
		{ "3", "Telegraph" },
		{ "4", "Telex" },
		{ "5", "S.W.I.F.T." },
		{ "6", "Other transmission networks" },
		{ "7", "Networks not defined" },
		{ "8", "Fedwire" },
		{ "9", "Personal (face-to-face)" },
		{ "10", "Registered air mail" },
		{ "11", "Registered mail" },
		{ "12", "Courier" },
		{ "13", "Messenger" },
		{ "14", "National ACH" },
		{ "15", "Other ACH" },
		{ "ZZZ", "Mutually defined" },
	};
}