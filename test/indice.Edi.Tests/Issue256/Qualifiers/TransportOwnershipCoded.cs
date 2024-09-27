using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Transport ownership, coded
/// </summary>
public class TransportOwnershipCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TransportOwnershipCoded(string s) => new TransportOwnershipCoded { Code = s };

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
		{ "1", "Transport for the owner's account" },
		{ "2", "Transport for another account" },
		{ "3", "Private transport" },
	};
}