using System.Globalization;
using indice.Edi.Tests.Models;
using indice.Edi.Utilities;
using Xunit;

namespace indice.Edi.Tests;

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

    [Theory]
    [InlineData("20171020", "2017-10-20T00:00:00Z")]
    [Trait(Traits.Issue, "#95")]
    public void DateTimeFromString_Component_MaxLength_Exceeds_StringLength_Test(string dateText, string output) {
        var expected = DateTime.Parse(output, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        var grammar = EdiGrammar.NewEdiFact();
        var edi = $@"UNB+UNOC:3+109910000+999999999+171215:1048+00336++AO05Q118ke1'
UNH+00001+AESG:02:001:KV'
DTM+137:{dateText}:102'
UNT+6+00001'
UNZ+1+00336'
";
        var interchange = default(Interchange_Issue95);
        using (var stream = Helpers.StreamFromString(edi)) {
            interchange = new EdiSerializer().Deserialize<Interchange_Issue95>(new StreamReader(stream), grammar);
        }

        Assert.Equal(expected, interchange.Msg.DateTime);
    }

    [Theory]
    [InlineData("20170726", "2034", "2017-07-26T20:34:00Z", "011", "08")]
    [InlineData("20170726", "20341", "2017-07-26T00:00:00Z", "011", "08")] // this should fail to parse the time part thus showing 00:00:00
    [InlineData("20170726", "203406", "2017-07-26T20:34:06Z", "011", "08")]
    [InlineData("20170726", "1558123", "2017-07-26T15:58:12.3Z", "011", "08")]
    [InlineData("20170726", "15581234", "2017-07-26T15:58:12.34Z", "011", "08")]
    [Trait(Traits.Issue, "#130")]
    [Trait(Traits.Issue, "#172")]
    public void DateTimeFromStringTestMiliseconds(string dateText, string timeText, string expectedISODate, string intText, string longText) {
        var expected = DateTime.Parse(expectedISODate, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        var grammar = EdiGrammar.NewX12();
        var edi = $@"ISA*00*          *00*          *01*000123456      *ZZ*TEST2      *090827*0936*U*00401*000000055*0*T*>~
	GS*PO*000123456*TEST2*20090827*1041*2*X*004010~
		ST*850*0001~
			DTM*{intText}*{dateText}*{timeText}*{longText}~
		SE*50*0001~
	GE*1*2~
IEA*1*000000001~
";

        var interchange = default(ParseComponentValues_Interchange);
        using (var stream = Helpers.StreamFromString(edi)) {
            interchange = new EdiSerializer().Deserialize<ParseComponentValues_Interchange>(new StreamReader(stream), grammar);
        }

        Assert.Equal(expected, interchange.Msg.DateTime);
        Assert.Equal(int.Parse(intText), interchange.Msg.Integer);
        Assert.Equal(long.Parse(longText), interchange.Msg.Long);
    }


    [Fact]
    [Trait(Traits.Issue, "#280")]
    public void ParseWithSingleSegmentGroup() 
    {
        var grammar = EdiGrammar.NewEdiFact();
        var edi = $@"UNA:+.? '
UNB+:0+++020125:0304+++++0'
UNH+1'
BGM+'
DTM+137:202506150701:203'
DTM+ZZZ:1:805'
RFF+Z05:SYL'
RFF+LI:02382812480014'
RFF+Z09:P015673'
NAD+FR+22222:160:SVK+++++++SE'
NAD+DO+11111:160:SVK+++++++SE'
UNT+0000000010+1'";
        var message = new Edifact_Issue280();
        using (var stream = Helpers.StreamFromString(edi)) {
            message = new EdiSerializer().Deserialize<Edifact_Issue280>(new StreamReader(stream), grammar);
        }

        Assert.NotNull(message.Parties);
    }
}
