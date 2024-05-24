using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Sector/subject identification qualifier
/// </summary>
public class SectorSubjectIdentificationQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SectorSubjectIdentificationQualifier(string s) => new SectorSubjectIdentificationQualifier { Code = s };

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
		{ "1", "Construction industry" },
		{ "2", "Governmental export conditions" },
		{ "3", "Chemical industry" },
		{ "4", "Electronic industry" },
		{ "5", "Automotive industry" },
		{ "6", "Steel industry" },
		{ "7", "Factoring" },
		{ "8", "Defence industry" },
		{ "9", "Alcohol beverage industry" },
		{ "10", "Police" },
		{ "11", "Customs" },
		{ "12", "Health regulation" },
		{ "13", "Balance of payments" },
		{ "14", "National legislation" },
		{ "15", "Government" },
	};
}