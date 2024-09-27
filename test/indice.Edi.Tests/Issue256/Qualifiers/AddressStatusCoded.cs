using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Address status, coded
/// </summary>
public class AddressStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator AddressStatusCoded(string s) => new AddressStatusCoded { Code = s };

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
		{ "1", "Permanent address" },
		{ "2", "Current address" },
		{ "4", "Previous address" },
		{ "5", "Former address" },
	};
}