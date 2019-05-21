using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;

namespace MerchantsGalaxyGuide.Handlers
{
    /// <summary>
    /// InvalidSentenceHandler class defines the logic to process a Invalid sentence.
    /// </summary>
    public class InvalidSentenceHandler : BaseSentenceHandler
    {
        private readonly IExecutionContext _executionContext;
        public InvalidSentenceHandler(IExecutionContext executionContext) : base(executionContext)
        {
            _executionContext = executionContext;
        }
        /// <summary>
        /// This method is used to process a invalid sentence.
        /// The output of the process is a standard result defined for Invalid Sentence type.
        /// </summary>
        /// <param name="sentence">Sentence object to be processed.</param>
        public override void Process(Sentence sentence)
        {
            var inValidContent = sentence.TypeDefinition.Result.Format;
            _executionContext.Results.Add(new Sentence(inValidContent));
        }
    }
}
