﻿#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System.Text;
using System.Globalization;

namespace indice.Edi.Utilities;

static class StringUtils
{
    public const string CarriageReturnLineFeed = "\r\n";
    public const string Empty = "";
    public const char CarriageReturn = '\r';
    public const char LineFeed = '\n';
    public const char Tab = '\t';

    public static string FormatWith(this string format, IFormatProvider provider, object arg0) => format.FormatWith(provider, [arg0]);

    public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1) => format.FormatWith(provider, [arg0, arg1]);

    public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2) => format.FormatWith(provider, [arg0, arg1, arg2]);

    public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2, object arg3) => format.FormatWith(provider, [arg0, arg1, arg2, arg3]);

    private static string FormatWith(this string format, IFormatProvider provider, params object[] args) {
        // leave this a private to force code to use an explicit overload
        // avoids stack memory being reserved for the object array
        if (string.IsNullOrEmpty(format)) {
            throw new ArgumentNullException(nameof(format));
        }
        return string.Format(provider, format, args);
    }

    /// <summary>
    /// Determines whether the string is all white space. Empty string will return false.
    /// </summary>
    /// <param name="s">The string to test whether it is all white space.</param>
    /// <returns>
    /// 	<c>true</c> if the string is all white space; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsWhiteSpace(string s) {
        if (s == null)
            throw new ArgumentNullException(nameof(s));

        if (s.Length == 0)
            return false;

        for (int i = 0; i < s.Length; i++) {
            if (!char.IsWhiteSpace(s[i]))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Nulls an empty string.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns>Null if the string was null, otherwise the string unchanged.</returns>
    public static string NullEmptyString(string s) => (string.IsNullOrEmpty(s)) ? null : s;

    public static StringWriter CreateStringWriter(int capacity) {
        StringBuilder sb = new StringBuilder(capacity);
        StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);

        return sw;
    }

    public static int? GetLength(string value) {
        if (value == null)
            return null;
        else
            return value.Length;
    }

    public static void ToCharAsUnicode(char c, char[] buffer) {
        buffer[0] = '\\';
        buffer[1] = 'u';
        buffer[2] = MathUtils.IntToHex((c >> 12) & '\x000f');
        buffer[3] = MathUtils.IntToHex((c >> 8) & '\x000f');
        buffer[4] = MathUtils.IntToHex((c >> 4) & '\x000f');
        buffer[5] = MathUtils.IntToHex(c & '\x000f');
    }

    public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue) {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (valueSelector == null)
            throw new ArgumentNullException(nameof(valueSelector));

        var caseInsensitiveResults = source.Where(s => string.Equals(valueSelector(s), testValue, StringComparison.OrdinalIgnoreCase));
        if (caseInsensitiveResults.Count() <= 1) {
            return caseInsensitiveResults.SingleOrDefault();
        } else {
            // multiple results returned. now filter using case sensitivity
            var caseSensitiveResults = source.Where(s => string.Equals(valueSelector(s), testValue, StringComparison.Ordinal));
            return caseSensitiveResults.SingleOrDefault();
        }
    }

    public static string ToCamelCase(string s) {
        if (string.IsNullOrEmpty(s))
            return s;

        if (!char.IsUpper(s[0]))
            return s;

        char[] chars = s.ToCharArray();

        for (int i = 0; i < chars.Length; i++) {
            bool hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                break;
            chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
        }

        return new string(chars);
    }

    public static bool IsHighSurrogate(char c) => char.IsHighSurrogate(c);

    public static bool IsLowSurrogate(char c) => char.IsLowSurrogate(c);

    public static bool StartsWith(this string source, char value) => (source.Length > 0 && source[0] == value);

    public static bool EndsWith(this string source, char value) => (source.Length > 0 && source[source.Length - 1] == value);
}
