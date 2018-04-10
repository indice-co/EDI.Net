using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Utilities;
using Xunit;

namespace indice.Edi.Tests
{
    public class ParseTests
    {
        [Fact]
        [Trait(Traits.Tag, "Parser")]
        public void IntegerToStringTest() {
            Assert.Equal(new decimal?(290.12M), EdiExtensions.Parse("29012", (Picture)"9(13)V9(2)", null));
            Assert.Equal(new decimal?(290.12M), EdiExtensions.Parse("290.12", (Picture)"9(13)V9(2)", '.'));
            Assert.Equal(new decimal?(290.12M), EdiExtensions.Parse("290.12", (Picture)"X(13)", '.'));
        }
    }
}
