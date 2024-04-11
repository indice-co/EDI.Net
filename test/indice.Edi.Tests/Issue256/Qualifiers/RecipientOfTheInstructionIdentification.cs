using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Recipient of the instruction identification
/// </summary>
public class RecipientOfTheInstructionIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator RecipientOfTheInstructionIdentification(string s) => new RecipientOfTheInstructionIdentification { Code = s };

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
		{ "4", "Beneficiary" },
		{ "5", "Contact party 1" },
		{ "6", "Contact party 2" },
		{ "7", "Contact party 3" },
		{ "8", "Contact party 4" },
		{ "9", "Contact bank 1" },
		{ "10", "Contact bank 2" },
	};
}