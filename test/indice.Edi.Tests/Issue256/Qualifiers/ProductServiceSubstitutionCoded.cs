using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Product/service substitution, coded
/// </summary>
public class ProductServiceSubstitutionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ProductServiceSubstitutionCoded(string s) => new ProductServiceSubstitutionCoded { Code = s };

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
		{ "1", "No substitution allowed" },
		{ "2", "Supply any binding if edition ordered not available" },
		{ "3", "Supply paper binding if edition ordered not available" },
		{ "4", "Supply cloth binding if edition ordered not available" },
		{ "5", "Supply library binding if edition ordered not available" },
		{ "6", "Equivalent item substitution" },
		{ "7", "Alternate item substitution allowed" },
		{ "ZZZ", "Mutually defined" },
	};
}