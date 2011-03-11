using System.Collections.Generic;
using System.Linq;

namespace CrockfordBase32
{
    public class CrockfordBase32Encoding
    {
        const int Base = 32;

        static readonly IDictionary<int, char> encodeMappings;
        static readonly IDictionary<char, int> decodeMappings;
        static CrockfordBase32Encoding()
        {
            var symbols = new SymbolDefinitions();
            encodeMappings = symbols.EncodeMappings;
            decodeMappings = symbols.DecodeMappings;
        }

        public string GetString(int number)
        {
            if (number == 0)
                return encodeMappings[0].ToString();

            var characters = new List<char>();

            var nextBase = 1 * Base;
            while (number > 0)
            {
                var currentValue = number % nextBase;
                number = (number - currentValue) / Base;
                characters.Add(encodeMappings[currentValue]);
                nextBase *= Base;
            }

            return new string(((IEnumerable<char>)characters).Reverse().ToArray());
        }
    }
}