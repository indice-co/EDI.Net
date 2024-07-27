using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To describe the number and type of packages/physical units.
/// </summary>
[EdiSegment, EdiPath("PAC")]
public class PAC
{
	/// <summary>
	/// Number of individual parts of a shipment either unpacked, or packed in such a way that they cannot be divided without first undoing the packing.
	/// </summary>
	[EdiValue("9(8)", Path = "PAC/0", Mandatory = false)]
	public decimal? NumberOfPackages { get; set; }

	/// <summary>
	/// Packaging level and details, terms and conditions.
	/// </summary>
	[EdiPath("PAC/1")]
	public PAC_PackagingDetails? PackagingDetails { get; set; }

	/// <summary>
	/// Type of package by name or by code from a specified source.
	/// </summary>
	[EdiPath("PAC/2")]
	public PAC_PackageType? PackageType { get; set; }

	/// <summary>
	/// Identification of the form in which goods are described.
	/// </summary>
	[EdiPath("PAC/3")]
	public PAC_PackageTypeIdentification? PackageTypeIdentification { get; set; }

	/// <summary>
	/// Indication of responsibility for payment and load contents of returnable packages.
	/// </summary>
	[EdiPath("PAC/4")]
	public PAC_ReturnablePackageDetails? ReturnablePackageDetails { get; set; }
}

/// <summary>
/// Packaging level and details, terms and conditions.
/// </summary>
[EdiElement]
public class PAC_PackagingDetails
{
	/// <summary>
	/// Indication of level of packaging specified.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/0", Mandatory = false)]
	public PackagingLevelCoded? PackagingLevelCoded { get; set; }

	/// <summary>
	/// Code giving packaging, handling and marking related information.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/1", Mandatory = false)]
	public PackagingRelatedInformationCoded? PackagingRelatedInformationCoded { get; set; }

	/// <summary>
	/// Code identifying packaging terms and conditions.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/2", Mandatory = false)]
	public PackagingTermsAndConditionsCoded? PackagingTermsAndConditionsCoded { get; set; }
}

/// <summary>
/// Type of package by name or by code from a specified source.
/// </summary>
[EdiElement]
public class PAC_PackageType
{
	/// <summary>
	/// Coded description of the form in which goods are presented.
	/// </summary>
	[EdiValue("X(17)", Path = "PAC/*/0", Mandatory = false)]
	public string? TypeOfPackagesIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of the form in which goods are presented.
	/// </summary>
	[EdiValue("X(35)", Path = "PAC/*/3", Mandatory = false)]
	public string? TypeOfPackages { get; set; }
}

/// <summary>
/// Identification of the form in which goods are described.
/// </summary>
[EdiElement]
public class PAC_PackageTypeIdentification
{
	/// <summary>
	/// Code indicating the format of a description.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/0", Mandatory = true)]
	public ItemDescriptionTypeCoded? ItemDescriptionTypeCoded { get; set; }

	/// <summary>
	/// Description of the form in which goods are presented.
	/// </summary>
	[EdiValue("X(35)", Path = "PAC/*/1", Mandatory = true)]
	public string? TypeOfPackages1 { get; set; }

	/// <summary>
	/// Identification of the type of item number.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/2", Mandatory = false)]
	public ItemNumberTypeCoded? ItemNumberTypeCoded1 { get; set; }

	/// <summary>
	/// Description of the form in which goods are presented.
	/// </summary>
	[EdiValue("X(35)", Path = "PAC/*/3", Mandatory = false)]
	public string? TypeOfPackages2 { get; set; }

	/// <summary>
	/// Identification of the type of item number.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/4", Mandatory = false)]
	public ItemNumberTypeCoded? ItemNumberTypeCoded2 { get; set; }
}

/// <summary>
/// Indication of responsibility for payment and load contents of returnable packages.
/// </summary>
[EdiElement]
public class PAC_ReturnablePackageDetails
{
	/// <summary>
	/// To indicate responsibility for payment of return freight charges for packaging that is returnable.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/0", Mandatory = false)]
	public ReturnablePackageFreightPaymentResponsibilityCoded? ReturnablePackageFreightPaymentResponsibilityCoded { get; set; }

	/// <summary>
	/// To indicate the composition of goods loaded into a returnable package.
	/// </summary>
	[EdiValue("X(3)", Path = "PAC/*/1", Mandatory = false)]
	public ReturnablePackageLoadContentsCoded? ReturnablePackageLoadContentsCoded { get; set; }
}