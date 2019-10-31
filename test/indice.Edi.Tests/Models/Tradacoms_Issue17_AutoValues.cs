using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models
{
    public class Interchange_Issue17
    {
        [EdiValue("X(14)", Path = "STX/1/0")]
        public string SenderCode { get; set; }

        [EdiValue("X(35)", Path = "STX/1/1")]
        public string SenderName { get; set; }

        [EdiValue("9(6)", Path = "STX/3/0", Format = "yyMMdd", Description = "TRDT - Date")]
        [EdiValue("9(6)", Path = "STX/3/1", Format = "HHmmss", Description = "TRDT - Time")]
        public DateTime TransmissionStamp { get; set; }

        public InterchangeHeader Head { get; set; }

        public InterchangeVatSummary Vat { get; set; }

        public InterchangeTrailer Summary { get; set; }

        public List<UtilityBill> Invoices { get; set; }

        [EdiSegment, EdiPath("MHD")]
        public class MessageHeader
        {
            [EdiValue("9(1)", Path = "*/0/0")]
            public int Position { get; set; }
            public MessageType Type { get; set; }
        }

        [EdiElement, EdiPath("MHD/1")]
        public class MessageType
        {
            [EdiValue("9(1)", Path = "*/*/0")]
            public string Name { get; set; }

            [EdiValue("9(1)", Path = "*/*/1")]
            public int Version { get; set; }
        }

        [EdiMessage, EdiCondition("UTLHDR", Path = "MHD/1")]
        public class InterchangeHeader
        {
            public MessageHeader Head { get; set; }

            [EdiValue("9(4)"), EdiPath("TYP")]
            public string TransactionCode { get; set; }

            [EdiValue("X(40)", Path = "CDT/1")]
            public string ClientName { get; set; }
        }

        [EdiMessage, EdiCondition("UTLTLR", Path = "MHD/1")]
        public class InterchangeTrailer
        {
            public MessageHeader Head { get; set; }
        }

        [EdiMessage, EdiCondition("UVATLR", Path = "MHD/1")]
        public class InterchangeVatSummary
        {
            public MessageHeader Head { get; set; }

            [EdiValue("9(4)"), EdiPath("TYP")]
            public string TransactionCode { get; set; }
        }

        [EdiMessage, EdiCondition("UTLBIL", Path = "MHD/1")]
        public class UtilityBill
        {
            public MessageHeader Head { get; set; }

            [EdiValue("X(17)", Path = "BCD/2/0", Description = "INVN - Date")]
            public string InvoiceNumber { get; set; }

            public MetetAdminNumber Meter { get; set; }
            public ContractData SupplyContract { get; set; }

            [EdiValue("X(3)", Path = "BCD/5/0", Description = "BTCD - Date")]
            public BillTypeCode BillTypeCode { get; set; }

            [EdiValue("9(6)", Path = "BCD/1/0", Format = "yyMMdd", Description = "TXDT - Date")]
            public DateTime IssueDate { get; set; }

            [EdiValue("9(6)", Path = "BCD/7/0", Format = "yyMMdd", Description = "SUMO - Date")]
            public DateTime StartDate { get; set; }

            [EdiValue("9(6)", Path = "BCD/7/1", Format = "yyMMdd", Description = "SUMO - Date")]
            public DateTime EndDate { get; set; }

            public UtilityBillTrailer Totals { get; set; }
            public UtilityBillValueAddedTax Vat { get; set; }
            public List<ConsumptionChargeCharge> Charges { get; set; }

            public override string ToString() {
                return string.Format("{0} TD:{1:d} F:{2:d} T:{3:d} Type:{4}", InvoiceNumber, IssueDate, StartDate, EndDate, BillTypeCode);
            }
        }
    }
}
