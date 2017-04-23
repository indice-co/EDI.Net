namespace indice.Edi.FormatSpec
{
    public interface IFormatSpec
    {
        /// <summary>
        /// This is the total size of the string in digits
        /// </summary>
        int Scale { get; }

        /// <summary>
        /// Length of field can is variable
        /// </summary>
        bool VariableLength { get; }

        /// <summary>
        /// indicates the <see cref="Kind" /> of the value represented. (ie <see cref="FormatKind.Alphanumeric"/>)
        /// </summary>
        FormatKind Kind { get; }

        /// <summary>
        /// In case of floating point number this holds the number of decimal places. Its length.
        /// </summary>
        int Precision { get; }
    }
}