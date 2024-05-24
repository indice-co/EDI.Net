using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Equipment supplier, coded
/// </summary>
public class EquipmentSupplierCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator EquipmentSupplierCoded(string s) => new EquipmentSupplierCoded { Code = s };

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
		{ "1", "Shipper supplied" },
		{ "2", "Carrier supplied" },
		{ "3", "Consolidator supplied" },
		{ "4", "Deconsolidator supplied" },
		{ "5", "Third party supplied" },
	};
}