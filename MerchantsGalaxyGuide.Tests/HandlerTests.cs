using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Handlers;
using MerchantsGalaxyGuide.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MerchantsGuideToTheGalaxy.Test
{
    [TestClass]
    public class HandlerTests
    {
        private readonly DeclarativeSentenceHandler _declarativeSentenceHandler;
        private readonly ImperativeSentenceHandler _imperativeSentenceHandler;
        private readonly InterrogativeSentenceHandler _interrogativeSentenceHandler;
        private readonly ISentenceParser _sentenceParser;
        private readonly IConfiguration _configuration;
        private readonly IWordParser _wordParser;
        public HandlerTests()
        {
            _configuration = Utility.LoadConfiguration();
            _wordParser = new WordParser(_configuration);
            _sentenceParser = new SentenceParser(_wordParser, _configuration);
            var handlers = Utility.LoadHandlers(_configuration);

            _declarativeSentenceHandler = handlers.Handlers[SentenceType.Declarative] as DeclarativeSentenceHandler;
            _imperativeSentenceHandler = handlers.Handlers[SentenceType.Imperative] as ImperativeSentenceHandler;
            _interrogativeSentenceHandler = handlers.Handlers[SentenceType.Interrogative] as InterrogativeSentenceHandler;
        }

        [TestMethod]
        public void ValidDelarativeSentenceHandlerTest()
        {
            var sentence = _sentenceParser.Parse("glob is I");
            _declarativeSentenceHandler.Process(sentence);
            Assert.AreEqual(RomanNumber.I, Utility.Context.ConstantsToRomanMap["glob"]);
        }

        [TestMethod]
        public void ValidImperativeSentenceHandlerTest()
        {
            var declarativeSentence = _sentenceParser.Parse("glob is I");
            _declarativeSentenceHandler.Process(declarativeSentence);

            var imperativeSentence = _sentenceParser.Parse("glob glob Silver is 34 Credits");
            _imperativeSentenceHandler.Process(imperativeSentence);
            var productUnitValue = Utility.Context.ProductUnitValues.FirstOrDefault(p => p.Product == "Silver");
            Assert.AreEqual(17, productUnitValue.Value);
            Assert.AreEqual("Credits", productUnitValue.Unit);
        }

        [TestMethod]
        public void IValidnterrogativeSentenceHandlerTest()
        {
            var declarativeSentence = _sentenceParser.Parse("glob is I");
            _declarativeSentenceHandler.Process(declarativeSentence);

            var imperativeSentence = _sentenceParser.Parse("glob glob Silver is 34 Credits");
            _imperativeSentenceHandler.Process(imperativeSentence);

            var interrogativeSentence = _sentenceParser.Parse("how many Credits is glob Silver ?");
            _interrogativeSentenceHandler.Process(interrogativeSentence);
            
            Assert.IsTrue(Utility.Context.Results.Any(p => p.ToString() == "glob Silver is 17 Credits"));
        }
    }
}
