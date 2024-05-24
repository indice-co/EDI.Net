using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Simple data element character representation, coded
/// </summary>
public class SimpleDataElementCharacterRepresentationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SimpleDataElementCharacterRepresentationCoded(string s) => new SimpleDataElementCharacterRepresentationCoded { Code = s };

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
		{ "1", "Alphabetic" },
		{ "2", "Alphanumeric" },
		{ "3", "Numeric" },
		{ "4", "Binary" },
		{ "5", "Date" },
		{ "6", "Identifier" },
		{ "7", "Time" },
		{ "8", "Numeric, implied decimal point of 0 places" },
		{ "9", "String" },
		{ "10", "Numeric, implied decimal point of 1 place" },
		{ "11", "Numeric, implied decimal point of 2 places" },
		{ "12", "Numeric, implied decimal point of 3 places" },
		{ "13", "Numeric, implied decimal point of 4 places" },
		{ "14", "Numeric, implied decimal point of 5 places" },
		{ "15", "Numeric, implied decimal point of 6 places" },
		{ "16", "Numeric, implied decimal point of 7 places" },
		{ "17", "Numeric, implied decimal point of 8 places" },
		{ "18", "Numeric, implied decimal point of 9 places" },
	};
}