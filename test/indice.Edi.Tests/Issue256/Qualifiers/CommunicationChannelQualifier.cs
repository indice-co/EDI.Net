using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Communication channel qualifier
/// </summary>
public class CommunicationChannelQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CommunicationChannelQualifier(string s) => new CommunicationChannelQualifier { Code = s };

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
		{ "AA", "Circuit switching" },
		{ "AB", "SITA" },
		{ "AC", "ARINC" },
		{ "AD", "AT&T mailbox" },
		{ "AE", "Peripheral device" },
		{ "AF", "U.S. Defense Switched Network" },
		{ "AG", "U.S. federal telecommunications system" },
		{ "CA", "Cable address" },
		{ "EI", "EDI transmission" },
		{ "EM", "Electronic mail" },
		{ "EX", "Extension" },
		{ "FT", "File transfer access method" },
		{ "FX", "Telefax" },
		{ "GM", "GEIS (General Electric Information Service) mailbox" },
		{ "IE", "IBM information exchange" },
		{ "IM", "Internal mail" },
		{ "MA", "Mail" },
		{ "PB", "Postbox number" },
		{ "PS", "Packet switching" },
		{ "SW", "S.W.I.F.T." },
		{ "TE", "Telephone" },
		{ "TG", "Telegraph" },
		{ "TL", "Telex" },
		{ "TM", "Telemail" },
		{ "TT", "Teletext" },
		{ "TX", "TWX" },
		{ "XF", "X.400" },
	};
}