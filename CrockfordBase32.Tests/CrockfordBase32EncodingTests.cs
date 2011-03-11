using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrockfordBase32.Tests
{
    [TestClass]
    public class CrockfordBase32EncodingTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CrockfordBase32Encoding_Encode_ShouldThrowArgumentOutOfRangeExceptionForNegativeNumber()
        {
            try
            {
                new CrockfordBase32Encoding().Encode(-1, false);
                Assert.Fail("Expected exception was never thrown");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("number", ex.ParamName);
            }
        }

        [TestMethod]
        [DeploymentItem(@"CrockfordBase32.Tests\TestData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"|DataDirectory|\TestData.xml", "test", DataAccessMethod.Sequential)]
        public void CrockfordBase32Encoding_Encode_ShouldReturnExpectedResult()
        {
            var number = int.Parse((string)TestContext.DataRow["number"]);
            var expected = (string)TestContext.DataRow["encodedString"];

            var actual = new CrockfordBase32Encoding().Encode(number, false);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem(@"CrockfordBase32.Tests\TestData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"|DataDirectory|\TestData.xml", "test", DataAccessMethod.Sequential)]
        public void CrockfordBase32Encoding_Encode_ShouldReturnExpectedResultWithCheckDigit()
        {
            var number = int.Parse((string)TestContext.DataRow["number"]);
            var expected = (string)TestContext.DataRow["encodedString"];
            var checkDigit = (string)TestContext.DataRow["checkDigit"];

            var actual = new CrockfordBase32Encoding().Encode(number, true);

            Assert.AreEqual(expected + checkDigit, actual);
        }

        [TestMethod]
        public void CrockfordBase32Encoding_Decode_ShouldThrowArgumentNullExceptionForNullInput()
        {
            try
            {
                new CrockfordBase32Encoding().Decode(null, false);
                Assert.Fail("Expected exception was never thrown");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("encodedString", ex.ParamName);
            }
        }

        [TestMethod]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForBadCharacter()
        {
            Assert.IsNull(new CrockfordBase32Encoding().Decode("/", false));
        }

        [TestMethod]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForBadCharacterWithinValidOtherwiseInput()
        {
            Assert.IsNull(new CrockfordBase32Encoding().Decode("a/b", false));
        }

        [TestMethod]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForEmptyString()
        {
            Assert.IsNull(new CrockfordBase32Encoding().Decode(string.Empty, false));
        }

        [TestMethod]
        [DeploymentItem(@"CrockfordBase32.Tests\TestData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"|DataDirectory|\TestData.xml", "test", DataAccessMethod.Sequential)]
        public void CrockfordBase32Encoding_Decode_ShouldReturnExpectedResult()
        {
            var encodedString = (string)TestContext.DataRow["encodedString"];
            var expected = int.Parse((string)TestContext.DataRow["number"]);
            
            var actual = new CrockfordBase32Encoding().Decode(encodedString, false);

            Assert.AreEqual(expected, actual);
        }
    }
}