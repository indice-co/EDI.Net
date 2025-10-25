using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Terms of payment identification
/// </summary>
public class TermsOfPaymentIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TermsOfPaymentIdentification(string s) => new TermsOfPaymentIdentification { Code = s };

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
		{ "1", "Draft(s) drawn on issuing bank" },
		{ "2", "Draft(s) drawn on advising bank" },
		{ "3", "Draft(s) drawn on reimbursing bank" },
		{ "4", "Draft(s) drawn on applicant" },
		{ "5", "Draft(s) drawn on any other drawee" },
		{ "6", "No drafts" },
	};
}