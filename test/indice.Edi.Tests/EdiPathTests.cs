using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiPathTests
    {
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

        [InlineData("GS[0][0]")]
        [InlineData("GS/0/0")]
        [InlineData("GS/0")]
        [InlineData("GS")]
        [Theory]
        public void ParseHandlesTwoLetterSegmentNames(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("GS[0][0]", path.ToString());
        }

        [InlineData("B10[0][0]")]
        [InlineData("B10/0/0")]
        [InlineData("B10/0")]
        [InlineData("B10")]
        [Theory]
        public void ParseHandlesOneLetterTwoNumberSegmentNames(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("B10[0][0]", path.ToString());
        }
    }
}
