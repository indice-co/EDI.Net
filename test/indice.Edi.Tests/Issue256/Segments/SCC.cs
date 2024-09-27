using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify scheduling conditions.
/// </summary>
[EdiSegment, EdiPath("SCC")]
public class SCC
{
	/// <summary>
	/// Code indicating the level of commitment of schedule information.
	/// </summary>
	[EdiValue("X(3)", Path = "SCC/0", Mandatory = true)]
	public DeliveryPlanStatusIndicatorCoded? DeliveryPlanStatusIndicatorCoded { get; set; }

	/// <summary>
	/// Indication of general instruction for delivery.
	/// </summary>
	[EdiValue("X(3)", Path = "SCC/1", Mandatory = false)]
	public DeliveryRequirementsCoded? DeliveryRequirementsCoded { get; set; }

	/// <summary>
	/// Shipment, delivery or production interval pattern and timing.
	/// </summary>
	[EdiPath("SCC/2")]
	public SCC_PatternDescription? PatternDescription { get; set; }
}

/// <summary>
/// Shipment, delivery or production interval pattern and timing.
/// </summary>
[EdiElement]
public class SCC_PatternDescription
{
	/// <summary>
	/// Code specifying interval grouping of the delivery, production, etc. of the schedule.
	/// </summary>
	[EdiValue("X(3)", Path = "SCC/*/0", Mandatory = false)]
	public FrequencyCoded? FrequencyCoded { get; set; }

	/// <summary>
	/// Code specifying the days/periods for routine shipments or deliveries.
	/// </summary>
	[EdiValue("X(3)", Path = "SCC/*/1", Mandatory = false)]
	public DespatchPatternCoded? DespatchPatternCoded { get; set; }

	/// <summary>
	/// Code specifying the detailed times for routine shipments or deliveries within data element 2015 Despatch pattern, coded.
	/// </summary>
	[EdiValue("X(3)", Path = "SCC/*/2", Mandatory = false)]
	public DespatchPatternTimingCoded? DespatchPatternTimingCoded { get; set; }
}