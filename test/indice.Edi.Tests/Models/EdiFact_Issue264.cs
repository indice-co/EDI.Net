using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class Interchange_Issue264
{
    public Message Msg { get; set; }

    [EdiMessage]
    [EdiSegment, EdiPath("IFT")]
    public class Message
    {
        [EdiValue("X(3)", Mandatory = true, Path = "IFT/0")]
        public string Code { get; set; }

        [EdiValue("X(10)", Path = "IFT/0/1")]
        public DocumentType DocumentType { get; set; }
    }

    public enum DocumentType
    {
        Invoice = 1,
        CreditNote = 2,
        DebitNote = 3
    }
}
