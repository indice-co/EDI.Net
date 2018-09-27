# EDI.Net ![alt text](design/logo-64.png "Edi Net logo")

[![Build status](https://ci.appveyor.com/api/projects/status/8tqewqqey5vim3ul?svg=true)](https://ci.appveyor.com/project/cleftheris/edi-net) 
[![NuGet Downloads](https://img.shields.io/nuget/dt/indice.Edi.svg)](https://www.nuget.org/packages/indice.Edi/)

EDI Serializer/Deserializer. Used to read & write EDI streams. 

This is a ground up implementation and __does not__ make use of `XML Serialization` in any step of the process. This reduces the overhead of converting into multiple formats allong the way of getting the desired Clr object. This makes the process quite fast.

Tested with __[Tradacoms](https://en.wikipedia.org/wiki/TRADACOMS)__, __[EDIFact](https://en.wikipedia.org/wiki/EDIFACT)__ and __[ANSI ASC X12](https://en.wikipedia.org/wiki/ASC_X12) (X12)__ formats. 

Using attributes you can express all EDI rules like Mandatory/Conditional Segments, Elements & Components 
as well as describe component values size length and precision with the [picture syntax](#the-picture-clause) (e.g `9(3)`, `9(10)V9(2)` and `X(3)`). 

## Quick links

* [Installation](#installation)
* [Attributes](#attributes)
* [Example usage](#example-usage)
  * [Deserialization (EDI to POCOs)](#deserialization-edi-to-pocos)
  * [Serialization (POCOs to EDI)](#serialization-pocos-to-edi)
* [Contributions](#contributions)
* [The Picture clause](#the-picture-clause)
* [Roadmap](#roadmap-todo)

## Installation

To install Edi.Net, run the following command in the Package Manager Console. Or download it [here](https://www.nuget.org/packages/indice.Edi/)

```powershell
PM> Install-Package "indice.Edi"
```
## Attributes. 
The general rules of thumb are :

| Attribute             | Description      | 
|-----------------------|------------------|
| __EdiValue__          | Any value inside a segment. (ie the component value _500_ in bold) | 
|                       | UCI+001342651817+9907137000005:500+9912022000002:__500__+7         |
| __EdiElement__        | Elements are considered to be groups of values otherwise known as groups of components. One can use this attribute to deserialize into a complex class that resides inside a segment. For example this can usually be used to deserialize more than one value between `+` into a ComplexType (ie the whole element into a new class _9912022000002:500_ in bold) |
|                       | UCI+001342651817+9907137000005:500+__9912022000002:500__+7 |
| __EdiPath__           | To specify the path |
| __EdiSegment__        | Marks a propery/class to be deserialized for a given segment. Used in conjunction with EdiPath  |
| __EdiSegmentGroup__   | Marks a propery/class as a logical container of segments. This allows a user to decorate a class whith information regarding the starting and ending segments that define a virtual group other than the standard ones (Functional Group etc). Can be applied on Lists the same way that `[Message]` or `[Segment]` attributes work |
| __EdiMessage__        | Marks a propery/class to be deserialized for any message found.  |
| __EdiGroup__          | Marks a propery/class to be deserialized for any group found. |
| __EdiCondition__      | In case multiple MessageTypes or Segment types with the same name. Used to discriminate the classes based on a component value |


## Example usage:

There are available configurations (`EdiGrammar`) for `EDIFact`, `Tradacoms` and `X12`. Working examples for all supported EDI formats can be found in the source code under [tests](https://github.com/indice-co/EDI.Net/tree/master/test/indice.Edi.Tests).

- EdiFact [sample POCO classes](https://github.com/indice-co/EDI.Net/blob/master/test/indice.Edi.Tests/Models/EdiFact01.cs)
- TRADACOMS [sample classes](https://github.com/indice-co/EDI.Net/blob/master/test/indice.Edi.Tests/Models/UtilityBill.cs) (UtilityBill)
- X12 [sample classes](https://github.com/indice-co/EDI.Net/blob/master/test/indice.Edi.Tests/Models/X12_850.cs) (850 Purchase Order)

_Note that all examples may be partialy implemented transmissions for demonstration purposes although they are a good starting point. If someone has complete poco classes for any transmition please feel free to contribute a complete test._


### Deserialization (EDI to POCOs)
The following example makes use of the `Tradacoms` grammar and deserializes the `sample.edi` file to the `Interchange` class. 


```csharp
var grammar = EdiGrammar.NewTradacoms();
var interchange = default(Interchange);
using (var stream = new StreamReader(@"c:\temp\sample.edi")) {
    interchange = new EdiSerializer().Deserialize<Interchange>(stream, grammar);
}
```

### Serialization (POCOs to EDI)
In this case we are instantiating our POCO class `Interchange` and then fill-it up with values before finally serializing to `out.edi`.

```csharp
var grammar = EdiGrammar.NewTradacoms();
var interchange = new Interchange();
// fill properies 
interchange.TransmissionDate = DateTime.Now;
...
// serialize to file.
using (var textWriter = new StreamWriter(File.Open(@"c:\temp\out.edi", FileMode.Create))) {
    using (var ediWriter = new EdiTextWriter(textWriter, grammar)) { 
        new EdiSerializer().Serialize(ediWriter, interchange);
    }
}
```

### Model
Annotated POCOS example using **part** of Tradacoms UtilityBill format:

```csharp
public class Interchange
{
    [EdiValue("X(14)", Path = "STX/1/0")]
    public string SenderCode { get; set; }

    [EdiValue("X(35)", Path = "STX/1/1")]
    public string SenderName { get; set; }

    [EdiValue("9(6)", Path = "STX/3/0", Format = "yyMMdd", Description = "TRDT - Date")]
    [EdiValue("9(6)", Path = "STX/3/1", Format = "HHmmss", Description = "TRDT - Time")]
    public DateTime TransmissionStamp { get; set; }

    public InterchangeHeader Header { get; set; }

    public InterchangeTrailer Trailer { get; set; }

    public List<UtilityBill> Invoices { get; set; }
}

[EdiMessage, EdiCondition("UTLHDR", Path = "MHD/1")]
public class InterchangeHeader
{
    [EdiValue("9(4)"), EdiPath("TYP")]
    public string TransactionCode { get; set; }

    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }
}

[EdiMessage, EdiCondition("UTLTLR", Path = "MHD/1")]
public class InterchangeTrailer
{

    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }
}

[EdiMessage, EdiCondition("UTLBIL", Path = "MHD/1")]
public class UtilityBill
{
    [EdiValue("9(1)", Path = "MHD/1/1")]
    public int Version { get; set; }

    [EdiValue("X(17)", Path = "BCD/2/0", Description = "INVN - Date")]
    public string InvoiceNumber { get; set; }

    public MetetAdminNumber Meter { get; set; }
    public ContractData SupplyContract { get; set; }

    [EdiValue("X(3)", Path = "BCD/5/0", Description = "BTCD - Date")]
    public BillTypeCode BillTypeCode { get; set; }

    [EdiValue("9(6)", Path = "BCD/1/0", Format = "yyMMdd", Description = "TXDT - Date")]
    public DateTime IssueDate { get; set; }

    [EdiValue("9(6)", Path = "BCD/7/0", Format = "yyMMdd", Description = "SUMO - Date")]
    public DateTime StartDate { get; set; }

    [EdiValue("9(6)", Path = "BCD/7/1", Format = "yyMMdd", Description = "SUMO - Date")]
    public DateTime EndDate { get; set; }

    public UtilityBillTrailer Totals { get; set; }
    public UtilityBillValueAddedTax Vat { get; set; }
    public List<UtilityBillCharge> Charges { get; set; }

    public override string ToString() {
        return string.Format("{0} TD:{1:d} F:{2:d} T:{3:d} Type:{4}", InvoiceNumber, IssueDate, StartDate, EndDate, BillTypeCode);
    }
}

[EdiSegment, EdiPath("CCD")]
public class UtilityBillCharge
{
    [EdiValue("9(10)", Path = "CCD/0")]
    public int SequenceNumber { get; set; }

    [EdiValue("X(3)", Path = "CCD/1")]
    public ChargeIndicator? ChargeIndicator { get; set; }

    [EdiValue("9(13)", Path = "CCD/1/1")]
    public int? ArticleNumber { get; set; }

    [EdiValue("X(3)", Path = "CCD/1/2")]
    public string SupplierCode { get; set; }

    [EdiValue("9(10)V9(3)", Path = "CCD/10/0", Description = "CONS")]
    public decimal? UnitsConsumedBilling { get; set; }
}
```
    
## Contributions

The following is a set of guidelines for contributing to EDI.Net.

##### Did you find a bug?

- Ensure the bug was not already reported by searching on GitHub under [Issues](https://github.com/indice-co/EDI.Net/issues).
- If you're unable to find an open issue addressing the problem, open a [new one](https://github.com/indice-co/EDI.Net/issues/new). Be sure to include a title and clear description, as much relevant information as possible, and a code sample or an executable test case demonstrating the expected behavior that is not occurring.

##### Did you write a patch that fixes a bug?
Open a new GitHub pull request with the patch.
Ensure the PR description clearly describes the problem and solution. Include the relevant issue number if applicable.

##### Build the sourcecode

As of v1.0.7 the solution was adapted to support the dotnet core project system. Then it was adapted again since the dotnet core tooling was officialy relased (_March 7th 2017 at the launch of Visual studio 2017_).
In order to build and test the source code you will need either one of the following.

- Visual studio 2017 + the dotnet core workload (for `v1.1.3` onwards)
- Visual studio 2015 Update 3 & .NET Core 1.0.0 - VS 2015 Tooling (for versions `v1.0.7` - `v1.1.2`)
- [VS Code + .NET Core SDK](https://code.visualstudio.com/docs/runtimes/dotnet) 

for more information check .Net Core [official page](https://www.microsoft.com/net/core).


## The Picture clause
The _Picture Clause_ is taken from COBOL laguage and the way it handles expressing numeric and alphanumric data types. It is used throughout tradacoms.

|Symbol | Description      | Example Picture  | Component           | c# result
|:-----:|------------------|:----------------:|---------------------|------------------|
|   9   | Numeric          | `9(3)`           | `013`               | `int v = 13;`
|   A   | Alphabetic       | not used         | -                   | -
|   X   | Alphanumeric     | `X(20)`          | `This is alphanumeric`| `string v = "This is alphanumeric";`
|   V   | Implicit Decimal | `9(3)V9(2)`      | `01342`            | `decimal v = 13.42M;`
|   S   | Sign             | not used         | - | - |
|   P   | Assumed Decimal  | not used         | - | - |

## Roadmap (TODO)

- [x] Implement serializer `Serialize` to write Clr classes to edi format (Using attributes). (planned for v1.1) 
- [ ] Start github wiki page and begin documentation.
- [ ] Create a seperate package (or packages per EDI Format) to host well known interchange transmitions (ie Tradacoms Utitlity Bill). Then anyone can fork and contribute his own set of POCO classes.

_Disclaimer. The project was inspired and influenced by the work done in the excellent library [JSON.Net](https://github.com/JamesNK/Newtonsoft.Json) by James Newton King. Some utility parts for reflection string parsing etc. are used as is_
