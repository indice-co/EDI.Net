using indice.Edi.Tests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Console
{
    public class Program
    {
        public void Main(string[] args)
        {
            var grammar = EdiGrammar.NewTradacoms();
            var interchange = default(Interchange);
            using (var textreader = File.OpenText(@"C:\Users\c.leftheris\Source\Repos\EDI.Net\src\indice.Edi.Tests\Samples\tradacoms.utilitybill.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange>(textreader, grammar);
            }
            var count = interchange.Invoices.Count;
        }
    }
}
