using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiTextReaderTests
    {
        public const string TRADACOMS_ORDER9_SAMPLE = "STX=ANA:1+5000000000000:SOME STORES LTD+5010000000000:SUPPLIER UK LTD+070315:130233+000007+PASSW+ORDHDR+B'MHD=1+ORDHDR:9'TYP=0430+NEW-ORDERS'SDT=5010000000000:000030034'CDT=5000000000000'FIL=1630+1+070315'MTR=6'MHD=2+ORDERS:9'CLO=5000000000283:89828+EAST SOMEWHERE DEPOT'ORD=70970::070315'DIN=070321++0000'OLD=1+5010210000000++:00893592+12+60++++CRUSTY ROLLS:4 PACK'OTR=1'MTR=7'MHD=3+ORDTLR:9'OFT=1'MTR=3'END=3'";

        [Fact]
        public void EdiTextReaderTest() {
            var msgCount = 0;
            var interchangeTrailerCount = new int?();
            var grammar = EdiGrammar.NewTradacoms();

            using (var ediReader = new EdiTextReader(new StreamReader(StreamFromString(TRADACOMS_ORDER9_SAMPLE)), grammar)) {
                while (ediReader.Read()) {
                    if (ediReader.IsStartMessage) {
                        msgCount++;
                    }
                    if (ediReader.TokenType == EdiToken.ComponentStart &&
                        ediReader.Path == grammar.InterchangeTrailerTag + "[0]") {
                        interchangeTrailerCount = ediReader.ReadAsInt32();
                    }
                }
            }

            Assert.Equal(673, msgCount);
            Assert.Equal(673, interchangeTrailerCount.Value);
        }

        private MemoryStream StreamFromString(string value) {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
