using System;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class Interchange_Issue130
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        [EdiValue("9(8)", Path = "DTM/1", Format = "yyyyMMdd", Description = "DTM02 - Date (Format = CCYYMMDD)")]
        [EdiValue("9(8)", Path = "DTM/2", Format = "HHmmssff", Description = "DTM03 - Time (Format = HHmmssff)")]
        public DateTime DateTime { get; set; }
    }
}
