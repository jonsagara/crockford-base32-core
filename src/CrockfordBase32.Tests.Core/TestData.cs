using System.Collections;
using System.Collections.Generic;

namespace CrockfordBase32.Tests.Core
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new TestDatum { Number = "0", EncodedString = "0", CheckDigit = "0" } };
            yield return new object[] { new TestDatum { Number = "1", EncodedString = "1", CheckDigit = "1" } };
            yield return new object[] { new TestDatum { Number = "2", EncodedString = "2", CheckDigit = "2" } };
            yield return new object[] { new TestDatum { Number = "3", EncodedString = "3", CheckDigit = "3" } };
            yield return new object[] { new TestDatum { Number = "4", EncodedString = "4", CheckDigit = "4" } };
            yield return new object[] { new TestDatum { Number = "5", EncodedString = "5", CheckDigit = "5" } };
            yield return new object[] { new TestDatum { Number = "6", EncodedString = "6", CheckDigit = "6" } };
            yield return new object[] { new TestDatum { Number = "7", EncodedString = "7", CheckDigit = "7" } };
            yield return new object[] { new TestDatum { Number = "8", EncodedString = "8", CheckDigit = "8" } };
            yield return new object[] { new TestDatum { Number = "9", EncodedString = "9", CheckDigit = "9" } };
            yield return new object[] { new TestDatum { Number = "10", EncodedString = "A", CheckDigit = "A" } };
            yield return new object[] { new TestDatum { Number = "11", EncodedString = "B", CheckDigit = "B" } };
            yield return new object[] { new TestDatum { Number = "12", EncodedString = "C", CheckDigit = "C" } };
            yield return new object[] { new TestDatum { Number = "13", EncodedString = "D", CheckDigit = "D" } };
            yield return new object[] { new TestDatum { Number = "14", EncodedString = "E", CheckDigit = "E" } };
            yield return new object[] { new TestDatum { Number = "15", EncodedString = "F", CheckDigit = "F" } };
            yield return new object[] { new TestDatum { Number = "16", EncodedString = "G", CheckDigit = "G" } };
            yield return new object[] { new TestDatum { Number = "17", EncodedString = "H", CheckDigit = "H" } };
            yield return new object[] { new TestDatum { Number = "18", EncodedString = "J", CheckDigit = "J" } };
            yield return new object[] { new TestDatum { Number = "19", EncodedString = "K", CheckDigit = "K" } };
            yield return new object[] { new TestDatum { Number = "20", EncodedString = "M", CheckDigit = "M" } };
            yield return new object[] { new TestDatum { Number = "21", EncodedString = "N", CheckDigit = "N" } };
            yield return new object[] { new TestDatum { Number = "22", EncodedString = "P", CheckDigit = "P" } };
            yield return new object[] { new TestDatum { Number = "23", EncodedString = "Q", CheckDigit = "Q" } };
            yield return new object[] { new TestDatum { Number = "24", EncodedString = "R", CheckDigit = "R" } };
            yield return new object[] { new TestDatum { Number = "25", EncodedString = "S", CheckDigit = "S" } };
            yield return new object[] { new TestDatum { Number = "26", EncodedString = "T", CheckDigit = "T" } };
            yield return new object[] { new TestDatum { Number = "27", EncodedString = "V", CheckDigit = "V" } };
            yield return new object[] { new TestDatum { Number = "28", EncodedString = "W", CheckDigit = "W" } };
            yield return new object[] { new TestDatum { Number = "29", EncodedString = "X", CheckDigit = "X" } };
            yield return new object[] { new TestDatum { Number = "30", EncodedString = "Y", CheckDigit = "Y" } };
            yield return new object[] { new TestDatum { Number = "31", EncodedString = "Z", CheckDigit = "Z" } };
            yield return new object[] { new TestDatum { Number = "32", EncodedString = "10", CheckDigit = "*" } };
            yield return new object[] { new TestDatum { Number = "33", EncodedString = "11", CheckDigit = "~" } };
            yield return new object[] { new TestDatum { Number = "34", EncodedString = "12", CheckDigit = "$" } };
            yield return new object[] { new TestDatum { Number = "35", EncodedString = "13", CheckDigit = "=" } };
            yield return new object[] { new TestDatum { Number = "36", EncodedString = "14", CheckDigit = "U" } };
            yield return new object[] { new TestDatum { Number = "37", EncodedString = "15", CheckDigit = "0" } };
            yield return new object[] { new TestDatum { Number = "64", EncodedString = "20", CheckDigit = "V" } };
            yield return new object[] { new TestDatum { Number = "468", EncodedString = "EM", CheckDigit = "R" } };
            yield return new object[] { new TestDatum { Number = "3783", EncodedString = "3P7", CheckDigit = "9" } };
            yield return new object[] { new TestDatum { Number = "4546", EncodedString = "4E2", CheckDigit = "*" } };
            yield return new object[] { new TestDatum { Number = "65535", EncodedString = "1ZZZ", CheckDigit = "8" } };
            // UInt64::MaxValue
            yield return new object[] { new TestDatum { Number = "18446744073709551615", EncodedString = "FZZZZZZZZZZZZ", CheckDigit = "B" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
