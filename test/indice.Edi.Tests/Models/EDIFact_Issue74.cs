using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class Interchange_Issue74
{

    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        public TSR_Segment TSR { get; set; }
    }

    [EdiSegment, EdiPath("TSR")]
    public class TSR_Segment
    {
        //[EdiValue("X(3)", Path = "TSR/0/0")]
        //public string ContractAndCarriageConditionCode { get; set; }

        //[EdiValue("X(3)", Path = "TSR/1/0")]
        //public string CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Mandatory = true, Path = "TSR/2/0")]
        public string ServiceCode { get; set; }

        public override string ToString() {
            return ServiceCode;
        }
    }
}
