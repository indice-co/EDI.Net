using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    internal class EdiStructure {
        private static readonly ThreadSafeStore<Type, EdiTypeDescriptor> typeStore = new ThreadSafeStore<Type, EdiTypeDescriptor>(GetTypeDescriptor);
        private readonly EdiStructureType _StructureType;
        private readonly EdiStructure _Container;
        private readonly int _Index;
        private readonly object _Instance;
        private readonly EdiTypeDescriptor _Descriptor;
        private readonly Queue<EdiEntry> _CachedReads;
        private readonly EdiConditionAttribute[] _Conditions;
        private readonly EdiConditionStackMode _ConditionStackMode;
        private bool _isClosed;

        /// <summary>
        /// Represents the type of the structure. (Interchange, Group, Message, Segment etc.) 
        /// </summary>
        public EdiStructureType StructureType {
            get { return _StructureType; }
        }
        
        /// <summary>
         /// Represents immediate container structure. Essentialy this is the parent level. 
         /// </summary>
        public EdiStructure Container {
            get { return _Container; }
        }

        /// <summary>
        /// The <see cref="EdiTypeDescriptor"/> that contains all information derived from the anotations on the CLR <seealso cref="Type"/>.
        /// </summary>
        public EdiTypeDescriptor Descriptor {
            get { return _Descriptor; }
        }

        /// <summary>
        /// An index indicating that this structure is part of a collention at posion <see cref="Index"/>
        /// </summary>
        public int Index {
            get { return _Index; }
        }

        /// <summary>
        /// The CLR model instance that maps to the structure
        /// </summary>
        public object Instance {
            get { return _Instance; }
        }

        /// <summary>
        /// A queue that contains all the read entries that where used by advancing the reader in order to search for pottential clues. 
        /// It is populated when searching for Conditions and emptied when populating the values.
        /// </summary>
        public Queue<EdiEntry> CachedReads {
            get { return _CachedReads; }
        }

        /// <summary>
        /// All conditions that led to the creation of this structure. 
        /// </summary>
        public EdiConditionAttribute[] Conditions {
            get { return _Conditions; }
        }

        /// <summary>
        /// The conditions stack mode.
        /// </summary>
        public EdiConditionStackMode ConditionStackMode {
            get { return _ConditionStackMode; }
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
        /// The sequence escape path.
        /// </summary>
        public EdiPath? SequenceEnd {
            get {
                return Descriptor.SegmentGroupInfo.SequenceEndInternal;
            }
        }

        /// <summary>
        /// The sequence members.
        /// </summary>
        public EdiPath[] GroupMembers {
            get {
                return Descriptor.SegmentGroupInfo.Members;
            }
        }

        /// <summary>
        /// Checks wether the group contains a segment. Will throw in case of not defined members on the group attribute.
        /// </summary>
        /// <param name="segmentName"></param>
        /// <returns></returns>
        public bool GroupContains(string segmentName) {
            return Descriptor.SegmentGroupInfo.Contains(segmentName);
        }

        public EdiStructure(EdiStructureType structureType, object instance)
            : this(structureType, null, null, instance, 0, new Queue<EdiEntry>()) {
        }

        public EdiStructure(EdiStructureType structureType, EdiStructure parent, EdiPropertyDescriptor property, object instance)
            : this(structureType, parent, property, instance, 0, new Queue<EdiEntry>()) {
        }

        public EdiStructure(EdiStructureType structureType, EdiStructure parent, EdiPropertyDescriptor property, object instance, int index, Queue<EdiEntry> cache) {
            ValidationUtils.ArgumentNotNull(instance, "instance");
            _StructureType = structureType;
            _Container = parent;
            _Instance = instance;
            _Index = index;
            _Descriptor = typeStore.Get(instance.GetType());
            _CachedReads = cache;
            _Conditions = Descriptor.Attributes.OfType<EdiConditionAttribute>().Concat(property?.Conditions ?? new EdiConditionAttribute[0]).ToArray();
            _ConditionStackMode = _Conditions.Length > 0 && (
                property?.ConditionStackMode == EdiConditionStackMode.Any ||
                Descriptor.Attributes.OfType<EdiAnyAttribute>().Any())
                ? EdiConditionStackMode.Any : EdiConditionStackMode.All;
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
            text.Append($"{StructureType}");
            switch (StructureType) {
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
