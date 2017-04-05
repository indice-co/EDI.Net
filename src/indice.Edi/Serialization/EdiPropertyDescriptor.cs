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
        private readonly EdiConditionAttribute[] _Conditions;
        private readonly EdiValueAttribute _ValueInfo;
        private readonly EdiSegmentGroupAttribute _SegmentGroupInfo;

        public List<EdiAttribute> Attributes {
            get { return _Attributes; }
        }

        public PropertyInfo Info {
            get { return _Info; }
        }

        public string Path {
            get {
                return _PathInfo?.Path;
            }
        }

        public string Segment {
            get {
                return _PathInfo?.Segment;
            }
        }

        public EdiConditionAttribute[] Conditions {
            get {
                return _Conditions;
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
        public EdiSegmentGroupAttribute SegmentGroupInfo {
            get {
                return _SegmentGroupInfo;
            }
        }

        public bool MarksSegmentGroup {
            get {
                return _SegmentGroupInfo != null;
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
            var conditions = Attributes.OfType<EdiConditionAttribute>().ToArray();
            _Conditions = conditions.Length > 0 ? conditions : null;
            _ValueInfo = Attributes.OfType<EdiValueAttribute>().FirstOrDefault();
            _SegmentGroupInfo = Attributes.OfType<EdiSegmentGroupAttribute>().FirstOrDefault();
            if (_ValueInfo != null && _ValueInfo.Path != null && _PathInfo == null) {
                _PathInfo = new EdiPathAttribute(_ValueInfo.Path);
            }
            if (_SegmentGroupInfo != null && _SegmentGroupInfo.StartInternal.Segment != null && _PathInfo == null) {
                _PathInfo = new EdiPathAttribute(_SegmentGroupInfo.StartInternal.Segment);
            }
        }

        public override string ToString() {
            if (ValueInfo != null) {
                return $"Value @ {Path}";
            }
            if (Attributes.Count > 0) {
                return $"{Attributes.InferStructure()} @ {Path}";
            }
            return base.ToString();
        }
    }
}
