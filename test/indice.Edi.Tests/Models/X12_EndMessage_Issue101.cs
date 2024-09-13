using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class Interchange_Issue101
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        [EdiValue("9(4)", Path = "ST/1")]
        public string HeaderControl { get; set; }
        
        public Return Return { get; set; }
        
        [EdiValue("9(4)", Path = "SE/1")]
        public string TrailerControl { get; set; }
    }
		
		[EdiSegmentGroup("TFS")]
		public class Return
		{
			public FormGroupSegment FormGroupSegment { get; set; }
		}

    [EdiSegmentGroup("FGS")]
    public class FormGroupSegment
    {
        public DateSegment DateSegment { get; set; }
        [EdiValue("X(5)", Path = "FGS/2")]
        public string Id { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DateSegment
    {
        [EdiValue("9(8)", Format = "yyyyMMdd", Path = "DTM/1")]
        public DateTime Date { get; set; }
    }
}
