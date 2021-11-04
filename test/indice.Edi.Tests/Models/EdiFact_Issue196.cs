using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    internal class EdiFact_Issue196_delfor
    {
        public UNB_segment UNB { get; set; }
        public List<Delfor_message> Messages { get; set; }
        

        [EdiMessage]
        public class Delfor_message
        {

            public UNH_segment MessageHeader { get; set; }

            public BGM_segment DeliverySchedule { get; set; }


            [EdiCondition("137", Path = "DTM/0/0")]
            public DTM_segment DocumentDate { get; set; }

            [EdiCondition("ADE", Path = "RFF/0/0")]
            public RFF_segment ReferenceSeller { get; set; }


            [EdiSegment, EdiCondition("BY", Path = "NAD/0/0"), EdiPath("NAD")]
            public NAD_segment BuyerData { get; set; }
            [EdiSegment, EdiCondition("IV", Path = "NAD/0/0"), EdiPath("NAD")]
            public NAD_segment InvoicePartyData { get; set; }
            [EdiSegment, EdiCondition("SE", Path = "NAD/0/0"), EdiPath("NAD")]
            public NAD_segment SellerData { get; set; }


            [EdiCondition("D", Path = "UNS/0")]
            public UNS_segment UNS_Detail { get; set; }
            [EdiCondition("CN", Path = "NAD/0")]
            public List<SG_02> Details { get; set; }


            [EdiCondition("S", Path = "UNS/0")]
            public UNS_segment UNS_Sumary { get; set; }
        }

        [EdiSegment, EdiPath("RFF")]
        public class RFF_segment
        {
            [EdiValue("X(3)", Path = "RFF/0/0")]
            public string Qualifier { get; set; }
            [EdiValue("X(35)", Path = "RFF/0/1")]
            public string Number { get; set; }
            [EdiValue("X(6)", Path = "RFF/0/2")]
            public string Line { get; set; }
            [EdiValue("X(35)", Path = "RFF/0/3")]
            public string VersionNumber { get; set; }
        }

        [EdiSegment, EdiPath("DTM")]
        public class DTM_segment
        {
            [EdiValue("X(3)", Path = "DTM/0/0")]
            public string Qualifier { get; set; }

            [EdiValue("X(35)", Path = "DTM/0/1")]
            public string DateTime { get; set; }

            [EdiValue("X(3)", Path = "DTM/0/2")]
            public string Format { get; set; }
        }

        [EdiSegment, EdiPath("BGM")]
        public class BGM_segment
        {
            [EdiValue("X(3)", Path = "BGM/0/0")]
            public string MessageName { get; set; }
            [EdiValue("X(35)", Path = "BGM/1/0")]
            public string DocumentNumber { get; set; }
        }

        [EdiSegment, EdiPath("UNS")]
        public class UNS_segment
        {
            [EdiValue("X(1)", Path = "UNS/0", Mandatory = true)]
            public string UNS { get; set; }
        }



        [EdiSegmentGroup("NAD", SequenceEnd = "UNS")]
        public class SG_02 : NAD_segment
        {
            public List<SG_03> Lines { get; set; }
        }

        [EdiSegment, EdiPath("NAD")]
        public class NAD_segment
        {
            [EdiValue("X(3)", Path = "NAD/0/0")]
            public string PartyQualifier { get; set; }

            [EdiValue("X(35)", Path = "NAD/1/0")]
            public string PartyId { get; set; }

            [EdiValue("X(3)", Path = "NAD/1/1")]
            public string ResponsibleAgency { get; set; }
        }

        [EdiSegmentGroup("LIN", SequenceEnd = "UNS")]
        public class SG_03 : LIN_segment
        {

        }

        [EdiSegment, EdiPath("LIN")]
        public class LIN_segment
        {
            [EdiValue("X(6)", Path = "LIN/0/0")]
            public string LineNumber { get; set; }

            [EdiValue("X(3)", Path = "LIN/1/0")]
            public string Code { get; set; }

            public ItemNumber NumberIdentification { get; set; }

            [EdiElement, EdiPath("LIN/2")]
            public class ItemNumber
            {
                [EdiValue("X(35)", Path = "LIN/2/0")]
                public string Number { get; set; }

                [EdiValue("X(3)", Path = "LIN/2/1")]
                public string Type { get; set; }
            }
        }

        [EdiSegment, EdiPath("UNB")]
        public class UNB_segment
        {
            [EdiValue("X(4)", Path = "UNB/0/0", Mandatory = true)]
            public string SyntaxIdentifier { get; set; }
            [EdiValue("X(1)", Path = "UNB/0/1", Mandatory = true)]
            public string SyntaxVersionNumber { get; set; }


            // S002 : EMETTEUR DE L'INTERCHANGE
            [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
            public string SenderIdentification { get; set; }
            [EdiValue("X(4)", Path = "UNB/1/1", Mandatory = false)]
            public string SenderIdentificationCodeQualifier { get; set; }
            [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
            public string AddressForReverseRouting { get; set; }


            // S003 : RECEPTEUR DE L'INTERCHANGE
            [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
            public string RecipientIdentification { get; set; }
            [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = false)]
            public string RecipientIdentificationCodeQualifier { get; set; }
            [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
            public string RoutingAddress { get; set; }


            // S004 : DATE ET HEURE DE PREPARATION
            [EdiValue("X(6)", Path = "UNB/3/0")]
            public string DateOfPreparation { get; set; }
            [EdiValue("X(4)", Path = "UNB/3/1")]
            public string TimeOfPreparation { get; set; }


            // S005 : REFERENCE DE CONTROLE DE L’INTERCHANGE
            [EdiValue("X(14)", Path = "UNB/4/0", Mandatory = true)]
            public string INTERCHANGECONTROLREFERENCE { get; set; }


            // 0026 : APPLICATIONREFERENCE
            [EdiValue("X(14)", Path = "UNB/6/0", Mandatory = true)]
            public string APPLICATIONREFERENCE { get; set; }

        }
        [EdiSegment, EdiPath("UNH")]
        public class UNH_segment
        {
            [EdiValue("X(14)", Path = "UNH/0", Description = "Unique message reference assigned by the sender")]
            public string MessageRefNo { get; set; }

            [EdiValue("X(6)", Path = "UNH/1/0", Description = "Code identifying a type of message")]
            public string MessageTypeIdentifier { get; set; }

            [EdiValue("X(3)", Path = "UNH/1/1", Description = "Version number of a message type")]
            public string MessageTypeVerNo { get; set; }

            [EdiValue("X(3)", Path = "UNH/1/2", Description = "Release number within the current message type version number")]
            public string MessageTypeReleaseNo { get; set; }

            [EdiValue("X(2)", Path = "UNH/1/3", Description = "Code identifying the agency controlling the specification, maintenance and publication of the message type")]
            public string ControllingAgency { get; set; }
            [EdiValue("X(6)", Path = "UNH/1/4", Mandatory = false, Description = "Code, assigned by the association responsible for the design and maintenance of the message type concerned, which further identifies the message")]
            public string AssociationAssignedCode { get; set; }
        }
    }
}
