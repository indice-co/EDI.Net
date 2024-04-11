using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify a component of a data structure (e.g. an array or table).
/// </summary>
[EdiSegment, EdiPath("SCD")]
public class SCD
{
	/// <summary>
	/// To identify the function of a structure component.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/0", Mandatory = true)]
	public ComponentFunctionQualifier? ComponentFunctionQualifier { get; set; }

	/// <summary>
	/// The identification of a structure component.
	/// </summary>
	[EdiPath("SCD/1")]
	public SCD_StructureComponentIdentification? StructureComponentIdentification { get; set; }

	/// <summary>
	/// Identification of a transaction party by code.
	/// </summary>
	[EdiPath("SCD/2")]
	public SCD_PartyIdentificationDetails? PartyIdentificationDetails { get; set; }

	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/3", Mandatory = false)]
	public StatusCoded? StatusCoded { get; set; }

	/// <summary>
	/// Number indicating the level of an object which is in a hierarchy.
	/// </summary>
	[EdiValue("9(2)", Path = "SCD/4", Mandatory = false)]
	public decimal? ConfigurationLevel { get; set; }

	/// <summary>
	/// To identify the position of an object in a structure containing the object.
	/// </summary>
	[EdiPath("SCD/5")]
	public SCD_PositionIdentification? PositionIdentification { get; set; }

	/// <summary>
	/// Specific product characteristic data.
	/// </summary>
	[EdiPath("SCD/6")]
	public SCD_ProductCharacteristic? ProductCharacteristic { get; set; }
}

/// <summary>
/// The identification of a structure component.
/// </summary>
[EdiElement]
public class SCD_StructureComponentIdentification
{
	/// <summary>
	/// The identifier of a structure component.
	/// </summary>
	[EdiValue("X(35)", Path = "SCD/*/0", Mandatory = true)]
	public string? StructureComponentIdentifier { get; set; }

	/// <summary>
	/// Code specifying the type/source of identity number.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/*/1", Mandatory = false)]
	public IdentityNumberQualifier? IdentityNumberQualifier { get; set; }
}

/// <summary>
/// Identification of a transaction party by code.
/// </summary>
[EdiElement]
public class SCD_PartyIdentificationDetails
{
	/// <summary>
	/// Code identifying a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "SCD/*/0", Mandatory = true)]
	public string? PartyIdIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// To identify the position of an object in a structure containing the object.
/// </summary>
[EdiElement]
public class SCD_PositionIdentification
{
	/// <summary>
	/// A unique number assigned by the sender to identify a level within a hierarchical structure.
	/// </summary>
	[EdiValue("X(12)", Path = "SCD/*/0", Mandatory = false)]
	public string? HierarchicalIdNumber { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "SCD/*/1", Mandatory = false)]
	public string? SequenceNumber { get; set; }
}

/// <summary>
/// Specific product characteristic data.
/// </summary>
[EdiElement]
public class SCD_ProductCharacteristic
{
	/// <summary>
	/// A code from an industry code list which provides specific data about a product characteristic.
	/// </summary>
	[EdiValue("X(17)", Path = "SCD/*/0", Mandatory = true)]
	public string? CharacteristicIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SCD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "SCD/*/3", Mandatory = false)]
	public string? Characteristic1 { get; set; }

	/// <summary>
	/// Free form description of the product characteristic.
	/// </summary>
	[EdiValue("X(35)", Path = "SCD/*/4", Mandatory = false)]
	public string? Characteristic2 { get; set; }
}