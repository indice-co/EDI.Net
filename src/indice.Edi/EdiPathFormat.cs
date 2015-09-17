using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi
{
    public class EdiPathFormat : IFormatProvider, ICustomFormatter
    {
        private const string ALL_ARRAY_FORMAT = "{0}[{1}][{2}]";
        private const string ALL_URI_FORMAT = "{0}/{1}/{2}";
        private const string SEGMENT_FORMAT = "{0}";
        private const string ELEMENT_URI_FORMAT = "{0}/{1}";
        private const string ELEMENT_ARRAY_FORMAT = "{0}[{1}]";
        private static readonly string[] availableFormatStrings = new [] { "S", "E", "C", "s", "e", "c" };

        public object GetFormat(Type formatType) {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider) {
            // Provide default formatting if arg is not an Int64. 
            if (arg.GetType() != typeof(EdiPath))
                try {
                    return HandleOtherFormats(fmt, arg);
                } catch (FormatException e) {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", fmt), e);
                }

            // Provide default formatting for unsupported format strings. 
            if (!(availableFormatStrings.Any(s => s == fmt) || string.IsNullOrWhiteSpace(fmt)))
                try {
                    return HandleOtherFormats(fmt, arg);
                } catch (FormatException e) {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", fmt), e);
                }

            var culture = formatProvider as CultureInfo ?? CultureInfo.CurrentCulture;
            var path = (EdiPath)arg;

            var mask = "";
            switch (fmt) {
                case "S":
                case "s":
                    mask = SEGMENT_FORMAT;
                    break;
                case "E":
                    mask = ELEMENT_ARRAY_FORMAT;
                    break;
                case "e":
                    mask = ELEMENT_URI_FORMAT;
                    break;
                case "c":
                    mask = ALL_URI_FORMAT;
                    break;
                case "C":
                default:
                    mask = ALL_ARRAY_FORMAT;
                    break;
            }
            return string.Format(mask, path.Segment, path.ElementIndex, path.ComponentIndex);
        }
        
        private string HandleOtherFormats(string format, object arg) {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return string.Empty;
        }
    }

}
