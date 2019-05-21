using MerchantsGalaxyGuide.Model;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface which defines the a Sentence parser.
    /// </summary>
    public interface ISentenceParser
    {
        /// <summary>
        /// This methods defines the parse operation of a sentence.
        /// </summary>
        /// <param name="content">string content.</param>
        /// <returns>Fully parsed Sentence.</returns>
        Sentence Parse(string content);
    }
}
