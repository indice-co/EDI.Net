using indice.Edi.Tests.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace indice.Edi.Tests
{
    public class EdiTextReaderTests
    {
        [Fact]
        public void ReaderTest() {
            var msgCount = 0;
            var grammar = EdiGrammar.NewTradacoms();

            using (var ediReader = new EdiTextReader(new StreamReader(Helpers.GetResourceStream("tradacoms.order9.edi")), grammar)) {
                while (ediReader.Read()) {
                    if (ediReader.IsStartMessage) {
                        msgCount++;
                    }
                }
            }
            Assert.Equal(4, msgCount);
        }
        
        [Fact]
        public void DeserializeTest() {
            var grammar = EdiGrammar.NewTradacoms();
            var interchange = default(Interchange);
            using (var stream = Helpers.GetResourceStream("tradacoms.utilitybill.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange>(new StreamReader(stream), grammar);
            }
            Assert.Equal(1, interchange.Invoices.Count);
        }

        [Fact]
        public void EscapeCharactersTest() {
            var grammar = EdiGrammar.NewTradacoms();
            var interchange = default(Interchange);
            using (var stream = Helpers.GetResourceStream("tradacoms.utilitybill.escape.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange>(new StreamReader(stream), grammar);
            }
            Assert.Equal("GEORGE'S FRIED CHIKEN + SONS. Could be the best chicken yet?", interchange.Head.ClientName);
        }

        [Fact]
        public void EdiFact_01_Test()
        {
            var grammar = EdiGrammar.NewEdiFact();
            var interchange = default(Models.EdiFact01.Interchange);
            using (var stream = Helpers.GetResourceStream("edifact.01.edi"))
            {
                interchange = new EdiSerializer().Deserialize<Models.EdiFact01.Interchange>(new StreamReader(stream), grammar);
            }

            //Test Interchange de-serialization
            Assert.Equal("UNOC", interchange.SyntaxIdentifier);
            Assert.Equal(3, interchange.SyntaxVersion);
            Assert.Equal("1234567891123", interchange.SenderId);
            Assert.Equal("14", interchange.PartnerIDCodeQualifier);
            Assert.Equal("7080005059275", interchange.RecipientId);
            Assert.Equal("14", interchange.ParterIDCode);
            Assert.Equal("SPOTMARKED", interchange.RoutingAddress);
            Assert.Equal(new DateTime(2012, 10, 10, 11, 4, 0), interchange.DateOfPreparation);
            Assert.Equal("HBQ001", interchange.ControlRef);


            var quote = interchange.QuoteMessage;

            //Test Quote Message Header
            Assert.Equal("1", quote.MessageRef);
            Assert.Equal("QUOTES", quote.MessageType);
            Assert.Equal("D", quote.Version);
            Assert.Equal("96A", quote.ReleaseNumber);
            Assert.Equal("UN", quote.ControllingAgency);
            Assert.Equal("EDIEL2", quote.AssociationAssignedCode);
            Assert.Equal("S", quote.CommonAccessRef);

            Assert.Equal("310", quote.MessageName);
            Assert.Equal("2010101900026812", quote.DocumentNumber);
            Assert.Equal("9", quote.MessageFunction);
            Assert.Equal("AB", quote.ResponseType);

            Assert.NotNull(interchange.QuoteMessage.MessageDate.ID);
            Assert.NotNull(interchange.QuoteMessage.ProcessingStartDate.ID);
            Assert.NotNull(interchange.QuoteMessage.ProcessingEndDate.ID);


            Assert.Equal(new DateTime(2010, 10, 19, 11, 04, 00), quote.MessageDate.DateTime);
            Assert.Equal(new DateTime(2010, 10, 19, 23, 00, 00), quote.ProcessingStartDate.DateTime);
            Assert.Equal(new DateTime(2010, 10, 20, 23, 00, 00), quote.ProcessingEndDate.DateTime);

            Assert.Equal(1, quote.UTCOffset.Hours);

            Assert.Equal("2", quote.CurrencyQualifier);
            Assert.Equal("SEK", quote.ISOCurrency);


            Assert.Equal(2, quote.NAD.Count);
            Assert.Equal("FR", quote.NAD[0].PartyQualifier);
            Assert.Equal("1234567891123", quote.NAD[0].PartyId);
            Assert.Equal("9", quote.NAD[0].ResponsibleAgency);

            Assert.Equal("DO", quote.NAD[1].PartyQualifier);
            Assert.Equal("7080005059275", quote.NAD[1].PartyId);
            Assert.Equal("9", quote.NAD[1].ResponsibleAgency);

            Assert.Equal("105", quote.LocationQualifier);
            Assert.Equal("SE1", quote.LocationId);
            Assert.Equal("SM", quote.LocationResponsibleAgency);

            Assert.Equal("SE1", quote.LocationId);
            Assert.Equal("SM", quote.LocationResponsibleAgency);
            
            var linArray = quote.Lines;
            Assert.Equal(new DateTime(2010, 10, 19, 23, 00, 00), linArray[0].Period.Date.From);
            Assert.Equal(new DateTime(2010, 10, 20, 00, 00, 00), linArray[1].Period.Date.From);
            Assert.Equal(new DateTime(2010, 10, 20, 01, 00, 00), linArray[2].Period.Date.From);
            Assert.All(quote.Lines, i => Assert.Equal(2, i.Prices.Count));
        }
        
        [Fact]
        public void X12_Grammar_Test() {
            var grammar = EdiGrammar.NewX12();
            var interchange = default(Models.PurchaseOrder_850);
            using (var stream = Helpers.GetResourceStream("x12.850.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.PurchaseOrder_850>(new StreamReader(stream), grammar);
            }
            Assert.Equal(new DateTime(2009, 8, 27, 9, 36, 00), interchange.Date);
            Assert.Equal(new DateTime(2009, 8, 27, 10, 41, 00), interchange.Groups[0].Date);
            Assert.Equal(19.95M, interchange.Groups[0].Orders[0].Items[0].UnitPrice);
            Assert.Equal("126 Any St", interchange.Groups[0].Orders[0].Addresses[0].AddressInformation);
        }

        [Fact]
        public void X12_850_Issue27_Test() {
            var grammar = EdiGrammar.NewX12();
            var interchange = default(Models.PurchaseOrder_850);
            using (var stream = Helpers.GetResourceStream("x12.850a.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.PurchaseOrder_850>(new StreamReader(stream), grammar);
            }
            Assert.Equal(new DateTime(2009, 8, 27, 9, 36, 00), interchange.Date);
            Assert.Equal(new DateTime(2009, 8, 27, 10, 41, 00), interchange.Groups[0].Date);
            Assert.Equal(19.95M, interchange.Groups[0].Orders[0].Items[0].UnitPrice);
            Assert.Equal("126 Any St", interchange.Groups[0].Orders[0].Addresses[0].AddressInformation);
        }

        [Fact]
        public void X12_214_Test() {
            var grammar = EdiGrammar.NewX12();
            var interchange = default(Models.Transportation_214);
            using (var stream = Helpers.GetResourceStream("x12.214.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.Transportation_214>(new StreamReader(stream), grammar);
            }
            var message = interchange.Groups[0].Messages[0];
            Assert.Equal(3, message.Places.Count);
            Assert.Equal(1751807, message.ReferenceIdentification);
        }
        [Fact]
        public void X12_204_Test() {
            var grammar = EdiGrammar.NewX12();
            grammar.SetAdvice(
                segmentNameDelimiter: '*', 
                dataElementSeparator: '*', 
                componentDataElementSeparator: ':', 
                segmentTerminator: '~', 
                releaseCharacter: null, 
                reserved: null, 
                decimalMark: '.');

            string text = File.ReadAllText(@"C:\Users\cleft\Source\GitHub\indice\EDI.Net\test\indice.Edi.Tests\Samples\204-MGCTLYST-SAMPLE.EDI");
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(text.Replace('\n', '~')));
            
            var segmentCount = 0;
            using (var ediReader = new EdiTextReader(new StreamReader(stream), grammar)) {
                while (ediReader.Read()) {
                    if (ediReader.TokenType == EdiToken.SegmentName) {
                        segmentCount++;
                    }
                }
            }
            Assert.Equal(43, segmentCount);
        }

        [Fact]
        public void ReaderStrips_Z_Padding_Test() {
            var grammar = EdiGrammar.NewEdiFact();
            using (var ediReader = new EdiTextReader(new StreamReader(Helpers.StreamFromString("DTM+ZZZ'DTM+ZZ1'")), grammar)) {
                ediReader.Read();
                ediReader.Read();
                ediReader.Read();
                ediReader.Read(); // move to component
                var number = ediReader.ReadAsInt32();
                Assert.Null(number);
                ediReader.Read();
                ediReader.Read();
                ediReader.Read();
                ediReader.Read(); // move to component;
                var number2 = ediReader.ReadAsInt32();
                Assert.Equal(1, number2.Value);
            }
        }
    }
}
