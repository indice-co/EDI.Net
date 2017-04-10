#region Using

using System.Collections.Generic;

using indice.Edi.Serialization;

#endregion Using

namespace indice.Edi.Tests.Models
{
    public class ORDRSP
    {
        private List<Nachricht> _listnachricht;

        public List<Nachricht> ListNachricht { get; set; }

        [EdiSegmentGroup("CTA")]
        public class CTA
        {
            [EdiValue("X(3)")]
            [EdiPath("CTA/0/0")]
            public string Funktion { get; set; }

            [EdiValue("X(17)")]
            [EdiPath("CTA/1/0")]
            public string Kontaktnummer { get; set; }

            [EdiValue("X(255)")]
            [EdiPath("CTA/1/1")]
            public string Kontakt { get; set; }

            //public List<COM> Com { get; set; }

            [EdiCondition("TE", Path = "COM/0/1")]
            public COM Telefon { get; set; }

            [EdiCondition("EM", Path = "COM/0/1")]
            public COM EM { get; set; }
        }

        [EdiSegment]
        [EdiPath("COM")]
        public class COM
        {
            [EdiValue("X(255)")]
            [EdiPath("COM/0/0")]
            public string Kommunikationsverbindung { get; set; }

            [EdiValue("X(3)")]
            [EdiPath("COM/0/1")]
            public string Art { get; set; }
        }

        [EdiSegment]
        [EdiPath("DTM")]
        public class DTM
        {
            [EdiValue("X(3)", Path = "DTM/0/0")]
            public string Code { get; set; }

            [EdiValue("X(35)", Path = "DTM/0/1")]
            public string Datum { get; set; }

            [EdiValue("X(3)", Path = "DTM/0/2")]
            public string Format { get; set; }
        }

        [EdiMessage]
        public class Nachricht
        {
            [EdiCondition("MR", Path = "NAD/0/0")]
            public NAD Empfaenger { get; set; }

            [EdiCondition("MS", Path = "NAD/0/0")]
            public NAD Absender { get; set; }

            [EdiCondition("ON", Path = "RFF/0/0")]
            public RFF Referenz_der_Anfrage { get; set; }
        }

        [EdiSegmentGroup("NAD")]
        public class NAD
        {
            [EdiValue("X(3)", Path = "NAD/0/0")]
            public string Qualifier { get; set; }

            [EdiValue("X(35)", Path = "NAD/1/0")]
            public string ID { get; set; }

            [EdiValue("X(3)", Path = "NAD/1/2")]
            public string Code { get; set; }

            public CTA CTA { get; set; }
        }

        [EdiSegmentGroup("RFF")]
        public class RFF
        {
            [EdiValue("X(70)", Path = "RFF/0/0")]
            public string Code { get; set; }

            [EdiValue("X(3)", Path = "RFF/0/1")]
            public string Qualifier { get; set; }

            public DTM Referenz_der_Anfragedatum { get; set; }
        }
    }
}