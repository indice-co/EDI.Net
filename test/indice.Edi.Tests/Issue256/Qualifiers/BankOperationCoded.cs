using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Bank operation, coded
/// </summary>
public class BankOperationCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator BankOperationCoded(string s) => new BankOperationCoded { Code = s };

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
		{ "ABX", "Automated bills of exchange" },
		{ "BEX", "Bills of exchange" },
		{ "BGI", "Bankgiro" },
		{ "BKD", "Bank draft" },
		{ "BKI", "Bank initiated" },
		{ "CAL", "Cash letter" },
		{ "CHG", "Charges" },
		{ "CHI", "Cheque international" },
		{ "CHN", "Cheque national" },
		{ "CLR", "Clearing" },
		{ "COL", "Collection" },
		{ "COM", "Commission" },
		{ "CON", "Cash concentration" },
		{ "CPP", "Cash payment by post" },
		{ "CUX", "Currencies" },
		{ "DDT", "Direct debit" },
		{ "DEP", "Deposit cash operation" },
		{ "FEX", "Foreign exchange" },
		{ "FGI", "Free format giro" },
		{ "INT", "Interest" },
		{ "LOC", "Letter of credit" },
		{ "LOK", "Lockbox" },
		{ "MSC", "Miscellaneous" },
		{ "PAC", "Payment card" },
		{ "PGI", "Postgiro" },
		{ "POS", "Point of sale" },
		{ "REC", "Returned cheques" },
		{ "RET", "Returned items" },
		{ "RGI", "Reference giro" },
		{ "RTR", "Returned transfers" },
		{ "SEC", "Securities" },
		{ "STO", "Standing order" },
		{ "TCK", "Travellers cheque" },
		{ "TRF", "Transfer" },
		{ "UGI", "Urgent giro" },
		{ "VDA", "Value date adjustment" },
		{ "WDL", "Withdrawal cash operation" },
		{ "ZZZ", "Mutually defined" },
	};
}