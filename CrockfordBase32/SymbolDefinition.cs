using System.Collections.Generic;

namespace CrockfordBase32
{
    internal class SymbolDefinition
    {
        public int Value { get; set; }
        public IEnumerable<char> DecodeSymbols { get; set; }
        public char EncodeSymbol { get; set; }
    }
}