using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    class EdiTypeDescriptor
    {
        private readonly EdiStructureType _Container;
        private readonly int _Index;
        private readonly object _Instance;
        private readonly List<EdiAttribute> _Attributes;
        private readonly List<EdiPropertyDescriptor> _Properties;

        public EdiStructureType Container {
            get { return _Container; }
        }

        public int Index {
            get { return _Index; }
        }
        public object Instance {
            get { return _Instance; }
        }

        public List<EdiAttribute> Attributes {
            get { return _Attributes; }
        }

        public List<EdiPropertyDescriptor> Properties {
            get { return _Properties; }
        }

        public Type ClrType {
            get { return Instance == null ? null : Instance.GetType(); }
        }

        public EdiTypeDescriptor(EdiStructureType container, object instance)
            : this(container, instance, 0) {
        }

        public EdiTypeDescriptor(EdiStructureType container, object instance, int index) {
            _Container = container;
            _Instance = instance;
            _Index = index;
            _Properties = new List<EdiPropertyDescriptor>();
            var props = ClrType.GetProperties().Select(pi => new EdiPropertyDescriptor(pi)).Where(pi => pi.Attributes.Any());
            // support for multiple value attributes on the same property. Bit hacky.
            foreach (var p in props) {
                var valueAttributes = p.Attributes.OfType<EdiValueAttribute>().ToArray();
                if (valueAttributes.Length > 1) {
                    foreach (var vAttr in valueAttributes) {
                        _Properties.Add(new EdiPropertyDescriptor(p.Info, p.Attributes.Except(new[] { vAttr })));
                    }
                } else {
                    _Properties.Add(p);
                }
            }
            
            _Attributes = new List<EdiAttribute>();
            Attributes.AddRange(ClrType.GetTypeInfo().GetCustomAttributes<EdiAttribute>());
        }
    }
}
