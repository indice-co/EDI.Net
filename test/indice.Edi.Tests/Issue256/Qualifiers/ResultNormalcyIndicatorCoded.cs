using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Result normalcy indicator, coded
/// </summary>
public class ResultNormalcyIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ResultNormalcyIndicatorCoded(string s) => new ResultNormalcyIndicatorCoded { Code = s };

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
		{ "1", "Above high reference limit" },
		{ "2", "Below low reference limit" },
	};
}