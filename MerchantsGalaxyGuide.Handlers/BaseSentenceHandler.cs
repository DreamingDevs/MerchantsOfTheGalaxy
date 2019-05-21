using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;

namespace MerchantsGalaxyGuide.Handlers
{
    /// <summary>
    /// BaseSentenceHandler is an abstract class which serves as the base implementation of IHandler.
    /// This class is inherited by handlers to process different types of sentences.
    /// </summary>
    public abstract class BaseSentenceHandler : IHandler
    {
        private readonly IExecutionContext _executionContext;
        public BaseSentenceHandler(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }
        /// <summary>
        /// Process is an abstract method which should be overriden by derived classes to process a given sentence.
        /// </summary>
        /// <param name="sentence">Sentence object to be processed.</param>
        public abstract void Process(Sentence sentence);
    }
}
