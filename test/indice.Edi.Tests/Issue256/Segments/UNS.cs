using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To separate header, detail and summary sections of a message.
/// </summary>
[EdiSegment, EdiPath("UNS")]
public class UNS
{
	/// <summary>
	/// Separates sections in a message.
	/// </summary>
	[EdiValue("X(1)", Path = "UNS/0", Mandatory = true)]
	public SectionIdentification? SectionIdentification { get; set; }

}