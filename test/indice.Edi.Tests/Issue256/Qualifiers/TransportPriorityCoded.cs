using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Transport priority, coded
/// </summary>
public class TransportPriorityCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TransportPriorityCoded(string s) => new TransportPriorityCoded { Code = s };

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
		{ "1", "Express" },
		{ "2", "High speed" },
		{ "3", "Normal speed" },
		{ "4", "Post service" },
	};
}