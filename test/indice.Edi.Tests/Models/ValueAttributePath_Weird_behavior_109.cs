using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class ValueAttributePath_Weird_behavior
    {
        public Message Msg { get; set; }

        [EdiMessage]
        public class Message
        {
            public PAC_Segment PAC { get; set; }
        }


        [EdiSegment, EdiPath("PAC")]
        public class PAC_Segment
        {
            [EdiValue("X(3)", Path = "PAC/0/0")]
            public string PackageCount { get; set; }

            [EdiValue("X(3)", Path = "PAC/1/1")]
            public string PackageDetailLevel { get; set; }

            [EdiValue("X(3)", Path = "PAC/2/0")]
            public string PackageType { get; set; }
        }
    }
}
