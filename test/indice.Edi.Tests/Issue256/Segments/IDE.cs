using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify an object.
/// </summary>
[EdiSegment, EdiPath("IDE")]
public class IDE
{
	/// <summary>
	/// To identify a type of object.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/0", Mandatory = true)]
	public IdentificationQualifier? IdentificationQualifier { get; set; }

	/// <summary>
	/// The identification of an object.
	/// </summary>
	[EdiPath("IDE/1")]
	public IDE_IdentificationNumber? IdentificationNumber { get; set; }

	/// <summary>
	/// Identification of a transaction party by code.
	/// </summary>
	[EdiPath("IDE/2")]
	public IDE_PartyIdentificationDetails? PartyIdentificationDetails { get; set; }

	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/3", Mandatory = false)]
	public StatusCoded? StatusCoded { get; set; }

	/// <summary>
	/// Number indicating the level of an object which is in a hierarchy.
	/// </summary>
	[EdiValue("9(2)", Path = "IDE/4", Mandatory = false)]
	public decimal? ConfigurationLevel { get; set; }

	/// <summary>
	/// To identify the position of an object in a structure containing the object.
	/// </summary>
	[EdiPath("IDE/5")]
	public IDE_PositionIdentification? PositionIdentification { get; set; }

	/// <summary>
	/// Specific product characteristic data.
	/// </summary>
	[EdiPath("IDE/6")]
	public IDE_ProductCharacteristic? ProductCharacteristic { get; set; }
}

/// <summary>
/// The identification of an object.
/// </summary>
[EdiElement]
public class IDE_IdentificationNumber
{
	/// <summary>
	/// The number given to an object for its unique identification.
	/// </summary>
	[EdiValue("X(35)", Path = "IDE/*/0", Mandatory = true)]
	public string? IdentityNumber { get; set; }

	/// <summary>
	/// Code specifying the type/source of identity number.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/*/1", Mandatory = false)]
	public IdentityNumberQualifier? IdentityNumberQualifier { get; set; }

	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/*/2", Mandatory = false)]
	public StatusCoded? StatusCoded { get; set; }
}

/// <summary>
/// Identification of a transaction party by code.
/// </summary>
[EdiElement]
public class IDE_PartyIdentificationDetails
{
	/// <summary>
	/// Code identifying a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "IDE/*/0", Mandatory = true)]
	public string? PartyIdIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// To identify the position of an object in a structure containing the object.
/// </summary>
[EdiElement]
public class IDE_PositionIdentification
{
	/// <summary>
	/// A unique number assigned by the sender to identify a level within a hierarchical structure.
	/// </summary>
	[EdiValue("X(12)", Path = "IDE/*/0", Mandatory = false)]
	public string? HierarchicalIdNumber { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "IDE/*/1", Mandatory = false)]
	public string? SequenceNumber { get; set; }
}

/// <summary>
/// Specific product characteristic data.
/// </summary>
[EdiElement]
public class IDE_ProductCharacteristic
{
	/// <summary>
	/// A code from an industry code list which provides specific data about a product characteristic.
	/// </summary>
	[EdiValue("X(17)", Path = "IDE/*/0", Mandatory = true)]
	public string? CharacteristicIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "IDE/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "IDE/*/3", Mandatory = false)]
	public string? Characteristic1 { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "IDE/*/4", Mandatory = false)]
	public string? Characteristic2 { get; set; }
}