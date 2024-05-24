using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Maintenance operation, coded
/// </summary>
public class MaintenanceOperationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MaintenanceOperationCoded(string s) => new MaintenanceOperationCoded { Code = s };

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
		{ "1", "New" },
		{ "2", "Add and replace" },
		{ "3", "Replace" },
		{ "4", "Delete" },
		{ "5", "None" },
		{ "6", "Addition" },
		{ "7", "Change" },
		{ "8", "Marked for deletion" },
	};
}