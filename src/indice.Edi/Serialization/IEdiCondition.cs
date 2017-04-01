namespace indice.Edi.Serialization
{
    public interface IEdiCondition
    {
        bool SatisfiedBy(string value);
    }
}
