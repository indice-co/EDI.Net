using System.IO;
using indice.Edi.Tests.Issue256;
using indice.Edi.Tests.Issue256.Interchanges;
using Xunit;

namespace indice.Edi.Tests
{
    public class Edifact_INVOICTests
    {
        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        [Trait(Traits.Issue, "#256")]
        public void ReferenceSegment()
        {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(Interchange<INVOIC>);

            using (var stream = Helpers.GetResourceStream("edifact.Issue256.INVOIC.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange<INVOIC>>(new StreamReader(stream), grammar);
            }

            Assert.NotNull(interchange);
        }
    }
}
