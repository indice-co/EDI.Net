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
        public void ParseMustHandleUriAndIndexFormats(string text)
        {
            var path = EdiPath.Parse(text);
            Assert.True(path.Equals("DTM[0][0]"));  
        }
    }
}
