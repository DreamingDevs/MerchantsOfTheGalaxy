using MerchantsGalaxyGuide.Constants;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// SentenceTypeDefinition class defines the definition of a sentence with rules.
    /// This class also provides the output format which is used as a result of the sentence processing.
    /// </summary>
    public class SentenceTypeDefinition
    {
        /// <summary>
        /// This property defines the type of the sentence.
        /// </summary>
        public SentenceType Type { get; set; }
        /// <summary>
        /// This property holds all the rules associated with a specific sentence type.
        /// These are rules are typically preloaded on the application start.
        /// </summary>
        public List<SentenceRule> Rules { get; set; }
        /// <summary>
        /// This property defines the format of the result which is generated on processing the sentence.
        /// </summary>
        public ResultFormat Result { get; set; }
    }
}
