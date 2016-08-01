using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models.EdiFact01
{
    [EdiElement, EdiPath("DTM/0"), EdiCondition("137", Path = "DTM/0/0")]
    public class CreationDate
    {
        [EdiValue("9(3)", Path = "DTM/0/0")]
        public int Code { get; set; }
        [EdiValue("X(12)", Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
        public DateTime Date { get; set; }
    }

    [EdiElement, EdiPath("DTM/0"), EdiCondition("ZZZ", Path = "DTM/0/0")]
    public class CreationZone
    {
        [EdiValue("X(3)", Path = "DTM/0/0")]
        public string Code { get; set; }
        [EdiValue("9(1)", Path = "DTM/0/1")]
        public int UtcOffset { get; set; }
    }

    public class Interchange
    {
        public List<Message> Messages { get; set; }
    }

    [EdiMessage]
    public class Message
    {
        public CreationDate CreationDate { get; set; }

        public CreationZone CreationZone { get; set; }
    }
}
