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
        [Fact]
        [Trait(Traits.Tag, "Parser")]
        public void IntegerToStringTest() {
            Assert.Equal("   12", EdiExtensions.ToEdiString(12, (Picture)"X(5)"));
            Assert.Equal("00012", EdiExtensions.ToEdiString(12, (Picture)"9(5)"));
            Assert.Equal("00012", EdiExtensions.ToEdiString(12, (Picture)"9(5)"));
            Assert.Equal("12", EdiExtensions.ToEdiString(12, (Picture)"X(1)"));
            Assert.Equal("12", EdiExtensions.ToEdiString(12, (Picture)"9(1)"));
            
            Assert.Equal("29012", EdiExtensions.ToEdiString(290.12M, (Picture)"9(3)V9(2)", '.'));
        }
    }
}
