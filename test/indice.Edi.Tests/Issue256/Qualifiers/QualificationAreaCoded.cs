using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Qualification area, coded
/// </summary>
public class QualificationAreaCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator QualificationAreaCoded(string s) => new QualificationAreaCoded { Code = s };

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
		{ "1", "Public administration sector" },
		{ "2", "Agricultural sector" },
		{ "3", "Automotive sector" },
		{ "4", "Transport sector" },
		{ "5", "Finance sector" },
		{ "6", "Tourism sector" },
		{ "7", "Construction sector" },
	};
}