using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class EdiFact_Issue121_ElementList
    {
        public Message Msg { get; set; }

        [EdiMessage]
        public class Message
        {
            public List<Attribute> Attributes { get; set; }
        }

        [EdiSegment, EdiPath("ATR")]
        public class Attribute
        {
            [EdiPath("ATR/*")]
            public List<Information> Infos { get; set; }
        }

        [EdiElement]
        public class Information
        {
            [EdiValue("X(4)", Path = "*/*/0")]
            public string Code { get; set; }

            [EdiValue("X(256)", Path = "*/*/1")]
            public string Value { get; set; }
        }
    }
}
