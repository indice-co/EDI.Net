using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiTextWriterTests
    {
        [Fact]
        public void WriterWritesStructureTest() {
            var grammar = EdiGrammar.NewEdiFact();
            var expected =
@"UNA:+.? '
UNB+UNOC:3+1234567891123:14+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'
UNH+1+QUOTES:D:96A:UN:EDIEL2+S'
BGM+310+2010101900026812+9+AB'
DTM+137:201010191104:203'
DTM+163:201010192300:203'
DTM+164:201010202300:203'
DTM+ZZZ:1:805'
CUX+2:SEK'
NAD+FR+1234567891123::9'
LOC+105+SE1::SM'
NAD+DO+7080005059275::9'
LIN+1++1420:::SM'
DTM+324:201010192300201010192400:Z13'
PRI+CAL:-2100'
RNG+4+Z01:-0.1'
PRI+CAL:21000'
RNG+4+Z01:-0.1'
LIN+2++1420:::SM'
DTM+324:201010200000201010200100:Z13'
PRI+CAL:-2100'
RNG+4+Z01:0'
PRI+CAL:21000'
RNG+4+Z01:0'
LIN+3++1420:::SM'
DTM+324:201010200100201010200200:Z13'
PRI+CAL:-2100'
RNG+4+Z01:0'
PRI+CAL:21000'
UNS+S'
CNT+1:0'
CNT+ZZZ:453600'
UNT+158+1'
UNZ+1+20101000064507'";
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                writer.WriteToken(EdiToken.SegmentName, "UNA");
                writer.WriteToken(EdiToken.SegmentName, "UNB");
                writer.WriteToken(EdiToken.SegmentName, "UNH");
                writer.WriteToken(EdiToken.SegmentName, "BGM");
                writer.WriteToken(EdiToken.SegmentName, "DTM");
                writer.WriteToken(EdiToken.SegmentName, "DTM");
                writer.WriteToken(EdiToken.SegmentName, "UNT");
                writer.WriteToken(EdiToken.SegmentName, "UNZ");
            }
            Assert.Equal(expected, output.ToString());
        }
    }
}
