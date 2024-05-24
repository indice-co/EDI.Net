using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a unit of equipment.
/// </summary>
[EdiSegment, EdiPath("EQD")]
public class EQD
{
	/// <summary>
	/// Code identifying type of equipment.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/0", Mandatory = true)]
	public EquipmentQualifier? EquipmentQualifier { get; set; }

	/// <summary>
	/// Marks (letters and/or numbers) identifying equipment used for transport such as a container.
	/// </summary>
	[EdiPath("EQD/1")]
	public EQD_EquipmentIdentification? EquipmentIdentification { get; set; }

	/// <summary>
	/// Code and/or name identifying size and type of equipment used in transport. Code preferred.
	/// </summary>
	[EdiPath("EQD/2")]
	public EQD_EquipmentSizeAndType? EquipmentSizeAndType { get; set; }

	/// <summary>
	/// To indicate the party that is the supplier of the equipment.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/3", Mandatory = false)]
	public EquipmentSupplierCoded? EquipmentSupplierCoded { get; set; }

	/// <summary>
	/// Indication of the action related to the equipment.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/4", Mandatory = false)]
	public EquipmentStatusCoded? EquipmentStatusCoded { get; set; }

	/// <summary>
	/// To indicate the extent to which the equipment is full or empty.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/5", Mandatory = false)]
	public FullEmptyIndicatorCoded? FullEmptyIndicatorCoded { get; set; }

}

/// <summary>
/// Marks (letters and/or numbers) identifying equipment used for transport such as a container.
/// </summary>
[EdiElement]
public class EQD_EquipmentIdentification
{
	/// <summary>
	/// Marks (letters and/or numbers) which identify equipment e.g. unit load device.
	/// </summary>
	[EdiValue("X(17)", Path = "EQD/*/0", Mandatory = false)]
	public string? EquipmentIdentificationNumber { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Identification of the name of a country or other geographical entity as specified in ISO 3166.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/*/3", Mandatory = false)]
	public string? CountryCoded { get; set; }
}

/// <summary>
/// Code and/or name identifying size and type of equipment used in transport. Code preferred.
/// </summary>
[EdiElement]
public class EQD_EquipmentSizeAndType
{
	/// <summary>
	/// Coded description of the size and type of equipment e.g. unit load device.
	/// </summary>
	[EdiValue("X(10)", Path = "EQD/*/0", Mandatory = false)]
	public EquipmentSizeAndTypeIdentification? EquipmentSizeAndTypeIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "EQD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Plain language description of the size and type of equipment e.g. unit load device.
	/// </summary>
	[EdiValue("X(35)", Path = "EQD/*/3", Mandatory = false)]
	public string? EquipmentSizeAndType { get; set; }
}