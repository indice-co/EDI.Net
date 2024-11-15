using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;
public class Interchange_Issue272
{
    public PFD PFD { get; set; }
}

[EdiSegment, EdiPath("PFD")]
public class PFD
{
    [EdiValue(Path = "PFD/0/0")]
    public string PFD_01A_SpecificSeat { get; set; }

    [EdiValue(Path = "PFD/0/1")]
    public string PFD_01B_FreeSeatingReference { get; set; }

    [EdiValue(Path = "PFD/1/0")]
    public string PFD_02A_NoSmokingIndicator { get; set; }

    [EdiValue(Path = "PFD/1/1")]
    public string PFD_02B_CabinClassDesignator { get; set; }

    [EdiValue(Path = "PFD/1/2")]
    public string PFD_02C_CabinClassNumber { get; set; }

    [EdiValue(Path = "PFD/1/3")]
    public string PFD_02D_FreeTextCabinClassZone { get; set; }

    [EdiValue(Path = "PFD/1/4")]
    public string PFD_02E_SegmentAirportOfDeparture { get; set; }

    [EdiValue(Path = "PFD/1/5")]
    public string PFD_02F_SegmentAirportOfArrival { get; set; }

    [EdiValue(Path = "PFD/1/6")]
    public string PFD_02G_EquipmentCode { get; set; }

    [EdiValue(Path = "PFD/1/7")]
    public string PFD_02H_SeatRequestFullfilledIndicator { get; set; }

    [EdiValue(Path = "PFD/1/8")]
    public string PFD_02I_SeatCharacteristics { get; set; }

    [EdiValue(Path = "PFD/1/9")]
    public string PFD_02J_SeatCharacteristics { get; set; }

    [EdiValue(Path = "PFD/1/10")]
    public string PFD_02K_SeatCharacteristics { get; set; }

    [EdiValue(Path = "PFD/1/11")]
    public string PFD_02L_SeatCharacteristics { get; set; }

    [EdiValue(Path = "PFD/1/12")]
    public string PFD_02M_SeatCharacteristics { get; set; }

    [EdiValue(Path = "PFD/2/0")]
    public string PFD_07_PaxIdForBPPrint { get; set; }

    [EdiValue(Path = "PFD/2/1")]
    public string PFD_07_InfantIdForBPPrint { get; set; }

    [EdiValue(Path = "PFD/3/0")]
    public string PFD_08_IssueReissueBoardingPass { get; set; }

    [EdiValue(Path = "PFD/4/0")]
    public string PFD_09_BarcodeForBoardingPass { get; set; }

    [EdiValue(Path = "PFD/5/0")]
    public string PFD_10_PaxPriorityInformation { get; set; }

    [EdiPath("PFD/6/0..*")]
    public PFD_SeatDetail[] SeatDetails { get; set; }

    [EdiPath("PFD/7/0..*")]
    public PFD_SeatDefinition[] SeatDefinitions { get; set; }
}

[EdiElement]
public class PFD_SeatDetail
{
    [EdiValue(Path = "*/*/0")]
    public string PFD_11A_SpecificSeat { get; set; }

    [EdiValue(Path = "*/*/1")]
    public string PFD_11B_FreeSeatingReference { get; set; }
}

[EdiElement]
public class PFD_SeatDefinition
{
    [EdiValue(Path = "*/*/0")]
    public string PFD_12A_NoSmokingIndicator { get; set; }

    [EdiValue(Path = "*/*/1")]
    public string PFD_12B_CabinClassDesignator { get; set; }

    [EdiValue(Path = "*/*/2")]
    public string PFD_12C_CabinClassNumber { get; set; }

    [EdiValue(Path = "*/*/3")]
    public string PFD_12D_FreeTextCabinClassZone { get; set; }

    [EdiValue(Path = "*/*/4")]
    public string PFD_12E_SegmentAirportOfDeparture { get; set; }

    [EdiValue(Path = "*/*/5")]
    public string PFD_12F_SegmentAirportOfArrival { get; set; }

    [EdiValue(Path = "*/*/6")]
    public string PFD_12G_EquipmentCode { get; set; }

    [EdiValue(Path = "*/*/7")]
    public string PFD_12H_SeatRequestFullfilledIndicator { get; set; }

    [EdiValue(Path = "*/*/8")]
    public string PFD_12I_SeatCharacteristics { get; set; }

    [EdiValue(Path = "*/*/9")]
    public string PFD_12J_SeatCharacteristics { get; set; }

    [EdiValue(Path = "*/*/10")]
    public string PFD_12K_SeatCharacteristics { get; set; }

    [EdiValue(Path = "*/*/11")]
    public string PFD_12L_SeatCharacteristics { get; set; }

    [EdiValue(Path = "*/*/12")]
    public string PFD_12M_SeatCharacteristics { get; set; }
}
