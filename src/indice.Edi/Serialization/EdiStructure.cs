using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    internal class EdiStructure
    {
        private static readonly ThreadSafeStore<Type, EdiTypeDescriptor> typeStore = new ThreadSafeStore<Type, EdiTypeDescriptor>(GetTypeDescriptor);
        private readonly EdiStructureType _Container;
        private readonly int _Index;
        private readonly object _Instance;
        private readonly EdiTypeDescriptor _Descriptor;
        private readonly Queue<EdiEntry> _CachedReads;

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
            if (Index > 0)
                return $"{Container} [{Index + 1}]";

            return $"{Container}";
        }
    }
}
