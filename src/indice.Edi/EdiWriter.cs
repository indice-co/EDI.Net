using System;
using System.Collections.Generic;
using System.IO;
#if !PORTABLE
using System.Numerics;
#endif
using indice.Edi.Utilities;
using System.Globalization;
using System.Linq;

namespace indice.Edi
{
    /// <summary>
    /// Represents a writer that provides a fast, non-cached, forward-only way of generating Edi data.
    /// </summary>
    public abstract class EdiWriter : IDisposable
    {
        internal enum State
        {
            Start = 0,
            SegmentName = 1,
            SegmentStart = 2,
            Segment = 3,
            ElementStart = 4,
            Element = 5,
            ComponentStart = 6,
            Component = 7,
            Closed = 8,
            Error = 9
        }
        

        static EdiWriter() {
            
        }

        private List<EdiPosition> _stack;
        private EdiPosition _currentPosition;
        private State _currentState;
        private Formatting _formatting;

        /// <summary>
        /// Gets or sets a value indicating whether the underlying stream or
        /// <see cref="TextReader"/> should be closed when the writer is closed.
        /// </summary>
        /// <value>
        /// true to close the underlying stream or <see cref="TextReader"/> when
        /// the writer is closed; otherwise false. The default is true.
        /// </value>
        public bool CloseOutput { get; set; }

        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <value>The top.</value>
        protected internal int Top {
            get {
                int depth = (_stack != null) ? _stack.Count : 0;
                if (Peek() != EdiContainerType.None) {
                    depth++;
                }

                return depth;
            }
        }

        /// <summary>
        /// Gets the state of the writer.
        /// </summary>
        public WriteState WriteState {
            get {
                switch (_currentState) {
                    case State.Error:
                        return WriteState.Error;
                    case State.Closed:
                        return WriteState.Closed;
                    case State.SegmentStart:
                    case State.SegmentName:
                    case State.Segment:
                        return WriteState.Segment;
                    case State.Element:
                    case State.ElementStart:
                        return WriteState.Element;
                    case State.Component:
                    case State.ComponentStart:
                        return WriteState.Component;
                    case State.Start:
                        return WriteState.Start;
                    default:
                        throw EdiWriterException.Create(this, "Invalid state: " + _currentState, null);
                }
            }
        }

        internal string ContainerPath {
            get {
                if (_currentPosition.Type == EdiContainerType.None || _stack == null) {
                    return string.Empty;
                }

                return EdiPosition.BuildPath(_stack);
            }
        }

        /// <summary>
        /// Gets the path of the writer. 
        /// </summary>
        public string Path {
            get {
                if (_currentPosition.Type == EdiContainerType.None) {
                    return string.Empty;
                }

                bool insideContainer = (_currentState != State.ComponentStart
                                        && _currentState != State.ElementStart
                                        && _currentState != State.SegmentStart
                                        && _currentState != State.SegmentName);

                EdiPosition? current = insideContainer ? (EdiPosition?)_currentPosition : null;

                return EdiPosition.BuildPath(current != null ? _stack.Concat(new[] { current.Value }) : _stack);
            }
        }

        private string _dateFormatString;
        private CultureInfo _culture;

        /// <summary>
        /// Indicates how JSON text output is formatted.
        /// </summary>
        public Formatting Formatting {
            get { return _formatting; }
            set {
                if (value < Formatting.None || value > Formatting.LinePerSegment) {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _formatting = value;
            }
        }

        /// <summary>
        /// Get or set how <see cref="DateTime"/> and <see cref="DateTimeOffset"/> values are formatting when writing JSON text.
        /// </summary>
        public string DateFormatString {
            get { return _dateFormatString; }
            set { _dateFormatString = value; }
        }

        /// <summary>
        /// Gets or sets the culture used when writing JSON. Defaults to <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        public CultureInfo Culture {
            get { return _culture ?? CultureInfo.InvariantCulture; }
            set { _culture = value; }
        }

        /// <summary>
        /// Creates an instance of the <c>EdiWriter</c> class. 
        /// </summary>
        protected EdiWriter() {
            _currentState = State.Start;
            _formatting = Formatting.None;

            CloseOutput = true;
        }

        internal void UpdateScopeWithFinishedValue() {
            if (_currentPosition.HasIndex) {
                _currentPosition.Position++;
            }
        }

        private void Push(EdiContainerType value) {
            if (_currentPosition.Type != EdiContainerType.None) {
                if (_stack == null) {
                    _stack = new List<EdiPosition>();
                }

                _stack.Add(_currentPosition);
            }

            _currentPosition = new EdiPosition(value);
        }

        private EdiContainerType Pop() {
            EdiPosition oldPosition = _currentPosition;

            if (_stack != null && _stack.Count > 0) {
                _currentPosition = _stack[_stack.Count - 1];
                _stack.RemoveAt(_stack.Count - 1);
            } else {
                _currentPosition = new EdiPosition();
            }

            return oldPosition.Type;
        }

        private EdiContainerType Peek() {
            return _currentPosition.Type;
        }

        /// <summary>
        /// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Closes this stream and the underlying stream.
        /// </summary>
        public virtual void Close() {
            AutoCompleteAll();
        }

        /// <summary>
        /// Writes the beginning of a Segment which; the name.
        /// </summary>
        public virtual void WriteSegmentName() {
            InternalWriteStart(EdiToken.SegmentName, EdiContainerType.Segment);
        }

        /// <summary>
        /// Writes the end of a JSON object.
        /// </summary>
        public virtual void WriteSegmentTerminator() {
            InternalWriteEnd(EdiContainerType.Segment);
        }


        /// <summary>
        /// Writes the segmant name.
        /// </summary>
        /// <param name="name">The name of the segment.</param>
        public virtual void WriteSegmentName(string name) {
            InternalWriteSegmentName(name);
        }

        /// <summary>
        /// Writes the end of the current JSON object or array.
        /// </summary>
        public virtual void WriteEnd() {
            WriteEnd(Peek());
        }

        /// <summary>
        /// Writes the current <see cref="EdiReader"/> token and its children.
        /// </summary>
        /// <param name="reader">The <see cref="EdiReader"/> to read the token from.</param>
        public void WriteToken(EdiReader reader) {
            WriteToken(reader, true);
        }

        /// <summary>
        /// Writes the current <see cref="EdiReader"/> token.
        /// </summary>
        /// <param name="reader">The <see cref="EdiReader"/> to read the token from.</param>
        /// <param name="writeChildren">A flag indicating whether the current token's children should be written.</param>
        public void WriteToken(EdiReader reader, bool writeChildren) {
            ValidationUtils.ArgumentNotNull(reader, nameof(reader));

            WriteTokenInternal(reader, writeChildren);
        }

        /// <summary>
        /// Writes the <see cref="EdiToken"/> token and its value.
        /// </summary>
        /// <param name="token">The <see cref="EdiToken"/> to write.</param>
        /// <param name="value">
        /// The value to write.
        /// A value is only required for tokens that have an associated value, e.g. the <see cref="String"/> property name for <see cref="EdiToken.SegmentName"/>.
        /// A null value can be passed to the method for token's that don't have a value, e.g. <see cref="EdiToken.SegmentStart"/>.</param>
        public void WriteToken(EdiToken token, object value) {
            switch (token) {
                case EdiToken.None:
                    // read to next
                    break;
                case EdiToken.SegmentStart:
                    // read to next
                    break;
                case EdiToken.SegmentName:
                    // read to next
                    break;
                case EdiToken.ElementStart:
                    // read to next
                    break;
                case EdiToken.ComponentStart:
                    // read to next
                    break;
                case EdiToken.Integer:
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
#if !PORTABLE
                    if (value is BigInteger) {
                        WriteValue((BigInteger)value);
                    } else
#endif
                    {
                        WriteValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
                    }
                    break;
                case EdiToken.Float:
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
                    if (value is decimal) {
                        WriteValue((decimal)value);
                    } else if (value is double) {
                        WriteValue((double)value);
                    } else if (value is float) {
                        WriteValue((float)value);
                    } else {
                        WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
                    }
                    break;
                case EdiToken.String:
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
                    WriteValue(value.ToString());
                    break;
                case EdiToken.Boolean:
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
                    WriteValue(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
                    break;
                case EdiToken.Null:
                    WriteNull();
                    break;
                case EdiToken.Date:
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
                    if (value is DateTimeOffset) {
                        WriteValue((DateTimeOffset)value);
                    } else {
                        WriteValue(Convert.ToDateTime(value, CultureInfo.InvariantCulture));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(token), token, "Unexpected token type.");
            }
        }

        /// <summary>
        /// Writes the <see cref="EdiToken"/> token.
        /// </summary>
        /// <param name="token">The <see cref="EdiToken"/> to write.</param>
        public void WriteToken(EdiToken token) {
            WriteToken(token, null);
        }

        internal virtual void WriteTokenInternal(EdiReader reader, bool writeChildren) {
            int initialDepth;

            if (reader.TokenType == EdiToken.None) {
                initialDepth = -1;
            } else if (!reader.TokenType.IsStartToken()) {
                initialDepth = reader.Depth + 1;
            } else {
                initialDepth = reader.Depth;
            }

            do {

            } while (false); // TODO: Make this right.
                //// stop if we have reached the end of the token being read
                //initialDepth - 1 < reader.Depth - reader.TokenType.IsEndToken() ? 1 : 0)
                //&& writeChildren
                //&& reader.Read());
        }

        private void WriteEnd(EdiContainerType type) {
            switch (type) {
                case EdiContainerType.Segment: break;
                case EdiContainerType.Element: break;
                case EdiContainerType.Component: break;
                default:
                    throw EdiWriterException.Create(this, "Unexpected type when writing end: " + type, null);
            }
        }

        private void AutoCompleteAll() {
            while (Top > 0) {
                WriteEnd();
            }
        }

        private EdiToken GetCloseTokenForType(EdiContainerType type) {
            switch (type) {
                case EdiContainerType.Segment:
                    return EdiToken.SegmentStart;
                case EdiContainerType.Element:
                    return EdiToken.ElementStart;
                case EdiContainerType.Component:
                    return EdiToken.ComponentStart;
                default:
                    throw EdiWriterException.Create(this, "No close token for type: " + type, null);
            }
        }

        private void AutoCompleteClose(EdiContainerType type) {
            // write closing symbol and calculate new state
            int levelsToComplete = 0;

            if (_currentPosition.Type == type) {
                levelsToComplete = 1;
            } else {
                int top = Top - 2;
                for (int i = top; i >= 0; i--) {
                    int currentLevel = top - i;

                    if (_stack[currentLevel].Type == type) {
                        levelsToComplete = i + 2;
                        break;
                    }
                }
            }

            if (levelsToComplete == 0) {
                throw EdiWriterException.Create(this, "No token to close.", null);
            }

            for (int i = 0; i < levelsToComplete; i++) {
                EdiToken token = GetCloseTokenForType(Pop());

                
                WriteEnd(token);

                EdiContainerType currentLevelType = Peek();

                switch (currentLevelType) {
                    case EdiContainerType.Segment:
                        _currentState = State.Segment;
                        break;
                    case EdiContainerType.Element:
                        _currentState = State.Element;
                        break;
                    case EdiContainerType.Component:
                        _currentState = State.Component;
                        break;
                    case EdiContainerType.None:
                        _currentState = State.Start;
                        break;
                    default:
                        throw EdiWriterException.Create(this, "Unknown JsonType: " + currentLevelType, null);
                }
            }
        }

        /// <summary>
        /// Writes the specified end token.
        /// </summary>
        /// <param name="token">The end token to write.</param>
        protected virtual void WriteEnd(EdiToken token) {
        }

        /// <summary>
        /// Writes indent characters.
        /// </summary>
        protected virtual void WriteNewLine() {
        }

        internal void AutoComplete(EdiToken tokenBeingWritten) {
            // gets new state based on the current state and what is being written
            // TODO: fix this too.
            State newState = State.Error;

            if (newState == State.Error) {
                throw EdiWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), _currentState.ToString()), null);
            }
            
            _currentState = newState;
        }

        #region WriteValue methods
        /// <summary>
        /// Writes a null value.
        /// </summary>
        public virtual void WriteNull() {
            InternalWriteValue(EdiToken.Null);
        }

        /// <summary>
        /// Writes raw EDI fragment without changing the writer's state.
        /// </summary>
        /// <param name="fragment">A raw EDI fragment to write.</param>
        public virtual void WriteRaw(string fragment) {
            InternalWriteRaw();
        }

        /// <summary>
        /// Writes a <see cref="String"/> value.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to write.</param>
        public virtual void WriteValue(string value) {
            InternalWriteValue(EdiToken.String);
        }

        /// <summary>
        /// Writes a <see cref="Int32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to write.</param>
        public virtual void WriteValue(int value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="UInt32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(uint value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Int64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to write.</param>
        public virtual void WriteValue(long value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="UInt64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(ulong value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Single"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to write.</param>
        public virtual void WriteValue(float value) {
            InternalWriteValue(EdiToken.Float);
        }

        /// <summary>
        /// Writes a <see cref="Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> value to write.</param>
        public virtual void WriteValue(double value) {
            InternalWriteValue(EdiToken.Float);
        }

        /// <summary>
        /// Writes a <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to write.</param>
        public virtual void WriteValue(bool value) {
            InternalWriteValue(EdiToken.Boolean);
        }

        /// <summary>
        /// Writes a <see cref="Int16"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to write.</param>
        public virtual void WriteValue(short value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="UInt16"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(ushort value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Char"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Char"/> value to write.</param>
        public virtual void WriteValue(char value) {
            InternalWriteValue(EdiToken.String);
        }

        /// <summary>
        /// Writes a <see cref="Byte"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Byte"/> value to write.</param>
        public virtual void WriteValue(byte value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="SByte"/> value.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(sbyte value) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Decimal"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to write.</param>
        public virtual void WriteValue(decimal value) {
            InternalWriteValue(EdiToken.Float);
        }

        /// <summary>
        /// Writes a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        public virtual void WriteValue(DateTime value) {
            InternalWriteValue(EdiToken.Date);
        }

        /// <summary>
        /// Writes a <see cref="DateTimeOffset"/> value.
        /// </summary>
        /// <param name="value">The <see cref="DateTimeOffset"/> value to write.</param>
        public virtual void WriteValue(DateTimeOffset value) {
            InternalWriteValue(EdiToken.Date);
        }

        /// <summary>
        /// Writes a <see cref="Guid"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Guid"/> value to write.</param>
        public virtual void WriteValue(Guid value) {
            InternalWriteValue(EdiToken.String);
        }

        /// <summary>
        /// Writes a <see cref="TimeSpan"/> value.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> value to write.</param>
        public virtual void WriteValue(TimeSpan value) {
            InternalWriteValue(EdiToken.String);
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Int32}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Int32}"/> value to write.</param>
        public virtual void WriteValue(int? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{UInt32}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{UInt32}"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(uint? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Int64}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Int64}"/> value to write.</param>
        public virtual void WriteValue(long? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{UInt64}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{UInt64}"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(ulong? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Single}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Single}"/> value to write.</param>
        public virtual void WriteValue(float? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Double}"/> value to write.</param>
        public virtual void WriteValue(double? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Boolean}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Boolean}"/> value to write.</param>
        public virtual void WriteValue(bool? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Int16}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Int16}"/> value to write.</param>
        public virtual void WriteValue(short? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{UInt16}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{UInt16}"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(ushort? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Char}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Char}"/> value to write.</param>
        public virtual void WriteValue(char? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Byte}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Byte}"/> value to write.</param>
        public virtual void WriteValue(byte? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{SByte}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{SByte}"/> value to write.</param>
        [CLSCompliant(false)]
        public virtual void WriteValue(sbyte? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Decimal}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Decimal}"/> value to write.</param>
        public virtual void WriteValue(decimal? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{DateTime}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{DateTime}"/> value to write.</param>
        public virtual void WriteValue(DateTime? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{DateTimeOffset}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{DateTimeOffset}"/> value to write.</param>
        public virtual void WriteValue(DateTimeOffset? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Guid}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Guid}"/> value to write.</param>
        public virtual void WriteValue(Guid? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{TimeSpan}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{TimeSpan}"/> value to write.</param>
        public virtual void WriteValue(TimeSpan? value) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Writes a <see cref="Uri"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Uri"/> value to write.</param>
        public virtual void WriteValue(Uri value) {
            if (value == null) {
                WriteNull();
            } else {
                InternalWriteValue(EdiToken.String);
            }
        }

        /// <summary>
        /// Writes a <see cref="Object"/> value.
        /// An error will raised if the value cannot be written as a single JSON token.
        /// </summary>
        /// <param name="value">The <see cref="Object"/> value to write.</param>
        public virtual void WriteValue(object value) {
            if (value == null) {
                WriteNull();
            } else {
#if !PORTABLE
                // this is here because adding a WriteValue(BigInteger) to EdiWriter will
                // mean the user has to add a reference to System.Numerics.dll
                if (value is BigInteger) {
                    throw CreateUnsupportedTypeException(this, value);
                }
#endif

                WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value);
            }
        }
        #endregion

        /// <summary>
        /// Writes out the given white space.
        /// </summary>
        /// <param name="ws">The string of white space characters.</param>
        public virtual void WriteWhitespace(string ws) {
            InternalWriteWhitespace(ws);
        }

        void IDisposable.Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (_currentState != State.Closed && disposing) {
                Close();
            }
        }

        internal static void WriteValue(EdiWriter writer, PrimitiveTypeCode typeCode, object value) {
            switch (typeCode) {
                case PrimitiveTypeCode.Char:
                    writer.WriteValue((char)value);
                    break;
                case PrimitiveTypeCode.CharNullable:
                    writer.WriteValue((value == null) ? (char?)null : (char)value);
                    break;
                case PrimitiveTypeCode.Boolean:
                    writer.WriteValue((bool)value);
                    break;
                case PrimitiveTypeCode.BooleanNullable:
                    writer.WriteValue((value == null) ? (bool?)null : (bool)value);
                    break;
                case PrimitiveTypeCode.SByte:
                    writer.WriteValue((sbyte)value);
                    break;
                case PrimitiveTypeCode.SByteNullable:
                    writer.WriteValue((value == null) ? (sbyte?)null : (sbyte)value);
                    break;
                case PrimitiveTypeCode.Int16:
                    writer.WriteValue((short)value);
                    break;
                case PrimitiveTypeCode.Int16Nullable:
                    writer.WriteValue((value == null) ? (short?)null : (short)value);
                    break;
                case PrimitiveTypeCode.UInt16:
                    writer.WriteValue((ushort)value);
                    break;
                case PrimitiveTypeCode.UInt16Nullable:
                    writer.WriteValue((value == null) ? (ushort?)null : (ushort)value);
                    break;
                case PrimitiveTypeCode.Int32:
                    writer.WriteValue((int)value);
                    break;
                case PrimitiveTypeCode.Int32Nullable:
                    writer.WriteValue((value == null) ? (int?)null : (int)value);
                    break;
                case PrimitiveTypeCode.Byte:
                    writer.WriteValue((byte)value);
                    break;
                case PrimitiveTypeCode.ByteNullable:
                    writer.WriteValue((value == null) ? (byte?)null : (byte)value);
                    break;
                case PrimitiveTypeCode.UInt32:
                    writer.WriteValue((uint)value);
                    break;
                case PrimitiveTypeCode.UInt32Nullable:
                    writer.WriteValue((value == null) ? (uint?)null : (uint)value);
                    break;
                case PrimitiveTypeCode.Int64:
                    writer.WriteValue((long)value);
                    break;
                case PrimitiveTypeCode.Int64Nullable:
                    writer.WriteValue((value == null) ? (long?)null : (long)value);
                    break;
                case PrimitiveTypeCode.UInt64:
                    writer.WriteValue((ulong)value);
                    break;
                case PrimitiveTypeCode.UInt64Nullable:
                    writer.WriteValue((value == null) ? (ulong?)null : (ulong)value);
                    break;
                case PrimitiveTypeCode.Single:
                    writer.WriteValue((float)value);
                    break;
                case PrimitiveTypeCode.SingleNullable:
                    writer.WriteValue((value == null) ? (float?)null : (float)value);
                    break;
                case PrimitiveTypeCode.Double:
                    writer.WriteValue((double)value);
                    break;
                case PrimitiveTypeCode.DoubleNullable:
                    writer.WriteValue((value == null) ? (double?)null : (double)value);
                    break;
                case PrimitiveTypeCode.DateTime:
                    writer.WriteValue((DateTime)value);
                    break;
                case PrimitiveTypeCode.DateTimeNullable:
                    writer.WriteValue((value == null) ? (DateTime?)null : (DateTime)value);
                    break;
                case PrimitiveTypeCode.DateTimeOffset:
                    writer.WriteValue((DateTimeOffset)value);
                    break;
                case PrimitiveTypeCode.DateTimeOffsetNullable:
                    writer.WriteValue((value == null) ? (DateTimeOffset?)null : (DateTimeOffset)value);
                    break;
                case PrimitiveTypeCode.Decimal:
                    writer.WriteValue((decimal)value);
                    break;
                case PrimitiveTypeCode.DecimalNullable:
                    writer.WriteValue((value == null) ? (decimal?)null : (decimal)value);
                    break;
                case PrimitiveTypeCode.Guid:
                    writer.WriteValue((Guid)value);
                    break;
                case PrimitiveTypeCode.GuidNullable:
                    writer.WriteValue((value == null) ? (Guid?)null : (Guid)value);
                    break;
                case PrimitiveTypeCode.TimeSpan:
                    writer.WriteValue((TimeSpan)value);
                    break;
                case PrimitiveTypeCode.TimeSpanNullable:
                    writer.WriteValue((value == null) ? (TimeSpan?)null : (TimeSpan)value);
                    break;
#if !PORTABLE
                case PrimitiveTypeCode.BigInteger:
                    // this will call to WriteValue(object)
                    writer.WriteValue((BigInteger)value);
                    break;
                case PrimitiveTypeCode.BigIntegerNullable:
                    // this will call to WriteValue(object)
                    writer.WriteValue((value == null) ? (BigInteger?)null : (BigInteger)value);
                    break;
#endif
                case PrimitiveTypeCode.Uri:
                    writer.WriteValue((Uri)value);
                    break;
                case PrimitiveTypeCode.String:
                    writer.WriteValue((string)value);
                    break;
                case PrimitiveTypeCode.Bytes:
                    writer.WriteValue((byte[])value);
                    break;
#if !(PORTABLE || DOTNET)
                case PrimitiveTypeCode.DBNull:
                    writer.WriteNull();
                    break;
#endif
                default:
#if !PORTABLE
                    if (value is IConvertible) {
                        // the value is a non-standard IConvertible
                        // convert to the underlying value and retry
                        IConvertible convertable = (IConvertible)value;

                        TypeInformation typeInformation = ConvertUtils.GetTypeInformation(convertable);

                        // if convertable has an underlying typecode of Object then attempt to convert it to a string
                        PrimitiveTypeCode resolvedTypeCode = (typeInformation.TypeCode == PrimitiveTypeCode.Object) ? PrimitiveTypeCode.String : typeInformation.TypeCode;
                        Type resolvedType = (typeInformation.TypeCode == PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type;

                        object convertedValue = convertable.ToType(resolvedType, CultureInfo.InvariantCulture);

                        WriteValue(writer, resolvedTypeCode, convertedValue);
                        break;
                    } else
#endif
                    {
                        throw CreateUnsupportedTypeException(writer, value);
                    }
            }
        }

        private static EdiWriterException CreateUnsupportedTypeException(EdiWriter writer, object value) {
            return EdiWriterException.Create(writer, "Unsupported type: {0}. Use the EdiSerializer class to get the object's EDI representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
        }

        /// <summary>
        /// Sets the state of the EdiWriter,
        /// </summary>
        /// <param name="token">The EdiToken being written.</param>
        /// <param name="value">The value being written.</param>
        protected void SetWriteState(EdiToken token, object value) {
            switch (token) {
                case EdiToken.SegmentStart:
                    InternalWriteStart(token, EdiContainerType.Segment);
                    break;
                case EdiToken.ElementStart:
                    InternalWriteStart(token, EdiContainerType.Element);
                    break;
                case EdiToken.ComponentStart:
                    InternalWriteStart(token, EdiContainerType.Component);
                    break;
                case EdiToken.SegmentName:
                    if (!(value is string)) {
                        throw new ArgumentException("A name is required when setting property name state.", nameof(value));
                    }

                    InternalWriteSegmentName((string)value);
                    break;
                case EdiToken.Integer:
                case EdiToken.Float:
                case EdiToken.String:
                case EdiToken.Boolean:
                case EdiToken.Date:
                case EdiToken.Null:
                default:
                    throw new ArgumentOutOfRangeException(nameof(token));
            }
        }

        internal void InternalWriteEnd(EdiContainerType container) {
            AutoCompleteClose(container);
        }

        internal void InternalWriteSegmentName(string name) {
            _currentPosition.SegmentName = name;
            AutoComplete(EdiToken.SegmentName);
        }

        internal void InternalWriteRaw() {
        }

        internal void InternalWriteStart(EdiToken token, EdiContainerType container) {
            UpdateScopeWithFinishedValue();
            AutoComplete(token);
            Push(container);
        }

        internal void InternalWriteValue(EdiToken token) {
            UpdateScopeWithFinishedValue();
            AutoComplete(token);
        }

        internal void InternalWriteWhitespace(string ws) {
            if (ws != null) {
                if (!StringUtils.IsWhiteSpace(ws)) {
                    throw EdiWriterException.Create(this, "Only white space characters should be used.", null);
                }
            }
        }
    }
}