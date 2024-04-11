using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify a directory and to give its release, status, controlling agency, language and maintenance operation.
/// </summary>
[EdiSegment, EdiPath("DII")]
public class DII
{
	/// <summary>
	/// To specify the version number or name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "DII/0", Mandatory = true)]
	public string? Version { get; set; }

	/// <summary>
	/// To specify the release number or release name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "DII/1", Mandatory = true)]
	public string? Release { get; set; }

	/// <summary>
	/// The status of a directory set.
	/// </summary>
	[EdiValue("X(3)", Path = "DII/2", Mandatory = false)]
	public string? DirectoryStatus { get; set; }

	/// <summary>
	/// Identification of the agency controlling the specification, maintenance and publication of the message.
	/// </summary>
	[EdiValue("X(2)", Path = "DII/3", Mandatory = false)]
	public string? ControlAgency { get; set; }

	/// <summary>
	/// Code of language (ISO 639-1988).
	/// </summary>
	[EdiValue("X(3)", Path = "DII/4", Mandatory = false)]
	public string? LanguageCoded { get; set; }

	/// <summary>
	/// To indicate the type of data maintenance operation for an object, such as add, delete, replace.
	/// </summary>
	[EdiValue("X(3)", Path = "DII/5", Mandatory = false)]
	public MaintenanceOperationCoded? MaintenanceOperationCoded { get; set; }

}