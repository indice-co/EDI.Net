namespace indice.Edi;

/// <summary>
/// Specifies the type of EDI token.
/// </summary>
public enum EdiToken
{
    /// <summary>
    /// This is returned by the <see cref="EdiReader"/> if a <see cref="EdiReader.Read"/> method has not been called. 
    /// </summary>
    None = 0,

    /// <summary>
    /// Reader is at the start of a Segment.
    /// </summary>
    SegmentStart = 1,

    /// <summary>
    /// Reader is at the start of a Segment.
    /// </summary>
    SegmentName = 2,

    /// <summary>
    /// Reader is at the start of a DataElement.
    /// </summary>
    ElementStart = 3,

    /// <summary>
    /// Reader is at the start of a Component.
    /// </summary>
    ComponentStart = 4,

    /// <summary>
    /// A string.
    /// </summary>
    String = 5,
    
    /// <summary>
    /// An integer.
    /// </summary>
    Integer = 6,

    /// <summary>
    /// A float.
    /// </summary>
    Float = 7,
    
    /// <summary>
    /// A boolean.
    /// </summary>
    Boolean = 8,

    /// <summary>
    /// A Date.
    /// </summary>
    Date = 9,

    /// <summary>
    /// A null token.
    /// </summary>
    Null = 10,
}
