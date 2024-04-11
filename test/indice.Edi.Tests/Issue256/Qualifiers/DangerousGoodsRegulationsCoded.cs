using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Dangerous goods regulations, coded
/// </summary>
public class DangerousGoodsRegulationsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DangerousGoodsRegulationsCoded(string s) => new DangerousGoodsRegulationsCoded { Code = s };

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
		{ "UI", "UK IMO book" },
		{ "ADR", "European agreement on the international carriage of dangerous goods on road (ADR)" },
		{ "AGS", "DE, ADR and GGVS combined regulations for combined transport" },
		{ "ANR", "ADNR, Autorisation de transport de matieres Dangereuses pour la Navigation sur le Rhin" },
		{ "ARD", "DE, ARD and RID - Combined regulations for combined transport" },
		{ "CFR", "49 code of federal regulations" },
		{ "COM", "DE, ADR, RID, GGVS and GGVE - Combined regulations for combined transport" },
		{ "GVE", "DE, GGVE (Gefahrgutverordnung Eisenbahn)" },
		{ "GVS", "DE, GGVS (Gefahrgutverordnung Strasse)" },
		{ "ICA", "IATA ICAO" },
		{ "IMD", "IMO IMDG code" },
		{ "RGE", "DE, RID and GGVE, Combined regulations for combined transport on rails" },
		{ "RID", "Railroad dangerous goods book (RID)" },
		{ "TEC", "Transport emergency trem card" },
		{ "ZZZ", "Mutually defined" },
	};
}