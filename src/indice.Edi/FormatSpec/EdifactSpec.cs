using System;
using System.Text.RegularExpressions;

namespace indice.Edi.FormatSpec
{
    /// <summary>
    /// String representation of the format of the element
    /// 
    /// a       alphabetic characters
    /// n       numeric characters
    /// an      alpha-numeric characters
    /// a3      3 alphabetic characters, fixed length
    /// n3      3 numeric characters, fixed length
    /// an3     3 alpha-numeric characters, fixed length
    /// a..3    up to 3 alphabetic characters, variable length
    /// n..3    up to 3 numeric characters, variable length
    /// an..3   up to 3 alpha-numeric characters, variable length
    /// </summary>
    public struct EdifactSpec : IFormatSpec
    {
        private const string PARSE_PATTERN = @"([a,n]{1,2})(\.\.)?(\d*)";
        private readonly int? _scale;
        private readonly FormatKind _kind;
        private readonly bool _variableLength;
        private readonly int _precision;

        public EdifactSpec(FormatKind kind, bool variableLength, int? scale) {
            _kind = kind;
            _variableLength = variableLength;
            _scale = scale;
            _precision = 10;
        }

        /// <summary>
        /// This is the total size of the string in digits
        /// </summary>
        public int Scale {
            get { return _scale ?? int.MaxValue; }
        }

        /// <summary>
        /// Length of field can is variable
        /// </summary>
        public bool VariableLength {
            get { return _variableLength; }
        }

        /// <summary>
        /// indicates the <see cref="Kind" /> of the value represented. (ie <see cref="EdifactSpecKind.Alphanumeric"/>)
        /// </summary>
        public FormatKind Kind {
            get { return _kind; }
        }

        public int Precision {
            get { return _precision; }
        }

        public bool IsValid {
            get {
                return _scale > 0;
            }
        }

        public override int GetHashCode() {
            return _kind.GetHashCode() ^ _scale.GetHashCode() ^ _variableLength.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj != null && obj is EdifactSpec) {
                var other = ((EdifactSpec)obj);
                return other._variableLength == _variableLength && other.Scale == Scale;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            string kindString;
            switch (Kind)
            {
                case FormatKind.Alphanumeric:
                    kindString = "an";
                    break;
                case FormatKind.Alphabetic:
                    kindString = "a";
                    break;
                case FormatKind.Numeric:
                    kindString = "n";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return $"{kindString}{(_variableLength ? ".." : "")}{(_scale.HasValue ? _scale.ToString() : "")}";
        }

        public static EdifactSpec Parse(string edifactSpecString) {
            var match = Regex.Match(edifactSpecString, PARSE_PATTERN);

            if (match == null || !match.Success || match.Groups.Count != 4) {
                throw new ArgumentException($"'{edifactSpecString}' is not a valid Mig Spec string", nameof(edifactSpecString));
            }

            FormatKind kind;
            switch (match.Groups[1].Value)
            {
                case "an":
                    kind = FormatKind.Alphanumeric;
                    break;
                case "a":
                    kind = FormatKind.Alphabetic;
                    break;
                case "n":
                    kind = FormatKind.Numeric;
                    break;
                default:
                    kind = FormatKind.Unknown;
                    break;
            }

            var variableLength = match.Groups[2].Value == "..";
            int? scale = null;
            if (int.TryParse(match.Groups[3].Value, out int scaleTemp)) {
                scale = scaleTemp;
            }
            return new EdifactSpec(kind, variableLength, scale);
        }

        public static implicit operator String(EdifactSpec value) {
            return value.ToString();
        }

        public static explicit operator EdifactSpec(string value) {
            return EdifactSpec.Parse(value);
        }
    }
}
