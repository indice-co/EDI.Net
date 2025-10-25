using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify a discrete or non-discrete result as a value or value range.
/// </summary>
[EdiSegment, EdiPath("RSL")]
public class RSL
{
	/// <summary>
	/// To specify the type of value or value range.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/0", Mandatory = true)]
	public ResultQualifier? ResultQualifier { get; set; }

	/// <summary>
	/// To specify the representation of a value or value range using code.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/1", Mandatory = false)]
	public ResultTypeCoded? ResultTypeCoded { get; set; }

	/// <summary>
	/// To specify a value.
	/// </summary>
	[EdiPath("RSL/2")]
	public RSL_ResultDetails? ResultDetails1 { get; set; }

	/// <summary>
	/// To specify a value.
	/// </summary>
	[EdiPath("RSL/3")]
	public RSL_ResultDetails? ResultDetails2 { get; set; }

	/// <summary>
	/// To specify a measurement unit.
	/// </summary>
	[EdiPath("RSL/4")]
	public RSL_MeasurementUnitDetails? MeasurementUnitDetails { get; set; }

	/// <summary>
	/// Code to specify (ab)normal value.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/5", Mandatory = false)]
	public ResultNormalcyIndicatorCoded? ResultNormalcyIndicatorCoded { get; set; }

}

/// <summary>
/// To specify a value.
/// </summary>
[EdiElement]
public class RSL_ResultDetails
{
	/// <summary>
	/// Value of the measured unit.
	/// </summary>
	[EdiValue("X(18)", Path = "RSL/*/0", Mandatory = false)]
	public string? MeasurementValue { get; set; }

	/// <summary>
	/// Code specifying the significance of a measurement value.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/*/1", Mandatory = false)]
	public MeasurementSignificanceCoded? MeasurementSignificanceCoded { get; set; }

	/// <summary>
	/// Code used to specify non-discrete measurement values.
	/// </summary>
	[EdiValue("X(17)", Path = "RSL/*/2", Mandatory = false)]
	public MeasurementAttributeIdentification? MeasurementAttributeIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/*/3", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/*/4", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify non-discrete measurement values.
	/// </summary>
	[EdiValue("X(70)", Path = "RSL/*/5", Mandatory = false)]
	public string? MeasurementAttribute { get; set; }
}

/// <summary>
/// To specify a measurement unit.
/// </summary>
[EdiElement]
public class RSL_MeasurementUnitDetails
{
	/// <summary>
	/// Coded identification of a measurement unit.
	/// </summary>
	[EdiValue("X(8)", Path = "RSL/*/0", Mandatory = false)]
	public string? MeasurementUnitIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "RSL/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Specification of a measurement unit as free text.
	/// </summary>
	[EdiValue("X(35)", Path = "RSL/*/3", Mandatory = false)]
	public string? MeasurementUnit { get; set; }
}