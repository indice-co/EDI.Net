using indice.Edi.Tests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiTextReaderTests
    {
        private static readonly Assembly _assembly = typeof(EdiTextReaderTests).GetTypeInfo().Assembly;
        private static Stream GetResourceStream(string fileName) {
            var qualifiedResources = _assembly.GetManifestResourceNames().OrderBy(x => x).ToArray();
            Stream stream = _assembly.GetManifestResourceStream("indice.Edi.Tests.Samples." + fileName);
            return stream;
        }

        private static MemoryStream StreamFromString(string value) {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        [Fact]
        public void EdiTextReaderTest() {
            var msgCount = 0;
            var grammar = EdiGrammar.NewTradacoms();

            using (var ediReader = new EdiTextReader(new StreamReader(GetResourceStream("tradacoms.order9.edi")), grammar)) {
                while (ediReader.Read()) {
                    if (ediReader.IsStartMessage) {
                        msgCount++;
                    }
                }
            }
            Assert.Equal(4, msgCount);
        }

        [Fact]
        public void EdiSerializerDeserializeTest() {
            var grammar = EdiGrammar.NewTradacoms();
            var interchange = default(Interchange);
            using (var stream = GetResourceStream("tradacoms.order9.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange>(new StreamReader(stream), grammar);
            }
            Assert.Equal(670, interchange.Invoices.Count);
        }
    }
}
