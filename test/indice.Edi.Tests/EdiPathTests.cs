using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiPathTests
    {
        [Trait(Traits.Tag, "Parser")]
        [InlineData("DTM[0][0]")]
        [InlineData("DTM/0/0")]
        [InlineData("DTM/0")]
        [InlineData("DTM")]
        [Theory]
        public void ParseHandlesUriAndIndexFormats(string text)
        {
            var path = EdiPath.Parse(text);
            Assert.True(path.Equals("DTM[0][0]"));  
        }

        [Trait(Traits.Tag, "Parser")]
        [InlineData("GS[0][0]")]
        [InlineData("GS/0/0")]
        [InlineData("GS/0")]
        [InlineData("GS")]
        [Theory]
        public void ParseHandlesTwoLetterSegmentNames(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("GS[0][0]", path.ToString());
        }

        [Trait(Traits.Tag, "Parser")]
        [InlineData("B10[0][0]")]
        [InlineData("B10/0/0")]
        [InlineData("B10/0")]
        [InlineData("B10")]
        [Theory]
        public void ParseHandlesOneLetterTwoNumberSegmentNames(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("B10[0][0]", path.ToString());
        }

        [Trait(Traits.Tag, "Parser")]
        [InlineData("B10[*][0]")]
        [InlineData("B10/*/0")]
        [InlineData("B10/*")]
        [Theory]
        public void ParseHandlesWildcards(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("B10[*][0]", path.ToString());
        }

        [Trait(Traits.Tag, "Writer")]
        [Fact]
        public void OrderByStructureTest() {
            var grammar = EdiGrammar.NewEdiFact();

            var input =    new[] { "BGM", "DTM/0/1", "DTM", "DTM/1", "CUX", "UNA", "UNT", "UNB", "UNZ" }.Select(i => (EdiPath)i).ToArray();             
            var expected = new[] { "UNA", "UNB", "BGM", "DTM", "DTM/0/1", "DTM/1/0", "CUX", "UNT", "UNZ" }.Select(i => (EdiPath)i).ToArray();
            var output = input.OrderBy(p => p, new EdiPathComparer(grammar));
            Assert.True(Enumerable.SequenceEqual(expected, output));
        }
    }
}
