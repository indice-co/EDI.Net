using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To specify the link of one non-EDIFACT external file to an EDIFACT message.
/// </summary>
[EdiSegment, EdiPath("EFI")]
public class EFI
{
	/// <summary>
	/// To identify a file.
	/// </summary>
	[EdiPath("EFI/0")]
	public EFI_FileIdentification? FileIdentification { get; set; }

	/// <summary>
	/// To define details relevant to a file.
	/// </summary>
	[EdiPath("EFI/1")]
	public EFI_FileDetails? FileDetails { get; set; }

	/// <summary>
	/// Number indicating the position in a sequence.
	/// </summary>
	[EdiValue("X(10)", Path = "EFI/2", Mandatory = false)]
	public string? SequenceNumber { get; set; }

}

/// <summary>
/// To identify a file.
/// </summary>
[EdiElement]
public class EFI_FileIdentification
{
	/// <summary>
	/// Name assigned to a file.
	/// </summary>
	[EdiValue("X(35)", Path = "EFI/*/0", Mandatory = false)]
	public string? FileName { get; set; }

	/// <summary>
	/// Plain language description of articles or products.
	/// </summary>
	[EdiValue("X(35)", Path = "EFI/*/1", Mandatory = false)]
	public string? ItemDescription { get; set; }
}

/// <summary>
/// To define details relevant to a file.
/// </summary>
[EdiElement]
public class EFI_FileDetails
{
	/// <summary>
	/// To give an identification of the file format.
	/// </summary>
	[EdiValue("X(17)", Path = "EFI/*/0", Mandatory = true)]
	public string? FileFormat { get; set; }

	/// <summary>
	/// To specify the version number or name of an object.
	/// </summary>
	[EdiValue("X(9)", Path = "EFI/*/1", Mandatory = false)]
	public string? Version { get; set; }

	/// <summary>
	/// A code to identify the data format.
	/// </summary>
	[EdiValue("X(3)", Path = "EFI/*/2", Mandatory = false)]
	public DataFormatCoded? DataFormatCoded { get; set; }

	/// <summary>
	/// To describe a data format in free form.
	/// </summary>
	[EdiValue("X(35)", Path = "EFI/*/3", Mandatory = false)]
	public string? DataFormat { get; set; }
}