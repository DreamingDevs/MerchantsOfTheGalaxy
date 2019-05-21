using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface to hold the exceution context of the batch processing of sentences.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// This property holds the input mappings between different constants and their assigned roman numerals.
        /// </summary>
        Dictionary<string, RomanNumber> ConstantsToRomanMap { get; set; }
        /// <summary>
        /// This property holds the input mappings between product, unit and its associated value.
        /// </summary>
        List<ProductUnitValue> ProductUnitValues { get; set; }
        /// <summary>
        /// This property holds the result of sentences, for example result of an interrogative sentence.
        /// </summary>
        List<Sentence> Results { get; set; }
    }
}
