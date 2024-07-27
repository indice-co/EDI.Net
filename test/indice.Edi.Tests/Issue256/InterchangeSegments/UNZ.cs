using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.InterchangeSegments;

/// <summary>
/// To end and check the completeness of an interchange.
/// </summary>
[EdiSegment, EdiPath("UNZ")]
public class UNZ
{
    /// <summary>
    /// Interchange control count
    /// </summary>
    [EdiValue("9(6)", Path = "UNZ/0", Mandatory = true)]
    public int? InterchangeControlCount { get; set; }

    /// <summary>
    /// Interchange control reference
    /// </summary>
    [EdiValue("X(14)", Path = "UNZ/1", Mandatory = true)]
    public string? InterchangeControlReference { get; set; }
}
