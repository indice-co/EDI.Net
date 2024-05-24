using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Class of trade, coded
/// </summary>
public class ClassOfTradeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ClassOfTradeCoded(string s) => new ClassOfTradeCoded { Code = s };

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
		{ "AG", "Agency" },
		{ "BG", "Buying group" },
		{ "BR", "Broker" },
		{ "CN", "Consolidator (master distributor)" },
		{ "DE", "Dealer" },
		{ "DI", "Distributor" },
		{ "JB", "Jobber" },
		{ "MF", "Manufacturer" },
		{ "OE", "OEM (Original equipment manufacturer)" },
		{ "RS", "Resale" },
		{ "RT", "Retailer" },
		{ "ST", "Stationer" },
		{ "WH", "Wholesaler" },
		{ "WS", "User" },
	};
}