using System.Globalization;
using System.Text;

namespace indice.Edi;

/// <summary>
/// An <see cref="IFormatProvider"/> for the <seealso cref="EdiPath"/> struct. Available format masks are 
///  "S", "E", "C", "s", "e", "c", "o". They mean "segment" "element" "component" and "original". The upercase counterparts will print the path in array format.
///  Original stands for the actual input path configured.
/// </summary>
public class EdiPathFormat : IFormatProvider, ICustomFormatter
{
    private const string ALL_ARRAY_FORMAT = "{0}[{1}][{2}]";
    private const string ALL_URI_FORMAT = "{0}/{1}/{2}";
    private const string SEGMENT_FORMAT = "{0}";
    private const string ELEMENT_URI_FORMAT = "{0}/{1}";
    private const string ELEMENT_ARRAY_FORMAT = "{0}[{1}]";
    private static readonly string[] availableFormatStrings = ["S", "E", "C", "s", "e", "c", "o"];

    /// <summary>
    /// Gets the format provider for the given <paramref name="formatType"/>.
    /// </summary>
    /// <param name="formatType"></param>
    /// <returns></returns>
    public object GetFormat(Type formatType) {
        if (formatType == typeof(ICustomFormatter))
            return this;
        else
            return null;
    }

    /// <summary>
    /// Formats the object <paramref name="arg"/> using the <paramref name="formatProvider"/>.
    /// </summary>
    /// <param name="fmt">Format string</param>
    /// <param name="arg">the object to format</param>
    /// <param name="formatProvider">The provider</param>
    /// <returns></returns>
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
            case "o":
                var pathText = new StringBuilder(path.Segment);
                if (path.Element.HasValue) {
                    pathText.Append($"/{path.Element}");
                    if (path.Component.HasValue) {
                        pathText.Append($"/{path.Component}");
                    }
                }
                return pathText.ToString();
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
        return string.Format(mask, path.Segment, path.Element.HasValue ? path.Element.Value : "0", path.Component.HasValue ? path.Component.Value : "0");
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
