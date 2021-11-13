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
        internal int FunctionalGroupCount;
        internal int MessageCount;
        internal int SegmentCount;
        internal int SegmentCountCache;
        internal string SegmentName;
        internal bool HasIndex;

        public EdiPosition(EdiContainerType type, EdiPosition? parent = null) {
            Type = type;
            HasIndex = TypeHasIndex(type);
            Position = -1;
            FunctionalGroupCount = parent?.FunctionalGroupCount ?? 0;
            MessageCount = parent?.MessageCount ?? 0;
            SegmentCount = parent?.SegmentCount ?? 0;
            SegmentName = parent?.SegmentName;
            SegmentCountCache = parent?.SegmentCount ?? 0;
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
            var sb = new StringBuilder();

            foreach (var state in positions) {
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

        public override string ToString() {
            switch (Type) {
                case EdiContainerType.Segment: return $"{SegmentName}" + (Position > -1 ? $"[{Position}]" : string.Empty);
                case EdiContainerType.Element:
                case EdiContainerType.Component:
                default: return $"{Type}" + (Position > -1 ? $"[{Position}]" : string.Empty);
            }
        }

        internal void AdvanceContrlCount(IEdiGrammar grammar) {
            if (SegmentName == grammar.FunctionalGroupTrailerTag) {
                SegmentCount +=2; // take into account one more (the header)
            }
            else if (SegmentName == grammar.InterchangeTrailerTag) {
                SegmentCount += SegmentCountCache;
            } else if (SegmentName == grammar.FunctionalGroupHeaderTag) {
                FunctionalGroupCount++;
                MessageCount = 0;
                SegmentCountCache += SegmentCount;
                SegmentCount = 1;
            } else if (SegmentName == grammar.MessageHeaderTag) {
                MessageCount++; 
                SegmentCountCache += SegmentCount;
                SegmentCount = 1;
            } else {
                SegmentCount++;
            }
        }
    }
}
