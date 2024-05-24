using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using indice.Edi.Serialization;
using Newtonsoft.Json;

namespace indice.Edi.Tests.Models
{
    [Serializable]
    [XmlRoot("APERAK")]
    public class EDIFACT_APPERAK_issue235
    {


        [JsonProperty("UNB")]
        [XmlElement(ElementName = "UNB")]
        public UNB? UNB_1 { get; set; }

        #region UNB // UNB SEGMENTS

        [EdiSegment, EdiPath("UNB")]
        public class UNB
        {
            [JsonProperty("S001")]
            [XmlElement(ElementName = "S001")]
            public S001? S001 { get; set; }

            [JsonProperty("S002")]
            [XmlElement(ElementName = "S002")]
            public UNB_S002? S002 { get; set; }

            [JsonProperty("S003")]
            [XmlElement(ElementName = "S003")]
            public UNB_S003? S003 { get; set; }

            [JsonProperty("S004")]
            [XmlElement(ElementName = "S004")]
            public S004? S004 { get; set; }

            [EdiValue("9(99)", Path = "*/4/0")]
            [JsonProperty("E0020")]
            [XmlElement(ElementName = "E0020")]
            public string? x_0020 { get; set; }

            [JsonProperty("S005")]
            [XmlElement(ElementName = "S005")]
            public S005? S005 { get; set; }

            [EdiValue("9(99)", Path = "*/6/0")]
            [JsonProperty("E0026")]
            [XmlElement(ElementName = "E0026")]
            public string? x_0026 { get; set; }

            [EdiValue("9(99)", Path = "*/7/0")]
            [JsonProperty("E0029")]
            [XmlElement(ElementName = "E0029")]
            public string? x_0029 { get; set; }

            [EdiValue("9(99)", Path = "*/8/0")]
            [JsonProperty("E0031")]
            [XmlElement(ElementName = "E0031")]
            public string? x_0031 { get; set; }

            [EdiValue("9(99)", Path = "*/9/0")]
            [JsonProperty("E0032")]
            [XmlElement(ElementName = "E0032")]
            public string? x_0032 { get; set; }

            [EdiValue("9(99)", Path = "*/10/0")]
            [JsonProperty("E0035")]
            [XmlElement(ElementName = "E0035")]
            public string? x_0035 { get; set; }
        }

        [EdiElement, EdiPath("*/0")]
        public class S001
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0001")]
            [XmlElement(ElementName = "E0001")]
            public string? x_0001 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0002")]
            [XmlElement(ElementName = "E0002")]
            public string? x_0002 { get; set; }
        }

        [EdiElement, EdiPath("*/1")]
        public class UNB_S002
        {
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0004")]
            [XmlElement(ElementName = "E0004")]
            public string? x_0004 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0007")]
            [XmlElement(ElementName = "E0007")]
            public string? x_0007 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0008")]
            [XmlElement(ElementName = "E0008")]
            public string? x_0008 { get; set; }
        }

        [EdiElement, EdiPath("*/2")]
        public class UNB_S003
        {
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0010")]
            [XmlElement(ElementName = "E0010")]
            public string? x_0010 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0007")]
            [XmlElement(ElementName = "E0007")]
            public string? x_0007 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0014")]
            [XmlElement(ElementName = "E0014")]
            public string? x_0014 { get; set; }
        }

        [EdiElement, EdiPath("*/3")]
        public class S004
        {
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0017")]
            [XmlElement(ElementName = "E0017")]
            public string? x_0017 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0019")]
            [XmlElement(ElementName = "E0019")]
            public string? x_0019 { get; set; }

        }

        [EdiElement, EdiPath("*/5")]
        public class S005
        {
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0022")]
            [XmlElement(ElementName = "E0022")]
            public string? x_0022 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0025")]
            [XmlElement(ElementName = "E0025")]
            public string? x_0025 { get; set; }
        }

        #endregion



        [JsonProperty("CONTENT")]
        [XmlElement(ElementName = "CONTENT")]
        public Message EDIFACT { get; set; }

        [JsonProperty("UNZ")]
        [XmlElement(ElementName = "UNZ")]
        [EdiSegment, EdiPath("UNZ")]
        public UNZ? UNZ_1 { get; set; }

        #region UNZ

        [EdiSegment, EdiPath("UNZ")]
        public class UNZ
        {

            [JsonProperty("E0036")]
            [XmlElement(ElementName = "E0036")]
            [EdiValue(Path = "*/0/0")]
            public string x_0036 { get; set; } //od unh do tego


            [JsonProperty("E0020")]
            [XmlElement(ElementName = "E0020")]
            [EdiValue("9(99)", Path = "*/1/0")]
            public string x_0020 { get; set; }
        }


        #endregion


        [EdiMessage]
        public class Message
        {
            [JsonProperty("UNH")]
            [XmlElement(ElementName = "UNH")]
            public UNH UNH { get; set; }

            [JsonProperty("BGM")]
            [XmlElement(ElementName = "BGM")]
            public BGM BGM { get; set; }

            [JsonProperty("DTM")]
            [XmlElement(ElementName = "DTM")]
            public List<DTM> DTM { get; set; }

            [JsonProperty("SG1")]
            [XmlElement(ElementName = "SG1")]
            public List<SG1> SG1 { get; set; }

            [JsonProperty("SG2")]
            [XmlElement(ElementName = "SG2")]
            public List<SG2> SG2 { get; set; }

            [JsonProperty("SG3")]
            [XmlElement(ElementName = "SG3")]
            public List<SG3> SG3 { get; set; }

            [JsonProperty("SG4")]
            [XmlElement(ElementName = "SG4")]
            public List<SG4> SG4 { get; set; }

            [JsonProperty("UNT")]
            [XmlElement(ElementName = "UNT")]
            public UNT UNT { get; set; }


        }


        #region UNH

        [EdiSegment, EdiPath("UNH")]
        public class UNH
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0062")]
            [XmlElement(ElementName = "E0062")]
            public string x_0062 { get; set; }

            [JsonProperty("S009")]
            [XmlElement(ElementName = "S009")]
            public S009? S009 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0068")]
            [XmlElement(ElementName = "E0068")]
            private string x_0068 { get; set; }

            [JsonProperty("S010")]
            [XmlElement(ElementName = "S010")]
            public S010 S010;

            [JsonProperty("S016")]
            [XmlElement(ElementName = "S016")]
            public S016 S016 { get; set; }

            [JsonProperty("S017")]
            [XmlElement(ElementName = "S017")]
            public S017 S017 { get; set; }

            [JsonProperty("S018")]
            [XmlElement(ElementName = "S018")]
            public S018 S018 { get; set; }

        }

        [EdiElement, EdiPath("*/1")]
        public class S009
        {
            //Mandatory = true,
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0065")]
            [XmlElement(ElementName = "E0065")]
            public string x_0065 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0052")]
            [XmlElement(ElementName = "E0052")]
            public string x_0052 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0054")]
            [XmlElement(ElementName = "E0054")]
            public string x_0054 { get; set; }

            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }

            [EdiValue("9(99)", Path = "*/*/4")]
            [JsonProperty("E0057")]
            [XmlElement(ElementName = "E0057")]
            public string x_0057 { get; set; }

            [EdiValue("9(99)", Path = "*/*/5")]
            [JsonProperty("E0110")]
            [XmlElement(ElementName = "E0110")]
            public string x_0110 { get; set; }

            [EdiValue("9(99)", Path = "*/*/6")]
            [JsonProperty("E0113")]
            [XmlElement(ElementName = "E0113")]
            public string x_0113 { get; set; }
        }

        [EdiElement, EdiPath("*/3")]
        public class S010
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0070")]
            [XmlElement(ElementName = "E0070")]
            public string? x_0070 { get; set; } = string.Empty;

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0073")]
            [XmlElement(ElementName = "E0073")]
            public string? x_0073 { get; set; } = string.Empty;
        }

        [EdiElement, EdiPath("*/4")]
        public class S016
        {
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0115")]
            [XmlElement(ElementName = "E0115")]
            public string x_0115 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0116")]
            [XmlElement(ElementName = "E0116")]
            public string x_0116 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0118")]
            [XmlElement(ElementName = "E0118")]
            public string x_0118 { get; set; }

            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }
        }

        [EdiElement, EdiPath("*/5")]
        public class S017
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0121")]
            [XmlElement(ElementName = "E0121")]
            public string x_0121 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0122")]
            [XmlElement(ElementName = "E0122")]
            public string x_0122 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0124")]
            [XmlElement(ElementName = "E0124")]
            public string x_0124 { get; set; }

            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }
        }

        [EdiElement, EdiPath("*/6")]
        public class S018
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0127")]
            [XmlElement(ElementName = "E0127")]
            public string x_0127 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0128")]
            [XmlElement(ElementName = "E0128")]
            public string x_0128 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0130")]
            [XmlElement(ElementName = "E0130")]
            public string x_0130 { get; set; }

            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }
        }

        #endregion

        #region BGM
        /// <summary>
        /// Beginning of message
        /// </summary>
        [EdiSegment, EdiPath("BGM")]
        public class BGM
        {
            [JsonProperty("C002")]
            [XmlElement(ElementName = "C002")]
            public C002 C002 { get; set; }

            [JsonProperty("C106")]
            [XmlElement(ElementName = "C106")]
            public C106 C106 { get; set; }

            [EdiValue("9(99)", Path = "*/2/0")]
            [JsonProperty("E1225")]
            [XmlElement(ElementName = "E1225")]
            public string x_1225 { get; set; }

            [EdiValue("9(99)", Path = "*/3/0")]
            [JsonProperty("E4343")]
            [XmlElement(ElementName = "E4343")]
            public string x_4343 { get; set; }
        }

        /// <summary>
        /// DOCUMENT/MESSAGE NAME
        /// </summary>
        [EdiElement, EdiPath("*/0")]
        public class C002
        {
            /// <summary>
            /// Document name code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E1001")]
            [XmlElement(ElementName = "E1001")]
            public string x_1001 { get; set; }
            /// <summary>
            /// Code list identification code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            public string x_1131 { get; set; }
            /// <summary>
            /// Code list responsible agency code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            public string x_3055 { get; set; }
            /// <summary>
            /// Document name
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E1000")]
            [XmlElement(ElementName = "E1000")]
            public string x_1000 { get; set; }
        }


        /// <summary>
        /// DOCUMENT/MESSAGE IDENTIFICATION
        /// </summary>
        [EdiElement, EdiPath("*/1")]
        public class C106
        {
            /// <summary>
            /// Document identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E1004")]
            [XmlElement(ElementName = "E1004")]
            public string x_1004 { get; set; }
            /// <summary>
            /// Version identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1056")]
            [XmlElement(ElementName = "E1056")]
            public string x_1056 { get; set; }
            /// <summary>
            /// Revision identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E1060")]
            [XmlElement(ElementName = "E1060")]
            public string x_1060 { get; set; }

        }

        #endregion

        #region DTM

        /// <summary>
        /// Date/time/period
        /// </summary>
        [EdiSegment, EdiPath("DTM")]
        public class DTM
        {
            /// <summary>
            /// Date/time/period
            /// </summary>
            [JsonProperty("C507")]
            [XmlElement(ElementName = "C507")]
            public C507 C507 { get; set; }
        }

        [EdiElement, EdiPath("*/0")]
        public class C507
        {
            /// <summary>
            /// Date or time or period function code qualifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E2005")]
            [XmlElement(ElementName = "E2005")]
            public string x_2005 { get; set; }
            /// <summary>
            /// Date or time or period value
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E2380")]
            [XmlElement(ElementName = "E2380")]
            public string x_2380 { get; set; }
            /// <summary>
            /// Date or time or period format code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E2379")]
            [XmlElement(ElementName = "E2379")]
            public string x_2379 { get; set; }
        }

        #endregion

        #region SG1

        [EdiSegmentGroup("DOC", "DTM")]
        public class SG1
        {
            [JsonProperty("DOC")]
            [XmlElement(ElementName = "DOC")]
            public SG1_DOC DOC { get; set; }

            [JsonProperty("DTM")]
            [XmlElement(ElementName = "DTM")]
            public List<SG1_DTM> DTM { get; set; }
        }

        #region SG1_DOC

        [EdiSegment, EdiPath("DOC")]
        public class SG1_DOC
        {

            [JsonProperty("C002")]
            [XmlElement(ElementName = "C002")]
            public SG1_C002 SG1_C002 { get; set; }

            [JsonProperty("C503")]
            [XmlElement(ElementName = "C503")]
            public SG1_C503 SG1_C503 { get; set; }

            [JsonProperty("E3153")]
            [XmlElement(ElementName = "E3153")]
            [EdiValue("9(99)", Path = "*/2/0")]
            public string x_3153 { get; set; }

            [JsonProperty("E1220")]
            [XmlElement(ElementName = "E1220")]
            [EdiValue("9(99)", Path = "*/3/0")]
            public string x_1220 { get; set; }

            [JsonProperty("E1218")]
            [XmlElement(ElementName = "E1218")]
            [EdiValue("9(99)", Path = "*/4/0")]
            public string x_1218 { get; set; }

        }


        [EdiElement, EdiPath("*/0")]
        public class SG1_C002
        {

            [JsonProperty("E1001")]
            [XmlElement(ElementName = "E1001")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_1001 { get; set; }


            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_1131 { get; set; }

            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_3055 { get; set; }

            [JsonProperty("E1000")]
            [XmlElement(ElementName = "E1000")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_1000 { get; set; }
        }

        [EdiElement, EdiPath("*/1")]
        public class SG1_C503
        {

            [JsonProperty("E1004")]
            [XmlElement(ElementName = "E1004")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_1004 { get; set; }

            [JsonProperty("E1373")]
            [XmlElement(ElementName = "E1375")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_1373 { get; set; }

            [JsonProperty("E1366")]
            [XmlElement(ElementName = "E1366")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_1366 { get; set; }

            [JsonProperty("E3453")]
            [XmlElement(ElementName = "E3453")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_3453 { get; set; }

            [JsonProperty("E1056")]
            [XmlElement(ElementName = "E1056")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_1056 { get; set; }

            [JsonProperty("E1060")]
            [XmlElement(ElementName = "E1060")]
            [EdiValue("9(99)", Path = "*/*/5")]
            public string x_1060 { get; set; }

        }

        #endregion

        #region SG1_DTM

        [EdiSegment, EdiPath("DTM")]
        public class SG1_DTM
        {
            [JsonProperty("C507")]
            [XmlElement(ElementName = "C507")]
            public SG1_C507 C507 { get; set; }
        }

        [EdiElement, EdiPath("*/0")]
        public class SG1_C507
        {
            /// <summary>
            /// Date or time or period function code qualifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E2005")]
            [XmlElement(ElementName = "E2005")]
            public string x_2005 { get; set; }
            /// <summary>
            /// Date or time or period value
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E2380")]
            [XmlElement(ElementName = "E2380")]
            public string x_2380 { get; set; }
            /// <summary>
            /// Date or time or period format code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E2379")]
            [XmlElement(ElementName = "E2379")]
            public string x_2379 { get; set; }
        }

        #endregion

        #endregion

        #region SG2

        [EdiSegmentGroup("RFF", "DTM")]
        public class SG2
        {
            [JsonProperty("RFF")]
            [XmlElement(ElementName = "RFF")]
            public SG2_RFF RFF { get; set; }

            [JsonProperty("DTM")]
            [XmlElement(ElementName = "DTM")]
            public List<SG2_DTM> DTM { get; set; }
        }

        #region SG2_RFF

        [EdiSegment, EdiPath("RFF")]
        public class SG2_RFF
        {
            [JsonProperty("C506")]
            [XmlElement(ElementName = "C506")]
            public SG2_C506 SG2_C506 { get; set; }
        }

        [EdiElement, EdiPath("*/0")]
        public class SG2_C506
        {

            [JsonProperty("E1153")]
            [XmlElement(ElementName = "E1153")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_1153 { get; set; }

            [JsonProperty("E1154")]
            [XmlElement(ElementName = "E1154")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_1154 { get; set; }

            [JsonProperty("E1156")]
            [XmlElement(ElementName = "E1156")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_1156 { get; set; }

            [JsonProperty("E4000")]
            [XmlElement(ElementName = "E4000")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_4000 { get; set; }

            [JsonProperty("E1060")]
            [XmlElement(ElementName = "E1060")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_1060 { get; set; }
        }

        #endregion

        #region SG2_DTM

        [EdiSegment, EdiPath("DTM")]
        public class SG2_DTM
        {
            [JsonProperty("C507")]
            [XmlElement(ElementName = "C507")]
            public SG2_C507 C507 { get; set; }
        }

        [EdiElement, EdiPath("*/0")]
        public class SG2_C507
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E2005")]
            [XmlElement(ElementName = "E2005")]
            public string x_2005 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E2380")]
            [XmlElement(ElementName = "E2380")]
            public string x_2380 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E2379")]
            [XmlElement(ElementName = "E2379")]
            public string x_2379 { get; set; }
        }

        #endregion

        #endregion

        #region SG3

        // [EdiSegmentGroup("NAD")]
        public class SG3
        {
            [JsonProperty("NAD")]
            [XmlElement(ElementName = "NAD")]
            public SG3_NAD NAD { get; set; }
        }

        #region SG3_NAD

        /// <summary>
        /// Name and address
        /// </summary>
        [EdiSegment, EdiPath("NAD")]
        public class SG3_NAD
        {
            /// <summary>
            /// Party function code qualifier
            /// </summary>

            [JsonProperty("E3035")]
            [XmlElement(ElementName = "E3035")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_3035 { get; set; }

            [JsonProperty("C082")]
            [XmlElement(ElementName = "C082")]
            public SG3_C082 SG3_C082 { get; set; }

            [JsonProperty("C058")]
            [XmlElement(ElementName = "C058")]
            public SG3_C058 SG3_C058 { get; set; }

            [JsonProperty("C080")]
            [XmlElement(ElementName = "C080")]
            public SG3_C080 SG3_C080 { get; set; }

            [JsonProperty("C059")]
            [XmlElement(ElementName = "C059")]
            public SG3_C059 SG3_C059 { get; set; }

            [JsonProperty("E3164")]
            [XmlElement(ElementName = "E3164")]
            public string x_3164 { get; set; }

            [JsonProperty("C819")]
            [XmlElement(ElementName = "C819")]
            public SG3_C819 SG3_C819 { get; set; }

            /// <summary>
            /// Postal identification code
            /// </summary>
            [EdiValue("9(99)", Path = "*/6/0")]
            [JsonProperty("E3251")]
            [XmlElement(ElementName = "E3251")]
            public string x_3251 { get; set; }

            /// <summary>
            /// Country name code
            /// </summary>
            [EdiValue("9(99)", Path = "*/7/0")]
            [JsonProperty("E3207")]
            [XmlElement(ElementName = "E3207")]
            public string x_3207 { get; set; }
        }

        /// <summary>
        /// PARTY IDENTIFICATION DETAILS
        /// </summary>
        [EdiElement, EdiPath("*/1")]
        public class SG3_C082
        {
            /// <summary>
            /// Party identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E3039")]
            [XmlElement(ElementName = "E3039")]
            public string x_3039 { get; set; }

            /// <summary>
            /// Code list identification code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            public string x_1131 { get; set; }

            /// <summary>
            /// Code list responsible agency code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            public string x_3055 { get; set; }
        }

        /// <summary>
        /// NAME AND ADDRESS
        /// </summary>
        [EdiElement, EdiPath("*/2")]
        public class SG3_C058
        {
            /// <summary>
            /// Name and address description
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E3124")]
            [XmlElement(ElementName = "E3124")]
            public string? x_3124 { get; set; }
            /// <summary>
            /// Name and address description
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E3124_2")]
            [XmlElement(ElementName = "E3124_2")]
            public string? x_3124_2 { get; set; }
            /// <summary>
            /// Name and address description
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E3124_3")]
            [XmlElement(ElementName = "E3124_3")]
            public string? x_3124_3 { get; set; }
            /// <summary>
            /// Name and address description
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E3124_4")]
            [XmlElement(ElementName = "E3124_4")]
            public string? x_3124_4 { get; set; }
            /// <summary>
            /// Name and address description
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/4")]
            [JsonProperty("E3124_5")]
            [XmlElement(ElementName = "E3124_5")]
            public string? x_3124_5 { get; set; }

        }

        /// <summary>
        /// PARTY NAME
        /// </summary>
        [EdiElement, EdiPath("*/3")]
        public class SG3_C080
        {
            /// <summary>
            /// Party name
            /// </summary>
            [JsonProperty("E3036")]
            [XmlElement(ElementName = "E3036")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_3036 { get; set; }
            /// <summary>
            /// Party name
            /// </summary>
            [JsonProperty("E3036_2")]
            [XmlElement(ElementName = "E3036_2")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_3036_2 { get; set; }
            /// <summary>
            /// Party name
            /// </summary>
            [JsonProperty("E3036_3")]
            [XmlElement(ElementName = "E3036_3")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_3036_3 { get; set; }
            /// <summary>
            /// Party name
            /// </summary>
            [JsonProperty("E3036_4")]
            [XmlElement(ElementName = "E3036_4")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_3036_4 { get; set; }
            /// <summary>
            /// Party name
            /// </summary>
            [JsonProperty("E3036_5")]
            [XmlElement(ElementName = "E3036_5")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_3036_5 { get; set; }
            /// <summary>
            /// Party name
            /// </summary>
            [JsonProperty("E3036_6")]
            [XmlElement(ElementName = "E3036_6")]
            [EdiValue("9(99)", Path = "*/*/5")]
            public string x_3036_6 { get; set; }
        }

        /// <summary>
        /// STREET
        /// </summary>
        [EdiElement, EdiPath("*/4")]
        public class SG3_C059
        {
            /// <summary>
            /// Street and number or post office box identifier
            /// </summary>
            [JsonProperty("E3042")]
            [XmlElement(ElementName = "E3042")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_3042 { get; set; }
            /// <summary>
            /// Street and number or post office box identifier
            /// </summary>
            [JsonProperty("E3042_2")]
            [XmlElement(ElementName = "E3042_2")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_3042_2 { get; set; }
            /// <summary>
            /// Street and number or post office box identifier
            /// </summary>
            [JsonProperty("E3042_3")]
            [XmlElement(ElementName = "E3042_3")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_3042_3 { get; set; }
            /// <summary>
            /// Street and number or post office box identifier
            /// </summary>
            [JsonProperty("E3042_4")]
            [XmlElement(ElementName = "E3042_4")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_3042_4 { get; set; }
        }

        /// <summary>
        /// COUNTRY SUB-ENTITY DETAILS
        /// </summary>
        [EdiElement, EdiPath("*/5")]
        public class SG3_C819
        {
            /// <summary>
            /// Country sub-entity name code
            /// </summary>
            [JsonProperty("E3229")]
            [XmlElement(ElementName = "E3229")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_3229 { get; set; }

            /// <summary>
            /// Code list identification code
            /// </summary>
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_1131 { get; set; }

            /// <summary>
            /// Code list responsible agency code
            /// </summary>
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_3055 { get; set; }

            /// <summary>
            /// Country sub-entity name
            /// </summary>
            [JsonProperty("E3228")]
            [XmlElement(ElementName = "E3228")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_3228 { get; set; }
        }

        #endregion

        #endregion

        #region SG4

        [EdiSegmentGroup("ERC", "FTX", "RFF", "FTX")]
        public class SG4
        {
            [JsonProperty("ERC")]
            [XmlElement(ElementName = "ERC")]
            public SG4_ERC SG4_ERC { get; set; }

            [JsonProperty("FTX")]
            [XmlElement(ElementName = "FTX")]
            public SG4_FTX SG4_FTX { get; set; }

            [JsonProperty("SG5")]
            [XmlElement(ElementName = "SG5")]
            public List<SG5> SG5 { get; set; }

        }

        #region SG4_ERC

        [EdiSegment, EdiPath("ERC")]
        public class SG4_ERC
        {
            [JsonProperty("C901")]
            [XmlElement(ElementName = "C901")]
            public SG4_C901 SG4_C901 { get; set; }

        }


        [EdiElement, EdiPath("*/0")]
        public class SG4_C901
        {
            [JsonProperty("E9321")]
            [XmlElement(ElementName = "E9321")]
            [EdiValue("9(99)", Path = "*/0/0")]
            public string x_9321 { get; set; }

            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/0/1")]
            public string x_1131 { get; set; }

            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/0/2")]
            public string x_3055 { get; set; }

        }



        #endregion


        #region SG4_FTX

        [EdiSegment, EdiPath("FTX")]
        public class SG4_FTX
        {

            [EdiValue("9(99)", Path = "*/0/0")]
            [JsonProperty("E4451")]
            [XmlElement(ElementName = "E4451")]
            public string x_4451 { get; set; }

            [EdiValue("9(99)", Path = "*/1/0")]
            [JsonProperty("E4453")]
            [XmlElement(ElementName = "E4453")]
            public string x_4453 { get; set; }

            [JsonProperty("C107")]
            [XmlElement(ElementName = "C107")]
            public SG4_C107 SG4_C107 { get; set; }

            [JsonProperty("C108")]
            [XmlElement(ElementName = "C108")]
            public SG4_C108 SG4_C108 { get; set; }

            [EdiValue("9(99)", Path = "*/4/0")]
            [JsonProperty("E3453")]
            [XmlElement(ElementName = "E3453")]
            public string x_3453 { get; set; }

            [EdiValue("9(99)", Path = "*/5/0")]
            [JsonProperty("E4447")]
            [XmlElement(ElementName = "E4447")]
            public string x_4447 { get; set; }
        }


        [EdiElement, EdiPath("*/2")]
        public class SG4_C107
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E4441")]
            [XmlElement(ElementName = "E4441")]
            public string x_4441 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            public string x_1131 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            public string x_3055 { get; set; }
        }

        [EdiElement, EdiPath("*/3")]
        public class SG4_C108
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E4440")]
            [XmlElement(ElementName = "E4440")]
            public List<string> x_4440 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E4440_2")]
            [XmlElement(ElementName = "E4440_2")]
            public List<string> x_4440_2 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E4440_3")]
            [XmlElement(ElementName = "E4440_3")]
            public List<string> x_4440_3 { get; set; }

            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E4440_4")]
            [XmlElement(ElementName = "E4440_4")]
            public List<string> x_4440_4 { get; set; }

            [EdiValue("9(99)", Path = "*/*/4")]
            [JsonProperty("E4440_5")]
            [XmlElement(ElementName = "E4440_5")]
            public List<string> x_4440_5 { get; set; }
        }

        #endregion


        #region SG5

        [EdiSegmentGroup("RFF", "FTX")]
        public class SG5
        {

            [JsonProperty("RFF")]
            [XmlElement(ElementName = "RFF")]
            public SG5_RFF SG5_RFF { get; set; }

            [JsonProperty("FTX")]
            [XmlElement(ElementName = "FTX")]
            public List<SG5_FTX> SG5_FTX { get; set; }
        }

        #region SG5_RFF
        [EdiSegment, EdiPath("RFF")]
        public class SG5_RFF
        {
            /// <summary>
            /// REFERENCE
            /// </summary>
            [JsonProperty("C506")]
            [XmlElement(ElementName = "C506")]
            //[EdiElement, EdiPath("*/0")]
            public SG5_C506 SG5_C506 { get; set; }
        }

        /// <summary>
        /// REFERENCE
        /// </summary>
        [EdiElement, EdiPath("*/0")]
        public class SG5_C506
        {
            /// <summary>
            /// Reference code qualifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E1153")]
            [XmlElement(ElementName = "E1153")]
            public string X_1153 { get; set; }
            /// <summary>
            /// Reference identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1154")]
            [XmlElement(ElementName = "E1154")]
            public string X_1154 { get; set; }
            /// <summary>
            /// Document line identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E1156")]
            [XmlElement(ElementName = "E1156")]
            public string X_1156 { get; set; }
            /// <summary>
            /// Reference version identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E4000")]
            [XmlElement(ElementName = "E4000")]
            public string X_4000 { get; set; }
            /// <summary>
            /// Revision identifier
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/4")]
            [JsonProperty("E1060")]
            [XmlElement(ElementName = "E1060")]
            public string X_1060 { get; set; }
        }

        #endregion

        #region SG5_FTX

        [EdiSegment, EdiPath("FTX")]
        public class SG5_FTX
        {

            [EdiValue("9(99)", Path = "*/0/0")]
            [JsonProperty("E4451")]
            [XmlElement(ElementName = "E4451")]
            public string x_4451 { get; set; }

            [EdiValue("9(99)", Path = "*/1/0")]
            [JsonProperty("E4453")]
            [XmlElement(ElementName = "E4453")]
            public string x_4453 { get; set; }

            [JsonProperty("C107")]
            [XmlElement(ElementName = "C107")]
            public SG5_C107 SG5_C107 { get; set; }

            [JsonProperty("C108")]
            [XmlElement(ElementName = "C108")]
            public SG5_C108 SG5_C108 { get; set; }

            [EdiValue("9(99)", Path = "*/4/0")]
            [JsonProperty("E3453")]
            [XmlElement(ElementName = "E3453")]
            public string x_3453 { get; set; }

            [EdiValue("9(99)", Path = "*/5/0")]
            [JsonProperty("E4447")]
            [XmlElement(ElementName = "E4447")]
            public string x_4447 { get; set; }
        }


        [EdiElement, EdiPath("*/2")]
        public class SG5_C107
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E4441")]
            [XmlElement(ElementName = "E4441")]
            public string x_4441 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            public string x_1131 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            public string x_3055 { get; set; }
        }

        [EdiElement, EdiPath("*/3")]
        public class SG5_C108
        {

            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E4440")]
            [XmlElement(ElementName = "E4440")]
            public string x_4440 { get; set; }

            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E4440_2")]
            [XmlElement(ElementName = "E4440_2")]
            public string x_4440_2 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E4440_3")]
            [XmlElement(ElementName = "E4440_3")]
            public string x_4440_3 { get; set; }

            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E4440_4")]
            [XmlElement(ElementName = "E4440_4")]
            public string x_4440_4 { get; set; }

            [EdiValue("9(99)", Path = "*/*/4")]
            [JsonProperty("E4440_5")]
            [XmlElement(ElementName = "E4440_5")]
            public string x_4440_5 { get; set; }
        }

        #endregion

        #endregion

        #endregion

        #region UNT

        [EdiSegment, EdiPath("UNT")]
        public class UNT
        {

            [JsonProperty("E0074")]
            [XmlElement(ElementName = "E0074")]
            [EdiValue(Path = "*/0/0")]
            [EdiCount(EdiCountScope.Segments)]
            public string x_0074 { get; set; } //od unh do tego


            [JsonProperty("E0062")]
            [XmlElement(ElementName = "E0062")]
            [EdiValue("9(99)", Path = "*/1/0")]
            public string x_0062 { get; set; }
        }


        #endregion
    }

}
