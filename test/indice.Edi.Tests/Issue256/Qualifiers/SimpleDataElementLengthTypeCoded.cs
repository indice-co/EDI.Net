using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Simple data element length type, coded
/// </summary>
public class SimpleDataElementLengthTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SimpleDataElementLengthTypeCoded(string s) => new SimpleDataElementLengthTypeCoded { Code = s };

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
		{ "1", "Fixed" },
		{ "2", "Variable" },
	};
}