using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Date/time/period format qualifier
/// </summary>
public class DateTimePeriodFormatQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DateTimePeriodFormatQualifier(string s) => new DateTimePeriodFormatQualifier { Code = s };

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
		{ "2", "DDMMYY" },
		{ "3", "MMDDYY" },
		{ "101", "YYMMDD" },
		{ "102", "CCYYMMDD" },
		{ "103", "YYWWD" },
		{ "105", "YYDDD" },
		{ "106", "MMDD" },
		{ "107", "DDD" },
		{ "108", "WW" },
		{ "109", "MM" },
		{ "110", "DD" },
		{ "201", "YYMMDDHHMM" },
		{ "202", "YYMMDDHHMMSS" },
		{ "203", "CCYYMMDDHHMM" },
		{ "204", "CCYYMMDDHHMMSS" },
		{ "301", "YYMMDDHHMMZZZ" },
		{ "302", "YYMMDDHHMMSSZZZ" },
		{ "303", "CCYYMMDDHHMMZZZ" },
		{ "304", "CCYYMMDDHHMMSSZZZ" },
		{ "305", "MMDDHHMM" },
		{ "306", "DDHHMM" },
		{ "401", "HHMM" },
		{ "402", "HHMMSS" },
		{ "404", "HHMMSSZZZ" },
		{ "405", "MMMMSS" },
		{ "501", "HHMMHHMM" },
		{ "502", "HHMMSS-HHMMSS" },
		{ "503", "HHMMSSZZZ-HHMMSSZZZ" },
		{ "600", "CC" },
		{ "601", "YY" },
		{ "602", "CCYY" },
		{ "603", "YYS" },
		{ "604", "CCYYS" },
		{ "608", "CCYYQ" },
		{ "609", "YYMM" },
		{ "610", "CCYYMM" },
		{ "613", "YYMMA" },
		{ "614", "CCYYMMA" },
		{ "615", "YYWW" },
		{ "616", "CCYYWW" },
		{ "701", "YY-YY" },
		{ "702", "CCYY-CCYY" },
		{ "703", "YYS-YYS" },
		{ "704", "CCYYS-CCYYS" },
		{ "705", "YYPYYP" },
		{ "706", "CCYYP-CCYYP" },
		{ "707", "YYQ-YYQ" },
		{ "708", "CCYYQ-CCYYQ" },
		{ "709", "YYMM-YYMM" },
		{ "710", "CCYYMM-CCYYMM" },
		{ "711", "CCYYMMDD-CCYYMMDD" },
		{ "713", "YYMMDDHHMM-YYMMDDHHMM" },
		{ "715", "YYWW-YYWW" },
		{ "716", "CCYYWW-CCYYWW" },
		{ "717", "YYMMDD-YYMMDD" },
		{ "718", "CCYYMMDD-CCYYMMDD" },
		{ "801", "Year" },
		{ "802", "Month" },
		{ "803", "Week" },
		{ "804", "Day" },
		{ "805", "Hour" },
		{ "806", "Minute" },
		{ "807", "Second" },
		{ "808", "Semester" },
		{ "809", "Four months period" },
		{ "810", "Trimester" },
		{ "811", "Half month" },
		{ "812", "Ten days" },
		{ "813", "Day of the week" },
		{ "814", "Working days" },
	};
}