using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Payment time reference, coded
/// </summary>
public class PaymentTimeReferenceCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PaymentTimeReferenceCoded(string s) => new PaymentTimeReferenceCoded { Code = s };

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
		{ "1", "Date of order" },
		{ "2", "Date of confirmation" },
		{ "3", "Date of contract" },
		{ "4", "Date of signature of contract" },
		{ "5", "Date of invoice" },
		{ "6", "Date of credit note" },
		{ "7", "Date of present document" },
		{ "8", "Date of confirmation of order received" },
		{ "9", "Date invoice received" },
		{ "11", "Date credit note received" },
		{ "12", "Date present document received" },
		{ "13", "Date of resale by buyer" },
		{ "14", "Date proceeds of resale collected by buyer" },
		{ "21", "Date goods received by buyer" },
		{ "22", "Date goods received by buyer's agent" },
		{ "23", "Date goods received by carrier" },
		{ "24", "Date ex-works" },
		{ "25", "Date goods handed over for shipment by seller or agent" },
		{ "26", "Date of arrival of transport" },
		{ "27", "Date of outward frontier crossing" },
		{ "28", "Date of inward frontier crossing" },
		{ "29", "Date of delivery of goods to establishments/domicile/site" },
		{ "31", "Stipulated date for payment of documentary credit" },
		{ "32", "Stipulated date for acceptance of documentary credit" },
		{ "33", "Stipulated date for negotiation of documentary credit" },
		{ "41", "Date of delivery to buyer of documents representing goods" },
		{ "42", "Date of delivery to buyer's agent of documents representing goods" },
		{ "43", "Date of delivery to carrier of documents representing goods" },
		{ "44", "Date of delivery to intermediary bank of documents representing good" },
		{ "45", "Date of bill of lading, consignment note or other transport document" },
		{ "46", "Date of receipt for loading (mate's receipt)" },
		{ "47", "Date of negotiable instrument (draft, promissory note, bank)" },
		{ "48", "Date of receipt of tool dependent initial samples plus unlimited absolute bank guarantee plus value added tax" },
		{ "52", "Due date of negotiable instrument" },
		{ "53", "Date of presentation of negotiable instrument" },
		{ "54", "Date of acceptance of negotiable instrument" },
		{ "55", "Date of acceptance of tooling" },
		{ "56", "Date of receipt of tooling" },
		{ "57", "Date of acceptance of first samples produced under production conditions" },
		{ "60", "Date of start of work" },
		{ "61", "Date of end of work" },
		{ "62", "Date of provisional reception of work" },
		{ "63", "Date of final acceptance of work" },
		{ "64", "Date of certificate of preliminary acceptance" },
		{ "65", "Date of certificate of final acceptance" },
		{ "66", "Specified date" },
		{ "67", "Anticipated delivery date" },
		{ "68", "Effective date" },
		{ "69", "Invoice transmission date" },
		{ "70", "Date of issue of transport document(s)" },
		{ "71", "Date of presentation of documents" },
		{ "72", "Payment date" },
		{ "73", "Draft(s) at ... days sight" },
		{ "74", "Draft(s) at ... days date" },
		{ "75", "Draft(s) at ... days after date of issuance of transport document(s)" },
		{ "76", "Draft(s) at ... days after date of presentation of documents" },
		{ "77", "Specified draft date" },
		{ "78", "Customs clearance date (import)" },
		{ "79", "Customs clearance date (export)" },
		{ "80", "Date of salary payment" },
		{ "81", "Date of shipment as evidenced by the transport document(s)" },
		{ "ZZZ", "Other reference date agreed upon between the parties" },
	};
}