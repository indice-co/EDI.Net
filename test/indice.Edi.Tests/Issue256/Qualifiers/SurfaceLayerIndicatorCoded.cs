using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Surface/layer indicator, coded
/// </summary>
public class SurfaceLayerIndicatorCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator SurfaceLayerIndicatorCoded(string s) => new SurfaceLayerIndicatorCoded { Code = s };

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
		{ "1S", "Side one" },
		{ "2S", "Side two" },
		{ "AA", "On surface" },
		{ "AB", "Off surface" },
		{ "AC", "Soluble" },
		{ "AD", "Opposite corners" },
		{ "AE", "Corner Diagonals" },
		{ "BC", "Back of cab" },
		{ "BS", "Both sides" },
		{ "BT", "Bottom" },
		{ "DF", "Dual fuel tank positions" },
		{ "FR", "Front" },
		{ "IN", "Inside" },
		{ "LE", "Left" },
		{ "OA", "Overall" },
		{ "OS", "One side" },
		{ "OT", "Outside" },
		{ "RI", "Right" },
		{ "RR", "Rear" },
		{ "ST", "Spare tyre position" },
		{ "TB", "Tank bottom" },
		{ "TP", "Top" },
		{ "TS", "Two sides" },
		{ "UC", "Under cab" },
	};
}