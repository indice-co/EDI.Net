using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indice.Edi.Utilities;
using System.Globalization;

namespace indice.Edi.Serialization
{

    internal static class ReadQueueExtensions
    {
        public static bool ContainsPath(this Queue<EdiEntry> queue, string path) {
            if (string.IsNullOrWhiteSpace(path) || queue.Count == 0)
                return false;
            return queue.Any(entry => entry.Token.IsPrimitiveToken() && EdiPath.Parse(path).Equals(entry.Path));
        }

        public static string ReadAsString(this Queue<EdiEntry> queue, string path) {
            if (!ContainsPath(queue, path))
                return null;
            while (queue.Count > 0) {
                var entry = queue.Dequeue();
                if (entry.Token.IsPrimitiveToken() && EdiPath.Parse(path).Equals(entry.Path)) {
                    return entry.Value;
                }
            }
            return null;
        }

        public static int? ReadAsInt32(this Queue<EdiEntry> queue, string path, CultureInfo culture = null) {
            var text = ReadAsString(queue, path);
            if (text != null) {
                text = text.TrimStart('Z'); // Z suppresses leading zeros
            }
            if (string.IsNullOrEmpty(text))
                return null;

            var integer = default(int);
            if (!int.TryParse(text, NumberStyles.Integer, culture ?? CultureInfo.InvariantCulture, out integer)) {
                throw new EdiException("Cannot parse int from string '{0}'. Path {1}".FormatWith(culture, text, path));
            }
            return integer;
        }

        public static decimal? ReadAsDecimal(this Queue<EdiEntry> queue, string path, Picture? picture, char? decimalMark) {
            var text = ReadAsString(queue, path);
            if (string.IsNullOrEmpty(text))
                return null;
            
            return text.Parse(picture, decimalMark);
        }
    }

    internal struct EdiEntry
    {
        public EdiEntry(string path, EdiToken token, string value) {
            Path = path;
            Value = value;
            Token = token;
        }

        public string Path { get; }

        public string Value { get; }

        public EdiToken Token { get; }

        public bool HasValue { get { return Value != null; } }

        public override string ToString() {
            return $"{Path ?? "-"} {Value}";
        }
    }
}
