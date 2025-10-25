using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Clinical information qualifier
/// </summary>
public class ClinicalInformationQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ClinicalInformationQualifier(string s) => new ClinicalInformationQualifier { Code = s };

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
		{ "1", "Anamnesis" },
		{ "2", "Diagnosis" },
	};
}