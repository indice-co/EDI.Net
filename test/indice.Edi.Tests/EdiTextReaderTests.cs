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
        [Trait(Traits.Tag, "TRADACOMS")]
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
        [Trait(Traits.Tag, "TRADACOMS")]
        public void DeserializeTest() {
            var grammar = EdiGrammar.NewTradacoms();
            var interchange = default(Interchange);
            using (var stream = Helpers.GetResourceStream("tradacoms.utilitybill.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange>(new StreamReader(stream), grammar);
            }
            Assert.Equal(1, interchange.Invoices.Count);
        }

        [Fact]
        [Trait(Traits.Tag, "TRADACOMS")]
        public void EscapeCharactersTest() {
            var grammar = EdiGrammar.NewTradacoms();
            var interchange = default(Interchange);
            using (var stream = Helpers.GetResourceStream("tradacoms.utilitybill.escape.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange>(new StreamReader(stream), grammar);
            }
            Assert.Equal("GEORGE'S FRIED CHIKEN + SONS. Could be the best chicken yet?", interchange.Head.ClientName);
        }

        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
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
        [Trait(Traits.Tag, "X12")]
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
        [Trait(Traits.Tag, "X12")]
        public void X12_850_Issue27_Test() {
            var grammar = EdiGrammar.NewX12();
            var interchange = default(Models.PurchaseOrder_850);
            using (var stream = Helpers.GetResourceStream("x12.850a.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.PurchaseOrder_850>(new StreamReader(stream), grammar);
            }
            Assert.Equal(new DateTime(2009, 8, 27, 9, 36, 00), interchange.Date);
            Assert.Equal(new DateTime(2009, 8, 27, 10, 41, 00), interchange.Groups[0].Date);
            Assert.Equal(2.53M, interchange.Groups[0].Orders[0].Items[0].UnitPrice);
            Assert.Equal("East Point Drive, Suite 500", interchange.Groups[0].Orders[0].Addresses[0].AddressInformation);
            Assert.Equal(2, interchange.Groups[0].Orders[0].Items[0].MSG.Count());
            Assert.Equal("4.4OZ 100% POLYESTER QUILT", interchange.Groups[0].Orders[0].Items[0].MSG[0].MessageText);
            Assert.Equal("First Quality", interchange.Groups[0].Orders[0].Items[0].MSG[1].MessageText);
        }

        [Fact]
        [Trait(Traits.Tag, "X12")]
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
        [Trait(Traits.Tag, "X12")]
        public void X12_214_Trailers_Test() {
            var grammar = EdiGrammar.NewX12();
            var interchange = default(Models.Transportation_214);
            using (var stream = Helpers.GetResourceStream("x12.214.edi")) {
                interchange = new EdiSerializer().Deserialize<Models.Transportation_214>(new StreamReader(stream), grammar);
            }
            var group = interchange.Groups[0];
            var message = group.Messages[0];

            Assert.Equal(16, message.MessageSegmetsCount);
            Assert.Equal("822650001", message.MessageControlNumber);
            Assert.Equal(1, group.TransactionsCount);
            Assert.Equal(82265, group.GroupTrailerControlNumber);
        }
        [Fact]
        [Trait(Traits.Tag, "X12")]
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

            string text = string.Empty;
            using (var filestream = Helpers.GetResourceStream("204-MGCTLYST-SAMPLE.EDI"))
            using (var reader = new StreamReader(filestream))
                text = reader.ReadToEnd();
            
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(text.Replace('\n', '~')));
            
            var segmentCount = 0;
            using (var ediReader = new EdiTextReader(new StreamReader(stream), grammar)) {
                while (ediReader.Read()) {
                    if (ediReader.TokenType == EdiToken.SegmentName) {
                        segmentCount++;
                    }
                }
            }
            Assert.Equal(42, segmentCount);
        }

        [Fact]
        [Trait(Traits.Tag, "Parser")]
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

        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        public void EdiFact_D95B_CUSCAR_Test() {
            var grammar = EdiGrammar.NewEdiFact();

            var interchange = default(Interchange_D95B_CUSCAR);
            using (var stream = Helpers.GetResourceStream("edifact.D95B.CUSCAR.EDI")) {
                interchange = new EdiSerializer().Deserialize<Interchange_D95B_CUSCAR>(new StreamReader(stream), grammar);
            }

            //Interchange header
            Assert.Equal(interchange.Header_Field1a, "UNOA");
            Assert.Equal(interchange.Header_Field1b, "1");
            Assert.Equal(interchange.Header_Field2, "MSC");
            Assert.Equal(interchange.Header_Field3, "ECA");
            Assert.Equal(interchange.Header_Field4a, "20170119");
            Assert.Equal(interchange.Header_Field4b, "1010");
            Assert.Equal(interchange.Header_Field5, "20170119101016");
            

            //Message header info
            Assert.NotNull(interchange.Message);
            Assert.Equal(interchange.Message.Field1, "201701191AB652");
            Assert.Equal(interchange.Message.Field2a, null);
            Assert.Equal(interchange.Message.Field2b, "D");
            Assert.Equal(interchange.Message.Field2c, "95B");
            Assert.Equal(interchange.Message.Field2d, "UN");


            //BGM - Begging of message
            Assert.Equal(interchange.Message.BGM.Field1, "85");
            Assert.Equal(interchange.Message.BGM.Field2, "201701191AB652");
            Assert.Equal(interchange.Message.BGM.Field3, "9");


            Assert.Equal(interchange.Message.DTM.Field1a, "137");
            Assert.Equal(interchange.Message.DTM.Field1b, "20170119");
            Assert.Equal(interchange.Message.DTM.Field1c, "102");

            //NAD - Name And Addresses
            Assert.Equal(interchange.Message.NAD.Field1, "MS");
            Assert.Equal(interchange.Message.NAD.Field2a, "202487288");
            Assert.Equal(interchange.Message.NAD.Field2b, "172");
            Assert.Equal(interchange.Message.NAD.Field2c, "166");
            Assert.Equal(interchange.Message.NAD.Field3, "MSC");

            //Grou 1 - TDT Details of transport
            Assert.NotNull(interchange.Message.TDT_Group1);
            Assert.Equal(interchange.Message.TDT_Group1.Count(), 1);

            Assert.Equal(interchange.Message.TDT_Group1[0].Field1, "20");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field2, "AB652A");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field3, "1");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field4, null);
            Assert.Equal(interchange.Message.TDT_Group1[0].Field5a, "MSC");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field5b, "172");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field5c, "166");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field5d, "MSC SENA");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field6, null);
            Assert.Equal(interchange.Message.TDT_Group1[0].Field7a, null);
            Assert.Equal(interchange.Message.TDT_Group1[0].Field7b, null);
            Assert.Equal(interchange.Message.TDT_Group1[0].Field7c, "202487288");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field8a, "D5LQ7");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field8b, "146");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field8c, "14");
            Assert.Equal(interchange.Message.TDT_Group1[0].Field8d, null);
            Assert.Equal(interchange.Message.TDT_Group1[0].Field8e, "EG");

            //Group 1 - LOC Place/location identification
            Assert.NotNull(interchange.Message.TDT_Group1[0].LOC);
            Assert.Equal(interchange.Message.TDT_Group1[0].LOC.Count(), 1);
            Assert.Equal(interchange.Message.TDT_Group1[0].LOC[0].Field1, "60");
            Assert.Equal(interchange.Message.TDT_Group1[0].LOC[0].Field2a, "EGEDK");
            Assert.Equal(interchange.Message.TDT_Group1[0].LOC[0].Field2b, "139");
            Assert.Equal(interchange.Message.TDT_Group1[0].LOC[0].Field2c, null);
            Assert.Equal(interchange.Message.TDT_Group1[0].LOC[0].Field2d, "El Dekheila");

            //Group 1 - DTM Date/time/period
            Assert.NotNull(interchange.Message.TDT_Group1[0].DTM);
            Assert.Equal(interchange.Message.TDT_Group1[0].DTM.Count(), 2);

            Assert.Equal(interchange.Message.TDT_Group1[0].DTM[0].Field1a, "132");
            Assert.Equal(interchange.Message.TDT_Group1[0].DTM[0].Field1b, "1701290000");
            Assert.Equal(interchange.Message.TDT_Group1[0].DTM[0].Field1c, "201");
            Assert.Equal(interchange.Message.TDT_Group1[0].DTM[1].Field1a, "137");
            Assert.Equal(interchange.Message.TDT_Group1[0].DTM[1].Field1b, "20170110");
            Assert.Equal(interchange.Message.TDT_Group1[0].DTM[1].Field1c, "102");
            

            //Group 4 - CNI
            Assert.NotNull(interchange.Message.CNI_Group4);
            Assert.Equal(interchange.Message.CNI_Group4.Count(), 2);

            //CNI Segment 1
            Assert.Equal(interchange.Message.CNI_Group4[0].Field1, "1");
            Assert.Equal(interchange.Message.CNI_Group4[0].Field2, "MSCUXP080935");

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].Field1, "AAA");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].Field2, "MSCUDB592345");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[0].Field1, "9");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[0].Field2a, "IEDUB");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[0].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[0].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[0].Field2d, null);

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[1].Field1, "11");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[1].Field2a, "EGEDK");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[1].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[1].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].LOC[1].Field2d, null);
            

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[0].Field1, "CZ");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[0].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[0].Field2b, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[0].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[0].Field3, "LAKELAND DAIRIES,");

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[1].Field1, "CN");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[1].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[1].Field2b, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[1].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[1].Field3, "FRONERI ICE CREAM EGYPT");

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[2].Field1, "N1");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[2].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[2].Field2b, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[2].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].NAD[2].Field3, "FRONERI ICE CREAM EGYPT");


            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].Field1, "1");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].Field2a, "560");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].Field2b, "BG");
            

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[1].Field1, "2");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[1].Field2a, "880");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[1].Field2b, "BG");
            

            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].Field1, "1");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].Field2a, "560");
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].Field2b, "BG");
                                                        
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].FTX.Field1, "AAA");
                                                        
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].MEA[0].Field1, "AAE");
                                                        
            Assert.Equal(interchange.Message.CNI_Group4[0].RFF_Group5[0].GID_Group10[0].SPG.Field1, "FCIU5956299");
            


            //CNI Segment 2
            Assert.Equal(interchange.Message.CNI_Group4[1].Field1, "2");
            Assert.Equal(interchange.Message.CNI_Group4[1].Field2, "MSCUEK569969");

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].Field1, "BM");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].Field2, "MSCUEK569969");

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[0].Field1, "9");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[0].Field2a, "SGSIN");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[0].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[0].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[0].Field2d, null);

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[1].Field1, "11");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[1].Field2a, "EGALY");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[1].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[1].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[1].Field2d, null);

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[2].Field1, "8");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[2].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[2].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[2].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[2].Field2d, null);

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[3].Field1, "80");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[3].Field2a, "SG");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[3].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[3].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[3].Field2d, null);

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[4].Field1, "28");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[4].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[4].Field2b, "139");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[4].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].LOC[4].Field2d, null);



            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[0].Field1, "CZ");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[0].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[0].Field2b, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[0].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[0].Field3, "BPI A/S");
                                                        
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[1].Field1, "CN");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[1].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[1].Field2b, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[1].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[1].Field3, "TO ORDER");
                                                        
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[2].Field1, "N1");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[2].Field2a, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[2].Field2b, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[2].Field2c, null);
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].NAD[2].Field3, "EL IMAN COMPANY FOR TRADING EXPORT AND IMPORT (SAAD AND MOHAMED ELFAR)");


            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].GID_Group10[0].Field1, "1");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].GID_Group10[0].Field2a, "699");
            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].GID_Group10[0].Field2b, "BG");

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].GID_Group10[0].FTX.Field1, "AAA");

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].GID_Group10[0].MEA[0].Field1, "AAE");

            Assert.Equal(interchange.Message.CNI_Group4[1].RFF_Group5[0].GID_Group10[0].SPG.Field1, "FCIU2469131");

            //Interchange trailer
            Assert.Equal(interchange.Trailer_Field1, 1);
            Assert.Equal(interchange.Trailer_Field2, "20170119101016");
        }

        [Fact]
        [Trait(Traits.Tag, "EDIFact")]
        [Trait(Traits.Issue, "#35")]
        [Trait(Traits.Issue, "#42")]
        public void EdiFact_ORDRSP_Test() {
            var grammar = EdiGrammar.NewEdiFact();

            var interchange = default(Interchange_ORDRSP);
            using (var stream = Helpers.GetResourceStream("edifact.ORDRSP-formatted.edi")) {
                interchange = new EdiSerializer().Deserialize<Interchange_ORDRSP>(new StreamReader(stream), grammar);
            }

            Assert.Equal(2, interchange.Message.IMD_List.Count);
            Assert.NotNull(interchange.Message.IMD_Other);

            Assert.Null(interchange.Message.IMD_List[0].FieldA);
            Assert.Equal("Z01", interchange.Message.IMD_List[0].FieldB);
            Assert.Null(interchange.Message.IMD_List[0].FieldC);

            Assert.Null(interchange.Message.IMD_List[1].FieldA);
            Assert.Equal("Z10", interchange.Message.IMD_List[1].FieldB);
            Assert.Null(interchange.Message.IMD_List[1].FieldC);

            Assert.Null(interchange.Message.IMD_Other.FieldA);
            Assert.Equal("Z14", interchange.Message.IMD_Other.FieldB);
            Assert.Equal("Z07", interchange.Message.IMD_Other.FieldC);
        }

    }
}
