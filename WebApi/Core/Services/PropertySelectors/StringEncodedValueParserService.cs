using Core.Contract.Contract;

namespace Core.Services.PropertySelectors
{
    public class StringEncodedValueParserService : IEncodedValueParserService<string>
    {
        public string Get(string value)
        {
            return value;
        }
    }
}