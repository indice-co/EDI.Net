using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Transit direction, coded
/// </summary>
public class TransitDirectionCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TransitDirectionCoded(string s) => new TransitDirectionCoded { Code = s };

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
		{ "BS", "Buyer to seller" },
		{ "SB", "Seller to buyer" },
		{ "SC", "Subcontractor to seller" },
		{ "SD", "Seller to drop ship designated location" },
		{ "SF", "Seller to freight forwarder" },
		{ "SS", "Seller to subcontractor" },
		{ "ZZZ", "Mutually defined" },
	};
}