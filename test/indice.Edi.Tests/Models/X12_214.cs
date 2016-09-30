using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models
{
    public class Transportation_214
    {
        [EdiValue("9(2)", Path = "ISA/0", Description = "I01 - Authorization Information Qualifier")]
        public int AuthorizationInformationQualifier { get; set; }
        [EdiValue("X(10)", Path = "ISA/1", Description = "")]
        public string AuthorizationInformation { get; set; }
        [EdiValue("9(2)", Path = "ISA/2", Description = "I03 - Security Information Qualifier")]
        public string Security_Information_Qualifier { get; set; }

        [EdiValue("X(10)", Path = "ISA/3", Description = "I04 - Security Information")]
        public string Security_Information { get; set; }

        [EdiValue("9(2)", Path = "ISA/4", Description = "I05 - Interchange ID Qualifier")]
        public string ID_Qualifier { get; set; }

        [EdiValue("X(15)", Path = "ISA/5", Description = "I06 - Interchange Sender ID")]
        public string Sender_ID { get; set; }

        [EdiValue("9(2)", Path = "ISA/6", Description = "I05 - Interchange ID Qualifier")]
        public string ID_Qualifier2 { get; set; }

        [EdiValue("X(15)", Path = "ISA/7", Description = "I07 - Interchange Receiver ID")]
        public string Receiver_ID { get; set; }

        [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I08 - Interchange Date")]
        [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "TI09 - Interchange Time")]
        public DateTime Date { get; set; }

        [EdiValue("X(1)", Path = "ISA/10", Description = "I10 - Interchange Control Standards ID")]
        public string Control_Standards_ID { get; set; }

        [EdiValue("9(5)", Path = "ISA/11", Description = "I11 - Interchange Control Version Num")]
        public int ControlVersion { get; set; }

        [EdiValue("9(9)", Path = "ISA/12", Description = "I12 - Interchange Control Number")]
        public int ControlNumber { get; set; }

        [EdiValue("9(1)", Path = "ISA/13", Description = "I13 - Acknowledgement Requested")]
        public bool? AcknowledgementRequested { get; set; }

        [EdiValue("X(1)", Path = "ISA/14", Description = "I14 - Usage Indicator")]
        public string Usage_Indicator { get; set; }

        [EdiValue("X(1)", Path = "ISA/15", Description = "I15 - Component Element Separator")]
        public char? Component_Element_Separator { get; set; }
        [EdiValue("9(1)", Path = "IEA/0", Description = "I16 - Num of Included Functional Grps")]
        public int GroupsCount { get; set; }

        [EdiValue("9(9)", Path = "IEA/1", Description = "I12 - Interchange Control Number")]
        public int TrailerControlNumber { get; set; }

        public List<FunctionalGroup> Groups { get; set; }

        [EdiGroup]
        public class FunctionalGroup
        {
            [EdiValue("X(2)", Path = "GS/0", Description = "479 - Functional Identifier Code")]
            public string FunctionalIdentifierCode { get; set; }

            [EdiValue("X(15)", Path = "GS/1", Description = "142 - Application Sender's Code")]
            public string ApplicationSenderCode { get; set; }

            [EdiValue("X(15)", Path = "GS/2", Description = "124 - Application Receiver's Code")]
            public string ApplicationReceiverCode { get; set; }

            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "373 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "337 - Time")]
            public DateTime Date { get; set; }

            [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "28 - Group Control Number")]
            public int GroupControlNumber { get; set; }

            [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "455 Responsible Agency Code")]
            public string AgencyCode { get; set; }

            [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "480 Version / Release / Industry Identifier Code")]
            public string Version { get; set; }

            public List<Message> Messages { get; set; }

            [EdiValue("9(1)", Path = "GE/0", Description = "97 Number of Transaction Sets Included")]
            public int TransactionsCount { get; set; }

            [EdiValue("9(9)", Path = "GE/1", Description = "28 Group Control Number")]
            public int GroupTrailerControlNumber { get; set; }


        }


        [EdiMessage]
        public class Message
        {
            [EdiValue("9(3)", Path = "ST/00", Description = "")]
            public int IdentifierCode { get; set; }
            [EdiValue("X(9)", Path = "ST/01", Description = "")]
            public string ControlNumber { get; set; }

            [EdiValue("9(30)", Path = "B10/0")]
            public int ReferenceIdentification { get; set; }

            public List<Place> Places { get; set; }
        }

        [EdiSegment, EdiSegmentGroup("N1", SequenceEnd = "LX")]
        public class Place
        {
            [EdiValue("X(9)", Path = "N1/0", Description = "")]
            public string FieldValue1 { get; set; }

            [EdiValue("X(9)", Path = "N3/0", Description = "")]
            public string FieldValue2 { get; set; }
        }

    }
}
