using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
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

        public char[] Separators {
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
        
        public char SegmentNameDelimiter {
            get { return _SegmentNameDelimiter; }
        }

        public char ComponentDataElementSeparator {
            get { return _ComponentDataElementSeparator; }
        }

        public char DataElementSeparator {
            get { return _DataElementSeparator; }
        }

        public char? DecimalMark {
            get { return _DecimalMark; }
        }
        public char? ReleaseCharacter {
            get { return _ReleaseCharacter; }
        }

        public char[] Reserved {
            get { return _Reserved; }
        }

        public char SegmentTerminator {
            get { return _SegmentTerminator; }
        }

        public string ServiceStringAdviceTag { get { return _ServiceStringAdviceTag; } }

        public string InterchangeHeaderTag { get { return _InterchangeHeaderTag; } }

        public string FunctionalGroupHeaderTag { get { return _FunctionalGroupHeaderTag; } }

        public string MessageHeaderTag { get { return _MessageHeaderTag; } }

        public string MessageTrailerTag { get { return _MessageTrailerTag; } }

        public string FunctionalGroupTrailerTag { get { return _FunctionalGroupTrailerTag; } }

        public string InterchangeTrailerTag { get { return _InterchangeTrailerTag; } }

        public bool IsSpecial(char character) {
            return Separators.Contains(character);
        }

        public void SetAdvice(char[] _chars) {
            _ComponentDataElementSeparator = _chars[0];
            _SegmentNameDelimiter = _DataElementSeparator = _chars[1];
            _DecimalMark = _chars[2];
            _ReleaseCharacter = _chars[3];
            _Reserved = new[] { _chars[4] };
            _SegmentTerminator = _chars[5];

            ///TODO: must figure this out to work both for EDIFact and X12. 
            /// The above is only used by the former. http://stackoverflow.com/a/20112217/61577
        }

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

        public static IEdiGrammar NewEdiFact() {
            return new EdiGrammar();
        }

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

        public EdiGrammar Clone() {
            return new EdiGrammar(this);
        }
    }
}
