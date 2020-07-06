using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class ToEdiStringTests
    {
        [Trait(Traits.Tag, "Parser")]
        [Theory]
        [InlineData("   12", 12, "X(5)")]
        [InlineData("00012", 12, "9(5)")]
        [InlineData("12", 12, "X(1)")]
        [InlineData("12", 12, "9(1)")]
        public void IntegerToStringTest(string expectedValue, int value, string picture) {
            Assert.Equal(expectedValue, EdiExtensions.ToEdiString(value, (Picture)picture));
        }

        [Trait(Traits.Tag, "Parser")]
        [Theory]
        [InlineData("29012", 290.12, "9(3)V9(2)", null)]
        [InlineData("290.12", 290.12, "9(3)V9(2)", '.')]
        [InlineData("0290.12", 290.12, "9(4)V9(2)", '.')]
        [InlineData("0001,03", 1.028, "9(4)V9(2)", ',')]
        [InlineData("0,03", 0.028, "9(1)V9(2)", ',')]
        [InlineData("21,03", 21.03, "9(1)V9(2)", ',')]
        [InlineData(",03", 0.028, "9(0)V9(2)", ',')]
        public void FloatToStringTest(string expectedValue, decimal value, string picture, char? decimalMark) {
            Assert.Equal(expectedValue, EdiExtensions.ToEdiString(value, (Picture)picture, decimalMark));
        }
    }
}
