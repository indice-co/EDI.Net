using System.Text.RegularExpressions;

namespace indice.Edi.Serialization;

/// <summary>
/// Base class for <see cref="EdiAttribute"/>s 
/// </summary>
public abstract class EdiAttribute : Attribute
{
    /// <summary>
    /// String representation of this attribute.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Regex.Replace(GetType().Name, "Edi(.*)Attribute", "$1");
}
