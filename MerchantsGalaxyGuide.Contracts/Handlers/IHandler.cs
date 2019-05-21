using MerchantsGalaxyGuide.Model;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface which defines the process of a sentence.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// This methods defines the process of a sentence.
        /// </summary>
        /// <param name="sentence">Input sentence as Sentence object.</param>
        void Process(Sentence sentence);
    }
}
