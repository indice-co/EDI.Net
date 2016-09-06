using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiConditionAttribute : EdiPathAttribute
    {
        private readonly string _MatchValue;

        public string MatchValue {
            get {
                return _MatchValue;
            }
        }

        public EdiConditionAttribute(string matchValue)
            : base(new EdiPath()) {
            _MatchValue = matchValue;
        }
    }
}
