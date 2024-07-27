using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Party enacting instruction identification
/// </summary>
public class PartyEnactingInstructionIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PartyEnactingInstructionIdentification(string s) => new PartyEnactingInstructionIdentification { Code = s };

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
		{ "1", "Applicant's bank" },
		{ "2", "Issuing bank" },
		{ "3", "Beneficiary's bank" },
		{ "4", "Buyer" },
		{ "5", "Seller" },
	};
}