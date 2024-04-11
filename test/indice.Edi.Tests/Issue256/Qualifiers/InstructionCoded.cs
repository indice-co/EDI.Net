using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Instruction, coded
/// </summary>
public class InstructionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator InstructionCoded(string s) => new InstructionCoded { Code = s };

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
		{ "AA", "Send credit note" },
		{ "AB", "Change invoice" },
		{ "AD", "Advise" },
		{ "AE", "Change invoice" },
		{ "AF", "Stop delivery process" },
		{ "AG", "Send replacement" },
		{ "AH", "Pick-up" },
		{ "AI", "Advise by telecommunication" },
		{ "AJ", "Advise by fax" },
		{ "AK", "By registered airmail" },
		{ "AL", "By registered airmail in one set" },
		{ "AM", "By registered airmail in two sets" },
		{ "AP", "Advise by phone" },
		{ "AT", "Advise by telex" },
		{ "CO", "Convert" },
		{ "DA", "Without" },
		{ "DB", "May add" },
		{ "DC", "Confirm" },
		{ "DD", "By registered mail" },
		{ "DE", "By courier service" },
		{ "DF", "By teletransmission" },
		{ "DG", "Preadvice by teletransmission" },
		{ "DH", "By courier service in one set" },
		{ "DI", "By courier service in two sets" },
		{ "DJ", "By registered mail in one set" },
		{ "DK", "By registered mail in two sets" },
		{ "DN", "Per teletransmission" },
		{ "DO", "Advise beneficiary by phone" },
		{ "DP", "Late presentation of documents within D/C validity acceptable" },
		{ "EI", "EDI" },
		{ "EM", "Electronic mail" },
		{ "EX", "Expedite" },
		{ "QC", "Quality control held" },
		{ "QE", "Quality control embargo" },
		{ "RL", "Released" },
		{ "SW", "S.W.I.F.T." },
	};
}