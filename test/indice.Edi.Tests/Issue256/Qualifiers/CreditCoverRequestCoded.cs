using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Credit cover request, coded
/// </summary>
public class CreditCoverRequestCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CreditCoverRequestCoded(string s) => new CreditCoverRequestCoded { Code = s };

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
		{ "1", "Multi-currency credit cover" },
		{ "2", "Request new limit" },
		{ "3", "Request increased limit" },
		{ "4", "Request for preliminary assessment" },
		{ "5", "Request for new account only" },
	};
}