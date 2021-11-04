using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using indice.Edi.Utilities;
using indice.Edi.Serialization;

namespace indice.Edi
{
    /// <summary>
    /// Serializes and deserializes objects into and from the EDI format.
    /// The <see cref="EdiSerializer"/> enables you to control how objects are encoded into EDI.
    /// </summary>
    public class EdiSerializer
    {
        /// <summary>
        /// Gets or sets a value indicating whether segment groups should automatically end when a non-matching segment is found. 
        /// </summary>
        [Obsolete("Please do not use it has sideffects. If you have a SegmentGroup issue try to explicitly declare the list of segments that take part in order for it to close normally.")]
        public bool AutoEndSegmentGroups { get; set; }

        /// <summary>
        /// Enables compression when Serializing to edi. Sets the internal <see cref="EdiWriter.EnableCompression"/>. By default is set to true.
        /// </summary>
        public bool EnableCompression { get; set; } = true;

        /// <summary>
        /// If true will suppress any exceptions thrown due to bad escape sequences. Sets the internal <see cref="EdiReader.SuppressBadEscapeSequenceErrors"/>. By default is set to false.
        /// </summary>
        public bool SuppressBadEscapeSequenceErrors { get; set; }

        /// <summary>
        /// Deserializes the EDI structure contained by the specified <see cref="EdiReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="EdiReader"/> that contains the EDI structure to deserialize.</param>
        /// <returns>The <see cref="object"/> being deserialized.</returns>
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
            reader.SuppressBadEscapeSequenceErrors = SuppressBadEscapeSequenceErrors;

            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var implicitSegments = new[] {
                reader.Grammar.FunctionalGroupHeaderTag,
                reader.Grammar.FunctionalGroupTrailerTag,
                reader.Grammar.InterchangeHeaderTag,
                reader.Grammar.InterchangeTrailerTag,
                reader.Grammar.MessageHeaderTag,
                reader.Grammar.MessageTrailerTag
            };

            // the output value
            object value = null;

            var stack = new Stack<EdiStructure>();
            var structuralComparer = default(EdiPathComparer);
            // If this is not a collection type asume this type is the interchange.
            if (!objectType.IsCollectionType()) {

                while (reader.Read()) {
                    if (reader.IsStartInterchange) {
                        stack.Push(new EdiStructure(EdiStructureType.Interchange, Activator.CreateInstance(objectType)));
                    } else if (reader.IsEndInterchange) {
                        while (stack.Peek().StructureType != EdiStructureType.Interchange) {
                            stack.Pop();
                        }
                        value = stack.Peek().Instance;
                    }

                    if (reader.IsStartMessage) {
                        if (stack.Count == 0) {
                            stack.Push(new EdiStructure(EdiStructureType.Message, Activator.CreateInstance(objectType)));
                        } else { 
                            TryCreateContainer(reader, stack, EdiStructureType.Message);
                        }
                    } else if (reader.IsEndMessage) {
                        while (stack.Peek().StructureType > EdiStructureType.Message) {
                            stack.Pop();
                        }
                        value = stack.Peek().Instance;
                    }

                    if (reader.IsStartGroup) {
                        TryCreateContainer(reader, stack, EdiStructureType.Group);
                    } else if (reader.IsEndGroup) {
                        while (stack.Peek().StructureType > EdiStructureType.Group) {
                            stack.Pop();
                        }
                        value = stack.Peek().Instance;
                    } 
                    if (reader.TokenType == EdiToken.SegmentName) {
                        while (true) {
                            if (TryCreateContainer(reader, stack, EdiStructureType.SegmentGroup)
                                || TryCreateContainer(reader, stack, EdiStructureType.Segment)
                                || implicitSegments.Contains(reader.Value)
                                || !AutoEndSegmentGroups) {
                                break;
                            }

                            stack.Pop();

                            if (stack.Count == 0) {
                                throw new EdiException(
                                    "Unable to deserialize segment {0}. No matching property found on stack.".FormatWith(
                                        reader.Culture,
                                        reader.Value));
                            }
                        }
                    }

                    if (reader.TokenType == EdiToken.ElementStart) {
                        TryCreateContainer(reader, stack, EdiStructureType.Element);
                    }
                    else if (stack.Count > 0 && stack.Peek().CachedReads.Count > 0) {
                        var allCachedReads = stack.Peek().CachedReads;
                        while (allCachedReads.Count > 0 && TryCreateContainer(reader, stack, EdiStructureType.Element)) {
                            if (stack.Peek().CachedReads.Any(x => x.HasValue)) {
                                PopulateValue(reader, stack, ref structuralComparer);
                            }
                        }
                    }

                    if (reader.TokenType == EdiToken.ComponentStart || (stack.Count > 0 && stack.Peek().CachedReads.Count > 0 && reader.TokenType.IsPrimitiveToken())) {
                        PopulateValue(reader, stack, ref structuralComparer);
                    }
                }
            }
            // else if this is indeed a collection type this must be a collection of messages.
            else {
                throw new NotImplementedException("Collection types are not supported as the root Type. Try to wrap List of Messages inside a container type.");
            }

            return value;
        }

        internal void PopulateValue(EdiReader reader, Stack<EdiStructure> stack, ref EdiPathComparer structuralComparer) {
            structuralComparer = new EdiPathComparer(reader.Grammar);
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
                                                           p.Path != null &&
                                                            (((EdiPath)p.Path).Equals(reader.Path) || structure.CachedReads.ContainsPath(p.Path))
                                                           ).OrderBy(p => (EdiPath)p.Path, structuralComparer).ToArray();

                for (var i = 0; i < valueProps.Length; i++) {
                    var descriptor = valueProps[i];
                    // should I use the text reader? 
                    // Values must be read only once on first pass! 
                    // Otherwise the reader position moves forward 
                    // and the serializer gets out of sync.
                    var useTheReader = current == structure && reader.TokenType == EdiToken.ComponentStart && ((EdiPath)descriptor.Path).Equals(reader.Path) && i == 0;
                    switch (ConvertUtils.GetTypeCode(descriptor.Info.PropertyType)) {
                        case PrimitiveTypeCode.Empty: break;
                        case PrimitiveTypeCode.Object:
                            PopulateObjectValue(reader, structure, descriptor, useTheReader);
                            break;
                        case PrimitiveTypeCode.Char:
                        case PrimitiveTypeCode.CharNullable:
                            PopulateCharValue(reader, structure, descriptor, useTheReader);
                            break;
                        case PrimitiveTypeCode.Boolean:
                        case PrimitiveTypeCode.BooleanNullable:
                            PopulateBooleanValue(reader, structure, descriptor, useTheReader);
                            break;
                        case PrimitiveTypeCode.Byte: break;
                        case PrimitiveTypeCode.ByteNullable: break;
                        case PrimitiveTypeCode.UInt32: break;
                        case PrimitiveTypeCode.UInt32Nullable: break;
                        case PrimitiveTypeCode.SByte: break;
                        case PrimitiveTypeCode.SByteNullable: break;
                        case PrimitiveTypeCode.Int16: break;
                        case PrimitiveTypeCode.Int16Nullable: break;
                        case PrimitiveTypeCode.UInt16: break;
                        case PrimitiveTypeCode.UInt16Nullable: break;
                        case PrimitiveTypeCode.Int32:
                        case PrimitiveTypeCode.Int32Nullable:
                            PopulateInt32Value(reader, structure, descriptor, useTheReader);
                            break;
                        case PrimitiveTypeCode.UInt64:
                        case PrimitiveTypeCode.UInt64Nullable:
                        case PrimitiveTypeCode.Int64:
                        case PrimitiveTypeCode.Int64Nullable:
                            PopulateInt64Value(reader, structure, descriptor, useTheReader);
                            break;
                        case PrimitiveTypeCode.Single:
                        case PrimitiveTypeCode.SingleNullable:
                        case PrimitiveTypeCode.Double:
                        case PrimitiveTypeCode.DoubleNullable:
                        case PrimitiveTypeCode.Decimal:
                        case PrimitiveTypeCode.DecimalNullable:
                            PopulateDecimalValue(reader, structure, descriptor, useTheReader);
                            break;
                        case PrimitiveTypeCode.DateTime:
                        case PrimitiveTypeCode.DateTimeNullable:
                            PopulateDateTimeValue(reader, structure, descriptor, useTheReader);
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
                            PopulateStringValue(reader, structure, descriptor, useTheReader);
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
                if (valueInfo.Picture != default(Picture)) {
                    dateString = dateString.Substring(0, Math.Min(dateString.Length, valueInfo.Picture.Scale));
                }
                var date = default(DateTime);
                if (dateString.TryParseEdiDate(valueInfo.Format, CultureInfo.InvariantCulture, out date)) {
                    var existingDateObject = descriptor.Info.GetValue(structure.Instance);
                    var existingDate = default(DateTime);
                    if (existingDateObject != null && !existingDateObject.Equals(default(DateTime))) {
                        if (existingDateObject is DateTime?) {
                            existingDate = ((DateTime?)existingDateObject).Value;
                        } else {
                            existingDate = ((DateTime)existingDateObject);
                        }
                        if (date - date.Date == default(TimeSpan) && DateTime.Today != date) {
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
        
        internal static void PopulateInt64Value(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            if (!descriptor.Info.PropertyType.IsEnum()) {
                var integer = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsInt64(valueInfo.Path, reader.Culture) :
                                                            read ? reader.ReadAsInt64() : (int?)reader.Value;
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
                var integerValue = default(int);
                var booleanValue = default(bool);
                if (int.TryParse(text, NumberStyles.Integer, reader.Culture, out integerValue)) {
                    booleanValue = integerValue == 1;
                } else if (bool.TryParse(text, out booleanValue)) {
                } else {
                    throw new EdiException("Unable to convert string '{0}' to boolean.".FormatWith(reader.Culture, text));
                }
                descriptor.Info.SetValue(structure.Instance, booleanValue);
            }
        }

        internal static void PopulateObjectValue(EdiReader reader, EdiStructure structure, EdiPropertyDescriptor descriptor, bool read) {
            var cache = structure.CachedReads;
            var valueInfo = descriptor.ValueInfo;
            var text = cache.ContainsPath(valueInfo.Path) ? cache.ReadAsString(valueInfo.Path) :
                                                     read ? reader.ReadAsString() : (string)reader.Value;
            descriptor.Info.SetValue(structure.Instance, ConvertUtils.ConvertOrCast(text, reader.Culture, descriptor.Info.PropertyType));
        }

        internal bool TryCreateContainer(EdiReader reader, Stack<EdiStructure> stack, EdiStructureType newContainer) {
            var index = 0;
            if (stack.Count == 0)
                return false;

            // clear once upon segment start. This is done here in order to keep any findings for future use
            if ((newContainer == EdiStructureType.SegmentGroup || newContainer == EdiStructureType.Segment) && reader.TokenType == EdiToken.SegmentName) {
                stack.Peek().CachedReads.Clear();
            }

            if (newContainer == EdiStructureType.SegmentGroup &&
                stack.Peek().StructureType >= EdiStructureType.SegmentGroup) {
                // strict hierarchy
                while (stack.Peek().StructureType > newContainer) {
                    var previous = stack.Pop(); // close this level
                }
                // nested hierarchy
                var readerSegment = reader.Value;
                foreach (var level in stack) {
                    if (!level.IsGroup)
                        continue;
                    var groupStart = level.GroupStart;
                    var sequenceEnd = level.SequenceEnd;
                    if (groupStart.Segment.Equals(readerSegment)) {
                        if (PositionMatchesStructure(reader, level, readerSegment as string)) { // if new occurance of my level or sibling found
                            level.Close(); // Close this level
                            index = level.Index + 1;
                            continue;
                        } else {
                            var canAdvance = FindForCurrentSegment(reader, level, EdiStructureType.SegmentGroup) != null;
                            if (canAdvance) {
                                break;
                            } 
                            level.Close(); // Close this level
                            index = level.Index + 1;
                            continue;
                        }
                    } else if (sequenceEnd.HasValue && sequenceEnd.Value.Segment.Equals(readerSegment)) {
                        level.Close(); // Close this level
                        continue;
                    } else if (level.GroupMembers.Length > 1 && !level.GroupContains(readerSegment as string)) {
                        level.Close(); // Close this level
                        continue;
                    }
                }
                var clearUpTo = stack.Reverse().FirstOrDefault(x => x.IsClosed)?.Container;
                if (clearUpTo != null) {
                    while (stack.Peek() != clearUpTo) {
                        stack.Pop();
                    }
                }
            } else {
                // strict hierarchy
                while (stack.Peek().StructureType >= newContainer) {
                    var previous = stack.Pop(); // close this level
                    if (previous.StructureType == newContainer)
                        index = previous.Index + 1; // seed collection index 
                }
            }

            var current = stack.Peek();
            var property = default(EdiPropertyDescriptor);
            var childCache = default(Queue<EdiEntry>);
            switch (newContainer) {
                case EdiStructureType.SegmentGroup:
                    property = FindForCurrentSegment(reader, current, newContainer);
                    break;
                case EdiStructureType.Segment:
                    property = FindForCurrentSegment(reader, current, newContainer);
                    break;
                case EdiStructureType.Element:
                    property = FindForCurrentElement(reader, current, newContainer, out childCache);
                    break;
                default:
                    property = FindForCurrentLogicalStructure(reader, current, newContainer);
                    break;
            }
            if (property == null) {
                return false;
            }
            var propValue = property.Info.GetValue(current.Instance, null);
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
            stack.Push(new EdiStructure(newContainer, current, property, propValue, index, childCache ?? current.CachedReads));
            return true;
        }

        private EdiPropertyDescriptor FindForCurrentSegment(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType) {
            var candidates = currentStructure.GetMatchingProperties(newContainerType);
            if (candidates.Length == 0) {
                return null;
            }
            var property = default(EdiPropertyDescriptor);
            if (reader.TokenType == EdiToken.SegmentName || currentStructure.CachedReads.Count > 0) {
                var segmentName = reader.TokenType == EdiToken.SegmentName ? reader.Value : ((EdiPath)currentStructure.CachedReads.Peek().Path).Segment;
                var matches = candidates.Where(p => segmentName.Equals(p.Segment)).ToArray();
                if (matches.Length == 0) {
                    property = null;
                } else if (matches.Length == 1 && matches[0].Conditions == null) {
                    property = matches[0];
                } else {
                    property = ConditionalMatch(reader, currentStructure, newContainerType, matches);
                }
            }
            return property;
        }

        private EdiPropertyDescriptor FindForCurrentElement(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType, out Queue<EdiEntry> elementReads) {
            elementReads = null;
            var candidates = currentStructure.GetMatchingProperties(newContainerType);
            if (candidates.Length == 0) {
                return null;
            }
            var property = default(EdiPropertyDescriptor);
            if (reader.TokenType == EdiToken.ElementStart || currentStructure.CachedReads.Count > 0) {
                var elementPath = reader.TokenType == EdiToken.ElementStart ? reader.Path : ((EdiPath)currentStructure.CachedReads.Peek().Path).ToString("E");
                var matches = candidates.Where(p => p.PathInfo.PathInternal.Equals(elementPath)).ToArray();
                if (matches.Length == 0) {
                    property = null;
                } else if (matches.Length == 1 && matches[0].Conditions == null) {
                    property = matches[0];
                } else {
                    property = ConditionalMatch(reader, currentStructure, newContainerType, matches);
                }
                if (property != null) {
                    elementReads = new Queue<EdiEntry>();
                    var parentCache = currentStructure.CachedReads;
                    while (parentCache.Count > 0 && elementPath == ((EdiPath)parentCache.Peek().Path).ToString("E")) {
                        elementReads.Enqueue(parentCache.Dequeue());
                    }
                    //foreach (var item in currentStructure.CachedReads) {
                    //    if (elementPath == ((EdiPath)item.Path).ToString("E")) { 
                    //        elementReads.Enqueue(item);
                    //    }
                    //}
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
            if (candidates.Length == 1 && candidates[0].Conditions == null) {
                property = candidates[0];
            } else {
                property = ConditionalMatch(reader, currentStructure, newContainerType, candidates);
            }
            return property;
        }

        private static EdiPropertyDescriptor ConditionalMatch(EdiReader reader, EdiStructure currentStructure, EdiStructureType newContainerType, params EdiPropertyDescriptor[] candidates) {
            if (!candidates.All(p => p.Conditions != null)) {
                throw new EdiException(
                "More than one properties on type '{0}' have the '{1}' attribute. Please add a 'Condition' attribute to all properties in order to discriminate where each {2} will go."
                    .FormatWith(CultureInfo.InvariantCulture, currentStructure.Descriptor.ClrType.Name, newContainerType, newContainerType));
            }

            var searchResults = SearchForward(reader, currentStructure.CachedReads, candidates.SelectMany(p => p.Conditions.Select(c => c.Path)));
            //if (searchResults.Length != 1) {
            //    throw new EdiException("More than one properties on type '{0}' have the '{1}' attribute but the 'Condition' attribute has a different search path declared."
            //        .FormatWith(CultureInfo.InvariantCulture, currentStructure.Descriptor.ClrType.Name, newContainerType));
            //}
            var property = candidates.SingleOrDefault(p => p.ConditionStackMode == EdiConditionStackMode.All ? p.Conditions.All(c => c.SatisfiedBy(searchResults[c.PathInternal]))
                                                                                                             : p.Conditions.Any(c => c.SatisfiedBy(searchResults[c.PathInternal])));
            if (property != null)
                return property;
            return null;
        }

        private static bool PositionMatchesStructure(EdiReader reader, EdiStructure structure, string segmentName) {
            if (structure.Conditions == null || structure.Conditions.Length == 0)
                return true; // cannot determine.
            var searchResults = SearchForward(reader, structure.CachedReads, structure.Conditions.Select(c => c.Path));

            var result = structure.ConditionStackMode == EdiConditionStackMode.All ? structure.Conditions.All(c => c.SatisfiedBy(searchResults[c.PathInternal]))
                                                                                   : structure.Conditions.Any(c => c.SatisfiedBy(searchResults[c.PathInternal]));

            if (result) {
                return true;
            }

            // search siblings on the same level before returning.
            var matchingProperties = structure.Container.GetMatchingProperties(segmentName);
            if (matchingProperties == null || matchingProperties.Length == 0)
                return false;
            foreach (var prop in matchingProperties) {
                if (!prop.MarksSegmentGroup)
                    continue;
                // if there is simply a matching sibling with no conditions return ok.
                if (prop.Conditions == null)
                    return true;
                searchResults = SearchForward(reader, structure.Container.CachedReads, prop.Conditions.Select(c => c.Path));

                var check = prop.ConditionStackMode == EdiConditionStackMode.All ? prop.Conditions.All(c => c.SatisfiedBy(searchResults[c.PathInternal]))
                                                                                 : prop.Conditions.Any(c => c.SatisfiedBy(searchResults[c.PathInternal]));
                if (check) {
                    return true;
                }
            }
            return false;
        }

        private static Dictionary<string, string> SearchForward(EdiReader reader, Queue<EdiEntry> cache, IEnumerable<string> pathsToSeekForValues) {
            var searchResults = pathsToSeekForValues.Distinct().ToDictionary(x => x, x => (string)null);
            foreach (var path in searchResults.Keys.ToArray()) {
                // search the cache first.
                var value = default(string);
                var found = false;
                if (cache.Count > 0) {
                    var entry = cache.Where(r => r.Path == path && r.Token.IsPrimitiveToken()).SingleOrDefault();
                    if (!default(EdiEntry).Equals(entry)) {
                        found = true;
                        value = entry.Value;
                    }
                }
                if (!found)
                    // if nothing found search the reader (arvance forward).
                    do {
                        if (reader.Path == path) {
                            value = reader.ReadAsString();
                            cache.Enqueue(new EdiEntry(reader.Path, reader.TokenType, value));
                            found = true;
                            value = reader.Value as string; // if found break;
                            break;
                        } else {
                            reader.Read();
                            cache.Enqueue(new EdiEntry(reader.Path, reader.TokenType, reader.Value as string));
                        }
                    } while (!found && reader.TokenType != EdiToken.SegmentStart);

                if (found) {
                    searchResults[path] = value;
                }
            }
            return searchResults;
        }


        #endregion

        /// <summary>
        /// Serializes the specified <see cref="object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="textWriter">The <see cref="TextWriter"/> used to write the EDI structure.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for reading from the text reader</param>
        /// <param name="value">The <see cref="object"/> to serialize.</param>
        public void Serialize(TextWriter textWriter, IEdiGrammar grammar, object value) {
            var ediTextWriter = new EdiTextWriter(textWriter, grammar);
            Serialize(ediTextWriter, value, null);
            ediTextWriter.Close();
        }


        /// <summary>
        /// Serializes the specified <see cref="object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="textWriter">The <see cref="TextWriter"/> used to write the EDI structure.</param>
        /// <param name="grammar">The <see cref="IEdiGrammar"/> to use for reading from the text reader</param>
        /// <param name="value">The <see cref="object"/> to serialize.</param>
        /// <param name="objectType">
        /// The type of the value being serialized.
        /// Specifing the type is optional.
        /// </param>
        public void Serialize(TextWriter textWriter, IEdiGrammar grammar, object value, Type objectType) {
            var ediTextWriter = new EdiTextWriter(textWriter, grammar);
            Serialize(ediTextWriter, value, objectType);
            ediTextWriter.Close();
        }

        /// <summary>
        /// Serializes the specified <see cref="object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="EdiWriter"/>. 
        /// </summary>
        /// <param name="ediWriter">The <see cref="EdiWriter"/> used to write the EDI structure.</param>
        /// <param name="value">The <see cref="object"/> to serialize.</param>
        public void Serialize(EdiWriter ediWriter, object value) {
            ediWriter.EnableCompression = EnableCompression;
            SerializeInternal(ediWriter, value, null);
            ediWriter.Close();
        }

        /// <summary>
        /// Serializes the specified <see cref="object"/> and writes the EDI structure
        /// to a <c>Stream</c> using the specified <see cref="EdiWriter"/>. 
        /// </summary>
        /// <param name="ediWriter">The <see cref="EdiWriter"/> used to write the EDI structure.</param>
        /// <param name="value">The <see cref="object"/> to serialize.</param>
        /// <param name="objectType">
        /// The type of the value being serialized.
        /// Specifing the type is optional.
        /// </param>
        public void Serialize(EdiWriter ediWriter, object value, Type objectType) {
            ediWriter.EnableCompression = EnableCompression;
            SerializeInternal(ediWriter, value, objectType);
            ediWriter.Close();
        }

        #region Write internals

        internal virtual void SerializeInternal(EdiWriter writer, object value, Type objectType) {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (value == null) {
                writer.WriteNull();
                return;
            }
            objectType = objectType ?? value.GetType();
            var stack = new Stack<EdiStructure>();
            // If this is not a collection type asume this type is the interchange.
            if (!objectType.IsCollectionType()) {
                stack.Push(new EdiStructure(EdiStructureType.Interchange, value));
                if (writer.WriteState == WriteState.Start)
                    writer.WriteServiceStringAdvice();
                SerializeStructure(writer, stack);
            }
            // else if this is indeed a collection type this must be a collection of messages.
            else {
                throw new NotImplementedException("Collection types are not supported as the root Type. Try to wrap List of Messages inside a container type.");
            }
        }



        private static void SerializeStructure(EdiWriter writer, Stack<EdiStructure> stack, EdiPathComparer structuralComparer = null) {
            structuralComparer = structuralComparer ?? new EdiPathComparer(writer.Grammar);
            var structure = stack.Peek();
            var properies = structure.GetOrderedProperties(structuralComparer);
            foreach (var property in properies) {
                var value = property.Info.GetValue(structure.Instance);
                if (property.ValueInfo != null) {
                    var path = (EdiPath)writer.Path;
                    var propertyPath = property.PathInfo.PathInternal;
                    var container = stack.Skip(1).FirstOrDefault();
                    if (propertyPath.Segment.IsWildcard) {
                        if (container.Descriptor.Path.HasValue && !container.Descriptor.Path.Value.Segment.IsWildcard) {
                            propertyPath = new EdiPath(container.Descriptor.Path.Value.Segment, propertyPath.Element, propertyPath.Component);
                        }
                    }
                    if (propertyPath.Element.IsWildcard) {
                        propertyPath = new EdiPath(propertyPath.Segment, new EdiPathFragment(structure.Index.ToString()), propertyPath.Component);
                    }
                    if (path.Segment != propertyPath.Segment ||
                        structuralComparer.Compare(path, propertyPath) >= 0) {
                        writer.WriteSegmentName(propertyPath.Segment);
                        path = (EdiPath)writer.Path;
                    }
                    // the following loop handles the write of unmapped preceding elements/components to the one being writen 
                    // so that path progression stays intact even though we do not have all properties present on the model.

                    while (structuralComparer.Compare(path, propertyPath) < 0) {
                        if (!path.Element.Equals(propertyPath.Element)) {
                            if (path.ElementIndex == 0 && writer.WriteState != WriteState.Component && writer.WriteState != WriteState.Element)
                                writer.WriteToken(EdiToken.Null);
                            else
                                writer.WriteToken(EdiToken.ElementStart);
                        } else if (!path.Component.Equals(propertyPath.Component)) {
                            if (path.ComponentIndex == 0 && writer.WriteState != WriteState.Component)
                                writer.WriteToken(EdiToken.Null);
                            else
                                writer.WriteToken(EdiToken.ComponentStart);
                        }
                        path = (EdiPath)writer.Path;
                    }
                    // handle auto generated values.
                    if (property.AutoGenerationInfo != null) {
                        // do stuff.
                        // there should be plenty of things to work with inside the EdiWriter itself. 
                        // We are already keeping keeping track of current position with an index.
                        // But it may need to track more stuff in order for this to happen.
                    }
                    writer.WriteValue(value, property.ValueInfo.Picture, property.ValueInfo.Format);
                } else {
                    // this is somekind of structure. Group/Message/Segment/SegmentGroup/Element
                    // is it a collection of some kind?
                    var container = property.Attributes.InferStructure();
                    if (property.Info.PropertyType.IsCollectionType()) {
                        var itemType = default(Type);
                        var collection = (value ?? new object[0]) as IList;
                        if (property.Info.PropertyType.IsArray) {
                            itemType = property.Info.PropertyType.GetElementType();
                        } else {
                            itemType = property.Info.PropertyType.GetGenericArguments().First();
                        }
                        int range_offset = 0;
                        if (container == EdiStructureType.Element && property.PathInfo.PathInternal.Element.IsRange) {
                            range_offset = property.PathInfo.PathInternal.Element.Min;
                        }
                        for (var i = 0; i < collection.Count; i++) {
                            var item = collection[i];
                            if (stack.Count == 0) {
                                throw new EdiException($"Serialization stack empty while in the middle of proccessing a collection of {itemType.Name}");
                            }
                            while (stack.Peek().StructureType >= container) {
                                var previous = stack.Pop();
                            }
                            stack.Push(new EdiStructure(container, stack.Peek(), property, item, range_offset + i, null));
                            SerializeStructure(writer, stack, structuralComparer);
                        }
                    } else {
                        // or a simple Container.
                        if (stack.Count == 0) {
                            throw new EdiException($"Serialization stack empty while in the middle of proccessing a collection of {property.Info.PropertyType.Name}");
                        }
                        while (stack.Peek().StructureType >= container) {
                            var previous = stack.Pop();
                        }
                        if (value == null)
                            continue;
                        stack.Push(new EdiStructure(container, stack.Peek(), property, value));
                        SerializeStructure(writer, stack, structuralComparer);
                    }
                }
            }
        }

        #endregion
    }
}
