using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify markings and labels on individual packages or physical units.
/// </summary>
[EdiSegment, EdiPath("PCI")]
public class PCI
{
	/// <summary>
	/// Code indicating instructions on how specified packages or physical units should be marked.
	/// </summary>
	[EdiValue("X(3)", Path = "PCI/0", Mandatory = false)]
	public MarkingInstructionsCoded? MarkingInstructionsCoded { get; set; }

	/// <summary>
	/// Shipping marks on packages in free text; one to ten lines.
	/// </summary>
	[EdiPath("PCI/1")]
	public PCI_MarksLabels? MarksLabels { get; set; }

	/// <summary>
	/// Code to identify whether goods of separate description or comprising separate consignments are contained in the same external packaging or to indicate that a container or similar unit load device is empty.
	/// </summary>
	[EdiValue("X(3)", Path = "PCI/2", Mandatory = false)]
	public ContainerPackageStatusCoded? ContainerPackageStatusCoded { get; set; }

	/// <summary>
	/// Specification of the type of marking that reflects the method that was used and the conventions adhered to for marking (e.g. of packages).
	/// </summary>
	[EdiPath("PCI/3")]
	public PCI_TypeOfMarking? TypeOfMarking { get; set; }
}

/// <summary>
/// Shipping marks on packages in free text; one to ten lines.
/// </summary>
[EdiElement]
public class PCI_MarksLabels
{
	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/0", Mandatory = true)]
	public string? ShippingMarks1 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/1", Mandatory = false)]
	public string? ShippingMarks2 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/2", Mandatory = false)]
	public string? ShippingMarks3 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/3", Mandatory = false)]
	public string? ShippingMarks4 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/4", Mandatory = false)]
	public string? ShippingMarks5 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/5", Mandatory = false)]
	public string? ShippingMarks6 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/6", Mandatory = false)]
	public string? ShippingMarks7 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/7", Mandatory = false)]
	public string? ShippingMarks8 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/8", Mandatory = false)]
	public string? ShippingMarks9 { get; set; }

	/// <summary>
	/// Marks and numbers identifying individual packages.
	/// </summary>
	[EdiValue("X(35)", Path = "PCI/*/9", Mandatory = false)]
	public string? ShippingMarks10 { get; set; }
}

/// <summary>
/// Specification of the type of marking that reflects the method that was used and the conventions adhered to for marking (e.g. of packages).
/// </summary>
[EdiElement]
public class PCI_TypeOfMarking
{
	/// <summary>
	/// To specify the type of marking that reflects the method and the conventions adhered to for marking.
	/// </summary>
	[EdiValue("X(3)", Path = "PCI/*/0", Mandatory = true)]
	public string? TypeOfMarkingCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PCI/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "PCI/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}