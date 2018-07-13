using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CrockfordBase32.Tests.Core
{
    public class CrockfordBase32EncodingTests
    {
        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldReturnASingleChunkFor0()
        {
            const int input = 0;
            var expected = new byte[] { 0 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldNotChunkANumberThatFitsIn5Bits()
        {
            const int input = 31;
            var expected = new byte[] { 31 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldChunkANumberThatFitsIn6Bits()
        {
            const int input = 32;
            var expected = new byte[] { 1, 0 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldChunkANumberThatFitsIn13Bits()
        {
            const int input = 4546;
            var expected = new byte[] { 4, 14, 2 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CrockfordBase32Encoding_Encode_ShouldReturnExpectedResult(TestDatum datum)
        {
            var number = ulong.Parse(datum.Number);
            var expected = datum.EncodedString;

            var actual = new CrockfordBase32Encoding().Encode(number, false);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CrockfordBase32Encoding_Encode_ShouldReturnExpectedResultWithCheckDigit(TestDatum datum)
        {
            var number = ulong.Parse(datum.Number);
            var expected = datum.EncodedString;
            var checkDigit = datum.CheckDigit;

            var actual = new CrockfordBase32Encoding().Encode(number, true);

            Assert.Equal(expected + checkDigit, actual);
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldThrowArgumentNullExceptionForNullInput()
        {
            try
            {
                new CrockfordBase32Encoding().Decode(null, false);
                Assert.True(false, "Expected exception was never thrown");
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("encodedString", ex.ParamName);
            }
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForBadCharacter()
        {
            Assert.Null(new CrockfordBase32Encoding().Decode("/", false));
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForBadCharacterWithinValidOtherwiseInput()
        {
            Assert.Null(new CrockfordBase32Encoding().Decode("a/b", false));
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForEmptyString()
        {
            Assert.Null(new CrockfordBase32Encoding().Decode(string.Empty, false));
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnExpectedResult(TestDatum datum)
        {
            var encodedString = datum.EncodedString;
            var expected = ulong.Parse(datum.Number);

            var actual = new CrockfordBase32Encoding().Decode(encodedString, false);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnExpectedResultWithValidCheckDigit(TestDatum datum)
        {
            var encodedString = datum.EncodedString;
            var checkDigit = datum.CheckDigit;
            var expected = ulong.Parse(datum.Number);

            var actual = new CrockfordBase32Encoding().Decode(encodedString + checkDigit, true);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForInvalidCheckDigit(TestDatum datum)
        {
            var encodedString = datum.EncodedString;

            var actual = new CrockfordBase32Encoding().Decode(encodedString + '#', true);

            Assert.Null(actual);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForIncorrectCheckDigit(TestDatum datum)
        {
            var encodedString = datum.EncodedString;
            var checkDigit = datum.CheckDigit;

            checkDigit = checkDigit.Equals("a", StringComparison.OrdinalIgnoreCase) ? "b" : "a";

            var actual = new CrockfordBase32Encoding().Decode(encodedString + checkDigit, true);

            Assert.Null(actual);
        }
    }
}
