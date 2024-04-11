using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the contract and carriage conditions and service and priority requirements for the transport.
/// </summary>
[EdiSegment, EdiPath("TSR")]
public class TSR
{
	/// <summary>
	/// To identify a contract and carriage condition.
	/// </summary>
	[EdiPath("TSR/0")]
	public TSR_ContractAndCarriageCondition? ContractAndCarriageCondition { get; set; }

	/// <summary>
	/// To identify a service (which may constitute an additional component to a basic contract).
	/// </summary>
	[EdiPath("TSR/1")]
	public TSR_Service? Service { get; set; }

	/// <summary>
	/// To indicate the priority of requested transport service.
	/// </summary>
	[EdiPath("TSR/2")]
	public TSR_TransportPriority? TransportPriority { get; set; }

	/// <summary>
	/// Rough classification of a type of cargo.
	/// </summary>
	[EdiPath("TSR/3")]
	public TSR_NatureOfCargo? NatureOfCargo { get; set; }
}

/// <summary>
/// To identify a contract and carriage condition.
/// </summary>
[EdiElement]
public class TSR_ContractAndCarriageCondition
{
	/// <summary>
	/// Code to identify the conditions of contract and carriage.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/0", Mandatory = true)]
	public ContractAndCarriageConditionCoded? ContractAndCarriageConditionCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// To identify a service (which may constitute an additional component to a basic contract).
/// </summary>
[EdiElement]
public class TSR_Service
{
	/// <summary>
	/// Identification of a service requirement (which may constitute an additional component to a basic contract).
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/0", Mandatory = true)]
	public ServiceRequirementCoded? ServiceRequirementCoded1 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier1 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded1 { get; set; }

	/// <summary>
	/// Identification of a service requirement (which may constitute an additional component to a basic contract).
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/3", Mandatory = false)]
	public ServiceRequirementCoded? ServiceRequirementCoded2 { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/4", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier2 { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/5", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded2 { get; set; }
}

/// <summary>
/// To indicate the priority of requested transport service.
/// </summary>
[EdiElement]
public class TSR_TransportPriority
{
	/// <summary>
	/// Coded priority of requested transport service.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/0", Mandatory = true)]
	public TransportPriorityCoded? TransportPriorityCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Rough classification of a type of cargo.
/// </summary>
[EdiElement]
public class TSR_NatureOfCargo
{
	/// <summary>
	/// Code indicating the type of cargo as a rough classification.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/0", Mandatory = true)]
	public NatureOfCargoCoded? NatureOfCargoCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TSR/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}