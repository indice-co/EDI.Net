using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify characteristics of a person such as ethnic origin.
/// </summary>
[EdiSegment, EdiPath("IHC")]
public class IHC
{
	/// <summary>
	/// To specify the type of specific person characteristic.
	/// </summary>
	[EdiValue("X(3)", Path = "IHC/0", Mandatory = true)]
	public PersonCharacteristicQualifier? PersonCharacteristicQualifier { get; set; }

	/// <summary>
	/// To specify an inherited characteristic of a person.
	/// </summary>
	[EdiPath("IHC/1")]
	public IHC_PersonInheritedCharacteristicDetails? PersonInheritedCharacteristicDetails { get; set; }
}

/// <summary>
/// To specify an inherited characteristic of a person.
/// </summary>
[EdiElement]
public class IHC_PersonInheritedCharacteristicDetails
{
	/// <summary>
	/// To specify a person inherited characteristic using a code value.
	/// </summary>
	[EdiValue("X(8)", Path = "IHC/*/0", Mandatory = false)]
	public string? PersonInheritedCharacteristicIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IHC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IHC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify a person inherited characteristic as free text.
	/// </summary>
	[EdiValue("X(70)", Path = "IHC/*/3", Mandatory = false)]
	public string? PersonInheritedCharacteristic { get; set; }
}