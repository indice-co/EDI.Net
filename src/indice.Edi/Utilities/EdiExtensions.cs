using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Utilities
{
    public static class EdiExtensions
    {
        public static bool IsStartToken(this EdiToken token) {
            switch (token) {
                case EdiToken.SegmentStart:
                case EdiToken.ElementStart:
                case EdiToken.ComponentStart:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPrimitiveToken(this EdiToken token) {
            switch (token) {
                case EdiToken.Integer:
                case EdiToken.Float:
                case EdiToken.String:
                case EdiToken.Boolean:
                case EdiToken.Null:
                case EdiToken.Date:
                    return true;
                default:
                    return false;
            }
        }


        public static IEnumerable<EdiAttribute> OfType(this IEnumerable<EdiAttribute> attributes, EdiStructureType container) {
            var typeToSearch = default(Type);
            switch (container) {
                case EdiStructureType.None:
                    break;
                case EdiStructureType.Interchange:
                    break;
                case EdiStructureType.Group:
                    typeToSearch = typeof(EdiGroupAttribute);
                    break;
                case EdiStructureType.Message:
                    typeToSearch = typeof(EdiMessageAttribute);
                    break;
                case EdiStructureType.Segment:
                    typeToSearch = typeof(EdiSegmentAttribute);
                    break;
                case EdiStructureType.Element:
                    typeToSearch = typeof(EdiElementAttribute);
                    break;
                default:
                    break;
            }

            return null == typeToSearch ? Enumerable.Empty<EdiAttribute>() : attributes.Where(a => a.GetType().Equals(typeToSearch));
        }
    }
}
