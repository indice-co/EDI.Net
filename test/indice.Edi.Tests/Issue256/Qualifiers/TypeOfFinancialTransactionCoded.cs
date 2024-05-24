using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Type of financial transaction, coded
/// </summary>
public class TypeOfFinancialTransactionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TypeOfFinancialTransactionCoded(string s) => new TypeOfFinancialTransactionCoded { Code = s };

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
		{ "1", "Clean payment" },
		{ "4", "Documentary payment" },
		{ "5", "Irrevocable documentary credit" },
		{ "6", "Revocable documentary credit" },
		{ "7", "Irrevocable and transferable documentary credit" },
		{ "8", "Revocable and transferable documentary credit" },
	};
}