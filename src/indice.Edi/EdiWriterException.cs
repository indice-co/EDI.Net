#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace indice.Edi
{
    /// <summary>
    /// The exception thrown when an error occurs while reading EDI text.
    /// </summary>
#if !(DOTNET || PORTABLE)
    [Serializable]
#endif
    public class EdiWriterException : EdiException
    {
        /// <summary>
        /// Gets the path to the EDI where the error occurred.
        /// </summary>
        /// <value>The path to the EDI where the error occurred.</value>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterException"/> class.
        /// </summary>
        public EdiWriterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public EdiWriterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterException"/> class
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public EdiWriterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !(DOTNET || PORTABLE)
        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        public EdiWriterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        internal EdiWriterException(string message, Exception innerException, string path)
            : base(message, innerException)
        {
            Path = path;
        }

        internal static EdiWriterException Create(EdiWriter writer, string message, Exception ex)
        {
            return Create(writer.ContainerPath, message, ex);
        }

        internal static EdiWriterException Create(string path, string message, Exception ex)
        {
            message = EdiPosition.FormatMessage(null, path, message);

            return new EdiWriterException(message, ex, path);
        }
    }
}