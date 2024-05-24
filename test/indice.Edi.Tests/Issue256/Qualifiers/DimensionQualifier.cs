using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Dimension qualifier
/// </summary>
public class DimensionQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DimensionQualifier(string s) => new DimensionQualifier { Code = s };

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
		{ "1", "Gross dimensions" },
		{ "2", "Package dimensions (incl. goods)" },
		{ "3", "Pallet dimensions (excl.goods)" },
		{ "4", "Pallet dimensions (incl.goods)" },
		{ "5", "Off-standard dimension front" },
		{ "6", "Off-standard dimension back" },
		{ "7", "Off-standard dimension right" },
		{ "8", "Off-standard dimension left" },
		{ "9", "Off-standard dimension general" },
		{ "10", "External equipment dimension" },
		{ "11", "Internal equipment dimensions" },
		{ "12", "Damage dimensions" },
		{ "13", "Off-standard dimensions height" },
		{ "14", "Equipment door dimensions" },
	};
}