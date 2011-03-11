using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrockfordBase32.Tests
{
    [TestClass]
    public class SymbolDefinitionsTests
    {
        [TestMethod]
        public void SymbolDefinitions_ValueEncodings_ShouldReturn32Entries()
        {
            var result = new SymbolDefinitions().ValueEncodings;
            Assert.AreEqual(32, result.Count);
        }

        [TestMethod]
        public void SymbolDefinitions_ValueEncodings_ShouldReturnAllDistinctEntries()
        {
            var result = new SymbolDefinitions().ValueEncodings;
            Assert.AreEqual(result.Count, result.Select(s => s.Value).Distinct().Count());
        }

        [TestMethod]
        public void SymbolDefinitions_CheckDigitEncodings_ShouldReturn37Entries()
        {
            var result = new SymbolDefinitions().CheckDigitEncodings;
            Assert.AreEqual(37, result.Count);
        }

        [TestMethod]
        public void SymbolDefinitions_CheckDigitEncodings_ShouldReturnAllDistinctEntries()
        {
            var result = new SymbolDefinitions().CheckDigitEncodings;
            Assert.AreEqual(result.Count, result.Select(s => s.Value).Distinct().Count());
        }

        [TestMethod]
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

                Assert.AreEqual(encodeValue, decodeValue);
            }
        }

        [TestMethod]
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

                Assert.AreEqual(encodeValue, decodeValue);
            }
        }
    }
}