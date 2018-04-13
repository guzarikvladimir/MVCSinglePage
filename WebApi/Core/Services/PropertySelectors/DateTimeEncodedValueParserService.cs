using System;
using Core.Contract.Contract;

namespace Core.Services.PropertySelectors
{
    public class DateTimeEncodedValueParserService : IEncodedValueParserService<DateTime?>
    {
        public DateTime? Get(string value)
        {
            string[] values = value.Split(' ');
            values[0] = values[0].Replace(':', '-');
            values[1] = values[1].Replace('\0', 'Z');
            value = string.Join(" ", values);

            return DateTime.Parse(value);
        }
    }
}