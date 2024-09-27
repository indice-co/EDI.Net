using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Measurement significance, coded
/// </summary>
public class MeasurementSignificanceCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MeasurementSignificanceCoded(string s) => new MeasurementSignificanceCoded { Code = s };

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
		{ "3", "Approximately" },
		{ "4", "Equal to" },
		{ "5", "Greater than or equal to" },
		{ "6", "Greater than" },
		{ "7", "Less than" },
		{ "8", "Less than or equal to" },
		{ "10", "Not equal to" },
		{ "11", "Trace" },
		{ "12", "True value" },
		{ "13", "Observed value" },
		{ "15", "Out of range" },
	};
}