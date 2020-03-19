using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    /// <summary>
    /// The wikipedia issue :)
    /// </summary>
    public class EdiFact_Issue152_Wikipedia_Transmission
    {
        public Message AvailabilityRequest { get; set; }

        [EdiMessage]
        public class Message
        {
            public MSG MessageType { get; set; }
            public IFT Description { get; set; }
            public ERC ErrorCode { get; set; }
            public ODI ODI { get; set; }
            public List<TVL_SG> Legs { get; set; }
        }

        /// <summary>
        /// MESSAGE TYPE IDENTIFICATION
        /// </summary>
        [EdiSegment, EdiPath("MSG")]
        public class MSG
        {
            public MessageIdentifier MessageIdentifier { get; set; }
            [EdiValue("X(3)", Path = "*/1/0")]
            public string DesignatedClassCode { get; set; }
            [EdiValue("X(3)", Path = "*/2/0")]
            public string MaintenanceOperationCode { get; set; }
            public Relationship Relationship { get; set; }
        }

        /// <summary>
        ///  C709
        /// </summary>
        [EdiElement, EdiPath("*/3")]
        public class Relationship
        {
            [EdiValue("X(3)", Path = "*/*/0")]
            public string RelationshipDescriptionCode { get; set; }
            [EdiValue("X(17)", Path = "*/*/1")]
            public string CodeListIdentificationCode { get; set; }
            [EdiValue("X(9)", Path = "*/*/2")]
            public string CodeListResponsibleAgencyCode { get; set; }
            [EdiValue("X(2)", Path = "*/*/3")]
            public string RelationshipDescription { get; set; }
        }


        /// <summary>
        /// C941
        /// </summary>
        [EdiElement, EdiPath("*/0")]
        public class MessageIdentifier
        {
            [EdiValue("X(6)", Path = "*/*/0")]
            public string MessageTypeCode { get; set; }
            [EdiValue("X(9)", Path = "*/*/1")]
            public string VersionIdentifier { get; set; }
            [EdiValue("X(9)", Path = "*/*/2")]
            public string ReleaseIdentifier { get; set; }
            [EdiValue("X(2)", Path = "*/*/3")]
            public string ControllingAgencyIdentifier { get; set; }
            [EdiValue("X(6)", Path = "*/*/4")]
            public string MessageImplementationIdentificationCode { get; set; }
            [EdiValue("X(6)", Path = "*/*/5")]
            public string RevisionIdentifier { get; set; }
            [EdiValue("X(3)", Path = "*/*/6")]
            public string DocumentStatusCode { get; set; }
        }

        /// <summary>
        /// ORIGIN AND DESTINATION DETAILS
        /// </summary>
        [EdiSegment, EdiPath("ODI")]
        public class ODI
        {
            [EdiValue("X(35)", Path = "*/0/0")]
            public string LocationNameCode { get; set; }

            [EdiValue("X(10)", Path = "*/0/1")]
            public string SequencePositionIdentifier { get; set; }
        }

        /// <summary>
        /// interactive free text
        /// </summary>
        [EdiSegment, EdiPath("IFT")]
        public class IFT
        {
            [EdiValue("9(6)", Path = "*/0/0")]
            public int Code { get; set; }
            [EdiValue("X(50)", Path = "*/1/0")]
            public string Text { get; set; }
            public override string ToString() => Text ?? base.ToString();
        }


        [EdiSegmentGroup("TVL", "PDI", "APD")]
        public class TVL_SG : TVL
        {
            public PDI PersonDemographicInformation { get; set; }
            public APD AdditionalTransportDetails { get; set; }
        }

        /// <summary>
        /// departure date/time, origin, destination, operating airline code, flight
        /// number, and operation suffix.
        /// </summary>
        [EdiSegment, EdiPath("TVL")]
        public class TVL
        {

            [EdiValue("9(6)", Path = "*/0/0", Format = "ddMMyy", Description = "Date")]
            [EdiValue("9(4)", Path = "*/0/1", Format = "HHmm", Description = "Time")]
            public DateTime? Departure { get; set; }


            private DateTime? _Arrival;
            [EdiValue("9(6)", Path = "*/0/2", Format = "ddMMyy", Description = "Date")]
            [EdiValue("9(4)", Path = "*/0/3", Format = "HHmm", Description = "Time")]
            public DateTime? Arrival { 
                get => _Arrival ?? Departure?.Date; 
                set => _Arrival = value;
            }

            [EdiValue("X(3)", Path = "*/1/0")]
            public string OriginAirport { get; set; }
            [EdiValue("X(3)", Path = "*/2/0")]
            public string DestinationAirport { get; set; }
            [EdiValue("X(3)", Path = "*/3/0")]
            public string OperatingAirlineCode { get; set; }
            [EdiValue("X(10)", Path = "*/4/0")]
            public string FlightNumber { get; set; }
            public override string ToString() => $"{OperatingAirlineCode}{FlightNumber} from {OriginAirport} {Departure:d} {Departure:HH:mm} to {DestinationAirport} {Arrival:d} {Arrival:HH:mm}";
        }

        /// <summary>
        /// ADDITIONAL TRANSPORT DETAILS
        /// </summary>
        [EdiSegment, EdiPath("APD")]
        public class APD
        {
            [EdiPath("*/0")]
            public TransportDetails TransportDetails { get; set; }

            [EdiPath("*/1")]
            public TerminalInformation TerminalInformation { get; set; }
        }

        /// <summary>
        /// PERSON DEMOGRAPHIC INFORMATION
        /// </summary>
        [EdiSegment, EdiPath("PDI")]
        public class PDI
        {

            [EdiValue("X(3)", Path = "*/0/0")]
            public string GenderCode { get; set; }
            [EdiPath("*/1")]
            public MaritalStatusDetails MaritalStatusDetails { get; set; }
            [EdiPath("*/2")]
            public RelegionDetails RelegionDetails { get; set; }

        }

        /// <summary>
        /// C085
        /// </summary>
        [EdiElement]
        public class MaritalStatusDetails                     
        {
            [EdiValue("X(3)", Path = "*/*/0")]
            public string MaritalStatusDescriptionCode  { get; set; }
            [EdiValue("X(17)", Path = "*/*/1")]
            public string CodeListIdentificationCode { get; set; }
            [EdiValue("X(3)", Path = "*/*/2")]
            public string CodeListResponsibleAgencyCode { get; set; }
            [EdiValue("X(35)", Path = "*/*/3")]
            public string MaritalStatusDescription { get; set; }
        }

        /// <summary>
        /// C101
        /// </summary>
        [EdiElement]
        public class RelegionDetails
        {
            [EdiValue("X(3)", Path = "*/*/0")]
            public string ReligionNameCode { get; set; }
            [EdiValue("X(17)", Path = "*/*/1")]
            public string CodeListIdentificationCode { get; set; }
            [EdiValue("X(3)", Path = "*/*/2")]
            public string CodeListResponsibleAgencyCode { get; set; }
            [EdiValue("X(35)", Path = "*/*/3")]
            public string ReligionName { get; set; }
        }


        /// <summary>
        /// 
        /// </summary>
        [EdiElement]
        public class TransportDetails
        {
            [EdiValue("X(8)", Path = "*/*/0")]
            public string TransportMeansDescriptionCode { get; set; }
            [EdiValue("X(3)", Path = "*/*/1")]
            public int NumberOfStops { get; set; }
            [EdiValue("X(6)", Path = "*/*/2")]
            public string LegDuration { get; set; }
            [EdiValue("X(10)", Path = "*/*/3")]
            public decimal? Percentage { get; set; }
            [EdiValue("X(7)", Path = "*/*/4")]
            public int DaysOfOperation { get; set; }
            [EdiValue("X(35)", Path = "*/*/5")]
            public string DateTimePeriodValue { get; set; }
            [EdiValue("X(1)", Path = "*/*/6")]
            public string ComplexingTransportIndicator { get; set; }
            [EdiValue("X(25)", Path = "*/*/7")]
            public string LocationNameCode1 { get; set; }
            [EdiValue("X(25)", Path = "*/*/8")]
            public string LocationNameCode2 { get; set; }
            [EdiValue("X(25)", Path = "*/*/9")]
            public string LocationNameCode3 { get; set; }
        }

        [EdiElement]
        public class TerminalInformation 
        {
            [EdiValue("X(6)", Path = "*/*/0")]
            public string GateIdentification  { get; set; }
            [EdiValue("X(25)", Path = "*/*/1")]
            public string RelatedPlaceLocationOneIdentification { get; set; }
            [EdiValue("X(25)", Path = "*/*/2")]
            public string RelatedPlaceLocationOneIdentification1 { get; set; }
        }

        /// <summary>
        /// APPLICATION ERROR INFORMATION
        /// </summary>
        [EdiSegment, EdiPath("ERC")]
        public class ERC
        {

            [EdiValue("X(8)", Path = "*/0/0")]
            public string ErrorCode { get; set; }

            [EdiValue("X(3)", Path = "*/0/0")]
            public string IdentificationCode { get; set; }

            [EdiValue("X(3)", Path = "*/0/0")]
            public string ResponsibleAgencyCode { get; set; }

            public override string ToString() => ErrorCode ?? base.ToString();
        }
    }
}
