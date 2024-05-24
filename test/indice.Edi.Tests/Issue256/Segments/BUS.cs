using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide information related to the processing and purpose of a financial message.
/// </summary>
[EdiSegment, EdiPath("BUS")]
public class BUS
{
	/// <summary>
	/// To specify a business reason.
	/// </summary>
	[EdiPath("BUS/0")]
	public BUS_BusinessFunction? BusinessFunction { get; set; }

	/// <summary>
	/// Describe the geographic area for message.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/1", Mandatory = false)]
	public GeographicEnvironmentCoded? GeographicEnvironmentCoded { get; set; }

	/// <summary>
	/// Specification of the type of a financial transaction in coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/2", Mandatory = false)]
	public TypeOfFinancialTransactionCoded? TypeOfFinancialTransactionCoded { get; set; }

	/// <summary>
	/// Identification of a bank operation by code.
	/// </summary>
	[EdiPath("BUS/3")]
	public BUS_BankOperation? BankOperation { get; set; }

	/// <summary>
	/// Indication that the payment is within one company.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/4", Mandatory = false)]
	public IntracompanyPaymentCoded? IntracompanyPaymentCoded { get; set; }

}

/// <summary>
/// To specify a business reason.
/// </summary>
[EdiElement]
public class BUS_BusinessFunction
{
	/// <summary>
	/// Specification of the type of business function.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/0", Mandatory = true)]
	public BusinessFunctionQualifier? BusinessFunctionQualifier { get; set; }

	/// <summary>
	/// Code describing the specific business function.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/1", Mandatory = true)]
	public BusinessFunctionCoded? BusinessFunctionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a business.
	/// </summary>
	[EdiValue("X(70)", Path = "BUS/*/4", Mandatory = false)]
	public string? BusinessDescription { get; set; }
}

/// <summary>
/// Identification of a bank operation by code.
/// </summary>
[EdiElement]
public class BUS_BankOperation
{
	/// <summary>
	/// Describe the method to transfer funds in coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/0", Mandatory = true)]
	public BankOperationCoded? BankOperationCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "BUS/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}