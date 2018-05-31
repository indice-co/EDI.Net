using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class Interchange_Issue91
    {
        public Message Msg { get; set; }


        [EdiMessage]
        public class Message
        {
            public List<Foo> Foos { get; set; }
        }


        [EdiSegmentGroup("XYZ"), EdiCondition("F", Path = "XYZ/0")]
        public class Foo
        {
            [EdiValue("X(3)", Path = "XYZ/1")]
            public string Name { get; set; }
            public List<Bar> Bars { get; set; }
        }

        [EdiSegmentGroup("XYZ"), EdiCondition("B", Path = "XYZ/0")]
        public class Bar
        {
            [EdiValue("9(9)", Path = "XYZ/1")]
            public int Amount { get; set; }
        }
    }
}
