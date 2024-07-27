using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify a pertinent quantity.
/// </summary>
[EdiSegment, EdiPath("QTY")]
public class QTY
{
	/// <summary>
	/// Quantity information in a transaction, qualified when relevant.
	/// </summary>
	[EdiPath("QTY/0")]
	public QTY_QuantityDetails? QuantityDetails { get; set; }
}

/// <summary>
/// Quantity information in a transaction, qualified when relevant.
/// </summary>
[EdiElement]
public class QTY_QuantityDetails
{
	/// <summary>
	/// Code giving specific meaning to a quantity.
	/// </summary>
	[EdiValue("X(3)", Path = "QTY/*/0", Mandatory = true)]
	public QuantityQualifier? QuantityQualifier { get; set; }

	/// <summary>
	/// Numeric value of a quantity.
	/// </summary>
	[EdiValue("9(15)", Path = "QTY/*/1", Mandatory = true)]
	public decimal? Quantity { get; set; }

	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "QTY/*/2", Mandatory = false)]
	public string? MeasureUnitQualifier { get; set; }
}