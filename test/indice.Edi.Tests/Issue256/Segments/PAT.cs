using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the payment terms basis.
/// </summary>
[EdiSegment, EdiPath("PAT")]
public class PAT
{
	/// <summary>
	/// Identification of the type of payment terms.
	/// </summary>
	[EdiValue("X(3)", Path = "PAT/0", Mandatory = true)]
	public PaymentTermsTypeQualifier? PaymentTermsTypeQualifier { get; set; }

	/// <summary>
	/// Terms of payment code from a specified source.
	/// </summary>
	[EdiPath("PAT/1")]
	public PAT_PaymentTerms? PaymentTerms { get; set; }

	/// <summary>
	/// Time details in payment terms.
	/// </summary>
	[EdiPath("PAT/2")]
	public PAT_TermsTimeInformation? TermsTimeInformation { get; set; }
}

/// <summary>
/// Terms of payment code from a specified source.
/// </summary>
[EdiElement]
public class PAT_PaymentTerms
{
	/// <summary>
	/// Identification of the terms of payment between the parties to a transaction (generic term).
	/// </summary>
	[EdiValue("X(17)", Path = "PAT/*/0", Mandatory = true)]
	public TermsOfPaymentIdentification? TermsOfPaymentIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PAT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PAT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Conditions of payment between the parties to a transaction (generic term).
	/// </summary>
	[EdiValue("X(35)", Path = "PAT/*/3", Mandatory = false)]
	public string? TermsOfPayment1 { get; set; }

	/// <summary>
	/// Conditions of payment between the parties to a transaction (generic term).
	/// </summary>
	[EdiValue("X(35)", Path = "PAT/*/4", Mandatory = false)]
	public string? TermsOfPayment2 { get; set; }
}

/// <summary>
/// Time details in payment terms.
/// </summary>
[EdiElement]
public class PAT_TermsTimeInformation
{
	/// <summary>
	/// Code relating payment terms to the date of a specific event.
	/// </summary>
	[EdiValue("X(3)", Path = "PAT/*/0", Mandatory = true)]
	public PaymentTimeReferenceCoded? PaymentTimeReferenceCoded { get; set; }

	/// <summary>
	/// Code relating payment terms to a time before, on or after the reference date.
	/// </summary>
	[EdiValue("X(3)", Path = "PAT/*/1", Mandatory = false)]
	public TimeRelationCoded? TimeRelationCoded { get; set; }

	/// <summary>
	/// Agreed or specified period of time (coded).
	/// </summary>
	[EdiValue("X(3)", Path = "PAT/*/2", Mandatory = false)]
	public TypeOfPeriodCoded? TypeOfPeriodCoded { get; set; }

	/// <summary>
	/// Number of periods of the type indicated in data element 2151 Type of period, coded.
	/// </summary>
	[EdiValue("9(3)", Path = "PAT/*/3", Mandatory = false)]
	public decimal? NumberOfPeriods { get; set; }
}