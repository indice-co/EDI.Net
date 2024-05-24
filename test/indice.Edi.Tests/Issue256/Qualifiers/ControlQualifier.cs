using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Control qualifier
/// </summary>
public class ControlQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ControlQualifier(string s) => new ControlQualifier { Code = s };

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
		{ "1", "Algebraic total of the quantity values in line items in a message" },
		{ "2", "Number of line items in message" },
		{ "3", "Number of line and sub items in message" },
		{ "4", "Number of invoice lines" },
		{ "5", "Number of Customs item detail lines" },
		{ "6", "Number of Customs entries" },
		{ "7", "Total gross weight" },
		{ "8", "Total pieces" },
		{ "9", "Total number of ULD (Unit Load Device)" },
		{ "10", "Total number of consignments" },
		{ "11", "Total number of packages" },
		{ "12", "Invoice total amount" },
		{ "13", "Number of loading lists" },
		{ "14", "Number of Customs commercial detail lines" },
		{ "15", "Total consignment, cube" },
		{ "16", "Total number of equipment" },
		{ "17", "Declared total Customs value" },
		{ "18", "Total reported quantity in net weight" },
		{ "19", "Total reported quantity in supplementary units" },
		{ "20", "Total reported invoice(s) value" },
		{ "21", "Total reported ancillary costs" },
		{ "22", "Total reported statistical value" },
		{ "23", "Total ordered quantity" },
		{ "24", "Number of orders referenced in this message" },
		{ "25", "Number of rejected order lines" },
		{ "26", "Total gross measurement/cube" },
		{ "27", "Total number of credit items given for control purposes" },
		{ "28", "Total number of debit items given for control purposes" },
		{ "29", "Total net weight of consignment" },
		{ "30", "Total number of empty containers" },
		{ "31", "Number of messages" },
		{ "32", "Total gross weight of the goods within the means of transport" },
		{ "33", "Total number of original Bills of Lading" },
		{ "34", "Total number of copy Bills of Lading" },
		{ "35", "Number of containers to be discharged" },
		{ "36", "Number of containers to be loaded" },
		{ "37", "Number of containers to be restowed" },
		{ "38", "Number of containers to be shifted" },
	};
}