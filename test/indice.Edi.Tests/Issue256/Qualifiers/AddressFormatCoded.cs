using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Address format, coded
/// </summary>
public class AddressFormatCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator AddressFormatCoded(string s) => new AddressFormatCoded { Code = s };

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
		{ "1", "Street name followed by number" },
		{ "2", "Number, road type, road name in this sequence" },
		{ "3", "Road type, road name, number in this sequence" },
		{ "4", "Post office box" },
		{ "5", "Unstructured address" },
	};
}