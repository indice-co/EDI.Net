using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;


internal class Interchange_Issue272_ValueRange
{
    public PFDSegment PFD { get; set; }

    [EdiSegment, EdiPath("PFD")]
    public class PFDSegment
    {
        [EdiPath("PFD/0..*")]
        public List<PFD_SeatDefinition> SeatDefinitions { get; set; } = new List<PFD_SeatDefinition>();
    }

    [EdiElement]
    public class PFD_SeatDefinition
    {
        [EdiValue(Path = "*/*/0")]
        public string PFD_NoSmokingIndicator { get; set; }

        [EdiValue(Path = "*/*/1")]
        public string PFD_CabinClassDesignator { get; set; }

        [EdiValue(Path = "*/*/2")]
        public string PFD_CabinClassNumber { get; set; }

        [EdiValue(Path = "*/*/3")]
        public string PFD_FreeTextCabinClassZone { get; set; }

        [EdiValue(Path = "*/*/4")]
        public string PFD_SegmentAirportOfDeparture { get; set; }

        [EdiValue(Path = "*/*/5")]
        public string PFD_SegmentAirportOfArrival { get; set; }

        [EdiValue(Path = "*/*/6")]
        public string PFD_EquipmentCode { get; set; }

        [EdiValue(Path = "*/*/7")]
        public string PFD_SeatRequestFullfilledIndicator { get; set; }

        [EdiValue(Path = "*/*/8..12")]
        public List<string> PFD_SeatCharacteristics { get; set; }
    }
}
