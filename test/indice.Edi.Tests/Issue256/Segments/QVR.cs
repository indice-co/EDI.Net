using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify item details relating to quantity variances.
/// </summary>
[EdiSegment, EdiPath("QVR")]
public class QVR
{
	/// <summary>
	/// Information on quantity difference.
	/// </summary>
	[EdiPath("QVR/0")]
	public QVR_QuantityDifferenceInformation? QuantityDifferenceInformation { get; set; }

	/// <summary>
	/// Code defining the disposition of any difference between the quantity ordered and invoiced, or shipped and invoiced for a line item or transaction.
	/// </summary>
	[EdiValue("X(3)", Path = "QVR/1", Mandatory = false)]
	public DiscrepancyCoded? DiscrepancyCoded { get; set; }

	/// <summary>
	/// Code and/or description of the reason for a change.
	/// </summary>
	[EdiPath("QVR/2")]
	public QVR_ReasonForChange? ReasonForChange { get; set; }
}

/// <summary>
/// Information on quantity difference.
/// </summary>
[EdiElement]
public class QVR_QuantityDifferenceInformation
{
	/// <summary>
	/// Numeric value of variance between ordered/shipped/invoiced quantities.
	/// </summary>
	[EdiValue("9(15)", Path = "QVR/*/0", Mandatory = true)]
	public decimal? QuantityDifference { get; set; }

	/// <summary>
	/// Code giving specific meaning to a quantity.
	/// </summary>
	[EdiValue("X(3)", Path = "QVR/*/1", Mandatory = false)]
	public QuantityQualifier? QuantityQualifier { get; set; }
}

/// <summary>
/// Code and/or description of the reason for a change.
/// </summary>
[EdiElement]
public class QVR_ReasonForChange
{
	/// <summary>
	/// Identification of the reason for a change.
	/// </summary>
	[EdiValue("X(3)", Path = "QVR/*/0", Mandatory = false)]
	public ChangeReasonCoded? ChangeReasonCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "QVR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "QVR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of the reason for a change.
	/// </summary>
	[EdiValue("X(35)", Path = "QVR/*/3", Mandatory = false)]
	public string? ChangeReason { get; set; }
}