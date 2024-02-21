using System;
using System.Collections.Generic;
using System.IO;
#if !(PORTABLE || NETSTANDARD10)
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
            Segment = 2,
            Element = 3,
            Component = 4,
            Value = 5,
            Closed = 6,
            Error = 7
        }
        // array that gives a new state based on the current state an the token being written
        private static readonly State[][] StateArray;

        internal static readonly State[][] StateArrayTempate =
        {
            //                                       Start                   SegmentName         Segment           Element           Component          Value              Closed       Error
            //
            /* None                         */new[] { State.Error,           State.Error,        State.Error,      State.Error,      State.Error,       State.Error,       State.Error, State.Error },
            /* SegmentStart                 */new[] { State.Segment,         State.Segment,      State.Error,      State.Segment,    State.Segment,     State.Segment,     State.Error, State.Error },
            /* SegmentName                  */new[] { State.SegmentName,     State.SegmentName,  State.SegmentName,State.SegmentName,State.SegmentName, State.SegmentName, State.Error, State.Error },
            /* ElementStart                 */new[] { State.Error,           State.Error,        State.Error,      State.Element,    State.Element,     State.Element,     State.Error, State.Error },
            /* ComponentStart               */new[] { State.Error,           State.Component,    State.Error,      State.Component,  State.Component,   State.Component,   State.Error, State.Error },
            /* Value (this will be copied)  */new[] { State.Error,           State.Value,        State.Error,      State.Value,      State.Value,       State.Value,       State.Error, State.Error }
        };

        internal static State[][] BuildStateArray() {
            var allStates = StateArrayTempate.ToList();
            var errorStates = StateArrayTempate[0];
            var valueStates = StateArrayTempate[5];

            foreach (EdiToken valueToken in Enum.GetValues(typeof(EdiToken))) {
                if (allStates.Count <= (int)valueToken) {
                    switch (valueToken) {
                        case EdiToken.Integer:
                        case EdiToken.Float:
                        case EdiToken.String:
                        case EdiToken.Boolean:
                        case EdiToken.Null:
                        case EdiToken.Date:
                            allStates.Add(valueStates);
                            break;
                        default:
                            allStates.Add(errorStates);
                            break;
                    }
                }
            }

            return allStates.ToArray();
        }

        static EdiWriter() {
            StateArray = BuildStateArray();
        }

        private readonly IEdiGrammar _grammar;
        private List<EdiPosition> _stack;
        private EdiPosition _currentPosition;
        private State _currentState;
        private Formatting _formatting;
        private CultureInfo _culture;

        /// <summary>
        /// Gets the <see cref="IEdiGrammar"/> rules for use in the reader.
        /// </summary>
        /// <value>The current reader state.</value>
        public IEdiGrammar Grammar {
            get { return _grammar; }
        }
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
                    case State.Start:
                        return WriteState.Start;
                    case State.SegmentName:
                    case State.Segment:
                        return WriteState.Segment;
                    case State.Element:
                        return WriteState.Element;
                    case State.Component:
                    case State.Value:
                        return WriteState.Component;
                    case State.Closed:
                        return WriteState.Closed;
                    case State.Error:
                        return WriteState.Error;
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

                bool insideContainer = (_currentState != State.Closed
                                        && _currentState != State.Error);

                EdiPosition? current = insideContainer ? (EdiPosition?)_currentPosition : null;
                var stack = _stack ?? new List<EdiPosition>();
                return EdiPosition.BuildPath(current != null ? stack.Concat(new[] { current.Value }) : _stack);
            }
        }


        /// <summary>
        /// Indicates how Edi text output is formatted.
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
        /// Absolute number of segments inside the transmition
        /// </summary>
        public int AbsoluteSegmentCount {
            get {
                return (_stack?.Count > 0 ? _stack[0].Position : _currentPosition.Position) + 1;
            }
        }

        /// <summary>
        /// The number of segments contextual.
        /// </summary>
        public int SegmentCount {
            get { return _currentPosition.SegmentCount; }
        }

        /// <summary>
        /// The number of messages contextual.
        /// </summary>
        public int MessageCount {
            get { return _currentPosition.MessageCount; }
        }

        /// <summary>
        /// The number of messages contextual.
        /// </summary>
        public int GroupCount {
            get { return _currentPosition.GroupCount; }
        }

        /// <summary>
        /// Gets or sets the culture used when writing Edi. Defaults to <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        public CultureInfo Culture {
            get { return _culture ?? CultureInfo.InvariantCulture; }
            set { _culture = value; }
        }

        /// <summary>
        /// Enable compression of edi messages. Avoids unnecessary empty component separators.
        /// </summary>
        public bool EnableCompression { get; set; }

        /// <summary>
        /// Escape decimal mark when encountered in text.
        /// </summary>
        public virtual bool EscapeDecimalMarkInText { get; set; }

        internal bool LastWriteNull { get; set; }
        internal int UnwrittenComponents { get; set; }
        internal int UnwrittenElements { get; set; }

        /// <summary>
        /// Creates an instance of the <c>EdiWriter</c> class. 
        /// </summary>
        protected EdiWriter(IEdiGrammar grammar) {
            if (null == grammar)
                throw new ArgumentNullException(nameof(grammar));
            _grammar = grammar;
            _currentState = State.Start;
            _formatting = Formatting.LinePerSegment;
            CloseOutput = true;
        }

        private void Push(EdiContainerType value) {
            if (value == EdiContainerType.None) {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Unexpected container type.");
            }
            if (_stack == null) {
                _stack = new List<EdiPosition>();
            }
            if (_currentPosition.Type == value) {
                _currentPosition.Position++;
            } else if (_currentPosition.HasIndex) {
                EdiPosition oldPosition = _currentPosition;
                _stack.Add(oldPosition);
                _currentPosition = new EdiPosition(value, oldPosition);
                _currentPosition.Position = 0;
            } else {
                _currentPosition = new EdiPosition(value);
                _currentPosition.Position = 0;
            }
        }

        private EdiContainerType Pop() {
            var oldPosition = _currentPosition;

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
        /// Writes the segmant name. And marks the beginning of a Segment.
        /// </summary>
        /// <param name="name">The name of the segment.</param>
        public virtual void WriteSegmentName(string name) {
            InternalWriteStart(EdiToken.SegmentName, EdiContainerType.Segment);
            InternalWriteSegmentName(name);
        }

        /// <summary>
        /// Writes the end of the current EDI structure.
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
        /// A value is only required for tokens that have an associated value, e.g. the <see cref="String"/> segmanet name name for <see cref="EdiToken.SegmentName"/>.
        /// A null value can be passed to the method for token's that don't have a value, e.g. <see cref="EdiToken.SegmentStart"/>.</param>
        public void WriteToken(EdiToken token, object value) {
            switch (token) {
                case EdiToken.None:
                    // read to next
                    break;
                case EdiToken.SegmentStart:
                    WriteSegmentTerminator();
                    break;
                case EdiToken.SegmentName:
                    // read to next
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
                    WriteSegmentName(value.ToString());
                    break;
                case EdiToken.ElementStart:
                    InternalWriteStart(EdiToken.ElementStart, EdiContainerType.Element);
                    if (!EnableCompression) WriteElementDelimiter();
                    break;
                case EdiToken.ComponentStart:
                    InternalWriteStart(EdiToken.ComponentStart, EdiContainerType.Component);
                    if (!EnableCompression) WriteComponentDelimiter();
                    break;
                case EdiToken.Integer:
                    ValidationUtils.ArgumentNotNull(value, nameof(value));
#if !(PORTABLE || NETSTANDARD10)
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
            // TODO: Write using an open EDI reader (Copy).
            throw new NotImplementedException();
        }

        private void WriteEnd(EdiContainerType type) {
            switch (type) {
                case EdiContainerType.Segment:
                case EdiContainerType.Element:
                case EdiContainerType.Component:
                    InternalWriteEnd(type);
                    break;
                default:
                    throw EdiWriterException.Create(this, "Unexpected type when writing end: " + type, null);
            }
        }

        private void AutoCompleteAll() {
            AutoCompleteClose(EdiContainerType.None);
        }

        private EdiToken GetCloseTokenForType(EdiContainerType type) {
            switch (type) {
                case EdiContainerType.Segment:
                    return EdiToken.SegmentStart;
                case EdiContainerType.Element:
                    return EdiToken.None;
                case EdiContainerType.Component:
                    return EdiToken.None;
                case EdiContainerType.None:
                    return EdiToken.None;
                default:
                    throw EdiWriterException.Create(this, "No close token for type: " + type, null);
            }
        }

        private void AutoCompleteClose(EdiContainerType type) {
            // write closing symbol and calculate new state
            while (Peek() >= type && Top > 0) {

                EdiToken token;
                bool forcebreak = false;
                if (Peek() == type && _currentPosition.HasIndex) {
                    token = GetCloseTokenForType(type);
                    forcebreak = true;
                } else {
                    token = GetCloseTokenForType(Pop());
                }

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
                        throw EdiWriterException.Create(this, "Unknown EdiContainerType: " + currentLevelType, null);
                }
                if (forcebreak)
                    break;
            }
        }

        /// <summary>
        /// Writes the specified end token.
        /// </summary>
        /// <param name="token">The end token to write.</param>
        protected virtual void WriteEnd(EdiToken token) {
            switch (token) {
                case EdiToken.SegmentStart:
                    WriteSegmentTerminator();
                    break;
            }
        }

        /// <summary>
        /// Writes the Service string advice. Usualy this is the first thing to write in a transmission and it is done by consulting with the current <see cref="IEdiGrammar"/>.
        /// </summary>
        public void WriteServiceStringAdvice() {
            if (_currentState != State.Start) {
                throw EdiWriterException.Create(this, "Service string advice can only be applied at the begining of the iterchange. Current state is: {0}".FormatWith(Culture, _currentState), null);
            }
            if (null != Grammar.ServiceStringAdviceTag) {
                WriteRaw(string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                              Grammar.ServiceStringAdviceTag,
                                              Grammar.ComponentDataElementSeparator,
                                              Grammar.DataElementSeparator,
                                              Grammar.DecimalMark,
                                              Grammar.ReleaseCharacter,
                                              new string(Grammar.Reserved),
                                              Grammar.SegmentTerminator));
                if (Formatting == Formatting.LinePerSegment) {
                    WriteNewLine();
                }
            }
        }

        /// <summary>
        /// Writes indent characters. Line terminator if allowed by the current <see cref="IEdiGrammar"/>.
        /// </summary>
        protected virtual void WriteNewLine() {

        }

        /// <summary>
        /// Writes the tag name of the <see cref="EdiContainerType.Segment"/>.
        /// </summary>
        protected virtual void WriteSegmentNameDelimiter() {

        }

        /// <summary>
        /// Writes the end of a Edi <see cref="EdiContainerType.Segment"/>.
        /// </summary>
        public virtual void WriteSegmentTerminator() {
            if (_formatting == Formatting.LinePerSegment) {
                WriteNewLine();
            }
        }

        /// <summary>
        /// Writes an <see cref="EdiContainerType.Element"/> separator.
        /// </summary>
        protected virtual void WriteElementDelimiter() {

        }

        /// <summary>
        /// Writes an <see cref="EdiContainerType.Component"/> separator.
        /// </summary>
        protected virtual void WriteComponentDelimiter() {

        }

        internal void AutoComplete(EdiToken tokenBeingWritten) {

            // gets new state based on the current state and what is being written
            State newState = StateArray[(int)tokenBeingWritten][(int)_currentState];
            if (newState == State.Error) {
                throw EdiWriterException.Create(this, "Token {0} in state {1} would result in an invalid Edi object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), _currentState.ToString()), null);
            }
            var isPrimitiveToken = tokenBeingWritten.IsPrimitiveToken();
            if (EnableCompression && isPrimitiveToken && !LastWriteNull) {
                for (var i = 0; i < UnwrittenElements; i++) {
                    WriteElementDelimiter();
                }
                for (var i = 0; i < UnwrittenComponents; i++) {
                    WriteComponentDelimiter();
                }
                UnwrittenElements = UnwrittenComponents = 0;
            }
            if ((_currentState == State.Element || _currentState == State.Component || _currentState == State.Value || _currentState == State.SegmentName) && tokenBeingWritten == EdiToken.SegmentName) {
                AutoCompleteClose(EdiContainerType.Segment);
            } else if (_currentState == State.SegmentName && isPrimitiveToken) {
                Push(EdiContainerType.Element);
                Push(EdiContainerType.Component);
            } else if (_currentState == State.Element && isPrimitiveToken) {
                Push(EdiContainerType.Component);
            } else if (_currentState == State.Value && isPrimitiveToken) {
                AutoCompleteClose(EdiContainerType.Component);
                if (!EnableCompression || !LastWriteNull) {
                    WriteComponentDelimiter();
                }
                Push(EdiContainerType.Component);
            } else if ((_currentState == State.Component || _currentState == State.Value) && tokenBeingWritten == EdiToken.ElementStart) {
                AutoCompleteClose(EdiContainerType.Element);
            } else if ((_currentState == State.Component || _currentState == State.Value) && tokenBeingWritten == EdiToken.ComponentStart) {
                AutoCompleteClose(EdiContainerType.Component);
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
        /// <param name="picture"></param>
        public virtual void WriteValue(string value, Picture? picture) {
            InternalWriteValue(EdiToken.String);
        }

        /// <summary>
        /// Writes a <see cref="Int32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(int value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="UInt32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(uint value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Int64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(long value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="UInt64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(ulong value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Single"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(float value, Picture? picture) {
            InternalWriteValue(EdiToken.Float);
        }

        /// <summary>
        /// Writes a <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(double value, Picture? picture) {
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
        /// <param name="picture"></param>
        public virtual void WriteValue(short value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="UInt16"/> value.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(ushort value, Picture? picture) {
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
        /// <param name="picture"></param>
        public virtual void WriteValue(sbyte value, Picture? picture) {
            InternalWriteValue(EdiToken.Integer);
        }

        /// <summary>
        /// Writes a <see cref="Decimal"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(decimal value, Picture? picture) {
            InternalWriteValue(EdiToken.Float);
        }

        /// <summary>
        /// Writes a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        /// <param name="format"></param>
        public virtual void WriteValue(DateTime value, string format) {
            InternalWriteValue(EdiToken.Date);
        }

        /// <summary>
        /// Writes a <see cref="DateTimeOffset"/> value.
        /// </summary>
        /// <param name="value">The <see cref="DateTimeOffset"/> value to write.</param>
        /// <param name="format"></param>
        public virtual void WriteValue(DateTimeOffset value, string format) {
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
        /// <param name="picture"></param>
        public virtual void WriteValue(int? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{UInt32}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{UInt32}"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(uint? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Int64}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Int64}"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(long? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{UInt64}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{UInt64}"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(ulong? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Single}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Single}"/> value to write.</param>
        /// <param name="picture"></param>
        public virtual void WriteValue(float? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Double}"/> value to write.</param>
        /// <param name="picture">The <see cref="Nullable{Picture}"/> picture that discribes the value.</param>
        public virtual void WriteValue(double? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
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
        /// <param name="picture">The <see cref="Nullable{Picture}"/> picture that discribes the value.</param>
        public virtual void WriteValue(short? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{UInt16}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{UInt16}"/> value to write.</param>
        /// <param name="picture">The <see cref="Nullable{Picture}"/> picture that discribes the value.</param>
        public virtual void WriteValue(ushort? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
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
        /// <param name="picture">The <see cref="Nullable{Picture}"/> picture that discribes the value.</param>
        public virtual void WriteValue(sbyte? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{Decimal}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{Decimal}"/> value to write.</param>
        /// <param name="picture">The <see cref="Nullable{Picture}"/> picture that discribes the value.</param>
        public virtual void WriteValue(decimal? value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), picture);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{DateTime}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{DateTime}"/> value to write.</param>
        /// <param name="format"></param>
        public virtual void WriteValue(DateTime? value, string format = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), format);
            }
        }

        /// <summary>
        /// Writes a <see cref="Nullable{DateTimeOffset}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Nullable{DateTimeOffset}"/> value to write.</param>
        /// <param name="format"></param>
        public virtual void WriteValue(DateTimeOffset? value, string format = null) {
            if (value == null) {
                WriteNull();
            } else {
                WriteValue(value.GetValueOrDefault(), format);
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
        public virtual void WriteValue(Uri value, Picture? picture = null) {
            if (value == null) {
                WriteNull();
            } else {
                InternalWriteValue(EdiToken.String);
            }
        }

        /// <summary>
        /// Writes a <see cref="object"/> value.
        /// An error will raised if the value cannot be written as a single Edi token.
        /// </summary>
        /// <param name="value">The <see cref="object"/> value to write.</param>
        public virtual void WriteValue(object value) {
            WriteValue(value, null, null);
        }

        /// <summary>
        /// Writes a <see cref="object"/> value.
        /// An error will raised if the value cannot be written as a single Edi token.
        /// </summary>
        /// <param name="value">The <see cref="object"/> value to write.</param>
        /// <param name="picture"></param>
        /// <param name="format">traditional string format mask</param>
        public virtual void WriteValue(object value, Picture? picture, string format) {
            if (value == null) {
                WriteNull();
            } else {
#if !(PORTABLE || NETSTANDARD10)
                // this is here because adding a WriteValue(BigInteger) to EdiWriter will
                // mean the user has to add a reference to System.Numerics.dll
                if (value is BigInteger) {
                    throw CreateUnsupportedTypeException(this, value);
                }
#endif

                WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value, picture, format);
            }
        }
        #endregion


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

        internal static void WriteValue(EdiWriter writer, PrimitiveTypeCode typeCode, object value, Picture? picture, string format) {
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
                    writer.WriteValue((sbyte)value, picture);
                    break;
                case PrimitiveTypeCode.SByteNullable:
                    writer.WriteValue((value == null) ? (sbyte?)null : (sbyte)value, picture);
                    break;
                case PrimitiveTypeCode.Int16:
                    writer.WriteValue((short)value, picture);
                    break;
                case PrimitiveTypeCode.Int16Nullable:
                    writer.WriteValue((value == null) ? (short?)null : (short)value, picture);
                    break;
                case PrimitiveTypeCode.UInt16:
                    writer.WriteValue((ushort)value, picture);
                    break;
                case PrimitiveTypeCode.UInt16Nullable:
                    writer.WriteValue((value == null) ? (ushort?)null : (ushort)value, picture);
                    break;
                case PrimitiveTypeCode.Int32:
                    writer.WriteValue((int)value, picture);
                    break;
                case PrimitiveTypeCode.Int32Nullable:
                    writer.WriteValue((value == null) ? (int?)null : (int)value, picture);
                    break;
                case PrimitiveTypeCode.Byte:
                    writer.WriteValue((byte)value, picture);
                    break;
                case PrimitiveTypeCode.ByteNullable:
                    writer.WriteValue((value == null) ? (byte?)null : (byte)value, picture);
                    break;
                case PrimitiveTypeCode.UInt32:
                    writer.WriteValue((uint)value, picture);
                    break;
                case PrimitiveTypeCode.UInt32Nullable:
                    writer.WriteValue((value == null) ? (uint?)null : (uint)value);
                    break;
                case PrimitiveTypeCode.Int64:
                    writer.WriteValue((long)value, picture);
                    break;
                case PrimitiveTypeCode.Int64Nullable:
                    writer.WriteValue((value == null) ? (long?)null : (long)value, picture);
                    break;
                case PrimitiveTypeCode.UInt64:
                    writer.WriteValue((ulong)value, picture);
                    break;
                case PrimitiveTypeCode.UInt64Nullable:
                    writer.WriteValue((value == null) ? (ulong?)null : (ulong)value, picture);
                    break;
                case PrimitiveTypeCode.Single:
                    writer.WriteValue((float)value, picture);
                    break;
                case PrimitiveTypeCode.SingleNullable:
                    writer.WriteValue((value == null) ? (float?)null : (float)value);
                    break;
                case PrimitiveTypeCode.Double:
                    writer.WriteValue((double)value, picture);
                    break;
                case PrimitiveTypeCode.DoubleNullable:
                    writer.WriteValue((value == null) ? (double?)null : (double)value, picture);
                    break;
                case PrimitiveTypeCode.DateTime:
                    writer.WriteValue((DateTime)value, format);
                    break;
                case PrimitiveTypeCode.DateTimeNullable:
                    writer.WriteValue((value == null) ? (DateTime?)null : (DateTime)value, format);
                    break;
                case PrimitiveTypeCode.DateTimeOffset:
                    writer.WriteValue((DateTimeOffset)value, format);
                    break;
                case PrimitiveTypeCode.DateTimeOffsetNullable:
                    writer.WriteValue((value == null) ? (DateTimeOffset?)null : (DateTimeOffset)value, format);
                    break;
                case PrimitiveTypeCode.Decimal:
                    writer.WriteValue((decimal)value, picture);
                    break;
                case PrimitiveTypeCode.DecimalNullable:
                    writer.WriteValue((value == null) ? (decimal?)null : (decimal)value, picture);
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
#if !(PORTABLE || NETSTANDARD10)
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
                    writer.WriteValue((string)value, picture);
                    break;
                case PrimitiveTypeCode.Bytes:
                    writer.WriteValue((byte[])value);
                    break;
#if !(PORTABLE || NETSTANDARD10)
                case PrimitiveTypeCode.DBNull:
                    writer.WriteNull();
                    break;
#endif
                default:
#if !(PORTABLE || NETSTANDARD10)
                    if (value is IConvertible) {
                        // the value is a non-standard IConvertible
                        // convert to the underlying value and retry
                        IConvertible convertable = (IConvertible)value;

                        TypeInformation typeInformation = ConvertUtils.GetTypeInformation(convertable);

                        // if convertable has an underlying typecode of Object then attempt to convert it to a string
                        PrimitiveTypeCode resolvedTypeCode = (typeInformation.TypeCode == PrimitiveTypeCode.Object) ? PrimitiveTypeCode.String : typeInformation.TypeCode;
                        Type resolvedType = (typeInformation.TypeCode == PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type;

                        object convertedValue = convertable.ToType(resolvedType, CultureInfo.InvariantCulture);

                        WriteValue(writer, resolvedTypeCode, convertedValue, picture, format);
                        break;
                    } else
#endif
                    {
                        WriteValue(writer, PrimitiveTypeCode.String, $"{value}", picture, format);
                        break;
                        // consider throwing some times...
                        //throw CreateUnsupportedTypeException(writer, value);
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
                    InternalWriteValue(token);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(token));
            }
        }

        internal void InternalWriteEnd(EdiContainerType container) {
            AutoCompleteClose(container);
        }

        internal void InternalWriteSegmentName(string name) {
            _currentPosition.SegmentName = name;
            _currentPosition.AdvanceContrlCount(Grammar);
        }

        internal void InternalWriteRaw() {
        }

        internal void InternalWriteStart(EdiToken token, EdiContainerType container) {
            if (EnableCompression)
                switch (container) {
                    case EdiContainerType.Segment:
                        UnwrittenElements = UnwrittenComponents = 0;
                        break;
                    case EdiContainerType.Element:
                        UnwrittenComponents = 0;
                        UnwrittenElements++;
                        break;
                    case EdiContainerType.Component:
                        UnwrittenComponents++;
                        break;
                }
            AutoComplete(token);
            Push(container);
        }

        internal void InternalWriteValue(EdiToken token) {
            LastWriteNull = (token == EdiToken.Null);
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