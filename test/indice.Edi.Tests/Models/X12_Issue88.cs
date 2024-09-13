using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class Interchange_Issue88
{
    [EdiValue("9(2)", Path = "ISA/0", Description = "ISA01 - Authorization Information Qualifier")]
    public int AuthorizationInformationQualifier { get; set; }

    [EdiValue("X(10)", Path = "ISA/1", Description = "ISA02 - Authorization Information")]
    public string AuthorizationInformation { get; set; }

    [EdiValue("9(2)", Path = "ISA/2", Description = "ISA03 - Security Information Qualifier")]
    public string Security_Information_Qualifier { get; set; }

    [EdiValue("X(10)", Path = "ISA/3", Description = "ISA04 - Security Information")]
    public string Security_Information { get; set; }

    [EdiValue("9(2)", Path = "ISA/4", Description = "ISA05 - Interchange ID Qualifier")]
    public string ID_Qualifier { get; set; }

    [EdiValue("X(15)", Path = "ISA/5", Description = "ISA06 - Interchange Sender ID")]
    public string Sender_ID { get; set; }

    [EdiValue("9(2)", Path = "ISA/6", Description = "ISA07 - Interchange ID Qualifier")]
    public string ID_Qualifier2 { get; set; }

    [EdiValue("X(15)", Path = "ISA/7", Description = "ISA08 - Interchange Receiver ID")]
    public string Receiver_ID { get; set; }

    [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I09 - Interchange Date")]
    [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "I10 - Interchange Time")]
    public DateTime Date { get; set; }

    [EdiValue("X(1)", Path = "ISA/10", Description = "ISA11 - Interchange Control Standards ID")]
    public string Control_Standards_ID { get; set; }

    [EdiValue("9(5)", Path = "ISA/11", Description = "ISA12 - Interchange Control Version Num")]
    public int ControlVersion { get; set; }

    [EdiValue("9(9)", Path = "ISA/12", Description = "ISA13 - Interchange Control Number")]
    public int ControlNumber { get; set; }

    [EdiValue("9(1)", Path = "ISA/13", Description = "ISA14 - Acknowledgement Requested")]
    public bool? AcknowledgementRequested { get; set; }

    [EdiValue("X(1)", Path = "ISA/14", Description = "ISA15 - Usage Indicator")]
    public string Usage_Indicator { get; set; }

    [EdiValue("X(1)", Path = "ISA/15", Description = "ISA16 - Component Element Separator")]
    public char? Component_Element_Separator { get; set; }

    [EdiValue("9(1)", Path = "IEA/0", Description = "IEA01 - Num of Included Functional Grps")]
    public int GroupsCount { get; set; }

    [EdiValue("9(9)", Path = "IEA/1", Description = "IEA02 - Interchange Control Number")]
    public int TrailerControlNumber { get; set; }
}
