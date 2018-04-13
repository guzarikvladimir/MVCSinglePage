using Core.Contract.Contract;

namespace Core.Services.PropertySelectors
{
    public class DoubleEncodedValueParserService : IEncodedValueParserService<double?>
    {
        public double? Get(string value)
        {
            return double.Parse(value);
        }
    }
}