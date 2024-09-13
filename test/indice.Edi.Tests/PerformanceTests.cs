using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests;

public class PerformanceTests
{

    [Fact(Skip = "Performance"), Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#154"), Trait(Traits.Tag, "Performance")]
    public void Should_Parse_20_Megabyte_File() {
        var grammar = EdiGrammar.NewEdiFact();
        var interchange = default(EdiFact_Issue154_Transmission);
        using (var stream = Helpers.GetBigSampleStream("edifact.Issue154.Perf.edi")) {
            interchange = new EdiSerializer().Deserialize<EdiFact_Issue154_Transmission>(new StreamReader(stream), grammar);
        }

        Assert.Equal(78880, interchange.Messages.Count);
        Assert.NotNull(interchange);
    }
    [Fact(Skip = "Performance"), Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#154"), Trait(Traits.Tag, "Performance")]
    public void Should_Parse_20_Megabyte_File_02() {
        var grammar = EdiGrammar.NewEdiFact();
        var interchange = default(EdiFact_Issue154_Interchange);
        using (var stream = Helpers.GetBigSampleStream("edifact.Issue154.Perf02.edi")) {
            interchange = new EdiSerializer().Deserialize<EdiFact_Issue154_Interchange>(new StreamReader(stream), grammar);
        }

        //Assert.Equal(78880, interchange.Messages.Count);
        Assert.NotNull(interchange);
    }
}
