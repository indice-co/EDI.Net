using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Discrepancy, coded
/// </summary>
public class DiscrepancyCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DiscrepancyCoded(string s) => new DiscrepancyCoded { Code = s };

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
		{ "AA", "Item discontinued by wholesaler" },
		{ "AB", "Item no longer produced" },
		{ "AC", "Over-shipped" },
		{ "AD", "Item out of stock at manufacturer" },
		{ "AE", "Delivered but not advised" },
		{ "AF", "Goods delivered damaged" },
		{ "AG", "Delivered too late" },
		{ "AN", "Available now - no shipping schedule" },
		{ "AS", "Available now - scheduled to ship (date)" },
		{ "BK", "Back ordered from previous order" },
		{ "BP", "Shipment partial - back order to follow" },
		{ "CA", "Customer inquiry - all items" },
		{ "CC", "Shipment complete" },
		{ "CE", "Shipment includes extra items to meet price break" },
		{ "CI", "Customer inquiry - shipped items only" },
		{ "CK", "Cancelled from previous order" },
		{ "CM", "Shipment complete with additional quantity" },
		{ "CN", "Next carrier, PVE - (date)" },
		{ "CO", "Customer inquiry - unshipped items only" },
		{ "CP", "Shipment partial - considered complete, no backorder" },
		{ "CS", "Shipment complete with substitution" },
		{ "IC", "Item cancelled" },
		{ "IS", "Item represents substitution from original order" },
		{ "LS", "Last shipment (date)" },
		{ "NF", "Not yet published" },
		{ "NN", "Not in process - no shipping schedule" },
		{ "NS", "Not in process - schedule to ship (date)" },
		{ "OF", "Order sent to factory for production (date)" },
		{ "OM", "Item sent to factory for production (date)" },
		{ "OP", "Out of print" },
		{ "OS", "Item out of stock because of strike of force majeure" },
		{ "OW", "Item out of stock at wholesaler" },
		{ "PA", "Purchase order inquiry - all items" },
		{ "PD", "Purchase order complete" },
		{ "PI", "Purchase order inquiry - shipped items only" },
		{ "PK", "Packed-to-date (date)" },
		{ "PN", "In process - no shipping schedule" },
		{ "PO", "Purchase order inquiry - unshipped items only" },
		{ "PP", "Purchase order inquiry - specific items" },
		{ "PS", "In process - scheduled to ship (date)" },
		{ "RA", "Item rationed" },
		{ "SL", "Shipped-to-date (date)" },
		{ "SP", "Scheduled for production at factory" },
		{ "SS", "Split shipment" },
		{ "TW", "Item temporary discontinued by wholesaler" },
		{ "UR", "Unsolicited report" },
		{ "ZZZ", "Mutually defined" },
	};
}