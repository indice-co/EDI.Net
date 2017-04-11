using System.Collections.Generic;
using indice.Edi.Serialization;

[EdiPath("COM[0][0]"), EdiSegment]
public class COM
{
    [EdiValue("X(255)", Path = "COM/0/0")]
    public string Kommunikationsadresse {
        get; set;
    }

    [EdiValue("X(3)", Path = "COM/0/1")]
    public string Art {
        get; set;
    }
}

[EdiSegmentGroup("CTA[0][0]")]
public class CTA
{
    [EdiValue("X(3)", Path = "CTA/0/0")]
    public string Funktion {
        get; set;
    }

    [EdiValue("X(17)", Path = "CTA/1/0")]
    public string Kontaktnummer {
        get; set;
    }

    [EdiValue("X(0)", Path = "CTA/1/1")]
    public string Kontakt {
        get; set;
    }

    public List<COM> ListCOM {
        get; set;
    }
}

[EdiSegmentGroup("CUX[0][0]", SequenceEnd = "LIN[0][0]")]
public class CUX
{
    [EdiValue("X(3)", Path = "CUX/0/0")]
    public string Waehrungsangaben {
        get; set;
    }

    [EdiValue("X(3)", Path = "CUX/0/1")]
    public string Code {
        get; set;
    }

    [EdiValue("X(3)", Path = "CUX/0/2")]
    public string Qualifier {
        get; set;
    }
}

[EdiPath("DTM[0][0]"), EdiSegment]
public class DTM
{
    [EdiValue("X(3)", Path = "DTM/0/0")]
    public string Code {
        get; set;
    }

    [EdiValue("X(35)", Path = "DTM/0/1")]
    public string Datum {
        get; set;
    }

    [EdiValue("X(3)", Path = "DTM/0/2")]
    public string Format {
        get; set;
    }
}

[EdiPath("FTX[0][0]"), EdiSegment]
public class FTX
{
    [EdiValue("X(3)", Path = "FTX/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(35)", Path = "FTX/3/0")]
    public string Text1 {
        get; set;
    }

    [EdiValue("X(35)", Path = "FTX/3/1")]
    public string Text2 {
        get; set;
    }

    [EdiValue("X(35)", Path = "FTX/3/2")]
    public string Text3 {
        get; set;
    }

    [EdiValue("X(35)", Path = "FTX/3/3")]
    public string Text4 {
        get; set;
    }

    [EdiValue("X(35)", Path = "FTX/3/4")]
    public string Text5 {
        get; set;
    }
}

[EdiPath("IMD[0][0]"), EdiSegment]
public class IMD
{
    [EdiValue("X(3)", Path = "IMD/0/0")]
    public string Beschreibungsformat {
        get; set;
    }

    [EdiValue("X(3)", Path = "IMD/1/0")]
    public string Produkt {
        get; set;
    }

    [EdiValue("X(3)", Path = "IMD/2/0")]
    public string Code {
        get; set;
    }
}

[EdiSegmentGroup("LIN[0][0]", SequenceEnd = "UNS[0][0]")]
public class LIN
{
    [EdiValue("X(6)", Path = "LIN/0/0")]
    public string Positionsnummer {
        get; set;
    }

    [EdiValue("X(35)", Path = "LIN/2/0")]
    public string Leistungsnummer {
        get; set;
    }

    [EdiValue("X(3)", Path = "LIN/2/1")]
    public string Code {
        get; set;
    }

    [EdiCondition("145", Path = "QTY[0][0]")]
    public QTY Menge {
        get; set;
    }

    [EdiCondition("203", Path = "MOA[0][0]")]
    public MOA Positionsnettobetrag {
        get; set;
    }

    [EdiCondition("ACB", Path = "FTX[0][0]")]
    public FTX Allgemeine_Information {
        get; set;
    }

    [EdiCondition("CAL", Path = "PRI[0][0]")]
    public PRI Preisangaben {
        get; set;
    }

    [EdiCondition("Z09", Path = "RFF[0][0]")]
    public LIN_REF Geraetenummer {
        get; set;
    }

    [EdiCondition("Z06", Path = "RFF[0][0]")]
    public LIN_REF Positionsnummer_der_Bestellung {
        get; set;
    }
}

[EdiPath("RFF[0][0]"), EdiSegment]
public class LIN_REF
{
    [EdiValue("X(3)", Path = "RFF/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(70)", Path = "RFF/0/1")]
    public string Identifikation {
        get; set;
    }
}

[EdiPath("LOC[0][0]"), EdiSegment]
public class LOC
{
    [EdiValue("X(3)", Path = "LOC/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(35)", Path = "LOC/1/0")]
    public string Nummer {
        get; set;
    }
}

[EdiPath("MOA[0][0]"), EdiSegment]
public class MOA
{
    [EdiValue("X(3)", Path = "MOA/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(35)", Path = "MOA/0/1")]
    public string Geldbetrag {
        get; set;
    }
}

[EdiMessage]
public class Nachricht
{
    [EdiValue("X(70)", Path = "UNH/0/0")]
    public string Nachrichtenreferenznr {
        get; set;
    }

    public Nachrichten_Kopfsegment Nachrichten_Kopfsegment {
        get; set;
    }

    [EdiValue("X(3)", Path = "BGM/0/0")]
    public string Dokumentenname {
        get; set;
    }

    [EdiValue("X(70)", Path = "BGM/1/0")]
    public string Dokumentennummer {
        get; set;
    }

    [EdiCondition("137", Path = "DTM[0][0]")]
    public DTM Nachrichtendatum {
        get; set;
    }

    [EdiCondition("203", Path = "DTM[0][0]")]
    public DTM Ausführungsdatum {
        get; set;
    }

    [EdiCondition("Z02", Path = "DTM[0][0]")]
    public DTM Verschobener_Abmeldetermin {
        get; set;
    }

    public List<IMD> ListIMD {
        get; set;
    }

    [EdiCondition("ON", Path = "RFF[0][0]")]
    public RFF Referenz_der_Anfrage {
        get; set;
    }

    [EdiCondition("Z13", Path = "RFF[0][0]")]
    public RFF Pruefidentifikator {
        get; set;
    }

    [EdiValue("X(3)", Path = "AJT/0/0")]
    public string Antwortkategorie {
        get; set;
    }

    [EdiCondition("MR", Path = "NAD[0][0]")]
    public NAD Empfaenger {
        get; set;
    }

    [EdiCondition("MS", Path = "NAD[0][0]")]
    public NAD Absender {
        get; set;
    }

    [EdiCondition("DP", Path = "NAD[0][0]")]
    public NAD Lieferanschrift {
        get; set;
    }

    [EdiCondition("2", Path = "CUX[0][0]")]
    public CUX Waehrungsangaben {
        get; set;
    }

    public List<LIN> ListPositionsdaten {
        get; set;
    }

    [EdiCondition("24", Path = "MOA[0][0]")]
    public MOA Netto_Summenbetrag {
        get; set;
    }

    public UNT Nachrichten_Endsegment {
        get; set;
    }
}

[EdiElement, EdiPath("UNH[1][0]")]
public class Nachrichten_Kopfsegment
{
    [EdiValue("X(6)", Path = "UNH/1/0")]
    public string Nachrichtentyp {
        get; set;
    }

    [EdiValue("X(3)", Path = "UNH/1/1")]
    public string VersionTyp {
        get; set;
    }

    [EdiValue("X(3)", Path = "UNH/1/2")]
    public string Freigabenr {
        get; set;
    }

    [EdiValue("X(2)", Path = "UNH/1/3")]
    public string Verwaltende_Organisation {
        get; set;
    }

    [EdiValue("X(6)", Path = "UNH/1/4")]
    public string Versionnr {
        get; set;
    }
}

[EdiSegmentGroup("NAD[0][0]", SequenceEnd = "CUX[0][0]")]
public class NAD
{
    [EdiValue("X(3)", Path = "NAD/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(35)", Path = "NAD/1/0")]
    public string ID {
        get; set;
    }

    [EdiValue("X(3)", Path = "NAD/1/2")]
    public string Code {
        get; set;
    }

    public LOC LOC {
        get; set;
    }

    public CTA CTA {
        get; set;
    }
}

public class ORDRSP
{
    public List<Nachricht> ListNachricht {
        get; set;
    }
}

[EdiPath("PRI[0][0]"), EdiSegment]
public class PRI
{
    [EdiValue("X(3)", Path = "PRI/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(15)", Path = "PRI/0/1")]
    public string Betrag {
        get; set;
    }
}

[EdiPath("QTY[0][0]"), EdiSegment]
public class QTY
{
    [EdiValue("X(3)", Path = "QTY/0/0")]
    public string Qualifier {
        get; set;
    }

    [EdiValue("X(35)", Path = "QTY/0/1")]
    public string Menge {
        get; set;
    }

    [EdiValue("X(8)", Path = "QTY/0/2")]
    public string Code {
        get; set;
    }
}

[EdiSegmentGroup("RFF[0][0]", SequenceEnd = "AJT[0][0]")]
public class RFF
{
    [EdiValue("X(70)", Path = "RFF/0/0")]
    public string Code {
        get; set;
    }

    [EdiValue("X(3)", Path = "RFF/0/1")]
    public string Qualifier {
        get; set;
    }

    public DTM Referenz_Datum {
        get; set;
    }
}

[EdiPath("UNT[0][0]"), EdiSegment]
public class UNT
{
    [EdiValue("9(6)", Path = "UNT/0/0")]
    public int Anzahl_der_Segmente {
        get; set;
    }

    [EdiValue("X(14)", Path = "UNT/1/0")]
    public string Referenznummer {
        get; set;
    }
}