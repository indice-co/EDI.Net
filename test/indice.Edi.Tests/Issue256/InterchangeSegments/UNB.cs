using indice.Edi.Serialization;

namespace indice.Edi.Tests.Issue256.InterchangeSegments;

/// <summary>
/// To start, identify and specify an interchange.
/// </summary>
[EdiSegment, EdiPath("UNB")]
public class UNB
{
    /// <summary>
    /// Syntax identifier
    /// </summary>
    [EdiPath("UNB/0")]
    public UNB_SyntaxIdentifier? SyntaxIdentifier { get; set; }

    /// <summary>
    /// Interchange sender
    /// </summary>
    [EdiPath("UNB/1")]
    public UNB_InterchangeSender? InterchangeSender { get; set; }

    /// <summary>
    /// Interchange recipient
    /// </summary>
    [EdiPath("UNB/2")]
    public UNB_InterchangeRecipient? InterchangeRecipient { get; set; }

    /// <summary>
    /// Date/time of preparation
    /// </summary>
    [EdiPath("UNB/3")]
    public UNB_DateTimeOfPreparation? DateTimeOfPreparation { get; set; }

    /// <summary>
    /// Interchange control reference
    /// </summary>
    [EdiValue("X(14)", Path = "UNB/4", Mandatory = true)]
    public string? InterchangeControlReference { get; set; }

    /// <summary>
    /// Recipient's reference, password
    /// </summary>
    [EdiPath("UNB/5")]
    public UNB_RecipientsReferencePassword? RecipientsReferencePassword { get; set; }

    /// <summary>
    /// Application reference
    /// </summary>
    [EdiValue("X(14)", Path = "UNB/6", Mandatory = false)]
    public string? ApplicationReference { get; set; }

    /// <summary>
    /// Processing priority code
    /// </summary>
    [EdiValue("X(1)", Path = "UNB/7", Mandatory = false)]
    public string? ProcessingPriorityCode { get; set; }

    /// <summary>
    /// Acknowledgement request
    /// </summary>
    [EdiValue("9(1)", Path = "UNB/8", Mandatory = false)]
    public int? AcknowledgementRequest { get; set; }

    /// <summary>
    /// Communications agreement ID
    /// </summary>
    [EdiValue("X(35)", Path = "UNB/9", Mandatory = false)]
    public string? CommunicationsAgreementID { get; set; }

    /// <summary>
    /// Test indicator
    /// </summary>
    [EdiValue("9(1)", Path = "UNB/10", Mandatory = false)]
    public int? TestIndicator { get; set; }
}

/// <summary>
/// Syntax identifier
/// </summary>
[EdiElement]
public class UNB_SyntaxIdentifier
{
    /// <summary>
    /// Coded identification of the agency controlling a syntax and syntax level used in an interchange.
    /// </summary>
    [EdiValue("X(4)", Path = "UNB/*/0", Mandatory = true)]
    public string? SyntaxIdentifier { get; set; }

    /// <summary>
    /// Version number of the syntax identified in the syntax identifier (0001)
    /// </summary>
    [EdiValue("9(1)", Path = "UNB/*/1", Mandatory = true)]
    public int? SyntaxVersionNumber { get; set; }
}

/// <summary>
/// Interchange sender
/// </summary>
[EdiElement]
public class UNB_InterchangeSender
{
    /// <summary>
    /// Name or coded representation of the sender of a data interchange.
    /// </summary>
    [EdiValue("X(35)", Path = "UNB/*/0", Mandatory = true)]
    public string? SenderIdentification { get; set; }

    /// <summary>
    /// Qualifier referring to the source of codes for the identifiers of interchanging partners.
    /// </summary>
    [EdiValue("X(4)", Path = "UNB/*/1", Mandatory = false)]
    public string? PartnerIdentificationCodeQualifier { get; set; }

    /// <summary>
    /// Address specified by the sender of an interchange to be included by the recipient in the response interchanges to facilitate internal routing.
    /// </summary>
    [EdiValue("X(14)", Path = "UNB/*/2", Mandatory = false)]
    public string? AddressForReverseRouting { get; set; }
}

/// <summary>
/// Interchange recipient
/// </summary>
[EdiElement]
public class UNB_InterchangeRecipient
{
    /// <summary>
    /// Name or coded representation of the recipient of a data interchange.
    /// </summary>
    [EdiValue("X(35)", Path = "UNB/*/0", Mandatory = true)]
    public string? RecipientIdentification { get; set; }

    /// <summary>
    /// Qualifier referring to the source of codes for the identifiers of interchanging partners.
    /// </summary>
    [EdiValue("X(4)", Path = "UNB/*/1", Mandatory = false)]
    public string? PartnerIdentificationCodeQualifier { get; set; }

    /// <summary>
    /// Address specified by the recipient of an interchange to be included by the sender and used by the recipient for routing of received interchanges inside his organization.
    /// </summary>
    [EdiValue("X(14)", Path = "UNB/*/2", Mandatory = false)]
    public string? RoutingAddress { get; set; }
}

/// <summary>
/// Date/time of preparation
/// </summary>
[EdiElement]
public class UNB_DateTimeOfPreparation
{
    /// <summary>
    /// Local date when an interchange or a functional group was prepared.
    /// </summary>
    [EdiValue("9(6)", Path = "UNB/*/0", Mandatory = true)]
    public int? DateOfPreparation { get; set; }

    /// <summary>
    /// Local time of day when an interchange or a functional group was prepared.
    /// </summary>
    [EdiValue("9(4)", Path = "UNB/*/1", Mandatory = false)]
    public int? TimeOfPreparation { get; set; }
}

/// <summary>
/// Recipient's reference, password
/// </summary>
[EdiElement]
public class UNB_RecipientsReferencePassword
{
    /// <summary>
    /// Unique reference assigned by the recipient to the data interchange or a password to the recipient's system or to a third party network as specified in the partners interchange agreement.
    /// </summary>
    [EdiValue("X(14)", Path = "UNB/*/0", Mandatory = true)]
    public string? RecipientsReferencePassword { get; set; }

    /// <summary>
    /// Qualifier for the recipient's reference or password.
    /// </summary>
    [EdiValue("X(2)", Path = "UNB/*/1", Mandatory = false)]
    public string? RecipientsReferencePasswordQualifier { get; set; }
}