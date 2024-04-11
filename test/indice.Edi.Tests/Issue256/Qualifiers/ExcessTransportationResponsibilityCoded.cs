using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Excess transportation responsibility, coded
/// </summary>
public class ExcessTransportationResponsibilityCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ExcessTransportationResponsibilityCoded(string s) => new ExcessTransportationResponsibilityCoded { Code = s };

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
		{ "A", "Customer plant (receiving location)" },
		{ "B", "Material release issuer" },
		{ "S", "Supplier authority" },
		{ "X", "Responsibility to be determined" },
		{ "ZZZ", "Mutually defined" },
	};
}