using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// The mode that a condition is checked against a value. By default the mode is set to check for <see cref="EdiConditionCheckType.Equal"/>
    /// </summary>
    public enum EdiConditionCheckType
    {
        /// <summary>
        /// The value should be  equal
        /// </summary>
        Equal = 1,
        /// <summary>
        /// The value should not be equal.
        /// </summary>
        NotEqual = 2
    }

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
        public string[] Options { get; }

        public EdiConditionCheckType CheckFor { get; set; } = EdiConditionCheckType.Equal;

        public EdiConditionAttribute(string matchValue)
            : base(matchValue) {
        }

        public EdiConditionAttribute(string optionOne, string optionTwo)
            : base(null) {
            Options = new [] { optionOne, optionTwo };
        }
        public EdiConditionAttribute(string optionOne, string optionTwo, params string[] options)
          : base(null) {
            Options = options.Concat(new[] { optionOne, optionTwo }).ToArray();
        }

        public override bool SatisfiedBy(string value) {
            switch (CheckFor) {
                case EdiConditionCheckType.Equal:
                    return Options == null ? MatchValue == value : Options.Contains(value);
                case EdiConditionCheckType.NotEqual:
                    return Options == null ? MatchValue != value : !Options.Contains(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(CheckFor), $"Not expected {CheckFor}");
            }
        }
    }
}
