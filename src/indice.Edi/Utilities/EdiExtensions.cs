using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Utilities
{
    public static class EdiExtensions
    {
        public static bool IsStartToken(this EdiToken token) {
            switch (token) {
                case EdiToken.SegmentStart:
                case EdiToken.ElementStart:
                case EdiToken.ComponentStart:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPrimitiveToken(this EdiToken token) {
            switch (token) {
                case EdiToken.Integer:
                case EdiToken.Float:
                case EdiToken.String:
                case EdiToken.Boolean:
                case EdiToken.Null:
                case EdiToken.Date:
                    return true;
                default:
                    return false;
            }
        }


        public static IEnumerable<EdiAttribute> OfType(this IEnumerable<EdiAttribute> attributes, EdiStructureType container) {
            var typesToSearch = new Type[0];
            switch (container) {
                case EdiStructureType.None:
                case EdiStructureType.Interchange:
                    break;
                case EdiStructureType.Group:
                    typesToSearch = new [] { typeof(EdiGroupAttribute) };
                    break;
                case EdiStructureType.Message:
                    typesToSearch = new [] { typeof(EdiMessageAttribute) };
                    break;
                case EdiStructureType.SegmentGroup:
                    typesToSearch = new [] { typeof(EdiSegmentGroupAttribute) };
                    break;
                case EdiStructureType.Segment:
                    typesToSearch = new [] { typeof(EdiSegmentAttribute), typeof(EdiSegmentGroupAttribute) };
                    break;
                case EdiStructureType.Element:
                    typesToSearch = new [] { typeof(EdiElementAttribute) };
                    break;
                default:
                    break;
            }

            return null == typesToSearch ? Enumerable.Empty<EdiAttribute>() : attributes.Where(a => typesToSearch.Contains(a.GetType()));
        }
        
        public static bool TryParse(this string value, Picture? picture, char? decimalMark, out decimal number) {
            number = 0.0M;
            try {
                var result = Parse(value, picture, decimalMark);
                if (result.HasValue)
                    number = result.Value;
                return true;
            } catch {
                return false;
            }
        }

        public static decimal? Parse(this string value, Picture? picture, char? decimalMark) {
            if (string.IsNullOrEmpty(value))
                return null;
            decimal d;
            var provider = NumberFormatInfo.InvariantInfo;
            if (decimalMark.HasValue) {
                if (provider.NumberDecimalSeparator != decimalMark.ToString()) {
                    provider = provider.Clone() as NumberFormatInfo;
                    provider.NumberDecimalSeparator = decimalMark.Value.ToString();
                }
                if (decimal.TryParse(value, NumberStyles.Number, provider, out d)) {
                    return d;
                }
            }
            else if (picture.HasValue && picture.Value.Kind == PictureKind.Numeric && decimal.TryParse(value, NumberStyles.Integer, provider, out d)) {
                d = d * (decimal)Math.Pow(0.1, picture.Value.Precision);
                return d;
            }
            throw new EdiException("Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, value));
        }
    }
}
