using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Parsers;
using MerchantsGalaxyGuide.Contracts;

namespace MerchantsGuideToTheGalaxy.Test
{
    [TestClass]
    public class WordParserTests
    {
        private readonly IWordParser _wordParser;
        private readonly IConfiguration _configuration;
        public WordParserTests()
        {
            _configuration = Utility.LoadConfiguration();
            _wordParser = new WordParser(_configuration);
        }

        [TestMethod]
        public void ImperativeWordParserTest()
        {
            var words = _wordParser.Parse("glob prok Gold is 57800 Credits");

            Assert.IsTrue(1 <= words.Count(p => p.Type == WordType.Constant));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Product));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Operator));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Number));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Unit));
        }

        [TestMethod]
        public void InterrogativeWordParserTest()
        {
            var words = _wordParser.Parse("how many Credits is glob prok Iron ?");

            Assert.AreEqual(1, words.Count(p => p.Type == WordType.PrimaryQuery));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.SupportingQuery));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Unit));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Operator));
            Assert.IsTrue(1 <= words.Count(p => p.Type == WordType.Constant));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.Product));
            Assert.AreEqual(1, words.Count(p => p.Type == WordType.QueryDelimiter));
        }
    }
}
