using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace indice.Edi
{
    internal struct EdiPosition
    {

        internal EdiContainerType Type;
        internal int Position;
        internal string SegmentName;
        internal bool HasIndex;

        public EdiPosition(EdiContainerType type) {
            Type = type;
            HasIndex = TypeHasIndex(type);
            Position = -1;
            SegmentName = null;
        }

        internal void WriteTo(StringBuilder sb) {
            switch (Type) {
                case EdiContainerType.Segment:
                    if (sb.Length > 0)
                        sb.Append('.');

                    string segmentName = SegmentName;
                    sb.Append(segmentName);
                    break;
                case EdiContainerType.Element:
                case EdiContainerType.Component:
                    sb.Append('[');
                    sb.Append(Position);
                    sb.Append(']');
                    break;
            }
        }

        internal static bool TypeHasIndex(EdiContainerType type) {
            return (type == EdiContainerType.Segment || type == EdiContainerType.Element || type == EdiContainerType.Component);
        }

        internal static string BuildPath(IEnumerable<EdiPosition> positions) {
            StringBuilder sb = new StringBuilder();

            foreach (EdiPosition state in positions) {
                state.WriteTo(sb);
            }

            return sb.ToString();
        }

        internal static string FormatMessage(IEdiLineInfo lineInfo, string path, string message) {
            // don't add a fullstop and space when message ends with a new line
            if (!message.EndsWith(Environment.NewLine, StringComparison.Ordinal)) {
                message = message.Trim();

                if (!message.EndsWith('.'))
                    message += ".";

                message += " ";
            }

            message += "Path '{0}'".FormatWith(CultureInfo.InvariantCulture, path);

            if (lineInfo != null && lineInfo.HasLineInfo())
                message += ", line {0}, position {1}".FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);

            message += ".";

            return message;
        }
    }
}
