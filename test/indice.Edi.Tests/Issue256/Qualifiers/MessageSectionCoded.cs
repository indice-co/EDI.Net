using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Message section, coded
/// </summary>
public class MessageSectionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MessageSectionCoded(string s) => new MessageSectionCoded { Code = s };

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
		{ "1", "Heading section" },
		{ "2", "Detail section of a message" },
		{ "5", "Multiple sections" },
		{ "6", "Summary section" },
		{ "7", "Sub-line item" },
		{ "8", "Commercial heading section of CUSDEC" },
		{ "9", "Commercial line detail section of CUSDEC" },
		{ "10", "Customs item detail section of CUSDEC" },
		{ "11", "Customs sub-item detail section of CUSDEC" },
	};
}