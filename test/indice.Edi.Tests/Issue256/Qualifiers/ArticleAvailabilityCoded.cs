using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Article availability, coded
/// </summary>
public class ArticleAvailabilityCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ArticleAvailabilityCoded(string s) => new ArticleAvailabilityCoded { Code = s };

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
		{ "1", "New, announcement only" },
		{ "2", "New, available" },
		{ "3", "Obsolete" },
		{ "4", "Prototype" },
		{ "5", "Commodity" },
		{ "6", "Special" },
		{ "7", "Temporarily out" },
		{ "8", "Manufacture out" },
		{ "9", "Discontinued" },
		{ "10", "Seasonally available only" },
		{ "11", "Deletion, announcement only" },
	};
}