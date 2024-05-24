using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Measurement purpose qualifier
/// </summary>
public class MeasurementPurposeQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MeasurementPurposeQualifier(string s) => new MeasurementPurposeQualifier { Code = s };

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
		{ "CH", "Chemistry" },
		{ "CN", "Core notch dimensions" },
		{ "CS", "Core size" },
		{ "CT", "Counts" },
		{ "DR", "Decision result value" },
		{ "DT", "Dimensional tolerance" },
		{ "DV", "Discrete measurement value" },
		{ "DX", "Dimension used in price extension" },
		{ "EN", "Environmental conditions" },
		{ "FO", "Footage" },
		{ "IV", "Interpolated value" },
		{ "LC", "Limited weight/size coils" },
		{ "LL", "Lift limitation" },
		{ "PC", "Parting cut (sawcut)" },
		{ "PD", "Physical dimensions (product ordered)" },
		{ "PL", "Package limitations" },
		{ "RL", "Receiving facility limitations" },
		{ "RN", "Length limitations" },
		{ "SE", "Property specification" },
		{ "SH", "Shipping tolerance" },
		{ "SM", "Shade" },
		{ "SO", "Storage limitation" },
		{ "SR", "Surface roughness" },
		{ "ST", "Surface treatment" },
		{ "SU", "Surface" },
		{ "SV", "Specification value" },
		{ "TE", "Temperature" },
		{ "TL", "Transportation equipment limitations" },
		{ "TR", "Test result" },
		{ "TX", "Time used in price extension" },
		{ "VO", "Observed value" },
		{ "VT", "True value" },
		{ "WT", "Weights" },
		{ "WX", "Weight used in price extension" },
		{ "AAA", "Line item measurement" },
		{ "AAB", "Retail container dimension" },
		{ "AAC", "Retail container size" },
		{ "AAD", "Other US Government agency application" },
		{ "AAE", "Measurement" },
		{ "AAF", "Customs line item measurement" },
		{ "AAG", "Percentage of alcohol (by volume)" },
		{ "AAH", "Dimensions total weight" },
		{ "AAI", "Item weight" },
		{ "AAJ", "Visa quantity" },
		{ "AAK", "Licence (quantity deducted)" },
		{ "AAL", "Cargo loaded" },
		{ "AAM", "Cargo discharged" },
		{ "AAN", "Weight of conveyance" },
		{ "AAO", "Conveyance summer dead weight" },
		{ "AAP", "Containerized cargo on vessel's weight" },
		{ "AAQ", "Non-containerized cargo on vessel's weight" },
		{ "AAR", "1st specified tariff quantity" },
		{ "AAS", "2nd specified tariff quantity" },
		{ "AAT", "3rd specified tariff quantity" },
		{ "AAU", "Package" },
		{ "AAV", "Person" },
		{ "AAW", "Accuracy" },
		{ "AAX", "Consignment measurement" },
		{ "AAY", "Package measurement" },
		{ "AAZ", "Handling unit measurement" },
		{ "ABA", "Unit of measure used for ordered quantities" },
		{ "ABB", "Colour" },
		{ "ABC", "Size" },
		{ "ABD", "Length" },
		{ "ABE", "Height" },
		{ "ABF", "Width" },
		{ "ABG", "Diameter" },
		{ "ABH", "Depth" },
		{ "ABI", "Ventilation" },
		{ "ABJ", "Original unit of issue" },
		{ "ABK", "External dimension" },
		{ "ABL", "Internal dimension" },
		{ "ABM", "Test piece dimensions" },
		{ "ABN", "Average reading" },
		{ "ABP", "Unit of measure per unit of issue" },
		{ "ASW", "Weight ascertained" },
		{ "CHW", "Chargeable weight" },
		{ "DEN", "Density" },
		{ "EGW", "Estimated gross weight" },
		{ "EVO", "Estimated volume" },
		{ "LAO", "Vessel overall length" },
		{ "LGL", "Length limitations" },
		{ "LMT", "Loading meters" },
		{ "NAX", "Number of axles" },
		{ "PAL", "Payload" },
		{ "PLL", "Platform limitation" },
		{ "SPG", "Specific gravity" },
		{ "VOL", "Volume" },
	};
}