using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;


public class Interchange_Issue188
{
    public IList<IFTMINMessage> IftminMessages { get; } = new List<IFTMINMessage>();

    public IList<INVOICMessage> InvoiceMessages { get; } = new List<INVOICMessage>();


    [EdiCondition("IFTMIN", Path = "UNH/2/0")]
    [EdiMessage]
    public class IFTMINMessage
    {
        public IList<DTM> DTMs { get; } = new List<DTM>();
    }

    [EdiCondition("INVOIC", Path = "UNH/2/0")]
    [EdiMessage]
    public class INVOICMessage
    {
        public IList<DTM> DTMs { get; } = new List<DTM>();
    }

    [EdiSegment, EdiPath("DTM")]
    public class DTM
    {
        [EdiValue("9(3)", Path = "DTM/0/0")] public string Qualifier { get; set; }

        [EdiValue("X(35)", Path = "DTM/0/1")] public string Value { get; set; }

        [EdiValue("9(3)", Path = "DTM/0/2")] public string FormatQualifier { get; set; }
    }
}