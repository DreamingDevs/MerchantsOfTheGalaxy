using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MerchantsGuideToTheGalaxy.Test
{
    [TestClass]
    public class RomanNumberTests
    {
        [TestMethod]
        public void TwoDigitRomanNumeralAdditionTest()
        {
            var result = RomanNumbersUtility.ConvertRomantoNumber(new RomanNumber[] { RomanNumber.I, RomanNumber.I });
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ThreeDigitRomanNumeralAdditionTest()
        {
            var result = RomanNumbersUtility.ConvertRomantoNumber(new RomanNumber[] {RomanNumber.V, RomanNumber.I, RomanNumber.I });
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void TwoDigitRomanNumeralSubtractionTest()
        {
            var result = RomanNumbersUtility.ConvertRomantoNumber(new RomanNumber[] { RomanNumber.C, RomanNumber.D });
            Assert.AreEqual(400, result);
        }
    }
}
