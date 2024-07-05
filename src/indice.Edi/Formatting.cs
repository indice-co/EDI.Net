namespace indice.Edi;

/// <summary>
/// Specifies formatting options for the <see cref="EdiTextWriter"/>.
/// </summary>
public enum Formatting
{
    /// <summary>
    /// No special formatting is applied. This is the default.
    /// </summary>
    None = 0,

    /// <summary>
    /// Causes segments to be placed on new lines.
    /// </summary>
    LinePerSegment = 1
}