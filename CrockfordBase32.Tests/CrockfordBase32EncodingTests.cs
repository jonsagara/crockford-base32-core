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
    }
}