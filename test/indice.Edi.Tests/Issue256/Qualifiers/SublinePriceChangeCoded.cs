using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Sub-line price change, coded
/// </summary>
public class SublinePriceChangeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SublinePriceChangeCoded(string s) => new SublinePriceChangeCoded { Code = s };

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
		{ "A", "Added to the baseline item unit price" },
		{ "I", "Included in the baseline item unit price" },
		{ "S", "Subtracted from the baseline item unit price" },
	};
}