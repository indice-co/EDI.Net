using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    internal class EdiStructure {
        private static readonly ThreadSafeStore<Type, EdiTypeDescriptor> typeStore = new ThreadSafeStore<Type, EdiTypeDescriptor>(GetTypeDescriptor);
        private readonly EdiStructureType _Container;
        private readonly int _Index;
        private readonly object _Instance;
        private readonly EdiTypeDescriptor _Descriptor;
        private readonly Queue<EdiEntry> _CachedReads;
        private bool _isClosed;

        public EdiStructureType Container {
            get { return _Container; }
        }

        public EdiTypeDescriptor Descriptor {
            get { return _Descriptor; }
        }

        public int Index {
            get { return _Index; }
        }

        public object Instance {
            get { return _Instance; }
        }

        public Queue<EdiEntry> CachedReads {
            get { return _CachedReads; }
        }

        /// <summary>
        /// This <see cref="EdiStructure"/> is closed and must be removed from stack
        /// </summary>
        public bool IsClosed {
            get { return _isClosed; }
        }

        /// <summary>
        /// This checkes to see if this is a custom sequence of segments.
        /// </summary>
        public bool IsGroup {
            get { return Descriptor.IsSegmentGroup; }
        }

        /// <summary>
        /// The sequence start path.
        /// </summary>
        public EdiPath GroupStart {
            get {
                return Descriptor.SegmentGroupInfo.StartInternal;
            }
        }

        /// <summary>
        /// The sequence start path.
        /// </summary>
        public EdiPath? SequenceEnd {
            get {
                return Descriptor.SegmentGroupInfo.SequenceEndInternal;
            }
        }

        public EdiStructure(EdiStructureType container, object instance)
            : this(container, instance, 0, new Queue<EdiEntry>()) {
        }

        public EdiStructure(EdiStructureType container, object instance, int index, Queue<EdiEntry> cache) {
            ValidationUtils.ArgumentNotNull(instance, "instance");
            _Container = container;
            _Instance = instance;
            _Index = index;
            _Descriptor = typeStore.Get(instance.GetType());
            _CachedReads = cache;
        }

        private static EdiTypeDescriptor GetTypeDescriptor(Type type) => new EdiTypeDescriptor(type);

        public EdiPropertyDescriptor[] GetMatchingProperties(EdiStructureType sructureType) =>
            Descriptor.Properties.Where(p => p.Attributes.OfType(sructureType).Any()).ToArray();

        public EdiPropertyDescriptor[] GetMatchingProperties(string segmentName) =>
            Descriptor.Properties.Where(p => p.PathInfo?.PathInternal.Segment == segmentName).ToArray();

        public IEnumerable<EdiPropertyDescriptor> GetOrderedProperties(IEdiGrammar grammar) =>
            GetOrderedProperties(new EdiPathComparer(grammar));

        public IEnumerable<EdiPropertyDescriptor> GetOrderedProperties(IComparer<EdiPath> comparer) =>
            Descriptor.Properties.OrderBy(p => p.PathInfo?.PathInternal ?? default(EdiPath), comparer);
        
        public override string ToString() {
            var text = new System.Text.StringBuilder();
            text.Append($"{Container}");
            switch (Container) {
                case EdiStructureType.Element:
                    text.Append($" {Descriptor.Attributes.OfType<EdiPathAttribute>().FirstOrDefault()?.PathInternal.ToString("e")}"); // element
                    break;
                case EdiStructureType.SegmentGroup:
                    text.Append($" {GroupStart.ToString("s")}"); // only segment
                    break;
                default:
                    text.Append($" {Descriptor.Attributes.OfType<EdiPathAttribute>().FirstOrDefault()?.PathInternal.ToString("s")}"); // the rest
                    break;
            }
            if (Index > 0)
                text.Append($"[{Index + 1}]");
            return text.ToString();
        }

        /// <summary>
        /// Marks this <see cref="EdiStructure"/> ready for removal from the stack. 
        /// Useful on <seealso cref="EdiStructureType.SegmentGroup"/> where there is a close condition.
        /// </summary>
        public void Close() {
            if (_isClosed)
                throw new EdiException("Cannot close an already closed Structure");
            _isClosed = true;
        }
    }
}
