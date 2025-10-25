using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a range.
/// </summary>
[EdiSegment, EdiPath("RNG")]
public class RNG
{
	/// <summary>
	/// Identification of the type of range.
	/// </summary>
	[EdiValue("X(3)", Path = "RNG/0", Mandatory = true)]
	public RangeTypeQualifier? RangeTypeQualifier { get; set; }

	/// <summary>
	/// Range minimum and maximum limits.
	/// </summary>
	[EdiPath("RNG/1")]
	public RNG_Range? Range { get; set; }
}

/// <summary>
/// Range minimum and maximum limits.
/// </summary>
[EdiElement]
public class RNG_Range
{
	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "RNG/*/0", Mandatory = true)]
	public string? MeasureUnitQualifier { get; set; }

	/// <summary>
	/// Minimum of a range.
	/// </summary>
	[EdiValue("9(18)", Path = "RNG/*/1", Mandatory = false)]
	public decimal? RangeMinimum { get; set; }

	/// <summary>
	/// Maximum of a range.
	/// </summary>
	[EdiValue("9(18)", Path = "RNG/*/2", Mandatory = false)]
	public decimal? RangeMaximum { get; set; }
}