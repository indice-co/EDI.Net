using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Equipment size and type identification
/// </summary>
public class EquipmentSizeAndTypeIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator EquipmentSizeAndTypeIdentification(string s) => new EquipmentSizeAndTypeIdentification { Code = s };

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
		{ "1", "Dime coated tank" },
		{ "2", "Epoxy coated tank" },
		{ "3", "IMO1" },
		{ "4", "IMO2" },
		{ "5", "IMO3" },
		{ "6", "Pressurized tank" },
		{ "7", "Refrigerated tank" },
		{ "8", "Semi-refrigerated" },
		{ "9", "Stainless steel tank" },
		{ "10", "Nonworking reefer container 40 ft" },
		{ "11", "Box pallet" },
		{ "12", "Europallet" },
		{ "13", "Scandinavian pallet" },
		{ "14", "Trailer" },
		{ "15", "Nonworking reefer container 20 ft" },
		{ "16", "Exchangeable pallet" },
		{ "17", "Semi-trailer" },
		{ "18", "Tank container 20 ft." },
		{ "19", "Tank container 30 ft." },
		{ "20", "Tank container 40 ft." },
		{ "21", "Container IC 20 ft." },
		{ "22", "Container IC 30 ft." },
		{ "23", "Container IC 40 ft." },
		{ "24", "Refrigerator tank 20 ft." },
		{ "25", "Refrigerator tank 30 ft." },
		{ "26", "Refrigerator tank 40 ft." },
		{ "27", "Tank container IC 20 ft." },
		{ "28", "Tank container IC 30 ft." },
		{ "29", "Tank container IC 40 ft." },
		{ "30", "Refrigerator tank IC 20 ft." },
		{ "31", "Temperature controlled container 30 ft." },
		{ "32", "Refrigerator tank IC 40 ft." },
		{ "33", "Movable case: L < 6,15m" },
		{ "34", "Movable case: 6,15m < L < 7,82m" },
		{ "35", "Movable case: 7,82m < L < 9,15m" },
		{ "36", "Movable case: 9,15m < L < 10,90m" },
		{ "37", "Movable case: 10,90m < L < 13,75m" },
		{ "38", "Totebin" },
		{ "39", "Temperature controlled container 20 ft" },
		{ "40", "Temperature controlled container 40 ft" },
		{ "41", "Non working refrigerated (reefer) container 30ft." },
		{ "42", "Dual trailers" },
	};
}