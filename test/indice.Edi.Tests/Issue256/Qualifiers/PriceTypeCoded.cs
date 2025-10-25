using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Price type, coded
/// </summary>
public class PriceTypeCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator PriceTypeCoded(string s) => new PriceTypeCoded { Code = s };

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
		{ "AA", "Cancellation price" },
		{ "AB", "Per ton" },
		{ "AC", "Minimum order price" },
		{ "AD", "Export price" },
		{ "AE", "Range dependent price" },
		{ "AI", "Active ingredient" },
		{ "AQ", "As is quantity" },
		{ "CA", "Catalogue" },
		{ "CT", "Contract" },
		{ "CU", "Consumer unit" },
		{ "DI", "Distributor" },
		{ "EC", "ECSC price" },
		{ "NW", "Net weight" },
		{ "PC", "Price catalogue" },
		{ "PE", "Per each" },
		{ "PK", "Per kilogram" },
		{ "PL", "Per litre" },
		{ "PT", "Per tonne" },
		{ "PU", "Specified unit" },
		{ "PV", "Provisional price" },
		{ "PW", "Gross weight" },
		{ "QT", "Quoted" },
		{ "SR", "Suggested retail" },
		{ "TB", "To be negotiated" },
		{ "TU", "Traded unit" },
		{ "TW", "Theoretical weight" },
		{ "WH", "Wholesale" },
	};
}