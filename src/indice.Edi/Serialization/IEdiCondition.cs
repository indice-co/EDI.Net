namespace indice.Edi.Serialization;

/// <summary>
/// An interface for conditions.
/// </summary>
public interface IEdiCondition
{
    /// <summary>
    /// Returns try if the <see cref="IEdiCondition"/> is satisfied for the value passed.
    /// </summary>
    /// <param name="value">The value to check against the condition.</param>
    /// <returns></returns>
    bool SatisfiedBy(string value);
}
