using System.Collections.Generic;
using System.IO;
using System.Linq;
using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests
{
    public class Edifact_AutoEndSegmentGroups
    {
        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        [Trait(Traits.Issue, "#75")]
        public void AutoEndSegmentGroups() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(AutoEndSegmentGroups);

            using (var stream = Helpers.GetResourceStream("edifact.AutoEndSegmentGroups.edi")) {
                var serializer = new EdiSerializer { AutoEndSegmentGroups = true };
                interchange = serializer.Deserialize<AutoEndSegmentGroups>(new StreamReader(stream), grammar);
            }

            Assert.Equal(1, interchange.Messages.Count);

            var message = interchange.Messages.First();
            Assert.Equal(message.Group.Id, "Message1.Group");
            Assert.NotNull(message.Group.Element1);
            Assert.Equal(message.Group.Element1.Id, "Message1.Group.Element1");
            Assert.NotNull(message.Group.Element2);
            Assert.Equal(message.Group.Element2.Id, "Message1.Group.Element2");
            Assert.NotNull(message.AfterGroup);
            Assert.Equal(message.AfterGroup.Id, "Message1.AfterGroup");
        }

        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        public void AutoEndSegmentGroupsImplicitSegments() {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(AutoEndSegmentGroups);

            Assert.Throws<EdiException>(() => {
                using (var stream = Helpers.GetResourceStream("edifact.AutoEndSegmentGroups.edi")) {
                    var serializer = new EdiSerializer { AutoEndSegmentGroups = true, AutoEndSegmentGroupsOnImplicitSegments = true };
                    interchange = serializer.Deserialize<AutoEndSegmentGroups>(new StreamReader(stream), grammar);
                }
            });
        }

    }
}