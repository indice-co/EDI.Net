using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a coded or non coded value list.
/// </summary>
[EdiSegment, EdiPath("VLI")]
public class VLI
{
	/// <summary>
	/// The identification of a coded or non coded value list.
	/// </summary>
	[EdiPath("VLI/0")]
	public VLI_ValueListIdentification? ValueListIdentification { get; set; }

	/// <summary>
	/// Identification of a transaction party by code.
	/// </summary>
	[EdiPath("VLI/1")]
	public VLI_PartyIdentificationDetails? PartyIdentificationDetails { get; set; }

	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/2", Mandatory = false)]
	public StatusCoded? StatusCoded { get; set; }

	/// <summary>
	/// The name of a list of coded or non coded values.
	/// </summary>
	[EdiValue("X(70)", Path = "VLI/3", Mandatory = false)]
	public string? ValueListName { get; set; }

	/// <summary>
	/// To identify a designated class.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/4", Mandatory = false)]
	public ClassDesignatorCoded? ClassDesignatorCoded { get; set; }

	/// <summary>
	/// A code indicating the type of value list.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/5", Mandatory = false)]
	public ValueListTypeCoded? ValueListTypeCoded { get; set; }

	/// <summary>
	/// Specific product characteristic data.
	/// </summary>
	[EdiPath("VLI/6")]
	public VLI_ProductCharacteristic? ProductCharacteristic { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/7", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}

/// <summary>
/// The identification of a coded or non coded value list.
/// </summary>
[EdiElement]
public class VLI_ValueListIdentification
{
	/// <summary>
	/// The identifier of a coded or non coded value list.
	/// </summary>
	[EdiValue("X(35)", Path = "VLI/*/0", Mandatory = true)]
	public string? ValueListIdentifier { get; set; }

	/// <summary>
	/// Code specifying the type/source of identity number.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/*/1", Mandatory = false)]
	public IdentityNumberQualifier? IdentityNumberQualifier { get; set; }
}

/// <summary>
/// Identification of a transaction party by code.
/// </summary>
[EdiElement]
public class VLI_PartyIdentificationDetails
{
	/// <summary>
	/// Code identifying a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "VLI/*/0", Mandatory = true)]
	public string? PartyIdIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Specific product characteristic data.
/// </summary>
[EdiElement]
public class VLI_ProductCharacteristic
{
	/// <summary>
	/// A code from an industry code list which provides specific data about a product characteristic.
	/// </summary>
	[EdiValue("X(17)", Path = "VLI/*/0", Mandatory = true)]
	public string? CharacteristicIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "VLI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "VLI/*/3", Mandatory = false)]
	public string? Characteristic1 { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "VLI/*/4", Mandatory = false)]
	public string? Characteristic2 { get; set; }
}