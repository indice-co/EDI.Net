using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Type of means of transport identification
/// </summary>
public class TypeOfMeansOfTransportIdentification
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator TypeOfMeansOfTransportIdentification(string s) => new TypeOfMeansOfTransportIdentification { Code = s };

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
		{ "1", "Barge chemical tanker" },
		{ "2", "Coaster chemical tanker" },
		{ "3", "Dry bulk carrier" },
		{ "4", "Deep sea chemical tanker" },
		{ "5", "Gas tanker" },
		{ "6", "Aircraft" },
		{ "7", "Car with caravan" },
		{ "8", "Container ship" },
		{ "9", "Exceptional transport" },
		{ "10", "Bus" },
		{ "11", "Ship" },
		{ "12", "Ship tanker" },
		{ "13", "Ocean vessel" },
		{ "14", "Flatbed trailer" },
		{ "15", "Taxi" },
		{ "16", "Barge" },
		{ "17", "Customer determined means of transport" },
		{ "18", "Seller determined means of transport" },
		{ "21", "Rail tanker" },
		{ "22", "Rail silo tanker" },
		{ "23", "Rail bulk car" },
		{ "24", "Customer rail tanker" },
		{ "25", "Rail express" },
		{ "31", "Truck" },
		{ "32", "Road tanker" },
		{ "33", "Road silo tanker" },
		{ "35", "Truck/trailer with tilt" },
		{ "36", "Pipeline" },
		{ "37", "Hydrant cart" },
		{ "38", "Car" },
	};
}