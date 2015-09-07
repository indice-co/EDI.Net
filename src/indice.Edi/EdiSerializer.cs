using System;
using System.IO;

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
        /// <param name="reader">The <see cref="JsonReader"/> containing the object.</param>
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
            
            return value;
        }
    }
}
