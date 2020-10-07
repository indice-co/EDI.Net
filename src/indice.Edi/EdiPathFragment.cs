using System;
using System.Text.RegularExpressions;

namespace indice.Edi
{
    /// <summary>
    /// Represents a fragment on the <see cref="EdiPath"/>
    /// </summary>
    public struct EdiPathFragment : IComparable, IEquatable<EdiPathFragment>
    {
        private static Regex _RangePattern = new Regex(@"^(\d+|\*)\.\.(\d+|\*)$");
        private readonly string _Value;

        /// <summary>
        /// Constructs a <see cref="EdiPathFragment"/> given the current part
        /// </summary>
        /// <param name="fragment">A part of the <see cref="EdiPath"/></param>
        public EdiPathFragment(string fragment) {
            _Value = fragment;
        }

        /// <summary>
        /// The value of the <see cref="EdiPathFragment"/>.
        /// </summary>
        public string Value {
            get {
                return _Value;
            }
        }

        /// <summary>
        /// Converts the value to a zero based index.
        /// </summary>
        public int Index {
            get {
                if (string.IsNullOrEmpty(Value))
                    return 0;
                var index = 0;
                if (int.TryParse(Value, out index)) {
                    return index;
                }
                throw new InvalidCastException(string.Format("Cannot convert the fragment value \"{0}\" to an Index. Must be a positive indeger", Value));
            }
        }

        /// <summary>
        /// Converts the value to a zero based index.
        /// </summary>
        public int Min {
            get {
                if (!IsRange) return Index;
                if ("*..*".Equals(Value))
                    throw new InvalidCastException(string.Format("Cannot convert the fragment value \"{0}\" to range. Use * instead", Value));
                var min = _RangePattern.Match(Value).Groups[1].Value;
                if ("*".Equals(min)) {
                    return 0;
                }
                var index = 0;
                if (int.TryParse(min, out index)) {
                    return index;
                }
                throw new InvalidCastException(string.Format("Cannot convert the fragment value \"{0}\" to range. Minimum must be the * or a positive integer", Value));
            }
        }

        /// <summary>
        /// Converts the value to a zero based index.
        /// </summary>
        public int Max {
            get {
                if (!IsRange) return Index; 
                if ("*..*".Equals(Value))
                    throw new InvalidCastException(string.Format("Cannot convert the fragment value \"{0}\" to range. Use * instead", Value));
                var max = _RangePattern.Match(Value).Groups[2].Value;
                if ("*".Equals(max)) {
                    return int.MaxValue;
                }
                var index = 0;
                if (int.TryParse(max, out index)) {
                    return index;
                }
                throw new InvalidCastException(string.Format("Cannot convert the fragment value \"{0}\" to range. Maximum must be the * or a positive integer", Value));
            }
        }

        /// <summary>
        /// If the <see cref="IsWildcard"/> is true then the current path fragment can hold any type of value.
        /// This means any type of name in case of the segment name fragment OR any positive <see cref="int"/> for the element and component indices.
        /// </summary>
        public bool IsWildcard {
            get {
                return "*".Equals(Value);
            }
        }

        /// <summary>
        /// If the <see cref="IsRange"/> is true then the current path fragment can hold a range of possible indexes.
        /// This means any type of name in case of the segment name fragment OR any positive <see cref="int"/> for the element and component indices.
        /// </summary>
        public bool IsRange {
            get {
                return Value?.Contains("..") == true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="EdiPathFragment"/> has a value.
        /// </summary>
        public bool HasValue {
            get {
                return !string.IsNullOrWhiteSpace(Value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="EdiPathFragment"/> has a value.
        /// </summary>
        public bool HasIndex {
            get {
                return !HasValue || (!IsWildcard && Regex.IsMatch(Value, @"^\d+$"));
            }
        }

        /// <summary>
        /// Returns the hash code for this fragment
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return (Value ?? "0").GetHashCode();
        }

        /// <summary>
        /// Indicates whether this instance and the specified object are equal.
        /// If either one is a wildcard fragment the function will return true regardless.
        /// </summary>
        /// <param name="obj">The object to check equality with</param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj != null) {
                if (obj is EdiPathFragment) {
                    return Equals((EdiPathFragment)obj);
                } else if (obj is int) {
                    return Equals((EdiPathFragment)(int)obj);
                } else if (obj is string) {
                    return Equals((EdiPathFragment)(string)obj);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether this instance and the specified object are equal.
        /// If either one is a wildcard fragment the function will return true regardless.
        /// </summary>
        /// <param name="other">The object to check equality with</param>
        /// <returns></returns>
        public bool Equals(EdiPathFragment other) {
            bool eq = IsWildcard || other.IsWildcard || (HasIndex && Index.Equals(other.Index)) || Value.Equals(other.Value);

            if (!eq && (IsRange || other.HasIndex)) {
                return Min <= other.Index && Max >= other.Index;
            }
            else if (!eq && (HasIndex || other.IsRange)) {
                return other.Min <= Index && other.Max >= Index;
            }
            return eq;
        }

        /// <summary>
        /// Compares this instance to the <paramref name="obj"/> passed. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj) {
            if (obj != null) {
                var other = default(EdiPathFragment);
                if (obj is EdiPathFragment) {
                    other = (EdiPathFragment)obj;
                } else if (obj is int) {
                    other = (EdiPathFragment)(int)obj;
                } else if (obj is string) {
                    other = (EdiPathFragment)(string)obj;
                }
                if (Equals(other)) {
                    return 0;
                } else if (other.HasIndex && HasIndex) {
                    return Index.CompareTo(other.Index);
                } else {
                    return string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);
                }
            }
            return 0;
        }

    /// <summary>
    /// Returns the value of this <see cref="EdiPathFragment"/> or the wildcard character <code>'*'</code>.
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return Value;
    }

    /// <summary>
    /// Implicit cast operator from <see cref="EdiPathFragment"/> to <seealso cref="string"/>
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator string(EdiPathFragment value) {
        return value.ToString();
    }

    /// <summary>
    /// Explicit cast operator from <see cref="string"/> to <seealso cref="EdiPath"/>
    /// </summary>
    /// <param name="value"></param>
    public static explicit operator EdiPathFragment(string value) {
        return new EdiPathFragment(value);
    }

    /// <summary>
    /// Explicit cast operator from <see cref="int"/> to <seealso cref="EdiPath"/>
    /// </summary>
    /// <param name="value"></param>
    public static explicit operator EdiPathFragment(int value) {
        return new EdiPathFragment(value.ToString());
    }

    /// <summary>
    /// Explicit cast operator from <see cref="int"/> to <seealso cref="EdiPath"/>
    /// </summary>
    /// <param name="value"></param>
    public static explicit operator int(EdiPathFragment value) {
        return value.Index;
    }
}
}
