using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Response type, coded
/// </summary>
public class ResponseTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ResponseTypeCoded(string s) => new ResponseTypeCoded { Code = s };

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
		{ "AA", "Debit advice" },
		{ "AB", "Message acknowledgement" },
		{ "AC", "Acknowledge - with detail and change" },
		{ "AD", "Acknowledge - with detail, no change" },
		{ "AF", "Debit advice/message acknowledgement" },
		{ "AG", "Authentication" },
		{ "AI", "Acknowledge only changes" },
		{ "AJ", "Pending" },
		{ "AP", "Accepted" },
		{ "CA", "Conditionally accepted" },
		{ "CO", "Confirmation of measurements" },
		{ "NA", "No acknowledgement needed" },
		{ "RE", "Rejected" },
	};
}