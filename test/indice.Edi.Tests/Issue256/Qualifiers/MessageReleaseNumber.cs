using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace indice.Edi.Tests.Issue256.Qualifiers;

/// <summary>
/// Message release number
/// </summary>
public class MessageReleaseNumber
{
	/// <summary>
	/// String assign converter
	/// </summary>
	public static implicit operator MessageReleaseNumber(string s) => new MessageReleaseNumber { Code = s };

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
		{ "1", "First release" },
		{ "2", "Second release" },
		{ "00A", "Release 2000 - A" },
		{ "00B", "Release 2000 - B" },
		{ "01A", "Release 2001 - A" },
		{ "01B", "Release 2001 - B" },
		{ "01C", "Release 2001 - C" },
		{ "02A", "Release 2002 - A" },
		{ "02B", "Release 2002 - B" },
		{ "03A", "Release 2003 - A" },
		{ "03B", "Release 2003 - B" },
		{ "04A", "Release 2004 - A" },
		{ "04B", "Release 2004 - B" },
		{ "05A", "Release 2005 - A" },
		{ "05B", "Release 2005 - B" },
		{ "06A", "Release 2006 - A" },
		{ "06B", "Release 2006 - B" },
		{ "07A", "Release 2007 - A" },
		{ "07B", "Release 2007 - B" },
		{ "08A", "Release 2008 - A" },
		{ "08B", "Release 2008 - B" },
		{ "09A", "Release 2009 - A" },
		{ "09B", "Release 2009 - B" },
		{ "10A", "Release 2010 - A" },
		{ "10B", "Release 2010 - B" },
		{ "11A", "Release 2011 - A" },
		{ "11B", "Release 2011 - B" },
		{ "12A", "Release 2012 - A" },
		{ "12B", "Release 2012 - B" },
		{ "13A", "Release 2013 - A" },
		{ "13B", "Release 2013 - B" },
		{ "14A", "Release 2014 - A" },
		{ "14B", "Release 2014 - B" },
		{ "15A", "Release 2015 - A" },
		{ "15B", "Release 2015 - B" },
		{ "16A", "Release 2016 - A" },
		{ "16B", "Release 2016 - B" },
		{ "902", "Trial release 1990" },
		{ "911", "Trial release 1991" },
		{ "912", "Standard release 1991" },
		{ "921", "Trial release 1992" },
		{ "932", "Standard release 1993" },
		{ "93A", "Release 1993 - A" },
		{ "94A", "Release 1994 - A" },
		{ "94B", "Release 1994 - B" },
		{ "95A", "Release 1995 - A" },
		{ "95B", "Release 1995 - B" },
		{ "96A", "Release 1996 - A" },
		{ "96B", "Release 1996 - B" },
		{ "97A", "Release 1997 - A" },
		{ "97B", "Release 1997 - B" },
		{ "98A", "Release 1998 - A" },
		{ "98B", "Release 1998 - B" },
		{ "99A", "Release 1999 - A" },
		{ "99B", "Release 1999 - B" },
	};
}