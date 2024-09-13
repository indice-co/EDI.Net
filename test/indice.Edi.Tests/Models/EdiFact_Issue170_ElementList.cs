using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class EdiFact_Issue170_ElementList
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        public List<InteractiveFreeText> InteractiveFreeTexts { get; set; }
    }

    [EdiSegment, EdiPath("IFT")]
    public class InteractiveFreeText
    {
        [EdiValue("X(3)", Mandatory = true, Description = "C346-4451", Path = "IFT/0")]
        public string Code { get; set; }

        [EdiValue("X(4)", Description = "C346-9980", Path = "IFT/0/1")]
        public string CodeId { get; set; }

        [EdiValue("X(3)", Description = "C346-4405", Path = "IFT/0/2")]
        public string Fare { get; set; }

        public List<FreeText> Texts { get; set; }

        public override string ToString() => string.Join(" ", Texts);
    }

    [EdiElement, EdiPath("IFT/1..*")] // This take only index [1], but not from [1..*]]
    public class FreeText
    {
        [EdiValue("X(70)", Path = "IFT/*/0")]
        public string Text { get; set; }

        public override string ToString() => Text;
    }
}
