using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Back order, coded
/// </summary>
public class BackOrderCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator BackOrderCoded(string s) => new BackOrderCoded { Code = s };

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
		{ "B", "Back order only if new item (book industry - not yet published only)" },
		{ "F", "Factory ship" },
		{ "N", "No back order" },
		{ "W", "Warehouse ship" },
		{ "Y", "Back order if out of stock" },
		{ "ZZZ", "Mutually defined" },
	};
}