using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Characteristic relevance, coded
/// </summary>
public class CharacteristicRelevanceCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CharacteristicRelevanceCoded(string s) => new CharacteristicRelevanceCoded { Code = s };

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
		{ "1", "Information only" },
		{ "2", "Required within orders" },
		{ "3", "Requirement for subsequent business transactions" },
	};
}