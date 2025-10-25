using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Full/empty indicator, coded
/// </summary>
public class FullEmptyIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator FullEmptyIndicatorCoded(string s) => new FullEmptyIndicatorCoded { Code = s };

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
		{ "1", "More than one quarter volume available" },
		{ "2", "More than half volume available" },
		{ "3", "More than three quarters volume available" },
		{ "4", "Empty" },
		{ "5", "Full" },
		{ "6", "No volume available" },
		{ "7", "Full, mixed consignment" },
		{ "8", "Full, single consignment" },
	};
}