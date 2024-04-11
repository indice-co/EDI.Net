using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify results of the application of an authentification procedure.
/// </summary>
[EdiSegment, EdiPath("AUT")]
public class AUT
{
	/// <summary>
	/// The result of the application of an algorithm to the content of data element in a message.
	/// </summary>
	[EdiValue("X(35)", Path = "AUT/0", Mandatory = true)]
	public string? ValidationResult { get; set; }

	/// <summary>
	/// Identifies the cryptographic key used to calculate the validation result.
	/// </summary>
	[EdiValue("X(35)", Path = "AUT/1", Mandatory = false)]
	public string? ValidationKeyIdentification { get; set; }

}