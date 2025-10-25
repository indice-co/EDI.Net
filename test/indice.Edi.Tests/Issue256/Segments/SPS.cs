using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To define the sampling parameters associated with summary statistics reported.
/// </summary>
[EdiSegment, EdiPath("SPS")]
public class SPS
{
	/// <summary>
	/// Number of samples collected per specified unit of measure.
	/// </summary>
	[EdiPath("SPS/0")]
	public SPS_FrequencyDetails? FrequencyDetails { get; set; }

	/// <summary>
	/// A percent value expressing the confidence that a true value falls within a certain confidence interval.
	/// </summary>
	[EdiValue("9(6)", Path = "SPS/1", Mandatory = false)]
	public decimal? ConfidenceLimit { get; set; }

	/// <summary>
	/// Information about the number of observations.
	/// </summary>
	[EdiPath("SPS/2")]
	public SPS_SizeDetails? SizeDetails1 { get; set; }

	/// <summary>
	/// Information about the number of observations.
	/// </summary>
	[EdiPath("SPS/3")]
	public SPS_SizeDetails? SizeDetails2 { get; set; }

	/// <summary>
	/// Information about the number of observations.
	/// </summary>
	[EdiPath("SPS/4")]
	public SPS_SizeDetails? SizeDetails3 { get; set; }

	/// <summary>
	/// Information about the number of observations.
	/// </summary>
	[EdiPath("SPS/5")]
	public SPS_SizeDetails? SizeDetails4 { get; set; }

	/// <summary>
	/// Information about the number of observations.
	/// </summary>
	[EdiPath("SPS/6")]
	public SPS_SizeDetails? SizeDetails5 { get; set; }
}

/// <summary>
/// Number of samples collected per specified unit of measure.
/// </summary>
[EdiElement]
public class SPS_FrequencyDetails
{
	/// <summary>
	/// Indication of the application of a frequency.
	/// </summary>
	[EdiValue("X(3)", Path = "SPS/*/0", Mandatory = true)]
	public FrequencyQualifier? FrequencyQualifier { get; set; }

	/// <summary>
	/// A value indicating a repetitive occurrence.
	/// </summary>
	[EdiValue("9(9)", Path = "SPS/*/1", Mandatory = false)]
	public decimal? FrequencyValue { get; set; }

	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "SPS/*/2", Mandatory = false)]
	public string? MeasureUnitQualifier { get; set; }
}

/// <summary>
/// Information about the number of observations.
/// </summary>
[EdiElement]
public class SPS_SizeDetails
{
	/// <summary>
	/// Indication of the type or application of a size.
	/// </summary>
	[EdiValue("X(3)", Path = "SPS/*/0", Mandatory = false)]
	public SizeQualifier? SizeQualifier { get; set; }

	/// <summary>
	/// A specified magnitude.
	/// </summary>
	[EdiValue("9(15)", Path = "SPS/*/1", Mandatory = false)]
	public decimal? Size { get; set; }
}