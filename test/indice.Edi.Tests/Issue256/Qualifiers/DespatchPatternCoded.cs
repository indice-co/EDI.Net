using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Despatch pattern, coded
/// </summary>
public class DespatchPatternCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DespatchPatternCoded(string s) => new DespatchPatternCoded { Code = s };

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
		{ "1", "1st week of the month" },
		{ "2", "2nd week of the month" },
		{ "3", "3rd week of the month" },
		{ "4", "4th week of the month" },
		{ "5", "5th week of the month" },
		{ "6", "1st and 3rd weeks of the month" },
		{ "7", "2nd and 4th weeks of the month" },
		{ "10", "Monday through Friday" },
		{ "11", "Monday through Saturday" },
		{ "12", "Monday through Sunday" },
		{ "13", "Monday" },
		{ "14", "Tuesday" },
		{ "15", "Wednesday" },
		{ "16", "Thursday" },
		{ "17", "Friday" },
		{ "18", "Saturday" },
		{ "19", "Sunday" },
		{ "20", "Immediately" },
		{ "21", "As directed" },
		{ "22", "Each week of the month" },
		{ "23", "Daily Monday through Friday" },
		{ "24", "First decade of the month" },
		{ "25", "Second decade of the month" },
		{ "26", "Third decade of the month" },
		{ "27", "Each working hour" },
		{ "28", "Each 2 working hours" },
		{ "29", "Each 3 working hours" },
		{ "30", "Each 4 working hours" },
		{ "31", "Each working day" },
		{ "32", "Each 2 working days" },
		{ "33", "Each 3 working days" },
		{ "34", "Each 4 working days" },
		{ "35", "Each 5 working days" },
		{ "36", "Each 6 working days" },
		{ "37", "Each 8 working days" },
		{ "38", "Each 9 working days" },
		{ "39", "Each 10 working days" },
		{ "40", "Each 11 working days" },
		{ "41", "Each 12 working days" },
		{ "42", "Each 13 working days" },
		{ "43", "Each working week" },
		{ "44", "Each 2 working weeks" },
		{ "45", "Each 3 working weeks" },
		{ "46", "Each 4 working weeks" },
		{ "47", "Each working month" },
		{ "48", "Each 2 working months" },
		{ "49", "Each 3 working months" },
		{ "ZZZ", "Mutually defined" },
	};
}