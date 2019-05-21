using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;
using System.Linq;

namespace MerchantsGalaxyGuide.Core
{
    /// <summary>
    /// Interpreter class is the implementation of IInterpreter interface and provides an APT to process different sentences.
    /// </summary>
    public class Interpreter : IInterpreter
    {
        private readonly ISentenceParser _sentenceParser;
        private readonly IExecutionContext _executionContext;
        private readonly ISentenceHandlers _sentenceHandlers;
        public Interpreter(ISentenceParser sentenceParser, IExecutionContext executionContext, ISentenceHandlers sentenceHandlers)
        {
            _sentenceParser = sentenceParser;
            _executionContext = executionContext;
            _sentenceHandlers = sentenceHandlers;
        }

        /// <summary>
        /// This method process multiple lines of string content and returns the processed results.
        /// </summary>
        /// <param name="lines">Input array of content in string format.</param>
        /// <returns>Output array of content in string format.</returns>
        public string[] Process(string[] lines)
        {
            var sentences = new List<Sentence>();
            foreach(var sentence in lines)
            {
                sentences.Add(_sentenceParser.Parse(sentence));
            }

             ProcessSentences(sentences);
            return _executionContext.Results.Select(p => p.ToString()).ToArray();
        }

        private void ProcessSentences(List<Sentence> sentences)
        {
            foreach(var sentence in sentences)
            {
                var handler = _sentenceHandlers.Handlers.FirstOrDefault(p => p.Key == sentence.TypeDefinition.Type).Value;
                handler.Process(sentence);
            }
        }
    }
}
