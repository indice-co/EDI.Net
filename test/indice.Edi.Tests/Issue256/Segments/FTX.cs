using System.Collections.Generic;
using indice.Edi.Tests.Issue256.Qualifiers;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.Segments;

/// <summary>
/// To provide free form or coded text information.
/// </summary>
[EdiSegment, EdiPath("FTX")]
public class FTX
{
	/// <summary>
	/// Code specifying subject of a free text.
	/// </summary>
	[EdiValue("X(3)", Path = "FTX/0", Mandatory = true)]
	public TextSubjectQualifier? TextSubjectQualifier { get; set; }

	/// <summary>
	/// Code specifying how to handle the text.
	/// </summary>
	[EdiValue("X(3)", Path = "FTX/1", Mandatory = false)]
	public TextFunctionCoded? TextFunctionCoded { get; set; }

	/// <summary>
	/// Coded reference to a standard text and its source.
	/// </summary>
	[EdiPath("FTX/2")]
	public FTX_TextReference? TextReference { get; set; }

	/// <summary>
	/// Free text; one to five lines.
	/// </summary>
	[EdiPath("FTX/3")]
	public FTX_TextLiteral? TextLiteral { get; set; }

	/// <summary>
	/// Code of language (ISO 639-1988).
	/// </summary>
	[EdiValue("X(3)", Path = "FTX/4", Mandatory = false)]
	public string? LanguageCoded { get; set; }

}

/// <summary>
/// Coded reference to a standard text and its source.
/// </summary>
[EdiElement]
public class FTX_TextReference
{
	/// <summary>
	/// Free text in coded form.
	/// </summary>
	[EdiValue("X(17)", Path = "FTX/*/0", Mandatory = true)]
	public string? FreeTextIdentification { get; set; }

	/// <summary>
	/// Identification of a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "FTX/*/1", Mandatory = false)]
	public CodeListQualifier? CodeListQualifier { get; set; }

	/// <summary>
	/// Code identifying the agency responsible for a code list.
	/// </summary>
	[EdiValue("X(3)", Path = "FTX/*/2", Mandatory = false)]
	public CodeListResponsibleAgencyCoded? CodeListResponsibleAgencyCoded { get; set; }
}

/// <summary>
/// Free text; one to five lines.
/// </summary>
[EdiElement]
public class FTX_TextLiteral
{
	/// <summary>
	/// Free text field available to the message sender for information.
	/// </summary>
	[EdiValue("X(70)", Path = "FTX/*/0", Mandatory = true)]
	public string? FreeText1 { get; set; }

	/// <summary>
	/// Free text field available to the message sender for information.
	/// </summary>
	[EdiValue("X(70)", Path = "FTX/*/1", Mandatory = false)]
	public string? FreeText2 { get; set; }

	/// <summary>
	/// Free text field available to the message sender for information.
	/// </summary>
	[EdiValue("X(70)", Path = "FTX/*/2", Mandatory = false)]
	public string? FreeText3 { get; set; }

	/// <summary>
	/// Free text field available to the message sender for information.
	/// </summary>
	[EdiValue("X(70)", Path = "FTX/*/3", Mandatory = false)]
	public string? FreeText4 { get; set; }

	/// <summary>
	/// Free text field available to the message sender for information.
	/// </summary>
	[EdiValue("X(70)", Path = "FTX/*/4", Mandatory = false)]
	public string? FreeText5 { get; set; }
}