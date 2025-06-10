using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class EDIFACT_ExplicitNesting_issue227
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        [EdiValue(Mandatory = true, Path = "TIT/0")]
        public string Title { get; set; }
        
        public List<Level1> L1s { get; set; }
    }

    [EdiSegment]
    public class Level1
    {
        [EdiValue(Mandatory = true, Path = "LE1/0")]
        public string Name { get; set; }
        
        public List<Level2> L2s { get; set; }
    }

    [EdiSegment]
    public class Level2
    {
        [EdiValue(Path = "LE2/0")]
        public string SubName { get; set; } 
    }
}
