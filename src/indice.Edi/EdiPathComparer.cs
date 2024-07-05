namespace indice.Edi;

/// <summary>
/// Compares two <see cref="EdiPath"/>s based on their logical structure.
/// 
/// <list type="bullet">
/// <listheader>The resulting order would be:</listheader>
/// <item><description>ServiceStringAdvice</description></item>
/// <item><description>InterchangeHeader</description></item>
/// <item><description>FunctionalGroupHeader</description></item>
/// <item><description>MessageHeader</description></item>
/// <item><description>CustomSegments</description></item>
/// <item><description>MessageTrailer</description></item>
/// <item><description>FunctionalGroupTrailer</description></item>
/// <item><description>InterchangeTrailer</description></item>
/// </list>
/// </summary>
public class EdiPathComparer : IComparer<EdiPath>
{
    private readonly List<string> segmentOrder;
    private readonly int customSegmentIndex;

    /// <summary>
    /// Construct an <see cref="EdiPathComparer"/> given the <seealso cref="IEdiGrammar"/>.
    /// </summary>
    /// <param name="grammar"></param>
    public EdiPathComparer(IEdiGrammar grammar) {
        if (null == grammar)
            throw new ArgumentNullException(nameof(grammar));
        segmentOrder = new List<string> {
            grammar.InterchangeHeaderTag,
            grammar.FunctionalGroupHeaderTag,
            grammar.MessageHeaderTag,
            null, // custom segments go here.
            grammar.MessageTrailerTag,
            grammar.FunctionalGroupTrailerTag,
            grammar.InterchangeTrailerTag
        };
        if (!string.IsNullOrWhiteSpace(grammar.ServiceStringAdviceTag)) {
            segmentOrder.Insert(0, grammar.ServiceStringAdviceTag);
            customSegmentIndex = 4;
        } else {
            customSegmentIndex = 3;
        }
    }
    
    /// <summary>
    /// Compares two <see cref="EdiPath"/>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(EdiPath x, EdiPath y) {
        if (x.Segment != y.Segment) { 
            var i = Rank(x);
            var j = Rank(y);
            return i.CompareTo(j);
        }
        return x.CompareTo(y);
    }

    /// <summary>
    /// Rank an <see cref="EdiPath"/>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public int Rank(EdiPath path) {
        var i = segmentOrder.IndexOf(path.Segment);
        i = i > -1 ? i : customSegmentIndex;
        return i;
    }
}
