using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a country/place/location/related location one/related location two.
/// </summary>
[EdiSegment, EdiPath("LOC")]
public class LOC
{
	/// <summary>
	/// Code identifying the function of a location.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/0", Mandatory = true)]
	public PlaceLocationQualifier? PlaceLocationQualifier { get; set; }

	/// <summary>
	/// Identification of a location by code or name.
	/// </summary>
	[EdiPath("LOC/1")]
	public LOC_LocationIdentification? LocationIdentification { get; set; }

	/// <summary>
	/// Identification the first related location by code or name.
	/// </summary>
	[EdiPath("LOC/2")]
	public LOC_RelatedLocationOneIdentification? RelatedLocationOneIdentification { get; set; }

	/// <summary>
	/// Identification of second related location by code or name.
	/// </summary>
	[EdiPath("LOC/3")]
	public LOC_RelatedLocationTwoIdentification? RelatedLocationTwoIdentification { get; set; }

	/// <summary>
	/// To specify the relationship between two or more items.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/4", Mandatory = false)]
	public string? RelationCoded { get; set; }

}

/// <summary>
/// Identification of a location by code or name.
/// </summary>
[EdiElement]
public class LOC_LocationIdentification
{
	/// <summary>
	/// Identification of the name of place/location, other than 3164 City name.
	/// </summary>
	[EdiValue("X(25)", Path = "LOC/*/0", Mandatory = false)]
	public string? PlaceLocationIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Name of place/location, other than 3164 City name.
	/// </summary>
	[EdiValue("X(70)", Path = "LOC/*/3", Mandatory = false)]
	public string? PlaceLocation { get; set; }
}

/// <summary>
/// Identification the first related location by code or name.
/// </summary>
[EdiElement]
public class LOC_RelatedLocationOneIdentification
{
	/// <summary>
	/// Specification of the first related place/location by code.
	/// </summary>
	[EdiValue("X(25)", Path = "LOC/*/0", Mandatory = false)]
	public string? RelatedPlaceLocationOneIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Specification of the first related place/location by name.
	/// </summary>
	[EdiValue("X(70)", Path = "LOC/*/3", Mandatory = false)]
	public string? RelatedPlaceLocationOne { get; set; }
}

/// <summary>
/// Identification of second related location by code or name.
/// </summary>
[EdiElement]
public class LOC_RelatedLocationTwoIdentification
{
	/// <summary>
	/// Specification of a second related place/location by code.
	/// </summary>
	[EdiValue("X(25)", Path = "LOC/*/0", Mandatory = false)]
	public string? RelatedPlaceLocationTwoIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "LOC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Specification of a second related place/location by name.
	/// </summary>
	[EdiValue("X(70)", Path = "LOC/*/3", Mandatory = false)]
	public string? RelatedPlaceLocationTwo { get; set; }
}