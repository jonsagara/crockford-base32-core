namespace CrockfordBase32
{
    using System.Collections.Generic;

    internal class SymbolDefinition
    {
        public int Value { get; set; }
        public IEnumerable<char> DecodeSymbols { get; set; } = null!;
        public char EncodeSymbol { get; set; }
    }
}
