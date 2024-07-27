using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To identify dangerous goods.
/// </summary>
[EdiSegment, EdiPath("DGS")]
public class DGS
{
	/// <summary>
	/// Code indicating the regulation, international or national, applicable for a means of transport.
	/// </summary>
	[EdiValue("X(3)", Path = "DGS/0", Mandatory = false)]
	public DangerousGoodsRegulationsCoded? DangerousGoodsRegulationsCoded { get; set; }

	/// <summary>
	/// The identification of the dangerous goods in code.
	/// </summary>
	[EdiPath("DGS/1")]
	public DGS_HazardCode? HazardCode { get; set; }

	/// <summary>
	/// Information on United Nations Dangerous Goods classification.
	/// </summary>
	[EdiPath("DGS/2")]
	public DGS_UndgInformation? UndgInformation { get; set; }

	/// <summary>
	/// Temperature at which a vapor according to ISO 1523/73 can be ignited.
	/// </summary>
	[EdiPath("DGS/3")]
	public DGS_DangerousGoodsShipmentFlashpoint? DangerousGoodsShipmentFlashpoint { get; set; }

	/// <summary>
	/// Identification of a packing group by code.
	/// </summary>
	[EdiValue("X(3)", Path = "DGS/4", Mandatory = false)]
	public PackingGroupCoded? PackingGroupCoded { get; set; }

	/// <summary>
	/// Emergency procedures for ships carrying dangerous goods.
	/// </summary>
	[EdiValue("X(6)", Path = "DGS/5", Mandatory = false)]
	public string? EmsNumber { get; set; }

	/// <summary>
	/// Medical first aid guide.
	/// </summary>
	[EdiValue("X(4)", Path = "DGS/6", Mandatory = false)]
	public string? Mfag { get; set; }

	/// <summary>
	/// The identification of a transport emergency card giving advice for emergency actions.
	/// </summary>
	[EdiValue("X(10)", Path = "DGS/7", Mandatory = false)]
	public string? TremCardNumber { get; set; }

	/// <summary>
	/// Identification of the Orange placard required on the means of transport.
	/// </summary>
	[EdiPath("DGS/8")]
	public DGS_HazardIdentification? HazardIdentification { get; set; }

	/// <summary>
	/// Markings identifying the type of hazardous goods and similar information.
	/// </summary>
	[EdiPath("DGS/9")]
	public DGS_DangerousGoodsLabel? DangerousGoodsLabel { get; set; }

	/// <summary>
	/// Code defining the quantity and the type of package in which a product is allowed to be shipped in a passenger or freight aircraft.
	/// </summary>
	[EdiValue("X(3)", Path = "DGS/10", Mandatory = false)]
	public string? PackingInstructionCoded { get; set; }

	/// <summary>
	/// Identification of the type of means of transport determined to carry particular goods, not necessarily being hazardous.
	/// </summary>
	[EdiValue("X(3)", Path = "DGS/11", Mandatory = false)]
	public CategoryOfMeansOfTransportCoded? CategoryOfMeansOfTransportCoded { get; set; }

	/// <summary>
	/// Code giving evidence that transportation of particular hazardous cargo is permitted and identifies the restrictions being put upon a particular transport.
	/// </summary>
	[EdiValue("X(3)", Path = "DGS/12", Mandatory = false)]
	public string? PermissionForTransportCoded { get; set; }

}

/// <summary>
/// The identification of the dangerous goods in code.
/// </summary>
[EdiElement]
public class DGS_HazardCode
{
	/// <summary>
	/// Dangerous goods code.
	/// </summary>
	[EdiValue("X(7)", Path = "DGS/*/0", Mandatory = true)]
	public string? HazardCodeIdentification { get; set; }

	/// <summary>
	/// Number giving additional hazard code classification of a goods item within the applicable dangerous goods regulation.
	/// </summary>
	[EdiValue("X(7)", Path = "DGS/*/1", Mandatory = false)]
	public string? HazardSubstanceItemPageNumber { get; set; }

	/// <summary>
	/// The version/revision number of date of issuance of the code used.
	/// </summary>
	[EdiValue("X(10)", Path = "DGS/*/2", Mandatory = false)]
	public string? HazardCodeVersionNumber { get; set; }
}

/// <summary>
/// Information on United Nations Dangerous Goods classification.
/// </summary>
[EdiElement]
public class DGS_UndgInformation
{
	/// <summary>
	/// Unique serial number assigned within the United Nations to substances and articles contained in a list of the dangerous goods most commonly carried.
	/// </summary>
	[EdiValue("9(4)", Path = "DGS/*/0", Mandatory = false)]
	public decimal? UndgNumber { get; set; }

	/// <summary>
	/// Lowest temperature, in the case of dangerous goods, at which vapour from an inflammable liquid forms an ignitable mixture with air.
	/// </summary>
	[EdiValue("X(8)", Path = "DGS/*/1", Mandatory = false)]
	public string? DangerousGoodsFlashpoint { get; set; }
}

/// <summary>
/// Temperature at which a vapor according to ISO 1523/73 can be ignited.
/// </summary>
[EdiElement]
public class DGS_DangerousGoodsShipmentFlashpoint
{
	/// <summary>
	/// Temperature in centigrade determined by the closed cup test as per ISO 1523/73 where a vapour is given off that can be ignited.
	/// </summary>
	[EdiValue("9(3)", Path = "DGS/*/0", Mandatory = false)]
	public decimal? ShipmentFlashpoint { get; set; }

	/// <summary>
	/// Indication of the unit of measurement in which weight (mass), capacity, length, area, volume or other quantity is expressed.
	/// </summary>
	[EdiValue("X(3)", Path = "DGS/*/1", Mandatory = false)]
	public string? MeasureUnitQualifier { get; set; }
}

/// <summary>
/// Identification of the Orange placard required on the means of transport.
/// </summary>
[EdiElement]
public class DGS_HazardIdentification
{
	/// <summary>
	/// The id. number for the Orange Placard (upper part) required on the means of transport.
	/// </summary>
	[EdiValue("X(4)", Path = "DGS/*/0", Mandatory = false)]
	public string? HazardIdentificationNumberUpperPart { get; set; }

	/// <summary>
	/// The number for the Orange Placard (lower part) required on the means of transport.
	/// </summary>
	[EdiValue("X(4)", Path = "DGS/*/1", Mandatory = false)]
	public string? SubstanceIdentificationNumberLowerPart { get; set; }
}

/// <summary>
/// Markings identifying the type of hazardous goods and similar information.
/// </summary>
[EdiElement]
public class DGS_DangerousGoodsLabel
{
	/// <summary>
	/// Marking identifying the type of hazardous goods (substance), Loading/Unloading instructions and advising actions in case of emergency.
	/// </summary>
	[EdiValue("X(4)", Path = "DGS/*/0", Mandatory = false)]
	public string? DangerousGoodsLabelMarking1 { get; set; }

	/// <summary>
	/// Marking identifying the type of hazardous goods (substance), Loading/Unloading instructions and advising actions in case of emergency.
	/// </summary>
	[EdiValue("X(4)", Path = "DGS/*/1", Mandatory = false)]
	public string? DangerousGoodsLabelMarking2 { get; set; }

	/// <summary>
	/// Marking identifying the type of hazardous goods (substance), Loading/Unloading instructions and advising actions in case of emergency.
	/// </summary>
	[EdiValue("X(4)", Path = "DGS/*/2", Mandatory = false)]
	public string? DangerousGoodsLabelMarking3 { get; set; }
}