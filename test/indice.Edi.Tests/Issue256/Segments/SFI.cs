using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify regulatory safety information.
/// </summary>
[EdiSegment, EdiPath("SFI")]
public class SFI
{
	/// <summary>
	/// A unique number assigned by the sender to identify a level within a hierarchical structure.
	/// </summary>
	[EdiValue("X(12)", Path = "SFI/0", Mandatory = true)]
	public string? HierarchicalIdNumber { get; set; }

	/// <summary>
	/// To identify the safety section to which information relates.
	/// </summary>
	[EdiPath("SFI/1")]
	public SFI_SafetySection? SafetySection { get; set; }

	/// <summary>
	/// To identify additional safety information.
	/// </summary>
	[EdiPath("SFI/2")]
	public SFI_AdditionalSafetyInformation? AdditionalSafetyInformation { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "SFI/3", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}

/// <summary>
/// To identify the safety section to which information relates.
/// </summary>
[EdiElement]
public class SFI_SafetySection
{
	/// <summary>
	/// To identify the safety section number.
	/// </summary>
	[EdiValue("9(2)", Path = "SFI/*/0", Mandatory = true)]
	public decimal? SafetySectionNumber { get; set; }

	/// <summary>
	/// To identify the safety section name.
	/// </summary>
	[EdiValue("X(70)", Path = "SFI/*/1", Mandatory = false)]
	public string? SafetySectionName { get; set; }
}

/// <summary>
/// To identify additional safety information.
/// </summary>
[EdiElement]
public class SFI_AdditionalSafetyInformation
{
	/// <summary>
	/// Element to identify the additional safety information in coded form.
	/// </summary>
	[EdiValue("X(3)", Path = "SFI/*/0", Mandatory = true)]
	public string? AdditionalSafetyInformationCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SFI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "SFI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Element to enable additional safety information to be specified as free text.
	/// </summary>
	[EdiValue("X(35)", Path = "SFI/*/3", Mandatory = false)]
	public string? AdditionalSafetyInformation { get; set; }
}