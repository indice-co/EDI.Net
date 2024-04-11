using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Document/message status, coded
/// </summary>
public class DocumentMessageStatusCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator DocumentMessageStatusCoded(string s) => new DocumentMessageStatusCoded { Code = s };

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
		{ "1", "Accepted" },
		{ "2", "Accompanying goods" },
		{ "3", "Conditionally accepted" },
		{ "4", "To arrive by separate EDI message" },
		{ "5", "Information only" },
		{ "6", "To arrive by manual means" },
		{ "7", "To be raised and sent" },
		{ "8", "Rejected" },
		{ "9", "To be printed" },
		{ "10", "Document currently valid" },
		{ "11", "Document not available" },
		{ "12", "Document exhausted by declaration and attached" },
		{ "13", "Document not exhausted by declaration and attached" },
		{ "14", "Document exhausted by declaration and previously lodged" },
		{ "15", "Document not exhausted by declaration and previously lodged" },
		{ "16", "Document not attached" },
		{ "17", "Document with the goods" },
		{ "18", "Document attached, to be returned after endorsement" },
		{ "19", "Document applied for" },
		{ "20", "Received for shipment" },
		{ "21", "Shipped on board" },
		{ "22", "Status 0" },
		{ "23", "Status 1" },
		{ "24", "Status 2" },
		{ "25", "Message under development" },
	};
}