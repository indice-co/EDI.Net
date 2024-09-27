using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Delivery requirements, coded
/// </summary>
public class DeliveryRequirementsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DeliveryRequirementsCoded(string s) => new DeliveryRequirementsCoded { Code = s };

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
		{ "AA", "Ship on authorization" },
		{ "BK", "Ship partial - balance back order" },
		{ "CD", "Cancel if not delivered by date" },
		{ "DA", "Do not deliver after" },
		{ "DB", "Do not deliver before" },
		{ "DD", "Deliver on date" },
		{ "IS", "Substitute item" },
		{ "P1", "No schedule established" },
		{ "P2", "Ship as soon as possible" },
		{ "SC", "Ship complete order" },
		{ "SF", "Ship partial, if no freight rate increase" },
		{ "SP", "Ship partial - balance cancel" },
	};
}