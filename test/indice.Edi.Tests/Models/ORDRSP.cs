#region Using

using System.Collections.Generic;

using indice.Edi.Serialization;

#endregion

namespace indice.Edi.Tests.Models;

public class ORDRSP
{
    private List<Nachricht> _listnachricht;

    #region Properties

    public List<Nachricht> ListNachricht
    {
        get { return _listnachricht; }
        set { _listnachricht = value; }
    }

    #endregion

    #region Nested type: CTA

    [EdiPath("CTA/0/0")]
    [EdiSegment]
    public class CTA
    {
        private string _funktion;

        private string _kontakt;

        private string _kontaktnummer;

        #region Properties

        [EdiValue("X(3)")]
        [EdiPath("CTA/0/0")]
        public string Funktion
        {
            get { return _funktion; }
            set { _funktion = value; }
        }

        [EdiValue("X(17)")]
        [EdiPath("CTA/1/0")]
        public string Kontaktnummer
        {
            get { return _kontaktnummer; }
            set { _kontaktnummer = value; }
        }

        [EdiValue("X(0)")]
        [EdiPath("CTA/1/1")]
        public string Kontakt
        {
            get { return _kontakt; }
            set { _kontakt = value; }
        }

        #endregion
    }

    #endregion

    #region Nested type: DTM

    [EdiSegment]
    [EdiPath("DTM/0/0")]
    public class DTM
    {
        private string _code;

        private string _datum;

        private string _format;

        #region Properties

        [EdiValue("X(3)", Path = "DTM/0/0")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [EdiValue("X(35)", Path = "DTM/0/1")]
        public string Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        [EdiValue("X(3)", Path = "DTM/0/2")]
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        #endregion
    }

    #endregion

    #region Nested type: Nachricht

    [EdiMessage]
    public class Nachricht
    {
        private NAD _absender;
        private NAD _empfaenger;

        #region Properties

        [EdiCondition("MR", Path = "NAD/0/0")]
        public NAD Empfaenger
        {
            get { return _empfaenger; }
            set { _empfaenger = value; }
        }

        [EdiCondition("MS", Path="NAD/0/0")]
        public NAD Absender
        {
            get { return _absender; }
            set { _absender = value; }
        }

        [EdiCondition("ON", Path = "RFF/0/0")]
        public SG1 Referenz_der_Anfrage { get; set; }

        #endregion
    }

    #endregion

    #region Nested type: NAD

    [EdiSegmentGroup("NAD/0/0")]
    public class NAD
    {
        private string _code;

        private CTA _cta;

        private string _id;
        private string _qualifier;

        #region Properties

        [EdiValue("X(3)", Path = "NAD/0/0")]
        public string Qualifier
        {
            get { return _qualifier; }
            set { _qualifier = value; }
        }

        [EdiValue("X(35)", Path="NAD/1/0")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [EdiValue("X(3)", Path= "NAD/1/2")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public CTA CTA
        {
            get { return _cta; }
            set { _cta = value; }
        }

        #endregion
    }

    #endregion

    #region Nested type: RFF

    [EdiSegment]
    [EdiPath("RFF/0/0")]
    public class RFF
    {
        private string _code;

        private string _qualifier;

        #region Properties

        [EdiValue("X(70)", Path = "RFF/0/0")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [EdiValue("X(3)", Path = "RFF/0/1")]
        public string Qualifier
        {
            get { return _qualifier; }
            set { _qualifier = value; }
        }

        #endregion
    }

    #endregion

    #region Nested type: SG1

    [EdiSegmentGroup("RFF", SequenceEnd = "AJT")]
    public class SG1
    {
        private DTM _referenzDatum;

        private string _code;

        private string _qualifier;

        #region Properties

        [EdiValue("X(70)", Path = "RFF/0/0")]
        public string Code {
            get { return _code; }
            set { _code = value; }
        }

        [EdiValue("X(3)", Path = "RFF/0/1")]
        public string Qualifier {
            get { return _qualifier; }
            set { _qualifier = value; }
        }

        public DTM Referenz_der_Anfragedatum
        {
            get { return _referenzDatum; }
            set { _referenzDatum = value; }
        }

        #endregion
    }

    #endregion
}