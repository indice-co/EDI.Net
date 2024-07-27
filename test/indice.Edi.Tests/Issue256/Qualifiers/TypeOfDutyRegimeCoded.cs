using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Type of duty regime, coded
/// </summary>
public class TypeOfDutyRegimeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TypeOfDutyRegimeCoded(string s) => new TypeOfDutyRegimeCoded { Code = s };

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
		{ "1", "Origin subject to EC/EFTA preference" },
		{ "2", "Origin subject to other preference agreement" },
		{ "3", "No preference origin" },
		{ "8", "Excluded origin" },
		{ "9", "Imposed origin" },
	};
}