using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Sequence number source, coded
/// </summary>
public class SequenceNumberSourceCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SequenceNumberSourceCoded(string s) => new SequenceNumberSourceCoded { Code = s };

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
		{ "1", "Broadcast 1" },
		{ "2", "Broadcast 2" },
		{ "3", "Manufacturer sequence number" },
		{ "4", "Manufacturer production sequence number" },
		{ "5", "Transmission sequence" },
		{ "6", "Structure sequence" },
	};
}