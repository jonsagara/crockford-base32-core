using System.Linq;
using Xunit;

namespace CrockfordBase32.Tests.Core
{
    public class SymbolDefinitionsTests
    {
        [Fact]
        public void SymbolDefinitions_ValueEncodings_ShouldReturn32Entries()
        {
            var result = new SymbolDefinitions().ValueEncodings;
            Assert.Equal(32, result.Count);
        }

        [Fact]
        public void SymbolDefinitions_ValueEncodings_ShouldReturnAllDistinctEntries()
        {
            var result = new SymbolDefinitions().ValueEncodings;
            Assert.Equal(result.Count, result.Select(s => s.Value).Distinct().Count());
        }

        [Fact]
        public void SymbolDefinitions_CheckDigitEncodings_ShouldReturn37Entries()
        {
            var result = new SymbolDefinitions().CheckDigitEncodings;
            Assert.Equal(37, result.Count);
        }

        [Fact]
        public void SymbolDefinitions_CheckDigitEncodings_ShouldReturnAllDistinctEntries()
        {
            var result = new SymbolDefinitions().CheckDigitEncodings;
            Assert.Equal(result.Count, result.Select(s => s.Value).Distinct().Count());
        }

        [Fact]
        public void SymbolDefinitions_ValueDecodings_ShouldReturnReciprocalEntriesForAllEncodings()
        {
            var symbols = new SymbolDefinitions();
            var encodeMappings = symbols.ValueEncodings;
            var decodeMappings = symbols.ValueDecodings;

            foreach (var encodeMapping in encodeMappings)
            {
                var encodeValue = encodeMapping.Key;
                var encodeChar = encodeMapping.Value;
                var decodeValue = decodeMappings[encodeChar];

                Assert.Equal(encodeValue, decodeValue);
            }
        }

        [Fact]
        public void SymbolDefinitions_CheckDigitDecodings_ShouldReturnReciprocalEntriesForAllEncodings()
        {
            var symbols = new SymbolDefinitions();
            var encodeMappings = symbols.CheckDigitEncodings;
            var decodeMappings = symbols.CheckDigitDecodings;

            foreach (var encodeMapping in encodeMappings)
            {
                var encodeValue = encodeMapping.Key;
                var encodeChar = encodeMapping.Value;
                var decodeValue = decodeMappings[encodeChar];

                Assert.Equal(encodeValue, decodeValue);
            }
        }
    }
}
