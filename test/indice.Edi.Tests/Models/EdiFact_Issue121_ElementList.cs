using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

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

public class EdiFact_Issue121_ElementList_Conditions
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        [EdiCondition("NEW", CheckFor = EdiConditionCheckType.Equal, Path = "ATR/0/0")]
        public List<Criteria> Attributes { get; set; }

        [EdiCondition("V", CheckFor = EdiConditionCheckType.Equal, Path = "ATR/1/0")]
        public List<Criteria> Attributes1 { get; set; }
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

    [EdiSegmentGroup("ATR", "LTS")]
    public class Criteria : Attribute
    {
        public LTS LTS { get; set; }
    }

    [EdiSegment, EdiPath("LTS")]
    public class LTS
    {
        [EdiValue("X(100)", Path = "LTS/0", Mandatory = true)]
        public string Value { get; set; }
    }
}
