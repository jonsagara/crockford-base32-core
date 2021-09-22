namespace CrockfordBase32
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CrockfordBase32Encoding
    {
        public const int Base = 32;

        public const int CheckDigitBase = 37;

        private static readonly IDictionary<int, char> valueEncodings;

        private static readonly IDictionary<int, char> checkDigitEncodings;

        private static readonly IDictionary<char, int> valueDecodings;

        private static readonly IDictionary<char, int> checkDigitDecodings;

        static CrockfordBase32Encoding()
        {
            SymbolDefinitions symbolDefinitions = new SymbolDefinitions();
            valueEncodings = symbolDefinitions.ValueEncodings;
            checkDigitEncodings = symbolDefinitions.CheckDigitEncodings;
            valueDecodings = symbolDefinitions.ValueDecodings;
            checkDigitDecodings = symbolDefinitions.CheckDigitDecodings;
        }

        public static string Encode(ulong input, bool includeCheckDigit)
        {
            IEnumerable<char> enumerable = from chunk in SplitInto5BitChunks(input)
                                           select valueEncodings[chunk];
            if (includeCheckDigit)
            {
                int key = (int)(input % CheckDigitBase);
                enumerable = enumerable.Concat(new char[1]
                {
                checkDigitEncodings[key]
                });
            }

            return new string(enumerable.ToArray());
        }

        public static IEnumerable<ulong> SplitIntoUlongs(IEnumerable<byte> input)
        {
            const int chunkLength = sizeof(ulong);
            int chunks = (int)Math.Ceiling((double)(input.Count()) / chunkLength);
            var paddedBytes = new byte[chunks * chunkLength];
            Array.Fill<byte>(paddedBytes, 0);
            input.ToArray().CopyTo(paddedBytes, 0);
            var memoryBytes = new Memory<byte>(paddedBytes);

            List<ulong> enumerable = new List<ulong>(chunks);
            for (int i = 0; i < paddedBytes.Length; i += chunkLength)
            {
                var chunk = memoryBytes.Slice(i, chunkLength)
                        .ToArray();
                var u = BitConverter.ToUInt64(
                    value: chunk);
                enumerable.Add(u);
            }

            return enumerable;
        }

        public static IEnumerable<string> SplitEncodedByCheckDigits(string input)
        {
            List<string> results = new List<string>();
            char[] digits = checkDigitDecodings.Keys.ToArray<char>();
            int lastOffset = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (digits.Contains(input[i]))
                {
                    results.Add(input.Substring(lastOffset, 1 + i - lastOffset));
                    lastOffset = i + 1;
                }
            }

            return results;
        }

        public static string EncodeMultipleBytesAsSeparateUlongs(IEnumerable<byte> inputBytes, bool includeCheckDigit)
        {
            List<char> output = new List<char>();
            IEnumerable<ulong> inputLongs = SplitIntoUlongs(inputBytes);
            int i = 0;

            foreach (ulong input in inputLongs)
            {
                IEnumerable<char> enumerable = from chunk in SplitInto5BitChunks(input)
                                               select valueEncodings[chunk];

                output.AddRange(enumerable);

                if (includeCheckDigit)
                {
                    int key = (int)(input % CheckDigitBase);
                    output.Add(checkDigitEncodings[key]);
                }
            }

            return new string(output.ToArray());
        }

        internal static IEnumerable<byte> SplitInto5BitChunks(ulong input)
        {
            List<byte> list = new List<byte>();
            do
            {
                ulong num = input << 59 >> 59;
                list.Insert(0, (byte)num);
                input >>= 5;
            }
            while (input != 0);
            return list;
        }

        public static IEnumerable<ulong?> DecodeMultipleCheckDigitEncoded(string encodedString)
        {
            List<ulong?> result = new List<ulong?>();
            foreach (var group in SplitEncodedByCheckDigits(encodedString))
            {
                result.Add(Decode(group, true));
            }

            return result;
        }

        public static ulong? Decode(string encodedString, bool treatLastCharacterAsCheckDigit)
        {
            if (encodedString == null)
            {
                throw new ArgumentNullException("encodedString");
            }

            if (encodedString.Length == 0)
            {
                return null;
            }

            IEnumerable<char> enumerable = encodedString.Reverse().ToArray();
            int? num = null;
            if (treatLastCharacterAsCheckDigit)
            {
                char key = enumerable.First();
                if (!checkDigitDecodings.ContainsKey(key))
                {
                    return null;
                }

                num = checkDigitDecodings[key];
                enumerable = enumerable.Skip(1);
            }

            ulong num2 = 0uL;
            ulong num3 = 1uL;
            foreach (char item in enumerable)
            {
                if (!valueDecodings.ContainsKey(item))
                {
                    return null;
                }

                int num4 = valueDecodings[item];
                num2 += (ulong)((long)num4 * (long)num3);
                num3 *= Base;
            }

            if (num.HasValue && (int?)(num2 % CheckDigitBase) != num)
            {
                return null;
            }

            return num2;
        }
    }
}
