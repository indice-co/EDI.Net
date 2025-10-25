using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Type of inventory affected, coded
/// </summary>
public class TypeOfInventoryAffectedCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TypeOfInventoryAffectedCoded(string s) => new TypeOfInventoryAffectedCoded { Code = s };

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
		{ "1", "Accepted product inventory" },
		{ "2", "Damaged product inventory" },
		{ "3", "Bonded inventory" },
		{ "4", "Reserved inventory" },
	};
}