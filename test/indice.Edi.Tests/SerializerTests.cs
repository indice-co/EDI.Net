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
            #region ...edi text output...
@"UNA:+.? '
UNB+UNOC:3+1234567891123:14:+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'
UNH+1+QUOTES:D:96A:UN:EDIEL2+S'
BGM+310+2010101900026812+9+AB'
DTM+137:201010191104:203'
DTM+163:201010192300:203'
DTM+164:201010202300:203'
DTM+:1:805'
CUX+2:SEK'
NAD+FR+1234567891123::9'
NAD+DO+7080005059275::9'
LOC+105+SE1::SM'
LIN+1++1420:::SM'
DTM+324:201010192300201010200000:Z13'
PRI+CAL:-2100:'
RNG+4+1:-0.1'
PRI+CAL:21000:'
RNG+4+1:-0.1'
LIN+2++1420:::SM'
DTM+324:201010200000201010200100:Z13'
PRI+CAL:-2100:'
RNG+4+1:0'
PRI+CAL:21000:'
RNG+4+1:0'
LIN+3++1420:::SM'
DTM+324:201010200100201010200200:Z13'
PRI+CAL:-2100:'
RNG+4+1:0'
PRI+CAL:21000:'
UNS+S'
UNT+158+1'
UNZ+1+20101000064507'
";
            #endregion

            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                new EdiSerializer().Serialize(writer, interchange);
            }
            Assert.Equal(expected, output.ToString());
        }
    }
}
