using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Controlling agency
/// </summary>
public class ControllingAgency
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ControllingAgency(string s) => new ControllingAgency { Code = s };

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
		{ "AA", "EDICONSTRUCT" },
		{ "AB", "DIN (Deutsches Institut fuer Normung)" },
		{ "AC", "ICS (International Chamber of Shipping)" },
		{ "AD", "UPU (Union Postale Universelle)" },
		{ "CC", "CCC (Customs Co-operation Council)" },
		{ "CE", "CEFIC (Conseil Europeen des Federations de l'Industrie Chimique)" },
		{ "EC", "EDICON" },
		{ "ED", "EDIFICE (Electronic industries project)" },
		{ "EE", "EC + EFTA (European Communities and European Free Trade Association)" },
		{ "EN", "EAN (European Article Numbering Association)" },
		{ "ER", "UIC (International Union of railways)" },
		{ "EU", "European Union" },
		{ "EX", "IECC (International Express Carriers Conference)" },
		{ "IA", "IATA (International Air Transport Association)" },
		{ "KE", "KEC (Korea EDIFACT Committee)" },
		{ "LI", "LIMNET" },
		{ "OD", "ODETTE (Organization for Data Exchange through Tele- Transmission in Europe)" },
		{ "RI", "RINET (Reinsurance and Insurance Network)" },
		{ "RT", "UN/ECE/TRADE/WP.4/GE.1/EDIFACT Rapporteurs' Teams" },
		{ "UN", "UN/ECE/TRADE/WP.4" },
	};
}