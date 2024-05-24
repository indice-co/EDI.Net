using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the temperature setting.
/// </summary>
[EdiSegment, EdiPath("TMP")]
public class TMP
{
	/// <summary>
	/// A code giving specific meaning to the temperature.
	/// </summary>
	[EdiValue("X(3)", Path = "TMP/0", Mandatory = true)]
	public TemperatureQualifier? TemperatureQualifier { get; set; }

	/// <summary>
	/// The temperature under which the goods are (to be) stored or shipped.
	/// </summary>
	[EdiPath("TMP/1")]
	public TMP_TemperatureSetting? TemperatureSetting { get; set; }
}

/// <summary>
/// The temperature under which the goods are (to be) stored or shipped.
/// </summary>
[EdiElement]
public class TMP_TemperatureSetting
{
	/// <summary>
	/// The actual temperature value in degrees Celsius (e.g. 008, 020).
	/// </summary>
	[EdiValue("9(3)", Path = "TMP/*/0", Mandatory = false)]
	public decimal? TemperatureSetting { get; set; }

	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "TMP/*/1", Mandatory = false)]
	public string? MeasureUnitQualifier { get; set; }
}