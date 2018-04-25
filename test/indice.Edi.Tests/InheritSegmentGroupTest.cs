using System;
using System.Collections.Generic;
using System.Text;
using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests
{
    public class InheritSegmentGroupTest
    {
        [Fact]
        public void InheritSegmentGroup() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = new InheritSegmentGroup();
            interchange.Messages = new List<Models.InheritSegmentGroup.Message>();
            var msg = new InheritSegmentGroup.Message();
            msg.Id = "Group1";
            msg.Element = new Models.InheritSegmentGroup.InGroup();
            msg.Element.Id = "Element1";
            interchange.Messages.Add(msg);

            string output = null;
            using (var writer = new System.IO.StringWriter()) {
                new EdiSerializer().Serialize(writer, grammar, interchange);
                output = writer.ToString();
            }

            var GrpPos = output.IndexOf("Group1");
            var ElmPos = output.IndexOf("Element1");

            Assert.True(GrpPos >= 0);
            Assert.True(ElmPos >= 0);
            Assert.True(GrpPos < ElmPos);
        }
    }
}
