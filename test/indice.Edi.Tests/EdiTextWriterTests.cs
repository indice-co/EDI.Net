﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiTextWriterTests
    {
        [Fact, Trait(Traits.Tag, "Writer")]
        public void WriterWrites_ServiceStringAdvice_Test() {
            var expected = "UNA:+.? '";
            var output = new StringBuilder();
            var grammar = EdiGrammar.NewEdiFact();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                writer.WriteServiceStringAdvice();
            }
            Assert.Equal(expected, output.ToString().TrimEnd());
        }


        [Fact, Trait(Traits.Tag, "Writer")]
        public void WriterWritesStructureTest() {
            var grammar = EdiGrammar.NewEdiFact();
            var expected = new StringBuilder().AppendLine("UNA:+.? '")
                                              .AppendLine("UNB+UNOC:3+1234567891123:14+7080005059275:14:SPOTMARKED+101012:1104+HBQ001++++1'")
                                              .AppendLine("UNH+1+QUOTES:D:96A:UN:EDIEL2+S'");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                writer.WriteServiceStringAdvice();
                writer.WriteToken(EdiToken.SegmentName, "UNB");         Assert.Equal("UNB", writer.Path);
                writer.WriteToken(EdiToken.String, "UNOC");             Assert.Equal("UNB[0][0]", writer.Path);
                writer.WriteToken(EdiToken.ComponentStart);             Assert.Equal("UNB[0][1]", writer.Path);
                writer.WriteToken(EdiToken.Integer, 3);                 Assert.Equal("UNB[0][1]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[1]", writer.Path);
                writer.WriteToken(EdiToken.String, "1234567891123");    Assert.Equal("UNB[1][0]", writer.Path);
                writer.WriteToken(EdiToken.Integer, 14);                Assert.Equal("UNB[1][1]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[2]", writer.Path);
                writer.WriteValue(7080005059275);                       Assert.Equal("UNB[2][0]", writer.Path);
                writer.WriteValue(14);                                  Assert.Equal("UNB[2][1]", writer.Path);
                writer.WriteValue("SPOTMARKED");                        Assert.Equal("UNB[2][2]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[3]", writer.Path);
                writer.WriteValue(new DateTime(2012, 10, 10, 11, 04, 0), "ddMMyy"); Assert.Equal("UNB[3][0]", writer.Path);
                writer.WriteValue(new DateTime(2012, 10, 10, 11, 04, 0), "HHmm");   Assert.Equal("UNB[3][1]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[4]", writer.Path);
                writer.WriteValue("HBQ001");                            Assert.Equal("UNB[4][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[5]", writer.Path);
                writer.WriteValue((string)null);                        Assert.Equal("UNB[5][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[6]", writer.Path);
                writer.WriteValue((string)null);                        Assert.Equal("UNB[6][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[7]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNB[8]", writer.Path);
                writer.WriteValue(1);                                   Assert.Equal("UNB[8][0]", writer.Path);

                writer.WriteToken(EdiToken.SegmentName, "UNH");         Assert.Equal("UNH", writer.Path);
                writer.WriteValue(1);                                   Assert.Equal("UNH[0][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNH[1]", writer.Path);
                writer.WriteValue("QUOTES");                            Assert.Equal("UNH[1][0]", writer.Path);
                writer.WriteValue('D');                                 Assert.Equal("UNH[1][1]", writer.Path);
                writer.WriteValue("96A");                               Assert.Equal("UNH[1][2]", writer.Path);
                writer.WriteValue("UN");                                Assert.Equal("UNH[1][3]", writer.Path);
                writer.WriteValue("EDIEL2");                            Assert.Equal("UNH[1][4]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);               Assert.Equal("UNH[2]", writer.Path);
                writer.WriteValue("S");                                 Assert.Equal("UNH[2][0]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }


        [Fact, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#109")]
        public void WriterProgressessThePathCorrectly_On_NullToken() {
            var grammar = EdiGrammar.NewEdiFact();
            var expected = new StringBuilder().AppendLine("UNA:+.? '")
                                              .AppendLine("PAC+1+:52+PK'");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                writer.WriteServiceStringAdvice();
                writer.WriteToken(EdiToken.SegmentName, "PAC"); Assert.Equal("PAC",       writer.Path);
                writer.WriteValue(1);                           Assert.Equal("PAC[0][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);       Assert.Equal("PAC[1]",    writer.Path);
                writer.WriteToken(EdiToken.Null);               Assert.Equal("PAC[1][0]", writer.Path);
                writer.WriteValue(52);                          Assert.Equal("PAC[1][1]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart);       Assert.Equal("PAC[2]",    writer.Path);
                writer.WriteValue("PK");                        Assert.Equal("PAC[2][0]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }

        [Fact, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#141")]
        public void WriterWrites_Boolean_Correctly() {
            var grammar = EdiGrammar.NewEdiFact();
            var expected = new StringBuilder().Append($"AAA+1+:0'{Environment.NewLine}");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar)) {
                writer.WriteToken(EdiToken.SegmentName, "AAA"); Assert.Equal("AAA", writer.Path);
                writer.WriteValue(true); Assert.Equal("AAA[0][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart); Assert.Equal("AAA[1]", writer.Path);
                writer.WriteToken(EdiToken.Null); Assert.Equal("AAA[1][0]", writer.Path);
                writer.WriteValue(false); Assert.Equal("AAA[1][1]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }

        [Theory, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#160"), Trait(Traits.Issue, "#162")]
        [InlineData(true)]
        [InlineData(false)]
        public void WriterWrites_Precision_Correctly(bool escapeDecimalMarkInText) {
            var grammar = EdiGrammar.NewEdiFact();
            grammar.SetAdvice('+', '+', ':', '\'', '?', null, ',');
            var escapeChar = escapeDecimalMarkInText ? "?" : ""; 
            var expected = new StringBuilder().Append($"AAA+10,42+:234,46:234{escapeChar},45563'{Environment.NewLine}");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar) { EscapeDecimalMarkInText = escapeDecimalMarkInText }) {
                writer.WriteToken(EdiToken.SegmentName, "AAA"); Assert.Equal("AAA", writer.Path);
                writer.WriteValue(10.42345, (Picture)"9(1)V9(2)"); Assert.Equal("AAA[0][0]", writer.Path);
                writer.WriteToken(EdiToken.ElementStart); Assert.Equal("AAA[1]", writer.Path);
                writer.WriteToken(EdiToken.Null); Assert.Equal("AAA[1][0]", writer.Path);
                writer.WriteValue(234.45563, (Picture)"9(1)V9(2)"); Assert.Equal("AAA[1][1]", writer.Path);
                writer.WriteValue("234,45563"); Assert.Equal("AAA[1][2]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }
        
        [Theory, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#198")]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void WriterLimits_Maximum_When_Alphanumeric(bool escapeDecimalMarkInText, bool strictAlphanumericCharLimit) {
            var grammar = EdiGrammar.NewEdiFact();
            grammar.SetAdvice('+', '+', ':', '\'', '?', null, ',');
            var escapeChar = escapeDecimalMarkInText ? "?" : "";
            var extraChars = strictAlphanumericCharLimit ? "" : "11111";
            var expected = new StringBuilder().Append($"AAA+STRING{escapeChar},LIMITED{extraChars}'{Environment.NewLine}");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar) { EscapeDecimalMarkInText = escapeDecimalMarkInText,
                                                                                      StrictAlphanumericCharLimit = strictAlphanumericCharLimit}) {
                writer.WriteToken(EdiToken.SegmentName, "AAA"); Assert.Equal("AAA", writer.Path);
                writer.WriteValue("STRING,LIMITED11111", (Picture)"X(14)"); Assert.Equal("AAA[0][0]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }

        
        [Theory, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#198")]
        [InlineData(TestEnumWriter.AAA, 1)]
        [InlineData(TestEnumWriter.AAA, 2)]
        [InlineData(TestEnumWriter.AAA, 3)]
        public void WriterEnumsAsString_And_Limits_Its_value_When_Alphanumeric_Picture_StrictAlphanumericCharLimited(TestEnumWriter enumratorWrite, int pictureScale) {
            var grammar = EdiGrammar.NewEdiFact();
            grammar.SetAdvice('+', '+', ':', '\'', '?', null, ',');
            var expected = new StringBuilder().Append($"AAA+{Enum.GetName(enumratorWrite).Substring(0, pictureScale)}'{Environment.NewLine}");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar) {
                StrictAlphanumericCharLimit = true
            }) {
                writer.WriteToken(EdiToken.SegmentName, "AAA"); Assert.Equal("AAA", writer.Path);
                writer.WriteValue(enumratorWrite, (Picture)$"X({pictureScale})", null); Assert.Equal("AAA[0][0]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }

        [Theory, Trait(Traits.Tag, "Writer"), Trait(Traits.Issue, "#198")]
        [InlineData(TestEnumWriter.AAA)]
        public void WriterEnumsAsString_When_Alphanumeric_Picture(TestEnumWriter enumratorWrite) {
            var grammar = EdiGrammar.NewEdiFact();
            grammar.SetAdvice('+', '+', ':', '\'', '?', null, ',');
            var expected = new StringBuilder().Append($"AAA+{Enum.GetName(enumratorWrite)}'{Environment.NewLine}");
            var output = new StringBuilder();
            using (var writer = new EdiTextWriter(new StringWriter(output), grammar) {
                StrictAlphanumericCharLimit = false
            }) {
                writer.WriteToken(EdiToken.SegmentName, "AAA"); Assert.Equal("AAA", writer.Path);
                writer.WriteValue(enumratorWrite, (Picture)$"X(5)", null); Assert.Equal("AAA[0][0]", writer.Path);
            }
            Assert.Equal(expected.ToString(), output.ToString());
        }
    }
}
