using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify the relevance of a characteristic information for subsequent business processes.
/// </summary>
[EdiSegment, EdiPath("CCI")]
public class CCI
{
	/// <summary>
	/// Specification of the type of class.
	/// </summary>
	[EdiValue("X(3)", Path = "CCI/0", Mandatory = false)]
	public PropertyClassCoded? PropertyClassCoded { get; set; }

	/// <summary>
	/// Identification of measurement type.
	/// </summary>
	[EdiPath("CCI/1")]
	public CCI_MeasurementDetails? MeasurementDetails { get; set; }

	/// <summary>
	/// Specific product characteristic data.
	/// </summary>
	[EdiPath("CCI/2")]
	public CCI_ProductCharacteristic? ProductCharacteristic { get; set; }

	/// <summary>
	/// To specify the relevance of a characteristic.
	/// </summary>
	[EdiValue("X(3)", Path = "CCI/3", Mandatory = false)]
	public CharacteristicRelevanceCoded? CharacteristicRelevanceCoded { get; set; }

}

/// <summary>
/// Identification of measurement type.
/// </summary>
[EdiElement]
public class CCI_MeasurementDetails
{
	/// <summary>
	/// Specification of the property measured.
	/// </summary>
	[EdiValue("X(3)", Path = "CCI/*/0", Mandatory = false)]
	public PropertyMeasuredCoded? PropertyMeasuredCoded { get; set; }

	/// <summary>
	/// Code specifying the significance of a measurement value.
	/// </summary>
	[EdiValue("X(3)", Path = "CCI/*/1", Mandatory = false)]
	public MeasurementSignificanceCoded? MeasurementSignificanceCoded { get; set; }

	/// <summary>
	/// Code used to specify non-discrete measurement values.
	/// </summary>
	[EdiValue("X(17)", Path = "CCI/*/2", Mandatory = false)]
	public MeasurementAttributeIdentification? MeasurementAttributeIdentification { get; set; }

	/// <summary>
	/// To specify non-discrete measurement values.
	/// </summary>
	[EdiValue("X(70)", Path = "CCI/*/3", Mandatory = false)]
	public string? MeasurementAttribute { get; set; }
}

/// <summary>
/// Specific product characteristic data.
/// </summary>
[EdiElement]
public class CCI_ProductCharacteristic
{
	/// <summary>
	/// A code from an industry code list which provides specific data about a product characteristic.
	/// </summary>
	[EdiValue("X(17)", Path = "CCI/*/0", Mandatory = true)]
	public string? CharacteristicIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CCI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CCI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "CCI/*/3", Mandatory = false)]
	public string? Characteristic1 { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "CCI/*/4", Mandatory = false)]
	public string? Characteristic2 { get; set; }
}