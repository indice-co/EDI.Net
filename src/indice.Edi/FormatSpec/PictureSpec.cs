using System;
using System.Text.RegularExpressions;

namespace indice.Edi.FormatSpec
{
    /// <summary>
    /// Indicates the number of numeric (9) digits or alphanumeric (X) characters allowed in the data field.  
    /// If the field is numeric, this excludes any minus sign or the decimal point.  
    /// The decimal point is implied and its position within the data field is indicate by V.
    /// </summary>
    public struct PictureSpec : IFormatSpec
    {
        private const string PARSE_PATTERN = @"([9X]{1})\s?\((\d+?)\)\s?(V9\((\d+?)\))?";
        private readonly byte _Precision;
        private readonly byte _Scale;
        private readonly FormatKind _Kind;

        /// <summary>
        /// This is the total size of the string in digits
        /// </summary>
        public int Scale {
            get { return _Scale; }
        }

        /// <summary>
        /// In case of floating point number this holds the number of decimal places. Its length.
        /// </summary>
        public int Precision {
            get { return _Precision; }
        }

        /// <summary>
        /// indicates the <see cref="Kind" /> of the value represented. (ie <see cref="FormatKind.Alphanumeric"/>)
        /// </summary>
        public FormatKind Kind {
            get { return _Kind; }
        }

        /// <summary>
        /// This indicated if the value is a floating point number with by checking whether <see cref="Precision"/> is positive.
        /// </summary>
        public bool HasPrecision {
            get {
                return _Precision > 0;
            }
        }

        public bool IsValid {
            get {
                return _Scale > 0;
            }
        }

        public PictureSpec(string spec)
        {
            var match = Regex.Match(spec, PARSE_PATTERN);

            if (match != null) {
                var kind = match.Groups[1].Value == "X" ? FormatKind.Alphanumeric : FormatKind.Numeric;
                var length = byte.Parse(match.Groups[2].Value);
                byte decimalLength = 0;
                if (kind == FormatKind.Numeric && !string.IsNullOrWhiteSpace(match.Groups[3].Value)) {
                    decimalLength = byte.Parse(match.Groups[4].Value);
                }
                _Scale = (byte)(length + decimalLength);
                _Precision = decimalLength;
                _Kind = kind;

            } else {
                throw new ArgumentException($"Specification string '{spec}' could not be parsed to PictureSpec class", nameof(spec));
            }
        }

        public PictureSpec(byte length) {
            _Scale = length;
            _Precision = 0;
            _Kind = FormatKind.Alphanumeric;
        }

        public PictureSpec(byte length, FormatKind kind) {
            _Scale = length;
            _Precision = 0;
            _Kind = kind;
        }

        public PictureSpec(byte integerLength, byte decimalLength) {
            _Scale = (byte)(integerLength + decimalLength);
            _Precision = decimalLength;
            _Kind = FormatKind.Numeric;
        }

        public PictureSpec(byte integerLength, byte decimalLength, FormatKind kind) {
            _Scale = (byte)(integerLength + decimalLength);
            _Precision = decimalLength;
            _Kind = kind;
        }

        public override int GetHashCode() {
            return _Kind.GetHashCode() ^ _Scale.GetHashCode() ^ _Precision.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj != null && obj is PictureSpec) {
                var other = ((PictureSpec)obj);
                return other.Precision == Precision && other.Scale == Scale;
            }
            return base.Equals(obj);
        }

        public override string ToString() {
            switch (Kind)
            {
                case FormatKind.Alphanumeric:
                    return string.Format("X({0})", Scale);
                case FormatKind.Numeric:
                    return HasPrecision ? string.Format("9({0}) V9({1})", Scale - Precision, Precision) : string.Format("9({0})", Scale);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Kind), $"Unsupported format kind: {Kind}");
            }
        }

        public static PictureSpec Parse(string format)
        {
            return new PictureSpec(format);
        }

        public static implicit operator String(PictureSpec value) {
            return value.ToString();
        }

        public static explicit operator PictureSpec(string value) {
            return PictureSpec.Parse(value);
        }

        public bool VariableLength { get { return false; } }
    }
}