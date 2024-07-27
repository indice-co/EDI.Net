using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide component details of an object (e.g. product, container) such as its type and the material of which it is composed.
/// </summary>
[EdiSegment, EdiPath("COD")]
public class COD
{
	/// <summary>
	/// To identify the type of unit/component of an object (e.g. lock, door, tyre).
	/// </summary>
	[EdiPath("COD/0")]
	public COD_TypeOfUnitComponent? TypeOfUnitComponent { get; set; }

	/// <summary>
	/// To identify the material of which a component is composed (e.g. steel, plastics).
	/// </summary>
	[EdiPath("COD/1")]
	public COD_ComponentMaterial? ComponentMaterial { get; set; }
}

/// <summary>
/// To identify the type of unit/component of an object (e.g. lock, door, tyre).
/// </summary>
[EdiElement]
public class COD_TypeOfUnitComponent
{
	/// <summary>
	/// Code identifying the type of unit/component of an object (e.g. lock, door, tyre).
	/// </summary>
	[EdiValue("X(3)", Path = "COD/*/0", Mandatory = false)]
	public string? TypeOfUnitComponentCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description identifying the type of unit/component of an object (e.g. lock, door, tyre).
	/// </summary>
	[EdiValue("X(35)", Path = "COD/*/3", Mandatory = false)]
	public string? TypeOfUnitComponent { get; set; }
}

/// <summary>
/// To identify the material of which a component is composed (e.g. steel, plastics).
/// </summary>
[EdiElement]
public class COD_ComponentMaterial
{
	/// <summary>
	/// Code identifying the material of which a component is composed (e.g. steel, plastics).
	/// </summary>
	[EdiValue("X(3)", Path = "COD/*/0", Mandatory = false)]
	public string? ComponentMaterialCoded { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COD/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "COD/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Description identifying the material of which a component is composed (e.g. steel, plastics).
	/// </summary>
	[EdiValue("X(35)", Path = "COD/*/3", Mandatory = false)]
	public string? ComponentMaterial { get; set; }
}