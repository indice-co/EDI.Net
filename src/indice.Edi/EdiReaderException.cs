using System;

namespace indice.Edi
{
    /// <summary>
    /// The exception thrown when an error occurs while reading EDI text.
    /// </summary>
#if !(DOTNET || PORTABLE)
    [Serializable]
#endif
    public class EdiReaderException : EdiException
    {
        /// <summary>
        /// Gets the line number indicating where the error occurred.
        /// </summary>
        /// <value>The line number indicating where the error occurred.</value>
        public int LineNumber { get; private set; }

        /// <summary>
        /// Gets the line position indicating where the error occurred.
        /// </summary>
        /// <value>The line position indicating where the error occurred.</value>
        public int LinePosition { get; private set; }

        /// <summary>
        /// Gets the path to the EDI where the error occurred.
        /// </summary>
        /// <value>The path to the EDI where the error occurred.</value>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiReaderException"/> class.
        /// </summary>
        public EdiReaderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiReaderException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public EdiReaderException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiReaderException"/> class
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public EdiReaderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal EdiReaderException(string message, Exception innerException, string path, int lineNumber, int linePosition)
            : base(message, innerException)
        {
            Path = path;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        internal static EdiReaderException Create(EdiReader reader, string message)
        {
            return Create(reader, message, null);
        }

        internal static EdiReaderException Create(EdiReader reader, string message, Exception ex) {
            return Create(reader as IEdiLineInfo, reader.Path, message, ex);
        }

        internal static EdiReaderException Create(IEdiLineInfo lineInfo, string path, string message, Exception ex)
        {
            message = EdiPosition.FormatMessage(lineInfo, path, message);

            int lineNumber;
            int linePosition;
            if (lineInfo != null && lineInfo.HasLineInfo()) {
                lineNumber = lineInfo.LineNumber;
                linePosition = lineInfo.LinePosition;
            } else {
                lineNumber = 0;
                linePosition = 0;
            }

            return new EdiReaderException(message, ex, path, lineNumber, linePosition);
        }
    }
}