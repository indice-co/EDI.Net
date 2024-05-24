using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify currencies used in the transaction and relevant details for the rate of exchange.
/// </summary>
[EdiSegment, EdiPath("CUX")]
public class CUX
{
	/// <summary>
	/// The usage to which a currency relates.
	/// </summary>
	[EdiPath("CUX/0")]
	public CUX_CurrencyDetails? CurrencyDetails1 { get; set; }

	/// <summary>
	/// The usage to which a currency relates.
	/// </summary>
	[EdiPath("CUX/1")]
	public CUX_CurrencyDetails? CurrencyDetails2 { get; set; }

	/// <summary>
	/// The rate at which one specified currency is expressed in another specified currency.
	/// </summary>
	[EdiValue("9(12)", Path = "CUX/2", Mandatory = false)]
	public decimal? RateOfExchange { get; set; }

	/// <summary>
	/// Code identifying the market upon which the currency exchange rate is based.
	/// </summary>
	[EdiValue("X(3)", Path = "CUX/3", Mandatory = false)]
	public CurrencyMarketExchangeCoded? CurrencyMarketExchangeCoded { get; set; }

}

/// <summary>
/// The usage to which a currency relates.
/// </summary>
[EdiElement]
public class CUX_CurrencyDetails
{
	/// <summary>
	/// Specification of the usage to which the currency relates.
	/// </summary>
	[EdiValue("X(3)", Path = "CUX/*/0", Mandatory = true)]
	public CurrencyDetailsQualifier? CurrencyDetailsQualifier { get; set; }

	/// <summary>
	/// Identification of the name or symbol of the monetary unit involved in the transaction.
	/// </summary>
	[EdiValue("X(3)", Path = "CUX/*/1", Mandatory = false)]
	public string? CurrencyCoded { get; set; }

	/// <summary>
	/// Code giving specific meaning to data element 6345 Currency.
	/// </summary>
	[EdiValue("X(3)", Path = "CUX/*/2", Mandatory = false)]
	public CurrencyQualifier? CurrencyQualifier { get; set; }

	/// <summary>
	/// Multiplying factor used in expressing the number of currency units.
	/// </summary>
	[EdiValue("9(4)", Path = "CUX/*/3", Mandatory = false)]
	public decimal? CurrencyRateBase { get; set; }
}