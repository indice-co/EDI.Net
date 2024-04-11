using indice.Edi.Tests.Issue256.InterchangeSegments;

namespace indice.Edi.Tests.Issue256.Interchanges;

public class Interchange<T>
{
    public UNB InterchangeHeader { get; set; }
    public T Invoice { get; set; }
    public UNZ InterchangeTrailer { get; set; }
}
