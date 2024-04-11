using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify physical measurements, including dimension tolerances, weights and counts.
/// </summary>
[EdiSegment, EdiPath("MEA")]
public class MEA
{
	/// <summary>
	/// Specification of the purpose of the measurement.
	/// </summary>
	[EdiValue("X(3)", Path = "MEA/0", Mandatory = true)]
	public MeasurementPurposeQualifier? MeasurementPurposeQualifier { get; set; }

	/// <summary>
	/// Identification of measurement type.
	/// </summary>
	[EdiPath("MEA/1")]
	public MEA_MeasurementDetails? MeasurementDetails { get; set; }

	/// <summary>
	/// Measurement value and relevant minimum and maximum tolerances in that order.
	/// </summary>
	[EdiPath("MEA/2")]
	public MEA_ValueRange? ValueRange { get; set; }

	/// <summary>
	/// Code indicating the surface or layer of a product that is being described.
	/// </summary>
	[EdiValue("X(3)", Path = "MEA/3", Mandatory = false)]
	public SurfaceLayerIndicatorCoded? SurfaceLayerIndicatorCoded { get; set; }

}

/// <summary>
/// Identification of measurement type.
/// </summary>
[EdiElement]
public class MEA_MeasurementDetails
{
	/// <summary>
	/// Specification of the property measured.
	/// </summary>
	[EdiValue("X(3)", Path = "MEA/*/0", Mandatory = false)]
	public PropertyMeasuredCoded? PropertyMeasuredCoded { get; set; }

	/// <summary>
	/// Code specifying the significance of a measurement value.
	/// </summary>
	[EdiValue("X(3)", Path = "MEA/*/1", Mandatory = false)]
	public MeasurementSignificanceCoded? MeasurementSignificanceCoded { get; set; }

	/// <summary>
	/// Code used to specify non-discrete measurement values.
	/// </summary>
	[EdiValue("X(17)", Path = "MEA/*/2", Mandatory = false)]
	public MeasurementAttributeIdentification? MeasurementAttributeIdentification { get; set; }

	/// <summary>
	/// To specify non-discrete measurement values.
	/// </summary>
	[EdiValue("X(70)", Path = "MEA/*/3", Mandatory = false)]
	public string? MeasurementAttribute { get; set; }
}

/// <summary>
/// Measurement value and relevant minimum and maximum tolerances in that order.
/// </summary>
[EdiElement]
public class MEA_ValueRange
{
	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "MEA/*/0", Mandatory = true)]
	public string? MeasureUnitQualifier { get; set; }

	/// <summary>
	/// Value of the measured unit.
	/// </summary>
	[EdiValue("X(18)", Path = "MEA/*/1", Mandatory = false)]
	public string? MeasurementValue { get; set; }

	/// <summary>
	/// Minimum of a range.
	/// </summary>
	[EdiValue("9(18)", Path = "MEA/*/2", Mandatory = false)]
	public decimal? RangeMinimum { get; set; }

	/// <summary>
	/// Maximum of a range.
	/// </summary>
	[EdiValue("9(18)", Path = "MEA/*/3", Mandatory = false)]
	public decimal? RangeMaximum { get; set; }

	/// <summary>
	/// To specify the number of significant digits.
	/// </summary>
	[EdiValue("9(2)", Path = "MEA/*/4", Mandatory = false)]
	public decimal? SignificantDigits { get; set; }
}