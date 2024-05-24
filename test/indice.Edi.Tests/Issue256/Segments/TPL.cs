using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify placement of goods or equipment in relation to the transport used. The segment serves as a pointer to the TDT segment group.
/// </summary>
[EdiSegment, EdiPath("TPL")]
public class TPL
{
	/// <summary>
	/// Code and/or name identifying the means of transport.
	/// </summary>
	[EdiPath("TPL/0")]
	public TPL_TransportIdentification? TransportIdentification { get; set; }
}

/// <summary>
/// Code and/or name identifying the means of transport.
/// </summary>
[EdiElement]
public class TPL_TransportIdentification
{
	/// <summary>
	/// Identification of the means of transport by name or number.
	/// </summary>
	[EdiValue("X(9)", Path = "TPL/*/0", Mandatory = false)]
	public string? IdOfMeansOfTransportIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TPL/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "TPL/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }

	/// <summary>
	/// Identification of the means of transport by name or number.
	/// </summary>
	[EdiValue("X(35)", Path = "TPL/*/3", Mandatory = false)]
	public string? IdOfTheMeansOfTransport { get; set; }

	/// <summary>
	/// Coded name of the country in which a means of transport is registered.
	/// </summary>
	[EdiValue("X(3)", Path = "TPL/*/4", Mandatory = false)]
	public string? NationalityOfMeansOfTransportCoded { get; set; }
}