using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
    public class EdiTextReader : EdiReader, IEdiLineInfo
    {
        private const char UnicodeReplacementChar = '\uFFFD';
        private readonly TextReader _reader;
        private char[] _chars;
        private int _charsUsed;
        private int _charPos;
        private int _lineStartPos;
        private int _lineNumber;
        private bool _isEndOfFile;
        private StringBuffer _buffer;
        private StringReference _stringReference;
        internal NameTable NameTable;

        public EdiTextReader(TextReader reader, IEdiGrammar grammar)
            : base(grammar) {
            if (null == reader)
                throw new ArgumentNullException(nameof(reader));
            _reader = reader;
            _lineNumber = 1;
            _chars = new char[1025];
        }
        /// <summary>
        /// Reads the next EDI token from the stream.
        /// </summary>
        /// <returns>
        /// true if the next token was read successfully; false if there are no more tokens to read.
        /// </returns>
        [DebuggerStepThrough]
        public override bool Read() {
            if (!ReadInternal()) {
                SetToken(EdiToken.None);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="Nullable{Decimal}"/>.
        /// </summary>
        /// <returns>A <see cref="Nullable{Decimal}"/>. This method will return <c>null</c> at the end of an array.</returns>
        public override decimal? ReadAsDecimal(Picture? picture = null) {
            return ReadAsDecimalInternal(picture);
        }

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="Nullable{Int32}"/>.
        /// </summary>
        /// <returns>A <see cref="Nullable{Int32}"/>. This method will return <c>null</c> at the end of an array.</returns>
        public override int? ReadAsInt32() {
            return ReadAsInt32Internal();
        }

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="String"/>. This method will return <c>null</c> at the end of an array.</returns>
        public override string ReadAsString() {
            return ReadAsStringInternal();
        }

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="Nullable{DateTime}"/>.
        /// </summary>
        /// <returns>A <see cref="String"/>. This method will return <c>null</c> at the end of an array.</returns>
        public override DateTime? ReadAsDateTime() {
            return ReadAsDateTimeInternal();
        }
        
        internal override bool ReadInternal() {
            while (true) {
                switch (_currentState) {
                    case State.Start:
                        return ParseServiceStringAdvice();
                    case State.Segment:
                    case State.SegmentStart:
                        return ParseSegment();
                    case State.Element:
                    case State.ElementStart:
                    case State.Component:
                    case State.ComponentStart:
                        return ParseValue();
                    case State.SegmentName:
                    case State.PostValue:
                        // returns true if it hits
                        // end of object or array
                        if (ParsePostValue())
                            return true;
                        break;
                    case State.Finished:
                        if (EnsureChars(0, false)) {
                            EatWhitespace(false);
                            if (_isEndOfFile) {
                                return false;
                            }
                            throw EdiReaderException.Create(this, "Additional text encountered after finished reading EDI content: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
                        }
                        return false;
                    default:
                        throw EdiReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, CurrentState));
                }
            }
        }

        #region Parse

        private bool ParsePostValue() {
            while (true) {
                char currentChar = _chars[_charPos];

                switch (currentChar) {
                    case ' ':
                    case StringUtils.Tab:
                        // eat
                        _charPos++;
                        break;
                    case StringUtils.CarriageReturn:
                        ProcessCarriageReturn(false);
                        break;
                    case StringUtils.LineFeed:
                        ProcessLineFeed();
                        break;
                    default:
                        if (char.IsWhiteSpace(currentChar)) {
                            // eat
                            _charPos++;
                            break;
                        }
                        if (Grammar.IsSpecial(currentChar)) {
                            if (Grammar.ComponentDataElementSeparator.Contains(currentChar)) {
                                _charPos++;
                                // finished parsing
                                SetToken(EdiToken.ComponentStart);
                                return true;
                            } else if (Grammar.DataElementSeparator.Contains(currentChar)) {
                                _charPos++;
                                SetToken(EdiToken.ElementStart);
                                return true;
                            } else if (Grammar.SegmentTerminator == currentChar) {
                                _charPos++;
                                SetToken(EdiToken.SegmentStart);
                                return true;
                            } else {
                                throw EdiReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
                            }
                        } else {
                            throw EdiReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
                        }
                }
            }
        }

        private bool ParseServiceStringAdvice() {
            if (!string.IsNullOrEmpty(Grammar.ServiceStringAdviceTag) && 
                '\0' == _chars[_charPos]) {
                if (ReadData(false, 9) == 0)
                    return false;

                var segmentName = new char[3];
                Array.Copy(_chars, _charPos, segmentName, 0, 3);
                if (new string(segmentName) == Grammar.ServiceStringAdviceTag) {
                    var stringAdvice = new char[6];
                    Array.Copy(_chars, _charPos + 3, stringAdvice, 0, 6);
                    Grammar.SetAdvice(stringAdvice);
                    _charPos = _charPos + 3 + 6;
                }
            }
            SetToken(EdiToken.SegmentStart);
            return true;
        }

        private bool ParseSegment() {
            while (true) {
                char currentChar = _chars[_charPos];

                switch (currentChar) {
                    case '\0':
                        if (_charsUsed == _charPos) {
                            if (ReadData(false) == 0)
                                return false;
                        } else {
                            _charPos++;
                        }
                        break;
                    case StringUtils.CarriageReturn:
                        ProcessCarriageReturn(false);
                        break;
                    case StringUtils.LineFeed:
                        ProcessLineFeed();
                        break;
                    case ' ':
                    case StringUtils.Tab:
                        // eat
                        _charPos++;
                        break;
                    default:
                        if (Grammar.SegmentTerminator == currentChar) {
                            SetToken(EdiToken.SegmentStart);
                            _charPos++;
                            return true;
                        } else if (char.IsWhiteSpace(currentChar)) {
                            // eat
                            _charPos++;
                        } else if (_currentState == State.Start) {
                            SetToken(EdiToken.SegmentStart);
                            return true;
                        } else {
                            return ParseSegmentName();
                        }
                        break;
                }
            }
        }
        private bool ParseSegmentName() {
            char firstChar = _chars[_charPos];
            ShiftBufferIfNeeded();
            ReadStringIntoBuffer();

            string segmentName;
            if (NameTable != null) {
                segmentName = NameTable.Get(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length);
                // no match in name table
                if (segmentName == null)
                    segmentName = _stringReference.ToString();
            } else {
                segmentName = _stringReference.ToString();
            }
            EatWhitespace(false);

            if (!Grammar.DataElementSeparator.Contains(_chars[_charPos]))
                throw EdiReaderException.Create(this, "Invalid character after parsing property name. Expected '{1}' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos], string.Join("', '", Grammar.DataElementSeparator)));
            
            SetToken(EdiToken.SegmentName, segmentName);

            ClearRecentString();
            return true;
        }

        private bool ParseValue() {
            while (true) {
                char currentChar = _chars[_charPos];

                switch (currentChar) {
                    case '\0':
                        if (_charsUsed == _charPos) {
                            if (ReadData(false) == 0)
                                return false;
                        } else {
                            _charPos++;
                        }
                        break;
                    case StringUtils.CarriageReturn:
                        ProcessCarriageReturn(false);
                        break;
                    case StringUtils.LineFeed:
                        ProcessLineFeed();
                        break;
                    case ' ':
                    case StringUtils.Tab:
                        // eat
                        _charPos++;
                        break;
                    default:
                        if (char.IsWhiteSpace(currentChar)) {
                            // eat
                            _charPos++;
                            break;
                        } else if (_currentState == State.ElementStart) {
                            SetToken(EdiToken.ComponentStart);
                            return true;
                        } else if (Grammar.IsSpecial(currentChar)) {
                            ParseString(true);
                            return true;
                        }
                        ParseString();
                        return true;
                }
            }
        }
        #endregion

        #region LineInfo
        /// <summary>
        /// Gets a value indicating whether the class can return line information.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if LineNumber and LinePosition can be provided; otherwise, <c>false</c>.
        /// </returns>
        public bool HasLineInfo() {
            return true;
        }

        /// <summary>
        /// Gets the current line number.
        /// </summary>
        /// <value>
        /// The current line number or 0 if no line information is available (for example, HasLineInfo returns false).
        /// </value>
        public int LineNumber {
            get {
                if (CurrentState == State.Start && LinePosition == 0)
                    return 0;

                return _lineNumber;
            }
        }

        /// <summary>
        /// Gets the current line position.
        /// </summary>
        /// <value>
        /// The current line position or 0 if no line information is available (for example, HasLineInfo returns false).
        /// </value>
        public int LinePosition {
            get { return _charPos - _lineStartPos; }
        }
        #endregion


#if DEBUG
        internal void SetCharBuffer(char[] chars) {
            _chars = chars;
        }
#endif

        private void ClearRecentString() {
            if (_buffer != null)
                _buffer.Position = 0;

            _stringReference = new StringReference();
        }

        private StringBuffer GetBuffer() {
            if (_buffer == null) {
                _buffer = new StringBuffer(1025);
            } else {
                _buffer.Position = 0;
            }

            return _buffer;
        }

        private void OnNewLine(int pos) {
            _lineNumber++;
            _lineStartPos = pos - 1;
        }

        private void ParseString(bool forceNull = false) {
            ShiftBufferIfNeeded();
            ReadStringIntoBuffer();
            SetPostValueState();

            string text = _stringReference.ToString();
            SetToken(forceNull ? EdiToken.Null : EdiToken.String, forceNull ? null : text, false);

            ClearRecentString();
        }
        #region Helpers

        private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count) {
            const int charByteCount = 2;

            Buffer.BlockCopy(src, srcOffset * charByteCount, dst, dstOffset * charByteCount, count * charByteCount);
        }

        private void ShiftBufferIfNeeded() {
            // once in the last 10% of the buffer shift the remaining content to the start to avoid
            // unnessesarly increasing the buffer size when reading numbers/strings
            int length = _chars.Length;
            if (length - _charPos <= length * 0.1) {
                int count = _charsUsed - _charPos;
                if (count > 0)
                    BlockCopyChars(_chars, _charPos, _chars, 0, count);

                _lineStartPos -= _charPos;
                _charPos = 0;
                _charsUsed = count;
                _chars[_charsUsed] = '\0';
            }
        }

        private int ReadData(bool append) {
            return ReadData(append, 0);
        }

        private int ReadData(bool append, int charsRequired) {
            if (_isEndOfFile)
                return 0;

            // char buffer is full
            if (_charsUsed + charsRequired >= _chars.Length - 1) {
                if (append) {
                    // copy to new array either double the size of the current or big enough to fit required content
                    int newArrayLength = Math.Max(_chars.Length * 2, _charsUsed + charsRequired + 1);

                    // increase the size of the buffer
                    char[] dst = new char[newArrayLength];

                    BlockCopyChars(_chars, 0, dst, 0, _chars.Length);

                    _chars = dst;
                } else {
                    int remainingCharCount = _charsUsed - _charPos;

                    if (remainingCharCount + charsRequired + 1 >= _chars.Length) {
                        // the remaining count plus the required is bigger than the current buffer size
                        char[] dst = new char[remainingCharCount + charsRequired + 1];

                        if (remainingCharCount > 0)
                            BlockCopyChars(_chars, _charPos, dst, 0, remainingCharCount);

                        _chars = dst;
                    } else {
                        // copy any remaining data to the beginning of the buffer if needed and reset positions
                        if (remainingCharCount > 0)
                            BlockCopyChars(_chars, _charPos, _chars, 0, remainingCharCount);
                    }

                    _lineStartPos -= _charPos;
                    _charPos = 0;
                    _charsUsed = remainingCharCount;
                }
            }

            int attemptCharReadCount = _chars.Length - _charsUsed - 1;

            int charsRead = _reader.Read(_chars, _charsUsed, attemptCharReadCount);

            _charsUsed += charsRead;

            if (charsRead == 0)
                _isEndOfFile = true;

            _chars[_charsUsed] = '\0';
            return charsRead;
        }

        private bool EnsureChars(int relativePosition, bool append) {
            if (_charPos + relativePosition >= _charsUsed)
                return ReadChars(relativePosition, append);

            return true;
        }

        private bool ReadChars(int relativePosition, bool append) {
            if (_isEndOfFile)
                return false;

            int charsRequired = _charPos + relativePosition - _charsUsed + 1;

            int totalCharsRead = 0;

            // it is possible that the TextReader doesn't return all data at once
            // repeat read until the required text is returned or the reader is out of content
            do {
                int charsRead = ReadData(append, charsRequired - totalCharsRead);

                // no more content
                if (charsRead == 0)
                    break;

                totalCharsRead += charsRead;
            } while (totalCharsRead < charsRequired);

            if (totalCharsRead < charsRequired)
                return false;
            return true;
        }

        private void ReadStringIntoBuffer() {
            int charPos = _charPos;
            int initialPosition = _charPos;
            int lastWritePosition = _charPos;
            StringBuffer buffer = null;

            while (true) {
                switch (_chars[charPos++]) {
                    case '\0':
                        if (_charsUsed == charPos - 1) {
                            charPos--;

                            if (ReadData(true) == 0) {
                                _charPos = charPos;
                                throw EdiReaderException.Create(this, "Unterminated string. Expected delimiter.");
                            }
                        }
                        break;
                    // TODO: Make use of the release character.
                    case '\\':
                        _charPos = charPos;
                        if (!EnsureChars(0, true)) {
                            _charPos = charPos;
                            throw EdiReaderException.Create(this, "Unterminated string. Expected delimiter.");
                        }

                        // start of escape sequence
                        int escapeStartPos = charPos - 1;

                        char currentChar = _chars[charPos];

                        char writeChar;

                        switch (currentChar) {
                            case 'b':
                                charPos++;
                                writeChar = '\b';
                                break;
                            case 't':
                                charPos++;
                                writeChar = '\t';
                                break;
                            case 'n':
                                charPos++;
                                writeChar = '\n';
                                break;
                            case 'f':
                                charPos++;
                                writeChar = '\f';
                                break;
                            case 'r':
                                charPos++;
                                writeChar = '\r';
                                break;
                            case '\\':
                                charPos++;
                                writeChar = '\\';
                                break;
                            case 'u':
                                charPos++;
                                _charPos = charPos;
                                writeChar = ParseUnicode();

                                if (StringUtils.IsLowSurrogate(writeChar)) {
                                    // low surrogate with no preceding high surrogate; this char is replaced
                                    writeChar = UnicodeReplacementChar;
                                } else if (StringUtils.IsHighSurrogate(writeChar)) {
                                    bool anotherHighSurrogate;

                                    // loop for handling situations where there are multiple consecutive high surrogates
                                    do {
                                        anotherHighSurrogate = false;

                                        // potential start of a surrogate pair
                                        if (EnsureChars(2, true) && _chars[_charPos] == '\\' && _chars[_charPos + 1] == 'u') {
                                            char highSurrogate = writeChar;

                                            _charPos += 2;
                                            writeChar = ParseUnicode();

                                            if (StringUtils.IsLowSurrogate(writeChar)) {
                                                // a valid surrogate pair!
                                            } else if (StringUtils.IsHighSurrogate(writeChar)) {
                                                // another high surrogate; replace current and start check over
                                                highSurrogate = UnicodeReplacementChar;
                                                anotherHighSurrogate = true;
                                            } else {
                                                // high surrogate not followed by low surrogate; original char is replaced
                                                highSurrogate = UnicodeReplacementChar;
                                            }

                                            if (buffer == null)
                                                buffer = GetBuffer();

                                            WriteCharToBuffer(buffer, highSurrogate, lastWritePosition, escapeStartPos);
                                            lastWritePosition = _charPos;
                                        } else {
                                            // there are not enough remaining chars for the low surrogate or is not follow by unicode sequence
                                            // replace high surrogate and continue on as usual
                                            writeChar = UnicodeReplacementChar;
                                        }
                                    } while (anotherHighSurrogate);
                                }

                                charPos = _charPos;
                                break;
                            default:
                                charPos++;
                                _charPos = charPos;
                                throw EdiReaderException.Create(this, "Bad EDI escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, @"\" + currentChar));
                        }

                        if (buffer == null)
                            buffer = GetBuffer();

                        WriteCharToBuffer(buffer, writeChar, lastWritePosition, escapeStartPos);

                        lastWritePosition = charPos;
                        break;
                    case StringUtils.CarriageReturn:
                        _charPos = charPos - 1;
                        ProcessCarriageReturn(true);
                        charPos = _charPos;
                        break;
                    case StringUtils.LineFeed:
                        _charPos = charPos - 1;
                        ProcessLineFeed();
                        charPos = _charPos;
                        break;
                    default:
                        if (Grammar.IsSpecial(_chars[charPos - 1])) {
                            charPos--;
                            if (initialPosition == lastWritePosition) {
                                _stringReference = new StringReference(_chars, initialPosition, charPos - initialPosition);
                            } else {
                                if (buffer == null)
                                    buffer = GetBuffer();

                                if (charPos > lastWritePosition)
                                    buffer.Append(_chars, lastWritePosition, charPos - lastWritePosition);

                                _stringReference = new StringReference(buffer.GetInternalBuffer(), 0, buffer.Position);
                            }
                            _charPos = charPos;
                            return;
                        }
                        break;
                }
            }
        }

        private void WriteCharToBuffer(StringBuffer buffer, char writeChar, int lastWritePosition, int writeToPosition) {
            if (writeToPosition > lastWritePosition) {
                buffer.Append(_chars, lastWritePosition, writeToPosition - lastWritePosition);
            }

            buffer.Append(writeChar);
        }

        private char ParseUnicode() {
            char writeChar;
            if (EnsureChars(4, true)) {
                string hexValues = new string(_chars, _charPos, 4);
                char hexChar = Convert.ToChar(int.Parse(hexValues, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
                writeChar = hexChar;

                _charPos += 4;
            } else {
                throw EdiReaderException.Create(this, "Unexpected end while parsing unicode character.");
            }
            return writeChar;
        }

        private void ProcessLineFeed() {
            _charPos++;
            OnNewLine(_charPos);
        }

        private void ProcessCarriageReturn(bool append) {
            _charPos++;

            if (EnsureChars(1, append) && _chars[_charPos] == StringUtils.LineFeed)
                _charPos++;

            OnNewLine(_charPos);
        }

        private bool EatWhitespace(bool oneOrMore) {
            bool finished = false;
            bool ateWhitespace = false;
            while (!finished) {
                char currentChar = _chars[_charPos];

                switch (currentChar) {
                    case '\0':
                        if (_charsUsed == _charPos) {
                            if (ReadData(false) == 0)
                                finished = true;
                        } else {
                            _charPos++;
                        }
                        break;
                    case StringUtils.CarriageReturn:
                        ProcessCarriageReturn(false);
                        break;
                    case StringUtils.LineFeed:
                        ProcessLineFeed();
                        break;
                    default:
                        if (currentChar == ' ' || char.IsWhiteSpace(currentChar)) {
                            ateWhitespace = true;
                            _charPos++;
                        } else {
                            finished = true;
                        }
                        break;
                }
            }

            return (!oneOrMore || ateWhitespace);
        }

        #endregion
    }
}
