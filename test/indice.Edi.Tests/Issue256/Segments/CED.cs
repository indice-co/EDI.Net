using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To give a precise definition of all necessary elements belonging to the configuration of a computer system like hardware, firmware, operating system, communication (VANS, network type, protocol, format) and application software.
/// </summary>
[EdiSegment, EdiPath("CED")]
public class CED
{
	/// <summary>
	/// A code to identify the computer environment details.
	/// </summary>
	[EdiValue("X(3)", Path = "CED/0", Mandatory = true)]
	public ComputerEnvironmentDetailsQualifier? ComputerEnvironmentDetailsQualifier { get; set; }

	/// <summary>
	/// To identify parts of a computer environment.
	/// </summary>
	[EdiPath("CED/1")]
	public CED_ComputerEnvironmentIdentification? ComputerEnvironmentIdentification { get; set; }
}

/// <summary>
/// To identify parts of a computer environment.
/// </summary>
[EdiElement]
public class CED_ComputerEnvironmentIdentification
{
	/// <summary>
	/// A code to identify the computer environment.
	/// </summary>
	[EdiValue("X(3)", Path = "CED/*/0", Mandatory = false)]
	public string? ComputerEnvironmentCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CED/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "CED/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To define the product name of the specified computer environment part.
	/// </summary>
	[EdiValue("X(35)", Path = "CED/*/3", Mandatory = false)]
	public string? ComputerEnvironment { get; set; }

	/// <summary>
	/// To specify the version number or name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "CED/*/4", Mandatory = false)]
	public string? Version { get; set; }

	/// <summary>
	/// To specify the release number or release name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "CED/*/5", Mandatory = false)]
	public string? Release { get; set; }

	/// <summary>
	/// The number given to an object for its unique identification.
	/// </summary>
	[EdiValue("X(35)", Path = "CED/*/6", Mandatory = false)]
	public string? IdentityNumber { get; set; }
}