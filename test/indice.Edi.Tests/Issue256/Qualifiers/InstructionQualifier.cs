using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Instruction qualifier
/// </summary>
public class InstructionQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator InstructionQualifier(string s) => new InstructionQualifier { Code = s };

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
		{ "1", "Action required" },
		{ "2", "Party instructions" },
		{ "3", "Maximum value exceeded instructions" },
		{ "4", "Confirmation instructions" },
		{ "5", "Method of issuance" },
		{ "6", "Pre-advice instructions" },
		{ "7", "Documents delivery instruction" },
		{ "8", "Additional terms and/or conditions documentary credit" },
		{ "9", "Investment instruction" },
	};
}