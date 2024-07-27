using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the number of units.
/// </summary>
[EdiSegment, EdiPath("EQN")]
public class EQN
{
	/// <summary>
	/// Identification of number of units and its purpose.
	/// </summary>
	[EdiPath("EQN/0")]
	public EQN_NumberOfUnitDetails? NumberOfUnitDetails { get; set; }
}

/// <summary>
/// Identification of number of units and its purpose.
/// </summary>
[EdiElement]
public class EQN_NumberOfUnitDetails
{
	/// <summary>
	/// Number of units of a certain type.
	/// </summary>
	[EdiValue("9(15)", Path = "EQN/*/0", Mandatory = false)]
	public decimal? NumberOfUnits { get; set; }

	/// <summary>
	/// Indication of the objective of number of units information.
	/// </summary>
	[EdiValue("X(3)", Path = "EQN/*/1", Mandatory = false)]
	public NumberOfUnitsQualifier? NumberOfUnitsQualifier { get; set; }
}