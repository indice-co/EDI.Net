using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Equipment qualifier
/// </summary>
public class EquipmentQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator EquipmentQualifier(string s) => new EquipmentQualifier { Code = s };

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
		{ "AA", "Ground equipment" },
		{ "AB", "Chain" },
		{ "AD", "Temperature recorder" },
		{ "AE", "Body trailer" },
		{ "AG", "Slipsheet" },
		{ "AH", "No special equipment needed" },
		{ "AI", "Vessel hold" },
		{ "BL", "Blocks" },
		{ "BR", "Barge" },
		{ "BX", "Boxcar" },
		{ "CH", "Chassis" },
		{ "CN", "Container" },
		{ "LU", "Load/unload device on equipment" },
		{ "PA", "Pallet" },
		{ "PL", "Platform" },
		{ "RF", "Flat car" },
		{ "RG", "Reefer generator" },
		{ "RO", "Rope" },
		{ "RR", "Rail car" },
		{ "SW", "Swap body" },
		{ "TE", "Trailer" },
		{ "TP", "Tarpaulin" },
		{ "TS", "Tackles" },
		{ "UL", "ULD (Unit load device)" },
		{ "BPN", "Box pallet non exchangeable" },
		{ "BPY", "Box pallet EUR Y non exchangeable" },
		{ "DPA", "Deadlight (panel)" },
		{ "EFP", "Exchangeable EUR flat pallet" },
		{ "EYP", "Exchangeable EUR Y box pallet" },
		{ "FPN", "Flat pallet EUR non exchangeable" },
		{ "FPR", "Flat pallet (railway property) non exchangeable" },
		{ "FSU", "Forked support" },
		{ "LAR", "Lashing rope" },
		{ "MPA", "Movable panel" },
		{ "PBP", "Identified private box pallet" },
		{ "PFP", "Identified private flat pallet" },
		{ "PPA", "Protecting panel" },
		{ "PST", "Portable stove" },
		{ "RGF", "Ground facility" },
		{ "SCA", "Small container category A" },
		{ "SCB", "Small container category B" },
		{ "SCC", "Small container category C" },
		{ "SFA", "Stiffening ring of frame" },
		{ "SPP", "Identified special pallet" },
		{ "STR", "Strap" },
		{ "TSU", "Tarpaulin support" },
	};
}