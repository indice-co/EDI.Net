namespace indice.Edi;

/// <summary>
/// The <see cref="IEdiGrammar"/> comprises of all the basic structural settings.
/// Essentialy these are the rules for X12, EDIFact or TRADACOMS.
/// </summary>
public interface IEdiGrammar
{
    /// <summary>
    /// Segment name delimiter is the character used to seperate between a segment name and its elements. Default value <value>'+'</value> same as <see cref="DataElementSeparator"/>
    /// </summary>
    char SegmentNameDelimiter { get; }

    /// <summary>
    /// Component data element separator is the "second level" separator of data elements within a message segment. Default value  <value>':'</value>
    /// </summary>
    /// <value>The character used to separate between components</value>
    char ComponentDataElementSeparator { get; }

    /// <summary>
    /// Data element separator is the "first level" separator of data elements within a message segment. Default value <value>'+'</value>
    /// </summary>
    /// <value>An array of possible characters</value>
    char DataElementSeparator { get; }

    /// <summary>
    /// Used in EDI-Fact Only. Otherwize null
    /// </summary>
    char? DecimalMark { get; }

    /// <summary>
    /// <para>The release character (analogous to the \ in regular expressions)</para>
    /// is used as a prefix to remove special meaning from the separator, segment termination, 
    /// and release characters when they are used as plain text. Default value is <value>'?'</value>
    /// </summary>
    char? ReleaseCharacter { get; }

    /// <summary>
    /// <para>
    /// These characters are reserved for future use. 
    /// </para>
    /// eg. <see cref="SegmentTerminator" /> or <seealso cref="DataElementSeparator" /> can not be any in this list.
    /// </summary>
    /// <value>An array of possible characters</value>
    char[] Reserved { get; }

    /// <summary>
    /// Segment terminator indicates the end of a message segment.
    /// </summary>
    char SegmentTerminator { get; }

    /// <summary>
    /// Only available in EDI Fact. Otherwize null
    /// </summary>
    string ServiceStringAdviceTag { get; }

    /// <summary>
    /// The segment name that marks the Interchange Header.
    /// </summary>
    string InterchangeHeaderTag { get; }

    /// <summary>
    /// The segment name that marks the Functional Group Header.
    /// </summary>
    string FunctionalGroupHeaderTag { get; }

    /// <summary>
    /// The segment name that marks the Message Header.
    /// </summary>
    string MessageHeaderTag { get; }

    /// <summary>
    /// The segment name that marks the Message Trailer.
    /// </summary>
    string MessageTrailerTag { get; }

    /// <summary>
    /// The segment name that marks the Functional Group Trailer.
    /// </summary>
    string FunctionalGroupTrailerTag { get; }

    /// <summary>
    /// The segment name that marks the interchange Trailer.
    /// </summary>
    string InterchangeTrailerTag { get; }

    /// <summary>
    /// Checks to see if a character is any of the known special characters.
    /// </summary>
    /// <param name="character"></param>
    /// <returns>True if the character is special. Otherwize false.</returns>
    bool IsSpecial(char character);

    /// <summary>
    /// Populates the Edi grammar delimiters using a eg UNA:+.? '
    /// </summary>
    /// <param name="chars"></param>
    void SetAdvice(char[] chars);

    /// <summary>
    /// Populates the Edi grammar delimiters using a eg UNA:+.? '
    /// </summary>
    /// <param name="segmentNameDelimiter">populates <see cref="SegmentNameDelimiter"/></param>
    /// <param name="dataElementSeparator">populates <see cref="DataElementSeparator"/></param>
    /// <param name="componentDataElementSeparator">populates <see cref="ComponentDataElementSeparator"/></param>
    /// <param name="segmentTerminator">populates <see cref="SegmentTerminator"/></param>
    /// <param name="releaseCharacter">populates <see cref="ReleaseCharacter"/></param>
    /// <param name="reserved">populates <see cref="Reserved"/></param>
    /// <param name="decimalMark">populates <see cref="DecimalMark"/> character</param>
    void SetAdvice(char segmentNameDelimiter,
                   char dataElementSeparator,
                   char componentDataElementSeparator,
                   char segmentTerminator,
                   char? releaseCharacter,
                   char? reserved,
                   char? decimalMark);

}
