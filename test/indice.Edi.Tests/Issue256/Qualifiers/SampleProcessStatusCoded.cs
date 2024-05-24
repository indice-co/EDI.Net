using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Sample process status, coded
/// </summary>
public class SampleProcessStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SampleProcessStatusCoded(string s) => new SampleProcessStatusCoded { Code = s };

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
		{ "1", "In process specimen" },
		{ "2", "Finished product specimen" },
	};
}