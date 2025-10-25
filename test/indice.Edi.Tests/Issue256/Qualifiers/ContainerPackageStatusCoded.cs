using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Container/package status, coded
/// </summary>
public class ContainerPackageStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ContainerPackageStatusCoded(string s) => new ContainerPackageStatusCoded { Code = s };

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
		{ "1", "Full load" },
		{ "2", "Part load" },
		{ "3", "Full load mixed consignments" },
		{ "4", "Part load mixed consignments" },
		{ "5", "Single invoiced load" },
		{ "6", "Multi invoiced load" },
		{ "7", "Empty" },
		{ "8", "Full load, multiple bills" },
	};
}