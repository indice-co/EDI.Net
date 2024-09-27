using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Category of means of transport, coded
/// </summary>
public class CategoryOfMeansOfTransportCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CategoryOfMeansOfTransportCoded(string s) => new CategoryOfMeansOfTransportCoded { Code = s };

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
		{ "1", "ADNR code, OS" },
		{ "2", "ADNR code, 1N" },
		{ "3", "ADNR code, 1S" },
		{ "4", "ADNR code, 2" },
		{ "5", "ADNR code, 3" },
		{ "6", "ADNR code, F" },
		{ "7", "ADNR code, NF" },
		{ "8", "ADNR code, ON" },
		{ "9", "ADNR code, X" },
	};
}