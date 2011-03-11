using System;
using System.Collections.Generic;

namespace CrockfordBase32
{
    public class CrockfordBase32Encoding
    {
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
            return new string(encodeMappings[number], 1);
        }
    }
}