using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Sample location, coded
/// </summary>
public class SampleLocationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SampleLocationCoded(string s) => new SampleLocationCoded { Code = s };

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
		{ "1", "Bore" },
		{ "2", "Rim" },
		{ "3", "Web" },
		{ "4", "Centre" },
		{ "5", "Core" },
		{ "6", "Surface" },
		{ "7", "Beginning" },
		{ "8", "End" },
		{ "9", "Middle" },
		{ "10", "Centre Back" },
		{ "11", "Centre front" },
		{ "12", "Centre middle" },
		{ "13", "Edge back" },
		{ "14", "Edge front" },
		{ "15", "Edge middle" },
		{ "16", "Quarter back" },
		{ "17", "Quarter front" },
		{ "18", "Quarter middle" },
		{ "19", "Weld" },
		{ "20", "Body" },
	};
}