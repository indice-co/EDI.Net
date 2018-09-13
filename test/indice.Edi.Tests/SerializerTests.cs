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


            var expected = new StringBuilder();
            if (withCompression) {
                #region ...compressed output... 
                expected.AppendLine("UNA:+.? '");
                expected.AppendLine("UNB+UNOC:3+1234567891123:14+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'");
                expected.AppendLine("UNH+1+QUOTES:D:96A:UN:EDIEL2+S'");
                expected.AppendLine("BGM+310+2010101900026812+9+AB'");
                expected.AppendLine("DTM+137:201010191104:203'");
                expected.AppendLine("DTM+163:201010192300:203'");
                expected.AppendLine("DTM+164:201010202300:203'");
                expected.AppendLine("DTM+:1:805'");
                expected.AppendLine("CUX+2:SEK'");
                expected.AppendLine("NAD+FR+1234567891123::9'");
                expected.AppendLine("NAD+DO+7080005059275::9'");
                expected.AppendLine("LOC+105+SE1::SM'");
                expected.AppendLine("LIN+1++1420:::SM'");
                expected.AppendLine("DTM+324:201010192300201010200000:013'");
                expected.AppendLine("PRI+CAL:-2100'");
                expected.AppendLine("RNG+4+1:-0.1'");
                expected.AppendLine("PRI+CAL:21000'");
                expected.AppendLine("RNG+4+1:-0.1'");
                expected.AppendLine("LIN+2++1420:::SM'");
                expected.AppendLine("DTM+324:201010200000201010200100:013'");
                expected.AppendLine("PRI+CAL:-2100'");
                expected.AppendLine("RNG+4+1:0'");
                expected.AppendLine("PRI+CAL:21000'");
                expected.AppendLine("RNG+4+1:0'");
                expected.AppendLine("LIN+3++1420:::SM'");
                expected.AppendLine("DTM+324:201010200100201010200200:013'");
                expected.AppendLine("PRI+CAL:-2100'");
                expected.AppendLine("RNG+4+1:0'");
                expected.AppendLine("PRI+CAL:21000'");
                expected.AppendLine("UNS+S'");
                expected.AppendLine("UNT+158+1'");
                expected.AppendLine("UNZ+1+20101000064507'");
            }
            #endregion
            else {
                #region ...uncompressed output...
                expected.AppendLine("UNA:+.? '");
                expected.AppendLine("UNB+UNOC:3+1234567891123:14:+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'");
                expected.AppendLine("UNH+1+QUOTES:D:96A:UN:EDIEL2+S'");
                expected.AppendLine("BGM+310+2010101900026812+9+AB'");
                expected.AppendLine("DTM+137:201010191104:203'");
                expected.AppendLine("DTM+163:201010192300:203'");
                expected.AppendLine("DTM+164:201010202300:203'");
                expected.AppendLine("DTM+:1:805'");
                expected.AppendLine("CUX+2:SEK'");
                expected.AppendLine("NAD+FR+1234567891123::9'");
                expected.AppendLine("NAD+DO+7080005059275::9'");
                expected.AppendLine("LOC+105+SE1::SM'");
                expected.AppendLine("LIN+1++1420:::SM'");
                expected.AppendLine("DTM+324:201010192300201010200000:013'");
                expected.AppendLine("PRI+CAL:-2100:'");
                expected.AppendLine("RNG+4+1:-0.1'");
                expected.AppendLine("PRI+CAL:21000:'");
                expected.AppendLine("RNG+4+1:-0.1'");
                expected.AppendLine("LIN+2++1420:::SM'");
                expected.AppendLine("DTM+324:201010200000201010200100:013'");
                expected.AppendLine("PRI+CAL:-2100:'");
                expected.AppendLine("RNG+4+1:0'");
                expected.AppendLine("PRI+CAL:21000:'");
                expected.AppendLine("RNG+4+1:0'");
                expected.AppendLine("LIN+3++1420:::SM'");
                expected.AppendLine("DTM+324:201010200100201010200200:013'");
                expected.AppendLine("PRI+CAL:-2100:'");
                expected.AppendLine("RNG+4+1:0'");
                expected.AppendLine("PRI+CAL:21000:'");
                expected.AppendLine("UNS+S'");
                expected.AppendLine("UNT+158+1'");
                expected.AppendLine("UNZ+1+20101000064507'");
                #endregion
            }

            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                new EdiSerializer() { EnableCompression = withCompression }.Serialize(writer, interchange);
            }
            Assert.Equal(expected.ToString(), output.ToString());
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
        
        [Fact, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#109")]
        public void ValueAttributePath_Weird_behavior_Test() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = new ValueAttributePath_Weird_behavior {
                Msg = new ValueAttributePath_Weird_behavior.Message {
                    PAC = new ValueAttributePath_Weird_behavior.PAC_Segment {
                        PackageCount = "1",
                        PackageDetailLevel = "52",
                        PackageType = "PK"
                    }
                }
            };
            string expected = "UNA:+.? '\r\nPAC+1+:52+PK";
            string output = null;
            using (var writer = new StringWriter()) {
                new EdiSerializer() { EnableCompression = false }.Serialize(writer, grammar, interchange);
                output = writer.ToString();
            }

            Assert.Equal(expected, output);
        }
    }
}
