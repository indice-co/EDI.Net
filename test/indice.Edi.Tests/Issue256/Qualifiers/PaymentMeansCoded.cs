using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Payment means, coded
/// </summary>
public class PaymentMeansCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PaymentMeansCoded(string s) => new PaymentMeansCoded { Code = s };

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
		{ "1", "Instrument not defined" },
		{ "2", "Automated clearing house credit" },
		{ "3", "Automated clearing house debit" },
		{ "4", "ACH demand debit reversal" },
		{ "5", "ACH demand credit reversal" },
		{ "6", "ACH demand credit" },
		{ "7", "ACH demand debit" },
		{ "8", "Hold" },
		{ "9", "National or regional clearing" },
		{ "10", "In cash" },
		{ "11", "ACH savings credit reversal" },
		{ "12", "ACH savings debit reversal" },
		{ "13", "ACH savings credit" },
		{ "14", "ACH savings debit" },
		{ "15", "Bookentry credit" },
		{ "16", "Bookentry debit" },
		{ "17", "ACH demand cash concentration/disbursement (CCD) credit" },
		{ "18", "ACH demand cash concentration/disbursement (CCD) debit" },
		{ "19", "ACH demand corporate trade payment (CTP) credit" },
		{ "20", "Cheque" },
		{ "21", "Banker's draft" },
		{ "22", "Certified banker's draft" },
		{ "23", "Bank cheque (issued by a banking or similar establishment)" },
		{ "25", "Certified cheque" },
		{ "26", "Local cheque" },
		{ "27", "ACH demand corporate trade payment (CTP) debit" },
		{ "28", "ACH demand corporate trade exchange (CTX) credit" },
		{ "29", "ACH demand corporate trade exchange (CTX) debit" },
		{ "30", "Credit transfer" },
		{ "31", "Debit transfer" },
		{ "32", "ACH demand cash concentration/disbursement plus (CCD+) credit" },
		{ "33", "ACH demand cash concentration/disbursement plus (CCD+) debit" },
		{ "34", "ACH prearranged payment and deposit (PPD)" },
		{ "35", "ACH savings cash concentration/disbursement (CCD) credit" },
		{ "36", "ACH savings cash concentration/disbursement (CCD) debit" },
		{ "37", "ACH savings corporate trade payment (CTP) credit" },
		{ "38", "ACH savings corporate trade payment (CTP) debit" },
		{ "39", "ACH savings corporate trade exchange (CTX) credit" },
		{ "40", "ACH savings corporate trade exchange (CTX) debit" },
		{ "41", "ACH savings cash concentration/disbursement plus (CCD+) credit" },
		{ "42", "Payment to bank account" },
		{ "43", "ACH savings cash concentration/disbursement plus (CCD+) debit" },
		{ "50", "Payment by postgiro" },
		{ "60", "Promissory note" },
		{ "61", "Promissory note signed by the debtor" },
		{ "62", "Promissory note signed by the debtor and endorsed by a bank" },
		{ "63", "Promissory note signed by the debtor and endorsed by a third party" },
		{ "64", "Promissory note signed by a bank" },
		{ "65", "Promissory note signed by a bank and endorsed by another bank" },
		{ "66", "Promissory note signed by a third party" },
		{ "67", "Promissory note signed by a third party and endorsed by a bank" },
		{ "70", "Bill drawn by the creditor on the debtor" },
		{ "74", "Bill drawn by the creditor on a bank" },
		{ "75", "Bill drawn by the creditor, endorsed by another bank" },
		{ "76", "Bill drawn by the creditor on a bank and endorsed by a third party" },
		{ "77", "Bill drawn by the creditor on a third party" },
		{ "78", "Bill drawn by creditor on third party, accepted and endorsed by bank" },
		{ "91", "Not transferable banker's draft" },
		{ "92", "Not transferable local cheque" },
		{ "93", "Reference giro" },
		{ "94", "Urgent giro" },
		{ "95", "Free format giro" },
		{ "96", "Requested method for payment was not used" },
		{ "97", "Clearing between partners" },
		{ "ZZZ", "Mutually defined" },
	};
}