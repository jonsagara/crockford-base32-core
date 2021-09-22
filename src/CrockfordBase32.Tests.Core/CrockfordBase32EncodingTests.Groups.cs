﻿namespace CrockfordBase32.Tests.Core
{
    using System.Collections.Generic;
    using Xunit;

    public partial class CrockfordBase32EncodingTests
    {
        [Theory]
        [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, false, "E1G50G1G4080Y3GD1G5GM288G")]
        [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, true, "E1G50G1G4080PY3GD1G5GM2880GG")]
        [InlineData(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 }, false, "FZZZZZZZZZZZZFZZZZZZZZZZZZ")]
        [InlineData(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 }, true, "FZZZZZZZZZZZZBFZZZZZZZZZZZZB")]
        public void CrockfordBase32Encoding_EncodeTwoUlongs(IEnumerable<byte> input, bool checkDigit, string expected)
        {
            var result = CrockfordBase32Encoding.EncodeMultipleBytesAsSeparateUlongs(input, checkDigit);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new ulong[] { 0x000102030405060708, 0x090A0B0C0D0E0F }, "E1G50G1G4080PY3GD1G5GM2880GG")]
        [InlineData(new ulong[] { 0xFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFF }, "FZZZZZZZZZZZZBFZZZZZZZZZZZZB")]
        public void CrockfordBase32Encoding_DecodeTwoUlongs(ulong[] expected, string input)
        {
            var result = CrockfordBase32Encoding.DecodeMultipleCheckDigitEncoded(input);
            var castedResult = CrockfordBase32Encoding.CastAndThrowIfNull(result);

            Assert.Equal(expected, castedResult);
        }

    }
}
