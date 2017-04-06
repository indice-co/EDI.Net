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
        public void ReferenceSegment ()
        {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(ORDRSP);

            using (var stream = Helpers.GetResourceStream("ordrsp.edi"))
            {
                interchange = new EdiSerializer().Deserialize<ORDRSP>(new StreamReader(stream), grammar);

                Assert.NotNull(interchange.ListNachricht.First().Referenz_der_Anfrage.ReferenzderAnfrage);
            }
        }
    }
}
