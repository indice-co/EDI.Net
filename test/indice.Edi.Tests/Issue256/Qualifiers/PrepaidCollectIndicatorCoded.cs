using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Prepaid/collect indicator, coded
/// </summary>
public class PrepaidCollectIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PrepaidCollectIndicatorCoded(string s) => new PrepaidCollectIndicatorCoded { Code = s };

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
		{ "A", "Payable elsewhere" },
		{ "B", "Third party to pay" },
		{ "C", "Collect" },
		{ "P", "Prepaid" },
	};
}