using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class SerializerTests
    {
        [Fact]
        public void SerializeTestTest() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(Models.EdiFact01.Interchange);
            using (var stream = Helpers.GetResourceStream("edifact.01.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.EdiFact01.Interchange>(new StreamReader(stream), grammar);
            }


            var expected =
@"UNA:+.? '
UNB+UNOC:3+1234567891123:14+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'
UNH+1+QUOTES:D:96A:UN:EDIEL2+S'
";
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                new EdiSerializer().Serialize(writer, interchange);
            }
            Assert.Equal(expected, output.ToString());
        }
    }
}
