using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests
{
    public class PerformanceTests
    {

        [Fact(Skip = "Performance"), Trait(Traits.Tag, "EDIFact"), Trait(Traits.Issue, "#154"), Trait(Traits.Tag, "Performance")]
        public void Should_Parse_20_Megabyte_File() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(EdiFact_Issue154_Transmission);
            using (var stream = Helpers.GetBigSampleStream("edifact.Issue154.Perf.edi")) {
                interchange = new EdiSerializer().Deserialize<EdiFact_Issue154_Transmission>(new StreamReader(stream), grammar);
            }
            Assert.NotNull(interchange);
        }
    }
}
