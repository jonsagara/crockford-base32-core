using System;
using System.Collections.Generic;
using System.Linq;

namespace CrockfordBase32
{
    public class CrockfordBase32Encoding
    {
        const int Base = 32;
        const int CheckDigitBase = 37;

        static readonly IDictionary<int, char> valueEncodings;
        static readonly IDictionary<int, char> checkDigitEncodings;
        static readonly IDictionary<char, int> valueDecodings;
        static readonly IDictionary<char, int> checkDigitDecodings;
        static CrockfordBase32Encoding()
        {
            var symbols = new SymbolDefinitions();
            valueEncodings = symbols.ValueEncodings;
            checkDigitEncodings = symbols.CheckDigitEncodings;
            valueDecodings = symbols.ValueDecodings;
            checkDigitDecodings = symbols.CheckDigitDecodings;
        }

        public string Encode(int number, bool includeCheckDigit)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("number", number, "Only non-negative values are supported by this encoding mechanism.");

            if (number == 0)
            {
                var result = valueEncodings[0].ToString();
                if (includeCheckDigit) result += checkDigitEncodings[0].ToString();
                return result;
            }

            var characters = new List<char>();

            if (includeCheckDigit)
            {
                var checkValue = number % CheckDigitBase;
                characters.Add(checkDigitEncodings[checkValue]);
            }

            var nextBase = 1 * Base;
            while (number > 0)
            {
                var currentValue = number % nextBase;
                number = (number - currentValue) / Base;
                characters.Add(valueEncodings[currentValue]);
                nextBase *= Base;
            }

            return new string(((IEnumerable<char>)characters).Reverse().ToArray());
        }

        public int? Decode(string encodedString, bool treatLastCharacterAsCheckDigit)
        {
            if (encodedString == null)
                throw new ArgumentNullException("encodedString");

            if (encodedString.Length == 0)
                return null;

            IEnumerable<char> charactersInReverse = encodedString.Reverse().ToArray();

            int? expectedCheckValue = null;
            if (treatLastCharacterAsCheckDigit)
            {
                var checkDigit = charactersInReverse.First();
                if (!checkDigitDecodings.ContainsKey(checkDigit)) return null;
                expectedCheckValue = checkDigitDecodings[checkDigit];

                charactersInReverse = charactersInReverse.Skip(1);
            }

            var number = 0;
            var currentBase = 1;
            foreach (var character in charactersInReverse)
            {
                if (!valueDecodings.ContainsKey(character)) return null;

                var value = valueDecodings[character];
                number += value*currentBase;

                currentBase *= Base;
            }

            if (expectedCheckValue.HasValue &&
                number % CheckDigitBase != expectedCheckValue)
                return null;

            return number;
        }
    }
}