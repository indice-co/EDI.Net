using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Type of period, coded
/// </summary>
public class TypeOfPeriodCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TypeOfPeriodCoded(string s) => new TypeOfPeriodCoded { Code = s };

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
		{ "D", "Day" },
		{ "F", "Period of two weeks" },
		{ "H", "Hour" },
		{ "M", "Month" },
		{ "P", "Four month period" },
		{ "S", "Second" },
		{ "W", "Week" },
		{ "Y", "Year" },
		{ "3M", "Quarter" },
		{ "6M", "Half-year" },
		{ "AA", "Air hour" },
		{ "AD", "Air day" },
		{ "CD", "Calendar day (includes weekends and holidays)" },
		{ "CW", "Calendar week (7day)" },
		{ "DC", "Ten days period" },
		{ "DW", "Work day" },
		{ "HM", "Half month" },
		{ "MN", "Minute" },
		{ "SD", "Surface day" },
		{ "SI", "Indefinite" },
		{ "WD", "Working days" },
		{ "WW", "5 day work week" },
		{ "ZZZ", "Mutually defined" },
	};
}