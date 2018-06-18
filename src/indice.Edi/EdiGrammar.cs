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
        protected char _SegmentNameDelimiter;
        protected char _ComponentDataElementSeparator;
        protected char _DataElementSeparator;
        protected char? _DecimalMark;
        protected char? _ReleaseCharacter;
        protected char[] _Reserved;
        protected char _SegmentTerminator;

        char[] _separators;
        
        protected string _ServiceStringAdviceTag;
        protected string _InterchangeHeaderTag;
        protected string _FunctionalGroupHeaderTag;
        protected string _MessageHeaderTag;
        protected string _MessageTrailerTag;
        protected string _FunctionalGroupTrailerTag;
        protected string _InterchangeTrailerTag;

        /// <summary>
        /// Constructs an <see cref="EdiGrammar"/> with the EdiFact defaults
        /// </summary>
        public EdiGrammar() {
            _ComponentDataElementSeparator = ':';
            _SegmentNameDelimiter = _DataElementSeparator = '+';
            _DecimalMark = '.';
            _ReleaseCharacter = '?';
            _Reserved = new[] { ' ' };
            _SegmentTerminator = '\'';
            
            _ServiceStringAdviceTag = "UNA";
            _InterchangeHeaderTag = "UNB";
            _FunctionalGroupHeaderTag = "UNG";
            _MessageHeaderTag = "UNH";
            _MessageTrailerTag = "UNT";
            _FunctionalGroupTrailerTag = "UNE";
            _InterchangeTrailerTag = "UNZ";
        }

        /// <summary>
        /// Construct an <see cref="EdiGrammar"/> given an other <seealso cref="IEdiGrammar"/>
        /// </summary>
        /// <param name="grammar"></param>
        public EdiGrammar(IEdiGrammar grammar) {
            _ComponentDataElementSeparator = grammar.ComponentDataElementSeparator;
            _DataElementSeparator = grammar.DataElementSeparator;
            _DecimalMark = grammar.DecimalMark;
            _ReleaseCharacter = grammar.ReleaseCharacter;
            _Reserved = grammar.Reserved.Clone() as char[];
            _SegmentTerminator = grammar.SegmentTerminator;

            _ServiceStringAdviceTag = grammar.ServiceStringAdviceTag;
            _InterchangeHeaderTag = grammar.InterchangeHeaderTag;
            _FunctionalGroupHeaderTag = grammar.FunctionalGroupHeaderTag;
            _MessageHeaderTag = grammar.MessageHeaderTag;
            _MessageTrailerTag = grammar.MessageTrailerTag;
            _FunctionalGroupTrailerTag = grammar.FunctionalGroupTrailerTag;
            _InterchangeTrailerTag = grammar.InterchangeTrailerTag;
        }


        private char[] Separators {
            get {
                if (_separators == null) {
                    _separators = new[] {
                        _SegmentNameDelimiter,
                        _ComponentDataElementSeparator,
                        _DataElementSeparator,
                        _SegmentTerminator
                    }.Distinct().ToArray();
                }
                return _separators;
            }
        }

        /// <summary>
        /// Segment name delimiter is the character used to seperate between a segment name and its elements. Default value <value>'+'</value> same as <see cref="DataElementSeparator"/>
        /// </summary>
        public char SegmentNameDelimiter {
            get { return _SegmentNameDelimiter; }
        }

        /// <summary>
        /// Component data element separator is the "second level" separator of data elements within a message segment. Default value  <value>':'</value>
        /// </summary>
        /// <value>The character used to separate between components</value>
        public char ComponentDataElementSeparator {
            get { return _ComponentDataElementSeparator; }
        }

        /// <summary>
        /// Data element separator is the "first level" separator of data elements within a message segment. Default value <value>'+'</value>
        /// </summary>
        /// <value>An array of possible characters</value>
        public char DataElementSeparator {
            get { return _DataElementSeparator; }
        }

        /// <summary>
        /// Used in EDI-Fact Only. Otherwize null
        /// </summary>
        public char? DecimalMark {
            get { return _DecimalMark; }
        }

        /// <summary>
        /// <para>The release character (analogous to the \ in regular expressions)</para>
        /// is used as a prefix to remove special meaning from the separator, segment termination, 
        /// and release characters when they are used as plain text. Default value is <value>'?'</value>
        /// </summary>
        public char? ReleaseCharacter {
            get { return _ReleaseCharacter; }
        }

        /// <summary>
        /// <para>
        /// These characters are reserved for future use. 
        /// </para>
        /// eg. <see cref="SegmentTerminator" /> or <seealso cref="DataElementSeparator" /> can not be any in this list.
        /// </summary>
        /// <value>An array of possible characters</value>
        public char[] Reserved {
            get { return _Reserved; }
        }

        /// <summary>
        /// Segment terminator indicates the end of a message segment.
        /// </summary>
        public char SegmentTerminator {
            get { return _SegmentTerminator; }
        }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string ServiceStringAdviceTag { get { return _ServiceStringAdviceTag; } }
        
        /// <summary>
        /// The segment name that marks the Interchange Header.
        /// </summary>
        public string InterchangeHeaderTag { get { return _InterchangeHeaderTag; } }

        /// <summary>
        /// The segment name that marks the Functional Group Header.
        /// </summary>
        public string FunctionalGroupHeaderTag { get { return _FunctionalGroupHeaderTag; } }
        
        /// <summary>
        /// The segment name that marks the Message Header.
        /// </summary>
        public string MessageHeaderTag { get { return _MessageHeaderTag; } }
        
        /// <summary>
        /// The segment name that marks the Message Trailer.
        /// </summary>
        public string MessageTrailerTag { get { return _MessageTrailerTag; } }
        
        /// <summary>
        /// The segment name that marks the Functional Group Trailer.
        /// </summary>
        public string FunctionalGroupTrailerTag { get { return _FunctionalGroupTrailerTag; } }
        
        /// <summary>
        /// The segment name that marks the interchange Trailer.
        /// </summary>
        public string InterchangeTrailerTag { get { return _InterchangeTrailerTag; } }
        
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
            _ComponentDataElementSeparator = chars[0];
            _SegmentNameDelimiter = _DataElementSeparator = chars[1];
            _DecimalMark = chars[2];
            _ReleaseCharacter = chars[3];
            _Reserved = new[] { chars[4] };
            _SegmentTerminator = chars[5];

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
            _ComponentDataElementSeparator = componentDataElementSeparator;
            _DataElementSeparator = dataElementSeparator;
            _SegmentNameDelimiter = segmentNameDelimiter;
            _ReleaseCharacter = releaseCharacter;
            _Reserved = reserved.HasValue ? new[] { reserved.Value } : new char[0];
            _DecimalMark = decimalMark;
            _SegmentTerminator = segmentTerminator;
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
                _SegmentNameDelimiter = '=',
                _ComponentDataElementSeparator = ':',
                _DataElementSeparator = '+',
                _DecimalMark = null,
                _ReleaseCharacter = '?',
                _Reserved = new[] { ' ' },
                _SegmentTerminator = '\'',
                _ServiceStringAdviceTag = null,
                _InterchangeHeaderTag = "STX",
                _FunctionalGroupHeaderTag = "BAT",
                _MessageHeaderTag = "MHD",
                _MessageTrailerTag = "MTR",
                _FunctionalGroupTrailerTag = "EOB",
                _InterchangeTrailerTag = "END",
            };
        }

        /// <summary>
        /// Factory for creating an <see cref="IEdiGrammar"/> with the X12 defaults.
        /// </summary>
        /// <returns>The <see cref="IEdiGrammar"/></returns>
        public static IEdiGrammar NewX12() {
            return new EdiGrammar() {
                _SegmentNameDelimiter = '*',
                _ComponentDataElementSeparator = '>',
                _DataElementSeparator = '*',
                _DecimalMark = '.',
                _ReleaseCharacter = null,
                _Reserved = new char[0],
                _SegmentTerminator = '~',
                _ServiceStringAdviceTag = null,
                _InterchangeHeaderTag = "ISA",
                _FunctionalGroupHeaderTag = "GS",
                _MessageHeaderTag = "ST",
                _MessageTrailerTag = "SE",
                _FunctionalGroupTrailerTag = "GE",
                _InterchangeTrailerTag = "IEA",
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
