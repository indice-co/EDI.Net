using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify relevant duty/tax/fee information.
/// </summary>
[EdiSegment, EdiPath("TAX")]
public class TAX
{
	/// <summary>
	/// Code identifying the function of an duty, tax or fee information.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/0", Mandatory = true)]
	public DutyTaxFeeFunctionQualifier? DutyTaxFeeFunctionQualifier { get; set; }

	/// <summary>
	/// Code and/or name identifying duty, tax or fee.
	/// </summary>
	[EdiPath("TAX/1")]
	public TAX_DutyTaxFeeType? DutyTaxFeeType { get; set; }

	/// <summary>
	/// Indication of account reference for duties, taxes and/or fees.
	/// </summary>
	[EdiPath("TAX/2")]
	public TAX_DutyTaxFeeAccountDetail? DutyTaxFeeAccountDetail { get; set; }

	/// <summary>
	/// Value or quantity on which a duty or tax will be assessed.
	/// </summary>
	[EdiValue("X(15)", Path = "TAX/3", Mandatory = false)]
	public string? DutyTaxFeeAssessmentBasis { get; set; }

	/// <summary>
	/// Rate of duty/tax/fee applicable to commodities or of tax applicable to services.
	/// </summary>
	[EdiPath("TAX/4")]
	public TAX_DutyTaxFeeDetail? DutyTaxFeeDetail { get; set; }

	/// <summary>
	/// Code identifying a tax/duty/fee category within a tax/duty/fee type system.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/5", Mandatory = false)]
	public DutyTaxFeeCategoryCoded? DutyTaxFeeCategoryCoded { get; set; }

	/// <summary>
	/// Number assigned to a purchaser (buyer, orderer) by a tax jurisdiction (state, country, etc).
	/// </summary>
	[EdiValue("X(20)", Path = "TAX/6", Mandatory = false)]
	public string? PartyTaxIdentificationNumber { get; set; }

}

/// <summary>
/// Code and/or name identifying duty, tax or fee.
/// </summary>
[EdiElement]
public class TAX_DutyTaxFeeType
{
	/// <summary>
	/// Identification of the type of duty or tax or fee applicable to commodities or of tax applicable to services.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/0", Mandatory = false)]
	public DutyTaxFeeTypeCoded? DutyTaxFeeTypeCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Type of duty or tax or fee applicable to commodities or of tax applicable to services.
	/// </summary>
	[EdiValue("X(35)", Path = "TAX/*/3", Mandatory = false)]
	public string? DutyTaxFeeType { get; set; }
}

/// <summary>
/// Indication of account reference for duties, taxes and/or fees.
/// </summary>
[EdiElement]
public class TAX_DutyTaxFeeAccountDetail
{
	/// <summary>
	/// Identification of a particular duty, tax or fee.
	/// </summary>
	[EdiValue("X(6)", Path = "TAX/*/0", Mandatory = true)]
	public string? DutyTaxFeeAccountIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Rate of duty/tax/fee applicable to commodities or of tax applicable to services.
/// </summary>
[EdiElement]
public class TAX_DutyTaxFeeDetail
{
	/// <summary>
	/// Identification of the rate of duty or tax or fee applicable to commodities or of tax applicable to services.
	/// </summary>
	[EdiValue("X(7)", Path = "TAX/*/0", Mandatory = false)]
	public string? DutyTaxFeeRateIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier1 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded1 { get; set; }

	/// <summary>
	/// Rate of duty or tax or fee applicable to commodities or of tax applicable to services.
	/// </summary>
	[EdiValue("X(17)", Path = "TAX/*/3", Mandatory = false)]
	public string? DutyTaxFeeRate { get; set; }

	/// <summary>
	/// Identification of the various elements of tax combination to be attributed to a commodity.
	/// </summary>
	[EdiValue("X(12)", Path = "TAX/*/4", Mandatory = false)]
	public DutyTaxFeeRateBasisIdentification? DutyTaxFeeRateBasisIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/5", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier2 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TAX/*/6", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded2 { get; set; }
}