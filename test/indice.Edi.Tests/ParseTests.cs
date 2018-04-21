using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Utilities;
using Xunit;

namespace indice.Edi.Tests
{
    public class ParseTests
    {
        [Theory]
        [InlineData("29012",  "9(13)V9(2)", null, 290.12)]
        [InlineData("29012",  "9(13)V9(2)", '.',  290.12)]
        [InlineData("290.12", "9(13)V9(2)", '.',  290.12)]
        [InlineData("290.12", "X(13)",      '.',  290.12)]
        [Trait(Traits.Tag, "Parser")]
        public void DecimalFromStringTest(string input, string format, char? decimalPoint, decimal output) {
            Assert.Equal(new decimal?(output), EdiExtensions.Parse(input, (Picture)format, decimalPoint));
        }
    }
}
