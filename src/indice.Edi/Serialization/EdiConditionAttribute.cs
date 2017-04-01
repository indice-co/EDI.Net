using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// In case multiple MessageTypes or Segment types with the same name. <see cref="EdiConditionAttribute"/> is used 
    /// to discriminate the classes based on a component value. This class serves as the base implementation. 
    /// You can subclass this and override the SatisfiedBy method in order to implement a custom condition check.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public abstract class EdiConditionBaseAttribute : EdiPathAttribute, IEdiCondition
    {
        private readonly string _MatchValue;

        public string MatchValue {
            get {
                return _MatchValue;
            }
        }

        public EdiConditionBaseAttribute(string matchValue)
            : base(new EdiPath()) {
            _MatchValue = matchValue;
        }

        public abstract bool SatisfiedBy(string value);
    }

    
    /// <summary>
    /// In case multiple MessageTypes or Segment types with the same name. <see cref="EdiConditionAttribute"/> is used 
    /// to discriminate the classes based on a component value
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EdiConditionAttribute : EdiConditionBaseAttribute
    {
        public EdiConditionAttribute(string matchValue)
            : base(matchValue) {
        }

        public override bool SatisfiedBy(string value) {
            return MatchValue == value;
        }
    }
}
