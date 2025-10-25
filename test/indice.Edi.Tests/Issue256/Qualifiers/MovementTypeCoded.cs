using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Movement type, coded
/// </summary>
public class MovementTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MovementTypeCoded(string s) => new MovementTypeCoded { Code = s };

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
		{ "1", "Breakbulk" },
		{ "2", "LCL/LCL" },
		{ "3", "FCL/FCL" },
		{ "4", "FCL/LCL" },
		{ "5", "LCL/FCL" },
		{ "11", "House to house" },
		{ "12", "House to terminal" },
		{ "13", "House to pier" },
		{ "21", "Terminal to house" },
		{ "22", "Terminal to terminal" },
		{ "23", "Terminal to pier" },
		{ "31", "Pier to house" },
		{ "32", "Pier to terminal" },
		{ "33", "Pier to pier" },
		{ "41", "Station to station" },
		{ "42", "House to warehouse" },
		{ "43", "Warehouse to house" },
		{ "44", "Station to house" },
	};
}