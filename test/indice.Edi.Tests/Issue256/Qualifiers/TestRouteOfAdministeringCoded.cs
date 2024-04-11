using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Test route of administering, coded
/// </summary>
public class TestRouteOfAdministeringCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TestRouteOfAdministeringCoded(string s) => new TestRouteOfAdministeringCoded { Code = s };

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
		{ "1", "Oral" },
		{ "2", "Dermal" },
		{ "3", "Inhalation" },
		{ "ZZZ", "Mutually defined" },
	};
}