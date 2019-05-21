using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MerchantsGuideToTheGalaxy.Test
{
    [TestClass]
    public class SentenceParserTests
    {
        private readonly ISentenceParser _sentenceParser;
        private readonly IConfiguration _configuration;
        private readonly IWordParser _wordParser;
        public SentenceParserTests()
        {
            _configuration = Utility.LoadConfiguration();
            _wordParser = new WordParser(_configuration);
            _sentenceParser = new SentenceParser(_wordParser, _configuration);
        }

        [TestMethod]
        public void ValidDelarativeSentenceTest()
        {
            var sentence = _sentenceParser.Parse("one is I");
            Assert.AreEqual(SentenceType.Declarative, sentence.TypeDefinition.Type);
        }

        [TestMethod]
        public void DelarativeSentenceWithMissingInformationTest()
        {
            var sentence = _sentenceParser.Parse("one is");
            Assert.AreEqual(SentenceType.Invalid, sentence.TypeDefinition.Type);
        }

        [TestMethod]
        public void InvalidDelarativeSentenceTest()
        {
            var sentence = _sentenceParser.Parse("one is is I");
            Assert.AreEqual(SentenceType.Invalid, sentence.TypeDefinition.Type);
        }
    }
}
