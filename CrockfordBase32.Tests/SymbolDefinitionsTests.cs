using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrockfordBase32.Tests
{
    [TestClass]
    public class SymbolDefinitionsTests
    {
        [TestMethod]
        public void SymbolDefinitions_EncodeMappings_ShouldReturn32Entries()
        {
            var result = new SymbolDefinitions().EncodeMappings;
            Assert.AreEqual(32, result.Count);
        }

        [TestMethod]
        public void SymbolDefinitions_DecodeMappings_ShouldReturnReciprocalEntriesForAllEncodeMappings()
        {
            var symbols = new SymbolDefinitions();
            var encodeMappings = symbols.EncodeMappings;
            var decodeMappings = symbols.DecodeMappings;

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