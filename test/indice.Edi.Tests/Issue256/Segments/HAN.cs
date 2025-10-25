using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify handling and where necessary, notify hazards.
/// </summary>
[EdiSegment, EdiPath("HAN")]
public class HAN
{
	/// <summary>
	/// Instruction for the handling of goods, products or articles in shipment, storage etc.
	/// </summary>
	[EdiPath("HAN/0")]
	public HAN_HandlingInstructions? HandlingInstructions { get; set; }

	/// <summary>
	/// To specify a hazardous material.
	/// </summary>
	[EdiPath("HAN/1")]
	public HAN_HazardousMaterial? HazardousMaterial { get; set; }
}

/// <summary>
/// Instruction for the handling of goods, products or articles in shipment, storage etc.
/// </summary>
[EdiElement]
public class HAN_HandlingInstructions
{
	/// <summary>
	/// Identification of the instructions on how specified goods, packages or containers should be handled.
	/// </summary>
	[EdiValue("X(3)", Path = "HAN/*/0", Mandatory = false)]
	public string? HandlingInstructionsCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "HAN/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "HAN/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Instructions on how specified goods, packages or containers should be handled.
	/// </summary>
	[EdiValue("X(70)", Path = "HAN/*/3", Mandatory = false)]
	public string? HandlingInstructions { get; set; }
}

/// <summary>
/// To specify a hazardous material.
/// </summary>
[EdiElement]
public class HAN_HazardousMaterial
{
	/// <summary>
	/// Code specifying the kind of hazard for a material.
	/// </summary>
	[EdiValue("X(4)", Path = "HAN/*/0", Mandatory = false)]
	public string? HazardousMaterialClassCodeIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "HAN/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "HAN/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// To specify the kind of hazard for a material.
	/// </summary>
	[EdiValue("X(35)", Path = "HAN/*/3", Mandatory = false)]
	public string? HazardousMaterialClass { get; set; }
}