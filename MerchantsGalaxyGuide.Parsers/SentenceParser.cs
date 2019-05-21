using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;
using System.Linq;

namespace MerchantsGalaxyGuide.Parsers
{
    /// <summary>
    /// Sentence Parser class performs actions on sentences. By leveraging Word parser class,
    /// this class determines the type of sentence.
    /// </summary>
    public class SentenceParser : ISentenceParser
    {
        private readonly IWordParser _wordParser;
        private readonly IConfiguration _configuration;
        private List<Sentence> _sentences;
        public SentenceParser(IWordParser wordParser, IConfiguration configuration)
        {
            _wordParser = wordParser;
            _configuration = configuration;
            _sentences = new List<Sentence>();
        }

        /// <summary>
        /// Parse method parses a given input string content to its equivalent sentence.
        /// </summary>
        /// <param name="content">Input sentence in string format.</param>
        /// <returns>Well formed parsed sentence.</returns>
        public Sentence Parse(string content)
        {
            var sentence = new Sentence(_wordParser.Parse(content));
            sentence.TypeDefinition = DetermineSentenceType(sentence);
            return sentence;
        }

        /// <summary>
        /// This method determines the type of the sentence, for example Declarative, Imperative etc.
        /// Method breaks the input sentence to set of rules and compare the rules against predefined rules
        /// of different sentence types.
        /// </summary>
        /// <param name="sentence">Sentence Object</param>
        /// <returns>Definition of a particular Sentence Type.</returns>
        private SentenceTypeDefinition DetermineSentenceType(Sentence sentence)
        {
            var rules = new List<SentenceRule>();
            foreach(var word in sentence.Words)
            {
                var format = rules.FirstOrDefault(p => p.WordType == word.Type);
                if(format == null)
                {
                    format = new SentenceRule { WordType = word.Type, Occurance = "1" };
                    rules.Add(format);
                }
                else
                {
                    format.Occurance = "*";
                }
            }
            
            return MapAndGetSentenceType(rules);
        }

        private SentenceTypeDefinition MapAndGetSentenceType(List<SentenceRule> currentRules)
        {
            SentenceTypeDefinition typeDefinition = null;
            List<SentenceTypeDefinition> sentenceTypeDefinitions = _configuration.SentenceTypeDefinitions.Where(p => p.Rules.Count == currentRules.Count).ToList();
            if(sentenceTypeDefinitions == null || !sentenceTypeDefinitions.Any())
            {
                return _configuration.SentenceTypeDefinitions.FirstOrDefault(p => p.Type == SentenceType.Invalid);
            }

            foreach(var sentenceTypeDefinition in sentenceTypeDefinitions)
            {
                int rulePosition = 0;
                bool isMatch = true;
                foreach(var rule in sentenceTypeDefinition.Rules)
                {
                    if(rule.WordType == currentRules[rulePosition].WordType && MatchOccurance(rule.Occurance, currentRules[rulePosition].Occurance))
                        isMatch = true;

                    else
                    {
                        isMatch = false;
                        break;
                    }
                    rulePosition++;
                }

                if(isMatch)
                {
                    typeDefinition = sentenceTypeDefinition;
                    break;
                }
            }

            if(typeDefinition == null)
            {
                return _configuration.SentenceTypeDefinitions.FirstOrDefault(p => p.Type == SentenceType.Invalid);
            }

            return typeDefinition;
        }

        private bool MatchOccurance(string expected, string actual)
        {
            if (expected == Keywords.StarDelimiter && (actual == Keywords.StarDelimiter || actual == "1"))
                return true;

            if (expected == "1" && actual == "1")
                return true;

            return false;
        }
    }
}
