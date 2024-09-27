using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Contact function, coded
/// </summary>
public class ContactFunctionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ContactFunctionCoded(string s) => new ContactFunctionCoded { Code = s };

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
		{ "AA", "Insurance contact" },
		{ "AB", "Workshop contact" },
		{ "AC", "Accepting contact" },
		{ "AD", "Accounting contact" },
		{ "AE", "Contract contact" },
		{ "AF", "Land registry contact" },
		{ "AG", "Agent" },
		{ "AH", "Coordination contact" },
		{ "AI", "Project management contact" },
		{ "AJ", "Investment contact" },
		{ "AK", "Works management contact" },
		{ "AL", "Personnel contact" },
		{ "AM", "Claims contact" },
		{ "AN", "Laboratory contact" },
		{ "AO", "Plant/equipment contact" },
		{ "AP", "Accounts payable contact" },
		{ "AQ", "Quantity surveyor contact" },
		{ "AR", "Accounts receivable contact" },
		{ "AS", "Public relations contact" },
		{ "AT", "Technical contact" },
		{ "AU", "City works authority contact" },
		{ "AV", "Maintenance contact" },
		{ "AW", "Town planning contact" },
		{ "AX", "Traffic authority contact" },
		{ "AY", "Electricity supply contact" },
		{ "AZ", "Gas supply contact" },
		{ "BA", "Water supply contact" },
		{ "BB", "Telecommunications network contact" },
		{ "BC", "Banking contact" },
		{ "BD", "New developments contact" },
		{ "BE", "Transport infrastructure authority" },
		{ "BF", "Service contact" },
		{ "BU", "Ultimate consignee" },
		{ "CA", "Carrier" },
		{ "CB", "Changed by" },
		{ "CC", "Responsible person for information production" },
		{ "CD", "Responsible person for information dissemination" },
		{ "CE", "Head of unit for computer data processing" },
		{ "CF", "Head of unit for information production" },
		{ "CG", "Head of unit for information dissemination" },
		{ "CN", "Consignee" },
		{ "CO", "Consignor" },
		{ "CP", "Responsible person for computer data processing" },
		{ "CR", "Customer relations" },
		{ "CW", "Confirmed with" },
		{ "DE", "Department/employee to execute export procedures" },
		{ "DI", "Department/employee to execute import procedures" },
		{ "DL", "Delivery contact" },
		{ "EB", "Entered by" },
		{ "EC", "Education coordinator" },
		{ "ED", "Engineering contact" },
		{ "EX", "Expeditor" },
		{ "GR", "Goods receiving contact" },
		{ "HE", "Emergency dangerous goods contact" },
		{ "HG", "Dangerous goods contact" },
		{ "HM", "Hazardous material contact" },
		{ "IC", "Information contact" },
		{ "IN", "Insurer contact" },
		{ "LB", "Place of delivery contact" },
		{ "LO", "Place of collection contact" },
		{ "MC", "Material control contact" },
		{ "MD", "Material disposition contact" },
		{ "MH", "Material handling contact" },
		{ "MR", "Message recipient contact" },
		{ "MS", "Message sender contact" },
		{ "NT", "Notification contact" },
		{ "OC", "Order contact" },
		{ "PA", "Prototype coordinator" },
		{ "PD", "Purchasing contact" },
		{ "PE", "Payee contact" },
		{ "PM", "Product management contact" },
		{ "QA", "Quality assurance contact" },
		{ "QC", "Quality coordinator contact" },
		{ "RD", "Receiving dock contact" },
		{ "SA", "Sales administration" },
		{ "SC", "Schedule contact" },
		{ "SD", "Shipping contact" },
		{ "SR", "Sales representative or department" },
		{ "SU", "Supplier contact" },
		{ "TA", "Traffic administrator" },
		{ "TD", "Test contact" },
		{ "TI", "Technical documentation recipient" },
		{ "TR", "Transport contact" },
		{ "WH", "Warehouse" },
		{ "ZZZ", "Mutually defined" },
	};
}