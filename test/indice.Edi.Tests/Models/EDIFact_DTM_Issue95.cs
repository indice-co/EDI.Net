using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class Interchange_Issue95
    {
        public Message Msg { get; set; }

        [EdiMessage]
        public class Message
        {
            [EdiValue("X(35)", Path = "DTM/0/1", Format = "yyyyMMdd")]
            public DateTime DateTime { get; set; }
        }
    }
}
