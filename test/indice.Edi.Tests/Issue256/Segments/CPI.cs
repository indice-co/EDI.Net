using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a charge.
/// </summary>
[EdiSegment, EdiPath("CPI")]
public class CPI
{
	/// <summary>
	/// Identification of a category or a zone of charges.
	/// </summary>
	[EdiPath("CPI/0")]
	public CPI_ChargeCategory? ChargeCategory { get; set; }

	/// <summary>
	/// Code identifying the method of payment.
	/// </summary>
	[EdiPath("CPI/1")]
	public CPI_MethodOfPayment? MethodOfPayment { get; set; }

	/// <summary>
	/// Code indicating whether freight item amount is prepaid or to be collected.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/2", Mandatory = false)]
	public PrepaidCollectIndicatorCoded? PrepaidCollectIndicatorCoded { get; set; }

}

/// <summary>
/// Identification of a category or a zone of charges.
/// </summary>
[EdiElement]
public class CPI_ChargeCategory
{
	/// <summary>
	/// To indicate the category or zone of charges.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/*/0", Mandatory = true)]
	public ChargeCategoryCoded? ChargeCategoryCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Code identifying the method of payment.
/// </summary>
[EdiElement]
public class CPI_MethodOfPayment
{
	/// <summary>
	/// Identification of method of payment for transport charges.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/*/0", Mandatory = true)]
	public TransportChargesMethodOfPaymentCoded? TransportChargesMethodOfPaymentCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CPI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}