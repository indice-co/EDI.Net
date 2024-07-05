using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace indice.Edi.Tests;

public static class Helpers
{
    private static readonly Assembly _assembly = typeof(EdiTextReaderTests).GetTypeInfo().Assembly;
    public static Stream GetResourceStream(string fileName) {
        var qualifiedResources = _assembly.GetManifestResourceNames().OrderBy(x => x).ToArray();
        Stream stream = _assembly.GetManifestResourceStream("indice.Edi.Tests.Samples." + fileName);
        return stream;
    }

    public static MemoryStream StreamFromString(string value) {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }

    public static Stream GetBigSampleStream(string fileName) {
        var path = Path.Combine(Path.GetDirectoryName(_assembly.Location), "SamplesBig", fileName);
        Stream stream = File.OpenRead(path);
        return stream;
    }
}
