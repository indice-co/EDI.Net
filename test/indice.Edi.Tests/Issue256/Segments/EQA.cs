using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify attached or related equipment.
/// </summary>
[EdiSegment, EdiPath("EQA")]
public class EQA
{
	/// <summary>
	/// Code identifying type of equipment.
	/// </summary>
	[EdiValue("X(3)", Path = "EQA/0", Mandatory = true)]
	public EquipmentQualifier? EquipmentQualifier { get; set; }

	/// <summary>
	/// Marks (letters and/or numbers) identifying equipment used for transport such as a container.
	/// </summary>
	[EdiPath("EQA/1")]
	public EQA_EquipmentIdentification? EquipmentIdentification { get; set; }
}

/// <summary>
/// Marks (letters and/or numbers) identifying equipment used for transport such as a container.
/// </summary>
[EdiElement]
public class EQA_EquipmentIdentification
{
	/// <summary>
	/// Marks (letters and/or numbers) which identify equipment e.g. unit load device.
	/// </summary>
	[EdiValue("X(17)", Path = "EQA/*/0", Mandatory = false)]
	public string? EquipmentIdentificationNumber { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EQA/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EQA/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Identification of the name of a country or other geographical entity as specified in ISO 3166.
	/// </summary>
	[EdiValue("X(3)", Path = "EQA/*/3", Mandatory = false)]
	public string? CountryCoded { get; set; }
}