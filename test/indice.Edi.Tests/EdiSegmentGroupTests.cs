using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using indice.Edi.Tests.Models;

using Xunit;

namespace indice.Edi.Tests
{
    public class EdiSegmentGroupTests
    {
        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        [Trait(Traits.Issue, "#26")]
        public void ReferenceSegment() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(ORDRSP);

            using (var stream = Helpers.GetResourceStream("edifact.ORDRSP-(2).edi")) {
                interchange = new EdiSerializer().Deserialize<ORDRSP>(new StreamReader(stream), grammar);

                Assert.NotNull(interchange.ListNachricht.First().Referenz_der_Anfrage.ReferenzderAnfrage);
            }
        }
    }
}
