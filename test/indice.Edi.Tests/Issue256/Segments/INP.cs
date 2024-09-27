using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify parties to an instruction and where relevant, the instruction.
/// </summary>
[EdiSegment, EdiPath("INP")]
public class INP
{
	/// <summary>
	/// Identify the sending and receiving parties of the instruction.
	/// </summary>
	[EdiPath("INP/0")]
	public INP_PartiesToInstruction? PartiesToInstruction { get; set; }

	/// <summary>
	/// To specify an instruction.
	/// </summary>
	[EdiPath("INP/1")]
	public INP_Instruction? Instruction { get; set; }

	/// <summary>
	/// Provides information regarding the status of an instruction.
	/// </summary>
	[EdiPath("INP/2")]
	public INP_StatusOfInstruction? StatusOfInstruction { get; set; }

	/// <summary>
	/// Code specifying the action to be taken or already taken.
	/// </summary>
	[EdiValue("X(3)", Path = "INP/3", Mandatory = false)]
	public ActionRequestNotificationCoded? ActionRequestNotificationCoded { get; set; }

}

/// <summary>
/// Identify the sending and receiving parties of the instruction.
/// </summary>
[EdiElement]
public class INP_PartiesToInstruction
{
	/// <summary>
	/// Party responsible for carrying out the instruction.
	/// </summary>
	[EdiValue("X(17)", Path = "INP/*/0", Mandatory = true)]
	public PartyEnactingInstructionIdentification? PartyEnactingInstructionIdentification { get; set; }

	/// <summary>
	/// Party to whom the instruction is directed for advice or action.
	/// </summary>
	[EdiValue("X(17)", Path = "INP/*/1", Mandatory = false)]
	public RecipientOfTheInstructionIdentification? RecipientOfTheInstructionIdentification { get; set; }
}

/// <summary>
/// To specify an instruction.
/// </summary>
[EdiElement]
public class INP_Instruction
{
	/// <summary>
	/// Code giving specific meaning to the type of instructions.
	/// </summary>
	[EdiValue("X(3)", Path = "INP/*/0", Mandatory = true)]
	public InstructionQualifier? InstructionQualifier { get; set; }

	/// <summary>
	/// Specification of an action to be taken by the receiver of the message.
	/// </summary>
	[EdiValue("X(3)", Path = "INP/*/1", Mandatory = false)]
	public InstructionCoded? InstructionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "INP/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "INP/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of an instruction.
	/// </summary>
	[EdiValue("X(35)", Path = "INP/*/4", Mandatory = false)]
	public string? Instruction { get; set; }
}

/// <summary>
/// Provides information regarding the status of an instruction.
/// </summary>
[EdiElement]
public class INP_StatusOfInstruction
{
	/// <summary>
	/// Provides information regarding a status.
	/// </summary>
	[EdiValue("X(3)", Path = "INP/*/0", Mandatory = true)]
	public StatusCoded? StatusCoded { get; set; }

	/// <summary>
	/// Name of a party involved in a transaction.
	/// </summary>
	[EdiValue("X(35)", Path = "INP/*/1", Mandatory = false)]
	public string? PartyName { get; set; }
}