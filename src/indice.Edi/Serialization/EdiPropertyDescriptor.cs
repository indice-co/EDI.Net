using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    internal class EdiPropertyDescriptor
    {
        private readonly PropertyInfo _Info;
        private readonly List<EdiAttribute> _Attributes;
        private readonly EdiPathAttribute _PathInfo;
        private readonly EdiConditionAttribute _ConditionInfo;
        private readonly EdiValueAttribute _ValueInfo;

        public List<EdiAttribute> Attributes {
            get { return _Attributes; }
        }

        public PropertyInfo Info {
            get { return _Info; }
        }

        public string Path {
            get {
                return _PathInfo.Path;
            }
        }
        public string Segment {
            get {
                return _PathInfo.Segment;
            }
        }
        public EdiConditionAttribute ConditionInfo {
            get {
                return _ConditionInfo;
            }
        }
        public EdiPathAttribute PathInfo {
            get {
                return _PathInfo;
            }
        }
        public EdiValueAttribute ValueInfo {
            get {
                return _ValueInfo;
            }
        }

        public EdiPropertyDescriptor(PropertyInfo info) 
            : this(info, null) {
        }

        public EdiPropertyDescriptor(PropertyInfo info, IEnumerable<EdiAttribute> attributes) {
            _Info = info;
            if (attributes == null) {
                attributes = info.GetCustomAttributes<EdiAttribute>()
                                 .Concat(info.PropertyType.GetTypeInfo().GetCustomAttributes<EdiAttribute>());
                if (info.PropertyType.IsCollectionType()) {
                    var itemType = default(Type);
                    if (info.PropertyType.HasElementType) {
                        itemType = info.PropertyType.GetElementType();
                    } else {
                        itemType = Info.PropertyType.GetGenericArguments().First();
                    }
                    attributes = attributes.Concat(itemType.GetTypeInfo().GetCustomAttributes<EdiAttribute>());
                }
            }
            _Attributes = attributes.ToList();
            _PathInfo = Attributes.OfType<EdiPathAttribute>().FirstOrDefault();
            _ConditionInfo = Attributes.OfType<EdiConditionAttribute>().FirstOrDefault();
            _ValueInfo = Attributes.OfType<EdiValueAttribute>().FirstOrDefault();
            if (_ValueInfo != null && _ValueInfo.Path != null && _PathInfo == null) {
                _PathInfo = new EdiPathAttribute(_ValueInfo.Path);
            }
        }
    }
}
