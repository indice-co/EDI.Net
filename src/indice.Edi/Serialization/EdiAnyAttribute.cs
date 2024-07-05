namespace indice.Edi.Serialization;

/// <summary>
/// The mode specified how the conditions found on top of properties and fields will be handled. <see cref="All"/> 
/// means all conditions must be satisfied versus <seealso cref="Any"/> means atleast one.
/// </summary>
public enum EdiConditionStackMode : byte
{
    /// <summary>
    /// All conditions must be satisfied.
    /// </summary>
    All = 0,

    /// <summary>
    /// At least one condition must be satisfied.
    /// </summary>
    Any = 1
}

/// <summary>
/// In case we need to alter the default <see cref="EdiConditionAttribute"/> stacking behavior. 
/// By default all stacked conditions must be satisfied for a binding to take place. This attribute changes the behavior to at least one.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class EdiAnyAttribute : EdiAttribute
{
}
