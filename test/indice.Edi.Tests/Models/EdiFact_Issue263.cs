using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;
public class Interchange_Issue263
{
    public GIN GIN_Segment { get; set; }

    [EdiSegment, EdiPath("GIN")]
    public class GIN
    {
        [EdiValue("X(3)", Path = "GIN/0/0")]
        public string Qualifier { get; set; }

        [EdiPath("GIN/1..*")]
        public List<IdentityNumber> IdentityNumbers { get; set; }

        [EdiElement]
        public class IdentityNumber
        {
            [EdiValue("X(35)", Path = "*/*/0")]
            public string Text { get; set; }

        }
    }
}
