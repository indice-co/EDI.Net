using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Measurement attribute identification
/// </summary>
public class MeasurementAttributeIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MeasurementAttributeIdentification(string s) => new MeasurementAttributeIdentification { Code = s };

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
		{ "1", "Clear" },
		{ "2", "Hazy" },
		{ "3", "Excess" },
		{ "4", "Some" },
		{ "5", "Undetectable" },
		{ "6", "Trace" },
		{ "7", "Yes" },
		{ "8", "Closed" },
		{ "9", "Passed" },
		{ "10", "Present" },
		{ "11", "Gel" },
		{ "12", "OK" },
		{ "13", "Slight" },
		{ "14", "No Good" },
		{ "15", "Marginal" },
		{ "16", "Nil" },
		{ "18", "Open" },
		{ "19", "Free" },
		{ "20", "No" },
		{ "21", "Checked" },
		{ "22", "Fail" },
		{ "23", "Absent" },
		{ "24", "Good" },
		{ "25", "Fair" },
		{ "26", "Poor" },
		{ "27", "Excellent" },
		{ "28", "Bright" },
		{ "29", "To be determined" },
		{ "32", "Conditional, free" },
		{ "33", "Balance" },
		{ "34", "Complete" },
		{ "35", "Low" },
		{ "36", "Not applicable" },
		{ "37", "Not determined" },
		{ "38", "Negligible" },
		{ "39", "Moderate" },
		{ "40", "Appreciable" },
		{ "41", "Not available" },
		{ "42", "Uncontrolled temperature" },
		{ "43", "Chilled" },
		{ "44", "Frozen" },
		{ "45", "Temperature controlled" },
	};
}