using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Geographic environment, coded
/// </summary>
public class GeographicEnvironmentCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator GeographicEnvironmentCoded(string s) => new GeographicEnvironmentCoded { Code = s };

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
		{ "DO", "Domestic" },
		{ "DR", "Domestic with regulatory information required" },
		{ "EA", "Economic area" },
		{ "IN", "International" },
		{ "IR", "International with regulatory information required" },
	};
}