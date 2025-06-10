using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests;

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

        var expected = $"UNA:+.? '{Environment.NewLine}TSR+++270'";
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
        var expected = $"UNA:+.? '{Environment.NewLine}PAC+1+:52+PK'{Environment.NewLine}";
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
        .AppendLine("UNZ+1+20191007120559'").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#121")]
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
        .AppendLine("LTS+�2'").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#170")]
    public void Serialize_ElementList_WithRanged_Path() {
        var grammar = EdiGrammar.NewEdiFact();
        var interchange = default(EdiFact_Issue170_ElementList);
        using (var stream = Helpers.GetResourceStream("edifact.Issue170.ElementList.edi")) {
            interchange = new EdiSerializer().Deserialize<EdiFact_Issue170_ElementList>(new StreamReader(stream), grammar);
        }

        var expected = new StringBuilder()
        .AppendLine("UNA:+.? '")
        .AppendLine("IFT+1:1+This+is+the first'")
        .AppendLine("IFT+1:2+And+this is+the seccond'")
        .AppendLine("IFT+1:3+while+this+should come third'").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#170")]
    public void Serialize_Elements_With_Properties_And_WildcardPaths() {
        var grammar = EdiGrammar.NewEdiFact();
        var interchange = new EdiFact_Issue170 {
            Msg = new EdiFact_Issue170.Message {
                Goods = new List<EdiFact_Issue170.GID> {
                 new EdiFact_Issue170.GID {
                     GoodsItemNumber = 1 ,
                     FirstPackagingLevel = new EdiFact_Issue170.GoodsItemPackageDetails { PackageQuantity = 1, PackageTypeDescriptionCode = "201" },
                     SecondPackagingLevel = new EdiFact_Issue170.GoodsItemPackageDetails { PackageQuantity = 4, PackageTypeDescriptionCode = "201" }
                }
             }
            }
        };

        var expected = new StringBuilder()
        .AppendLine("UNA:+.? '")
        .AppendLine("GID+1+1:201+4:201'").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "Tradacoms"), Trait(Traits.Issue, "#17")]
    public void Should_Serialize_Autogenerated_Counts() {
        var grammar = EdiGrammar.NewTradacoms();
        var interchange = default(Interchange_Issue17);
        using (var stream = Helpers.GetResourceStream("tradacoms.Issue17.autogeneratedvalues.edi")) {
            interchange = new EdiSerializer().Deserialize<Interchange_Issue17>(new StreamReader(stream), grammar);
        }
        Assert.NotNull(interchange);
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#149")]
    public void SegmentGroups_WithSingleSegment_ShouldEscape() {
        var grammar = EdiGrammar.NewEdiFact();
        var interchange = default(EdiFact_Issue149_SegmentGroups);
        using (var stream = Helpers.GetResourceStream("edifact.Issue149.SegmentGroups.edi")) {
            interchange = new EdiSerializer().Deserialize<EdiFact_Issue149_SegmentGroups>(new StreamReader(stream), grammar);
        }
        Assert.NotNull(interchange);
        Assert.NotNull(interchange.Message);
        Assert.NotNull(interchange.Message.SG15);
        Assert.Equal("ZZZ", interchange.Message.SG15.AJT1);
        Assert.Equal("35", interchange.Message.SG15.AJT2);
        Assert.NotNull(interchange.Message.SG25);
        Assert.Single(interchange.Message.SG25.SG26);
        Assert.Equal("1", interchange.Message.SG25.SG26[0].PAI1);
        Assert.Equal("1", interchange.Message.SG25.SG26[0].PAI2);
        Assert.Equal("1", interchange.Message.SG25.SG26[0].PAI3);
        Assert.NotNull(interchange.Message.SG25.SG26[0].MOA);
        Assert.Equal("ZZZ", interchange.Message.SG25.SG26[0].MOA.MOA1);
        Assert.Equal(9, interchange.Message.SG25.SG26[0].MOA.MOA2);
        Assert.Equal("XXX", interchange.Message.SG25.SG26[0].MOA.MOA3);
        Assert.Single(interchange.Message.SG25.SG26[0].References);
        Assert.Equal("AAA", interchange.Message.SG25.SG26[0].References[0].RFF1);
        Assert.Equal("X", interchange.Message.SG25.SG26[0].References[0].RFF2);
        Assert.Equal("4", interchange.Message.SG25.TAX1);
        Assert.Equal("D", interchange.Message.UNS1);
    }

    [Fact, Trait(Traits.Tag, "X12"), Trait(Traits.Issue, "#168"), Trait(Traits.Issue, "#17")]
    public void Serialize_MSG_Issue_168() {
        var eightFifty = new PurchaseOrder_850();

        var order = new PurchaseOrder_850.Order();
        eightFifty.Groups = new List<PurchaseOrder_850.FunctionalGroup>();
        var group = new PurchaseOrder_850.FunctionalGroup();
        group.Orders = new List<PurchaseOrder_850.Order>();
        var order1 = new PurchaseOrder_850.Order();
        order1.Items = new List<PurchaseOrder_850.OrderDetail>();
        group.Orders.Add(order1);

        eightFifty.Groups.Add(group);
        eightFifty.Groups[0].Orders.Add(order);

        int orderIndex = eightFifty.Groups[0].Orders.Count - 1;

        eightFifty.Groups[0].FunctionalIdentifierCode = "PO";
        eightFifty.Groups[0].ApplicationSenderCode = "test";
        eightFifty.Groups[0].ApplicationReceiverCode = "test";
        eightFifty.Groups[0].Date = new DateTime(2020, 10, 08, 22, 56, 00);
        eightFifty.Groups[0].GroupControlNumber = 1000 + 1000;
        eightFifty.Groups[0].GroupTrailerControlNumber = eightFifty.Groups[0].GroupControlNumber;
        eightFifty.Groups[0].AgencyCode = "X";
        eightFifty.Groups[0].Version = "test";

        var detail = new PurchaseOrder_850.OrderDetail();
        detail.OrderLineNumber = "001";
        detail.QuantityOrdered = 1;
        detail.UnitPrice = 10;

        var msg1 = new PurchaseOrder_850.MSG();
        msg1.MessageText = "aaa1";
        var msg2 = new PurchaseOrder_850.MSG();
        msg2.MessageText = "bbb2";
        var msg3 = new PurchaseOrder_850.MSG();
        msg3.MessageText = "ccc3";

        detail.MSG = new List<PurchaseOrder_850.MSG>();
        detail.MSG.Add(msg1);
        detail.MSG.Add(msg2);
        detail.MSG.Add(msg3);

        eightFifty.Groups[0].Orders[0].Items.Add(detail);

        var grammar = EdiGrammar.NewX12();

        var expected = new StringBuilder()
        .AppendLine("ISA*00********010101*0000**00000*000000000~")
        .AppendLine("GS*PO*test*test*20201008*2256*000002000*X*test~")
        .AppendLine("ST*~")
        .AppendLine("BEG*~")
        .AppendLine("CUR*~")
        .AppendLine("REF*~")
        .AppendLine("FOB*~")
        .AppendLine("ITD*~")
        .AppendLine("TD5*~")
        .AppendLine("MSG*~")
        .AppendLine("PO1*001*1**10~")
        .AppendLine("PID*~")
        .AppendLine("MEA***0~")
        .AppendLine("TC2*~")
        .AppendLine("MSG*aaa1~")
        .AppendLine("MSG*bbb2~")
        .AppendLine("MSG*ccc3~")
        .AppendLine("AMT*~")
        .AppendLine("SE*17~")
        .AppendLine("ST*~")
        .AppendLine("BEG*~")
        .AppendLine("CUR*~")
        .AppendLine("REF*~")
        .AppendLine("FOB*~")
        .AppendLine("ITD*~")
        .AppendLine("TD5*~")
        .AppendLine("MSG*~")
        .AppendLine("AMT*~")
        .AppendLine("SE*10~")
        .AppendLine("GE*2*000002000~")
        .AppendLine("IEA*1*000000000~").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, eightFifty);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#190")]
    public void Serialize_RangeError_Issue190() {
        var grammar = EdiGrammar.NewEdiFact();

        var ok = new EdiFact_Issue190.TIF {
            Names = new List<EdiFact_Issue190.GivenName> { "Hello", "World" }
        };
        var okResult = Serialize(ok);
        var okExpected = new StringBuilder().AppendLine("UNA:+.? '").AppendLine("TIF++Hello+World'").ToString();
        Assert.Equal(okExpected, okResult);

        var bad = new EdiFact_Issue190.BUGGEDTIF {
            Category = "ADT",
            Names = new List<EdiFact_Issue190.GivenName> { "Hello", "World" }
        };
        var badResult = Serialize(bad);
        var badExpected = new StringBuilder().AppendLine("UNA:+.? '").AppendLine("TIF+:ADT+Hello+World'").ToString();
        Assert.Equal(badExpected, badResult);

        string Serialize<T>(T data) {
            using var writer = new StringWriter();
            new EdiSerializer().Serialize(writer, grammar, data);
            return writer.ToString();
        }
    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#235")]
    public void Serialize_Should_not_create_double_UNB_Segment_Issue235() {
        var source = default(EDIFACT_APPERAK_issue235);
        using (var stream = Helpers.GetResourceStream("Aperak_issue235.xml")) {
            var serializer = new XmlSerializer(typeof(EDIFACT_APPERAK_issue235));
            source = (EDIFACT_APPERAK_issue235)serializer.Deserialize(stream);
        }
        Assert.NotNull(source);


        string Serialize<T>(T data) {
            var grammar = EdiGrammar.NewEdiFact();
            using var writer = new StringWriter();
            new EdiSerializer().Serialize(writer, grammar, data);
            return writer.ToString();
        }

        var output = Serialize(source);
        Regex unbRegex = new Regex("UNB");
        Assert.Single(unbRegex.Matches(output));
    }

    [Fact, Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#263")]
    public void Serialize_Should_Write_List_Of_Repeating_Elements_Issue235() {
        var source = new Interchange_Issue263.GIN {
            Qualifier = "AW",
            IdentityNumbers = [
                new () {  Text = "9998219" },
                new () {  Text = "9998218" },
                new () {  Text = "9998217" },
                new () {  Text = "9998216" },
                new () {  Text = "9998215" },
            ]
        };

        string Serialize<T>(T data) {
            var grammar = EdiGrammar.NewEdiFact();

            using var writer = new StringWriter();
            new EdiSerializer().Serialize(writer, grammar, data);
            return writer.ToString();
        }
        var expected = $"UNA:+.? '{Environment.NewLine}GIN+AW+9998219+9998218+9998217+9998216+9998215'{Environment.NewLine}";
        var output = Serialize(source);
        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "EDIFact")]
    public void Serialize_With_Explicit_Indication_of_Nesting() {
        var grammar = EdiGrammar.NewEdiFact();
        grammar.ExplicitIndicationOfNesting = true;
        var interchange = new EDIFACT_ExplicitNesting_issue227() {
            Msg = new() {
                Title = "Title",
                L1s = [
                    new() {
                        Name = "Name 1",
                        L2s = [
                            new() {
                                SubName = "Sub 1 1"
                            },
                            new() {
                                SubName = "Sub 1 2"
                            }
                        ]
                    },
                    new() {
                        Name = "Name 2",
                        L2s = [
                            new() {
                                SubName = "Sub 2 1"
                            }
                        ]
                    }
                ]
            }
        };
        var expected = new StringBuilder()
            .AppendLine("UNA:+.? '")
            .AppendLine("TIT+Title'")
            .AppendLine("LE1:1+Name 1'")
            .AppendLine("LE2:1:1+Sub 1 1'")
            .AppendLine("LE2:1:2+Sub 1 2'")
            .AppendLine("LE1:2+Name 2'")
            .AppendLine("LE2:2:1+Sub 2 1'").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }

    [Fact, Trait(Traits.Tag, "EDIFact")]
    public void Serialize_Without_Explicit_Indication_of_Nesting() {
        var grammar = EdiGrammar.NewEdiFact();
        grammar.ExplicitIndicationOfNesting = false;
        var interchange = new EDIFACT_ExplicitNesting_issue227() {
            Msg = new() {
                Title = "Title",
                L1s = [
                    new() {
                        Name = "Name 1",
                        L2s = [
                            new() {
                                SubName = "Sub 1 1"
                            },
                            new() {
                                SubName = "Sub 1 2"
                            }
                        ]
                    },
                    new() {
                        Name = "Name 2",
                        L2s = [
                            new() {
                                SubName = "Sub 2 1"
                            }
                        ]
                    }
                ]
            }
        };
        var expected = new StringBuilder()
            .AppendLine("UNA:+.? '")
            .AppendLine("TIT+Title'")
            .AppendLine("LE1+Name 1'")
            .AppendLine("LE2+Sub 1 1'")
            .AppendLine("LE2+Sub 1 2'")
            .AppendLine("LE1+Name 2'")
            .AppendLine("LE2+Sub 2 1'").ToString();
        string output = null;
        using (var writer = new StringWriter()) {
            new EdiSerializer().Serialize(writer, grammar, interchange);
            output = writer.ToString();
        }

        Assert.Equal(expected, output);
    }
}
