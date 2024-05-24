using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Returnable package load contents, coded
/// </summary>
public class ReturnablePackageLoadContentsCoded
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator ReturnablePackageLoadContentsCoded(string s) => new ReturnablePackageLoadContentsCoded { Code = s };

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
		{ "1", "Loaded with empty 4-block for blocking purposes" },
		{ "2", "Empty container with dunnage" },
		{ "3", "Empty container" },
		{ "4", "Loaded with production material" },
		{ "5", "Mixed empty and loaded" },
		{ "6", "Obsolete material" },
		{ "7", "Loaded with excess returned production material" },
		{ "8", "Loaded with rejected material" },
		{ "9", "Service part obsolete container" },
		{ "10", "Loaded with returned processed material" },
	};
}