using System;

namespace indice.Edi
{
    /// <summary>
    /// The exception thrown when an error occurs during JSON serialization or deserialization.
    /// </summary>
    public class EdiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdiException"/> class.
        /// </summary>
        public EdiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public EdiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiException"/> class
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public EdiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        internal static EdiException Create(IEdiLineInfo lineInfo, string path, string message) {
            message = EdiPosition.FormatMessage(lineInfo, path, message);

            return new EdiException(message);
        }
    }
}