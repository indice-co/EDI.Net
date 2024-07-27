using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Process type identification
/// </summary>
public class ProcessTypeIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ProcessTypeIdentification(string s) => new ProcessTypeIdentification { Code = s };

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
		{ "1", "Wood preparation" },
		{ "2", "Causticizing" },
		{ "3", "Digesting" },
		{ "4", "Brownstock washing" },
		{ "5", "Bleaching" },
		{ "6", "Pulp drying" },
		{ "7", "Freezing" },
		{ "8", "Processing of structured information" },
		{ "9", "Processing of identical information in structured and unstructured form" },
		{ "10", "Processing of different information in structured and unstructured form" },
		{ "11", "Processing of unstructured information" },
		{ "12", "Slaughter" },
		{ "13", "Packing" },
		{ "14", "Heat sterilisation" },
		{ "15", "Chemical sterilisation" },
		{ "16", "Fumigation" },
		{ "17", "Irradiation" },
	};
}