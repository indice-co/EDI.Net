using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Duty/tax/fee type, coded
/// </summary>
public class DutyTaxFeeTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DutyTaxFeeTypeCoded(string s) => new DutyTaxFeeTypeCoded { Code = s };

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
		{ "AAA", "Petroleum tax" },
		{ "AAB", "Provisional countervailing duty cash" },
		{ "AAC", "Provisional countervailing duty bond" },
		{ "AAD", "Tobacco tax" },
		{ "ADD", "Anti-dumping duty" },
		{ "BOL", "Stamp duty (Imposta di Bollo)" },
		{ "CAP", "Agricultural levy" },
		{ "CAR", "Car tax" },
		{ "COC", "Paper consortium tax (Italy)" },
		{ "CST", "Commodity specific tax" },
		{ "CUD", "Customs duty" },
		{ "CVD", "Countervailing duty" },
		{ "ENV", "Environmental tax" },
		{ "EXC", "Excise duty" },
		{ "EXP", "Agricultural export rebate" },
		{ "FET", "Federal excise tax" },
		{ "FRE", "Free" },
		{ "GCN", "General construction tax" },
		{ "GST", "Goods and services tax" },
		{ "ILL", "Illuminants tax" },
		{ "IMP", "Import tax" },
		{ "IND", "Individual tax" },
		{ "LAC", "Business license fee" },
		{ "LCN", "Local construction tax" },
		{ "LDP", "Light dues payable" },
		{ "LOC", "Local sales taxes" },
		{ "LST", "Lust tax" },
		{ "MCA", "Monetary compensatory amount" },
		{ "MCD", "Miscellaneous cash deposit" },
		{ "OTH", "Other taxes" },
		{ "PDB", "Provisional duty bond" },
		{ "PDC", "Provisional duty cash" },
		{ "PRF", "Preference duty" },
		{ "SCN", "Special construction tax" },
		{ "SSS", "Shifted social securities" },
		{ "STT", "State/provincial sales tax" },
		{ "SUP", "Suspended duty" },
		{ "SUR", "Surtax" },
		{ "SWT", "Shifted wage tax" },
		{ "TAC", "Alcohol mark tax" },
		{ "TOT", "Total" },
		{ "TOX", "Turnover tax" },
		{ "TTA", "Tonnage taxes" },
		{ "VAD", "Valuation deposit" },
		{ "VAT", "Value added tax" },
	};
}