using System;

namespace indice.Edi
{
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
        string InterchangeHeaderTag { get; }
        string FunctionalGroupHeaderTag { get; }
        string MessageHeaderTag { get; }
        string MessageTrailerTag { get; }
        string FunctionalGroupTrailerTag { get; }
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
        /// <param name="_chars"></param>
        void SetAdvice(char[] _chars);
    }
}
