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
                expected.Append("UNZ+1+20101000064507'");
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
                expected.Append("UNZ+1+20101000064507'");
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
            var expected = "UNA:+.? '\r\nPAC+1+:52+PK'";
            string output = null;
            using (var writer = new StringWriter()) {
                new EdiSerializer() { EnableCompression = false }.Serialize(writer, grammar, interchange);
                output = writer.ToString();
            }

            Assert.Equal(expected, output);
        }

        [Fact, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#138"), Trait(Traits.Issue, "#139")]
        public void Dont_write_segment_if_no_value_is_available_Test() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(EdiPaiement_Issue139);
            using (var stream = Helpers.GetResourceStream("edifact.Paiement.Issue139.EDI")) {
                interchange = new EdiSerializer().Deserialize<EdiPaiement_Issue139>(new StreamReader(stream), grammar);
            }
            Assert.NotNull(interchange);
            Assert.NotEmpty(interchange.GroupesFonctionnels);
            Assert.NotEmpty(interchange.GroupesFonctionnels[0].Messages);
            Assert.NotEmpty(interchange.GroupesFonctionnels[0].Messages[0].Intervenants);
            Assert.Equal(2, interchange.GroupesFonctionnels[0].Messages[0].Intervenants.Count);

            interchange.GroupesFonctionnels[0].Messages[0].Intervenants[1].Reference = null;

            var expected = new StringBuilder().AppendLine(@"UNA:+,? '")
            .AppendLine("UNB+UNOL:3+35044551600023:5:I+7501751:146+191007:1205+20191007120559+++++TDT-PED-IN-DP1501/CVA19+1'")
            .AppendLine("UNG+INFENT+NON_SECURISE_NON_SIGNE+MULTI_DISTRIBUTION+191007:1205+1+UN+D:00B:PD1501'")
            .AppendLine("UNH+00001+INFENT:D:00B:UN:PD1501'")
            .AppendLine("BGM+CVA:71:211+INFENT1905SRL BIC IS RN19100211451'")
            .AppendLine("DTM+242:20190930:102'")
            .AppendLine("RFF+AUM:EIC'")
            .AppendLine("RFF+AUN:TELE?'DECLAR::6.5.7.0'")
            .AppendLine("RFF+AUO:2017.01.0375'")
            .AppendLine("NAD+DT+350445516:100:107++SRL BIC IS RN 2018+0001 rue 1+VILLE 1++11111'")
            .AppendLine("RFF+ACD:CVAE1'")
            .AppendLine("NAD+FR+35044551600023:100:107++CEC_EDI_PAYE:CABINET D?'EXPERTISE COMPTABLE::::3+0016 ZI de 1?'Idustrie+BLOIS++41000'")
            .AppendLine("UNT+000100+00001'")
            .AppendLine("UNE+000001+1'")
            .Append("UNZ+1+20191007120559'").ToString();
            string output = null;
            using (var writer = new StringWriter()) {
                new EdiSerializer().Serialize(writer, grammar, interchange);
                output = writer.ToString();
            }

            Assert.Equal(expected, output);
        }

        [Fact, Trait(Traits.Tag, "EdiFact"), Trait(Traits.Issue, "#121")]
        public void Serialize_ElementList() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(EdiFact_Issue121_ElementList_Conditions);
            using (var stream = Helpers.GetResourceStream("edifact.Issue121.ElementList.Conditions.edi")) {
                interchange = new EdiSerializer().Deserialize<EdiFact_Issue121_ElementList_Conditions>(new StreamReader(stream), grammar);
            }
            var expected = new StringBuilder()
            .AppendLine("UNA:+.? '")
            .AppendLine("ATR+NEW+PRIO:N'")
            .AppendLine("ATR+NEW+TGIC:465+TGNO:716073+TGPC:SVX'")
            .AppendLine("ATR+NEW+ACCS:A+BGCL:Y+BRDP:KBP+OFFP:TSE+PRIL:N'")
            .AppendLine("ATR++V'")
            .Append("LTS+�2'").ToString();
            string output = null;
            using (var writer = new StringWriter()) {
                new EdiSerializer().Serialize(writer, grammar, interchange);
                output = writer.ToString();
            }

            Assert.Equal(expected, output);
        }

        //[Fact, Trait(Traits.Tag, "Tradacoms"), Trait(Traits.Issue, "#17")]
        //public void Should_Serialize_Autogenerated_Counts() {
        //    var grammar = EdiGrammar.NewTradacoms();
        //    var interchange = default(Interchange_Issue17);
        //    using (var stream = Helpers.GetResourceStream("tradacoms.Issue17.autogeneratedvalues.edi")) {
        //        interchange = new EdiSerializer().Deserialize<Interchange_Issue17>(new StreamReader(stream), grammar);
        //    }
        //    Assert.NotNull(interchange);
        //}
    }
}
