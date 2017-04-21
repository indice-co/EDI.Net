using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using indice.Edi.Tests.Models;

using Xunit;

namespace indice.Edi.Tests
{
    public class Edifact_ORDRSPTests
    {
        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        [Trait(Traits.Issue, "#34")]
        public void ReferenceSegment() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(ORDRSP);

            using (var stream = Helpers.GetResourceStream("edifact.ORDRSP.edi")) {
                interchange = new EdiSerializer().Deserialize<ORDRSP>(new StreamReader(stream), grammar);
            }
            var nachricht = interchange.ListNachricht[0];
            Assert.Equal("ON", nachricht.Referenz_der_Anfrage.Code);
            Assert.NotNull(nachricht.Referenz_der_Anfrage.Referenz_der_Anfragedatum);
            Assert.NotNull(nachricht.Absender.CTA);
            Assert.Equal("Spiderman, Clack", nachricht.Absender.CTA.Kontakt);
            Assert.Equal("IC", nachricht.Absender.CTA.Funktion);
            Assert.Null(nachricht.Empfaenger.CTA);
            Assert.Equal("293", nachricht.Empfaenger.Code);
            Assert.Equal("990000000000X", nachricht.Empfaenger.ID);
            Assert.Equal("MR", nachricht.Empfaenger.Qualifier);
        }
    }
}
