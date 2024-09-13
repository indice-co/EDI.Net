using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

class EdiPaiement_Issue139
{
    public Segment_UNB UNB { get; set; }
    public List<GroupeFonctionnel> GroupesFonctionnels { get; set; }
    public Segment_UNZ UNZ { get; set; }

    public EdiPaiement_Issue139() {
        UNB = new Segment_UNB();
        GroupesFonctionnels = new List<GroupeFonctionnel>();
        UNZ = new Segment_UNZ();
    }

    [EdiGroup]
    public class GroupeFonctionnel
    {
        public Segment_UNG UNG { get; set; }
        public List<Message> Messages { get; set; }
        public Segment_UNE UNE { get; set; }

        public GroupeFonctionnel() {
            UNG = new Segment_UNG();
            Messages = new List<Message>();
            UNE = new Segment_UNE();
        }
    }

    [EdiSegmentGroup("NAD", "RFF")]
    public class Intervenant : Segment_NAD
    {
        [EdiCondition("AWR", "ACD", Path = "RFF/0/0")]
        public Segment_RFF Reference { get; set; }
        public Intervenant() {
            //Reference = new Segment_RFF();
        }
    }

    [EdiMessage]
    public class Message
    {
        public Segment_UNH UNH { get; set; }
        public Segment_BGM BGM { get; set; }
        [EdiCondition("242", Path = "DTM/0/0")]
        public Segment_DTM DatePreparation { get; set; }

        #region Programme émetteur
        [EdiCondition("AUM", Path = "RFF/0/0")]
        public Segment_RFF NomEditeurProgEmetteur { get; set; }
        [EdiCondition("AUN", Path = "RFF/0/0")]
        public Segment_RFF NomProgEmetteur { get; set; }
        [EdiCondition("AUO", Path = "RFF/0/0")]
        public Segment_RFF NumAttestationEmetteur { get; set; }
        #endregion

        public List<Intervenant> Intervenants { get; set; }

        public Segment_UNT UNT { get; set; }
        public Message() {
            UNH = new Segment_UNH();
            BGM = new Segment_BGM();
            DatePreparation = new Segment_DTM();
            NomEditeurProgEmetteur = new Segment_RFF();
            NomProgEmetteur = new Segment_RFF();
            NumAttestationEmetteur = new Segment_RFF();
            Intervenants = new List<Intervenant>();
            UNT = new Segment_UNT();
        }
    }
    // En-tête d'interchange
    [EdiSegment, EdiPath("UNB")]
    public class Segment_UNB
    {
        // S001 : IDENTIFIANT DE SYNTAXE
        [EdiValue("X(4)", Path = "UNB/0/0", Mandatory = true)]
        public string S001_0001_1_IdentifiantSyntaxe { get; set; }
        [EdiValue("9(1)", Path = "UNB/0/1", Mandatory = true)]
        public int S001_0002_1_NumeroVersionSyntaxe { get; set; }

        // S002 : EMETTEUR DE L'INTERCHANGE
        [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
        public string S002_0004_1_IdentificationEmetteur { get; set; }
        [EdiValue("X(4)", Path = "UNB/1/1", Mandatory = true)]
        public string S002_0007_1_QualifiantIdentification { get; set; }
        [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
        public string S002_0008_1_AdresseAcheminementRetour { get; set; }

        // S003 : RECEPTEUR DE L'INTERCHANGE
        [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
        public string S003_0010_1_IdentificationDestinataire { get; set; }
        [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = true)]
        public string S003_0007_1_QualifiantIdentification { get; set; }
        [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
        public string S003_0014_1_AdresseAcheminement { get; set; }

        // S004 : DATE ET HEURE DE PREPARATION
        [EdiValue("9(6)", Path = "UNB/3/0", Format = "yyMMdd", Description = "Date de préparation")]
        public DateTime S004_0017_1_DatePreparation { get; set; }
        [EdiValue("9(4)", Path = "UNB/3/1", Format = "HHmm", Description = "Heure de préparation")]
        public DateTime S004_0019_1_HeurePreparation { get; set; }

        // 0020 : REFERENCE DE CONTROLE DE L’INTERCHANGE
        [EdiValue("X(14)", Path = "UNB/4/0", Mandatory = true)]
        public string _0020_1_ReferenceControleInterchange { get; set; }

        // S005 : REFERENCE DESTINATAIRE / MOT DE PASSE 
        // 0026 : REFERENCE APPLICATION
        // 0029 : CODE DE PRIORITE POUR LE TRAITEMENT
        // 0031 : DEMANDE D’ACCUSE DE RECEPTION

        // 0032 : IDENTIFICATION DE L’ACCORD D’INTERCHANGE
        [EdiValue("X(35)", Path = "UNB/9/0", Mandatory = true)]
        public string _0032_1_IdentificationAccordInterchange { get; set; }

        // 0035 : INDICATEUR DE TEST
        [EdiValue("9(1)", Path = "UNB/10/0", Mandatory = false)]
        public int _0035_1_IndicateurTest { get; set; }
    }

    // En-tête de groupe fonctionnel
    [EdiSegment, EdiPath("UNG")]
    public class Segment_UNG
    {
        // 0038 : INDENTIFICATION DU GROUPE FONCTIONNEL
        [EdiValue("X(6)", Path = "UNG/0/0", Mandatory = true)]
        public string _0038_1_IdentificationGouprFonctionnel { get; set; }

        // S006 : IDENTIF. DE L’EMETTEUR DE L’APPLICATION
        [EdiValue("X(35)", Path = "UNG/1/0", Mandatory = true)]
        public string S006_0040_1_IdentificationEmetteur { get; set; }

        // S007 : IDENTIF. DU DESTINATAIRE DE L’APPLICATION
        [EdiValue("X(35)", Path = "UNG/2/0", Mandatory = true)]
        public string S007_0044_1_IdentificationDestinataire { get; set; }

        // S004 : DATE ET HEURE DE PREPARATION
        [EdiValue("9(6)", Path = "UNG/3/0", Format = "yyMMdd", Description = "Date de préparation")]
        public DateTime S004_0017_1_DatePreparation { get; set; }
        [EdiValue("9(4)", Path = "UNG/3/1", Format = "HHmm", Description = "Heure de préparation")]
        public DateTime S004_0019_1_HeurePreparation { get; set; }

        // 0048 : N° DE REFERENCE DU GROUPE FONCTIONNEL
        [EdiValue("X(14)", Path = "UNG/4/0", Mandatory = true)]
        public string _0048_1_ReferenceGroupeFonctionnel { get; set; }

        // 0051 : AGENCE DE CONTROLE
        [EdiValue("X(2)", Path = "UNG/5/0", Mandatory = true)]
        public string _0051_1_AgenceControle { get; set; }

        // S008 : VERSION DU MESSAGE
        [EdiValue("X(3)", Path = "UNG/6/0", Mandatory = true)]
        public string S008_0052_1_NumeroVersionMessage { get; set; }
        [EdiValue("X(3)", Path = "UNG/6/1", Mandatory = true)]
        public string S008_0054_1_NumeroRevisionMessage { get; set; }
        [EdiValue("X(6)", Path = "UNG/6/2", Mandatory = true)]
        public string S008_0057_1_CodeAttibueParAssociation { get; set; }
    }

    // En-tête de message
    [EdiSegment, EdiPath("UNH")]
    public class Segment_UNH
    {
        // 0062 : NUMERO DE REFERENCE DU MESSAGE
        [EdiValue("X(14)", Path = "UNH/0/0", Mandatory = true)]
        public string _0062_1_NumeroReferenceMessage { get; set; }

        // S009 : IDENTIFIANT DU MESSAGE
        [EdiValue("X(6)", Path = "UNH/1/0", Mandatory = true)]
        public string S009_0065_1_IdentifiantTypeMessage { get; set; }
        [EdiValue("X(3)", Path = "UNH/1/1", Mandatory = true)]
        public string S009_0052_1_NumeroVersionTypeMessage { get; set; }
        [EdiValue("X(3)", Path = "UNH/1/2", Mandatory = true)]
        public string S009_0054_1_NumeroRevisionMessage { get; set; }
        [EdiValue("X(2)", Path = "UNH/1/3", Mandatory = true)]
        public string S009_0051_1_AgenceControle { get; set; }
        [EdiValue("X(6)", Path = "UNH/1/4", Mandatory = false)]
        public string S009_0057_1_CodeAttribueParAssociation { get; set; }

        // 0068 : REFERENCE COMMUNE D'ACCES
        [EdiValue("X(35)", Path = "UNH/2/0", Mandatory = false)]
        public string _0068_1_ReferenceCommuneAcces { get; set; }

        // S010 : STATUT DU TRANSFERT (n'est jamais utilisé)
    }
    // Debut du message
    [EdiSegment, EdiPath("BGM")]
    public class Segment_BGM
    {
        // C002 : NOM DU DOCUMENT OU MESSAGE
        [EdiValue("X(3)", Path = "BGM/0/0", Mandatory = true)]
        public string C002_1001_1_NomDocumentOuMessage { get; set; }
        [EdiValue("X(17)", Path = "BGM/0/1", Mandatory = true)]
        public string C002_1131_1_QltListeCodes { get; set; }
        [EdiValue("X(3)", Path = "BGM/0/2", Mandatory = true)]
        public string C002_3055_1_OrgResponbsableListeCodes { get; set; }
        [EdiValue("X(35)", Path = "BGM/0/3", Mandatory = false)]
        public string C002_1000_1_NomDocumentOuMessage2 { get; set; }

        // C106 : IDENTIFICATION DU DOCUMENT OU MESSAGE
        [EdiValue("X(35)", Path = "BGM/1/0", Mandatory = false)]
        public string C106_1004_1_NumeroDocumentOuMessage { get; set; }

        // 1225 : FONCTION DU MESSAGE(CODE) (n'est jamais utilisé)
        // 4343 : TYPE DE RÉPONSE (n'est jamais utilisé)
    }
    // Fin de message
    [EdiSegment, EdiPath("UNT")]
    public class Segment_UNT
    {
        // 0074 : NOMBRE DE SEGMENTS DANS LE MESSAGE
        [EdiValue("9(6)", Path = "UNT/0/0", Mandatory = true)]
        public int _0074_1_NombreSegmentsDansMessage { get; set; }
        // 0062 : NUMÉRO DE RÉFÉRENCE DU MESSAGE
        [EdiValue("X(14)", Path = "UNT/1/0", Mandatory = true)]
        public string _0062_1_NumeroReferenceMessage { get; set; }
    }

    // Fin de groupe fonctionnel
    [EdiSegment, EdiPath("UNE")]
    public class Segment_UNE
    {
        // 0060 : NOMBRE DE MESSAGES
        [EdiValue("9(6)", Path = "UNE/0/0", Mandatory = true)]
        public int _0060_1_NombreMessage { get; set; }
        // 0048 : N° DE REFERENCE DU GROUPE FONCTIONNEL
        [EdiValue("X(14)", Path = "UNE/1/0", Mandatory = true)]
        public string _0048_1_NumeroReferenceGroupeFonctionnel { get; set; }
    }

    // Fin d'interchange
    [EdiSegment, EdiPath("UNZ")]
    public class Segment_UNZ
    {
        // 0036 : COMPTEUR DE CONTROLE DE L’INTERCHANGE
        [EdiValue("X(1)", Path = "UNZ/0/0", Mandatory = true)]
        public int _0036_1_CompteurControleInterchange { get; set; }
        // 0020 : REFERENCE DE CONTROLE DE L’INTERCHANGE
        [EdiValue("X(14)", Path = "UNZ/1/0", Mandatory = true)]
        public string _0020_1_ReferenceControleInterchange { get; set; }
    }

    // Segment Date ou Heure ou Période (DTM)
    [EdiSegment, EdiPath("DTM")]
    public class Segment_DTM
    {
        // C507  : DATE OU HEURE OU PERIODE
        [EdiValue("X(3)", Path = "DTM/0/0", Mandatory = true)]
        public string C0057_2005_1_QualiteDate { get; set; }
        [EdiValue("X(35)", Path = "DTM/0/1", Mandatory = true)]
        public string C0057_2380_1_Date { get; set; }
        [EdiValue("X(3)", Path = "DTM/0/2", Mandatory = true)]
        public string C0057_2379_1_QualiteFormatDate { get; set; }
    }

    // Segment Référence (RFF)
    [EdiSegment, EdiPath("RFF")]
    public class Segment_RFF
    {
        // C506 : REFERENCE
        [EdiValue("X(3)", Path = "RFF/0/0", Mandatory = true)]
        public string C506_1153_1_QualiteReference { get; set; }
        [EdiValue("X(70)", Path = "RFF/0/1", Mandatory = true)]
        public string C506_1154_1_NumeroReference { get; set; }
        [EdiValue("X(6)", Path = "RFF/0/2", Mandatory = true)]
        public string C506_1156_1_NumeroLigne { get; set; }
        [EdiValue("X(35)", Path = "RFF/0/3", Mandatory = false)]
        public string C506_4000_1_NumeroVersionReference { get; set; }
        [EdiValue("X(6)", Path = "RFF/0/4", Mandatory = false)]
        public string C506_1060_1_NumeroRevision { get; set; }
    }

    // Segment Nom et adresse (NAD)
    [EdiSegment, EdiPath("NAD")]
    //[EdiSegmentGroup("NAD/0/0")]
    public class Segment_NAD
    {
        // 3035 : QLT DE L'INTERVENANT
        [EdiValue("X(3)", Path = "NAD/0/0", Mandatory = true)]
        public string _3035_1_QualiteIntervenant { get; set; }

        // C082 : INFO. DETAILLEES SUR L'IDENTIF. DE L'INTERV.
        [EdiValue("X(35)", Path = "NAD/1/0", Mandatory = true)]
        public string C082_3039_1_IdentifiantIntervenant { get; set; }
        [EdiValue("X(17)", Path = "NAD/1/1", Mandatory = true)]
        public string C082_1131_1_QualificationListeCodes { get; set; }
        [EdiValue("X(3)", Path = "NAD/1/2", Mandatory = true)]
        public string C082_3055_1_OrganismeResponsableListeCodes { get; set; }

        // C058 : NOM ET ADRESSE (adresse à l'étranger)
        [EdiValue("X(35)", Path = "NAD/2/0", Mandatory = false)]
        public string C058_3124_1_LigneAdresseEtranger { get; set; }
        [EdiValue("X(35)", Path = "NAD/2/1", Mandatory = false)]
        public string C058_3124_2_LigneAdresseEtranger { get; set; }
        [EdiValue("X(35)", Path = "NAD/2/2", Mandatory = false)]
        public string C058_3124_3_LigneAdresseEtranger { get; set; }
        [EdiValue("X(35)", Path = "NAD/2/3", Mandatory = false)]
        public string C058_3124_4_LigneAdresseEtranger { get; set; }
        [EdiValue("X(35)", Path = "NAD/2/4", Mandatory = false)]
        public string C058_3124_5_LigneAdresseEtranger { get; set; }

        // C080 : NOM DE L'INTERVENANT
        [EdiValue("X(35)", Path = "NAD/3/0", Mandatory = true)]
        public string C080_3036_1_NomIntervenant { get; set; }
        [EdiValue("X(35)", Path = "NAD/3/1", Mandatory = true)]
        public string C080_3036_2_NomIntervenant { get; set; }
        [EdiValue("X(35)", Path = "NAD/3/2", Mandatory = false)]
        public string C080_3036_3_NomIntervenant { get; set; }
        [EdiValue("X(35)", Path = "NAD/3/3", Mandatory = false)]
        public string C080_3036_4_NomIntervenant { get; set; }
        [EdiValue("X(35)", Path = "NAD/3/4", Mandatory = false)]
        public string C080_3036_5_NomIntervenant { get; set; }
        [EdiValue("X(3)", Path = "NAD/3/5", Mandatory = false)]
        public string C080_3045_1_FormatNomIntervenant { get; set; }

        // C059 : RUE
        [EdiValue("X(35)", Path = "NAD/4/0", Mandatory = true)]
        public string C059_3042_1_RueEtNumeroOuBP { get; set; }
        [EdiValue("X(35)", Path = "NAD/4/1", Mandatory = true)]
        public string C059_3042_2_RueEtNumeroOuBP { get; set; }
        [EdiValue("X(35)", Path = "NAD/4/2", Mandatory = true)]
        public string C059_3042_3_RueEtNumeroOuBP { get; set; }
        [EdiValue("X(35)", Path = "NAD/4/3", Mandatory = false)]
        public string C059_3042_4_RueEtNumeroOuBP { get; set; }
        [EdiValue("X(35)", Path = "NAD/4/4", Mandatory = false)]
        public string C059_3042_5_RueEtNumeroOuBP { get; set; }

        // 3164 : NOM DE LA VILLE
        [EdiValue("X(35)", Path = "NAD/5/0", Mandatory = true)]
        public string _3164_1_NomVille { get; set; }

        // C819 : INFO.DETAILLEES SUR UNE DIV. TERRIT. D'UN PAYS
        [EdiValue("X(9)", Path = "NAD/6/0", Mandatory = true)]
        public string C819_3229_1_IdentificationDivisionTerritoriale { get; set; }
        [EdiValue("X(17)", Path = "NAD/6/1", Mandatory = false)]
        public string C819_1131_2_QualificationListeCodes { get; set; }
        [EdiValue("X(3)", Path = "NAD/6/2", Mandatory = false)]
        public string C819_3055_2_OrganismeResponsableListeCodes { get; set; }
        [EdiValue("X(35)", Path = "NAD/6/3", Mandatory = false)]
        public string C819_3228_DivisionTerritorialePays { get; set; }

        // 3251 : CODE POSTAL
        [EdiValue("X(17)", Path = "NAD/7/0", Mandatory = false)]
        public string _3251_1_CodePostal { get; set; }

        // 3207 : PAYS
        [EdiValue("X(3)", Path = "NAD/8/0", Mandatory = false)]
        public string _3207_1_CodeIsoPays { get; set; }

        //public Segment_CTA Contact { get; set; }

        //public List<Segment_COM> Coordonnees { get; set; }

        public override string ToString() {
            return C080_3036_1_NomIntervenant ?? base.ToString();
        }
    }
    // Segment Informations sur le correspondant (CTA)
    [EdiSegment, EdiPath("CTA")]
    public class Segment_CTA
    {
        // 3139 : FONCTION DU CORRESPONDANT (CODE)
        [EdiValue("X(3)", Path = "CTA/0/0", Mandatory = true)]
        public string _3139_1_FonctionCorrespondant { get; set; }
        // C056 : INFO. DETAILLEES SUR LE SERVICE OU L'EMPLOYE
        [EdiValue("X(17)", Path = "CTA/1/0", Mandatory = true)]
        public string C056_3413_1_IdentificationServiceOuEmploye { get; set; }
        [EdiValue("X(35)", Path = "RFF/1/1", Mandatory = true)]
        public string C056_3412_1_ServiceOuEmploye { get; set; }
    }
    // Segment Coordonnées de communication (COM)
    [EdiSegment, EdiPath("COM")]
    public class Segment_COM
    {
        // C076 : COORDONNEES DE COMMUNICATION
        [EdiValue("X(512)", Path = "COM/0/0", Mandatory = true)]
        public string _C076_3148_1_NumeroCommunication { get; set; }
        [EdiValue("X(3)", Path = "COM/1/0", Mandatory = true)]
        public string _C076_3155_1_QualiteCanaCommunication { get; set; }
    }

}
