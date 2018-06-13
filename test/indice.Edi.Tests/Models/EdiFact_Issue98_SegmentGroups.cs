using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class Interchange_Issue98
    {
        public MessageInvoice Message { get; set; }

        [EdiMessage]
        public class MessageInvoice
        {
            [EdiElement, EdiPath("RFF/0")]
            public class ReferenceDetail
            {
                [EdiValue("X(3)", Path = "RFF/0/0")]
                public string CodeQualifier { get; set; }

                [EdiValue("X(35)", Path = "RFF/0/1")]
                public string Identifier { get; set; }
            }

            [EdiSegment, EdiPath("NAD")]
            public class PartyNameAddress
            {
                [EdiValue("X(3)", Path = "NAD/0/0")]
                public string CodeQualifier { get; set; }

                [EdiValue("X(35)", Path = "NAD/1/0")]
                public string PartyIdentifier { get; set; }
            }

            [EdiSegmentGroup("NAD", "FII", "RFF", "CTA")]
            public class PartyDetail : PartyNameAddress
            {
                public List<ReferenceDetail> References { get; set; }
            }

            [EdiCondition("SU", Path = "NAD/0")]
            public PartyDetail Seller { get; set; }

            [EdiCondition("BY", Path = "NAD/0")]
            public PartyDetail Buyer { get; set; }
        }
    }
}
