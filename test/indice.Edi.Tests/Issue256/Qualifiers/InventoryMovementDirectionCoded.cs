using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Inventory movement direction, coded
/// </summary>
public class InventoryMovementDirectionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator InventoryMovementDirectionCoded(string s) => new InventoryMovementDirectionCoded { Code = s };

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
		{ "1", "Movement out of inventory" },
		{ "2", "Movement into inventory" },
	};
}