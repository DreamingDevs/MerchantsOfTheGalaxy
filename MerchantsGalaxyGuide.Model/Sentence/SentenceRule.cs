using MerchantsGalaxyGuide.Constants;

namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// SentenceRule class defines a rule for a particular word type within the sentence.
    /// </summary>
    public class SentenceRule
    {
        /// <summary>
        /// This property holds the Word Type.
        /// </summary>
        public WordType WordType { get; set; }
        /// <summary>
        /// This property holds the number of occurances of the word with in a sentence.
        /// </summary>
        public string Occurance { get; set; }
    }
}
