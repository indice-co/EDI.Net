using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using indice.Edi.Tests.Models;
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


        [Theory]
        [InlineData("20170706", "000000", "2017-07-06T00:00:00Z")]
        [InlineData("20170717", "000000", "2017-07-17T00:00:00Z")]
        [InlineData("20180529", "155812", "2018-05-29T15:58:12Z")]
        [Trait(Traits.Issue, "#96")]
        public void DateTimeFromStringTest(string dateText, string timeText, string output) {
            var expected = DateTime.Parse(output, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var grammar = EdiGrammar.NewX12();
            var edi = $@"ISA*00*          *00*          *01*000123456      *ZZ*TEST2      *090827*0936*U*00401*000000055*0*T*>~
	GS*PO*000123456*TEST2*20090827*1041*2*X*004010~
		ST*850*0001~
			DTM*011*{dateText}*{timeText}*08~
		SE*50*0001~
	GE*1*2~
IEA*1*000000001~
";

            var interchange = default(Interchange_Issue96);
            using (var stream = Helpers.StreamFromString(edi)) {
                interchange = new EdiSerializer().Deserialize<Interchange_Issue96>(new StreamReader(stream), grammar);
            }
            
            Assert.Equal(expected, interchange.Msg.DateTime);
        }
    }
}
