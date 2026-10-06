using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

[EdiMessage]

public class EdiFact_Issue287 
{
    public UNB unb { get; set; }
    public UNH unh { get; set; }
    public Body body { get; set; }
    public Footer footer { get; set; }


    [EdiSegmentGroup("BGM", Mandatory = true)]
    public class Body
    {

        //[EdiElement] // <- with this attribut, only first part is valorized (BGM[0][0]) in BGM object
        public BGM bgm { get; set; }  // <- always null without modification (tried every possible attribute configuration on class, property, with or without initialization [ = new BGM();] etc.)

        public List<DTM> dtm { get; set; } = new List<DTM>();
        public List<NAD> nad { get; set; } = new List<NAD>();
        public List<RFF> rff { get; set; } = new List<RFF>();
        public List<CUX> cux { get; set; } = new List<CUX>();
    }

    [EdiSegmentGroup("UNT", Mandatory =true)]
    public class Footer {
        public UNT unt {get; set;}
    }

    #region EdiSegments
    [EdiSegment, EdiPath("UNH")]
    public record UNH
    {
        [EdiValue("X(14)", Path = "UNH/0/0", Mandatory = true)]
        public string MessageRefNum { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/0", Mandatory = true)]
        public string MessageType { get; set; }

        [EdiValue("X(3)", Path = "UNH/1/1", Mandatory = true)]
        public string Version { get; set; }

        [EdiValue("X(3)", Path = "UNH/1/2", Mandatory = true)]
        public string Release { get; set; }

        [EdiValue("X(2)", Path = "UNH/1/3", Mandatory = true)]
        public string ControllingAgency { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/4", Mandatory = false)]
        public string? AssociationAssignedCode { get; set; }

        [EdiValue("X(6)", Path = "UNH/1/5", Mandatory = false)]
        public string? CodeListDirectoryVersionNumber { get; set; }
    }

    [EdiSegment, EdiPath("UNB")]
    public class UNB
    {
        [EdiValue("X(4)", Path = "UNB/0/0", Mandatory = true)]
        public string SyntaxIdentifier { get; set; }
        [EdiValue("X(1)", Path = "UNB/0/1", Mandatory = true)]
        public string SyntaxVersionNumber { get; set; }


        // S002 : EMETTEUR DE L'INTERCHANGE
        [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
        public string SenderIdentification { get; set; }
        [EdiValue("X(4)", Path = "UNB/1/1", Mandatory = false)]
        public string SenderIdentificationCodeQualifier { get; set; }
        [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
        public string AddressForReverseRouting { get; set; }


        // S003 : RECEPTEUR DE L'INTERCHANGE
        [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
        public string RecipientIdentification { get; set; }
        [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = false)]
        public string RecipientIdentificationCodeQualifier { get; set; }
        [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
        public string RoutingAddress { get; set; }


        // S004 : DATE ET HEURE DE PREPARATION
        [EdiValue("X(6)", Path = "UNB/3/0")]
        public string DateOfPreparation { get; set; }
        [EdiValue("X(4)", Path = "UNB/3/1")]
        public string TimeOfPreparation { get; set; }


        // S005 : REFERENCE DE CONTROLE DE L’INTERCHANGE
        [EdiValue("X(14)", Path = "UNB/4/0", Mandatory = true)]
        public string INTERCHANGECONTROLREFERENCE { get; set; }


        // 0026 : APPLICATIONREFERENCE
        [EdiValue("X(14)", Path = "UNB/6/0", Mandatory = true)]
        public string APPLICATIONREFERENCE { get; set; }
        // 0029 : PROCESSING PRIORITY CODE
        [EdiValue("X(1)", Path = "UNB/7/0", Mandatory = false)]
        public string PROCESSING_PRIORITY_CODE { get; set; }
        // 0026 : APPLICATIONREFERENCE
        [EdiValue("X(35)", Path = "UNB/8/0", Mandatory = false)]
        public string COMMUNICATIONS_AGREEMENT { get; set; }

    }

    [EdiSegment, EdiPath("BGM")]
    public record BGM
    {
        [EdiValue("X(3)", Path = "BGM/0/0", Mandatory = false)]
        public string? DocumentNameCode { get; set; }

        [EdiValue("X(17)", Path = "BGM/0/1", Mandatory = false)]
        public string? CodeListIdentificationCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/0/2", Mandatory = false)]
        public string? CodeListResponsibleAgencyCode { get; set; }

        [EdiValue("X(35)", Path = "BGM/0/3", Mandatory = false)]
        public string? DocumentName { get; set; }

        [EdiValue("X(70)", Path = "BGM/1/0", Mandatory = false)]
        public string? DocumentIdentifier { get; set; }

        [EdiValue("X(9)", Path = "BGM/1/1", Mandatory = false)]
        public string? VersionIdentifier { get; set; }

        [EdiValue("X(6)", Path = "BGM/1/2", Mandatory = false)]
        public string? RevisionIdentifier { get; set; }

        [EdiValue("X(3)", Path = "BGM/2", Mandatory = true)]
        public string? MessageFunctionCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/3", Mandatory = false)]
        public string? ResponseTypeCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/4", Mandatory = false)]
        public string? DocumentStatusCode { get; set; }

        [EdiValue("X(3)", Path = "BGM/5", Mandatory = false)]
        public string? LanguageNameCode { get; set; }
    }

    [EdiSegment, EdiPath("RFF")]
    public class RFF { }

    [EdiSegment, EdiPath("CUX")]
    public class CUX { }

    [EdiSegment, EdiPath("UNT")]
    public record UNT
    {
        [EdiValue("9(10)", Path = "UNT/0", Description = "Number of Segments in Message", Mandatory = true)]
        [EdiCount(EdiCountScope.Segments)]
        public int SegmentCount { get; set; }

        [EdiValue("X(14)", Path = "UNT/1", Description = "Message Reference Number", Mandatory = true)]
        public string MessageRefNum { get; set; }
    }
    #endregion
}


