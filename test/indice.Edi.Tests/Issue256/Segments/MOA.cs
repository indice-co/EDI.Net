using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify a monetary amount.
/// </summary>
[EdiSegment, EdiPath("MOA")]
public class MOA
{
	/// <summary>
	/// Amount of goods or services stated as a monetary amount in a specified currency.
	/// </summary>
	[EdiPath("MOA/0")]
	public MOA_MonetaryAmount? MonetaryAmount { get; set; }
}

/// <summary>
/// Amount of goods or services stated as a monetary amount in a specified currency.
/// </summary>
[EdiElement]
public class MOA_MonetaryAmount
{
	/// <summary>
	/// Indication of type of amount.
	/// </summary>
	[EdiValue("X(3)", Path = "MOA/*/0", Mandatory = true)]
	public MonetaryAmountTypeQualifier? MonetaryAmountTypeQualifier { get; set; }

	/// <summary>
	/// Number of monetary units.
	/// </summary>
	[EdiValue("9(18)", Path = "MOA/*/1", Mandatory = false)]
	public decimal? MonetaryAmount { get; set; }

	/// <summary>
	/// Identification of the name or symbol of the monetary unit involved in the transaction.
	/// </summary>
	[EdiValue("X(3)", Path = "MOA/*/2", Mandatory = false)]
	public string? CurrencyCoded { get; set; }

	/// <summary>
	/// Code giving specific meaning to data element 6345 Currency.
	/// </summary>
	[EdiValue("X(3)", Path = "MOA/*/3", Mandatory = false)]
	public CurrencyQualifier? CurrencyQualifier { get; set; }

	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "MOA/*/4", Mandatory = false)]
	public StatusCoded? StatusCoded { get; set; }
}