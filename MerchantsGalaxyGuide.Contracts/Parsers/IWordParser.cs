using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface which defines the a Word parser.
    /// </summary>
    public interface IWordParser
    {
        /// <summary>
        /// This methods defines the parse operation of all the words of a given sentence.
        /// </summary>
        /// <param name="sentence">sentence string.</param>
        /// <returns>List of ContentWords.</returns>
        List<ContentWord> Parse(string sentence);
    }
}
