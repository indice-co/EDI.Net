using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi
{
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
        
        public int Compare(EdiPath x, EdiPath y) {
            if (x.Segment != y.Segment) { 
                var i = Rank(x);
                var j = Rank(y);
                return i.CompareTo(j);
            }
            return x.CompareTo(y);
        }

        public int Rank(EdiPath path) {
            var i = segmentOrder.IndexOf(path.Segment);
            i = i > -1 ? i : customSegmentIndex;
            return i;
        }
    }
}
