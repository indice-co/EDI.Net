using System.Collections.Generic;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

class Interchange_Issue174
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        public VehicleRecord VehicleRecord { get; set; }
    }

    [EdiElement]
    public class IdentificationNumber
    {
        [EdiValue("X(35)", Path = "*/*/0")]
        public string ObjectIdentifier { get; set; }

        [EdiValue("X(3)", Path = "*/*/1")]
        public string ObjectIdentificationCodeQualifier { get; set; }

        [EdiValue("X(3)", Path = "*/*/2")]
        public string StatusDescriptionCode { get; set; }

        public override string ToString() => $"{ObjectIdentifier}";
    }

    [EdiSegment, EdiPath("GIR")]
    public class RelatedIdentification
    {
        [EdiValue("X(3)", Path = "GIR/0/0")]
        public string SetIdentificationQualifier { get; set; }

        [EdiPath("GIR/1..*")]
        public List<IdentificationNumber> Numbers { get; set; }

        public override string ToString() => $"{SetIdentificationQualifier} ({Numbers.Count})";
    }

    [EdiSegment, EdiPath("GIN")]
    public class GIN
    {
        [EdiValue("X(3)", Path = "GIN/0/0")]
        public string IdentityNumberQualifier { get; set; }

        [EdiValue("X(17)", Path = "GIN/1/0")]
        public string VehicleIdentificationNo { get; set; }
    }

    [EdiSegment, EdiPath("RFF")]
    public class Reference
    {
        [EdiValue("X(3)", Path = "RFF/0/0")]
        public string CodeQualifier { get; set; }
        [EdiValue("X(70)", Path = "RFF/0/1")]
        public string Identifier { get; set; }
        [EdiValue("X(6)", Path = "RFF/0/2")]
        public string DocumentLineIdentifier { get; set; }
        [EdiValue("X(35)", Path = "RFF/0/3")]
        public string ReferenceVersionIdentifier { get; set; }
        [EdiValue("X(6)", Path = "RFF/0/4")]
        public string RevisionIdentifier { get; set; }

        public override string ToString() => $"{Identifier} {DocumentLineIdentifier} {ReferenceVersionIdentifier} {RevisionIdentifier}".Trim();
    }

    [EdiSegmentGroup("GIN")]
    public class VehicleRecord : GIN
    {
        public Reference RFF { get; set; }
        public List<RelatedIdentification> GIR_List { get; set; }
    }
}
