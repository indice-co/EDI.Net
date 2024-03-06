using System.Collections.Generic;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class EdiFact_Issue223
    {
        public class Message
        {
            public UNB Header { get; set; }

            public MessageSection MessageSection { get; set; }

            public UNZ Footer { get; set; }
        }

        // The only difference between the two Message classes is that there is only one Attribute
        // in the SegmentGroup starting at line 134
        public class MessageWithSinglePathValue
        {
            public UNB Header { get; set; }

            public MessageSectionWithSinglePathValue MessageSection { get; set; }

            public UNZ Footer { get; set; }
        }
    }

    [EdiSegment(Mandatory = true), EdiPath("UNB")]
    public class UNB
    {
    }

    [EdiSegment(Mandatory = true), EdiPath("UNZ")]
    public class UNZ
    {
        [EdiValue("9(6)", Path = "UNZ/0", Mandatory = true)]
        public int Counter { get; set; }
    }

    [EdiSegment(Mandatory = true), EdiPath("UNH")]
    public class UNH
    {
    }

    [EdiSegment(Mandatory = true), EdiPath("UNT")]
    public class UNT
    {
    }

    [EdiMessage]
    public class MessageSection
    {
        public MessageSection() {
            Header = new UNH();
            MessageStart = new BGM();
            Footer = new UNT();
        }

        public UNH Header { get; set; }

        public BGM MessageStart { get; set; }

        public List<IDE> Transactions { get; set; }

        public UNT Footer { get; set; }
    }

    [EdiMessage]
    public class MessageSectionWithSinglePathValue
    {
        public MessageSectionWithSinglePathValue() {
            Header = new UNH();
            MessageStart = new BGM();
            Footer = new UNT();
        }

        public UNH Header { get; set; }

        public BGM MessageStart { get; set; }

        public List<IDEWithSinglePathValue> Transactions { get; set; }

        public UNT Footer { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("IDE")]
    public class IDE
    {
        [EdiCondition("Z02", Path = "SEQ/0/0")]
        public List<Z02SegmentGroup> Z02Segment { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("IDE")]
    public class IDEWithSinglePathValue
    {
        [EdiCondition("Z02", Path = "SEQ/0/0")]
        public List<Z02SegmentGroupWithSinglePathValue> Z02Segment { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("SEQ")]
    public class Z02SegmentGroup
    {
        public Z02SegmentGroup() {
            SequenceQualifier = "Z02";
        }

        [EdiValue("X(3)", Mandatory = true, Path = "SEQ/0/0")]
        public string SequenceQualifier { get; set; }

        [EdiCondition("Z17", Path = "CCI/0/0")]
        public CCI_CAV_SegmentGroup Attribute1 { get; set; }

        [EdiCondition("ZA8", Path = "CCI/2/0")]
        public CCI_CAV_SegmentGroup Atrribute2 { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("SEQ")]
    public class Z02SegmentGroupWithSinglePathValue
    {
        public Z02SegmentGroupWithSinglePathValue() {
            SequenceQualifier = "Z02";
        }

        [EdiValue("X(3)", Mandatory = true, Path = "SEQ/0/0")]
        public string SequenceQualifier { get; set; }

        [EdiCondition("Z17", Path = "CCI/0/0")]
        public CCI_CAV_SegmentGroup Attribute1 { get; set; }
    }

    [EdiSegment, EdiSegmentGroup("CCI")]
    public class CCI_CAV_SegmentGroup
    {
        [EdiValue("X(3)", Mandatory = false, Path = "CCI/0/0")]
        public string CCIAttribute1 { get; set; }
        [EdiValue("X(3)", Mandatory = false, Path = "CCI/1/0")]
        public string CCIAttribute2 { get; set; }
        [EdiValue("X(17)", Mandatory = false, Path = "CCI/2/0")]
        public string CCIAttribute3 { get; set; }
        [EdiValue("X(17)", Mandatory = false, Path = "CCI/2/1")]
        public string CCIAttribute4 { get; set; }
        [EdiValue("X(3)", Mandatory = false, Path = "CCI/2/2")]
        public string CCIAttribute5 { get; set; }
        [EdiValue("X(35)", Mandatory = false, Path = "CCI/2/3")]
        public string CCIAttribute6 { get; set; }
        public List<CAV_Complete> CAVAttributes { get; set; }
    }

    [EdiSegment, EdiPath("CAV")]
    public class CAV_Complete
    {
        [EdiValue("X(3)", Mandatory = true, Path = "CAV/0/0")]
        public string CAVAttribute1 { get; set; }

        [EdiValue("X(17)", Mandatory = false, Path = "CAV/0/1")]
        public string CAVAttribute2 { get; set; }

        [EdiValue("X(3)", Mandatory = false, Path = "CAV/0/2")]
        public string CAVAttribute3 { get; set; }

        [EdiValue("X(35)", Mandatory = false, Path = "CAV/0/3")]
        public string CAVAttribute4 { get; set; }

        [EdiValue("X(35)", Mandatory = false, Path = "CAV/0/4")]
        public string CAVAttribute5 { get; set; }
    }
}
