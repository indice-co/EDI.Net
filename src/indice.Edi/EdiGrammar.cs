using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
    /// <summary>
    /// The <see cref="EdiGrammar"/> comprises of all the basic structural settings.
    /// Essentialy these are the rules for X12, EDIFact or TRADACOMS.
    /// </summary>
    public class EdiGrammar : IEdiGrammar
    {
        char[] _separators;
        
        /// <summary>
        /// Constructs an <see cref="EdiGrammar"/> with the EdiFact defaults
        /// </summary>
        public EdiGrammar() {
            ComponentDataElementSeparator = ':';
            SegmentNameDelimiter = DataElementSeparator = '+';
            DecimalMark = '.';
            ReleaseCharacter = '?';
            Reserved = new[] { ' ' };
            SegmentTerminator = '\'';
            
            ServiceStringAdviceTag = "UNA";
            InterchangeHeaderTag = "UNB";
            FunctionalGroupHeaderTag = "UNG";
            MessageHeaderTag = "UNH";
            MessageTrailerTag = "UNT";
            FunctionalGroupTrailerTag = "UNE";
            InterchangeTrailerTag = "UNZ";
        }

        /// <summary>
        /// Construct an <see cref="EdiGrammar"/> given an other <seealso cref="IEdiGrammar"/>
        /// </summary>
        /// <param name="grammar"></param>
        public EdiGrammar(IEdiGrammar grammar) {
            ComponentDataElementSeparator = grammar.ComponentDataElementSeparator;
            DataElementSeparator = grammar.DataElementSeparator;
            DecimalMark = grammar.DecimalMark;
            ReleaseCharacter = grammar.ReleaseCharacter;
            Reserved = grammar.Reserved.Clone() as char[];
            SegmentTerminator = grammar.SegmentTerminator;

            ServiceStringAdviceTag = grammar.ServiceStringAdviceTag;
            InterchangeHeaderTag = grammar.InterchangeHeaderTag;
            FunctionalGroupHeaderTag = grammar.FunctionalGroupHeaderTag;
            MessageHeaderTag = grammar.MessageHeaderTag;
            MessageTrailerTag = grammar.MessageTrailerTag;
            FunctionalGroupTrailerTag = grammar.FunctionalGroupTrailerTag;
            InterchangeTrailerTag = grammar.InterchangeTrailerTag;
        }


        private char[] Separators {
            get {
                if (_separators == null) {
                    _separators = new[] {
                        SegmentNameDelimiter,
                        ComponentDataElementSeparator,
                        DataElementSeparator,
                        SegmentTerminator
                    }.Distinct().ToArray();
                }
                return _separators;
            }
        }

        /// <summary>
        /// Segment name delimiter is the character used to seperate between a segment name and its elements. Default value <value>'+'</value> same as <see cref="DataElementSeparator"/>
        /// </summary>
        public char SegmentNameDelimiter { get; protected set; }

        /// <summary>
        /// Component data element separator is the "second level" separator of data elements within a message segment. Default value  <value>':'</value>
        /// </summary>
        /// <value>The character used to separate between components</value>
        public char ComponentDataElementSeparator { get; protected set; }

        /// <summary>
        /// Data element separator is the "first level" separator of data elements within a message segment. Default value <value>'+'</value>
        /// </summary>
        /// <value>An array of possible characters</value>
        public char DataElementSeparator { get; protected set; }

        /// <summary>
        /// Used in EDI-Fact Only. Otherwize null
        /// </summary>
        public char? DecimalMark { get; protected set; }

        /// <summary>
        /// <para>The release character (analogous to the \ in regular expressions)</para>
        /// is used as a prefix to remove special meaning from the separator, segment termination, 
        /// and release characters when they are used as plain text. Default value is <value>'?'</value>
        /// </summary>
        public char? ReleaseCharacter { get; protected set; }

        /// <summary>
        /// <para>
        /// These characters are reserved for future use. 
        /// </para>
        /// eg. <see cref="SegmentTerminator" /> or <seealso cref="DataElementSeparator" /> can not be any in this list.
        /// </summary>
        /// <value>An array of possible characters</value>
        public char[] Reserved { get; protected set; }

        /// <summary>
        /// Segment terminator indicates the end of a message segment.
        /// </summary>
        public char SegmentTerminator { get; protected set; }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string ServiceStringAdviceTag { get; protected set; }

        /// <summary>
        /// The segment name that marks the Interchange Header.
        /// </summary>
        public string InterchangeHeaderTag { get; protected set; }

        /// <summary>
        /// The segment name that marks the Functional Group Header.
        /// </summary>
        public string FunctionalGroupHeaderTag { get; protected set; }

        /// <summary>
        /// The segment name that marks the Message Header.
        /// </summary>
        public string MessageHeaderTag { get; protected set; }

        /// <summary>
        /// The segment name that marks the Message Trailer.
        /// </summary>
        public string MessageTrailerTag { get; protected set; }

        /// <summary>
        /// The segment name that marks the Functional Group Trailer.
        /// </summary>
        public string FunctionalGroupTrailerTag { get; protected set; }

        /// <summary>
        /// The segment name that marks the interchange Trailer.
        /// </summary>
        public string InterchangeTrailerTag { get; protected set; }

        /// <summary>
        /// Checks to see if a character is any of the known special characters.
        /// </summary>
        /// <param name="character"></param>
        /// <returns>True if the character is special. Otherwize false.</returns>
        public bool IsSpecial(char character) {
            return Separators.Contains(character);
        }

        /// <summary>
        /// Populates the Edi grammar delimiters using a eg UNA:+.? '
        /// </summary>
        /// <param name="chars"></param>
        public void SetAdvice(char[] chars) {
            ComponentDataElementSeparator = chars[0];
            SegmentNameDelimiter = DataElementSeparator = chars[1];
            DecimalMark = chars[2];
            ReleaseCharacter = chars[3];
            Reserved = new[] { chars[4] };
            SegmentTerminator = chars[5];
            _separators = null;
            //TODO: must figure this out to work both for EDIFact and X12. 
            // The above is only used by the former. http://stackoverflow.com/a/20112217/61577
        }

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
        public void SetAdvice(char segmentNameDelimiter,
                              char dataElementSeparator,
                              char componentDataElementSeparator,
                              char segmentTerminator,
                              char? releaseCharacter,
                              char? reserved,
                              char? decimalMark) {
            ComponentDataElementSeparator = componentDataElementSeparator;
            DataElementSeparator = dataElementSeparator;
            SegmentNameDelimiter = segmentNameDelimiter;
            ReleaseCharacter = releaseCharacter;
            Reserved = reserved.HasValue ? new[] { reserved.Value } : new char[0];
            DecimalMark = decimalMark;
            SegmentTerminator = segmentTerminator;
            _separators = null;
        }

        /// <summary>
        /// Factory for creating an <see cref="IEdiGrammar"/> with the EdiFact defaults.
        /// </summary>
        /// <returns>The <see cref="IEdiGrammar"/></returns>
        public static IEdiGrammar NewEdiFact() {
            return new EdiGrammar();
        }

        /// <summary>
        /// Factory for creating an <see cref="IEdiGrammar"/> with the Trandacoms defaults.
        /// </summary>
        /// <returns>The <see cref="IEdiGrammar"/></returns>
        public static IEdiGrammar NewTradacoms() {
            return new EdiGrammar() {
                SegmentNameDelimiter = '=',
                ComponentDataElementSeparator = ':',
                DataElementSeparator = '+',
                DecimalMark = null,
                ReleaseCharacter = '?',
                Reserved = new[] { ' ' },
                SegmentTerminator = '\'',
                ServiceStringAdviceTag = null,
                InterchangeHeaderTag = "STX",
                FunctionalGroupHeaderTag = "BAT",
                MessageHeaderTag = "MHD",
                MessageTrailerTag = "MTR",
                FunctionalGroupTrailerTag = "EOB",
                InterchangeTrailerTag = "END",
            };
        }

        /// <summary>
        /// Factory for creating an <see cref="IEdiGrammar"/> with the X12 defaults.
        /// </summary>
        /// <returns>The <see cref="IEdiGrammar"/></returns>
        public static IEdiGrammar NewX12() {
            return new EdiGrammar() {
                SegmentNameDelimiter = '*',
                ComponentDataElementSeparator = '>',
                DataElementSeparator = '*',
                DecimalMark = '.',
                ReleaseCharacter = null,
                Reserved = new char[0],
                SegmentTerminator = '~',
                ServiceStringAdviceTag = null,
                InterchangeHeaderTag = "ISA",
                FunctionalGroupHeaderTag = "GS",
                MessageHeaderTag = "ST",
                MessageTrailerTag = "SE",
                FunctionalGroupTrailerTag = "GE",
                InterchangeTrailerTag = "IEA",
            };
        }

        /// <summary>
        /// Clones the current <see cref="EdiGrammar"/>
        /// </summary>
        /// <returns>Returns a copy of the current <see cref="EdiGrammar"/> into a new instance</returns>
        public EdiGrammar Clone() {
            return new EdiGrammar(this);
        }
    }
}
