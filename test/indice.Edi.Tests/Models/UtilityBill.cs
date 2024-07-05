using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace indice.Edi.Tests.Models;

public class Interchange
{
    [EdiValue("X(14)", Path = "STX/1/0")]
    public string SenderCode { get; set; }

    [EdiValue("X(35)", Path = "STX/1/1")]
    public string SenderName { get; set; }

    [EdiValue("9(6)", Path = "STX/3/0", Format = "yyMMdd", Description = "TRDT - Date")]
    [EdiValue("9(6)", Path = "STX/3/1", Format = "HHmmss", Description = "TRDT - Time")]
    public DateTime TransmissionStamp { get; set; }

    public InterchangeHeader Head { get; set; }

    public List<UtilityBill> Invoices { get; set; }
    public InterchangeVatSummary Vat { get; set; }

    public InterchangeTrailer Summary { get; set; }
}


[EdiMessage, EdiCondition("UTLHDR", Path = "MHD/1")]
public class InterchangeHeader
{
    [EdiValue("9(4)"), EdiPath("TYP")]
    public string TransactionCode { get; set; }

    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }

    [EdiValue("X(40)", Path = "CDT/1")]
    public string ClientName { get; set; }
}

[EdiMessage, EdiCondition("UTLTLR", Path = "MHD/1")]
public class InterchangeTrailer
{

    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }
}

[EdiMessage, EdiCondition("UVATLR", Path = "MHD/1")]
public class InterchangeVatSummary
{
    [EdiValue("9(4)"), EdiPath("TYP")]
    public string TransactionCode { get; set; }

    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }
}



[EdiMessage, EdiCondition("UTLBIL", Path = "MHD/1")]
public class UtilityBill
{
    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }

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

[EdiSegment, EdiPath("CCD")]
public class ConsumptionChargeCharge
{
    
    [EdiValue("9(1)", Path = "CCD/0")]
    public int SequenceNumber { get; set; }

    [EdiValue("X(1)", Path = "CCD/1")]
    public ChargeIndicator? ChargeIndicator { get; set; }

    [EdiValue("9(1)", Path = "CCD/1/1")]
    public int? ArticleNumber { get; set; }

    [EdiValue("X(3)", Path = "CCD/1/2")]
    public string SupplierCode { get; set; }


    [EdiValue("X(6)", Path = "CCD/2/0", Description = "TCOD")]
    public string TariffCode { get; set; }

    [EdiValue("X(40)", Path = "CCD/2/1", Description = "TCOD")]
    public string TariffDescription { get; set; }


    [EdiValue("X(6)", Path = "CCD/3/0", Description = "TMOD")]
    public string TariffCodeModifier1 { get; set; }

    [EdiValue("X(6)", Path = "CCD/3/1", Description = "TMOD")]
    public string TariffCodeModifier2 { get; set; }

    [EdiValue("X(6)", Path = "CCD/3/2", Description = "TMOD")]
    public string TariffCodeModifier3 { get; set; }

    [EdiValue("X(6)", Path = "CCD/3/3", Description = "TMOD")]
    public string TariffCodeModifier4 { get; set; }


    [EdiValue("X(35)", Path = "CCD/4", Description = "MTNR")]
    public string MeterNumber { get; set; }

    [EdiValue("X(40)", Path = "CCD/5", Description = "MLOC")]
    public string MeterLocation { get; set; }

    [EdiValue("9(6)", Path = "CCD/6", Format = "yyMMdd", Description = "PRDT")]
    public DateTime? PresentReadDate { get; set; }

    [EdiValue("9(6)", Path = "CCD/7", Format = "yyMMdd", Description = "PVDT")]
    public DateTime? PreviousReadDate { get; set; }

    [EdiValue("9(3)", Path = "CCD/8", Description = "NDRP")]
    public int? ReadingPeriod { get; set; }


    [EdiValue("9(15)", Path = "CCD/9/0", Description = "PRRD")]
    public decimal PresentReading { get; set; }

    [EdiValue("X(4)", Path = "CCD/9/1", Description = "PRRD")]
    public ReadingDataType? PresentReadingType { get; set; }

    [EdiValue("9(15)", Path = "CCD/9/2", Description = "PRRD")]
    public decimal PreviousReading { get; set; }

    [EdiValue("X(4)", Path = "CCD/9/3", Description = "PRRD")]
    public ReadingDataType? PreviousReadingType { get; set; }


    [EdiValue("9(10)V9(3)", Path = "CCD/10/0", Description = "CONS")]
    public decimal? UnitsConsumedBilling { get; set; }

    [EdiValue("X(6)", Path = "CCD/10/1", Description = "CONS")]
    public string UnitOfMeasureBilling { get; set; }
    
    private string _UnitsNegativeBilling;
    [EdiValue("X(4)", Path = "CCD/10/2", Description = "CONS")]
    public string UnitsNegativeBilling {
        get { return _UnitsNegativeBilling; }
        set {
            _UnitsNegativeBilling = value;
            if (_UnitsNegativeBilling == "R") {
                UnitsConsumedBilling = UnitsConsumedBilling * -1;
            }
        }
    }


    [EdiValue("9(10)V9(3)", Path = "CCD/11/0", Description = "CONB")]
    public decimal? UnitsConsumedBase { get; set; }

    [EdiValue("X(6)", Path = "CCD/11/1", Description = "CONB")]
    public string UnitOfMeasureBase { get; set; }
    
    private string _UnitsNegativeBase;
    [EdiValue("X(4)", Path = "CCD/11/2", Description = "CONB")]
    public string UnitsNegativeBase {
        get { return _UnitsNegativeBase; }
        set {
            _UnitsNegativeBase = value;
            if (_UnitsNegativeBase == "R") {
                UnitsConsumedBase = UnitsConsumedBase * -1;
            }
        }
    }


    [EdiValue("X(3)", Path = "CCD/12/0", Description = "ADJF")]
    public string AdjustmentFactorCode { get; set; }

    [EdiValue("9(10)V9(5)", Path = "CCD/12/1", Description = "ADJF")]
    public decimal AdjustmentFactorValue { get; set; }

    [EdiValue("X(4)", Path = "CCD/12/2", Description = "ADJF")]
    public string AdjustmentFactorNegativeIndicator { get; set; }


    [EdiValue("9(10)V9(3)", Path = "CCD/13/0", Description = "CONA")]
    public decimal UnitsConsumedAdjusted { get; set; }

    [EdiValue("X(6)", Path = "CCD/13/1", Description = "CONA")]
    public string UnitOfMeasureAdjusted { get; set; }
    
    [EdiValue("X(4)", Path = "CCD/13/2", Description = "CONA")]
    public string NegativeIndicatorAdjusted { get; set; }


    [EdiValue("9(10)V9(5)", Path = "CCD/14", Description = "BPRI")]
    public decimal? BasePriceUnit { get; set; }


    [EdiValue("9(10)V9(3)", Path = "CCD/15/0", Description = "NUCT")]
    public decimal UnitsBilled { get; set; }

    [EdiValue("X(6)", Path = "CCD/15/1", Description = "NUCT")]
    public string UnitOfMeasureBilled { get; set; }
   
    [EdiValue("X(4)", Path = "CCD/15/2", Description = "NUCT")]
    public string NegativeIndicatorBilled { get; set; }


    [EdiValue("9(6)", Path = "CCD/16", Format = "yyMMdd", Description = "CSDT")]
    public DateTime ChargeStartDate { get; set; }

    [EdiValue("9(6)", Path = "CCD/17", Format = "yyMMdd", Description = "CEDT")]
    public DateTime ChargeEndDate { get; set; }

    [EdiValue("9(10)V9(5)", Path = "CCD/18", Description = "CPPU")]
    public decimal? PricePerUnit { get; set; }


    [EdiValue("9(10)V9(2)", Path = "CCD/18/0", Description = "CTOT")]
    public decimal TotalChargeForChargeType { get; set; }

    [EdiValue("X(4)", Path = "CCD/18/1", Description = "CTOT")]
    public string TotalChargeCreditIndicator { get; set; }


    [EdiValue("X(1)", Path = "CCD/19", Description = "TSUP")]
    public string VatTypeOfSupply { get; set; }

    [EdiValue("X(1)", Path = "CCD/20", Description = "VATC")]
    public VatRateCategoryCode? VatRateCategoryCode { get; set; }

    [EdiValue("9(3)V9(3)", Path = "CCD/21", Description = "VATP")]
    public string VatRatePercentage { get; set; }

    [EdiValue("X(17)", Path = "CCD/22/0", Description = "MSAD")]
    public string MeterSubAddressCode { get; set; }

    [EdiValue("X(40)", Path = "CCD/22/1", Description = "MSAD")]
    public string MeterSubAddressLine { get; set; }

    public override string ToString() {
        return string.Format("SeqNum:{0} Type:{1}", SequenceNumber, ChargeIndicator);
    }
}

[EdiSegment, EdiPath("BTL")]
public class UtilityBillTrailer
{
    //[EdiValue("9(10)", Path = "BTL/0")]
    //public decimal TotalPaymentDetails { get; set; }

    [EdiValue("9(10)", Path = "BTL/1")]
    public decimal TotalChargeBeforeVat { get; set; }

    [EdiValue("9(10)", Path = "BTL/2")]
    public decimal BillTotalVatAmmoutPayable { get; set; }

    //[EdiValue("9(10)", Path = "BTL/3")]
    //public decimal BalanceBroughtForward { get; set; }

    [EdiValue("9(10)", Path = "BTL/4")]
    public decimal TotalBillAmountPayable { get; set; }
    public override string ToString() {
        return string.Format("Net:{0} Vat:{1} Gross:{2}", TotalChargeBeforeVat, BillTotalVatAmmoutPayable, TotalBillAmountPayable);
    }
}

[EdiSegment, EdiPath("MAN")]
public class MetetAdminNumber
{
    static Dictionary<string, string> _distributorsIdentifier = new Dictionary<string, string>();
    public MetetAdminNumber() {
        if (_distributorsIdentifier == null || _distributorsIdentifier.Count() == 0) {
            _distributorsIdentifier.Add("10", "Eastern Electricity");
            _distributorsIdentifier.Add("11", "East Midlands Electricity");
            _distributorsIdentifier.Add("12", "London Electricity");
            _distributorsIdentifier.Add("13", "Manweb");
            _distributorsIdentifier.Add("14", "Midlands Electricity");
            _distributorsIdentifier.Add("15", "Northern Electricity");
            _distributorsIdentifier.Add("16", "NORWEB");
            _distributorsIdentifier.Add("17", "Scottish Hydro Electric");
            _distributorsIdentifier.Add("18", "Scottish Power");
            _distributorsIdentifier.Add("19", "SEEBOARD");
            _distributorsIdentifier.Add("20", "Southern Electric");
            _distributorsIdentifier.Add("21", "SWALEC");
            _distributorsIdentifier.Add("22", "South Western Electricity");
            _distributorsIdentifier.Add("23", "Yorkshire Electricity");
            _distributorsIdentifier.Add("TR", "TRANSCO");
        }
    }
    [EdiValue("9(10)", Path = "MAN/0", Description = "SEQA")]
    public int FirstLevelSequenceNumber { get; set; }

    [EdiValue("9(10)", Path = "MAN/1", Description = "SEQB")]
    public int SecondLevelSequenceNumber { get; set; }

    [EdiValue("X(2)", Path = "MAN/2/0", Description = "MADN")]
    public string DistributorIdentifier { get; set; }
    public string DistributorName {
        get {
            if (_distributorsIdentifier.ContainsKey(DistributorIdentifier))
                return _distributorsIdentifier[DistributorIdentifier];
            else
                throw new ArgumentOutOfRangeException(DistributorIdentifier, "Unregistered distributor identifier");

        }
    }

    [EdiValue("X(10)", Path = "MAN/2/1", Description = "MADN")]
    public string UniqueReferenceNumber { get; set; }

    [EdiValue("9(1)", Path = "MAN/2/2", Description = "MADN")]
    public int? CheckDigit { get; set; }

    [EdiValue("9(2)", Path = "MAN/2/3", Description = "MADN")]
    public int? ProfileType { get; set; }

    [EdiValue("9(3)", Path = "MAN/2/4", Description = "MADN")]
    public int? MeterTimeSwitchDetails { get; set; }

    [EdiValue("9(3)", Path = "MAN/2/5", Description = "MADN")]
    public int? LineLossFactor { get; set; }

    [EdiValue("X(35)", Path = "MAN/3", Description = "MTNR")]
    public string MeterSerialNumber { get; set; }

    [EdiValue("9(1)", Path = "MAN/4", Description = "NDIG")]
    public int? NumberOfDigits { get; set; }

    public override string ToString() {
        return string.Format("SN:{0} PC:{1} LLFC:{2} UniqueId:{3} Distributor:{4}", MeterSerialNumber, ProfileType, LineLossFactor, UniqueReferenceNumber, DistributorIdentifier);
    }

}

[EdiSegment, EdiPath("VAT")]
public class UtilityBillValueAddedTax
{
    [EdiValue("9(10)", Path = "VAT/0", Description = "SEQA")]
    public int FirstLevelSequenceNumber { get; set; }

    [EdiValue("9(3)", Path = "VAT/1", Description = "NDVT")]
    public int? NumberOfDays { get; set; }

    [EdiValue("9(3)V9(3)", Path = "VAT/2", Description = "PNDP")]
    public decimal? PercentageQualifyingFor { get; set; }

    [EdiValue("X(1)", Path = "VAT/3", Description = "VATC")]
    public VatRateCategoryCode VatRateCategoryCode { get; set; }

    [EdiValue("9(3)", Path = "VAT/4", Description = "VATP")]
    public decimal VatRatePercentage { get; set; }

    [EdiValue("9(10)", Path = "VAT/5/0", Description = "UVLA")]
    public decimal TotalChargeBeforeVat { get; set; }

    [EdiValue("9(10)", Path = "VAT/6/0", Description = "UVTT")]
    public decimal VatAmmountPayable { get; set; }

    [EdiValue("9(10)V9(2)", Path = "VAT/7/0", Description = "UCSI")]
    public decimal TotalChargeIncludingVat { get; set; }

    [EdiValue("9(4)", Path = "VAT/8", Description = "NRIL")]
    public int? NumberOfItemLines { get; set; }

    [EdiValue("X(3)", Path = "VAT/9", Description = "RFLV")]
    public ReasonForLowerVatRateType ReasonForLowerZeroVatRate { get; set; }//null-able

    public override string ToString() {
        return string.Format("Net:{0} Vat:{1} Gross:{2}", TotalChargeBeforeVat, VatAmmountPayable, TotalChargeIncludingVat);
    }
}

[EdiSegment, EdiPath("CDA")]
public class ContractData
{
    [EdiValue("X(17)", Path = "CDA/0", Description = "CPSC")]
    public string CurrentPriceScheduleReference { get; set; }

    [EdiValue("X(17)", Path = "CDA/1/0", Description = "ORNO")]
    public string CustomerOrderNumber { get; set; }

    [EdiValue("X(17)", Path = "CDA/1/1", Description = "ORNO")]
    public string SupplierOrderNumber { get; set; }

    [EdiValue("9(6)", Path = "CDA/1/2", Format = "yyMMdd", Description = "ORNO")]
    public DateTime? DateOrderedPlacedByCustomer { get; set; }

    [EdiValue("9(6)", Path = "CDA/1/3", Format = "yyMMdd", Description = "ORNO")]
    public DateTime? DateOrderedReceivedBySupplier { get; set; }

    [EdiValue("9(6)", Path = "CDA/2", Format = "yyMMdd", Description = "INSD")]
    public DateTime? InstallationDate { get; set; }

    [EdiValue("X(3)", Path = "CDA/3", Description = "REPE")]
    public string RentalPeriod { get; set; }

    public override string ToString() {
        return string.Format("CON:{0} SON:{1}", CustomerOrderNumber, SupplierOrderNumber);
    }
}


#region Edi Enumerations

public enum ChargeIndicator
{
    ConsumptionOnly = 1,
    CombinedConsumptionAndCharge = 2,
    ChargeOnly_ConsumptionBased = 3,
    ChargeOnly_Fixed = 4
}
public enum MeasureIndicator
{
    //KVA = Kilovolt - ampere
    //KWH = Kilowatt - hour
    //KW = Kilowatt
    //M3 = Cubic Metre (Gas only)
    //CUFT = Cubic Feet (Gas only)
    //1		=	1 Consumer Unit
}
public enum VatRateCategoryCode
{
    /// <summary>
    /// Edi Value Description : Low Rate
    /// </summary>
    L = 1,

    /// <summary>
    /// Edi Value Description : Standard Rate
    /// </summary>
    S,

    /// <summary>
    /// Edi Value Description : Exemption From VAT
    /// </summary>
    X,

    /// <summary>
    /// Edi Value Description : Services Outside The Scope of VAT
    /// </summary>
    O,

    /// <summary>
    /// Edi Value Description : Zero Rate
    /// </summary>
    Z,

    /// <summary>
    /// Edi Value Description : Standard Rate In Withdrawn Bill
    /// </summary>
    B,

    /// <summary>
    /// Edi Value Description : Lower Rate In Withdrawn Bill
    /// </summary>
    M,

    /// <summary>
    /// Edi Value Description : Zero Rate In Withdrawn Bill
    /// </summary>
    Q,

    /// <summary>
    /// Edi Value Description : Exemption From Vat In Withdrawn Bill
    /// </summary>
    U
}
public enum ReasonForLowerVatRateType
{
    /// <summary>
    /// Edi Value : L
    /// Edi Description : Low Consumption
    /// </summary>
    L = 1,

    /// <summary>
    /// Edi Value : D
    /// Edi Description : Domestic Usage
    /// </summary>
    D,

    /// <summary>
    /// Edi Value : C
    /// Edi Description : Combined
    /// </summary>
    C,

}
public enum ReportPeriod
{
    Monthly = 'M',
    Quarterly = 'Q'
}
public enum BillTypeCode
{
    /// <summary>
    /// Edi Value : F
    /// Edi Description : Final
    /// Business Description : Last bill when an account is closed
    /// </summary>
    F = 1,

    /// <summary>
    /// Edi Value : N
    /// Edi Description : Normal
    /// Business Description : Normal Invoice
    /// </summary>
    N,

    /// <summary>
    /// Edi Value : W
    /// Edi Description : Withdrawn
    /// Business Description : Reverses a previous bill
    /// </summary>
    W
}
public enum ReadingDataType
{
    NormalReading = 00,
    Estimated_Computer_Reading = 02,
    CustomersOwnReading = 04,
    ExchangeMeterReading = 06,
    ThirdPartyNormalReading = 09,
    ThirdPartyEstimated_Computer_Reading = 11,
    ReadingForInformationOnly = 12,
}
public enum DistributionBusinessCodes
{
    Eastern_Electricity = 10,
    East_Midlands_Electricity = 11,
    London_Electricity = 12,
    Manweb = 13,
    Midlands_Electricity = 14,
    Northern_Electricity = 15,
    NORWEB = 16,
    Scottish_Hydro_Electric = 17,
    Scottish_Power = 18,
    SEEBOARD = 19,
    Southern_Electric = 20,
    SWALEC = 21,
    South_Western_Electricity = 22,
    Yorkshire_Electricity = 23,
    //TRANSCO = TR
}

#endregion
