using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Date/time/period qualifier
/// </summary>
public class DateTimePeriodQualifier
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DateTimePeriodQualifier(string s) => new DateTimePeriodQualifier { Code = s };

	/// <summary>
	/// Code of the value
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// Value converted from code
	/// </summary>
	public string? Value => Code is null ? null : (Qualifiers.ContainsKey(Code) ? Qualifiers[Code] : null);

	/// <summary>
	/// All possible values
	/// </summary>
	[JsonIgnore]
	public Dictionary<string, string> Qualifiers => new Dictionary<string, string>
	{
		{ "2", "Delivery date/time, requested" },
		{ "3", "Invoice date/time" },
		{ "4", "Order date/time" },
		{ "7", "Effective date/time" },
		{ "8", "Order received date/time" },
		{ "9", "Processing date/time" },
		{ "10", "Shipment date/time, requested" },
		{ "11", "Despatch date and or time" },
		{ "12", "Terms discount due date/time" },
		{ "13", "Terms net due date" },
		{ "14", "Payment date/time, deferred" },
		{ "15", "Promotion start date/time" },
		{ "16", "Promotion end date/time" },
		{ "17", "Delivery date/time, estimated" },
		{ "18", "Installation date/time/period" },
		{ "20", "Cheque date/time" },
		{ "21", "Charge back date/time" },
		{ "22", "Freight bill date/time" },
		{ "35", "Delivery date/time, actual" },
		{ "36", "Expiry date" },
		{ "37", "Ship not before date/time" },
		{ "38", "Ship not later than date/time" },
		{ "39", "Ship week of date" },
		{ "42", "Superseded date/time" },
		{ "44", "Availability" },
		{ "45", "Compilation date and time" },
		{ "46", "Cancellation date" },
		{ "47", "Statistical time series date" },
		{ "48", "Duration" },
		{ "49", "Deliver not before and not after dates" },
		{ "50", "Goods receipt date/time" },
		{ "51", "Cumulative quantity start date" },
		{ "52", "Cumulative quantity end date" },
		{ "53", "Buyer's local time" },
		{ "54", "Seller's local time" },
		{ "55", "Confirmed date/time" },
		{ "58", "Clearance date (Customs)" },
		{ "59", "Inbond movement authorization date" },
		{ "60", "Engineering change level date" },
		{ "61", "Cancel if not delivered by this date" },
		{ "63", "Delivery date/time, latest" },
		{ "64", "Delivery date/time, earliest" },
		{ "65", "Delivery date/time, 1st schedule" },
		{ "67", "Delivery date/time, current schedule" },
		{ "69", "Delivery date/time, promised for" },
		{ "71", "Delivery date/time, requested for (after and including)" },
		{ "72", "Delivery date/time, promised for (after and including)" },
		{ "74", "Delivery date/time, requested for (prior to and including)" },
		{ "75", "Delivery date/time, promised for (prior to and including)" },
		{ "76", "Delivery date/time, scheduled for" },
		{ "79", "Shipment date/time, promised for" },
		{ "81", "Shipment date/time, requested for (after and including)" },
		{ "84", "Shipment date/time, requested for (prior to and including)" },
		{ "85", "Shipment date/time, promised for (prior to and including)" },
		{ "89", "Inquiry date" },
		{ "90", "Report start date" },
		{ "91", "Report end date" },
		{ "92", "Contract effective date" },
		{ "93", "Contract expiry date" },
		{ "94", "Production/manufacture date" },
		{ "95", "Bill of lading date" },
		{ "96", "Discharge date/time" },
		{ "97", "Transaction creation date" },
		{ "101", "Production date, no schedule established as of" },
		{ "107", "Deposit date/time" },
		{ "108", "Postmark date/time" },
		{ "109", "Receive at lockbox date" },
		{ "110", "Ship date, originally scheduled" },
		{ "111", "Manifest/ship notice date" },
		{ "113", "Sample required date" },
		{ "114", "Tooling required date" },
		{ "115", "Sample available date" },
		{ "116", "Equipment return period, expected" },
		{ "117", "Delivery date/time, first" },
		{ "118", "Cargo booking confirmed date/time" },
		{ "119", "Test completion date" },
		{ "123", "Documentary credit expiry date/time" },
		{ "124", "Despatch note date" },
		{ "125", "Import licence date" },
		{ "126", "Contract date" },
		{ "128", "Delivery date/time, last" },
		{ "129", "Exportation date" },
		{ "131", "Tax point date" },
		{ "132", "Arrival date/time, estimated" },
		{ "133", "Departure date/time, estimated" },
		{ "134", "Rate of exchange date/time" },
		{ "135", "Telex date" },
		{ "136", "Departure date/time" },
		{ "137", "Document/message date/time" },
		{ "138", "Payment date" },
		{ "140", "Payment due date" },
		{ "141", "Presentation date of Goods declaration (Customs)" },
		{ "142", "Labour wage determination date" },
		{ "143", "Acceptance date/time of goods" },
		{ "144", "Quota date" },
		{ "145", "Event date" },
		{ "146", "Entry date, estimated (Customs)" },
		{ "147", "Expiry date of export licence" },
		{ "148", "Acceptance date of Goods declaration (Customs)" },
		{ "149", "Invoice date, required" },
		{ "150", "Declaration/presentation date" },
		{ "151", "Importation date" },
		{ "152", "Exportation date for textiles" },
		{ "153", "Cancellation date/time, latest" },
		{ "154", "Acceptance date of document" },
		{ "155", "Accounting period start date" },
		{ "156", "Accounting period end date" },
		{ "157", "Validity start date" },
		{ "158", "Horizon start date" },
		{ "159", "Horizon end date" },
		{ "160", "Authorization date" },
		{ "161", "Release date of customer" },
		{ "162", "Release date of supplier" },
		{ "163", "Processing start date/time" },
		{ "164", "Processing end date/time" },
		{ "165", "Tax period start date" },
		{ "166", "Tax period end date" },
		{ "167", "Charge period start date" },
		{ "168", "Charge period end date" },
		{ "169", "Lead time" },
		{ "170", "Settlement due date" },
		{ "171", "Reference date/time" },
		{ "172", "Hired from date" },
		{ "173", "Hired until date" },
		{ "174", "Advise after date/time" },
		{ "175", "Advise before date/time" },
		{ "176", "Advise completed date/time" },
		{ "177", "Advise on date/time" },
		{ "178", "Arrival date/time, actual" },
		{ "179", "Booking date/time" },
		{ "180", "Closing date/time" },
		{ "181", "Positioning date/time of equipment" },
		{ "182", "Issue date" },
		{ "183", "Date, as at" },
		{ "184", "Notification date/time" },
		{ "185", "Commenced tank cleaning date/time" },
		{ "186", "Departure date/time, actual" },
		{ "187", "Authentication date/time of document" },
		{ "188", "Previous current account date" },
		{ "189", "Departure date/time, scheduled" },
		{ "190", "Transhipment date/time" },
		{ "191", "Delivery date/time, expected" },
		{ "192", "Expiration date/time of customs document" },
		{ "193", "Execution date" },
		{ "194", "Start date/time" },
		{ "195", "Expiry date of import licence" },
		{ "196", "Departure date/time, earliest" },
		{ "197", "Laytime first day" },
		{ "198", "Laytime last day" },
		{ "199", "Positioning date/time of goods" },
		{ "200", "Pick-up/collection date/time of cargo" },
		{ "201", "Pick-up date/time of equipment" },
		{ "202", "Posting date" },
		{ "203", "Execution date/time, requested" },
		{ "204", "Release date (Customs)" },
		{ "205", "Settlement date" },
		{ "206", "End date/time" },
		{ "207", "Commenced pumping ballast date/time" },
		{ "208", "Departure date/time, ultimate" },
		{ "209", "Value date" },
		{ "210", "Reinsurance current account period" },
		{ "211", "360/30" },
		{ "212", "360/28-31" },
		{ "213", "365-6/30" },
		{ "214", "365-6/28-31" },
		{ "215", "365/28-31" },
		{ "216", "365/30" },
		{ "217", "From date of award to latest delivery" },
		{ "218", "Authentication/validation date/time" },
		{ "219", "Crossborder date/time" },
		{ "221", "Interest period" },
		{ "222", "Presentation date, latest" },
		{ "223", "Delivery date/time, deferred" },
		{ "224", "Permit to admit date" },
		{ "225", "Certification of weight date/time" },
		{ "226", "Discrepancy date/time" },
		{ "227", "Beneficiary's banks due date" },
		{ "228", "Debit value date, requested" },
		{ "229", "Hoses connected date/time" },
		{ "230", "Hoses disconnected date/time" },
		{ "231", "Arrival date/time, earliest" },
		{ "232", "Arrival date/time, scheduled" },
		{ "233", "Arrival date/time, ultimate" },
		{ "234", "Collection date/time, earliest" },
		{ "235", "Collection date/time, latest" },
		{ "236", "Completed pumping ballast date/time" },
		{ "237", "Completed tank cleaning date/time" },
		{ "238", "Tanks accepted date/time" },
		{ "239", "Tanks inspected date/time" },
		{ "240", "Reinsurance accounting period" },
		{ "241", "From date of award to earliest delivery" },
		{ "242", "Preparation date/time of document" },
		{ "243", "Transmission date/time of document" },
		{ "244", "Settlement date, planned" },
		{ "245", "Underwriting year" },
		{ "246", "Accounting year" },
		{ "247", "Year of occurrence" },
		{ "248", "Loss" },
		{ "249", "Cash call date" },
		{ "250", "Re-exportation date" },
		{ "251", "Re-importation date" },
		{ "252", "Arrival date/time at initial port" },
		{ "253", "Departure date/time from last port of call" },
		{ "254", "Registration date of previous Customs declaration" },
		{ "255", "Availability due date" },
		{ "256", "From date of award to completion" },
		{ "257", "Calculation date" },
		{ "258", "Guarantee date (Customs)" },
		{ "259", "Conveyance registration date" },
		{ "260", "Valuation date (Customs)" },
		{ "261", "Release date/time" },
		{ "262", "Closure date/time/period" },
		{ "263", "Invoicing period" },
		{ "264", "Release frequency" },
		{ "265", "Due date" },
		{ "266", "Validation date" },
		{ "267", "Rate/price date/time" },
		{ "268", "Transit time/limits" },
		{ "270", "Ship during date" },
		{ "271", "Ship on or about date" },
		{ "272", "Documentary credit presentation period" },
		{ "273", "Validity period" },
		{ "274", "From date of order receipt to sample ready" },
		{ "275", "From date of tooling authorization to sample ready" },
		{ "276", "From date of receipt of tooling aids to sample ready" },
		{ "277", "From date of sample approval to first product shipment" },
		{ "278", "From date of order receipt to shipment" },
		{ "279", "From date of order receipt to delivery" },
		{ "280", "From last booked order to delivery" },
		{ "281", "Date of order lead time" },
		{ "282", "Confirmation date lead time" },
		{ "283", "Arrival date/time of transport lead time" },
		{ "284", "Before inventory is replenished based on stock check lead time" },
		{ "285", "Invitation to tender date/time" },
		{ "286", "Tender submission date/time" },
		{ "287", "Contract award date/time" },
		{ "288", "Price base date/time" },
		{ "290", "Contractual start date/time" },
		{ "291", "Start date/time, planned" },
		{ "292", "Works completion date/time, planned" },
		{ "293", "Works completion date/time, actual" },
		{ "294", "Hand over date/time, planned" },
		{ "295", "Hand over date/time, actual" },
		{ "296", "Retention release date/time" },
		{ "297", "Retention release date/time, partial" },
		{ "298", "Escalation start date" },
		{ "299", "Price adjustment start date" },
		{ "300", "Price adjustment limit date" },
		{ "301", "Value date of index" },
		{ "302", "Publication date" },
		{ "303", "Escalation date" },
		{ "304", "Price adjustment date" },
		{ "305", "Latest price adjustment date" },
		{ "306", "Work period" },
		{ "307", "Payment instruction date/time" },
		{ "308", "Payment valuation presentation date/time" },
		{ "309", "Blanks value date" },
		{ "310", "Received date/time" },
		{ "311", "On" },
		{ "312", "Ship not before and not after date/time" },
		{ "313", "Order to proceed date" },
		{ "314", "Planned duration of works" },
		{ "315", "Agreement to pay date" },
		{ "316", "Valuation date/time" },
		{ "317", "Reply date" },
		{ "318", "Request date" },
		{ "319", "Customer value date" },
		{ "320", "Declaration reference period" },
		{ "321", "Promotion date/period" },
		{ "322", "Accounting period" },
		{ "323", "Horizon period" },
		{ "324", "Processing date/period" },
		{ "325", "Tax period" },
		{ "326", "Charge period" },
		{ "327", "Instalment payment due date" },
		{ "328", "Payroll deduction date/time" },
		{ "329", "Birth date/time" },
		{ "330", "Joined employer date" },
		{ "331", "Contributions ceasing date/time" },
		{ "332", "Contribution period end date/time" },
		{ "333", "Part-time working change date/time" },
		{ "334", "Status change date/time" },
		{ "335", "Contribution period start date/time" },
		{ "336", "Salary change effective date" },
		{ "337", "Left employer date" },
		{ "338", "Benefit change date/time" },
		{ "339", "Category change date/time" },
		{ "340", "Joined fund date/time" },
		{ "341", "Waiting time" },
		{ "342", "On-board date" },
		{ "343", "Date/time of discount termination" },
		{ "344", "Date/time of interest due" },
		{ "345", "Days of operation" },
		{ "346", "Latest check-in time" },
		{ "347", "Slaughtering start date" },
		{ "348", "Packing start date" },
		{ "349", "Packing end date" },
		{ "350", "Test start date" },
		{ "351", "Inspection date" },
		{ "352", "Slaughtering end date" },
		{ "353", "Accounting transaction date" },
		{ "354", "Activity period date range" },
		{ "355", "Contractual delivery date" },
		{ "356", "Sales date, and or time, and or period" },
		{ "357", "Cancel if not published by this date" },
		{ "358", "Scheduled for delivery on or after" },
		{ "359", "Scheduled for delivery on or before" },
		{ "360", "Sell by date" },
		{ "361", "Best before date" },
		{ "362", "End availability date" },
		{ "363", "Total shelf life period" },
		{ "364", "Minimum shelf life remaining at time of despatch period" },
		{ "365", "Packaging date" },
		{ "366", "Inventory report date" },
		{ "367", "Previous meter reading date" },
		{ "368", "Latest meter reading date" },
		{ "369", "Date and or time of handling, estimated" },
		{ "370", "Date when container equipment becomes domestic" },
		{ "371", "Hydrotest date" },
		{ "372", "Equipment pre-trip date" },
		{ "373", "Mooring, date and time" },
		{ "374", "Road fund tax expiry date" },
		{ "375", "Date of first registration" },
		{ "376", "Bi-annual terminal inspection date" },
		{ "377", "Federal HighWay Administration (FHWA) inspection date" },
		{ "378", "Container Safety Convention (CSC) inspection date" },
		{ "379", "Periodic inspection date" },
		{ "380", "Drawing revision date" },
		{ "381", "Product lifespan at time of production" },
		{ "382", "Earliest sale date" },
		{ "383", "Cancel if not shipped by this date" },
		{ "384", "Previous invoice date" },
		{ "387", "Repair turnaround time" },
		{ "388", "Order amendment binding date" },
		{ "389", "Cure time" },
		{ "390", "From date of award to delivery" },
		{ "391", "From date of receipt of item to approval" },
		{ "392", "Equipment collection or pick-up date/time, earliest" },
		{ "393", "Equipment collection or pick-up date/time, planned" },
		{ "394", "Equipment positioning date/time, actual" },
		{ "395", "Equipment positioning date/time, estimated" },
		{ "396", "Equipment positioning date/time, requested" },
		{ "397", "Equipment positioning date/time, ultimate" },
		{ "398", "Goods collection or pick-up date/time, planned" },
		{ "399", "Goods positioning date/time, expected" },
		{ "400", "Cargo release date/time, ultimate" },
		{ "401", "Container Safety Convention (CSC) plate expiration date" },
		{ "402", "Document received date/time" },
		{ "403", "Discharge date/time, actual" },
		{ "404", "Loading date/time, actual" },
		{ "405", "Equipment collection or pick-up date/time, actual" },
		{ "406", "Goods positioning date/time, planned" },
		{ "407", "Document requested date/time" },
		{ "408", "Expected container hire from date/time" },
		{ "409", "Order completion date/time, ultimate" },
		{ "410", "Equipment repair ready date/time, ultimate" },
		{ "411", "Container stuffing date/time, ultimate" },
		{ "412", "Container stripping date/time, ultimate" },
		{ "413", "Discharge and loading completed date/time" },
		{ "414", "Equipment stock check date/time" },
		{ "415", "Activity reporting date" },
		{ "416", "Submission date" },
		{ "417", "Previous booking date/time" },
		{ "ZZZ", "Mutually defined" },
	};
}