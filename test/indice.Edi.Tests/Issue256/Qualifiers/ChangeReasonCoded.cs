using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Change reason, coded
/// </summary>
public class ChangeReasonCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ChangeReasonCoded(string s) => new ChangeReasonCoded { Code = s };

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
		{ "AA", "Member attribute change" },
		{ "AB", "Abroad" },
		{ "AC", "Member category change" },
		{ "AD", "Death" },
		{ "AE", "Disability" },
		{ "AF", "Early retirement" },
		{ "AG", "Hardship" },
		{ "AH", "Ill health" },
		{ "AI", "Leaving employer" },
		{ "AJ", "Leaving industry" },
		{ "AK", "Level/rate table change" },
		{ "AL", "Normal retirement" },
		{ "AM", "Other" },
		{ "AN", "Retrenchment" },
		{ "AO", "Resignation" },
		{ "AP", "Member status change" },
		{ "AQ", "Alternate quantity and unit of measurement" },
		{ "AR", "Article out of assortment for particular company" },
		{ "AS", "Article out of assortment" },
		{ "AT", "Item not ordered" },
		{ "AU", "No delivery due to outstanding payments" },
		{ "AV", "Out of inventory" },
		{ "AW", "Quantity adjustment" },
		{ "AX", "National pricing authority agreement is final" },
		{ "BD", "Blueprint deviation" },
		{ "BQ", "Balancing quantity" },
		{ "DC", "Date change" },
		{ "EV", "Estimated quantity" },
		{ "GU", "Gross volume per pack and unit of measure" },
		{ "GW", "Gross weight per pack" },
		{ "LD", "Length difference" },
		{ "MC", "Pack/size measure difference" },
		{ "PC", "Pack difference" },
		{ "PD", "Pack dimension difference" },
		{ "PQ", "Pack quantity" },
		{ "PS", "Product/services ID change" },
		{ "PW", "Pack weight difference" },
		{ "PZ", "Pack size difference" },
		{ "QO", "Quantity ordered" },
		{ "QP", "Quantity based on price qualifier" },
		{ "QT", "Quantity price break" },
		{ "SC", "Size difference" },
		{ "UM", "Unit of measure difference" },
		{ "UP", "Unit price" },
		{ "WD", "Width difference" },
		{ "WO", "Weight qualifier/gross weight per package" },
		{ "ZZZ", "Mutually defined" },
	};
}