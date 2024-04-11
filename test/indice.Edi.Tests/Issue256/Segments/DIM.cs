using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify dimensions.
/// </summary>
[EdiSegment, EdiPath("DIM")]
public class DIM
{
	/// <summary>
	/// To specify the dimensions applicable to each of the transportable units.
	/// </summary>
	[EdiValue("X(3)", Path = "DIM/0", Mandatory = true)]
	public DimensionQualifier? DimensionQualifier { get; set; }

	/// <summary>
	/// Specification of the dimensions of a transportable unit.
	/// </summary>
	[EdiPath("DIM/1")]
	public DIM_Dimensions? Dimensions { get; set; }
}

/// <summary>
/// Specification of the dimensions of a transportable unit.
/// </summary>
[EdiElement]
public class DIM_Dimensions
{
	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "DIM/*/0", Mandatory = true)]
	public string? MeasureUnitQualifier { get; set; }

	/// <summary>
	/// Length of pieces or packages stated for transport purposes.
	/// </summary>
	[EdiValue("9(15)", Path = "DIM/*/1", Mandatory = false)]
	public decimal? LengthDimension { get; set; }

	/// <summary>
	/// Width of pieces or packages stated for transport purposes.
	/// </summary>
	[EdiValue("9(15)", Path = "DIM/*/2", Mandatory = false)]
	public decimal? WidthDimension { get; set; }

	/// <summary>
	/// Height of pieces or packages stated for transport purposes.
	/// </summary>
	[EdiValue("9(15)", Path = "DIM/*/3", Mandatory = false)]
	public decimal? HeightDimension { get; set; }
}