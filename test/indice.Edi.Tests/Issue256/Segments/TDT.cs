using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the transport details such as mode of transport, means of transport, its conveyance reference number and the identification of the means of transport. The segment may be pointed to by the TPL segment.
/// </summary>
[EdiSegment, EdiPath("TDT")]
public class TDT
{
	/// <summary>
	/// Qualifier giving a specific meaning to the transport details.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/0", Mandatory = true)]
	public TransportStageQualifier? TransportStageQualifier { get; set; }

	/// <summary>
	/// Unique reference given by the carrier to a certain journey or departure of a means of transport (generic term).
	/// </summary>
	[EdiValue("X(17)", Path = "TDT/1", Mandatory = false)]
	public string? ConveyanceReferenceNumber { get; set; }

	/// <summary>
	/// Method of transport code or name. Code preferred.
	/// </summary>
	[EdiPath("TDT/2")]
	public TDT_ModeOfTransport? ModeOfTransport { get; set; }

	/// <summary>
	/// Code and/or name identifying the type of means of transport.
	/// </summary>
	[EdiPath("TDT/3")]
	public TDT_TransportMeans? TransportMeans { get; set; }

	/// <summary>
	/// Identification of a carrier by code and/or by name. Code preferred.
	/// </summary>
	[EdiPath("TDT/4")]
	public TDT_Carrier? Carrier { get; set; }

	/// <summary>
	/// Identification of the point of origin and point of direction.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/5", Mandatory = false)]
	public TransitDirectionCoded? TransitDirectionCoded { get; set; }

	/// <summary>
	/// To provide details of reason for, and responsibility for, use of transportation other than normally utilized.
	/// </summary>
	[EdiPath("TDT/6")]
	public TDT_ExcessTransportationInformation? ExcessTransportationInformation { get; set; }

	/// <summary>
	/// Code and/or name identifying the means of transport.
	/// </summary>
	[EdiPath("TDT/7")]
	public TDT_TransportIdentification? TransportIdentification { get; set; }

	/// <summary>
	/// Code indicating the ownership of the means of transport.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/8", Mandatory = false)]
	public TransportOwnershipCoded? TransportOwnershipCoded { get; set; }

}

/// <summary>
/// Method of transport code or name. Code preferred.
/// </summary>
[EdiElement]
public class TDT_ModeOfTransport
{
	/// <summary>
	/// Coded method of transport used for the carriage of the goods.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/0", Mandatory = false)]
	public string? ModeOfTransportCoded { get; set; }

	/// <summary>
	/// Method of transport used for the carriage of the goods.
	/// </summary>
	[EdiValue("X(17)", Path = "TDT/*/1", Mandatory = false)]
	public string? ModeOfTransport { get; set; }
}

/// <summary>
/// Code and/or name identifying the type of means of transport.
/// </summary>
[EdiElement]
public class TDT_TransportMeans
{
	/// <summary>
	/// Code defining the type of the means of transport being utilized.
	/// </summary>
	[EdiValue("X(8)", Path = "TDT/*/0", Mandatory = false)]
	public TypeOfMeansOfTransportIdentification? TypeOfMeansOfTransportIdentification { get; set; }

	/// <summary>
	/// Description of the type of the means of transport being utilized.
	/// </summary>
	[EdiValue("X(17)", Path = "TDT/*/1", Mandatory = false)]
	public string? TypeOfMeansOfTransport { get; set; }
}

/// <summary>
/// Identification of a carrier by code and/or by name. Code preferred.
/// </summary>
[EdiElement]
public class TDT_Carrier
{
	/// <summary>
	/// Identification of party undertaking or arranging transport of goods between named points.
	/// </summary>
	[EdiValue("X(17)", Path = "TDT/*/0", Mandatory = false)]
	public string? CarrierIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Name of party undertaking or arranging transport of goods between named points.
	/// </summary>
	[EdiValue("X(35)", Path = "TDT/*/3", Mandatory = false)]
	public string? CarrierName { get; set; }
}

/// <summary>
/// To provide details of reason for, and responsibility for, use of transportation other than normally utilized.
/// </summary>
[EdiElement]
public class TDT_ExcessTransportationInformation
{
	/// <summary>
	/// Indication of reason for excess transportation.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/0", Mandatory = true)]
	public ExcessTransportationReasonCoded? ExcessTransportationReasonCoded { get; set; }

	/// <summary>
	/// Indication of responsibility for excess transportation.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/1", Mandatory = true)]
	public ExcessTransportationResponsibilityCoded? ExcessTransportationResponsibilityCoded { get; set; }

	/// <summary>
	/// Customer provided authorization number to allow supplier to ship goods under specific freight conditions.  This number will be transmitted back to customer in the dispatch advice message.
	/// </summary>
	[EdiValue("X(17)", Path = "TDT/*/2", Mandatory = false)]
	public string? CustomerAuthorizationNumber { get; set; }
}

/// <summary>
/// Code and/or name identifying the means of transport.
/// </summary>
[EdiElement]
public class TDT_TransportIdentification
{
	/// <summary>
	/// Identification of the means of transport by name or number.
	/// </summary>
	[EdiValue("X(9)", Path = "TDT/*/0", Mandatory = false)]
	public string? IdOfMeansOfTransportIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Identification of the means of transport by name or number.
	/// </summary>
	[EdiValue("X(35)", Path = "TDT/*/3", Mandatory = false)]
	public string? IdOfTheMeansOfTransport { get; set; }

	/// <summary>
	/// Coded name of the country in which a means of transport is registered.
	/// </summary>
	[EdiValue("X(3)", Path = "TDT/*/4", Mandatory = false)]
	public string? NationalityOfMeansOfTransportCoded { get; set; }
}