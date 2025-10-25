using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the placement of goods in relation to equipment.
/// </summary>
[EdiSegment, EdiPath("SGP")]
public class SGP
{
	/// <summary>
	/// Marks (letters and/or numbers) identifying equipment used for transport such as a container.
	/// </summary>
	[EdiPath("SGP/0")]
	public SGP_EquipmentIdentification? EquipmentIdentification { get; set; }

	/// <summary>
	/// Number of individual parts of a shipment either unpacked, or packed in such a way that they cannot be divided without first undoing the packing.
	/// </summary>
	[EdiValue("9(8)", Path = "SGP/1", Mandatory = false)]
	public decimal? NumberOfPackages { get; set; }

}

/// <summary>
/// Marks (letters and/or numbers) identifying equipment used for transport such as a container.
/// </summary>
[EdiElement]
public class SGP_EquipmentIdentification
{
	/// <summary>
	/// Marks (letters and/or numbers) which identify equipment e.g. unit load device.
	/// </summary>
	[EdiValue("X(17)", Path = "SGP/*/0", Mandatory = false)]
	public string? EquipmentIdentificationNumber { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SGP/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SGP/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Identification of the name of a country or other geographical entity as specified in ISO 3166.
	/// </summary>
	[EdiValue("X(3)", Path = "SGP/*/3", Mandatory = false)]
	public string? CountryCoded { get; set; }
}