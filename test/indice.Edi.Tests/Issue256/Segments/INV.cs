using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide the different information related to the inventory management functions and needed to process properly the inventory movements and the inventory balances.
/// </summary>
[EdiSegment, EdiPath("INV")]
public class INV
{
	/// <summary>
	/// To specify the direction of inventory movement.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/0", Mandatory = false)]
	public InventoryMovementDirectionCoded? InventoryMovementDirectionCoded { get; set; }

	/// <summary>
	/// To specify the type of inventory which is affected by an inventory movement or expressed in an inventory balance.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/1", Mandatory = false)]
	public TypeOfInventoryAffectedCoded? TypeOfInventoryAffectedCoded { get; set; }

	/// <summary>
	/// To explain the reason for the inventory movement.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/2", Mandatory = false)]
	public ReasonForInventoryMovementCoded? ReasonForInventoryMovementCoded { get; set; }

	/// <summary>
	/// To specify the method used to establish an inventory balance.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/3", Mandatory = false)]
	public InventoryBalanceMethodCoded? InventoryBalanceMethodCoded { get; set; }

	/// <summary>
	/// To specify an instruction.
	/// </summary>
	[EdiPath("INV/4")]
	public INV_Instruction? Instruction { get; set; }
}

/// <summary>
/// To specify an instruction.
/// </summary>
[EdiElement]
public class INV_Instruction
{
	/// <summary>
	/// Code giving specific meaning to the type of instructions.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/*/0", Mandatory = true)]
	public InstructionQualifier? InstructionQualifier { get; set; }

	/// <summary>
	/// Specification of an action to be taken by the receiver of the message.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/*/1", Mandatory = false)]
	public InstructionCoded? InstructionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/*/2", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "INV/*/3", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description of an instruction.
	/// </summary>
	[EdiValue("X(35)", Path = "INV/*/4", Mandatory = false)]
	public string? Instruction { get; set; }
}