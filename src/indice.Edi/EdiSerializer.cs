using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using indice.Edi.Utilities;
using indice.Edi.Serialization;
using System.Reflection;

namespace indice.Edi
{
    public class EdiSerializer
    {
        /// <summary>
        /// Deserializes the EDI structure contained by the specified <see cref="EdiReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="EdiReader"/> that contains the EDI structure to deserialize.</param>
        /// <returns>The <see cref="Object"/> being deserialized.</returns>
        public object Deserialize(EdiReader reader) {
            return Deserialize(reader, null);
        }

        /// <summary>
        /// Deserializes the EDI structure contained by the specified <see cref="TextReader"/>
        /// into an instance of the specified type.
        /// </summary>
        /// /// <param name="reader">The <see cref="TextReader"/> containing the object.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for reading from the text reader</param>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <returns>The instance of <typeparamref name="T"/> being deserialized.</returns>
        public T Deserialize<T>(TextReader reader, IEdiGrammar grammar) {
            return (T)Deserialize(reader, grammar, typeof(T));
        }

        /// <summary>
        /// Deserializes the EDI structure contained by the specified <see cref="StringReader"/>
        /// into an instance of the specified type.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> containing the object.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for reading from the text reader</param>
        /// <param name="objectType">The <see cref="Type"/> of object being deserialized.</param>
        /// <returns>The instance of <paramref name="objectType"/> being deserialized.</returns>
        public object Deserialize(TextReader reader, IEdiGrammar grammar, Type objectType) {
            return Deserialize(new EdiTextReader(reader, grammar), objectType);
        }

        /// <summary>
        /// Deserializes the EDI structure contained by the specified <see cref="EdiReader"/>
        /// into an instance of the specified type.
        /// </summary>
        /// <param name="reader">The <see cref="EdiReader"/> containing the object.</param>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <returns>The instance of <typeparamref name="T"/> being deserialized.</returns>
        public T Deserialize<T>(EdiReader reader) {
            return (T)Deserialize(reader, typeof(T));
        }

        /// <summary>
        /// Deserializes the EDI structure contained by the specified <see cref="EdiReader"/>
        /// into an instance of the specified type.
        /// </summary>
        /// <param name="reader">The <see cref="EdiReader"/> containing the object.</param>
        /// <param name="objectType">The <see cref="Type"/> of object being deserialized.</param>
        /// <returns>The instance of <paramref name="objectType"/> being deserialized.</returns>
        public object Deserialize(EdiReader reader, Type objectType) {
            return DeserializeInternal(reader, objectType);
        }
        
        internal virtual object DeserializeInternal(EdiReader reader, Type objectType) {
            if (reader == null)
                throw new ArgumentNullException("reader");

            // set serialization options onto reader
            object value = null;

            var stack = new Stack<EdiStructure>();

            // If this is not a collection type asume this type is the interchange.
            if (!objectType.IsCollectionType()) {

                while (reader.Read()) {
                    if (reader.IsStartInterchange) {
                        stack.Push(new EdiStructure(EdiStructureType.Interchange, Activator.CreateInstance(objectType)));
                    }
                    if (reader.IsEndInterchange) {
                        while (stack.Peek().Container != EdiStructureType.Interchange) {
                            stack.Pop();
                        }
                        value = stack.Peek().Instance;
                    } else if (reader.IsStartGroup) {
                        TryCreateContainer(reader, stack, EdiStructureType.Group);
                    } else if (reader.IsStartMessage) {
                        TryCreateContainer(reader, stack, EdiStructureType.Message);
                    } else if (reader.TokenType == EdiToken.SegmentName) {
                        TryCreateContainer(reader, stack, EdiStructureType.Segment);
                    } else if (reader.TokenType == EdiToken.ElementStart) {
                        TryCreateContainer(reader, stack, EdiStructureType.Element);
                    }
                    if (reader.TokenType == EdiToken.ComponentStart) {
                        PopulateValue(reader, stack);
                    }
                }
            }
            // else if this is indeed a collection type this must be a collection of messages.
            else {
                // 1. reflect for any EdiAttributes foreach property.
                // 2. populate the values 
                // 3. go deeper in the elements.
            }

            return value;
        }

        internal void PopulateValue(EdiReader reader, Stack<EdiStructure> stack) {
            var current = stack.Peek();
            var typeDescriptor = current.Descriptor;
            var valueProps = typeDescriptor.Properties.Where(p => p.ValueInfo != null && reader.Path == p.Path).ToArray();
            for (var i = 0; i < valueProps.Length; i++) {
                var descriptor = valueProps[i];
                var valueInfo = descriptor.ValueInfo;
                var read = i == 0;
                switch (ConvertUtils.GetTypeCode(descriptor.Info.PropertyType)) {
                    case PrimitiveTypeCode.Empty:
                        break;
                    case PrimitiveTypeCode.Object:
                        break;
                    case PrimitiveTypeCode.Char:
                        break;
                    case PrimitiveTypeCode.CharNullable:
                        break;
                    case PrimitiveTypeCode.Boolean:
                        break;
                    case PrimitiveTypeCode.BooleanNullable:
                        break;
                    case PrimitiveTypeCode.SByte:
                        break;
                    case PrimitiveTypeCode.SByteNullable:
                        break;
                    case PrimitiveTypeCode.Int16:
                        break;
                    case PrimitiveTypeCode.Int16Nullable:
                        break;
                    case PrimitiveTypeCode.UInt16:
                        break;
                    case PrimitiveTypeCode.UInt16Nullable:
                        break;
                    case PrimitiveTypeCode.Int32:
                    case PrimitiveTypeCode.Int32Nullable:
                        if (!descriptor.Info.PropertyType.IsEnum()) {
                            var integer = read ? reader.ReadAsInt32() : (int?)reader.Value;
                            if (integer.HasValue) {
                                descriptor.Info.SetValue(current.Instance, ConvertUtils.ConvertOrCast(integer.Value, CultureInfo.InvariantCulture, descriptor.Info.PropertyType));
                            }
                        } else {
                            var enumValueString = read ? reader.ReadAsString() : (string)reader.Value;
                            if (!string.IsNullOrEmpty(enumValueString)) {
                                descriptor.Info.SetValue(current.Instance, ConvertUtils.ConvertOrCast(enumValueString, CultureInfo.InvariantCulture, descriptor.Info.PropertyType));
                            }
                        }
                        break;
                    case PrimitiveTypeCode.Byte:
                        break;
                    case PrimitiveTypeCode.ByteNullable:
                        break;
                    case PrimitiveTypeCode.UInt32:
                        break;
                    case PrimitiveTypeCode.UInt32Nullable:
                        break;
                    case PrimitiveTypeCode.Int64:
                        break;
                    case PrimitiveTypeCode.Int64Nullable:
                        break;
                    case PrimitiveTypeCode.UInt64:
                        break;
                    case PrimitiveTypeCode.UInt64Nullable:
                        break;
                    case PrimitiveTypeCode.Single:
                        break;
                    case PrimitiveTypeCode.SingleNullable:
                        break;
                    case PrimitiveTypeCode.Double:
                    case PrimitiveTypeCode.DoubleNullable:
                    case PrimitiveTypeCode.Decimal:
                    case PrimitiveTypeCode.DecimalNullable:
                        var numberFloat = read ? reader.ReadAsDecimal(descriptor.ValueInfo.Picture) : (decimal?)reader.Value;
                        if (numberFloat != null) {
                            descriptor.Info.SetValue(current.Instance, numberFloat);
                        }
                        break;
                    case PrimitiveTypeCode.DateTime:
                    case PrimitiveTypeCode.DateTimeNullable:
                        var dateString = read ? reader.ReadAsString() : (string)reader.Value;
                        if (dateString != null) {
                            dateString = dateString.Substring(0, valueInfo.Picture.Scale);
                            var date = default(DateTime);
                            if (DateTime.TryParseExact(dateString, valueInfo.Format, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out date)) {
                                var existingDateObject = descriptor.Info.GetValue(current.Instance);
                                var existingDate = default(DateTime);
                                if (existingDateObject != null && !existingDateObject.Equals(default(DateTime))) {
                                    if (existingDateObject is DateTime?) {
                                        existingDate = ((DateTime?)existingDateObject).Value;
                                    } else {
                                        existingDate = ((DateTime)existingDateObject);
                                    }
                                    if (date - date.Date == default(TimeSpan)) {
                                        date = date.Date.Add(existingDate - existingDate.Date);
                                    } else {
                                        date = existingDate.Add(date - date.Date);
                                    }
                                }
                                descriptor.Info.SetValue(current.Instance, date);
                            }
                        }
                        break;
                    case PrimitiveTypeCode.DateTimeOffset:
                        break;
                    case PrimitiveTypeCode.DateTimeOffsetNullable:
                        break;
                    case PrimitiveTypeCode.Guid:
                        break;
                    case PrimitiveTypeCode.GuidNullable:
                        break;
                    case PrimitiveTypeCode.TimeSpan:
                        break;
                    case PrimitiveTypeCode.TimeSpanNullable:
                        break;
                    case PrimitiveTypeCode.BigInteger:
                        break;
                    case PrimitiveTypeCode.BigIntegerNullable:
                        break;
                    case PrimitiveTypeCode.Uri:
                        break;
                    case PrimitiveTypeCode.String:
                        var text = read ? reader.ReadAsString() : (string)reader.Value;
                        descriptor.Info.SetValue(current.Instance, text);
                        break;
                    case PrimitiveTypeCode.Bytes:
                        break;
                    case PrimitiveTypeCode.DBNull:
                        break;
                    default:
                        break;
                }


            }
        }

        internal bool TryCreateContainer(EdiReader reader, Stack<EdiStructure> stack, EdiStructureType newContainer) {
            var index = 0;
            if (stack.Count == 0)
                return false;
            while (stack.Peek().Container >= newContainer) {
                var previous = stack.Pop();
                if (previous.Container == newContainer) {
                    index = previous.Index + 1;
                }
            }
            var current = stack.Peek();
            var candidates = current.Descriptor.Properties.Where(p => p.Attributes.OfType(newContainer).Any()).ToArray();
            var property = default(EdiPropertyDescriptor);
            if (newContainer == EdiStructureType.Segment) {
                property = FindForCurrentSegment(reader, candidates, current.Descriptor);
            } else if (newContainer == EdiStructureType.Element) {
                property = FindForCurrentElement(reader, candidates, current.Descriptor);
            } else {
                property = FindForCurrentLogicalStructure(reader, candidates, current.Descriptor, newContainer);
            }    
            if (property == null) {
                return false;
            }
            object propValue = property.Info.GetValue(current.Instance, null);
            if (propValue == null) {
                if (property.Info.PropertyType.IsCollectionType()) {
                    var baseType = typeof(List<>);
                    var genericType = baseType.MakeGenericType(property.Info.PropertyType.GetGenericArguments().First());
                    propValue = Activator.CreateInstance(genericType);
                } else {
                    propValue = Activator.CreateInstance(property.Info.PropertyType);
                }
                property.Info.SetValue(current.Instance, propValue);
            }
            if (propValue is IList) {
                var itemType = property.Info.PropertyType.GetGenericArguments().First();
                var item = Activator.CreateInstance(itemType);
                ((IList)propValue).Add(item);
                propValue = item;
            }
            stack.Push(new EdiStructure(newContainer, propValue, index));
            return true;
        }


        private EdiPropertyDescriptor FindForCurrentSegment(EdiReader reader, EdiPropertyDescriptor[] candidates, EdiTypeDescriptor current) {
            if (candidates.Length == 0) {
                return null;
            }
            var property = default(EdiPropertyDescriptor);
            if (reader.TokenType == EdiToken.SegmentName) { 
                property = candidates.SingleOrDefault(p => p.Segment.Equals(reader.Value));
            }
            return property;
        }
        private EdiPropertyDescriptor FindForCurrentElement(EdiReader reader, EdiPropertyDescriptor[] candidates, EdiTypeDescriptor current) {
            if (candidates.Length == 0) {
                return null;
            }
            var property = default(EdiPropertyDescriptor);
            if (reader.TokenType == EdiToken.ElementStart) {
                property = candidates.SingleOrDefault(p => p.PathInfo.PathInternal.ToString("S").Equals(reader.Path));
            }
            return property;
        }

        private EdiPropertyDescriptor FindForCurrentLogicalStructure(EdiReader reader, EdiPropertyDescriptor[] candidates, EdiTypeDescriptor current, EdiStructureType newContainer) {
            var property = default(EdiPropertyDescriptor);
            if (candidates.Length == 0) {
                return null;
            } else if (candidates.Length == 1 && candidates[0].ConditionInfo == null) {
                property = candidates[0];
            } else {
                if (!candidates.All(p => p.ConditionInfo != null)) {
                    "More than one properties on type '{0}' have a '{1}' attribute. Please add a 'Condition' attribute to all properties in order to discriminate where each message will go.".FormatWith(CultureInfo.InvariantCulture, current.ClrType.Name, newContainer);
                }
                if (candidates.Select(p => p.Path).Distinct().Count() != 1) {
                    "More than one properties on type '{0}' have a '{1}' attribute but the 'Condition' attribute has a different search path declared.".FormatWith(CultureInfo.InvariantCulture, current.ClrType.Name, newContainer);
                }
                var path = string.Empty;
                do {
                    reader.Read();
                    path = reader.Path;
                } while (reader.TokenType != EdiToken.SegmentStart && candidates[0].Path != path);

                var discriminator = reader.ReadAsString();
                property = candidates.SingleOrDefault(p => p.ConditionInfo.MatchValue == discriminator);
            }
            return property;
        }
    }
}
