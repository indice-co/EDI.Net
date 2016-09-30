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
        /// <param name="reader">The <see cref="TextReader"/> containing the object.</param>
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

        #region Read internals

        internal virtual object DeserializeInternal(EdiReader reader, Type objectType) {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

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
                throw new NotImplementedException("Collection types are not supported as the root Type. Try to wrap List of Messages inside a container type.");
            }

            return value;
        }

        internal void PopulateValue(EdiReader reader, Stack<EdiStructure> stack) {
            var current = stack.Peek();
            var maxLevelsUp = 0;
            var level = 0;
            // stack always enumerates backwards so this is a search upwards :-)
            // we search only if we need to have more than one property populated with the value from the reader.
            // maxLevelsUp = zero means only the current level (quicker).
            foreach (var structure in stack) {
                if (level++ > maxLevelsUp)
                    break;
                var typeDescriptor = structure.Descriptor;
                var valueProps = typeDescriptor.Properties
                                               .Where(p => p.ValueInfo != null &&
                                                            (reader.Path == p.Path || structure.CachedReads.ContainsPath(p.Path))
                                                           ).OrderByDescending(p => reader.Path == p.Path).ToArray();

                for (var i = 0; i < valueProps.Length; i++) {
                    var descriptor = valueProps[i];
                    // should I use the text reader? 
                    // Values must be read only once on first pass! 
                    // Otherwise the reader position moves forward 
                    // and the serializer gets out of sync.
                    var read = current == structure && reader.Path == descriptor.Path && i == 0;
                    switch (ConvertUtils.GetTypeCode(descriptor.Info.PropertyType)) {
                        case PrimitiveTypeCode.Empty: break;
                        case PrimitiveTypeCode.Object: break;
                        case PrimitiveTypeCode.Char:
                        case PrimitiveTypeCode.CharNullable:
                            PopulateCharValue(reader, structure, descriptor, read);
                            break;
                        case PrimitiveTypeCode.Boolean:
                        case PrimitiveTypeCode.BooleanNullable:
                            PopulateBooleanValue(reader, structure, descriptor, read);
                            break;
                        case PrimitiveTypeCode.SByte: break;
                        case PrimitiveTypeCode.SByteNullable: break;
                        case PrimitiveTypeCode.Int16: break;
                        case PrimitiveTypeCode.Int16Nullable: break;
                        case PrimitiveTypeCode.UInt16: break;
                        case PrimitiveTypeCode.UInt16Nullable: break;
                        case PrimitiveTypeCode.Int32:
                        case PrimitiveTypeCode.Int32Nullable:
                            PopulateInt32Value(reader, structure, descriptor, read);
                            break;
                        case PrimitiveTypeCode.Byte: break;
                        case PrimitiveTypeCode.ByteNullable: break;
                        case PrimitiveTypeCode.UInt32: break;
                        case PrimitiveTypeCode.UInt32Nullable: break;
                        case PrimitiveTypeCode.Int64: break;
                        case PrimitiveTypeCode.Int64Nullable: break;
                        case PrimitiveTypeCode.UInt64: break;
                        case PrimitiveTypeCode.UInt64Nullable: break;
                        case PrimitiveTypeCode.Single: break;
                        case PrimitiveTypeCode.SingleNullable: break;
                        case PrimitiveTypeCode.Double:
                        case PrimitiveTypeCode.DoubleNullable:
                        case PrimitiveTypeCode.Decimal:
                        case PrimitiveTypeCode.DecimalNullable:
                            PopulateDecimalValue(reader, structure, descriptor, read);
                            break;
                        case PrimitiveTypeCode.DateTime:
                        case PrimitiveTypeCode.DateTimeNullable:
                            PopulateDateTimeValue(reader, structure, descriptor, read);
                            break;
                        case PrimitiveTypeCode.DateTimeOffset: break;
                        case PrimitiveTypeCode.DateTimeOffsetNullable: break;
                        case PrimitiveTypeCode.Guid: break;
                        case PrimitiveTypeCode.GuidNullable: break;
                        case PrimitiveTypeCode.TimeSpan: break;
                        case PrimitiveTypeCode.TimeSpanNullable: break;
                        case PrimitiveTypeCode.BigInteger: break;
                        case PrimitiveTypeCode.BigIntegerNullable: break;
                        case PrimitiveTypeCode.Uri: break;
                        case PrimitiveTypeCode.String:
                            PopulateStringValue(reader, structure, descriptor, read);
                            break;
                        case PrimitiveTypeCode.Bytes: break;
                        case PrimitiveTypeCode.DBNull: break;
                    }
                }
            }
        }

        internal static void PopulateStringValue(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            var text = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsString(valueInfo.Path) :
                                                     read ? reader.ReadAsString() : (string)reader.Value;
            descriptor.Info.SetValue(structure.Instance, text);
        }

        internal static void PopulateDateTimeValue(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            var dateString = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsString(valueInfo.Path) :
                                                           read ? reader.ReadAsString() : (string)reader.Value;
            if (dateString != null) {
                dateString = dateString.Substring(0, valueInfo.Picture.Scale);
                var date = default(DateTime);
                if (DateTime.TryParseExact(dateString, valueInfo.Format, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out date)) {
                    var existingDateObject = descriptor.Info.GetValue(structure.Instance);
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
                    descriptor.Info.SetValue(structure.Instance, date);
                }
            }
        }

        internal static void PopulateDecimalValue(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            var numberFloat = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsDecimal(valueInfo.Path, descriptor.ValueInfo.Picture, reader.Grammar.DecimalMark) :
                                                            read ? reader.ReadAsDecimal(descriptor.ValueInfo.Picture) : (decimal?)reader.Value;
            if (numberFloat != null) {
                descriptor.Info.SetValue(structure.Instance, numberFloat);
            }
        }

        internal static void PopulateInt32Value(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            if (!descriptor.Info.PropertyType.IsEnum()) {
                var integer = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsInt32(valueInfo.Path, reader.Culture) :
                                                            read ? reader.ReadAsInt32() : (int?)reader.Value;
                if (integer.HasValue) {
                    descriptor.Info.SetValue(structure.Instance, ConvertUtils.ConvertOrCast(integer.Value, CultureInfo.InvariantCulture, descriptor.Info.PropertyType));
                }
            } else {
                var enumValueString = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsString(valueInfo.Path) :
                                                                    read ? reader.ReadAsString() : (string)reader.Value;
                if (!string.IsNullOrEmpty(enumValueString)) {
                    descriptor.Info.SetValue(structure.Instance, ConvertUtils.ConvertOrCast(enumValueString, CultureInfo.InvariantCulture, descriptor.Info.PropertyType));
                }
            }
        }

        internal static void PopulateCharValue(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            var text = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsString(valueInfo.Path) :
                                                     read ? reader.ReadAsString() : (string)reader.Value;

            if (!string.IsNullOrEmpty(text)) {
                if (text.Length > 1)
                    throw new EdiException("Unable to convert string '{0}' to char. It is more than 1 character long.".FormatWith(reader.Culture, text));
                descriptor.Info.SetValue(structure.Instance, text[0]);
            }
        }

        internal static void PopulateBooleanValue(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            var text = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsString(valueInfo.Path) :
                                                     read ? reader.ReadAsString() : (string)reader.Value;

            if (!string.IsNullOrEmpty(text)) {
                int integerValue = default(int);
                bool booleanValue = default(bool);
                if (int.TryParse(text, NumberStyles.Integer, reader.Culture, out integerValue)) {
                    booleanValue = integerValue == 1;
                } else if (bool.TryParse(text, out booleanValue)) {
                } else {
                    throw new EdiException("Unable to convert string '{0}' to boolean.".FormatWith(reader.Culture, text));
                }
                descriptor.Info.SetValue(structure.Instance, booleanValue);
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
            var property = default(EdiPropertyDescriptor);
            if (newContainer == EdiStructureType.Segment) {
                // for custom segment group structures that live side by side with other segments
                // we must inspect the stack in order to findout if we reached the end of the container. 
                // This is indicated by the end segment on the segment group attribute.
                if (stack.Count > 0 && current.Container == EdiStructureType.SegmentGroup) {
                    var groupMark = current.Descriptor.SegmentGroupInfo;
                    if (groupMark.SequenceEndInternal.Segment.Equals(reader.Value) ||
                        groupMark.StartInternal.Segment.Equals(reader.Value)) {
                        stack.Pop();
                        current = stack.Peek();
                    }
                }
                property = FindForCurrentSegment(reader, current, newContainer);
                // for segment group start we must inspect the descriptor
                if (property != null && property.MarksSegmentGroup &&
                                        property.SegmentGroupInfo.StartInternal.Segment.Equals(reader.Value)) {
                    return TryCreateContainer(reader, stack, EdiStructureType.SegmentGroup);
                }
            } else if (newContainer == EdiStructureType.Element) {
                property = FindForCurrentElement(reader, current, newContainer);
            } else {
                property = FindForCurrentLogicalStructure(reader, current, newContainer);
            }
            if (property == null) {
                return false;
            }
            object propValue = property.Info.GetValue(current.Instance, null);
            if (propValue == null) {
                if (property.Info.PropertyType.IsCollectionType()) {
                    if (property.Info.PropertyType.IsArray) {
                        var initalLength = 0;
                        propValue = Activator.CreateInstance(property.Info.PropertyType, initalLength);
                    } else {
                        var baseType = typeof(List<>);
                        var genericType = baseType.MakeGenericType(property.Info.PropertyType.GetGenericArguments().First());
                        propValue = Activator.CreateInstance(genericType);
                    }
                } else {
                    propValue = Activator.CreateInstance(property.Info.PropertyType);
                }
                property.Info.SetValue(current.Instance, propValue);
            }
            if (propValue is IList) {
                var itemType = default(Type);
                var item = default(object);
                if (property.Info.PropertyType.IsArray) {
                    itemType = property.Info.PropertyType.GetElementType();
                    item = Activator.CreateInstance(itemType);
                    var newArray = Array.CreateInstance(itemType, ((IList)propValue).Count + 1);
                    var oldArray = ((Array)propValue);
                    Array.Copy(oldArray, newArray, oldArray.Length);
                    newArray.SetValue(item, newArray.Length - 1);
                    property.Info.SetValue(current.Instance, newArray);
                } else {
                    itemType = property.Info.PropertyType.GetGenericArguments().First();
                    item = Activator.CreateInstance(itemType);
                    ((IList)propValue).Add(item);
                }
                propValue = item;
            }
            stack.Push(new EdiStructure(newContainer, propValue, index, current.CachedReads));
            return true;
        }

        private EdiPropertyDescriptor FindForCurrentSegment(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType) {
            currentStructure.CachedReads.Clear();
            var candidates = currentStructure.GetMatchingProperties(newContainerType);
            if (candidates.Length == 0) {
                return null;
            }
            var property = default(EdiPropertyDescriptor);
            if (reader.TokenType == EdiToken.SegmentName) {
                var matches = candidates.Where(p => p.Segment.Equals(reader.Value)).ToArray();
                if (matches.Length == 0) {
                    property = null;
                } else if (matches.Length == 1 && matches[0].ConditionInfo == null) {
                    property = matches[0];
                } else {
                    property = ConditionalMatch(reader, currentStructure, newContainerType, matches);
                }
            }
            return property;
        }

        private EdiPropertyDescriptor FindForCurrentElement(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType) {
            var candidates = currentStructure.GetMatchingProperties(newContainerType);
            if (candidates.Length == 0) {
                return null;
            }
            var property = default(EdiPropertyDescriptor);
            if (reader.TokenType == EdiToken.ElementStart) {
                var matches = candidates.Where(p => p.PathInfo.PathInternal.ToString("E").Equals(reader.Path)).ToArray();
                if (matches.Length == 0) {
                    property = null;
                } else if (matches.Length == 1 && matches[0].ConditionInfo == null) {
                    property = matches[0];
                } else {
                    property = ConditionalMatch(reader, currentStructure, newContainerType, matches);
                }
            }
            return property;
        }

        private EdiPropertyDescriptor FindForCurrentLogicalStructure(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType) {
            var candidates = currentStructure.GetMatchingProperties(newContainerType);
            var property = default(EdiPropertyDescriptor);
            if (candidates.Length == 0) {
                return null;
            }
            if (candidates.Length == 1 && candidates[0].ConditionInfo == null) {
                property = candidates[0];
            } else {
                property = ConditionalMatch(reader, currentStructure, newContainerType, candidates);
            }
            //if (property.MarksSegmentGroupStart && !property.SegmentGroupInfo.StartInternal.Segment.Equals(reader.Value)) {
            //    return null;
            //}
            return property;
        }

        private static EdiPropertyDescriptor ConditionalMatch(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType, EdiPropertyDescriptor[] matches) {
            if (!matches.All(p => p.ConditionInfo != null)) {
                throw new EdiException(
                "More than one properties on type '{0}' have the '{1}' attribute. Please add a 'Condition' attribute to all properties in order to discriminate where each {2} will go."
                    .FormatWith(CultureInfo.InvariantCulture, currentStructure.Descriptor.ClrType.Name, newContainerType, newContainerType));
            }
            if (matches.Select(p => p.Path).Distinct().Count() != 1) {
                throw new EdiException("More than one properties on type '{0}' have the '{1}' attribute but the 'Condition' attribute has a different search path declared."
                    .FormatWith(CultureInfo.InvariantCulture, currentStructure.Descriptor.ClrType.Name, newContainerType));
            }

            var readCache = currentStructure.CachedReads;
            var path = string.Empty;
            do {
                reader.Read();
                path = reader.Path;
                readCache.Enqueue(new EdiEntry(path, reader.TokenType, reader.Value as string));
            } while (reader.TokenType != EdiToken.SegmentStart && matches[0].Path != path);

            var discriminator = reader.ReadAsString();
            var property = matches.SingleOrDefault(p => p.ConditionInfo.MatchValue == discriminator);
            readCache.Enqueue(new EdiEntry(path, reader.TokenType, discriminator));
            return property;
        }
        #endregion

        /// <summary>
        /// Serializes the specified <see cref="Object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="textWriter">The <see cref="TextWriter"/> used to write the EDI structure.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for reading from the text reader</param>
        /// <param name="value">The <see cref="Object"/> to serialize.</param>
        public void Serialize(TextWriter textWriter, IEdiGrammar grammar, object value) {
            Serialize(new EdiTextWriter(textWriter, grammar), value, null);
        }


        /// <summary>
        /// Serializes the specified <see cref="Object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="textWriter">The <see cref="TextWriter"/> used to write the EDI structure.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for reading from the text reader</param>
        /// <param name="value">The <see cref="Object"/> to serialize.</param>
        /// <param name="objectType">
        /// The type of the value being serialized.
        /// Specifing the type is optional.
        /// </param>
        public void Serialize(TextWriter textWriter, IEdiGrammar grammar, object value, Type objectType) {
            Serialize(new EdiTextWriter(textWriter, grammar), value, objectType);
        }

        /// <summary>
        /// Serializes the specified <see cref="Object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="EdiWriter"/>. 
        /// </summary>
        /// <param name="ediWriter">The <see cref="EdiWriter"/> used to write the EDI structure.</param>
        /// <param name="value">The <see cref="Object"/> to serialize.</param>
        public void Serialize(EdiWriter ediWriter, object value) {
            SerializeInternal(ediWriter, value, null);
        }

        /// <summary>
        /// Serializes the specified <see cref="Object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="EdiWriter"/>. 
        /// </summary>
        /// <param name="ediWriter">The <see cref="EdiWriter"/> used to write the EDI structure.</param>
        /// <param name="value">The <see cref="Object"/> to serialize.</param>
        /// <param name="objectType">
        /// The type of the value being serialized.
        /// Specifing the type is optional.
        /// </param>
        public void Serialize(EdiWriter ediWriter, object value, Type objectType) {
            SerializeInternal(ediWriter, value, objectType);
        }

        #region Write internals
        
        internal virtual void SerializeInternal(EdiWriter writer, object value, Type objectType) {
            if (value == null) {
                writer.WriteNull();
                return;
            }
            objectType = objectType ?? value.GetType();
        }

        #endregion
    }
}
