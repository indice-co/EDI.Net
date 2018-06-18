using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace indice.Edi
{
    /// <summary>
    /// Path representing the Position inside an Edi <see cref="EdiContainerType.Segment"/> 
    /// </summary>
    public struct EdiPath : IComparable<EdiPath>, IEquatable<EdiPath>
    {

        private const string PARSE_PATTERN = @"^([A-Z]{1}[A-Z0-9]{1,3})?([\[/]{1}(\d+?)\]?)?([\[/]{1}(\d+?)\]?)?$"; // supports both "STX/2/1 and STX[2][1]"
        private readonly string _Segment;
        private readonly int _ElementIndex;
        private readonly int _ComponentIndex;

        /// <summary>
        /// The name of the <see cref="EdiContainerType.Segment"/>
        /// </summary>
        public string Segment {
            get { return _Segment; }
        }

        /// <summary>
        /// Zero based index of the <see cref="EdiContainerType.Element"/> location inside the <seealso cref="EdiContainerType.Segment"/>
        /// </summary>
        public int ElementIndex {
            get { return _ElementIndex; }
        }

        /// <summary>
        /// Zero based index of the <see cref="EdiContainerType.Component"/> location inside an <seealso cref="EdiContainerType.Element"/>
        /// </summary>
        public int ComponentIndex {
            get { return _ComponentIndex; }
        }

        /// <summary>
        /// constructs an <see cref="EdiPath"/> given the <paramref name="segment"/> name. Componet and Element idexes default to zero.
        /// </summary>
        /// <param name="segment">The <see cref="EdiContainerType.Segment"/> name</param>
        public EdiPath(string segment) : this (segment, 0, 0) {
            
        }

        /// <summary>
        /// Constructs an <see cref="EdiPath"/> and the <paramref name="elementIndex"/>. The index of the component defaults to zero.
        /// </summary>
        /// <param name="segment">The <see cref="EdiContainerType.Segment"/> name</param>
        /// <param name="elementIndex">Zero based index of the <see cref="EdiContainerType.Element"/> location inside the <seealso cref="EdiContainerType.Segment"/></param>
        public EdiPath(string segment, int elementIndex) : this(segment, elementIndex, 0) {
            
        }

        /// <summary>
        /// Constructs an <see cref="EdiPath"/>.
        /// </summary>
        /// <param name="segment">The <see cref="EdiContainerType.Segment"/> name</param>
        /// <param name="elementIndex">Zero based index of the <see cref="EdiContainerType.Element"/> location inside the <seealso cref="EdiContainerType.Segment"/></param>
        /// <param name="componentIndex">Zero based index of the <see cref="EdiContainerType.Component"/> location inside an <seealso cref="EdiContainerType.Element"/></param>
        public EdiPath(string segment, int elementIndex, int componentIndex) {
            _Segment = segment ?? string.Empty;
            _ElementIndex = elementIndex;
            _ComponentIndex = componentIndex;
        }
        
        /// <summary>
        /// Returns the hash code for the <see cref="EdiPath"/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return _Segment.GetHashCode() ^ _ElementIndex.GetHashCode() ^ _ComponentIndex.GetHashCode();
        }

        /// <summary>
        /// Checks two <see cref="EdiPath"/> for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EdiPath other) {
            return other.Segment == Segment && other.ElementIndex == ElementIndex && other.ComponentIndex == ComponentIndex;
        }

        /// <summary>
        /// Checks whether this instance and the specified object are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj != null && (obj is EdiPath || obj is string)) {
                var other = default(EdiPath);
                if (obj is EdiPath)
                    other = (EdiPath)obj;
                else
                    other = (EdiPath)(string)obj;
                return Equals(other);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns the string representation for the <see cref="EdiPath"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return ToString("C", null); 
        }

        /// <summary>
        /// Returns the string representation for the <see cref="EdiPath"/> using a format string. 
        /// Possible values are (s, e) where "s" stands for segment only, "e" stands for element only.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format) {
            return ToString(format, null);
        }

        /// <summary>
        /// Returns the string representation for the <see cref="EdiPath"/> using a <paramref name="formatProvider"/>.
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider formatProvider) {
            return ToString("C", formatProvider);
        }

        /// <summary>
        /// Returns the string representation for the <see cref="EdiPath"/> using a <paramref name="formatProvider"/> and a format string. 
        /// Possible values are (s, e) where "s" stands for segment only, "e" stands for element only.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider) {
            var formatter = new EdiPathFormat();
            return formatter.Format(format, this, formatProvider);
        }

        /// <summary>
        /// Parses the given <paramref name="text"/> into an <see cref="EdiPath"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static EdiPath Parse(string text) {
            var match = Regex.Match(text, PARSE_PATTERN);
            if (match != null) {
                var segment = match.Groups[1].Value;
                int element;
                int component;
                int.TryParse(match.Groups[3].Value, out element);
                int.TryParse(match.Groups[5].Value, out component);
                return new EdiPath(segment, element, component);
            } else {
                return new EdiPath();
            }
        }

        /// <summary>
        /// Compares this instance to the <paramref name="other"/> <see cref="EdiPath"/>.  
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(EdiPath other) {
            var result = string.Compare(Segment, other.Segment, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = ElementIndex.CompareTo(other.ElementIndex);
            if (result == 0) result = ComponentIndex.CompareTo(other.ComponentIndex);
            return result;
        }

        /// <summary>
        /// Implicit cast operator from <see cref="EdiPath"/> to <seealso cref="string"/>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string (EdiPath value) {
            return value.ToString();
        }

        /// <summary>
        /// Explicit cast operator from <see cref="string"/> to <seealso cref="EdiPath"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator EdiPath(string value) {
            return EdiPath.Parse(value);
        }
    }
}
