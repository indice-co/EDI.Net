using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi
{
    /// <summary>
    /// Compares two paths based on their logical structure.
    /// The resulting order would be:
    /// <list type="bullet">
    /// <item><description>ServiceStringAdviceTag.</description></item>
    /// <item><description>InterchangeHeaderTag.</description></item>
    /// <item><description>FunctionalGroupHeaderTag.</description></item>
    /// <item><description>MessageHeaderTag.</description></item>
    /// <item><description>MessageTrailerTag.</description></item>
    /// <item><description>FunctionalGroupTrailerTag.</description></item>
    /// <item><description>InterchangeTrailerTag.</description></item>
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
