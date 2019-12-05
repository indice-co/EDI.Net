using System;
using System.Collections.Generic;
using System.Globalization;
#if !(PORTABLE || NETSTANDARD10)
using System.Numerics;
#endif
using System.Text;
using System.IO;
using System.Xml;
using indice.Edi.Utilities;

namespace indice.Edi
{
    /// <summary>
    /// Represents a writer that provides a fast, non-cached, forward-only way of generating EDI data.
    /// </summary>
    public class EdiTextWriter : EdiWriter
    {
        private readonly TextWriter _writer;
        private readonly bool[] _charEscapeFlags;
        private char[] _writeBuffer;
        private IArrayPool<char> _arrayPool;
        private bool _closing;

        /// <summary>
        /// Gets or sets the writer's character array pool.
        /// </summary>
        public IArrayPool<char> ArrayPool {
            get { return _arrayPool; }
            set {
                if (value == null) {
                    throw new ArgumentNullException(nameof(value));
                }

                _arrayPool = value;
            }
        }


        /// <summary>
        /// Creates an instance of the <c>EdiWriter</c> class using the specified <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="textWriter">The <c>TextWriter</c> to write to.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for structure and dilimiters</param>
        public EdiTextWriter(TextWriter textWriter, IEdiGrammar grammar)
            : base(grammar) {
            if (textWriter == null) {
                throw new ArgumentNullException(nameof(textWriter));
            }

            _writer = textWriter;
            _charEscapeFlags = new bool[128];
            if (Grammar.ReleaseCharacter.HasValue) { 
                _charEscapeFlags[Grammar.DataElementSeparator] =
                _charEscapeFlags[Grammar.ComponentDataElementSeparator] =
                _charEscapeFlags[Grammar.SegmentNameDelimiter] =
                _charEscapeFlags[Grammar.SegmentTerminator] =
                _charEscapeFlags[Grammar.ReleaseCharacter.Value] = true;
            }
        }

        /// <summary>
        /// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
        /// </summary>
        public override void Flush() {
            _writer.Flush();
        }

        /// <summary>
        /// Closes this stream and the underlying stream.
        /// </summary>
        public override void Close() {
            _closing = true;
            base.Close();

            if (_writeBuffer != null) {
                BufferUtils.ReturnBuffer(_arrayPool, _writeBuffer);
                _writeBuffer = null;
            }

            if (CloseOutput && _writer != null) {
#if !(PORTABLE || NETSTANDARD10 || NETSTANDARD13)
                _writer.Close();
#else
                _writer.Dispose();
#endif
            }
        }
        
        /// <summary>
        /// Writes the segment name of a name/value pair on a Edi object.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public override void WriteSegmentName(string name) {
            InternalWriteStart(EdiToken.SegmentName, EdiContainerType.Segment);
            InternalWriteSegmentName(name);
            _writer.Write(name);
            WriteSegmentNameDelimiter();
        }

        /// <summary>
        /// Writes the end of a Edi <see cref="EdiContainerType.Segment"/>.
        /// </summary>
        public override void WriteSegmentTerminator() {
             _writer.Write(Grammar.SegmentTerminator); 
            if (Formatting == Formatting.LinePerSegment && !_closing) {
                WriteNewLine();
            }
        }

        /// <summary>
        /// Writes an <see cref="EdiContainerType.Component"/> separator.
        /// </summary>
        protected override void WriteComponentDelimiter() {
            _writer.Write(Grammar.ComponentDataElementSeparator);
        }

        /// <summary>
        /// Writes an <see cref="EdiContainerType.Element"/> separator.
        /// </summary>
        protected override void WriteElementDelimiter() {
            _writer.Write(Grammar.DataElementSeparator);
        }

        /// <summary>
        /// Writes the tag name of the <see cref="EdiContainerType.Segment"/>.
        /// </summary>
        protected override void WriteSegmentNameDelimiter() {
            _writer.Write(Grammar.SegmentNameDelimiter);
        }

        /// <summary>
        /// Writes indent characters. Line terminator if allowed by the current <see cref="IEdiGrammar"/>.
        /// </summary>
        protected override void WriteNewLine() {
            switch (Grammar.SegmentTerminator) {
                case StringUtils.CarriageReturn:
                    _writer.Write(StringUtils.LineFeed);
                    break;
                case StringUtils.LineFeed:
                    break;
                default:
                    _writer.WriteLine();
                    break;
            }
        }

        #region WriteValue methods
        /// <summary>
        /// Writes a <see cref="object"/> value.
        /// An error will raised if the value cannot be written as a single Edi token.
        /// </summary>
        /// <param name="value">The <see cref="object"/> value to write.</param>
        public override void WriteValue(object value) {
#if !(PORTABLE || NETSTANDARD10)
            if (value is BigInteger) {
                InternalWriteValue(EdiToken.Integer);

                _writer.Write(((BigInteger)value).ToString(CultureInfo.InvariantCulture));
            } else
#endif
            {
                base.WriteValue(value);
            }
        }

        /// <summary>
        /// Writes raw Edi.
        /// </summary>
        /// <param name="fragment">The raw Edi fragment to write.</param>
        public override void WriteRaw(string fragment) {
            _writer.Write(fragment);
        }

        /// <summary>
        /// Writes a <see cref="String"/> value.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(string value, Picture? picture) {
            InternalWriteValue(EdiToken.String);
            WriteEscapedString(value);
        }

        private void WriteEscapedString(string value) {
            EnsureWriteBuffer();
            var bufferPool = _arrayPool;

            if (value != null) {
                int lastWritePosition = 0;

                for (int i = 0; i < value.Length; i++) {
                    var c = value[i];

                    if (c < _charEscapeFlags.Length && !_charEscapeFlags[c]) {
                        continue;
                    }

                    string escapedValue = null;

                    if (c < _charEscapeFlags.Length) {
                        escapedValue = $"{Grammar.ReleaseCharacter.Value}{c}"; 
                    } else {
                        escapedValue = null;
                    }

                    if (escapedValue == null) {
                        continue;
                    }

                    if (i > lastWritePosition) {
                        int length = i - lastWritePosition;
                        int start = 0;

                        if (_writeBuffer == null || _writeBuffer.Length < length) {
                            char[] newBuffer = BufferUtils.RentBuffer(bufferPool, length);
                            
                            BufferUtils.ReturnBuffer(bufferPool, _writeBuffer);

                            _writeBuffer = newBuffer;
                        }

                        value.CopyTo(lastWritePosition, _writeBuffer, start, length - start);

                        // write unchanged chars before writing escaped text
                        _writer.Write(_writeBuffer, start, length - start);
                    }

                    lastWritePosition = i + 1;
                    _writer.Write(escapedValue);
                }

                if (lastWritePosition == 0) {
                    // no escaped text, write entire string
                    _writer.Write(value);
                } else {
                    int length = value.Length - lastWritePosition;

                    if (_writeBuffer == null || _writeBuffer.Length < length) {
                        _writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, length, _writeBuffer);
                    }

                    value.CopyTo(lastWritePosition, _writeBuffer, 0, length);

                    // write remaining text
                    _writer.Write(_writeBuffer, 0, length);
                }
            } else {
                WriteNull();
            }
        }
        
        /// <summary>
        /// Writes a <see cref="Int32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(int value, Picture? picture = null) {
            InternalWriteValue(EdiToken.Integer);
            _writer.Write(((int?)value).ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="UInt32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(uint value, Picture? picture = null) {
            InternalWriteValue(EdiToken.Integer);
            _writer.Write(((int?)value).ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="Int64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(long value, Picture? picture = null) {
            InternalWriteValue(EdiToken.Float);
            _writer.Write(value.ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="UInt64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(ulong value, Picture? picture = null) {
            InternalWriteValue(EdiToken.Integer);
            _writer.Write(((int?)value).ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="Single"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(float value, Picture? picture) {
            InternalWriteValue(EdiToken.Float);
            _writer.Write(value.ToEdiString(picture, Grammar.DecimalMark));
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Single}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Single}"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(float? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                InternalWriteValue(EdiToken.Float);
                _writer.Write(value.ToEdiString(picture, Grammar.DecimalMark));
            }
        }

        /// <summary>
        /// Writes a <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(double value, Picture? picture = null) {
            InternalWriteValue(EdiToken.Float);
            _writer.Write(value.ToEdiString(picture, Grammar.DecimalMark));
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Double}"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(double? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                InternalWriteValue(EdiToken.Float);
                _writer.Write(value.ToEdiString(picture, Grammar.DecimalMark));
            }
        }

        /// <summary>
        /// Writes a <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to write.</param>
        public override void WriteValue(bool value) {
            InternalWriteValue(EdiToken.Boolean);
            _writer.Write(value ? 1 : 0);
        }

        /// <summary>
        /// Writes a <see cref="Int16"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(short value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
            _writer.Write(((int?)value).ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="UInt16"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(ushort value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
            _writer.Write(((int?)value).ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="Char"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Char"/> value to write.</param>
        public override void WriteValue(char value) {
            InternalWriteValue(EdiToken.String);
            _writer.Write(value);
        }


        /// <summary>
        /// Writes a <see cref="SByte"/> value.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(sbyte value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
            _writer.Write(((int?)value).ToEdiString(picture));
            //WriteIntegerValue(value, picture);
        }

        /// <summary>
        /// Writes a <see cref="Decimal"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to write.</param>
        /// <param name="picture"></param>
        public override void WriteValue(decimal value, Picture? picture) {
            InternalWriteValue(EdiToken.Float);
            _writer.Write(value.ToEdiString(picture, Grammar.DecimalMark));
        }

        /// <summary>
        /// Writes a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        /// <param name="format"></param>
        public override void WriteValue(DateTime value, string format) {
            InternalWriteValue(EdiToken.Date);
            _writer.Write(value.ToString(format ?? "yyyyMMddHHmmss", Culture));
        }

        /// <summary>
        /// Writes a <see cref="DateTimeOffset"/> value.
        /// </summary>
        /// <param name="value">The <see cref="DateTimeOffset"/> value to write.</param>
        /// <param name="format"></param>
        public override void WriteValue(DateTimeOffset value, string format) {
            InternalWriteValue(EdiToken.Date);
            _writer.Write(value.UtcDateTime.ToString(format ?? "yyyyMMddHHmmss", Culture));
        }

        /// <summary>
        /// Writes a <see cref="Guid"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Guid"/> value to write.</param>
        public override void WriteValue(Guid value) {
            InternalWriteValue(EdiToken.String);

            string text = null;

#if !(PORTABLE || NETSTANDARD10 || NETSTANDARD13)
            text = value.ToString("D", CultureInfo.InvariantCulture);
#else
            text = value.ToString("D");
#endif
            _writer.Write(text);
        }

        /// <summary>
        /// Writes a <see cref="TimeSpan"/> value.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> value to write.</param>
        public override void WriteValue(TimeSpan value) {
            InternalWriteValue(EdiToken.String);
            string text = value.ToString(null, CultureInfo.InvariantCulture);
            _writer.Write(text);
        }

        /// <summary>
        /// Writes a <see cref="Uri"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Uri"/> value to write.</param>
        public override void WriteValue(Uri value) {
            if (value == null) {
                WriteNull();
            } else {
                InternalWriteValue(EdiToken.String);
                WriteEscapedString(value.OriginalString);
            }
        }
        #endregion

        private void EnsureWriteBuffer() {
            if (_writeBuffer == null) {
                // maximum buffer sized used when writing iso date
                _writeBuffer = BufferUtils.RentBuffer(_arrayPool, 35);
            }
        }

        private void WriteIntegerValue(long value, Picture? picture) {
            if (value >= 0 && value <= 9) {
                _writer.Write((char)('0' + value));
            } else {
                ulong uvalue = (value < 0) ? (ulong)-value : (ulong)value;

                if (value < 0) {
                    _writer.Write('-');
                }

                WriteIntegerValue(uvalue, picture);
            }
        }

        private void WriteIntegerValue(ulong uvalue, Picture? picture) {
            if (uvalue <= 9) {
                _writer.Write((char)('0' + uvalue));
            } else {
                EnsureWriteBuffer();

                int totalLength = MathUtils.IntLength(uvalue);
                int length = 0;

                do {
                    _writeBuffer[totalLength - ++length] = (char)('0' + (uvalue % 10));
                    uvalue /= 10;
                } while (uvalue != 0);

                _writer.Write(_writeBuffer, 0, length);
            }
        }
    }
}