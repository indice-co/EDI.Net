using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To request a credit cover, reply to that request and disclose the reason for the reply.
/// </summary>
[EdiSegment, EdiPath("CCD")]
public class CCD
{
	/// <summary>
	/// Request for a credit cover.
	/// </summary>
	[EdiValue("X(3)", Path = "CCD/0", Mandatory = false)]
	public CreditCoverRequestCoded? CreditCoverRequestCoded { get; set; }

	/// <summary>
	/// Reply to a request for credit cover.
	/// </summary>
	[EdiValue("X(3)", Path = "CCD/1", Mandatory = false)]
	public CreditCoverResponseCoded? CreditCoverResponseCoded { get; set; }

	/// <summary>
	/// Reason of reply to a request for credit cover.
	/// </summary>
	[EdiValue("X(3)", Path = "CCD/2", Mandatory = false)]
	public CreditCoverReasonCoded? CreditCoverReasonCoded { get; set; }

}