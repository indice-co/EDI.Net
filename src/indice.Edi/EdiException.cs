using System;
using System.Runtime.Serialization;

namespace indice.Edi
{
    /// <summary>
    /// The exception thrown when an error occurs during EDI serialization or deserialization.
    /// </summary>
#if !(PORTABLE || NETSTANDARD10 || NETSTANDARD13)
    [Serializable]
#endif
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


#if !(PORTABLE || NETSTANDARD10 || NETSTANDARD13)
        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        public EdiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        internal static EdiException Create(IEdiLineInfo lineInfo, string path, string message) {
            message = EdiPosition.FormatMessage(lineInfo, path, message);

            return new EdiException(message);
        }
    }
}