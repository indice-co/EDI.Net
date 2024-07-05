namespace indice.Edi.Serialization;

/// <summary>
/// <see cref="EdiGroupAttribute"/> marks a propery/class to be deserialized for any group found.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class EdiGroupAttribute : EdiStructureAttribute
{
    
}
