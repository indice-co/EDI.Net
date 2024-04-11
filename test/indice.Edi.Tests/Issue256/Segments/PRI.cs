using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify price information.
/// </summary>
[EdiSegment, EdiPath("PRI")]
public class PRI
{
	/// <summary>
	/// Identification of price type, price and related details.
	/// </summary>
	[EdiPath("PRI/0")]
	public PRI_PriceInformation? PriceInformation { get; set; }

	/// <summary>
	/// Code indicating disposition of the price change of a sub-line item.
	/// </summary>
	[EdiValue("X(3)", Path = "PRI/1", Mandatory = false)]
	public SublinePriceChangeCoded? SublinePriceChangeCoded { get; set; }

}

/// <summary>
/// Identification of price type, price and related details.
/// </summary>
[EdiElement]
public class PRI_PriceInformation
{
	/// <summary>
	/// Identification of a type of price.
	/// </summary>
	[EdiValue("X(3)", Path = "PRI/*/0", Mandatory = true)]
	public PriceQualifier? PriceQualifier { get; set; }

	/// <summary>
	/// The monetary value associated with a purchase or sale of an article, product or service.
	/// </summary>
	[EdiValue("9(15)", Path = "PRI/*/1", Mandatory = false)]
	public decimal? Price { get; set; }

	/// <summary>
	/// Code identifying the type of price of an item.
	/// </summary>
	[EdiValue("X(3)", Path = "PRI/*/2", Mandatory = false)]
	public PriceTypeCoded? PriceTypeCoded { get; set; }

	/// <summary>
	/// Code identifying pricing specification.
	/// </summary>
	[EdiValue("X(3)", Path = "PRI/*/3", Mandatory = false)]
	public PriceTypeQualifier? PriceTypeQualifier { get; set; }

	/// <summary>
	/// Basis on which the unit price/rate applies.
	/// </summary>
	[EdiValue("9(9)", Path = "PRI/*/4", Mandatory = false)]
	public decimal? UnitPriceBasis { get; set; }

	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "PRI/*/5", Mandatory = false)]
	public string? MeasureUnitQualifier { get; set; }
}