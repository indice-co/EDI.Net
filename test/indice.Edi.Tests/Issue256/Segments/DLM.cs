using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify limitations on deliveries.
/// </summary>
[EdiSegment, EdiPath("DLM")]
public class DLM
{
	/// <summary>
	/// Code to identify the back order agreement.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/0", Mandatory = false)]
	public BackOrderCoded? BackOrderCoded { get; set; }

	/// <summary>
	/// To specify an instruction.
	/// </summary>
	[EdiPath("DLM/1")]
	public DLM_Instruction? Instruction { get; set; }

	/// <summary>
	/// Identification of a special service by a code from a specified source or by description.
	/// </summary>
	[EdiPath("DLM/2")]
	public DLM_SpecialServicesIdentification? SpecialServicesIdentification { get; set; }

	/// <summary>
	/// Code indicating product or service substitution conditions.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/3", Mandatory = false)]
	public ProductServiceSubstitutionCoded? ProductServiceSubstitutionCoded { get; set; }

}

/// <summary>
/// To specify an instruction.
/// </summary>
[EdiElement]
public class DLM_Instruction
{
	/// <summary>
	/// Code giving specific meaning to the type of instructions.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/0", Mandatory = true)]
	public InstructionQualifier? InstructionQualifier { get; set; }

	/// <summary>
	/// Specification of an action to be taken by the receiver of the message.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/1", Mandatory = false)]
	public InstructionCoded? InstructionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of an instruction.
	/// </summary>
	[EdiValue("X(35)", Path = "DLM/*/4", Mandatory = false)]
	public string? Instruction { get; set; }
}

/// <summary>
/// Identification of a special service by a code from a specified source or by description.
/// </summary>
[EdiElement]
public class DLM_SpecialServicesIdentification
{
	/// <summary>
	/// Code identifying a special service.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/0", Mandatory = false)]
	public SpecialServicesCoded? SpecialServicesCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "DLM/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of a special service.
	/// </summary>
	[EdiValue("X(35)", Path = "DLM/*/3", Mandatory = false)]
	public string? SpecialService1 { get; set; }

	/// <summary>
	/// Description of a special service.
	/// </summary>
	[EdiValue("X(35)", Path = "DLM/*/4", Mandatory = false)]
	public string? SpecialService2 { get; set; }
}