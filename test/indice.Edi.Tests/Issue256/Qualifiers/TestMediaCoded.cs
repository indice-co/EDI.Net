using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Test media, coded
/// </summary>
public class TestMediaCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TestMediaCoded(string s) => new TestMediaCoded { Code = s };

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
		{ "1", "Animal" },
		{ "2", "Human" },
		{ "3", "Sulphide" },
		{ "4", "Aluminate" },
		{ "5", "Silicate" },
		{ "6", "Oxide" },
		{ "ZZZ", "Mutually defined" },
	};
}