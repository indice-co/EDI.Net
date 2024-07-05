namespace indice.Edi;

/// <summary>
/// The exception thrown when an error occurs while reading EDI text.
/// </summary>
[Serializable]
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

    internal EdiWriterException(string message, Exception innerException, string path)
        : base(message, innerException)
    {
        Path = path;
    }

    internal static EdiWriterException Create(EdiWriter writer, string message, Exception ex) => Create(writer.ContainerPath, message, ex);

    internal static EdiWriterException Create(string path, string message, Exception ex)
    {
        message = EdiPosition.FormatMessage(null, path, message);

        return new EdiWriterException(message, ex, path);
    }
}