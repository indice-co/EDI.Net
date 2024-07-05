namespace indice.Edi;

/// <summary>
/// Enum that specifies a hierarchy. These are the types of containers that can hold values. 
/// </summary>
internal enum EdiContainerType
{
    /// <summary>
    /// Unspecified container
    /// </summary>
    None = 0,
    
    /// <summary>
    /// <see cref="Segment"/> container.
    /// </summary>
    Segment = 1,

    /// <summary>
    /// <see cref="Element"/> container.
    /// </summary>
    Element = 2,

    /// <summary>
    /// <see cref="Component"/> container.
    /// </summary>
    Component = 3 
}
