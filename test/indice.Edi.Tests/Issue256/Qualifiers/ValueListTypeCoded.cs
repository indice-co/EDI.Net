using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Value list type, coded
/// </summary>
public class ValueListTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ValueListTypeCoded(string s) => new ValueListTypeCoded { Code = s };

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
		{ "1", "Non coded list" },
		{ "2", "Date and time list" },
		{ "3", "Coded list" },
	};
}