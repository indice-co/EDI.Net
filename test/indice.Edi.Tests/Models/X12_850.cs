using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models.X12_01
{
    /// <summary>
    /// Purchase Order 850
    /// </summary>
    public class PurchaseOrder_850
    {

        [EdiValue("9(2)", Path = "ISA/0", Description = "I01 - Authorization Information Qualifier")]
        public int AuthorizationInformationQualifier { get; set; }

        [EdiValue("X(10)", Path = "ISA/1", Description = "I02 - Authorization Information")]
        public string AuthorizationInformation { get; set; }

        [EdiValue("9(2)", Path = "ISA/2", Description = "I03 - Security Information Qualifier")]
        public string Security_Information_Qualifier { get; set; }

        [EdiValue("X(10)", Path = "ISA/3", Description = "I04 - Security Information")]
        public string Security_Information { get; set; }

        [EdiValue("9(2)", Path = "ISA/4", Description = "I05 - Interchange ID Qualifier")]
        public string ID_Qualifier { get; set; }

        [EdiValue("X(15)", Path = "ISA/5", Description = "I06 - Interchange Sender ID")]
        public string Sender_ID { get; set; }

        [EdiValue("9(2)", Path = "ISA/6", Description = "I05 - Interchange ID Qualifier")]
        public string ID_Qualifier2 { get; set; }

        [EdiValue("X(15)", Path = "ISA/7", Description = "I07 - Interchange Receiver ID")]
        public string Receiver_ID { get; set; }
        
        [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I08 - Interchange Date")]
        [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "TI09 - Interchange Time")]
        public DateTime Date { get; set; }

        [EdiValue("X(1)", Path = "ISA/10", Description = "I10 - Interchange Control Standards ID")]
        public string Control_Standards_ID { get; set; }

        [EdiValue("9(5)", Path = "ISA/11", Description = "I11 - Interchange Control Version Num")]
        public int ControlVersion { get; set; }

        [EdiValue("9(9)", Path = "ISA/12", Description = "I12 - Interchange Control Number")]
        public int ControlNumber { get; set; }

        [EdiValue("9(1)", Path = "ISA/13", Description = "I13 - Acknowledgement Requested")]
        public bool? AcknowledgementRequested { get; set; }

        [EdiValue("X(1)", Path = "ISA/14", Description = "I14 - Usage Indicator")]
        public string Usage_Indicator { get; set; }

        [EdiValue("X(1)", Path = "ISA/15", Description = "I15 - Component Element Separator")]
        public char? Component_Element_Separator { get; set; } 

        [EdiValue("9(1)", Path = "IEA/0", Description = "I16 - Num of Included Functional Grps")]
        public int GroupsCount { get; set; }

        [EdiValue("9(9)", Path = "IEA/1", Description = "I12 - Interchange Control Number")]
        public int TrailerControlNumber { get; set; }

        public List<FunctionalGroup> Groups { get; set; }

        [EdiGroup]
        public class FunctionalGroup
        {
            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "373 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "337 - Time")]
            public DateTime Date { get; set; }

            public List<Order> Orders { get; set; }
        }

        [EdiMessage]
        public class Order
        {
            [EdiValue("X(40)", Path = "N3/0", Description = "166 - Street Address")]
            public string StreetAddress { get; set; }

            [EdiValue(Path = "PO1/3", Description = "212 - Unit Price")]
            public decimal UnitPrice { get; set; }
        }
    }
}
