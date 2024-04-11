using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Business function, coded
/// </summary>
public class BusinessFunctionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator BusinessFunctionCoded(string s) => new BusinessFunctionCoded { Code = s };

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
		{ "ADV", "Advance payment" },
		{ "AGT", "Agricultural transfer" },
		{ "AMY", "Alimony" },
		{ "BEC", "Child benefit" },
		{ "BEN", "Unemployment/disability/benefit" },
		{ "BON", "Bonus" },
		{ "CAS", "Cash management transfer" },
		{ "CBF", "Capital building fringe fortune" },
		{ "CDT", "Commodity transfer" },
		{ "COC", "Commercial credit" },
		{ "COM", "Commission" },
		{ "COS", "Costs" },
		{ "CPY", "Copyright" },
		{ "DIV", "Dividend" },
		{ "FEX", "Foreign exchange" },
		{ "GDS", "Purchase and sale of goods" },
		{ "GVT", "Government payment" },
		{ "IHP", "Instalment/hire-purchase agreement" },
		{ "INS", "Insurance premium" },
		{ "INT", "Interest" },
		{ "LIF", "Licence fees" },
		{ "LOA", "Loan" },
		{ "LOR", "Loan repayment" },
		{ "NET", "Netting" },
		{ "PEN", "Pension" },
		{ "REF", "Refund" },
		{ "REN", "Rent" },
		{ "ROY", "Royalties" },
		{ "SAL", "Salary" },
		{ "SCV", "Purchase and sale of services" },
		{ "SEC", "Securities" },
		{ "SSB", "Social security benefit" },
		{ "SUB", "Subscription" },
		{ "TAX", "Tax payment" },
		{ "VAT", "Value added tax payment" },
		{ "ZZZ", "Mutually defined" },
	};
}