using System.Collections.Generic;
using System.Linq;

namespace CrockfordBase32
{
    internal class SymbolDefinitions : List<SymbolDefinition>
    {
        readonly List<SymbolDefinition> extraCheckDigits = new List<SymbolDefinition>();

        public SymbolDefinitions()
        {
            AddRange(new[]
            {
                new SymbolDefinition { Value = 0, EncodeSymbol = '0', DecodeSymbols = new[] { '0', 'O', 'o' } },
                new SymbolDefinition { Value = 1, EncodeSymbol = '1', DecodeSymbols = new[] { '1', 'I', 'i', 'L', 'l' } },
                new SymbolDefinition { Value = 2, EncodeSymbol = '2', DecodeSymbols = new[] { '2' } },
                new SymbolDefinition { Value = 3, EncodeSymbol = '3', DecodeSymbols = new[] { '3' } },
                new SymbolDefinition { Value = 4, EncodeSymbol = '4', DecodeSymbols = new[] { '4' } },
                new SymbolDefinition { Value = 5, EncodeSymbol = '5', DecodeSymbols = new[] { '5' } },
                new SymbolDefinition { Value = 6, EncodeSymbol = '6', DecodeSymbols = new[] { '6' } },
                new SymbolDefinition { Value = 7, EncodeSymbol = '7', DecodeSymbols = new[] { '7' } },
                new SymbolDefinition { Value = 8, EncodeSymbol = '8', DecodeSymbols = new[] { '8' } },
                new SymbolDefinition { Value = 9, EncodeSymbol = '9', DecodeSymbols = new[] { '9' } },
                new SymbolDefinition { Value = 10, EncodeSymbol = 'A', DecodeSymbols = new[] { 'A', 'a' } },
                new SymbolDefinition { Value = 11, EncodeSymbol = 'B', DecodeSymbols = new[] { 'B', 'b' } },
                new SymbolDefinition { Value = 12, EncodeSymbol = 'C', DecodeSymbols = new[] { 'C', 'c' } },
                new SymbolDefinition { Value = 13, EncodeSymbol = 'D', DecodeSymbols = new[] { 'D', 'd' } },
                new SymbolDefinition { Value = 14, EncodeSymbol = 'E', DecodeSymbols = new[] { 'E', 'e' } },
                new SymbolDefinition { Value = 15, EncodeSymbol = 'F', DecodeSymbols = new[] { 'F', 'f' } },
                new SymbolDefinition { Value = 16, EncodeSymbol = 'G', DecodeSymbols = new[] { 'G', 'g' } },
                new SymbolDefinition { Value = 17, EncodeSymbol = 'H', DecodeSymbols = new[] { 'H', 'h' } },
                new SymbolDefinition { Value = 18, EncodeSymbol = 'J', DecodeSymbols = new[] { 'J', 'j' } },
                new SymbolDefinition { Value = 19, EncodeSymbol = 'K', DecodeSymbols = new[] { 'K', 'k' } },
                new SymbolDefinition { Value = 20, EncodeSymbol = 'M', DecodeSymbols = new[] { 'M', 'm' } },
                new SymbolDefinition { Value = 21, EncodeSymbol = 'N', DecodeSymbols = new[] { 'N', 'n' } },
                new SymbolDefinition { Value = 22, EncodeSymbol = 'P', DecodeSymbols = new[] { 'P', 'p' } },
                new SymbolDefinition { Value = 23, EncodeSymbol = 'Q', DecodeSymbols = new[] { 'Q', 'q' } },
                new SymbolDefinition { Value = 24, EncodeSymbol = 'R', DecodeSymbols = new[] { 'R', 'r' } },
                new SymbolDefinition { Value = 25, EncodeSymbol = 'S', DecodeSymbols = new[] { 'S', 's' } },
                new SymbolDefinition { Value = 26, EncodeSymbol = 'T', DecodeSymbols = new[] { 'T', 't' } },
                new SymbolDefinition { Value = 27, EncodeSymbol = 'V', DecodeSymbols = new[] { 'V', 'v' } },
                new SymbolDefinition { Value = 28, EncodeSymbol = 'W', DecodeSymbols = new[] { 'W', 'w' } },
                new SymbolDefinition { Value = 29, EncodeSymbol = 'X', DecodeSymbols = new[] { 'X', 'x' } },
                new SymbolDefinition { Value = 30, EncodeSymbol = 'Y', DecodeSymbols = new[] { 'Y', 'y' } },
                new SymbolDefinition { Value = 31, EncodeSymbol = 'Z', DecodeSymbols = new[] { 'Z', 'z' } },
            });

            extraCheckDigits.AddRange(new[]
            {
                new SymbolDefinition { Value = 32, EncodeSymbol = '*', DecodeSymbols = new[] { '*' } },
                new SymbolDefinition { Value = 33, EncodeSymbol = '~', DecodeSymbols = new[] { '~' } },
                new SymbolDefinition { Value = 34, EncodeSymbol = '$', DecodeSymbols = new[] { '$' } },
                new SymbolDefinition { Value = 35, EncodeSymbol = '=', DecodeSymbols = new[] { '=' } },
                new SymbolDefinition { Value = 36, EncodeSymbol = 'U', DecodeSymbols = new[] { 'U', 'u' } },
            });
        }

        public IDictionary<int, char> ValueEncodings
        {
            get
            {
                return this.ToDictionary(s => s.Value, s => s.EncodeSymbol);
            }
        }

        public IDictionary<int, char> CheckDigitEncodings
        {
            get
            {
                return this
                    .Union(extraCheckDigits)
                    .ToDictionary(s => s.Value, s => s.EncodeSymbol);
            }
        }

        public IDictionary<char, int> ValueDecodings
        {
            get
            {
                return this
                    .SelectMany(s => s.DecodeSymbols.Select(d => new { s.Value, DecodeSymbol = d }))
                    .ToDictionary(s => s.DecodeSymbol, s => s.Value);
            }
        }

        public IDictionary<char, int> CheckDigitDecodings
        {
            get
            {
                return this
                    .Union(extraCheckDigits)
                    .SelectMany(s => s.DecodeSymbols.Select(d => new { s.Value, DecodeSymbol = d }))
                    .ToDictionary(s => s.DecodeSymbol, s => s.Value);
            }
        }
    }
}