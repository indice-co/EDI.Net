using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class Interchange_Issue172
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        public List<HighLevelName> HighLevelNames { get; set; }
    }


    [EdiSegmentGroup("AA", "BB"), EdiCondition("1", Path = "AA/0")]
    public class HighLevelName : AA
    {
        public BB OtherName { get; set; }
        public List<MidLevelName> MidLevelNames { get; set; }
    }

    [EdiSegmentGroup("AA", "BB"), EdiCondition("2", Path = "AA/0")]
    public class MidLevelName : AA
    {
        public BB OtherName { get; set; }
        public List<LowLevelName> LowLevelNames { get; set; }
    }

    [EdiSegmentGroup("AA", "BB"), EdiCondition("3", Path = "AA/0")]
    public class LowLevelName : AA
    {
        public BB OtherName { get; set; }
    }

    [EdiSegment, EdiPath("AA")]
    public class AA
    {
        [EdiValue("X(3)", Path = "AA/0", Description = "AA01")]
        public string TypeCode { get; set; }

        [EdiValue("X(80)", Path = "AA/1", Description = "AA02")]
        public string Name { get; set; }
    }

    [EdiSegment, EdiPath("BB")]
    public class BB
    {
        [EdiValue("X(80)", Path = "BB/0", Description = "BB01")]
        public string OtherName { get; set; }
    }

}
