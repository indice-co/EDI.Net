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

        /// <summary>
        /// The value used to compare against.
        /// </summary>
        public string MatchValue {
            get {
                return _MatchValue;
            }
        }

        /// <summary>
        /// Creates a Condition attribute by passingthe expected <paramref name="matchValue"/>.
        /// </summary>
        /// <param name="matchValue">The expected value to compare against.</param>
        public EdiConditionBaseAttribute(string matchValue)
            : base(new EdiPath()) {
            _MatchValue = matchValue;
        }

        /// <summary>
        /// Checks if the condition is satisfied.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool SatisfiedBy(string value);

        /// <summary>
        /// Returns a string that represents the condition.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"Condition = {MatchValue}";
        }
    }

    
    /// <summary>
    /// In case multiple MessageTypes or Segment types with the same name. <see cref="EdiConditionAttribute"/> is used 
    /// to discriminate the classes based on a component value
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EdiConditionAttribute : EdiConditionBaseAttribute
    {
        /// <summary>
        /// Possible values to check against.
        /// </summary>
        public string[] Options { get; }

        /// <summary>
        /// Condition satisfaction oprator. Can be either <see cref="EdiConditionCheckType.Equal"/> or <seealso cref="EdiConditionCheckType.NotEqual"/>.
        /// </summary>
        public EdiConditionCheckType CheckFor { get; set; } = EdiConditionCheckType.Equal;
        
        /// <summary>
        /// Creates a Condition attribute by passingthe expected <paramref name="matchValue"/>.
        /// </summary>
        /// <param name="matchValue">The expected value to compare against.</param>
        public EdiConditionAttribute(string matchValue)
            : base(matchValue) {
        }

        /// <summary>
        /// Creates a Condition attribute by passing the two possible values to check against.
        /// </summary>
        /// <param name="optionOne">One of the possible expected values</param>
        /// <param name="optionTwo">The second possibly expected value</param>
        public EdiConditionAttribute(string optionOne, string optionTwo)
            : base(null) {
            Options = new [] { optionOne, optionTwo };
        }

        /// <summary>
        /// Creates a Condition attribute by passing the multiple (more than two) values to check against.
        /// </summary>
        /// <param name="optionOne">One of the possible expected values</param>
        /// <param name="optionTwo">The second possibly expected value</param>
        /// <param name="options">The rest values.</param>
        public EdiConditionAttribute(string optionOne, string optionTwo, params string[] options)
          : base(null) {
            Options = options.Concat(new[] { optionOne, optionTwo }).ToArray();
        }

        /// <summary>
        /// Checks if the condition is satisfied.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Returns a string that represents the condition.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            switch (CheckFor) {
                case EdiConditionCheckType.Equal:
                    if(Options == null || Options.Length == 1)
                    {
                        return $"Condition = {MatchValue}";
                    }
                    return $"Condition in ({string.Join(", ", Options)})";
                case EdiConditionCheckType.NotEqual:
                    if(Options == null || Options.Length == 1)
                    {
                        return $"Condition != {MatchValue}";
                    }
                    return $"Condition not in ({string.Join(", ", Options)})";
                default:
                    throw new EdiException($"Unexpected condition type {CheckFor}");
            }
        }
    }
}
