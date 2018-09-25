using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiGrammarTests
    {
        [Fact]
        public void EdiGrammarSetAdvice_ChangesSpecialCharacters() {

            var grammar = EdiGrammar.NewEdiFact();
            Assert.True(grammar.IsSpecial('\''));
            grammar.SetAdvice(new[] { ':', '+', '.', '?', ' ', '\r' });
            Assert.True(grammar.IsSpecial('\r'));
        }
    }
}
