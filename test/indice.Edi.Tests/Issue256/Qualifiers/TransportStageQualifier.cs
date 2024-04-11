using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Transport stage qualifier
/// </summary>
public class TransportStageQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TransportStageQualifier(string s) => new TransportStageQualifier { Code = s };

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
		{ "1", "Inland transport" },
		{ "2", "At the statistical territory limit" },
		{ "10", "Pre-carriage transport" },
		{ "11", "At border" },
		{ "12", "At departure" },
		{ "13", "At destination" },
		{ "14", "At the statistical territory limit" },
		{ "20", "Main-carriage transport" },
		{ "21", "Main carriage - first carrier" },
		{ "22", "Main carriage - second carrier" },
		{ "23", "Main carriage - third carrier" },
		{ "24", "Inland waterway transport" },
		{ "25", "Delivery carrier all transport" },
		{ "26", "Second pre-carriage transport" },
		{ "27", "Pre-acceptance transport" },
		{ "28", "Second on-carriage transport" },
		{ "30", "On-carriage transport" },
	};
}