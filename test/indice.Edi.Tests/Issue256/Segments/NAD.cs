using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the name/address and their related function, either by CO82 only and/or unstructured by CO58 or structured by CO80 thru 3207.
/// </summary>
[EdiSegment, EdiPath("NAD")]
public class NAD
{
	/// <summary>
	/// Code giving specific meaning to a party.
	/// </summary>
	[EdiValue("X(3)", Path = "NAD/0", Mandatory = true)]
	public PartyQualifier? PartyQualifier { get; set; }

	/// <summary>
	/// Identification of a transaction party by code.
	/// </summary>
	[EdiPath("NAD/1")]
	public NAD_PartyIdentificationDetails? PartyIdentificationDetails { get; set; }

	/// <summary>
	/// Unstructured name and address: one to five lines.
	/// </summary>
	[EdiPath("NAD/2")]
	public NAD_NameAndAddress? NameAndAddress { get; set; }

	/// <summary>
	/// Identification of a transaction party by name, one to five lines. Party name may be formatted.
	/// </summary>
	[EdiPath("NAD/3")]
	public NAD_PartyName? PartyName { get; set; }

	/// <summary>
	/// Street address and/or PO Box number in a structured address: one to three lines.
	/// </summary>
	[EdiPath("NAD/4")]
	public NAD_Street? Street { get; set; }

	/// <summary>
	/// Name of a city (a town, a village) for addressing purposes.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/5", Mandatory = false)]
	public string? CityName { get; set; }

	/// <summary>
	/// Identification of the name of sub-entities (state, province) defined by appropriate governmental agencies.
	/// </summary>
	[EdiValue("X(9)", Path = "NAD/6", Mandatory = false)]
	public string? CountrySubentityIdentification { get; set; }

	/// <summary>
	/// Code defining postal zones or addresses.
	/// </summary>
	[EdiValue("X(9)", Path = "NAD/7", Mandatory = false)]
	public string? PostcodeIdentification { get; set; }

	/// <summary>
	/// Identification of the name of a country or other geographical entity as specified in ISO 3166.
	/// </summary>
	[EdiValue("X(3)", Path = "NAD/8", Mandatory = false)]
	public string? CountryCoded { get; set; }

}

/// <summary>
/// Identification of a transaction party by code.
/// </summary>
[EdiElement]
public class NAD_PartyIdentificationDetails
{
	/// <summary>
	/// Code identifying a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/0", Mandatory = true)]
	public string? PartyIdIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "NAD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "NAD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Unstructured name and address: one to five lines.
/// </summary>
[EdiElement]
public class NAD_NameAndAddress
{
	/// <summary>
	/// Free form name and address description.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/0", Mandatory = true)]
	public string? NameAndAddressLine1 { get; set; }

	/// <summary>
	/// Free form name and address description.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/1", Mandatory = false)]
	public string? NameAndAddressLine2 { get; set; }

	/// <summary>
	/// Free form name and address description.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/2", Mandatory = false)]
	public string? NameAndAddressLine3 { get; set; }

	/// <summary>
	/// Free form name and address description.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/3", Mandatory = false)]
	public string? NameAndAddressLine4 { get; set; }

	/// <summary>
	/// Free form name and address description.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/4", Mandatory = false)]
	public string? NameAndAddressLine5 { get; set; }
}

/// <summary>
/// Identification of a transaction party by name, one to five lines. Party name may be formatted.
/// </summary>
[EdiElement]
public class NAD_PartyName
{
	/// <summary>
	/// Name of a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/0", Mandatory = true)]
	public string? PartyName1 { get; set; }

	/// <summary>
	/// Name of a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/1", Mandatory = false)]
	public string? PartyName2 { get; set; }

	/// <summary>
	/// Name of a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/2", Mandatory = false)]
	public string? PartyName3 { get; set; }

	/// <summary>
	/// Name of a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/3", Mandatory = false)]
	public string? PartyName4 { get; set; }

	/// <summary>
	/// Name of a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/4", Mandatory = false)]
	public string? PartyName5 { get; set; }

	/// <summary>
	/// Specification of the representation of a party name.
	/// </summary>
	[EdiValue("X(3)", Path = "NAD/*/5", Mandatory = false)]
	public PartyNameFormatCoded? PartyNameFormatCoded { get; set; }
}

/// <summary>
/// Street address and/or PO Box number in a structured address: one to three lines.
/// </summary>
[EdiElement]
public class NAD_Street
{
	/// <summary>
	/// Street and number in plain language, or Post Office Box No.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/0", Mandatory = true)]
	public string? StreetAndNumberPoBox1 { get; set; }

	/// <summary>
	/// Street and number in plain language, or Post Office Box No.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/1", Mandatory = false)]
	public string? StreetAndNumberPoBox2 { get; set; }

	/// <summary>
	/// Street and number in plain language, or Post Office Box No.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/2", Mandatory = false)]
	public string? StreetAndNumberPoBox3 { get; set; }

	/// <summary>
	/// Street and number in plain language, or Post Office Box No.
	/// </summary>
	[EdiValue("X(35)", Path = "NAD/*/3", Mandatory = false)]
	public string? StreetAndNumberPoBox4 { get; set; }
}