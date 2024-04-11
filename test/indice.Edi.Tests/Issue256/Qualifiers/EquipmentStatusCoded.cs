using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Equipment status, coded
/// </summary>
public class EquipmentStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator EquipmentStatusCoded(string s) => new EquipmentStatusCoded { Code = s };

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
		{ "1", "Continental" },
		{ "2", "Export" },
		{ "3", "Import" },
		{ "4", "Remain on board" },
		{ "5", "Shifter" },
		{ "6", "Transhipment" },
		{ "7", "Shortlanded" },
		{ "8", "Overlanded" },
		{ "9", "Domestic" },
		{ "10", "Positioning" },
		{ "11", "Delivery" },
		{ "12", "Redelivery" },
		{ "13", "Repair" },
	};
}