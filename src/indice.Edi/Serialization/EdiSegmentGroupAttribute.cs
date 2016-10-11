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
        private EdiPath _StartPath;
        private EdiPath _EndPath;

        /// <summary>
        /// The segment name that marks the begining of a each group.
        /// </summary>
        /// <param name="segmentStart"></param>
        public EdiSegmentGroupAttribute(string segmentStart) {
            Start = segmentStart;
        }
        
        internal EdiPath StartInternal {
            get { return _StartPath; }
        }

        internal EdiPath SequenceEndInternal {
            get { return _EndPath; }
        }

        /// <summary>
        /// The segment name that defines the start of a group.
        /// </summary>
        public string Start {
            get { return _StartPath; }
            set { _StartPath = (EdiPath)value; }
        }
        
        /// <summary>
        /// Optionaly define the segment name that defines the end of the group or sequence.
        /// </summary>
        public string SequenceEnd {
            get { return _EndPath; }
            set { _EndPath = (EdiPath)value; }
        }
    }
}
