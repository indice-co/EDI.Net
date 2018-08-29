using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests
{
    public class SerializerTests
    {
        [Theory]
        [Trait(Traits.Tag, "Writer")]
        [InlineData(true)]
        [InlineData(false)]
        public void SerializeTest(bool withCompression) {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(Models.EdiFact01.Interchange);
            using (var stream = Helpers.GetResourceStream("edifact.01.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.EdiFact01.Interchange>(new StreamReader(stream), grammar);
            }


            var expected = withCompression ?

            #region ...compressed output...
@"UNA:+.? '
UNB+UNOC:3+1234567891123:14+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'
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
DTM+324:201010192300201010200000:013'
PRI+CAL:-2100'
RNG+4+1:-0.1'
PRI+CAL:21000'
RNG+4+1:-0.1'
LIN+2++1420:::SM'
DTM+324:201010200000201010200100:013'
PRI+CAL:-2100'
RNG+4+1:0'
PRI+CAL:21000'
RNG+4+1:0'
LIN+3++1420:::SM'
DTM+324:201010200100201010200200:013'
PRI+CAL:-2100'
RNG+4+1:0'
PRI+CAL:21000'
UNS+S'
UNT+158+1'
UNZ+1+20101000064507'
"
            #endregion
            :
            #region ...uncompressed output...
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
DTM+324:201010192300201010200000:013'
PRI+CAL:-2100:'
RNG+4+1:-0.1'
PRI+CAL:21000:'
RNG+4+1:-0.1'
LIN+2++1420:::SM'
DTM+324:201010200000201010200100:013'
PRI+CAL:-2100:'
RNG+4+1:0'
PRI+CAL:21000:'
RNG+4+1:0'
LIN+3++1420:::SM'
DTM+324:201010200100201010200200:013'
PRI+CAL:-2100:'
RNG+4+1:0'
PRI+CAL:21000:'
UNS+S'
UNT+158+1'
UNZ+1+20101000064507'
";
            #endregion

            if (!"\r\n".Equals(Environment.NewLine)) {
                expected = expected.Replace("\r\n", Environment.NewLine);
            }
            
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                new EdiSerializer() { EnableCompression = withCompression }.Serialize(writer, interchange);
            }
            Assert.Equal(expected, output.ToString());
        }

        [Fact, Trait(Traits.Tag, "Writer"), Trait(Traits.Bug, "#74")]
        public void SegmentWithOneElementDoesNotPrintPrecedingComponentSeparators() {
            var interchange = new Interchange_Issue74() {
                Msg = new Interchange_Issue74.Message {
                    TSR = new Interchange_Issue74.TSR_Segment {
                        ServiceCode = "270"
                    }
                }
            };

            var expected = "UNA:+.? '\r\nTSR+++270'";
            var output = new StringBuilder();
            var grammar = EdiGrammar.NewEdiFact();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                new EdiSerializer().Serialize(writer, interchange);
            }
            Assert.Equal(expected, output.ToString().TrimEnd());
        }

        [Fact, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#90")]
        public void InheritedSegmentGroupClass_SerializesContainerProperies_first() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = new InheritSegmentGroup {
                Messages = new List<InheritSegmentGroup.Message> {
                    new InheritSegmentGroup.Message {
                        Id = "Group1",
                        Element = new InheritSegmentGroup.InGroup {
                            Id = "Element1"
                        }
                    }
                }
            };

            string output = null;
            using (var writer = new StringWriter()) {
                new EdiSerializer().Serialize(writer, grammar, interchange);
                output = writer.ToString();
            }

            var GrpPos = output.IndexOf("Group1");
            var ElmPos = output.IndexOf("Element1");

            Assert.True(GrpPos >= 0);
            Assert.True(ElmPos >= 0);
            Assert.True(GrpPos < ElmPos);
        }
        
    }
}
