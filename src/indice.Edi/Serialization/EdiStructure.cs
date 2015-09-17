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

        public EdiStructure(EdiStructureType container, object instance)
            : this(container, instance, 0) {
        }

        public EdiStructure(EdiStructureType container, object instance, int index) {
            ValidationUtils.ArgumentNotNull(instance, "instance");
            _Container = container;
            _Instance = instance;
            _Index = index;
            _Descriptor = typeStore.Get(instance.GetType());
        }

        private static EdiTypeDescriptor GetTypeDescriptor(Type type) {
            return new EdiTypeDescriptor(type);
        }
    }
}
