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

        [EdiValue("9(1)", Path = "END/0/0")]
        [EdiCount(EdiCountScope.Segments)]
        public int TrailerControlCount { get; set; }

        public InterchangeHeader Head { get; set; }

        public List<UtilityBill> Invoices { get; set; }

        public InterchangeVatSummary Vat { get; set; }

        public InterchangeTrailer Summary { get; set; }


        [EdiSegment, EdiPath("MHD")]
        public class MessageHead
        {
            [EdiValue("9(1)", Path = "*/0/0")] 
            [EdiCount(EdiCountScope.Messages)]
            public int Position { get; set; }
            public MessageType Type { get; set; }
        }

        [EdiSegment, EdiPath("MTR")]
        public class MessageTrail
        {
            [EdiValue("9(1)", Path = "*/0/0")]
            [EdiCount(EdiCountScope.Segments)]
            public int ControlCount { get; set; }

            public override string ToString() {
                return $"Count = {ControlCount}";
            }
        }

        [EdiElement, EdiPath("MHD/1")]
        public class MessageType
        {
            [EdiValue("9(1)", Path = "*/*/0")]
            public string Name { get; set; }

            [EdiValue("9(1)", Path = "*/*/1")]
            public int Version { get; set; }

            public override string ToString() {
                if (!string.IsNullOrWhiteSpace(Name))
                    return $"{Name} v{Version}";
                return base.ToString();
            }
        }

        [EdiMessage, EdiCondition("UTLHDR", Path = "MHD/1")]
        public class InterchangeHeader : MessageHead
        {

            [EdiValue("9(4)"), EdiPath("TYP")]
            public string TransactionCode { get; set; }
            [EdiSegment, EdiPath("SDT")]
            public PartnerLocationTable SDT { get; set; }
            [EdiSegment, EdiPath("CDT")]
            public PartnerLocationTable CDT { get; set; }

            public FIL FIL { get; set; }

            public REF REF { get; set; }
            public MessageTrail Trailer { get; set; }
        }

        [EdiMessage, EdiCondition("UTLTLR", Path = "MHD/1")]
        public class InterchangeTrailer : MessageHead
        {
            public MessageTrail Trailer { get; set; }

        }

        [EdiMessage, EdiCondition("UVATLR", Path = "MHD/1")]
        public class InterchangeVatSummary : MessageHead
        {

            [EdiValue("9(4)"), EdiPath("TYP")]
            public string TransactionCode { get; set; }
            public MessageTrail Trailer { get; set; }
        }

        [EdiMessage, EdiCondition("UTLBIL", Path = "MHD/1")]
        public class UtilityBill : MessageHead
        {

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
            public MessageTrail Trailer { get; set; }

            public override string ToString() {
                return string.Format("{0} TD:{1:d} F:{2:d} T:{3:d} Type:{4}", InvoiceNumber, IssueDate, StartDate, EndDate, BillTypeCode);
            }
        }

        [EdiSegment, EdiPath("FIL")]
        public class FIL
        {
            [EdiValue("9(1)", Path = "*/0")]
            public int Attrubute1 { get; set; }
            [EdiValue("9(1)", Path = "*/1")]
            public int Attrubute2 { get; set; }
            [EdiValue("9(1)", Path = "*/2")]
            public int Attrubute3 { get; set; }
        }
        [EdiSegment, EdiPath("REF")]
        public class REF
        {
            [EdiValue("9(1)", Path = "*/0/0")]
            public int Attrubute1 { get; set; }
            [EdiValue("9(1)", Path = "*/0/1")]
            public int Attrubute2 { get; set; }
        }

        public class PartnerLocationTable
        {
            [EdiValue("9(4)"), EdiPath("*/0")]
            public string ReferenceCode { get; set; } //VAT0 - SUPPLIER’S VAT REGISTRATION NUMBER
            [EdiValue("X(35)"), EdiPath("*/1")]
            public string Name { get; set; }// SNAM - SUPPLIER’S NAME
            [EdiValue("X(35)"), EdiPath("*/2/0")]
            public string Address1 { get; set; } // SAD0 - SUPPLIER’S ADDRESS LINE 1
            [EdiValue("X(35)"), EdiPath("*/2/1")]
            public string Address2 { get; set; } //SAD1 - SUPPLIER’S ADDRESS LINE 2
            [EdiValue("X(35)"), EdiPath("*/2/2")]
            public string Address3 { get; set; } //SAD2 - SUPPLIER’S ADDRESS LINE 3
            [EdiValue("X(35)"), EdiPath("*/2/3")]
            public string City { get; set; } //SAD3 - SUPPLIER’S ADDRESS LINE 4
            [EdiValue("9(4)"), EdiPath("*/2/4")]
            public string Zip { get; set; } //SAD4 - SUPPLIER’S POST CODE
    }

    }
}
