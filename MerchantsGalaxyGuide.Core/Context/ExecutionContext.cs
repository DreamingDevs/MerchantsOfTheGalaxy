using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Core
{
    /// <summary>
    /// ExecutionContext class implements IExecutionContext interface and holds the exceution 
    /// context of the batch processing of sentences.
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        public ExecutionContext()
        {
            ConstantsToRomanMap = new Dictionary<string, RomanNumber>();
            ProductUnitValues = new List<ProductUnitValue>();
            Results = new List<Sentence>();
        }
        /// <summary>
        /// This property holds the input mappings between different constants and their assigned roman numerals.
        /// </summary>
        public Dictionary<string, RomanNumber> ConstantsToRomanMap { get; set; }
        /// <summary>
        /// This property holds the input mappings between product, unit and its associated value.
        /// </summary>
        public List<ProductUnitValue> ProductUnitValues { get; set; }
        /// <summary>
        /// This property holds the result of sentences, for example result of an interrogative sentence.
        /// </summary>
        public List<Sentence> Results { get; set; }
    }
}
