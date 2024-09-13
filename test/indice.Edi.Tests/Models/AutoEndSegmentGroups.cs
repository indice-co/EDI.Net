using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

class AutoEndSegmentGroups
{
    public List<Message> Messages { get; set; }

    [EdiMessage]
    public class Message
    {
        public Group Group { get; set; }

        public AfterGroup AfterGroup { get; set; }
    }

    [EdiSegmentGroup("GRP")]
    public class Group
    {
        [EdiValue("X(35)", Path = "GRP/0")]
        public string Id { get; set; }

        public InGroup1 Element1 { get; set; }

        public InGroup2 Element2 { get; set; }
    }

    [EdiPath("IG1")]
    [EdiSegment]
    public class InGroup1
    {
        [EdiValue("X(35)", Path = "IG1/0")]
        public string Id { get; set; }
    }

    [EdiPath("IG2")]
    [EdiSegment]
    public class InGroup2
    {
        [EdiValue("X(35)", Path = "IG2/0")]
        public string Id { get; set; }
    }

    [EdiPath("AFT")]
    [EdiSegment]
    public class AfterGroup
    {
        [EdiValue("X(35)", Path = "AFT/0")]
        public string Id { get; set; }
    }
}
