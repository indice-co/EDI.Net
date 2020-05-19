using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    /// <summary>
    /// Invoice message INVOIC D96B
    /// </summary>
    public class EDIFact_D96B_INVOIC
    {

        [EdiSegment, EdiPath("DTM")]
        public class DTM
        {
            [EdiPath("*/0")]
            public DateTimeElement DateTimePeriod { get; set; }

            public override string ToString() {
                return DateTimePeriod?.ToString() ?? base.ToString();
            }
        }

        [EdiSegment, EdiPath("BGM")]
        public class BeginingOfMessage
        {
            [EdiPath("*/0")]
            public NameElement Name { get; set; }
            [EdiPath("*/1")]
            public IdentificationElement Identification { get; set; }

            public override string ToString() => $"{Name} {Identification}";

        }
        /// <summary>
        /// C002 DOCUMENT/MESSAGE NAME
        /// </summary>
        [EdiElement]
        public class NameElement
        {
            [EdiValue("X(3)", Path = "*/*/0")]
            public string NameCode { get; set; }

            [EdiValue("X(17)", Path = "*/*/1")]
            public string IdentificationCode { get; set; }

            [EdiValue("X(3)", Path = "*/*/2")]
            public string ResponsibleAgencyCode { get; set; }

            [EdiValue("X(35)", Path = "*/*/3")]
            public string Name { get; set; }

            public override string ToString() {
                return $"{NameCode} {Name}".Trim();
            }
        }

        /// <summary>
        /// C106 DOCUMENT/MESSAGE IDENTIFICATION	
        /// </summary>
        [EdiElement]
        public class IdentificationElement
        {

            [EdiValue("X(35)", Path = "*/*/0")]
            public string DocumentId { get; set; }
            [EdiValue("X(9)", Path = "*/*/1")]
            public string VersionId { get; set; }
            [EdiValue("X(6)", Path = "*/*/2")]
            public string RevisionId { get; set; }
            [EdiValue("X(3)", Path = "*/*/3")]
            public string MessageFucntionCode { get; set; }
            [EdiValue("X(3)", Path = "*/*/4")]
            public string ResponseTypeCode { get; set; }
            public override string ToString() {
                return $"{DocumentId}.{VersionId}.{RevisionId}";
            }
        }

        [EdiElement]
        public class DateTimeElement
        {
            [EdiValue("9(3)", Path = "*/*/0")]
            public int ID { get; set; }

            [EdiValue("X(35)", Path = "*/*/1")]
            public string Value { get; set; }


            [EdiValue("9(3)", Path = "*/*/2")]
            public int FormatQualifier { get; set; }

            public DateTime? AsDateTime() {
                if (string.IsNullOrWhiteSpace(Value)) return null;
                string[] formats = null;
                switch (FormatQualifier) {
                    case 203:
                        formats = new[] { "yyyyMMddHHmm" };
                        break;
                    case 204:
                        formats = new[] { "yyyyMMddHHmmss" };
                        break;
                    case 201:
                        formats = new[] { "yyMMddHHmm" };
                        break;
                    case 102:
                        formats = new[] { "yyyyMMdd" };
                        break;
                }
                if (DateTime.TryParseExact(Value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datetime))
                    return datetime;
                return null;
            }
            public override string ToString() {
                return AsDateTime().ToString() ?? base.ToString();
            }
        }
    }
}
