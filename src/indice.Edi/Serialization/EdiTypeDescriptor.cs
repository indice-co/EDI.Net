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
        private readonly List<EdiAttribute> _Attributes;
        private readonly List<EdiPropertyDescriptor> _Properties;
        private readonly Type _ClrType;


        public List<EdiAttribute> Attributes {
            get { return _Attributes; }
        }

        public List<EdiPropertyDescriptor> Properties {
            get { return _Properties; }
        }

        public Type ClrType {
            get { return _ClrType; }
        }
        
        public EdiTypeDescriptor(Type clrType) {
            _ClrType = clrType;
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
