using indice.Edi.Serialization;
using System;

namespace indice.Edi.Tests.Models
{
    public class Interchange_D95B_CUSCAR
    {
        #region Interchange Header

        [EdiValue(Path = "UNB/0")]
        public string Header_Field1a { get; set; }

        [EdiValue(Path = "UNB/0/1")]
        public string Header_Field1b { get; set; }

        [EdiValue(Path = "UNB/1")]
        public string Header_Field2 { get; set; }

        [EdiValue(Path = "UNB/2")]
        public string Header_Field3 { get; set; }

        [EdiValue(Path = "UNB/3/0")]
        public string Header_Field4a { get; set; }

        [EdiValue(Path = "UNB/3/1")]
        public string Header_Field4b { get; set; }

        [EdiValue(Path = "UNB/4")]
        public string Header_Field5 { get; set; }

        #endregion Interchange Header




        public Message_D95B Message { get; set; }
        




        #region Interchange Trailer

        [EdiValue(Path = "UNZ/0")]
        public int Trailer_Field1 { get; set; }

        [EdiValue(Path = "UNZ/1")]
        public string Trailer_Field2 { get; set; }

        #endregion Interchange Trailer
    }

    [EdiMessage]
    public class Message_D95B
    {
        #region Message Header Info

        [EdiValue(Path = "UNH/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "UNH/1/0")]
        public string Field2a { get; set; }

        [EdiValue(Path = "UNH/1/1")]
        public string Field2b { get; set; }

        [EdiValue(Path = "UNH/1/2")]
        public string Field2c { get; set; }

        [EdiValue(Path = "UNH/1/3")]
        public string Field2d { get; set; }

        #endregion Message Header Info


        public BGM BGM { get; set; }
        public DTM DTM { get; set; }
        public NAD NAD { get; set; }


        public TDT_Group1[] TDT_Group1 { get; set; }
        public CNI_Group4[] CNI_Group4 { get; set; }
    }





    #region EdiSegmentGroups

    [EdiSegmentGroup("TDT", SequenceEnd = "CNI")]
    public class TDT_Group1
    {
        [EdiValue(Path = "TDT/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "TDT/1")]
        public string Field2 { get; set; }

        [EdiValue(Path = "TDT/2")]
        public string Field3 { get; set; }

        [EdiValue(Path = "TDT/3")]
        public string Field4 { get; set; }

        [EdiValue(Path = "TDT/4/0")]
        public string Field5a { get; set; }

        [EdiValue(Path = "TDT/4/1")]
        public string Field5b { get; set; }

        [EdiValue(Path = "TDT/4/2")]
        public string Field5c { get; set; }

        [EdiValue(Path = "TDT/4/3")]
        public string Field5d { get; set; }

        [EdiValue(Path = "TDT/5")]
        public string Field6 { get; set; }

        [EdiValue(Path = "TDT/6/0")]
        public string Field7a { get; set; }

        [EdiValue(Path = "TDT/6/1")]
        public string Field7b { get; set; }

        [EdiValue(Path = "TDT/6/2")]
        public string Field7c { get; set; }

        [EdiValue(Path = "TDT/7/0")]
        public string Field8a { get; set; }

        [EdiValue(Path = "TDT/7/1")]
        public string Field8b { get; set; }

        [EdiValue(Path = "TDT/7/2")]
        public string Field8c { get; set; }

        [EdiValue(Path = "TDT/7/3")]
        public string Field8d { get; set; }

        [EdiValue(Path = "TDT/7/4")]
        public string Field8e { get; set; }



        public LOC[] LOC { get; set; }

        public DTM[] DTM { get; set; }
    }
    
    /*
      You cannot use a class CNI e.g.

      [EdiSegment, EdiPath("CNI")]
      public class CNI
      {
            [EdiValue(Path = "CNI/0")]
            public string Field1 { get; set; }
            
            [EdiValue(Path = "CNI/1")]
            public string Field2 { get; set; }
      }

      and use this class inside the CNI_Group4
    */
    [EdiSegmentGroup("CNI", SequenceEnd = "UNT")]
    public class CNI_Group4
    {
        /*
         public CNI CNI_Segment { get; set; } <-- This is invalid
         
         The CNI_Group4 is not a stand alone segment like e.g. BMG segment. 
         It is a group and more specifically it is the start of the group of segments, so you declare properties inside THIS (CNI_Group4) class and use EdiValue to point the data from the file 
        */
        [EdiValue(Path = "CNI/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "CNI/1")]
        public string Field2 { get; set; }

        public RFF_Group5[] RFF_Group5 { get; set; }
    }

    [EdiSegmentGroup("RFF", SequenceEnd = "UNT")]
    public class RFF_Group5
    {
        [EdiValue(Path = "RFF/0")] //this is equivalent [EdiValue(Path = "RFF/0/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "RFF/0/1")]
        public string Field2 { get; set; }

        public LOC[] LOC { get; set; }
        public NAD[] NAD { get; set; }
        public GID_Group10[] GID_Group10 { get; set; }
    }
    
    [EdiSegmentGroup("GID", SequenceEnd = "SPG")]
    public class GID_Group10
    {
        [EdiValue(Path = "GID/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "GID/1")]
        public string Field2a { get; set; }

        [EdiValue(Path = "GID/1/1")]
        public string Field2b { get; set; }

        public FTX FTX { get; set; }
        public MEA[] MEA { get; set; }
        public SGP SPG { get; set; }
    }

    #endregion





    #region Segments

    [EdiSegment, EdiPath("BGM")]
    public class BGM
    {
        [EdiValue(Path = "BGM/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "BGM/1")]
        public string Field2 { get; set; }

        [EdiValue(Path = "BGM/2")]
        public string Field3 { get; set; }
    }

    [EdiSegment, EdiPath("DTM")]
    public class DTM
    {
        [EdiValue(Path = "DTM/0/0")]
        public string Field1a { get; set; }

        [EdiValue(Path = "DTM/0/1")]
        public string Field1b { get; set; }

        [EdiValue(Path = "DTM/0/2")]
        public string Field1c { get; set; }

        public override string ToString() {
            var year = Field1b.Substring(0, 4);
            var month = Field1b.Substring(4, 2);
            var day = Field1b.Substring(6, 2);

            var dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));

            return dt.ToString("d");
        }
    }

    [EdiSegment, EdiPath("NAD")]
    public class NAD
    {

        [EdiValue(Path = "NAD/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "NAD/1/0")]
        public string Field2a { get; set; }

        [EdiValue(Path = "NAD/1/1")]
        public string Field2b { get; set; }

        [EdiValue(Path = "NAD/1/2")]
        public string Field2c { get; set; }

        [EdiValue(Path = "NAD/2")]
        public string Field3 { get; set; }
    }
    
    [EdiSegment, EdiPath("LOC")]
    public class LOC
    {
        [EdiValue, EdiPath("LOC/0")]
        public string Field1 { get; set; }

        [EdiValue, EdiPath("LOC/1/0")]
        public string Field2a { get; set; }

        [EdiValue, EdiPath("LOC/1/1")]
        public string Field2b { get; set; }

        [EdiValue, EdiPath("LOC/1/2")]
        public string Field2c { get; set; }

        [EdiValue, EdiPath("LOC/1/3")]
        public string Field2d { get; set; }
    }

    [EdiSegment, EdiPath("FTX")]
    public class FTX
    {
        [EdiValue(Path = "FTX/0")]
        public string Field1 { get; set; }
    }

    [EdiSegment, EdiPath("MEA")]
    public class MEA
    {

        [EdiValue(Path = "MEA/0")]
        public string Field1 { get; set; }

        [EdiValue(Path = "MEA/1")]
        public string Field2 { get; set; }

        [EdiValue(Path = "MEA/2/0")]
        public string Field3a { get; set; }

        [EdiValue(Path = "MEA/2/1")]
        public string Field3b { get; set; }
    }

    [EdiSegment, EdiPath("SGP")]
    public class SGP
    {
        [EdiValue(Path = "SGP/0")]
        public string Field1 { get; set; }
    }

    #endregion

}
