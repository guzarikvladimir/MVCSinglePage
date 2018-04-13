namespace Core.Contract.Contract
{
    public interface IEncodedValueParserService<out T>
    {
        T Get(string value);
    }
}