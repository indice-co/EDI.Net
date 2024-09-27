using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Status, coded
/// </summary>
public class StatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator StatusCoded(string s) => new StatusCoded { Code = s };

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
		{ "1", "To be done" },
		{ "2", "Done" },
		{ "3", "Passed on" },
		{ "4", "Final" },
		{ "5", "Subject to final payment" },
		{ "6", "Minimum" },
		{ "7", "Fixed" },
		{ "8", "Maximum" },
		{ "9", "Information" },
		{ "10", "0 day available" },
		{ "11", "1 day available" },
		{ "12", "2 days available" },
		{ "13", "3 days available" },
		{ "14", "Uncollected funds" },
		{ "15", "Nil" },
		{ "16", "None advised" },
		{ "17", "Requested" },
		{ "18", "Free of charge" },
		{ "19", "Rounded" },
		{ "20", "Permanent" },
		{ "21", "Temporary" },
		{ "22", "Subject to agreed condition" },
		{ "23", "Added" },
		{ "24", "Deducted" },
		{ "25", "Included" },
	};
}