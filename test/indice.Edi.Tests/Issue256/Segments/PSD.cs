using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To define the physical sample parameters associated with a test, resulting in discrete measurements.
/// </summary>
[EdiSegment, EdiPath("PSD")]
public class PSD
{
	/// <summary>
	/// Code specifying the stage in the product development cycle at which the specimen was selected for testing.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/0", Mandatory = false)]
	public SampleProcessStatusCoded? SampleProcessStatusCoded { get; set; }

	/// <summary>
	/// Code denoting the method of selecting the test specimen.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/1", Mandatory = false)]
	public SampleSelectionMethodCoded? SampleSelectionMethodCoded { get; set; }

	/// <summary>
	/// Number of samples collected per specified unit of measure.
	/// </summary>
	[EdiPath("PSD/2")]
	public PSD_FrequencyDetails? FrequencyDetails { get; set; }

	/// <summary>
	/// Code describing the state of the specimen.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/3", Mandatory = false)]
	public SampleDescriptionCoded? SampleDescriptionCoded { get; set; }

	/// <summary>
	/// Code specifying the direction in which the sample was taken.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/4", Mandatory = false)]
	public SampleDirectionCoded? SampleDirectionCoded { get; set; }

	/// <summary>
	/// Identification of location within the specimen, from which the sample was taken.
	/// </summary>
	[EdiPath("PSD/5")]
	public PSD_SampleLocationDetails? SampleLocationDetails1 { get; set; }

	/// <summary>
	/// Identification of location within the specimen, from which the sample was taken.
	/// </summary>
	[EdiPath("PSD/6")]
	public PSD_SampleLocationDetails? SampleLocationDetails2 { get; set; }

	/// <summary>
	/// Identification of location within the specimen, from which the sample was taken.
	/// </summary>
	[EdiPath("PSD/7")]
	public PSD_SampleLocationDetails? SampleLocationDetails3 { get; set; }
}

/// <summary>
/// Number of samples collected per specified unit of measure.
/// </summary>
[EdiElement]
public class PSD_FrequencyDetails
{
	/// <summary>
	/// Indication of the application of a frequency.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/*/0", Mandatory = true)]
	public FrequencyQualifier? FrequencyQualifier { get; set; }

	/// <summary>
	/// A value indicating a repetitive occurrence.
	/// </summary>
	[EdiValue("9(9)", Path = "PSD/*/1", Mandatory = false)]
	public decimal? FrequencyValue { get; set; }

	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/*/2", Mandatory = false)]
	public string? MeasureUnitQualifier { get; set; }
}

/// <summary>
/// Identification of location within the specimen, from which the sample was taken.
/// </summary>
[EdiElement]
public class PSD_SampleLocationDetails
{
	/// <summary>
	/// Code specifying the location, within the specimen, from which the sample was taken.
	/// </summary>
	[EdiValue("X(3)", Path = "PSD/*/0", Mandatory = false)]
	public SampleLocationCoded? SampleLocationCoded { get; set; }

	/// <summary>
	/// Free form description of the location within the specimen, from which the sample was taken.
	/// </summary>
	[EdiValue("X(35)", Path = "PSD/*/1", Mandatory = false)]
	public string? SampleLocation { get; set; }
}