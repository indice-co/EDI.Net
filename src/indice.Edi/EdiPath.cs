using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace indice.Edi
{
    public struct EdiPath {
        private const string PARSE_PATTERN = @"^([A-Z]{3})([\[/]{1}(\d+?)\]?)?([\[/]{1}(\d+?)\]?)?$"; // supports both "STX/2/1 and STX[2][1]"
        private readonly string _Segment;
        private readonly int _ElementIndex;
        private readonly int _ComponentIndex;

        public string Segment {
            get { return _Segment; }
        }

        public int ElementIndex {
            get { return _ElementIndex; }
        }

        public int ComponentIndex {
            get { return _ComponentIndex; }
        }
        
        public EdiPath(string segment) : this (segment, 0, 0) {
            
        }

        public EdiPath(string segment, int elementIndex) : this(segment, elementIndex, 0) {
            
        }
        public EdiPath(string segment, int elementIndex, int componentIndex) {
            _Segment = segment ?? string.Empty;
            _ElementIndex = elementIndex;
            _ComponentIndex = componentIndex;
        }
        
        public override int GetHashCode() {
            return _Segment.GetHashCode() ^ _ElementIndex.GetHashCode() ^ _ComponentIndex.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj != null && obj is EdiPath) {
                var other = ((EdiPath)obj);
                return other.Segment == Segment && other.ElementIndex == ElementIndex && other.ComponentIndex == ComponentIndex;
            }
            return base.Equals(obj);
        }

        public override string ToString() {
            return ToString("C", null); 
        }

        public string ToString(string format) {
            return ToString(format, null);
        }
        public string ToString(IFormatProvider formatProvider) {
            return ToString("C", formatProvider);
        }
        public string ToString(string format, IFormatProvider formatProvider) {
            var formatter = new EdiPathFormat();
            return formatter.Format(format, this, formatProvider);
        }

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

        public static implicit operator String(EdiPath value) {
            return value.ToString();
        }

        public static explicit operator EdiPath(string value) {
            return EdiPath.Parse(value);
        }
    }
}
