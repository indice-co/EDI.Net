using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Identity number qualifier
/// </summary>
public class IdentityNumberQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator IdentityNumberQualifier(string s) => new IdentityNumberQualifier { Code = s };

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
		{ "AA", "House bill of lading" },
		{ "AB", "1st structure element name" },
		{ "AC", "2nd structure element name" },
		{ "AD", "3rd structure element name" },
		{ "AE", "4th structure element name" },
		{ "AF", "5th structure element name" },
		{ "AG", "6th structure element name" },
		{ "AH", "7th structure element name" },
		{ "AI", "8th structure element name" },
		{ "AJ", "9th structure element name" },
		{ "AK", "Data set" },
		{ "AL", "Kanban card number" },
		{ "AM", "Level number" },
		{ "AN", "Manufacturing reference number" },
		{ "AO", "Position number in package" },
		{ "AP", "Product" },
		{ "AQ", "Release number" },
		{ "AR", "Statistical concept" },
		{ "AS", "Table" },
		{ "AT", "Transport packing group number" },
		{ "AU", "Value list" },
		{ "AV", "Value list subset" },
		{ "AW", "Serial shipping container code" },
		{ "AX", "Case number" },
		{ "AY", "Financial security identification number" },
		{ "AZ", "Compact disk player security code number" },
		{ "BA", "Question in questionnaire" },
		{ "BB", "Questionnaire" },
		{ "BC", "Check digit" },
		{ "BD", "Vehicle telephone identification number" },
		{ "BE", "Batch excluded" },
		{ "BF", "Door key number" },
		{ "BG", "Fleet number" },
		{ "BH", "Ignition key number" },
		{ "BI", "Radio security code number" },
		{ "BJ", "Serial shipping container code" },
		{ "BK", "Fleet vehicle unit number" },
		{ "BL", "Vehicle registration number" },
		{ "BM", "Accounting Classification Reference Number (ACRN)" },
		{ "BN", "Serial number" },
		{ "BO", "Fund" },
		{ "BP", "Special Accounting Classification Reference Number (ACRN)" },
		{ "BQ", "Project" },
		{ "BR", "Transportation Account Code (TAC)" },
		{ "BS", "Financial details" },
		{ "BT", "Account manager" },
		{ "BX", "Batch number" },
		{ "CN", "Chassis number" },
		{ "EE", "Engine number" },
		{ "EM", "Emulsion number" },
		{ "IL", "Invoice line number" },
		{ "ML", "Marking/label number" },
		{ "PN", "Part number" },
		{ "SC", "Secondary Customs tariff number" },
		{ "VV", "Vehicle identity number" },
	};
}