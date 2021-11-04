using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using indice.Edi.Serialization;
using Newtonsoft.Json;

namespace indice.Edi.Tests.Models
{
    public class Edifact_Issue203
    {
        [JsonProperty("INVOIC")]
        [XmlElement(ElementName = "INVOIC")]
        public Message EDIFACT_INVOICS4 { get; set; }

        [EdiMessage]
        public class Message
        {
            [JsonProperty("UNH")]
            [XmlElement(ElementName = "UNH")]
            public UNH UNH { get; set; }

            [JsonProperty("BGM")]
            [XmlElement(ElementName = "BGM")]
            public BGM BGM { get; set; }

            [JsonProperty("SG6")]
            [XmlElement(ElementName = "SG6")]
            public List<SG6> SG6 { get; set; }

            [JsonProperty("UNS")]
            [XmlElement(ElementName = "UNS")]
            public UNS UNS { get; set; }


            [JsonProperty("SG52")]
            [XmlElement(ElementName = "SG52")]
            public List<SG52> SG52 { get; set; }


        }

        #region UNH
        /// <summary>
        /// Message header
        /// </summary>
        [EdiSegment, EdiPath("UNH")]
        public class UNH
        {
            /// <summary>
            /// Message reference number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0062")]
            [XmlElement(ElementName = "E0062")]
            public string x_0062 { get; set; }

            [JsonProperty("S009")]
            [XmlElement(ElementName = "S009")]
            public S009 S009 { get; set; }

            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0113")]
            [XmlElement(ElementName = "E0113")]
            private string x_0113 { get; set; }

            [JsonProperty("S010")]
            [XmlElement(ElementName = "S010")]
            public S010 S010 { get; set; }

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
        /// <summary>
        /// MESSAGE IDENTIFIER
        /// </summary>
        [EdiElement, EdiPath("*/1")]
        public class S009
        {
            /// <summary>
            /// Message type
            /// </summary>
            [EdiValue, EdiPath("*/*/0")]
            [JsonProperty("E0065")]
            [XmlElement(ElementName = "E0065")]
            public string x_0065 { get; set; }
            /// <summary>
            /// Message version number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0052")]
            [XmlElement(ElementName = "E0052")]
            public string x_0052 { get; set; }
            /// <summary>
            /// Message release number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0054")]
            [XmlElement(ElementName = "E0054")]
            public string x_0054 { get; set; }
            /// <summary>
            /// Controlling agency, coded
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }
            /// <summary>
            /// Association assigned code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/4")]
            [JsonProperty("E0057")]
            [XmlElement(ElementName = "E0057")]
            public string x_0057 { get; set; }
            /// <summary>
            /// Code list directory version number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/5")]
            [JsonProperty("E0110")]
            [XmlElement(ElementName = "E0110")]
            public string x_0110 { get; set; }
            /// <summary>
            /// Message type sub-function identification
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/6")]
            [JsonProperty("E0113")]
            [XmlElement(ElementName = "E0113")]
            public string x_0113 { get; set; }
        }
        /// <summary>
        /// STATUS OF THE TRANSFER
        /// </summary>
        [EdiElement, EdiPath("*/3")]
        public class S010
        {
            /// <summary>
            /// Sequence of transfers
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0070")]
            [XmlElement(ElementName = "E0070")]
            public string x_0070 { get; set; }
            /// <summary>
            /// First and last transfer
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0073")]
            [XmlElement(ElementName = "E0073")]
            public string x_0073 { get; set; }
        }
        /// <summary>
        /// MESSAGE SUBSET IDENTIFICATION
        /// </summary>
        [EdiElement, EdiPath("*/4")]
        public class S016
        {
            /// <summary>
            /// Message subset identification
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0115")]
            [XmlElement(ElementName = "E0115")]
            public string x_0115 { get; set; }
            /// <summary>
            /// Message subset version number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0116")]
            [XmlElement(ElementName = "E0116")]
            public string x_0116 { get; set; }
            /// <summary>
            /// Message subset release number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0118")]
            [XmlElement(ElementName = "E0118")]
            public string x_0118 { get; set; }
            /// <summary>
            /// Controlling agency, coded
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }
        }
        /// <summary>
        /// MESSAGE IMPLEMENTATION GUIDELINE IDENTIFICATION
        /// </summary>
        [EdiElement, EdiPath("*/5")]
        public class S017
        {
            /// <summary>
            /// Message implementation guideline identification
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0121")]
            [XmlElement(ElementName = "E0121")]
            public string x_0121 { get; set; }
            /// <summary>
            /// Message implementation guideline version number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0122")]
            [XmlElement(ElementName = "E0122")]
            public string x_0122 { get; set; }
            /// <summary>
            /// Message implementation guideline release number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0124")]
            [XmlElement(ElementName = "E0124")]
            public string x_0124 { get; set; }
            /// <summary>
            /// Controlling agency, coded
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E0051")]
            [XmlElement(ElementName = "E0051")]
            public string x_0051 { get; set; }
        }
        /// <summary>
        /// SCENARIO IDENTIFICATION
        /// </summary>
        [EdiElement, EdiPath("*/6")]
        public class S018
        {
            /// <summary>
            /// Scenario identification
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/0")]
            [JsonProperty("E0127")]
            [XmlElement(ElementName = "E0127")]
            public string x_0127 { get; set; }
            /// <summary>
            /// Scenario version number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E0128")]
            [XmlElement(ElementName = "E0128")]
            public string x_0128 { get; set; }
            /// <summary>
            /// Scenario release number
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E0130")]
            [XmlElement(ElementName = "E0130")]
            public string x_0130 { get; set; }
            /// <summary>
            /// Controlling agency, coded
            /// </summary>
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
            public C002 C106 { get; set; }
            /// <summary>
            /// Response type code
            /// </summary>
            [EdiValue("9(99)", Path = "*/2/0")]
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
            [EdiValue("9(99)", Path = "*/*/1")]
            [JsonProperty("E1001")]
            [XmlElement(ElementName = "E1001")]
            public string x_1001 { get; set; }
            /// <summary>
            /// Code list identification code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/2")]
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            public string x_1131 { get; set; }
            /// <summary>
            /// Code list responsible agency code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            public string x_3055 { get; set; }
            /// <summary>
            /// Document name
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/4")]
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
            /// <summary>
            /// Message function code
            /// </summary>
            [EdiValue("9(99)", Path = "*/*/3")]
            [JsonProperty("E1225")]
            [XmlElement(ElementName = "E1225")]
            public string x_1225 { get; set; }
        }

        #endregion


        #region SG6
        [EdiSegmentGroup("TAX", "MOA", SequenceEnd = "UNS")]
        public class SG6 : SG6_TAX
        {
            // [JsonProperty("TAX")]
            // [XmlElement(ElementName = "TAX")]
            //  public SG6_TAX SG6_TAX { get; set; }

            [JsonProperty("MOA")]
            [XmlElement(ElementName = "MOA")]
            public SG6_MOA SG6_MOA { get; set; }
        }

        #region SG6_TAX

        /// <summary>
        /// Duty/tax/fee details
        /// </summary>
        [EdiSegment, EdiPath("TAX")]
        public class SG6_TAX
        {
            /// <summary>
            /// Duty or tax or fee function code qualifier
            /// </summary>
            [JsonProperty("E5283")]
            [XmlElement(ElementName = "E5283")]
            [EdiValue("9(99)", Path = "*/0/0")]
            public string x_5283 { get; set; }

            /// <summary>
            /// DUTY/TAX/FEE TYPE
            /// </summary>
            [JsonProperty("C241")]
            [XmlElement(ElementName = "C241")]
            public SG6_C241 SG6_C241 { get; set; }

            /// <summary>
            /// DUTY/TAX/FEE ACCOUNT DETAIL
            /// </summary>
            [JsonProperty("C533")]
            [XmlElement(ElementName = "C533")]
            public SG6_C533 SG6_C533 { get; set; }

            /// <summary>
            /// Duty or tax or fee assessment basis value
            /// </summary>
            [JsonProperty("E5286")]
            [XmlElement(ElementName = "E5286")]
            [EdiValue("9(99)", Path = "*/3/0")]
            public string x_5286 { get; set; }

            /// <summary>
            /// DUTY/TAX/FEE DETAIL
            /// </summary>
            [JsonProperty("C243")]
            [XmlElement(ElementName = "C243")]
            public SG6_C243 SG6_C243 { get; set; }

            /// <summary>
            /// Duty or tax or fee category code
            /// </summary>
            [JsonProperty("E5305")]
            [XmlElement(ElementName = "E5305")]
            [EdiValue("9(99)", Path = "*/5/0")]
            public string x_5305 { get; set; }

            /// <summary>
            /// Party tax identifier
            /// </summary>
            [JsonProperty("E3446")]
            [XmlElement(ElementName = "E3446")]
            [EdiValue("9(99)", Path = "*/6/0")]
            public string x_3446 { get; set; }

            /// <summary>
            /// Calculation sequence code
            /// </summary>
            [JsonProperty("E1227")]
            [XmlElement(ElementName = "E1227")]
            [EdiValue("9(99)", Path = "*/7/0")]
            public string x_1227 { get; set; }
        }

        /// <summary>
        /// DUTY/TAX/FEE TYPE
        /// </summary>
        [EdiElement, EdiPath("*/1")]
        public class SG6_C241
        {
            /// <summary>
            /// Duty or tax or fee type name code
            /// </summary>
            [JsonProperty("E5153")]
            [XmlElement(ElementName = "E5153")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5153 { get; set; }

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
            /// Duty or tax or fee type name
            /// </summary>
            [JsonProperty("E5152")]
            [XmlElement(ElementName = "E5152")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_5152 { get; set; }
        }

        /// <summary>
        /// DUTY/TAX/FEE ACCOUNT DETAIL
        /// </summary>
        [EdiElement, EdiPath("*/2")]
        public class SG6_C533
        {
            /// <summary>
            /// Duty or tax or fee account code
            /// </summary>
            [JsonProperty("E5289")]
            [XmlElement(ElementName = "E5289")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5289 { get; set; }

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
        }

        /// <summary>
        /// DUTY/TAX/FEE DETAIL
        /// </summary>
        [EdiElement, EdiPath("*/4")]
        public class SG6_C243
        {
            /// <summary>
            /// Duty or tax or fee rate code
            /// </summary>
            [JsonProperty("E5279")]
            [XmlElement(ElementName = "E5279")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5279 { get; set; }

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
            /// Duty or tax or fee rate
            /// </summary>
            [JsonProperty("E5278")]
            [XmlElement(ElementName = "E5278")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_5278 { get; set; }

            /// <summary>
            /// Duty or tax or fee rate basis code
            /// </summary>
            [JsonProperty("E5273")]
            [XmlElement(ElementName = "E5273")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_5273 { get; set; }

            /// <summary>
            /// Code list identification code
            /// </summary>
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/*/5")]
            public string x_1131_2 { get; set; }

            /// <summary>
            /// Code list responsible agency code
            /// </summary>
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/*/6")]
            public string x_3055_2 { get; set; }
        }

        #endregion

        #region SG6_MOA
        [EdiSegment, EdiPath("MOA")]
        public class SG6_MOA
        {
            /// <summary>
            /// MONETARY AMOUNT
            /// </summary>
            [JsonProperty("C516")]
            [XmlElement(ElementName = "C516")]
            public SG6_C516 SG6_C516 { get; set; }
        }

        /// <summary>
        /// MONETARY AMOUNT
        /// </summary>
        [EdiElement, EdiPath("*/0")]
        public class SG6_C516
        {
            /// <summary>
            /// Monetary amount type code qualifier
            /// </summary>
            [JsonProperty("E5025")]
            [XmlElement(ElementName = "E5025")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5025 { get; set; }

            /// <summary>
            /// Monetary amount
            /// </summary>
            [JsonProperty("E5004")]
            [XmlElement(ElementName = "E5004")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_5004 { get; set; }

            /// <summary>
            /// Currency identification code
            /// </summary>
            [JsonProperty("E6345")]
            [XmlElement(ElementName = "E6345")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_6345 { get; set; }

            /// <summary>
            /// Currency type code qualifier
            /// </summary>
            [JsonProperty("E6343")]
            [XmlElement(ElementName = "E6343")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_6343 { get; set; }

            /// <summary>
            /// Status description code
            /// </summary>
            [JsonProperty("E4405")]
            [XmlElement(ElementName = "E4405")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_4405 { get; set; }
        }

        #endregion

        #endregion


        #region UNS
        [EdiSegment, EdiPath("UNS")]
        public class UNS
        {
            /// <summary>
            /// Section identification
            /// </summary>
            [JsonProperty("E0081")]
            [XmlElement(ElementName = "E0081")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_0081 { get; set; }
        }

        #endregion

        //TODO
        #region SG52
        [EdiSegmentGroup("TAX", "MOA", SequenceEnd = "UNT"), EdiCondition("all")]
        public class SG52 : SG52_TAX
        {
            //  [JsonProperty("TAX")]
            //  [XmlElement(ElementName = "TAX")]
            //  public SG52_TAX SG52_TAX { get; set; }

            [JsonProperty("MOA")]
            [XmlElement(ElementName = "MOA")]
            public List<SG52_MOA> SG52_MOA { get; set; }
        }

        #region SG52_TAX

        /// <summary>
        /// Duty/tax/fee details
        /// </summary>
        [EdiSegment, EdiPath("TAX")]
        public class SG52_TAX
        {
            /// <summary>
            /// Duty or tax or fee function code qualifier
            /// </summary>
            [JsonProperty("E5283")]
            [XmlElement(ElementName = "E5283")]
            [EdiValue("9(99)", Path = "*/0/0")]
            public string x_5283 { get; set; }

            /// <summary>
            /// DUTY/TAX/FEE TYPE
            /// </summary>
            [JsonProperty("C241")]
            [XmlElement(ElementName = "C241")]
            public SG52_C241 SG52_C241 { get; set; }

            /// <summary>
            /// DUTY/TAX/FEE ACCOUNTDETAIL
            /// </summary>
            [JsonProperty("C533")]
            [XmlElement(ElementName = "C533")]
            public SG52_C533 SG52_C533 { get; set; }

            /// <summary>
            /// Duty or tax or fee assessment basis value
            /// </summary>
            [JsonProperty("E5286")]
            [XmlElement(ElementName = "E5286")]
            [EdiValue("9(99)", Path = "*/3/0")]
            public string x_5286 { get; set; }

            /// <summary>
            /// DUTY/TAX/FEE DETAIL
            /// </summary>
            [JsonProperty("C243")]
            [XmlElement(ElementName = "C243")]
            public SG52_C243 SG52_C243 { get; set; }

            /// <summary>
            /// Duty or tax or fee category code
            /// </summary>
            [JsonProperty("E5305")]
            [XmlElement(ElementName = "E5305")]
            [EdiValue("9(99)", Path = "*/5/0")]
            public string x_5305 { get; set; }

            /// <summary>
            /// Party tax identifier
            /// </summary>
            [JsonProperty("E3446")]
            [XmlElement(ElementName = "E3446")]
            [EdiValue("9(99)", Path = "*/6/0")]
            public string x_3446 { get; set; }

            /// <summary>
            /// Calculation sequence code
            /// </summary>
            [JsonProperty("E1227")]
            [XmlElement(ElementName = "E1227")]
            [EdiValue("9(99)", Path = "*/7/0")]
            public string x_1227 { get; set; }
        }

        /// <summary>
        /// DUTY/TAX/FEE TYPE 
        /// </summary>
        [EdiElement, EdiPath("*/1")]
        public class SG52_C241
        {
            /// <summary>
            /// Duty or tax or fee type name code
            /// </summary>
            [JsonProperty("E5153")]
            [XmlElement(ElementName = "E5153")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5153 { get; set; }

            /// <summary>
            /// Code list identification code 
            /// </summary>
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_1131 { get; set; }

            /// <summary>
            ///Code list responsible agency code
            /// </summary>
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_3055 { get; set; }

            /// <summary>
            /// Duty or tax or fee type name 
            /// </summary>
            [JsonProperty("E5152")]
            [XmlElement(ElementName = "E5152")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_5152 { get; set; }
        }

        /// <summary>
        /// DUTY/TAX/FEE ACCOUNT DETAIL
        /// </summary>
        [EdiElement, EdiPath("*/2")]
        public class SG52_C533
        {
            /// <summary>
            /// Duty or tax or fee account code
            /// </summary>
            [JsonProperty("E5289")]
            [XmlElement(ElementName = "E5289")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5289 { get; set; }

            /// <summary>
            /// Code list identification code 
            /// </summary>
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_1131 { get; set; }

            /// <summary>
            ///Code list responsible agency code
            /// </summary>
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_3055 { get; set; }
        }

        /// <summary>
        ///  DUTY/TAX/FEE DETAIL
        /// </summary>
        [EdiElement, EdiPath("*/4")]
        public class SG52_C243
        {
            /// <summary>
            /// Duty or tax or fee rate code 
            /// </summary>
            [JsonProperty("E5279")]
            [XmlElement(ElementName = "E5279")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5279 { get; set; }

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
            /// Duty or tax or fee rate
            /// </summary>
            [JsonProperty("E5278")]
            [XmlElement(ElementName = "E5278")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_5278 { get; set; }

            /// <summary>
            /// Duty or tax or fee rate basis code
            /// </summary>
            [JsonProperty("E5273")]
            [XmlElement(ElementName = "E5273")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_5273 { get; set; }

            /// <summary>
            /// Code list identification code 
            /// </summary>
            [JsonProperty("E1131")]
            [XmlElement(ElementName = "E1131")]
            [EdiValue("9(99)", Path = "*/*/5")]
            public string x_1131_2 { get; set; }

            /// <summary>
            /// Code list responsible agency code
            /// </summary>
            [JsonProperty("E3055")]
            [XmlElement(ElementName = "E3055")]
            [EdiValue("9(99)", Path = "*/*/6")]
            public string x_3055_2 { get; set; }
        }

        #endregion

        #region SG52_MOA

        /// <summary>
        /// Monetary amount
        /// </summary>
        [EdiSegment, EdiPath("MOA")]
        public class SG52_MOA
        {
            /// <summary>
            /// MONETARY AMOUNT
            /// </summary>
            [JsonProperty("C516")]
            [XmlElement(ElementName = "C516")]
            public SG52_C516 SG52_C516 { get; set; }
        }

        /// <summary>
        /// MONETARY AMOUNT
        /// </summary>
        [EdiElement, EdiPath("*/0")]
        public class SG52_C516
        {
            /// <summary>
            ///  Monetary amount type code qualifier
            /// </summary>
            [JsonProperty("E5025")]
            [XmlElement(ElementName = "E5025")]
            [EdiValue("9(99)", Path = "*/*/0")]
            public string x_5025 { get; set; }

            /// <summary>
            /// Monetary amount
            /// </summary>
            [JsonProperty("E5004")]
            [XmlElement(ElementName = "E5004")]
            [EdiValue("9(99)", Path = "*/*/1")]
            public string x_5004 { get; set; }

            /// <summary>
            /// Currency identification code 
            /// </summary>
            [JsonProperty("E6345")]
            [XmlElement(ElementName = "E6345")]
            [EdiValue("9(99)", Path = "*/*/2")]
            public string x_6345 { get; set; }

            /// <summary>
            /// Currency type code qualifier
            /// </summary>
            [JsonProperty("E6343")]
            [XmlElement(ElementName = "E6343")]
            [EdiValue("9(99)", Path = "*/*/3")]
            public string x_6343 { get; set; }

            /// <summary>
            /// Status description code 
            /// </summary>
            [JsonProperty("E4405")]
            [XmlElement(ElementName = "E4405")]
            [EdiValue("9(99)", Path = "*/*/4")]
            public string x_4405 { get; set; }
        }


        #endregion

        #endregion

    }
