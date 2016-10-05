using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// In case multiple MessageTypes or Segment types with the same name. <see cref="EdiConditionAttribute"/> is used 
    /// to discriminate the classes based on a component value
    /// </summary>
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
