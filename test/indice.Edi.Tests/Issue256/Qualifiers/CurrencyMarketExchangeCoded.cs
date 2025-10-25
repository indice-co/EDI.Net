using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Currency market exchange, coded
/// </summary>
public class CurrencyMarketExchangeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator CurrencyMarketExchangeCoded(string s) => new CurrencyMarketExchangeCoded { Code = s };

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
		{ "AAA", "Paris exchange" },
		{ "AMS", "Amsterdam exchange" },
		{ "ARG", "Bolsa de Comercio de Buenos Aires" },
		{ "AST", "Australian exchange" },
		{ "AUS", "Wien exchange" },
		{ "BEL", "Brussels exchange" },
		{ "CAN", "Toronto exchange" },
		{ "CAR", "Contractual agreement exchange rate" },
		{ "CIE", "US Customs Information Exchange" },
		{ "DEN", "Copenhagen exchange" },
		{ "ECR", "European Community period exchange rate" },
		{ "FIN", "Helsinki exchange" },
		{ "FRA", "Frankfurt exchange" },
		{ "IMF", "International Monetary Fund" },
		{ "LNF", "London exchange, first closing" },
		{ "LNS", "London exchange, second closing" },
		{ "MIL", "Milan exchange" },
		{ "NOR", "Oslo exchange" },
		{ "NYC", "New York exchange" },
		{ "PHI", "Philadelphia exchange" },
		{ "SRE", "Specific railway exchange currency" },
		{ "SWE", "Stockholm exchange" },
		{ "ZUR", "Zurich exchange" },
	};
}