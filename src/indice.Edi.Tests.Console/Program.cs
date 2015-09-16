using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Console
{
    public class Program
    {
        public void Main(string[] args)
        {
            var tests = new EdiTextReaderTests();
            tests.EdiTextReaderTest();
        }
    }
}
