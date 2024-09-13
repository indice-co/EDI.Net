using indice.Edi.Tests.Models;
using Xunit;

namespace indice.Edi.Tests;

public class Edifact_AutoEndSegmentGroups
{
    [Fact]
    [Trait(Traits.Tag, "EDIFact")]
    [Trait(Traits.Issue, "#75")]
    public void AutoEndSegmentGroups() {
        var grammar = EdiGrammar.NewEdiFact();
        var interchange = default(AutoEndSegmentGroups);

        using (var stream = Helpers.GetResourceStream("edifact.AutoEndSegmentGroups.edi")) {
            var serializer = new EdiSerializer {AutoEndSegmentGroups = true};
            interchange = serializer.Deserialize<AutoEndSegmentGroups>(new StreamReader(stream), grammar);
        }

        Assert.Single(interchange.Messages);

        var message = interchange.Messages.First();
        Assert.Equal("Message1.Group", message.Group.Id);
        Assert.NotNull(message.Group.Element1);
        Assert.Equal("Message1.Group.Element1", message.Group.Element1.Id);
        Assert.NotNull(message.Group.Element2);
        Assert.Equal("Message1.Group.Element2", message.Group.Element2.Id);
        Assert.NotNull(message.AfterGroup);
        Assert.Equal("Message1.AfterGroup", message.AfterGroup.Id);
    }
}