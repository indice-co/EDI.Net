using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    class InheritSegmentGroup
    {
        public List<Message> Messages { get; set; }

        [EdiSegment(), EdiPath("GRP")]
        public class GRP
        {
            [EdiValue("X(35)", Path = "GRP/0")]
            public string Id { get; set; }
        }

        [EdiSegment, EdiPath("IN1")]
        public class InGroup
        {
            [EdiValue("X(35)", Path = "IN1/0")]
            public string Id { get; set; }
        }

        [EdiSegmentGroup("GRP")]
        public class Message : GRP
        {
            public InGroup Element { get; set; }
        }
    }
}
