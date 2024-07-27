using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide the value of a characteristic.
/// </summary>
[EdiSegment, EdiPath("CAV")]
public class CAV
{
	/// <summary>
	/// To provide the value of a characteristic.
	/// </summary>
	[EdiPath("CAV/0")]
	public CAV_CharacteristicValue? CharacteristicValue { get; set; }
}

/// <summary>
/// To provide the value of a characteristic.
/// </summary>
[EdiElement]
public class CAV_CharacteristicValue
{
	/// <summary>
	/// To identify the value of a particular characteristic in coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "CAV/*/0", Mandatory = false)]
	public string? CharacteristicValueCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CAV/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CAV/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To identify the value of a characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "CAV/*/3", Mandatory = false)]
	public string? CharacteristicValue1 { get; set; }

	/// <summary>
	/// To identify the value of a characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "CAV/*/4", Mandatory = false)]
	public string? CharacteristicValue2 { get; set; }
}