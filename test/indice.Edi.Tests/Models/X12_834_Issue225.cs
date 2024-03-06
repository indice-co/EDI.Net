using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class X12_834_Issue225
    {
        public ISA ISA { get; set; }

        public List<FunctionalGroup> FunctionalGroups { get; set; }

        public IEA IEA { get; set; }
    }

    [EdiGroup]
    public class FunctionalGroup
    {
        public GS GS { get; set; }

        public List<EDI834> TransactionSetHeaders { get; set; }

        public GE GE { get; set; }
    }

    [EdiSegment, EdiPath("ISA")]
    public class ISA
    {
        [EdiValue("X(99)", Path = "ISA/0")]
        public string ISA_01 { get; set; }

        [EdiValue("X(99)", Path = "ISA/1")]
        public string ISA_02 { get; set; }

        [EdiValue("X(99)", Path = "ISA/2")]
        public string ISA_03 { get; set; }

        [EdiValue("X(99)", Path = "ISA/3")]
        public string ISA_04 { get; set; }

        [EdiValue("X(99)", Path = "ISA/4")]
        public string ISA_05 { get; set; }

        [EdiValue("X(99)", Path = "ISA/5")]
        public string ISA_06 { get; set; }

        [EdiValue("X(99)", Path = "ISA/6")]
        public string ISA_07 { get; set; }

        [EdiValue("X(99)", Path = "ISA/7")]
        public string ISA_08 { get; set; }

        [EdiValue("X(99)", Path = "ISA/8")]
        public string ISA_09 { get; set; }

        [EdiValue("X(99)", Path = "ISA/9")]
        public string ISA_10 { get; set; }
    }

    [EdiSegment, EdiPath("IEA")]
    public class IEA
    {
        [EdiValue("X(99)", Path = "IEA/0")]
        public string IEA_01 { get; set; }

        [EdiValue("X(99)", Path = "IEA/1")]
        public string IEA_02 { get; set; }
    }

    [EdiSegment, EdiPath("GS")]
    public class GS
    {
        [EdiValue("X(99)", Path = "GS/0")]
        public string GS_01 { get; set; }

        [EdiValue("X(99)", Path = "GS/1")]
        public string GS_02 { get; set; }

        [EdiValue("X(99)", Path = "GS/2")]
        public string GS_03 { get; set; }

        [EdiValue("X(99)", Path = "GS/3")]
        public string GS_04 { get; set; }

        [EdiValue("X(99)", Path = "GS/4")]
        public string GS_05 { get; set; }

        [EdiValue("X(99)", Path = "GS/5")]
        public string GS_06 { get; set; }

        [EdiValue("X(99)", Path = "GS/6")]
        public string GS_07 { get; set; }
    }

    [EdiSegment, EdiPath("GE")]
    public class GE
    {
        [EdiValue("X(99)", Path = "GE/0")]
        public string GE_01 { get; set; }

        [EdiValue("X(99)", Path = "GE/1")]
        public string GE_02 { get; set; }
    }

    [EdiMessage, EdiSegmentGroup("ST", SequenceEnd = "SE")]
    public class EDI834
    {
        public ST ST { get; set; }

        //public BGN BGN { get; set; }

        //public List<REF> REF { get; set; }

        //public List<DTP> DTP { get; set; }

        //public List<AMT> AMT { get; set; }

        //public List<QTY> QTY { get; set; }

        //public List<N1Loop1000> N1Loop { get; set; }

        public List<INSLoop2000> INSLoop { get; set; }

        public SE SE { get; set; }
    }

    [EdiSegment, EdiPath("ST")]
    public class ST
    {
        [EdiValue("X(99)", Path = "ST/0")]
        public string ST_01 { get; set; }

        [EdiValue("X(99)", Path = "ST/1")]
        public string ST_02 { get; set; }

        [EdiValue("X(99)", Path = "ST/2")]
        public string ST_03 { get; set; }
    }

    [EdiSegment, EdiPath("SE")]
    public class SE
    {
        [EdiValue("X(99)", Path = "SE/0")]
        [EdiCount(EdiCountScope.Segments)]
        public string SE_01 { get; set; }

        [EdiValue("X(99)", Path = "SE/1")]
        public string SE_02 { get; set; }
    }

    [EdiSegmentGroup("INS", new[] { "REF", "DTP", "NM1", "DSB", "HD" }, SequenceEnd = "INS")]
    public class INSLoop2000 : INS
    {
        public List<REF> REF { get; set; }

        //public List<DTP> DTP { get; set; }

        public List<NM1Loop2100> NM1Loop { get; set; }

        //public List<DSBLoop2200> DSBLoop { get; set; }
        //public List<HDLoop2300> HDLoop { get; set; }
        //public List<LCLoop2400> LCLoop { get; set; }
        //public List<FSALoop2500> FCALoop { get; set; }
        //public List<RPLoop2600> RPLoop { get; set; }
    }

    [EdiSegmentGroup("NM1", new[] { "PER", "N3", "N4", "DMG", "PM", "EC", "ICM", "AMT", "HLH", "HI", "LUI" })]
    public class NM1Loop2100 : NM1
    {
        public PER PER { get; set; }
        //public N3 N3 { get; set; }
        //public N4 N4 { get; set; }
        //public DMG DMG { get; set; }
        //public PM PM { get; set; }
        //public List<EC> EC { get; set; }
        //public ICM ICM { get; set; }
        //public List<AMT> AMT { get; set; }
        //public HLH HLH { get; set; }
        //public List<HI> HI { get; set; }
        //public List<LUI> LUI { get; set; }

    }

    [EdiSegment, EdiPath("NM1")]
    public class NM1
    {
        [EdiValue("X(99)", Path = "NM1/0")]
        public string NM1_01 { get; set; }

        [EdiValue("X(99)", Path = "NM1/1")]
        public string NM1_02 { get; set; }

        [EdiValue("X(99)", Path = "NM1/2")]
        public string NM1_03 { get; set; }

        [EdiValue("X(99)", Path = "NM1/3")]
        public string NM1_04 { get; set; }

        [EdiValue("X(99)", Path = "NM1/4")]
        public string NM1_05 { get; set; }

        [EdiValue("X(99)", Path = "NM1/5")]
        public string NM1_06 { get; set; }

        [EdiValue("X(99)", Path = "NM1/6")]
        public string NM1_07 { get; set; }

        [EdiValue("X(99)", Path = "NM1/7")]
        public string NM1_08 { get; set; }

        [EdiValue("X(99)", Path = "NM1/8")]
        public string NM1_09 { get; set; }

        [EdiValue("X(99)", Path = "NM1/9")]
        public string NM1_10 { get; set; }

        [EdiValue("X(99)", Path = "NM1/10")]
        public string NM1_11 { get; set; }

        [EdiValue("X(99)", Path = "NM1/11")]
        public string NM1_12 { get; set; }
    }

    [EdiSegment, EdiPath("PER")]
    public class PER
    {
        [EdiValue("X(99)", Path = "PER/0")]
        public string PER_01 { get; set; }

        [EdiValue("X(99)", Path = "PER/1")]
        public string PER_02 { get; set; }

        [EdiValue("X(99)", Path = "PER/2")]
        public string PER_03 { get; set; }

        [EdiValue("X(99)", Path = "PER/3")]
        public string PER_04 { get; set; }

        [EdiValue("X(99)", Path = "PER/4")]
        public string PER_05 { get; set; }

        [EdiValue("X(99)", Path = "PER/5")]
        public string PER_06 { get; set; }

        [EdiValue("X(99)", Path = "PER/6")]
        public string PER_07 { get; set; }

        [EdiValue("X(99)", Path = "PER/7")]
        public string PER_08 { get; set; }

        [EdiValue("X(99)", Path = "PER/8")]
        public string PER_09 { get; set; }
    }

    [EdiSegment, EdiPath("REF")]
    public class REF
    {
        [EdiValue("X(99)", Path = "REF/0")]
        public string REF_01 { get; set; }

        [EdiValue("X(99)", Path = "REF/1")]
        public string REF_02 { get; set; }

        [EdiValue("X(99)", Path = "REF/2")]
        public string REF_03 { get; set; }

        [EdiValue("X(99)", Path = "REF/3")]
        public string REF_04 { get; set; }
    }

    [EdiSegment, EdiPath("INS")]
    public class INS
    {
        [EdiValue("X(99)", Path = "INS/0")]
        public string INS_01 { get; set; }

        [EdiValue("X(99)", Path = "INS/1")]
        public string INS_02 { get; set; }

        [EdiValue("X(99)", Path = "INS/2")]
        public string INS_03 { get; set; }

        [EdiValue("X(99)", Path = "INS/3")]
        public string INS_04 { get; set; }

        [EdiValue("X(99)", Path = "INS/4")]
        public string INS_05 { get; set; }

        [EdiValue("X(99)", Path = "INS/5")]
        public string INS_06 { get; set; }

        [EdiValue("X(99)", Path = "INS/6")]
        public string INS_07 { get; set; }

        [EdiValue("X(99)", Path = "INS/7")]
        public string INS_08 { get; set; }

        [EdiValue("X(99)", Path = "INS/8")]
        public string INS_09 { get; set; }

        [EdiValue("X(99)", Path = "INS/9")]
        public string INS_10 { get; set; }

        [EdiValue("X(99)", Path = "INS/10")]
        public string INS_11 { get; set; }

        [EdiValue("X(99)", Path = "INS/11")]
        public string INS_12 { get; set; }

        [EdiValue("X(99)", Path = "INS/12")]
        public string INS_13 { get; set; }

        [EdiValue("X(99)", Path = "INS/13")]
        public string INS_14 { get; set; }

        [EdiValue("X(99)", Path = "INS/14")]
        public string INS_15 { get; set; }

        [EdiValue("X(99)", Path = "INS/15")]
        public string INS_16 { get; set; }

        [EdiValue("X(99)", Path = "INS/16")]
        public string INS_17 { get; set; }

        [EdiValue("X(99)", Path = "INS/17")]
        public string INS_18 { get; set; }

        [EdiValue("X(99)", Path = "INS/18")]
        public string INS_19 { get; set; }
    }
}
