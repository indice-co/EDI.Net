using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// <see cref="EdiSegmentGroupAttribute"/> Marks a propery/class as a logical container of segments. 
    /// This allows a user to decorate a class whith information regarding the starting and ending segments 
    /// that define a virtual group other than the standard ones (Functional Group etc). 
    /// Can be applied on Lists the same way that [Message] or [Segment] attributes work. 
    /// Also known as Segment Loops
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiSegmentGroupAttribute : EdiStructureAttribute
    {
        private EdiPath[] _Members = new EdiPath[1];
        private EdiPath? _EndPath;

        /// <summary>
        /// The Segment group attribute identifies a logical group by passing the start segment name 
        /// and the included (other segments included on the same level).
        /// </summary>
        /// <param name="segmentStart">The segment name that identifies the begining of the group</param>
        /// <param name="includedSegments">The rest segments included under the same level in the group</param>
        public EdiSegmentGroupAttribute(string segmentStart, params string[] includedSegments) {
            _Members = new[] { segmentStart }.Concat(includedSegments).Select(x => (EdiPath)x).ToArray();
        }
        
        internal EdiPath StartInternal {
            get { return _Members[0]; }
        }

        internal EdiPath? SequenceEndInternal {
            get { return _EndPath; }
        }
        internal EdiPath[] Members {
            get { return _Members; }
        }

        /// <summary>
        /// Allows for a container class to have one or more instances of a child
        /// SegmentGroup containing the same starting segment.
        /// </summary>
        public bool AllowNestedGroupWithSameStartSegment { get; set; }

        /// <summary>
        /// The segment name that defines the start of a group.
        /// </summary>
        public string Start {
            get { return _Members[0]; }
            set { _Members[0] = (EdiPath)value; }
        }
        
        /// <summary>
        /// Optionaly define the segment name that defines the end of the group or sequence. 
        /// IMPORTANT: this segment must be one that is NOT included in the group, 
        /// but one that safely can identify that the group has ended (outside the group).
        /// </summary>
        public string SequenceEnd {
            get { return _EndPath.HasValue ? (string)_EndPath.Value : null; }
            set { _EndPath = value != null ? new EdiPath?((EdiPath)value) : null; }
        }

        /// <summary>
        /// Checks wether the group contains a segment
        /// </summary>
        /// <param name="segmentName"></param>
        /// <returns></returns>
        public bool Contains(string segmentName) {
            if (_Members.Length == 1) {
                throw new InvalidOperationException("Cannot determine if a segment name is contained in the segment group since the group members are not populated");
            }
            return _Members.Any(x => segmentName.Equals(x.Segment));
        }

        /// <summary>
        /// Returns a string that represents the object as a concatenated list of segment names. Plus the break segment end if available.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string text = string.Join(", ", _Members.Select(x => x.Segment));
            if (_EndPath.HasValue) {
                text += $" ↵ {_EndPath.Value.Segment}";
            }
            return text;
        }
    }
}
