using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify an address.
/// </summary>
[EdiSegment, EdiPath("ADR")]
public class ADR
{
	/// <summary>
	/// To describe the usage of an address.
	/// </summary>
	[EdiPath("ADR/0")]
	public ADR_AddressUsage? AddressUsage { get; set; }

	/// <summary>
	/// To specify the details of an address.
	/// </summary>
	[EdiPath("ADR/1")]
	public ADR_AddressDetails? AddressDetails { get; set; }

	/// <summary>
	/// Name of a city (a town, a village) for addressing purposes.
	/// </summary>
	[EdiValue("X(35)", Path = "ADR/2", Mandatory = false)]
	public string? CityName { get; set; }

	/// <summary>
	/// Code defining postal zones or addresses.
	/// </summary>
	[EdiValue("X(9)", Path = "ADR/3", Mandatory = false)]
	public string? PostcodeIdentification { get; set; }

	/// <summary>
	/// Identification of the name of a country or other geographical entity as specified in ISO 3166.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/4", Mandatory = false)]
	public string? CountryCoded { get; set; }

	/// <summary>
	/// To specify a part of a country (eg county or part of a city).
	/// </summary>
	[EdiPath("ADR/5")]
	public ADR_CountrySubentityDetails? CountrySubentityDetails { get; set; }

	/// <summary>
	/// Identification of a location by code or name.
	/// </summary>
	[EdiPath("ADR/6")]
	public ADR_LocationIdentification? LocationIdentification { get; set; }
}

/// <summary>
/// To describe the usage of an address.
/// </summary>
[EdiElement]
public class ADR_AddressUsage
{
	/// <summary>
	/// To specify the purpose of the address.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/0", Mandatory = false)]
	public AddressPurposeCoded? AddressPurposeCoded { get; set; }

	/// <summary>
	/// To specify the type of address.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/1", Mandatory = false)]
	public AddressTypeCoded? AddressTypeCoded { get; set; }

	/// <summary>
	/// To specify the status of the address.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/2", Mandatory = false)]
	public AddressStatusCoded? AddressStatusCoded { get; set; }
}

/// <summary>
/// To specify the details of an address.
/// </summary>
[EdiElement]
public class ADR_AddressDetails
{
	/// <summary>
	/// To specify the content of the various address components.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/0", Mandatory = true)]
	public AddressFormatCoded? AddressFormatCoded { get; set; }

	/// <summary>
	/// To specify a component of an address.
	/// </summary>
	[EdiValue("X(70)", Path = "ADR/*/1", Mandatory = true)]
	public string? AddressComponent1 { get; set; }

	/// <summary>
	/// To specify a component of an address.
	/// </summary>
	[EdiValue("X(70)", Path = "ADR/*/2", Mandatory = false)]
	public string? AddressComponent2 { get; set; }

	/// <summary>
	/// To specify a component of an address.
	/// </summary>
	[EdiValue("X(70)", Path = "ADR/*/3", Mandatory = false)]
	public string? AddressComponent3 { get; set; }

	/// <summary>
	/// To specify a component of an address.
	/// </summary>
	[EdiValue("X(70)", Path = "ADR/*/4", Mandatory = false)]
	public string? AddressComponent4 { get; set; }

	/// <summary>
	/// To specify a component of an address.
	/// </summary>
	[EdiValue("X(70)", Path = "ADR/*/5", Mandatory = false)]
	public string? AddressComponent5 { get; set; }
}

/// <summary>
/// To specify a part of a country (eg county or part of a city).
/// </summary>
[EdiElement]
public class ADR_CountrySubentityDetails
{
	/// <summary>
	/// Identification of the name of sub-entities (state, province) defined by appropriate governmental agencies.
	/// </summary>
	[EdiValue("X(9)", Path = "ADR/*/0", Mandatory = false)]
	public string? CountrySubentityIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Name of sub-entities (state, province) defined by appropriate governmental agencies.
	/// </summary>
	[EdiValue("X(35)", Path = "ADR/*/3", Mandatory = false)]
	public string? CountrySubentity { get; set; }
}

/// <summary>
/// Identification of a location by code or name.
/// </summary>
[EdiElement]
public class ADR_LocationIdentification
{
	/// <summary>
	/// Identification of the name of place/location, other than 3164 City name.
	/// </summary>
	[EdiValue("X(25)", Path = "ADR/*/0", Mandatory = false)]
	public string? PlaceLocationIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "ADR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Name of place/location, other than 3164 City name.
	/// </summary>
	[EdiValue("X(70)", Path = "ADR/*/3", Mandatory = false)]
	public string? PlaceLocation { get; set; }
}