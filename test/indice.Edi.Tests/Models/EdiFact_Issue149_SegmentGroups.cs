using System.Collections.Generic;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

/// <summary>
/// Issue 149. Deserializer doesn't close segment group level if group consists of only one segment.
/// </summary>
public class EdiFact_Issue149_SegmentGroups
{
    public EdiFact_Issue149_SegmentGroups_Message Message { get; set; }
}

[EdiMessage]
public class EdiFact_Issue149_SegmentGroups_Message
{
    /// <summary>
    /// Single-segment group which causes issue.
    /// </summary>
    public EdiFact_Issue149_SegmentGroups_SG15 SG15 { get; set; }

    /// <summary>
    /// Deserializer doesn't close level SG15 and tries to read content of this group as SG15.
    /// </summary>
    public EdiFact_Issue149_SegmentGroups_SG25 SG25 { get; set; }

    /// <summary>
    /// And this too. Both stay null after deserialization unless you specify UNS as sequence end for EdiFact_Issue149_SegmentGroups_SG15.
    /// </summary>
    [EdiValue("X(1)", Path = "UNS/0", Mandatory = true)]
    public string UNS1 { get; set; }
}

//[EdiSegmentGroup("AJT", "FTX")] // Adding one more random segment "fixes" the issue since level.GroupMembers.Length > 1 becomes true and deserializer closes this level when finds other segments.
[EdiSegment, EdiPath("AJT")]
public class EdiFact_Issue149_SegmentGroups_SG15
{
    [EdiValue("X(3)", Path = "AJT/0", Mandatory = true)]
    public string AJT1 { get; set; }

    [EdiValue("X(2)", Path = "AJT/1")]
    public string AJT2 { get; set; }
}

[EdiSegmentGroup("TAX", "PAI", "RFF", "MOA")]
public class EdiFact_Issue149_SegmentGroups_SG25
{
    [EdiValue("X(3)", Path = "TAX/0", Mandatory = true)]
    public string TAX1 { get; set; }

    public List<EdiFact_Issue149_SegmentGroups_SG26> SG26 { get; set; }
}

[EdiSegmentGroup("PAI", "RFF", "MOA")]
public class EdiFact_Issue149_SegmentGroups_SG26 : EdiFact_Issue149_PAI
{
    public EdiFact_Issue149_MOA MOA { get; set; }
    public List<EdiFact_Issue149_SegmentGroups_SG26_RFF> References { get; set; }
}

[EdiSegment, EdiPath("PAI")]
public class EdiFact_Issue149_PAI
{
    [EdiValue("X(3)", Path = "PAI/0/2")]
    public string PAI1 { get; set; }

    [EdiValue("X(17)", Path = "PAI/0/3")]
    public string PAI2 { get; set; }

    [EdiValue("X(2)", Path = "PAI/0/4", Mandatory = true)]
    public string PAI3 { get; set; }
}

[EdiSegment, EdiPath("MOA")]
public class EdiFact_Issue149_MOA
{
    [EdiValue("X(3)", Path = "MOA/0/0", Mandatory = true)]
    public string MOA1 { get; set; }

    [EdiValue("9(35)", Path = "MOA/0/1")]
    public int MOA2 { get; set; }

    [EdiValue("X(3)", Path = "MOA/0/2")]
    public string MOA3 { get; set; }
}

[EdiSegment, EdiPath("RFF")]
public class EdiFact_Issue149_SegmentGroups_SG26_RFF
{
    [EdiValue("X(3)", Path = "RFF/0/0", Mandatory = true)]
    public string RFF1 { get; set; }

    [EdiValue("X(70)", Path = "RFF/0/1")]
    public string RFF2 { get; set; }
}