using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using System;
using System.Linq;

namespace MerchantsGalaxyGuide.Handlers
{
    /// <summary>
    /// DeclarativeSentenceHandler class defines the logic to process a declarative sentence.
    /// </summary>
    public class DeclarativeSentenceHandler : BaseSentenceHandler
    {
        private readonly IExecutionContext _executionContext;
        public DeclarativeSentenceHandler(IExecutionContext executionContext) : base(executionContext)
        {
            _executionContext = executionContext;
        }
        /// <summary>
        /// This method is used to process a declarative sentence.
        /// As a result of the process, Execution Context is updated with Constants to Roman numerals mappings.
        /// </summary>
        /// <param name="sentence">Sentence object to be processed.</param>
        public override void Process(Sentence sentence)
        {
            var constant = sentence.Words.First().Content;
            var roman = (RomanNumber)Enum.Parse(typeof(RomanNumber), sentence.Words.Last().Content);

            if (!_executionContext.ConstantsToRomanMap.Any(p => p.Key == constant))
                _executionContext.ConstantsToRomanMap.Add(constant, roman);
            else
                _executionContext.ConstantsToRomanMap[constant] = roman;
        }
    }
}
